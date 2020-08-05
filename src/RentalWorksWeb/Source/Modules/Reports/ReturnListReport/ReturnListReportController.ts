routes.push({
    pattern: /^reports\/returnlistreport/, action: function (match: RegExpExecArray) {
        return ReturnListReportController.getModuleScreen();
    }
});

const returnListTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Return List Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ReturnListReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
        <div data-control="FwFormField" data-datafield="DealId" data-type="text" class="fwcontrol fwformfield" data-caption="" style="flex:0 1 100px;display:none;" data-enabled="false" data-savesetting="false"></div>
        <div data-control="FwFormField" data-datafield="DepartmentId" data-type="text" class="fwcontrol fwformfield" data-caption="" style="flex:0 1 100px;display:none;" data-enabled="false" data-savesetting="false"></div>
        <div data-control="FwFormField" data-datafield="ContractId" data-type="text" class="fwcontrol fwformfield" data-caption="" style="flex:0 1 100px;display:none;" data-enabled="false" data-savesetting="false"></div>
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:380px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="IncludeTrackedByBarcode" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include I-Codes tracked by Bar Code/Serial No." style="float:left;max-width:420px;"></div>
                  <div data-datafield="PrintBarcodes" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Print Bar Codes" style="float:left;max-width:420px;"></div>
                  !--<div data-datafield="PrintContainersToRefillReport" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Print Containers to Refill Report" style="float:left;max-width:420px;"></div>--
                  <div data-datafield="PaginateByInventoryType" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Paginate By Inventory Type" style="float:left;max-width:420px;"></div>
                  <div data-datafield="AddBoxforMeterReading" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Add Box for Meter Reading" style="float:left;max-width:420px;"></div>
                  <div data-datafield="PrintICodeColumn" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Print I-Code Column" style="float:left;max-width:420px;"></div>
                  <div data-datafield="PrintAisleShelf" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Print Aisle/ Shelf" style="float:left;max-width:420px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
               <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <!--may readd -->
                <!--<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">-->
                <!--  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Orders" data-datafield="OrderId" data-displayfield="Orders" data-validationname="OrderValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>-->
                <!--</div>-->
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType"  data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category"  data-validationname="RentalCategoryValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId"  data-displayfield="ICode" data-validationname="RentalInventoryValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
class ReturnListReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ReturnListReport', 'api/v1/returnlistreport', returnListTemplate);
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
        //this.loadLists($form);

        // Default settings for first time running
        //const orders = JSON.parse(sessionStorage.getItem('order'));
        //FwFormField.setValue($form, 'div[data-datafield="OrderId"]', orders.orderid, orders.descrition, orders.estrentto);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        parameters.department = JSON.parse(sessionStorage.getItem('department')).department;
        parameters.warehouse = JSON.parse(sessionStorage.getItem('warehouse')).warehouse;
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
            const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');

            switch (datafield) {
                case 'InventoryTypeId':
                    request.uniqueids.Rental = true;
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                    break;
                case 'CategoryId':
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    }
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                    break;
                case 'InventoryId':
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    };
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                    break;
            }
        
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
    }
    //----------------------------------------------------------------------------------------------
};

var ReturnListReportController: any = new ReturnListReport();
//----------------------------------------------------------------------------------------------