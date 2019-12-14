routes.push({
    pattern: /^reports\/rentallostanddamagedbillinghistoryreport/, action: function (match: RegExpExecArray) {
        return RentalLostAndDamagedBillingHistoryReportController.getModuleScreen();
    }
});

const rentalLnDBillingHistoryTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Rental Lost and Damaged Billing History Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RentalLostAndDamagedBillingHistoryReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:450px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Retired Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From" data-datafield="FromDate" data-required="true" style="float:left;max-width:200px;"></div>
                 </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To" data-datafield="ToDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:250px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude Retired Items" data-datafield="ExcludeRetiredItems"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude Unretired Items" data-datafield="ExcludeUnretiredItems"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidate" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-validationname="RentalCategoryValidation" data-formbeforevalidate="beforeValidate" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-validationname="RentalInventoryValidation" data-formbeforevalidate="beforeValidate" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Retired Reason" data-datafield="RetiredReasonId" data-displayfield="RetiredReason" data-validationname="RetiredReasonValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Unretired Reason" data-datafield="UnretiredReasonId" data-displayfield="UnretiredReason" data-validationname="UnretiredReasonValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
              </div>
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
class RentalLostAndDamagedBillingHistoryReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RentalLostAndDamagedBillingHistoryReport', 'api/v1/rentallostanddamagedbillinghistoryreport', rentalLnDBillingHistoryTemplate);
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
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
    };
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {

 
            switch (datafield) {
                case 'InventoryTypeId':
                    request.uniqueids.Rental = true;
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                    break;
                case 'DealId':
                    request.uniqueids.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                    break;
                case 'CategoryId':
                    request.uniqueids.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                    break;
                case 'InventoryId':
                    request.uniqueids.InventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
                    request.uniqueids.CategoryId = FwFormField.getValueByDataField($form, 'CategoryId');
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                    break;
                case 'RetiredReasonId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateretiredreason`);
                    break;
                case 'CustomerId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                    break;
                case 'WarehouseId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                    break;
                case 'UnretiredReasonId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateunretiredreason`);
                    break;
            }
    };
    //----------------------------------------------------------------------------------------------
};

var RentalLostAndDamagedBillingHistoryReportController: any = new RentalLostAndDamagedBillingHistoryReport();