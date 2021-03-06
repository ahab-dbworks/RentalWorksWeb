routes.push({
    pattern: /^reports\/rentalinventoryavailabilityreport/, action: function (match: RegExpExecArray) {
        return RentalInventoryAvailabilityReportController.getModuleScreen();
    }
});

const rentalInventoryAvailTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Rental Inventory Availability Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RentalInventoryAvailabilityReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" style="display:flex;flex-wrap:wrap;" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield date-field" data-caption="From:" data-datafield="FromDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield date-field" data-caption="To:" data-datafield="ToDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <span style="color:red;padding-left:15px;font-size:.8em;">(Maximum of 30 Days)</span>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Type">
                  <div data-datafield="IsDetail" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                    <div data-value="false" data-caption="Summary"></div>
                    <div data-value="true" data-caption="Detail"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Classification">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="Classifications" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:200px;"></div>
                </div>
              </div>
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
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Fixed Asset">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="FixedAsset">
                    <div data-value="IncludeOnly" data-caption="Include Fixed Assets Only"></div>
                    <div data-value="Exclude" data-caption="Exclude Fixed Assets"></div>
                    <div data-value="All" data-caption="All"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:355px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="IncludeZeroQuantity" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Items with Zero Quantity" style="float:left;max-width:420px;"></div>
                  <div data-datafield="OnlyIncludeLowAndNegative" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Include Inventory with Low or Negative Availability" style="float:left;max-width:420px;"></div>
                  <div data-datafield="OnlyIncludeNegative" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Include Inventory with Negative Availability" style="float:left;max-width:420px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType"  data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category"  data-validationname="RentalCategoryValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub-Category" data-datafield="SubCategoryId"  data-displayfield="SubCategory" data-validationname="SubCategoryValidation" data-validationpeek="false" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
</div>
`;
//----------------------------------------------------------------------------------------------
class RentalInventoryAvailabilityReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RentalInventoryAvailabilityReport', 'api/v1/rentalinventoryavailabilityreport', rentalInventoryAvailTemplate);
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

        // Mutually exclusive option settings
        $form.on('change', 'div[data-datafield="OnlyIncludeLowAndNegative"] input[type=checkbox]', e => {
            if (FwFormField.getValueByDataField($form, 'OnlyIncludeLowAndNegative') === true) {
                $form.find('div[data-datafield="OnlyIncludeNegative"] input').prop('checked', false);
            }
        });
        $form.on('change', 'div[data-datafield="OnlyIncludeNegative"] input[type=checkbox]', e => {
            if (FwFormField.getValueByDataField($form, 'OnlyIncludeNegative') === true) {
                $form.find('div[data-datafield="OnlyIncludeLowAndNegative"] input').prop('checked', false);
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        // Default settings for first time running
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        FwFormField.setValueByDataField($form, 'FixedAsset', 'All');

        this.loadLists($form);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        const today = FwLocale.getDate();
        const twoWeeks = FwLocale.getDate(today, null, { Quantity: 2, ObjectModified: 'weeks' });
        FwFormField.setValueByDataField($form, 'FromDate', today);
        FwFormField.setValueByDataField($form, 'ToDate', twoWeeks);

        $form.find('.date-field').on('changeDate', event => {
            this.dateValidation($form, event);
        });
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="Classifications"]'), [{ value: "I", text: "Item", selected: "T" }, { value: "A", text: "Accessory", selected: "T" }, { value: "C", text: "Complete", selected: "T" }, { value: "K", text: "Kit", selected: "T" }, { value: "N", text: "Container", selected: "T" }, { value: "M", text: "Miscellaneous", selected: "F" }]);
        FwFormField.loadItems($form.find('div[data-datafield="TrackedBys"]'), [
            { value: "BARCODE", text: "Barcode", selected: "T" },
            { value: "QUANTITY", text: "Quantity", selected: "T" },
            { value: "SERIALNO", text: "Serial Number", selected: "T" },
            { value: "RFID", text: "RFID", selected: "T" },
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="Ranks"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }, { value: "E", text: "E", selected: "T" }, { value: "F", text: "F", selected: "T" }, { value: "G", text: "G", selected: "T" }]);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {

            const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
            const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
            const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
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
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterentalcategory`);
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
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validaterentalinventory`);
                    break;
                case 'WarehouseId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                    break;
            }
        }
    //----------------------------------------------------------------------------------------------
    dateValidation = function ($form, event) {
        const $element = jQuery(event.currentTarget);
        const todayParsed = Date.parse(FwLocale.getDate());
        const parsedFromDate = Date.parse(FwFormField.getValueByDataField($form, 'FromDate'));
        const parsedToDate = Date.parse(FwFormField.getValueByDataField($form, 'ToDate'));

        if ($element.attr('data-datafield') === 'FromDate' && parsedFromDate < todayParsed) {
            $form.find('div[data-datafield="FromDate"]').addClass('error dev-err');
            FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before Today's Date.");
        }
        else if (parsedToDate < parsedFromDate) {
            $form.find('div[data-datafield="ToDate"]').addClass('error dev-err');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
        }
        else {
            $form.find('div[data-datafield="FromDate"]').removeClass('error dev-err');
            $form.find('div[data-datafield="ToDate"]').removeClass('error dev-err');
        }
    }
    //----------------------------------------------------------------------------------------------
};

var RentalInventoryAvailabilityReportController: any = new RentalInventoryAvailabilityReport();