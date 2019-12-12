routes.push({
    pattern: /^reports\/rentalinventoryusagereport/, action: function (match: RegExpExecArray) {
        return RentalInventoryUsageReportController.getModuleScreen();
    }
});

const rentalInventoryUsageTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Rental Inventory Usage Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RentalInventoryUsageReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:205px;">
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
            <div class="flexcolumn" style="max-width:380px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="ExcludeZeroOwned" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Items with Zero Quantity Owned" style="float:left;max-width:420px;"></div>
                  <div data-datafield="OnlyIncludeItemsThatAreTheMainItemOfAComplete" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Include Items That Are The Main Item Of A Complete" style="float:left;max-width:420px;"></div>
                </div>
                <div class="fwcontrol fwcontainer flexrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="FilterDatesByUtilizationPercent" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter By Utilization Percent" style="flex:0 1 180px;"></div>
                  <div data-datafield="UtilizationFilterMode" data-control="FwFormField" data-type="select" class="fwcontrol fwformfield " data-caption="" data-enabled="false" style="flex:0 1 65px;"></div>
                  <div data-datafield="UtilizationFilterAmount" data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption=""  data-enabled="false" style="flex:0 1 53px;"></div>
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
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="RentalCategoryValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-formbeforevalidate="beforeValidate" data-displayfield="ICode" data-validationname="RentalInventoryValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
class RentalInventoryUsageReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RentalInventoryUsageReport', 'api/v1/rentalinventoryusagereport', rentalInventoryUsageTemplate);
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

        // Default settings for first time running
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        const aMonthAgo = FwFunc.getDate(today, -30);
        FwFormField.setValueByDataField($form, 'FromDate', aMonthAgo);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const $filterOperators = $form.find('div[data-datafield="UtilizationFilterMode"]');
        FwFormField.loadItems($filterOperators, [
            { value: 'ALL', text: 'ALL' },
            { value: 'GT', text: '>' },
            { value: 'LT', text: '<' },
            { value: 'EQ', text: '=' },
            { value: 'GTE', text: '>=' },
            { value: 'LTE', text: '<=' }
        ], true);

        const filterDates = $form.find('div[data-datafield="FilterDatesByUtilizationPercent"] input');
        filterDates.on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                FwFormField.enable($form.find('div[data-datafield="UtilizationFilterMode"]'));
                FwFormField.enable($form.find('div[data-datafield="UtilizationFilterAmount"]'));
            } else {
                FwFormField.disable($form.find('div[data-datafield="UtilizationFilterMode"]'));
                FwFormField.disable($form.find('div[data-datafield="UtilizationFilterAmount"]'));
            }
        });
        filterDates.change();
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const validationName = request.module;
        if (validationName != null) {
            const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
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
                case 'RentalInventoryValidation':
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    };
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
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

var RentalInventoryUsageReportController: any = new RentalInventoryUsageReport();
//----------------------------------------------------------------------------------------------