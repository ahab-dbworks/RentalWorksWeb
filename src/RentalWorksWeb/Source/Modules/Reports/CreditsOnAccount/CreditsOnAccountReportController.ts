routes.push({
    pattern: /^reports\/creditsonaccountreport/, action: function (match: RegExpExecArray) {
        return CreditsOnAccountReportController.getModuleScreen();
    }
});

const creditsOnAccountTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport creditsonaccount" data-control="FwContainer" data-type="form" data-version="1" data-caption="Credits On Account" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="CreditsOnAccountReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:410px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credits On Account">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Include Customers and Deals with Remaining Balance" data-datafield="OnlyRemaining" style="float:left;max-width:400px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidateDeal" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
class CreditsOnAccountReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('CreditsOnAccountReport', 'api/v1/creditsonaccountreport', creditsOnAccountTemplate);
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
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        FwFormField.setValue($form, 'div[data-datafield="OnlyRemaining"]', 'T');
    };
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateDeal($browse: any, $form: any, request: any) {
        request.uniqueids = {};
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');

        if (customerId) {
            request.uniqueids.CustomerId = customerId;
        }
    };
    //----------------------------------------------------------------------------------------------
};

var CreditsOnAccountReportController: any = new CreditsOnAccountReport();