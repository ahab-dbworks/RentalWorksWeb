routes.push({
    pattern: /^reports\/quoteordermasterreport/, action: function (match: RegExpExecArray) {
        return QuoteOrderMasterReportController.getModuleScreen();
    }
});

const quoteordermasterTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Quote / Order Master Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuoteOrderMasterReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:230px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filter Order by Date">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="FilterDate" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield filter-dates" data-caption="Filter Dates" style="float:left;max-width:110px;"></div>
                    <div data-datafield="DateType" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-enabled="false">
                      <div data-value="ORDER_DATE" data-caption="Quote / Order Date"></div>
                      <div data-value="ESTIMATED_START_DATE" data-caption="Quote / Order Estimated Start Date"></div>
                    </div>
                  <div data-datafield="FromDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From" data-enabled="false" style="float:left;max-width:160px;"></div>
                  <div data-datafield="ToDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To" data-enabled="false" style="float:left;max-width:160px;"></div>
                </div>
              </div>
            </div>
             <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Type">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="OrderType" style="float:left;max-width:110px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="QuoteStatus" style="float:left;max-width:125px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="OrderStatus" style="float:left;max-width:125px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal Type" data-datafield="DealTypeId" data-displayfield="DealType" data-validationname="DealTypeValidation" style="float:left;min-width:400px;"></div>
                </div>
               <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal Status" data-datafield="DealStatusId" data-displayfield="DealStatus" data-formbeforevalidate="beforeValidate" data-validationname="DealStatusValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-formbeforevalidate="beforeValidate" data-validationname="DealValidation" style="float:left;min-width:400px;"></div>
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
class QuoteOrderMasterReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('QuoteOrderMasterReport', 'api/v1/quoteordermasterreport', quoteordermasterTemplate);
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
        screen.unload = function () {
        };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();

        // Expose date fields if Filter Date
        $form.on('change', '.filter-dates input[type=checkbox]', e => {
            const filterDate = FwFormField.getValueByDataField($form, 'FilterDate');
            FwFormField.toggle($form.find('div[data-datafield="DateType"]'), filterDate);
            FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), filterDate);
            FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), filterDate);
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);

        this.loadLists($form);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="QuoteStatus"]'), [
            { value: "PROSPECT", text: "Prospect", selected: "T" },
            { value: "ACTIVE", text: "Active", selected: "T" },
            { value: "HOLD", text: "Hold", selected: "T" },
            { value: "RESERVED", text: "Reserved", selected: "T" },
            { value: "ORDERED", text: "Ordered", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" },
            { value: "CANCELLED", text: "Cancelled", selected: "T" },
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="OrderStatus"]'), [
            { value: "CONFIRMED", text: "Confirmed", selected: "T" },
            { value: "ACTIVE", text: "Active", selected: "T" },
            { value: "HOLD", text: "Hold", selected: "T" },
            { value: "COMPLETE", text: "Complete", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" },
            { value: "CANCELLED", text: "Cancelled", selected: "T" },
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="OrderType"]'), [
            { value: "Q", text: "Quote", selected: "T" },
            { value: "O", text: "Order", selected: "T" },
        ]);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        // Filter Dates
        const filterDate = FwFormField.getValueByDataField($form, 'FilterDate');
        FwFormField.toggle($form.find('div[data-datafield="DateType"]'), filterDate);
        FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), filterDate);
        FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), filterDate);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const dealId = FwFormField.getValueByDataField($form, 'DealId');


        request.uniqueids = {};

        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids.Rental = true;
                break;
            case 'DealValidation':
                if (customerId !== "") {
                    request.uniqueids.CustomerId = customerId;
                }
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
};

var QuoteOrderMasterReportController: any = new QuoteOrderMasterReport();
//----------------------------------------------------------------------------------------------