//routes.push({ pattern: /^module\/completeqc$/, action: function (match: RegExpExecArray) { return CompleteQcController.getModuleScreen(); } });

class CompleteQc {
    Module: string = 'CompleteQc';
    caption: string = Constants.Modules.Inventory.children.CompleteQc.caption;
    nav: string = Constants.Modules.Inventory.children.CompleteQc.nav;
    id: string = Constants.Modules.Inventory.children.CompleteQc.id;
    itemId: string = '';
    itemQcId: string = '';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.itemId = '';
            this.itemQcId = '';
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');
        $form.find('div[data-datafield="Code"] input').select().focus();
        // ----------
        $form.find('.completeqc').on('click', () => {
            const request: any = {
                QcAnyway: false,
                Code: FwFormField.getValue2($form.find('div[data-datafield="Code"]'))
            };

            this.completeQc($form, request);
        })
        // ----------
        $form.find('.code').on('keypress', e => {
            if (e.which === 13) {
                const request: any = {
                    QcAnyway: false,
                    Code: FwFormField.getValue2($form.find('div[data-datafield="Code"]'))
                };

                this.completeQc($form, request);
            }
        });
        // ----------
        $form.find('.updateqc').on('click', () => {
            const request: any = {
                ItemId: this.itemId,
                ItemQcId: this.itemQcId,
                ConditionId: FwFormField.getValue2($form.find('div[data-datafield="Condition"]')),
                Note: FwFormField.getValue2($form.find('div[data-datafield="Note"]')),
                CurrentFootCandles: FwFormField.getValueByDataField($form, 'CurrentFootCandles'),
                RequiredFootCandles: FwFormField.getValueByDataField($form, 'RequiredFootCandles'),
                SoftwareEffectiveDate: FwFormField.getValueByDataField($form, 'SoftwareEffectiveDate'),
                CurrentSoftwareVersion: FwFormField.getValueByDataField($form, 'CurrentSoftwareVersion'),
                RequiredSoftwareVersion: FwFormField.getValueByDataField($form, 'RequiredSoftwareVersion'),
            }
            this.updateQc($form, request);
        })

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        // ----------
        const $inventoryAttributeValueGrid = $form.find('div[data-grid="InventoryAttributeValueGrid"]');
        const $inventoryAttributeValueGridControl = FwBrowse.loadGridFromTemplate('InventoryAttributeValueGrid');
        $inventoryAttributeValueGrid.empty().append($inventoryAttributeValueGridControl);
        $inventoryAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                InventoryId: $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
            };
        });
        $inventoryAttributeValueGridControl.data('beforesave', function (request) {
            request.InventoryId = $form.find('div.fwformfield[data-datafield="InventoryId"] input').val()
        });
        FwBrowse.init($inventoryAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($inventoryAttributeValueGridControl);
    }
    //----------------------------------------------------------------------------------------------
    completeQc($form, request) {
        try {
            const $completed = $form.find('.qcsuccess');
            FwAppData.apiMethod(true, 'POST', 'api/v1/completeqc/completeqcitem', request, FwServices.defaultTimeout, response => {
                $form.find('div.msg').html('');
                FwFormField.setValueByDataField($form, 'ICode', '');
                FwFormField.setValueByDataField($form, 'Description', '');
                FwFormField.setValueByDataField($form, 'Condition', '');
                FwFormField.setValueByDataField($form, 'Note', '');
                if (response.success) {
                    $completed.show();
                    if (!response.ShowFootCandles) {
                        $form.find('.foot-candles').hide();
                    }
                    if (!response.ShowSoftwareVersion) {
                        $form.find('.software-version').hide();
                    }
                    $form.find('.code').removeClass('error');
                    $form.find('.success-msg:not(.update)').html(`<div style="margin:0px 0px 0px 5px;"><span>QC Completed Successfully</span></div>`);
                    FwFormField.setValueByDataField($form, 'ICode', response.ICode);
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Condition', response.ConditionId, response.Condition);
                    this.itemId = response.ItemId;
                    this.itemQcId = response.ItemQcId;
                    const $inventoryAttributeValueGrid = $form.find('[data-name="InventoryAttributeValueGrid"]');
                    FwBrowse.search($inventoryAttributeValueGrid);
                    $form.find('div[data-datafield="Code"] input').select().focus();
                } else {
                    $completed.hide();
                    $form.find('.code').addClass('error');
                    $form.find('div.error-msg').html(`<div style="margin:0px 0px 0px 5px;"><span>${response.msg}</span></div>`);
                    $form.find('div[data-datafield="Code"] input').select().focus();
                }
            }, null, $form);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    updateQc($form, request) {
        try {
            FwAppData.apiMethod(true, 'POST', 'api/v1/completeqc/updateqcitem', request, FwServices.defaultTimeout, function onSuccess(response) {
                $form.find('div[data-datafield="Code"] input').select().focus();
                $form.find('div.msg').html('');
                $form.find('.qcsuccess').hide();
                $form.find('.success-msg.update').html(`<div style="margin:5px 0px 0px 5px;"><span>QC Updated Successfully</span></div>`);
            }, null, $form);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
           <div id="completeqcform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Complete QC" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="CompleteQcController">
              <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs">
                </div>
                <div class="tabpages">
                  <div class="flexpage">
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Complete QC for Rental Items">
                        <div class="flexrow"><span>Scan or type the Bar Code or Serial Number of an item below and hit Enter to complete the QC of the item.</span></div>
                        <div class="flexrow" style="margin-bottom:20px;"><span>Optionally update the Condition, Notes, etc. for this item.</span></div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="code fwcontrol fwformfield" data-caption="Bar Code/Serial No" data-datafield="Code" style="flex:0 1 300px;"></div>
                          <div class="fwformcontrol completeqc" data-type="button" style="flex:0 1 105px;margin:16px 0 10px 10px;">Complete QC</div>
                        </div>
                        <div class="msg success-msg"></div>
                        <div class="msg error-msg"></div>
                        <div class="flexrow" style="max-width:800px">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="flex:1 1 125px;" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Description" style="flex:1 1 350px;" data-enabled="false"></div>
                        </div>
                        <div class="qcsuccess" style="display:none;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Condition" data-datafield="Condition" style="flex:0 1 300px;" data-validationname="InventoryConditionValidation"></div>
                          </div>
                          <div class="flexrow">
                            <div class="flexcolumn" style="flex:1 1 650px;">
                              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Attributes">
                                <div class="flexrow">
                                  <div data-control="FwGrid" data-grid="InventoryAttributeValueGrid" data-securitycaption="Inventory Attribute Value"></div>
                                </div>
                              </div>
                            </div>
                            <div class="flexcolumn" style="flex:0 1 800px;">
                              <div class="fwcontrol fwcontainer fwform-section foot-candles" data-control="FwContainer" data-type="section" data-caption="Foot Candles">
                                <div class="flexrow">
                                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield lamps" data-caption="Current" data-datafield="CurrentFootCandles" style="flex:1 1 110px;"></div>
                                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Required" data-datafield="RequiredFootCandles" style="flex:1 1 125px;margin-left:40px;"></div>
                                </div>
                              </div>
                              <div class="fwcontrol fwcontainer fwform-section software-version" data-control="FwContainer" data-type="section" data-caption="Software Version">
                                <div class="flexrow">
                                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Required" data-datafield="RequiredSoftwareVersion" data-enabled="false" style="flex:1 1 120px;"></div>
                                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Current" data-datafield="CurrentSoftwareVersion" data-displayfield="SoftwareVersion" data-validationname="SoftwareVersionValidation" data-enabled="true" style="flex:1 1 120px;"></div>
                                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Effective Date" data-datafield="SoftwareEffectiveDate" data-enabled="true" style="flex:1 1 120px;"></div>
                                </div>
                              </div>
                            </div>
                          </div>
                          <div class="flexrow" style="max-width:800px">
                            <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Note" data-datafield="Note" style="flex:1 1 450px;"></div>
                          </div>
                          <div class="flexrow">
                            <div class="fwformcontrol updateqc" data-type="button" style="flex:0 1 90px;margin:15px 15px 10px 10px;">Update QC</div>
                          </div>
                        </div>
                        <div class="msg success-msg update"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var CompleteQcController = new CompleteQc();