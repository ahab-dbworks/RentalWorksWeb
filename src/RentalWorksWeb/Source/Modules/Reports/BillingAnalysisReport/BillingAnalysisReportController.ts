routes.push({
    pattern: /^reports\/billinganalysisreport/, action: function (match: RegExpExecArray) {
        return BillingAnalysisReportController.getModuleScreen();
    }
});

const billingAnalysisReportTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Billing Analysis Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="BillingAnalysisReportController">
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
                  <!--<div class="flexcolumn">
                    <div data-datafield="" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield " data-caption="" style="float:left;">
                      <div data-value="" data-caption="Estimated Start Date"></div>
                      <div data-value="" data-caption="Create Date"></div>
                    </div>
                  </div>-->
                  <div class="flexcolumn">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From:" data-datafield="FromDate" data-required="true"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To:" data-datafield="ToDate" data-required="true"></div>
                  </div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Project Status" data-datafield="IncludeProjectStatus"></div>
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Credits Invoiced" data-datafield="IncludeCreditsInvoiced"></div>         
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude Orders Billed in Total" data-datafield="ExcludeOrdersBilledInTotal"></div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Include" style="max-height:170px;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="IncludeFilter" style="float:left;max-width:200px;"></div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Include Tax">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="IncludeTaxFilter" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="Status" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-showinactivemenu="true" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Project" data-datafield="ProjectId" data-displayfield="Project" data-validationname="ProjectValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-validationname="UserValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
class BillingAnalysisReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('BillingAnalysisReport', 'api/v1/billinganalysisreport', billingAnalysisReportTemplate);
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
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        const aMonthAgo = FwFunc.getDate(today, -30);
        FwFormField.setValueByDataField($form, 'FromDate', aMonthAgo);

        this.loadLists($form);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="IncludeFilter"]'), [
            { value: "Rental", text: "Rental", selected: "T" },
            { value: "Sales", text: "Sales", selected: "T" },
            { value: "Misc", text: "Misc", selected: "T" },
            { value: "Labor", text: "Labor", selected: "T" },
            { value: "Ld", text: "L&D", selected: "T" },
            { value: "RentalSale", text: "Used Sale", selected: "T" }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="IncludeTaxFilter"]'), [
            { value: "OrderTotal", text: "Order Total", selected: "T" },
            { value: "Cost", text: "Cost", selected: "T" },
            { value: "ToBill", text: "To Bill", selected: "T" },
            { value: "VendorInvoice", text: "Vendor Invoice", selected: "T" }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="Status"]'), [
            { value: "CONFIRMED", text: "Confirmed", selected: "T" },
            { value: "ACTIVE", text: "Active", selected: "T" },
            { value: "COMPLETE", text: "Complete", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" },
            { value: "HOLD", text: "Hold", selected: "T" },
            { value: "PROSPECT QUOTE", text: "Prospect Quote", selected: "T" },
            { value: "ACTIVE QUOTE", text: "Active Quote", selected: "T" }
        ]);
    }
    //----------------------------------------------------------------------------------------------
};

var BillingAnalysisReportController: any = new BillingAnalysisReport();