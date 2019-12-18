class PickList {
    Module: string = 'PickList';
    apiurl: string = 'api/v1/picklist';
    caption: string = Constants.Modules.Warehouse.children.PickList.caption;
    nav: string = Constants.Modules.Warehouse.children.PickList.nav;
    id: string = Constants.Modules.Warehouse.children.PickList.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();
        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        let $emailHistorySubModuleBrowse = this.openEmailHistoryBrowse($form);
        $form.find('.emailhistory-page').append($emailHistorySubModuleBrowse);

        this.events($form);
        this.renderPrintButton($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'PickListId', uniqueids.PickListId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Pick List', '', (e: JQuery.ClickEvent) => {
            try {
                this.printPickListFromBrowse(options.$browse);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $allWarehouses = FwMenu.generateDropDownViewBtn('ALL', false, "ALL");
        const $userWarehouse = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        let viewWarehouse = [];
        viewWarehouse.push($allWarehouses, $userWarehouse);
        FwMenu.addViewBtn(options.$menu, 'Warehouse', viewWarehouse, true, "WarehouseId");
    }
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Pick List', '', (e: JQuery.ClickEvent) => {
            try {
                this.printPickListFromForm(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Pick List', '2zX9FJ9f8TX5', (e: JQuery.ClickEvent) => {
            try {
                const pickListNumber = FwFormField.getValueByDataField(options.$form, 'PickListNumber');
                const pickListId = FwFormField.getValueByDataField(options.$form, 'PickListId');
                this.cancelPickList(pickListId, pickListNumber);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderPrintButton($form: any) {
        var $print = FwMenu.addStandardBtn($form.find('.fwmenu:first'), 'Print');
        $print.prepend('<i class="material-icons">print</i>');
        $print.on('click', () => {
            this.printPickListFromForm($form);
        });
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    events($form) {}
    //----------------------------------------------------------------------------------------------
    printPickListFromBrowse($browse: JQuery): void {
        try {
            let pickListNumber = $browse.find('.selected [data-browsedatafield="PickListNumber"]').attr('data-originalvalue');
            let pickListId = $browse.find('.selected [data-browsedatafield="PickListId"]').attr('data-originalvalue');
            let orderType = $browse.find('.selected [data-browsedatafield="OrderType"]').attr('data-originalvalue');
            if (pickListId != null) {
                $browse = PickListReportController.openForm();
                FwModule.openModuleTab($browse, 'Pick List Report for ' + pickListNumber, true, 'REPORT', true);
                FwFormField.setValueByDataField($browse, 'PickListId', pickListId, pickListNumber);
                FwFormField.setValueByDataField($browse, 'OrderType', orderType);
            } else {
                FwNotification.renderNotification('WARNING', 'Select a Picklist to print.');
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    printPickListFromForm($form: JQuery): void {
        try {
            const pickListNumber = FwFormField.getValueByDataField($form, 'PickListNumber');
            const pickListId = FwFormField.getValueByDataField($form, 'PickListId');
            const orderType = FwFormField.getValueByDataField($form, 'OrderType');
            const $report = PickListReportController.openForm();

            FwModule.openSubModuleTab($form, $report);
            FwFormField.setValueByDataField($report, 'PickListId', pickListId, pickListNumber);
            FwFormField.setValueByDataField($report, 'OrderType', orderType);
            const $tabPage = FwTabs.getTabPageByElement($report);
            const $tab = FwTabs.getTabByElement(jQuery($tabPage));
            $tab.find('.caption').html('Print Pick List');
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    openEmailHistoryBrowse($form) {
        var $browse = EmailHistoryController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                RelatedToId: $form.find('[data-datafield="PicklistId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form) {
        FwBrowse.renderGrid({
            nameGrid: 'PickListItemGrid',
            gridSecurityId: 'fa0NLEHNkNU0',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasEdit = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PickListId: FwFormField.getValueByDataField($form, 'PickListId')
                };
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    cancelPickList(pickListId, pickListNumber) {
        var self = this;
        var $confirmation = FwConfirmation.renderConfirmation('Cancel Pick List', `Cancel Pick List ${pickListNumber}?`);
        var $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        var $no = FwConfirmation.addButton($confirmation, 'No');

        $yes.on('click', function () {
            FwAppData.apiMethod(true, 'DELETE', `${self.apiurl}/${pickListId}`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', 'Pick List Cancelled');
                    FwConfirmation.destroyConfirmation($confirmation);
                    //Close tab
                    var $tab = jQuery('div.tab.active');
                    var $control = jQuery(this).closest('.fwcontrol');
                    var $form = jQuery('#' + $tab.attr('data-tabpageid')).find('.fwform');
                    if (typeof $form !== 'undefined') {
                        FwModule.closeForm($form, $tab);
                    } else {
                        var isactivetab = $tab.hasClass('active');
                        var $newactivetab = (($tab.next().length > 0) ? $tab.next() : $tab.prev());
                        FwTabs.removeTab($tab);
                        if (isactivetab) {
                            FwTabs.setActiveTab($control, $newactivetab);
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, $confirmation);
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        var $pickListItemGrid = $form.find('[data-name="PickListItemGrid"]');
        FwBrowse.search($pickListItemGrid);
    }
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="PickList" data-control="FwBrowse" data-type="Browse" id="PickListBrowse" class="fwcontrol fwbrowse" data-orderby="PickListId" data-controller="PickListController">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="PickListId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-datafield="OrderId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="125px" data-visible="false">
            <div class="field" data-caption="Order Type" data-datafield="OrderType" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Pick No." data-datafield="PickListNumber" data-browsedatatype="text" data-sort="desc" data-sortsequence="3" ></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Pick Date" data-datafield="PickDate" data-browsedatatype="text" data-sortsequence="1" data-sort="desc"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Pick Time" data-datafield="PickTime" data-browsedatatype="text" data-sortsequence="2" data-sort="desc"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Order No." data-datafield="OrderNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Order Description" data-datafield="OrderDescription" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="175px" data-visible="true">
            <div class="field" data-caption="Department" data-datafield="Department" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="175px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="125px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-browsedatatype="text" data-sort="off"></div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="picklistform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Pick List" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PickListController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="PickListId"></div>
          <div id="picklistform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="Pick List"></div>
              <div data-type="tab" id="emailhistorytab" class="tab" data-tabpageid="emailhistorytabpage" data-caption="Email History"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 900px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Pick List">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Pick List No." data-datafield="PickListNumber" data-enabled="false" style="flex:0 1 125px;"></div>
                          <!--<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderNumber" data-enabled="false" style="flex:1 1 150px;"></div>-->
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="OrderDescription" data-enabled="false" style="flex:1 1 325px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="Deal" data-enabled="false" style="flex:1 1 325px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Type" data-datafield="OrderType" data-enabled="false" style="display:none;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As Of" data-datafield="InputDate" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="InputTime" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Department" data-datafield="Department" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Due Date/Time">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="DueDate" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Time" data-datafield="DueTime" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="wideflexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items">
                      <div data-control="FwGrid" data-grid="PickListItemGrid" data-securitycaption="Pick List Item"></div>
                    </div>
                  </div>
                </div>
              </div>
             <div data-type="tabpage" id="emailhistorytabpage" class="tabpage submodule emailhistory-page" data-tabid="emailhistorytab"></div>
            </div>
          </div>
        </div>`;
    }
}
//---------------------------------------------------------------------------------
var PickListController = new PickList();