routes.push({
    pattern: /^reports\/creditsonaccountreport/, action: function (match: RegExpExecArray) {
        return RwCreditsOnAccountReportController.getModuleScreen();
    }
});

var templateCreditsOnAccountFrontEnd = `
<div class="fwcontrol fwcontainer fwform fwreport creditsonaccount" data-control="FwContainer" data-type="form" data-version="1" data-caption="Credits On Account" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwCreditsOnAccountReportController">
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
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Include Customers and Deals with Remaining Balance" data-datafield="IncludeRemainingBalance" style="float:left;max-width:400px;"></div>
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
class RwCreditsOnAccountReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('CreditsOnAccountReport', 'api/v1/creditsonaccountreport', templateCreditsOnAccountFrontEnd);
        //this.reportOptions.HasDownloadExcel = true;
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        let $form = this.openForm();

        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () {
        };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        let $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        var appOptions: any = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };

        FwFormField.setValue($form, 'div[data-datafield="IncludeRemainingBalance"]', 'T');
    };
    //----------------------------------------------------------------------------------------------
};

var RwCreditsOnAccountReportController: any = new RwCreditsOnAccountReport();