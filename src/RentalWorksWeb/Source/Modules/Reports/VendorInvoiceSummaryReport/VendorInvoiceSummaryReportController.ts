routes.push({
    pattern: /^reports\/vendorinvoicesummaryreport/, action: function (match: RegExpExecArray) {
        return VendorInvoiceSummaryReportController.getModuleScreen();
    }
});
const vendorInvoiceSummaryTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Vendor Invoice Summary" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="VendorInvoiceSummaryReportController">
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
            <div class="flexcolumn" style="max-width:230px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Type">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield datatype" data-datafield="DateType" style="float:left;max-width:200px;">
                    <div data-value="INVOICE_DATE" data-caption="Invoice Date"></div>
                    <div data-value="BILLING_START_DATE" data-caption="Billing Start Date"></div>
                  </div>
                </div>
              </div>
              <div class="flexcolumn" style="max-width:230px;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-datafield="IncludeAccruals" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield include-accruals" data-caption="Include Accruals" style="float:left;max-width:110px;"></div>
                    <div data-datafield="AccrualFromDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Accrual From Date" data-enabled="false" style="float:left;max-width:160px;"></div>
                    <div data-datafield="AccrualToDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Accrual To Date" data-enabled="false" style="float:left;max-width:160px;"></div>
                    <div data-datafield="AccrualsOnly" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Accruals Only" data-enabled="false" style="float:left;max-width:110px;"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="Statuses" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-formbeforevalidate="beforeValidate" data-validationname="DealValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Purchase Order" data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrder" data-formbeforevalidate="beforeValidate" data-validationname="PurchaseOrderValidation" style="float:left;min-width:400px;"></div>
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
class VendorInvoiceSummaryReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('VendorInvoiceSummaryReport', 'api/v1/vendorinvoicesummaryreport', vendorInvoiceSummaryTemplate);
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
        // Expose date fields if Include Accruals
        $form.on('change', '.include-accruals input[type=checkbox]', e => {
            const isChecked = jQuery(e.currentTarget).is(':checked');
            FwFormField.setValueByDataField($form, 'AccrualFromDate', '');
            FwFormField.setValueByDataField($form, 'AccrualToDate', '');
            FwFormField.toggle($form.find('div[data-datafield="AccrualFromDate"]'), isChecked);
            FwFormField.toggle($form.find('div[data-datafield="AccrualToDate"]'), isChecked);
            $form.find('div[data-datafield="AccrualsOnly"] input').prop('checked', false);
            FwFormField.toggle($form.find('div[data-datafield="AccrualsOnly"]'), isChecked);
        });
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        this.loadLists($form);

        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));

        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        request.uniqueids = {};

        switch (validationName) {
            case 'PurchaseOrderValidation':
                if (vendorId !== "") {
                    request.uniqueids.VendorId = vendorId;
                }
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="Statuses"]'), [
            { value: "NEW", text: "New", selected: "T" },
            { value: "APPROVED", text: "Approved", selected: "T" },
            { value: "PROCESSED", text: "Processed", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" },
        ]);
    }
    //----------------------------------------------------------------------------------------------
};

var VendorInvoiceSummaryReportController: any = new VendorInvoiceSummaryReport();
//----------------------------------------------------------------------------------------------