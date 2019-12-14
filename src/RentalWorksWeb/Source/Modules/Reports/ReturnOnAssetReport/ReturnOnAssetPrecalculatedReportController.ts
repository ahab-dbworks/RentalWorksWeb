routes.push({
    pattern: /^reports\/returnonassetreport/, action: function (match: RegExpExecArray) {
        return ReturnOnAssetReportController.getModuleScreen();
    }
});

const returnOnAssetTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Return On Asset Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ReturnOnAssetReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:250px;">
             <div class="flexcolumn" style="max-width:350px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="ROA Period">
                <div data-datafield="UseDateValidation" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield filter-dates" data-caption="Use ROA Period" style="max-width:110px;"></div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Year" data-datafield="ReportYear" data-displayfield="Year" data-validationname="ReturnOnAssetYearValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Period" data-datafield="ReportPeriod" data-displayfield="Label" data-validationname="ReturnOnAssetPeriodValidation" style="float:left;max-width:300px;"></div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="ROA Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="UseDateRange" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield filter-dates" data-caption="Use ROA Date Range" style="max-width:110px;"></div>
                  <div class="flexcolumn">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield date-range" data-caption="From:" data-datafield="FromDate" data-required="true"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield date-range" data-caption="To:" data-datafield="ToDate" data-required="true"></div>
                  </div>
                </div>
              </div>
            </div>
            </div>
            <div class="flexcolumn" style="max-width:142px;">
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
            <div class="flexcolumn" style="max-width:208px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="IncludeZeroCurrentOwned" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Zero Current Owned" style="float:left;max-width:420px;"></div>
                  <div data-datafield="IncludeZeroAverageOwned" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Zero Average Owned" style="float:left;max-width:420px;"></div>
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
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub-Category" data-datafield="SubCategoryId" data-formbeforevalidate="beforeValidate" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" data-validationpeek="false" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
class ReturnOnAssetReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ReturnOnAssetReport', 'api/v1/returnonassetreport', returnOnAssetTemplate);
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
        FwFormField.setValue($form, 'div[data-datafield="ReportPeriod"]', 'FY', 'Full Year');
        const date = new Date();
        const year = date.getFullYear();
        FwFormField.setValue($form, 'div[data-datafield="ReportYear"]', year, year);

        // Expose date fields if Filter Date
        //$form.on('change', '.filter-dates input[type=checkbox]', e => {
        //    //const filterDate = FwFormField.getValueByDataField($form, 'UseDateRange');
        //    //FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), filterDate);
        //    //FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), filterDate);
        //    //FwFormField.toggle($form.find('div[data-datafield="ReportYear"]'), !filterDate);
        //    //FwFormField.toggle($form.find('div[data-datafield="ReportPeriod"]'), !filterDate);
        //    //$form.find('.date-range').attr('data-required', `${filterDate}`);
        //});

        // Mutually exclusive Image settings
        $form.on('change', 'div[data-datafield="UseDateRange"] input[type=checkbox]', e => {
            if (FwFormField.getValueByDataField($form, 'UseDateRange') === true) {
                $form.find('div[data-datafield="UseDateValidation"] input').prop('checked', false);
                FwFormField.toggle($form.find('div[data-datafield="ReportYear"]'), false);
                FwFormField.toggle($form.find('div[data-datafield="ReportPeriod"]'), false);
                FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), true);
                FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), true);
                $form.find('.date-range').attr('data-required', `true`);
            } else {
                $form.find('div[data-datafield="UseDateValidation"] input').prop('checked', true);
                FwFormField.toggle($form.find('div[data-datafield="ReportYear"]'), true);
                FwFormField.toggle($form.find('div[data-datafield="ReportPeriod"]'), true);
                FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), false);
                FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), false);
                $form.find('.date-range').attr('data-required', `false`);
            }
        });
        // Mutually exclusive Image settings
        $form.on('change', 'div[data-datafield="UseDateValidation"] input[type=checkbox]', e => {
            if (FwFormField.getValueByDataField($form, 'UseDateValidation') === true) {
                $form.find('div[data-datafield="UseDateRange"] input').prop('checked', false);
                FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), false);
                FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), false);
                $form.find('.date-range').attr('data-required', `false`);
                FwFormField.toggle($form.find('div[data-datafield="ReportYear"]'), true);
                FwFormField.toggle($form.find('div[data-datafield="ReportPeriod"]'), true);
            } else {
                $form.find('div[data-datafield="UseDateRange"] input').prop('checked', true);
                FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), true);
                FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), true);
                $form.find('.date-range').attr('data-required', `true`);
                FwFormField.toggle($form.find('div[data-datafield="ReportYear"]'), false);
                FwFormField.toggle($form.find('div[data-datafield="ReportPeriod"]'), false);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        // Filter Dates
        const useDateRange = FwFormField.getValueByDataField($form, 'UseDateRange');
        FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), useDateRange);
        FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), useDateRange);
        $form.find('.date-range').attr('data-required', `${useDateRange}`);
        FwFormField.toggle($form.find('div[data-datafield="ReportYear"]'), !useDateRange);
        FwFormField.toggle($form.find('div[data-datafield="ReportPeriod"]'), !useDateRange);
        FwFormField.setValueByDataField($form, 'UseDateValidation', !useDateRange);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {

            const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');


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
                case 'SubCategoryId':
                    request.uniqueids.Rental = true;
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    }
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
                    }
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                    break;
                case 'InventoryId':
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    };
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
                    };
                    if (subCategoryId !== "") {
                        request.uniqueids.SubCategoryId = subCategoryId;
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                    break;
                case 'WarehouseId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                    break;
            }
        
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="TrackedBys"]'), [{ value: "BARCODE", text: "Barcode", selected: "T" }, { value: "QUANTITY", text: "Quantity", selected: "T" }, { value: "SERIALNO", text: "Serial Number", selected: "T" }, { value: "RFID", text: "RFID", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="Ranks"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }, { value: "E", text: "E", selected: "T" }, { value: "F", text: "F", selected: "T" }, { value: "G", text: "G", selected: "T" }]);
    }
    //----------------------------------------------------------------------------------------------
};

var ReturnOnAssetReportController: any = new ReturnOnAssetReport();
//----------------------------------------------------------------------------------------------