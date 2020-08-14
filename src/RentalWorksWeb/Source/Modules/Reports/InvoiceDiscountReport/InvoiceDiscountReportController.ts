routes.push({
    pattern: /^reports\/invoicediscountreport/, action: function (match: RegExpExecArray) {
        return InvoiceDiscountReportController.getModuleScreen();
    }
});

const invoiceDiscountTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport invoicediscountreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Agent Billing" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="InvoiceDiscountReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
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
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Type">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield datatype" data-datafield="DateType" style="float:left;max-width:200px;">
                    <div data-value="INVOICE_DATE" data-caption="Invoice Date"></div>
                    <div data-value="BILLING_START_DATE" data-caption="Billing Start Date"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:250px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="DiscountPercent" data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-required="true" data-caption="Show Invoices with a Discount Percent </br> Greater than or Equal to" style="float:left;max-width:420px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal"  data-validationname="DealValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
class InvoiceDiscountReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('InvoiceDiscountReport', 'api/v1/invoicediscountreport', invoiceDiscountTemplate);
        this.reportOptions.HasDownloadExcel = true;
        this.designerProvisioned = true;
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
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        $form.find('div[data-datafield="DiscountPercent"] .fwformfield-caption').css('margin-bottom', "2em");

        // Default settings for first time running
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        const today = FwLocale.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        const aMonthAgo = FwLocale.getDate(today, null, { Quantity: -1, ObjectModified: 'months' });
        FwFormField.setValueByDataField($form, 'FromDate', aMonthAgo);
        FwFormField.setValueByDataField($form, 'DiscountPercent', '1');
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    //beforeValidate($browse, $form, request) {
    //    const validationName = request.module;
    //    const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
    //    request.uniqueids = {};

    //    switch (validationName) {
    //        case 'DealValidation':
    //            if (customerId !== "") {
    //                request.uniqueids.CustomerId = customerId;
    //            }
    //            break;
    //    };
    //};
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'CustomerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'DealId':
                if (customerId !== "") {
                    request.uniqueids.CustomerId = customerId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'DiscountReasonId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatediscountreason`);
                break;
        }
    }
};

var InvoiceDiscountReportController: any = new InvoiceDiscountReport();
//----------------------------------------------------------------------------------------------