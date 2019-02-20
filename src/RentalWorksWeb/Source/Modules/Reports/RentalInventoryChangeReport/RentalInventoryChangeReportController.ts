﻿routes.push({
    pattern: /^reports\/RentalInventoryChangeReport/, action: function (match: RegExpExecArray) {
        return RwRentalInventoryChangeReportController.getModuleScreen();
    }
});

var rentalInventoryChangeTemplateFrontEnd = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Rental Inventory Change Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwRentalInventoryChangeReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:250px;">
                     <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From:" data-datafield="FromDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To:" data-datafield="ToDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracked By">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="TrackedBys" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:75px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rank">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="Ranks" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:75px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:300px;">
               <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="RentalCategoryValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sub-Category" data-datafield="SubCategoryId" data-formbeforevalidate="beforeValidate" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-formbeforevalidate="beforeValidate" data-displayfield="ICode" data-validationname="RentalInventoryValidation" style="float:left;max-width:300px;"></div>
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
class RwRentalInventoryChangeReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RentalInventoryChangeReport', 'api/v1/RentalInventoryChangeReport', rentalInventoryChangeTemplateFrontEnd);
        //this.reportOptions.HasDownloadExcel = true;
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        let $form = this.openForm();

        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () {
        };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        let $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {

        this.load($form, this.reportOptions);
        var appOptions: any = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };

        this.loadLists($form);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        const convertedParams: any = {};

        convertedParams.DateType = parameters.DateType;
        convertedParams.ToDate = parameters.ToDate;
        convertedParams.FromDate = parameters.FromDate;
        convertedParams.IncludeNoCharge = parameters.IncludeNoCharge;
        convertedParams.OfficeLocationId = parameters.OfficeLocationId;
        convertedParams.DepartmentId = parameters.DepartmentId;
        convertedParams.DealId = parameters.DealId;
        convertedParams.AgentId = parameters.UserId;
        convertedParams.CustomerId = 'Testing';
        return convertedParams;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $form, request) {
        var validationName = request.module;
        if (validationName != null) {
            const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
            request.uniqueids = {};

            switch (validationName) {
                case 'InventoryTypeValidation':
                    request.uniqueids.Rental = true;
                    break;
                case 'RentalCategoryValidation':
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    }
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids.Rental = true;
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    }
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
                    }
                    break;
                case 'RentalInventoryValidation':
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    };
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
                    };
                    if (subCategoryId !== "") {
                        request.uniqueids.SubCategoryId = subCategoryId;
                    };
                    break;
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="TrackedBys"]'), [{ value: "BARCODE", text: "Barcode", selected: "T" }, { value: "QUANTITY", text: "Quantity", selected: "T" }, { value: "SERIALNO", text: "Serial Number", selected: "T" }, { value: "RFID", text: "RFID", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="Ranks"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }, { value: "E", text: "E", selected: "T" }, { value: "F", text: "F", selected: "T" }, { value: "G", text: "G", selected: "T" }]);
    }
    //----------------------------------------------------------------------------------------------
};

var RwRentalInventoryChangeReportController: any = new RwRentalInventoryChangeReportClass();
//----------------------------------------------------------------------------------------------