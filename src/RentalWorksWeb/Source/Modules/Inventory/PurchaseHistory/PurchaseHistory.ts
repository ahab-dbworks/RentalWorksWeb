class PurchaseHistory {
    Module: string = 'PurchaseHistory';
    apiurl: string = 'api/v1/purchase';
    caption: string = Constants.Modules.Inventory.children.PurchaseHistory.caption;
    nav: string = Constants.Modules.Inventory.children.PurchaseHistory.nav;
    id: string = Constants.Modules.Inventory.children.PurchaseHistory.id;
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
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: JQuery = this.openBrowse();

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

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        return $browse;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'PurchaseId', uniqueids.PurchaseId);
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
    renderGrids($form) {
        //Depreciation Grid
        FwBrowse.renderGrid({
            nameGrid: 'DepreciationGrid',
            gridSecurityId: 'Wi9NxgGglKjTN',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = true;
                options.hasEdit = true;
                options.hasDelete = true;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PurchaseId: FwFormField.getValueByDataField($form, 'PurchaseId'),
                };
            }, 
            beforeSave: (request: any) => {
                request.PurchaseId = FwFormField.getValueByDataField($form, 'PurchaseId');
            }
        });
    };
    //---------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        const $depreciationGrid = $form.find('[data-name="DepreciationGrid"]');
        FwBrowse.search($depreciationGrid);
        this.showHideWarhouseRow($form);
    };
    //---------------------------------------------------------------------------------------------
     showHideWarhouseRow($form) {
        const currencyId = FwFormField.getValueByDataField($form, 'CurrencyId');
        const warehouseCurrencyId = FwFormField.getValueByDataField($form, 'WarehouseDefaultCurrencyId');
        if (currencyId !== '' && currencyId !== warehouseCurrencyId) {
            $form.find('.warehouse-currency').show();
        } else {
            $form.find('.warehouse-currency').hide();
        }
    }
    //---------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="PurchaseHistory" data-control="FwBrowse" data-type="Browse" id="PurchaseHistoryBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="PurchaseHistoryController">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="PurchaseId" data-browsedatatype="key"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Purchased" data-datafield="PurchaseDate" data-browsedatatype="date" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Received" data-datafield="ReceiveDate" data-browsedatatype="date" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Vendor" data-datafield="Vendor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="PO Number" data-datafield="PurchaseOrderNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Quantity" data-datafield="Quantity" data-browsedatatype="number" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Unit Cost" data-datafield="UnitCost" data-browsedatatype="money" data-cellcolor="CurrencyColor" data-currencysymbol="CurrencySymbol" data-digits="2" data-formatnumeric="true" data-sort="off" style="justify-content:flex-end;"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Extended" data-datafield="CostExtended" data-browsedatatype="money" data-cellcolor="CurrencyColor" data-currencysymbol="CurrencySymbol" data-digits="2" data-formatnumeric="true" data-sort="off" style="justify-content:flex-end;"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
          </div>
        </div>`;
    };
    //---------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="purchasehistoryform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Purchase History" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PurchaseHistoryController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="PurchaseId"></div>
          <div id="purchasehistoryform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="purchasetab" class="tab" data-tabpageid="purchasetabpage" data-caption="General"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <!-- General tab -->
                <div data-type="tabpage" id="purchasetabpage" class="tabpage" data-tabid="purchasetab">
                  <div class="formpage">
                    <div class="formrow">
                      <div class="formcolumn" style="width:1100px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-validationname="WarehouseValidation" data-displayfield="Warehouse" data-enabled="false" style="float:left;width:250px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ownership" data-datafield="Ownership" data-enabled="false" style="float:left;width:250px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Purchase Date" data-datafield="PurchaseDateString" data-enabled="false" style="float:left;width:175px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Receive Date" data-datafield="ReceiveDateString" data-enabled="false" style="float:left;width:175px;"></div>
                          </div>
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-validationname="RentalInventoryValidation" data-displayfield="ICode" data-enabled="false" style="float:left;width:150px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-enabled="false" style="float:left;width:500px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" data-enabled="false" style="float:left;width:100px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-enabled="false" data-displayfield="Vendor" data-validationname="VendorValidation" style="flex:1 1 300px;"></div>
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Purchase PO Number" data-datafield="PurchasePoId" data-enabled="false" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" style="flex:1 1 150px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Outside PO Number" data-datafield="OutsidePurchaseOrderNumber" data-enabled="false" style="flex:1 1 150px;"></div>
                          </div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Cost">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" data-enabled="false" style="flex:1 1 75px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Unit Cost" data-datafield="UnitCost" data-currencysymbol="CurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Unit Cost with Tax" data-datafield="UnitCostWithTax" data-currencysymbol="CurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                          </div>
                          <div class="flexrow warehouse-currency" style="display:none;">
                            <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Warehouse Currency Code" data-datafield="WarehouseDefaultCurrencyId" data-displayfield="WarehouseDefaultCurrencyCode" data-enabled="false" style="flex:1 1 75px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="(converted) Unit Cost" data-datafield="UnitCostCurrencyConverted" data-currencysymbol="WarehouseDefaultCurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="(converted) Unit Cost with Tax" data-datafield="UnitCostWithTaxCurrencyConverted" data-currencysymbol="WarehouseDefaultCurrencySymbol" data-enabled="false" style="flex:1 1 75px;"></div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depreciation">
                            <div data-control="FwGrid" data-grid="DepreciationGrid"></div>
                          </div>
                       </div>
                      </div>
                    </div>
                  </div>
                </div>

              <!-- Notes tab -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="PurchaseNotes"></div>
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

var PurchaseHistoryController = new PurchaseHistory();