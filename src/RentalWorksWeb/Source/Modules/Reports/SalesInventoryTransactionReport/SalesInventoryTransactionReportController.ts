﻿routes.push({
    pattern: /^reports\/salesinventorytransactionreport/, action: function (match: RegExpExecArray) {
        return SalesInventoryTransactionReportController.getModuleScreen();
    }
});

const salesInventoryTransactionTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport salesinventorytransaction" data-control="FwContainer" data-type="form" data-version="1" data-caption="Sales Inventory Transactions" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="SalesInventoryTransactionReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From:" data-required="true" data-datafield="FromDate" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To:" data-required="true" data-datafield="ToDate" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Include">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="TransactionTypes" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:200px;margin-left:10px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="SalesCategoryValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub Category" data-datafield="SubCategoryId" data-displayfield="SubCategory" data-formbeforevalidate="beforeValidate" data-validationname="SubCategoryValidation" data-validationpeek="false" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-formbeforevalidate="beforeValidate" data-validationname="SalesInventoryValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;

//----------------------------------------------------------------------------------------------
class SalesInventoryTransactionReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('SalesInventoryTransactionReport', 'api/v1/salesinventorytransactionreport', salesInventoryTransactionTemplate);
        this.reportOptions.HasDownloadExcel = true;
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm();

        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () { };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        this.loadLists($form);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = ($browse, $form, request) => {
        const validationName = request.module;
        const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        request.uniqueids = {};

        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids.Sales = true;
                break;
            case 'SalesCategoryValidation':
                if (inventoryTypeId !== "") {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                    break;
                }
            case 'SubCategoryValidation':
                request.uniqueids.Sales = true;

                if (inventoryTypeId !== "") {
                    request.uniqueids.TypeId = inventoryTypeId;
                }
                if (categoryId !== "") {
                    request.uniqueids.CategoryId = categoryId;
                }
                break;
            case 'SalesInventoryValidation':
                if (inventoryTypeId !== "") {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                }
                if (categoryId !== "") {
                    request.uniqueids.CategoryId = categoryId;
                }
                if (subCategoryId !== "") {
                    request.uniqueids.SubCategoryId = subCategoryId;
                }
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="TransactionTypes"]'),
            [
                { value: "PURCHASE", text: "Purchase", selected: "T" },
                { value: "VENDOR RETURN", text: "Vendor Return", selected: "T" },
                { value: "SALES", text: "Sales", selected: "T" },
                { value: "CUSTOMER RETURN", text: "Customer Return", selected: "T" },
                { value: "ADJUSTMENT", text: "Adjustment", selected: "T" },
                { value: "TRANSFER", text: "Transfer", selected: "T" }
            ]);
    }
    //----------------------------------------------------------------------------------------------
};

var SalesInventoryTransactionReportController: any = new SalesInventoryTransactionReport();
//----------------------------------------------------------------------------------------------