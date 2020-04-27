routes.push({
    pattern: /^reports\/rentalinventorystatusandrevenuereport/, action: function (match: RegExpExecArray) {
        return RentalInventoryStatusAndRevenueReportController.getModuleScreen();
    }
});

const rentalInvStatusAndRevReportTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Rental Inventory Status and Revenue Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RentalInventoryStatusAndRevenueReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" style="display:flex;flex-wrap:wrap;" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:125px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rank">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="Ranks" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracked By">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="TrackedBys" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:350px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Revenue">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Period Revenue" data-datafield="IncludePeriodRevenue"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield toggle-enable" data-caption="From:" data-datafield="RevenueFromDate" data-required="true" style="width:125px; float:left;"></div>
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield toggle-enable" data-caption="To:" data-datafield="RevenueToDate" data-required="true" style="width:125px; float:left;"></div>                
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="RevenueFilterMode" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield toggle-enable" data-caption="" style="float:left;">
                    <div data-value="ALL" data-caption="All"></div>
                    <div data-value="GT" data-caption="Greater Than"></div>
                    <div data-value="LT" data-caption="Less Than"></div>
                  </div>
                  <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield toggle-enable filter-amount" data-caption="" data-datafield="RevenueFilterAmount" data-required="true" style="float:left; margin-left:20px; width:100px"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType"  data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category"  data-validationname="RentalCategoryValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode"  data-validationname="RentalInventoryValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
              </div>
            </div>
          </div>
          <div class="flexcolumn" style="max-width:375px;">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
              <div class="flexrow">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Include I-Codes not rented since" data-datafield="IncludeNotRentedSince" style="flex: 0 1 240px;"></div>
                <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="" data-datafield="NotRentedSinceDate" data-required="false" style="flex: 0 1 110px;margin: -9px 0 0 0;"></div>
              </div>
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include I-Codes with zero owned" data-datafield="IncludeZeroOwned"></div>
              </div>
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Order details for items Staged and Out" data-datafield="ShowStagedAndOut"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
//----------------------------------------------------------------------------------------------
class RentalInventoryStatusAndRevenueReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RentalInventoryStatusAndRevenueReport', 'api/v1/rentalinventorystatusandrevenuereport', rentalInvStatusAndRevReportTemplate);
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
        // Default settings for first time running
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'RevenueToDate', today);
        const aMonthAgo = FwFunc.getDate(today, -30);
        FwFormField.setValueByDataField($form, 'RevenueFromDate', aMonthAgo);

        this.loadLists($form);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const includePeriodRevenue = FwFormField.getValueByDataField($form, 'IncludePeriodRevenue');
        const $dateRangeFields = $form.find('.toggle-enable[data-type="date"]');
        if (includePeriodRevenue) {
            FwFormField.enable($form.find('.toggle-enable:not(.filter-amount)'));
            $dateRangeFields.attr('data-required', "true");
            const filterMode = FwFormField.getValueByDataField($form, 'RevenueFilterMode');
            const $filterAmountField = $form.find('[data-datafield="RevenueFilterAmount"]');

            if (filterMode === "ALL") {
                FwFormField.disable($filterAmountField);
                $filterAmountField.attr('data-required', "false");
            } else {
                FwFormField.enable($filterAmountField);
                $filterAmountField.attr('data-required', "true");
            }
        } else {
            FwFormField.disable($form.find('.toggle-enable'));
            $dateRangeFields.attr('data-required', "false");
        }
        $form.find('[data-datafield="IncludePeriodRevenue"]').on('change', e => {
            const isChecked = FwFormField.getValueByDataField($form, 'IncludePeriodRevenue');
            const $dateRangeFields = $form.find('.toggle-enable[data-type="date"]');
            const filterMode = FwFormField.getValueByDataField($form, 'RevenueFilterMode');

            if (isChecked) {
                const $filterAmountField = $form.find('[data-datafield="RevenueFilterAmount"]');
                if (filterMode === "ALL") {
                    FwFormField.disable($filterAmountField);
                    $filterAmountField.attr('data-required', "false");
                } else {
                    FwFormField.enable($filterAmountField);
                    $filterAmountField.attr('data-required', "true");
                }
                FwFormField.enable($form.find('.toggle-enable:not(.filter-amount)'));
                $dateRangeFields.attr('data-required', "true");
            } else {
                FwFormField.disable($form.find('.toggle-enable'));
                $dateRangeFields.attr('data-required', "false");
            }
        });

        $form.find('[data-datafield="RevenueFilterMode"]').on('change', e => {
            const filterMode = FwFormField.getValueByDataField($form, 'RevenueFilterMode');
            const $filterAmountField = $form.find('[data-datafield="RevenueFilterAmount"]');
            $filterAmountField.removeClass('error');
            if (filterMode === "ALL") {
                FwFormField.disable($filterAmountField);
                $filterAmountField.attr('data-required', "false");
            } else {
                FwFormField.enable($filterAmountField);
                $filterAmountField.attr('data-required', "true");

            }
        });

        $form.find('[data-datafield="IncludeNotRentedSince"]').on('change', e => {
            const $includeNotRentedSinceDateField = $form.find('[data-datafield="NotRentedSinceDate"]');
            const isChecked = FwFormField.getValueByDataField($form, 'IncludeNotRentedSince');
            if (isChecked) {
                FwFormField.enableDataField($form, 'NotRentedSinceDate');
                $includeNotRentedSinceDateField.attr('data-required', "true");
            } else {
                FwFormField.disableDataField($form, 'NotRentedSinceDate');
                $includeNotRentedSinceDateField.attr('data-required', "false");
            }
        });

    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="Ranks"]'), [
            { value: "A", text: "A", selected: "T" },
            { value: "B", text: "B", selected: "T" },
            { value: "C", text: "C", selected: "T" },
            { value: "D", text: "D", selected: "T" },
            { value: "E", text: "E", selected: "T" },
            { value: "F", text: "F", selected: "T" },
            { value: "G", text: "G", selected: "T" }
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="TrackedBys"]'), [
            { value: "BARCODE", text: "Bar Code", selected: "T" },
            { value: "QUANTITY", text: "Quantity", selected: "T" },
            { value: "SERIALNO", text: "Serial No.", selected: "T" },
            { value: "RFID", text: "RFID", selected: "F" }
        ]);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {

        const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        request.uniqueids = {};

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
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
        }

    }
};

var RentalInventoryStatusAndRevenueReportController: any = new RentalInventoryStatusAndRevenueReport();