routes.push({ pattern: /^module\/retired$/, action: function (match: RegExpExecArray) { return RetiredHistoryController.getModuleScreen(); } });

class RetiredHistory {
    Module: string = 'RetiredHistory';
    apiurl: string = 'api/v1/retired';
    caption: string = Constants.Modules.Inventory.children.RetiredHistory.caption;
    nav: string = Constants.Modules.Inventory.children.RetiredHistory.nav;
    id: string = Constants.Modules.Inventory.children.RetiredHistory.id;
    //nameItemAttributeValueGrid: string = 'ItemAttributeValueGrid';
    //nameItemQcGrid: string = 'ItemQcGrid';
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        options.hasDelete = false;
        options.hasInactive = false;
        FwMenu.addBrowseMenuButtons(options);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const $all: JQuery = FwMenu.generateDropDownViewBtn('ALL Warehouses', false, "ALL");
        const $userWarehouse: JQuery = FwMenu.generateDropDownViewBtn(warehouse.warehouse, true, warehouse.warehouseid);

        if (typeof this.ActiveViewFields["WarehouseId"] == 'undefined') {
            this.ActiveViewFields.WarehouseId = [warehouse.warehouseid];
        }

        const viewSubitems: Array<JQuery> = [];
        viewSubitems.push($userWarehouse, $all);
        FwMenu.addViewBtn(options.$menu, 'Warehouse', viewSubitems, true, "WarehouseId");
    };
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });

        //try {
        //    FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
        //        for (let key in response) {
        //            FwBrowse.addLegend($browse, key, response[key]);
        //        }
        //    }, function onError(response) {
        //        FwFunc.showError(response);
        //    }, $browse)
        //} catch (ex) {
        //    FwFunc.showError(ex);
        //}

        return $browse;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        this.events($form);
        return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'RetiredId', uniqueids.RetiredId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //---------------------------------------------------------------------------------------------
    //loadAudit($form: JQuery) {
    //    var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
    //    FwModule.loadAudit($form, uniqueid);
    //};
    //---------------------------------------------------------------------------------------------
    events($form) {
        $form.find('.retire-inv-btn').on('click', $tr => {
            const parentModuleInfo: any = {};
            parentModuleInfo.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
            parentModuleInfo.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
            parentModuleInfo.ICode = FwFormField.getTextByDataField($form, 'InventoryId');
            parentModuleInfo.Description = FwFormField.getValueByDataField($form, 'Description');
            parentModuleInfo.BarCode = FwFormField.getValueByDataField($form, 'BarCode');
            parentModuleInfo.SerialNumber = FwFormField.getValueByDataField($form, 'SerialNumber');
            parentModuleInfo.RetiredId = FwFormField.getValueByDataField($form, 'RetiredId');
            const $unretireForm = InventoryUnretireUtilityController.openForm('NEW', parentModuleInfo);
            FwModule.openSubModuleTab($form, $unretireForm);
            const $tab = FwTabs.getTabByElement($unretireForm);
            $tab.find('.caption').html('Unretire Utility');
        });

    }
    //---------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {

    };
    //---------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {

    };
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="RetiredHistory" data-control="FwBrowse" data-type="Browse" id="RetiredHistoryBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="RetiredHistoryController">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="RetiredId" data-browsedatatype="key"></div>
          </div>
          <div class="column flexcolumn" max-width="150px" data-visible="true">
            <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="450px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Bar Code" data-datafield="BarCode" data-browsedatatype="text" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="false">
            <div class="field" data-caption="RentalItemId" data-datafield="ItemId" data-browsedatatype="text" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Serial No." data-datafield="SerialNumber" data-browsedatatype="text" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Retired Date" data-datafield="RetiredDate" data-browsedatatype="date" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Retired Reason" data-datafield="RetiredReason" data-browsedatatype="text" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Retired by" data-datafield="Retiredby" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Retired Qty" data-datafield="RetiredQuantity" data-browsedatatype="number" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Unretired Qty" data-datafield="UnretiredQuantity" data-browsedatatype="number" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
    };
    //---------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="retiredhistoryform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Retired History" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RetiredHistoryController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="RetiredId"></div>
          <div id="retiredhistoryform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="retiredtab" class="tab" data-tabpageid="retiredtabpage" data-caption="General"></div>
            </div>
            <div class="tabpages">
              <!-- General tab -->
              <div data-type="tabpage" id="retiredtabpage" class="tabpage" data-tabid="retiredtab">
                <div class="formpage">
                  <div class="flexrow" style="max-width:650px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Retire History">
                      <div class="flexcolumn" style="max-width:650px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-validationname="RentalInventoryValidation" data-displayfield="ICode" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:1 1 400px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code" data-datafield="BarCode" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ItemId" data-datafield="ItemId" data-enabled="false" style="flex:1 1 150px;display:none;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Serial No." data-datafield="SerialNumber" data-enabled="false" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Retired Date" data-datafield="RetiredDate" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Retired By" data-datafield="RetiredByUserId" data-validationname="UserValidation" data-displayfield="RetiredBy" data-enabled="false" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Retired Qty" data-datafield="RetiredQuantity" data-enabled="false" style="flex:0 1 75px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Unretired Qty" data-datafield="UnretiredQuantity" data-enabled="false" style="flex:0 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="RetiredNotes"></div>
                        </div>
                        <div class="flexrow">
                          <div class="fwformcontrol retire-inv-btn" data-type="button" style="flex:0 1 160px;margin:15px 0 0 10px;text-align:center;">UNRETIRE INVENTORY</div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    };
}

var RetiredHistoryController = new RetiredHistory();