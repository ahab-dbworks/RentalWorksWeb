routes.push({
    pattern: /^reports\/retiredrentalinventoryreport/, action: function (match: RegExpExecArray) {
        return RwRetiredRentalInventoryReportController.getModuleScreen();
    }
});

const retiredRentalInventoryTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport retiredrentalinventoryreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Retired Rental Inventory" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwRetiredRentalInventoryReportController">
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
          <div class="flexcolumn" style="max-width:450px; float:left;">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="max-width:300px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-validationname="InventoryTypeValidation" style="max-width:300px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="max-width:300px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="max-width:300px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-validationname="RentalCategoryValidation" style="max-width:300px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sub Category" data-datafield="SubCategoryId" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" style="max-width:300px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-validationname="RentalInventoryValidation" style="max-width:300px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Retired Reason" data-datafield="RetiredReasonId" data-displayfield="RetiredReason" data-validationname="RetiredReasonValidation" style="max-width:300px;"></div>
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
class RwRetiredRentalInventoryReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RetiredRentalInventoryReport', 'api/v1/retiredrentalinventoryreport', retiredRentalInventoryTemplate);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`Rw${this.Module}Controller`);
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
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
};

var RwRetiredRentalInventoryReportController: any = new RwRetiredRentalInventoryReport();