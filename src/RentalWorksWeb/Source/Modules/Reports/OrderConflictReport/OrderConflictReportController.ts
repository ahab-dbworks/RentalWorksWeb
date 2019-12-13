routes.push({
    pattern: /^reports\/orderconflictreport/, action: function (match: RegExpExecArray) {
        return OrderConflictReportController.getModuleScreen();
    }
});

const orderConflictTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Availability Item Conflict" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="OrderConflictReportController">
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
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inventory Type">
                <div data-datafield="AvailableFor" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="R" data-caption="Rental"></div>
                  <div data-value="S" data-caption="Sales"></div>
                  <div data-value="" data-caption="All"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Conflict Type">
                <div data-datafield="ConflictType" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="P" data-caption="Positive"></div>
                  <div data-value="N" data-caption="Negative"></div>
                  <div data-value="ALL" data-caption="All"></div>
                </div>
              </div>
            </div>
           <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Classification">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="Classifications" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:200px;"></div>
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
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Order" data-datafield="OrderId" data-formbeforevalidate="beforeValidate" data-displayfield="Order" data-validationname="OrderValidation" data-validationpeek="false" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
</div>
`;
//----------------------------------------------------------------------------------------------
class OrderConflictReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('OrderConflictReport', 'api/v1/orderconflictreport', orderConflictTemplate);
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
    afterLoad($form) {
        const today = FwFunc.getDate();
        const twoWeeks = FwFunc.getDate(today, 14);
        FwFormField.setValueByDataField($form, 'FromDate', today);
        FwFormField.setValueByDataField($form, 'ToDate', twoWeeks);

        $form.find('.date-field').on('changeDate', event => {
            this.dateValidation($form, event);
        });
        FwFormField.setValueByDataField($form, 'ConflictType', 'N');
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="Classifications"]'), [{ value: "I", text: "Item", selected: "T" }, { value: "A", text: "Accessory", selected: "T" }, { value: "C", text: "Complete", selected: "T" }, { value: "K", text: "Kit", selected: "T" }, { value: "N", text: "Container", selected: "T" }, { value: "M", text: "Miscellaneous", selected: "F" }]);
        FwFormField.loadItems($form.find('div[data-datafield="TrackedBys"]'), [{ value: "BARCODE", text: "Barcode", selected: "T" }, { value: "QUANTITY", text: "Quantity", selected: "T" }, { value: "SERIALNO", text: "Serial Number", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="Ranks"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }, { value: "E", text: "E", selected: "T" }, { value: "F", text: "F", selected: "T" }, { value: "G", text: "G", selected: "T" }]);
    }
    //----------------------------------------------------------------------------------------------
    //beforeValidate = function ($browse, $form, request) {
    //    const validationName = request.module;
    //    const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
    //    const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
    //    const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
    //    if (validationName != null) {
    //        const dealId = FwFormField.getValueByDataField($form, 'DealId');
    //        request.uniqueids = {};

    //        switch (validationName) {
    //            case 'OrderValidation':
    //                if (dealId !== "") {
    //                    request.uniqueids.DealId = dealId;
    //                }
    //                break;
    //            case 'InventoryTypeValidation':
    //                request.uniqueids.Rental = true;
    //                break;
    //            case 'RentalCategoryValidation':
    //                if (inventoryTypeId !== "") {
    //                    request.uniqueids.InventoryTypeId = inventoryTypeId;
    //                }
    //                break;
    //            case 'SubCategoryValidation':
    //                request.uniqueids.Rental = true;
    //                if (inventoryTypeId !== "") {
    //                    request.uniqueids.InventoryTypeId = inventoryTypeId;
    //                }
    //                if (categoryId !== "") {
    //                    request.uniqueids.CategoryId = categoryId;
    //                }
    //                break;
    //            case 'RentalInventoryValidation':
    //                if (inventoryTypeId !== "") {
    //                    request.uniqueids.InventoryTypeId = inventoryTypeId;
    //                };
    //                if (categoryId !== "") {
    //                    request.uniqueids.CategoryId = categoryId;
    //                };
    //                if (subCategoryId !== "") {
    //                    request.uniqueids.SubCategoryId = subCategoryId;
    //                };
    //                break;
    //        }
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        request.uniqueids = {};

        switch (datafield) {
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'DealId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'OrderId':
                if (dealId !== "") {
                    request.uniqueids.DealId = dealId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'InventoryTypeId':
                request.uniqueids.Rental = true;
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'CategoryId':
                if (inventoryTypeId !== "") {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'SubCategoryId':
                request.uniqueids.Rental = true;
                if (inventoryTypeId !== "") {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                }
                if (categoryId !== "") {
                    request.uniqueids.CategoryId = categoryId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
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
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
        };
    }
    //----------------------------------------------------------------------------------------------
    dateValidation = function ($form, event) {
        const $element = jQuery(event.currentTarget);
        const todayParsed = Date.parse(FwFunc.getDate());
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

var OrderConflictReportController: any = new OrderConflictReport();