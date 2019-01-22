//routes.push({ pattern: /^module\/completeqc$/, action: function (match: RegExpExecArray) { return CompleteQcController.getModuleScreen(); } });

class CompleteQc {
    Module: string = 'CompleteQc';
    caption: string = 'Complete QC';
    nav: string = 'module/completeqc';
    id: string = '3F20813A-CC21-49D8-A5F8-9930B7F05404';
    itemId: string = '';
    itemQcId: string = '';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        let self = this;

        var $form = this.openForm('EDIT');

        screen.load = function () {
            FwModule.openModuleTab($form, 'Complete QC', false, 'FORM', true);
            this.itemId = '';
            this.itemQcId = '';
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        //var $form;
        let self = this;

        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        $form.off('change keyup', '.fwformfield[data-isuniqueid!="true"][data-enabled="true"][data-datafield!=""]');
        $form.find('div[data-datafield="Code"] input').select().focus();

        $form.find('.completeqc').on('click', function () {
            let request: any = {
                QcAnyway: false,
                Code: FwFormField.getValue2($form.find('div[data-datafield="Code"]'))
            };

            self.completeQc($form, request);
        })

        $form.find('.code').on('keypress', function (e) {
            if (e.which === 13) {
                let request: any = {
                    QcAnyway: false,
                    Code: FwFormField.getValue2($form.find('div[data-datafield="Code"]'))
                };

                self.completeQc($form, request);
            }
        });

        $form.find('.updateqc').on('click', function () {
            let request: any = {
                ItemId: self.itemId,
                ItemQcId: self.itemQcId,
                ConditionId: FwFormField.getValue2($form.find('div[data-datafield="Condition"]')),
                Note: FwFormField.getValue2($form.find('div[data-datafield="Note"]'))
            }
            self.updateQc($form, request);
        })

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    completeQc($form, request) {
        let self = this;
        let $completed = $form.find('.qcsuccess');
        try {
            FwAppData.apiMethod(true, 'POST', 'api/v1/completeqc/completeqcitem', request, FwServices.defaultTimeout, function onSuccess(response) {
                FwFormField.setValueByDataField($form, 'ICode', '');
                FwFormField.setValueByDataField($form, 'Description', '');
                FwFormField.setValueByDataField($form, 'Condition', '');
                FwFormField.setValueByDataField($form, 'Note', '');

                if (response.success) {
                    $completed.show();
                    $form.find('.code').removeClass('error');
                    $form.find('div.msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:green;color:white;">QC Completed Successfully</span></div>`);
                    FwFormField.setValueByDataField($form, 'ICode', response.ICode);
                    FwFormField.setValueByDataField($form, 'Description', response.Description);
                    FwFormField.setValueByDataField($form, 'Condition', response.ConditionId, response.Condition);
                    self.itemId = response.ItemId;
                    self.itemQcId = response.ItemQcId;
                    $form.find('div[data-datafield="Code"] input').select().focus();
                    $form.find('div.updatemsg').html('');
                } else {
                    $completed.hide();
                    $form.find('.code').addClass('error');
                    $form.find('div.msg').html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                    $form.find('div[data-datafield="Code"] input').select().focus();
                    $form.find('div.updatemsg').html('');
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
                $form.find('div.updatemsg').html(`<div style="margin:18px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:green;color:white;">QC Updated Successfully</span></div>`);
            }, null, $form);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: JQuery = this.openForm('EDIT');

        $form = this.openForm('EDIT');
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
    }
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
                    <div class="flexrow"><div><span>Scan or type the Bar Code or Serial Number of an item below and hit Enter to complete the QC of the item.</span></div></div>
                    <div class="flexrow" style="margin-bottom:20px;"><div><span>Optionally update the Condition, Notes, etc. for this item.</span></div></div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="code fwcontrol fwformfield" data-caption="Bar Code/Serial No" data-datafield="Code" style="flex:0 1 300px;"></div>
                      <div class="fwformcontrol completeqc" data-type="button" style="flex:0 1 105px;margin:15px 15px 10px 10px;">Complete QC</div>
                    </div>
                    <div class="msg"></div>
                    <div class="flexrow" style="max-width:800px">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="flex:1 1 125px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="Description" style="flex:1 1 350px;" data-enabled="false"></div>
                    </div>
                    <div class="qcsuccess" style="display:none;">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Condition" data-datafield="Condition" style="flex:0 1 300px;" data-validationname="InventoryConditionValidation"></div>
                      </div>
                      <div class="flexrow" style="max-width:800px">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Note" data-datafield="Note" style="flex:1 1 450px;"></div>
                      </div>
                      <div class="flexrow">
                          <div class="fwformcontrol updateqc" data-type="button" style="flex:0 1 90px;margin:15px 15px 10px 10px;">Update QC</div>
                      </div>
                    </div>
                    <div class="updatemsg"></div>   
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