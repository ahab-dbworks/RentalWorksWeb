routes.push({
    pattern: /^reports\/subrentalbillinganalysisreport/, action: function (match: RegExpExecArray) {
        return SubRentalBillingAnalysisReportController.getModuleScreen();
    }
});

const subRentalBIllingAnalysisReportTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Sub-Rental Billing Analysis Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="SubRentalBillingAnalysisReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" style="display:flex;flex-wrap:wrap;" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:350px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From:" data-datafield="FromDate" data-required="true" style="width:125px; float:left;"></div>
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To:" data-datafield="ToDate" data-required="true" style="width:125px; float:left;"></div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Vendor Tax" data-datafield="IncludeVendorTax"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="InvoiceStatus" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="PurchaseOrderStatus" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="PO Classification" data-datafield="PoClassificationId" data-displayfield="PoClassification" data-validationname="POClassificationValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Purchase Order" data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrder" data-validationname="PurchaseOrderValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-validationname="RentalInventoryValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
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
class SubRentalBillingAnalysisReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('SubRentalBillingAnalysisReport', 'api/v1/subrentalbillinganalysisreport', subRentalBIllingAnalysisReportTemplate);
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
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="Department"]', department.departmentid, department.department);
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        const today = FwLocale.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        const aMonthAgo = FwLocale.getDate(today, null, { Quantity: -1, ObjectModified: 'months' });
        FwFormField.setValueByDataField($form, 'FromDate', aMonthAgo);
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="InvoiceStatus"]'), [
            { value: "NEW", text: "New", selected: "T" },
            { value: "RETURNED", text: "Returned", selected: "T" },
            { value: "REVISED", text: "Revised", selected: "T" },
            { value: "APPROVED", text: "Approved", selected: "T" },
            { value: "PROCESSED", text: "Processed", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="PurchaseOrderStatus"]'), [
            { value: "NEW", text: "New", selected: "T" },
            { value: "OPEN", text: "Open", selected: "T" },
            { value: "RECEIVED", text: "Received", selected: "T" },
            { value: "COMPLETE", text: "Complete", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" }
        ]);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'DealId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'VendorId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatevendor`);
                break;
            case 'PoClassificationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepoclassification`);
                break;
            case 'PurchaseOrderId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepurchaseorder`);
                break;
            case 'InventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
};

var SubRentalBillingAnalysisReportController: any = new SubRentalBillingAnalysisReport();