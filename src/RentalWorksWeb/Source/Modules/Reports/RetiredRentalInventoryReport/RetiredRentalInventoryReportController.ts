routes.push({
    pattern: /^reports\/retiredrentalinventoryreport/, action: function (match: RegExpExecArray) {
        return RetiredRentalInventoryReportController.getModuleScreen();
    }
});

const retiredRentalInventoryTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport retiredrentalinventoryreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Retired Rental Inventory" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RetiredRentalInventoryReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="flexpage">
          <div class="flexcolumn" style="max-width:450px; float:left;">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Retired Date Range">
              <div class="flexrow">
                <div data-datafield="FromDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-required="true" data-caption="From" style="max-width:150px;"></div>
                <div data-datafield="ToDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-required="true" data-caption="To" style="max-width:150px;"></div>
              </div>
            </div>
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
              <div data-datafield="IncludeUnretired" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Un-Retired Items" style="max-width:200px;"></div>
              <div data-datafield="ShowSellInformation" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Sell Information" style="max-width:200px;"></div>
            </div>
          </div>
          <div class="flexcolumn" style="max-width:600px; float:left;">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-formbeforevalidate="beforeValidate" data-validationname="CustomerValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-formbeforevalidate="beforeValidate" data-validationname="DealValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="RentalCategoryValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub Category" data-datafield="SubCategoryId" data-displayfield="SubCategory" data-formbeforevalidate="beforeValidate" data-validationname="SubCategoryValidation" data-validationpeek="false" data-showinactivemenu="true" style="min-width:400px;"></div>
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-formbeforevalidate="beforeValidate" data-validationname="RentalInventoryValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
              <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Retired Reason" data-datafield="RetiredReasonId" data-displayfield="RetiredReason" data-validationname="RetiredReasonValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
            </div>
          </div>
        </div>
      </div>
      <div id="exporttabpage" class="tabpage exporttabpage" data-tabid="exporttab">
      </div>
    </div>
  </div>
</div>
`;
//----------------------------------------------------------------------------------------------
class RetiredRentalInventoryReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RetiredRentalInventoryReport', 'api/v1/retiredrentalinventoryreport', retiredRentalInventoryTemplate);
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

        FwFormField.setValueByDataField($form, 'ShowSellInformation', true);
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
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
};

var RetiredRentalInventoryReportController: any = new RetiredRentalInventoryReport();