routes.push({
    pattern: /^reports\/salesquotebillingreport/, action: function (match: RegExpExecArray) {
        return SalesQuoteBillingReportController.getModuleScreen();
    }
});

const salesQuoteBillingTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Sales Quote Billing" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="SalesQuoteBillingReportController">
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
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="User" data-validationname="UserValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="float:left;min-width:400px;"></div>
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
class SalesQuoteBillingReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('SalesQuoteBillingReport', 'api/v1/salesquotebillingreport', salesQuoteBillingTemplate);
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
    convertParameters(parameters: any): any {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    //----------------------------------------------------------------------------------------------
}

var SalesQuoteBillingReportController: any = new SalesQuoteBillingReportClass();
//----------------------------------------------------------------------------------------------