routes.push({
    pattern: /^reports\/quotereport/,
    action: function (match) {
        return QuoteReportController.getModuleScreen();
    }
});
const quoteTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport printorder" data-control="FwContainer" data-type="form" data-version="1" data-caption="Print Quote" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="QuoteReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:300px;">
               <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="QuoteId" data-displayfield="QuoteNumber" data-savesetting="false" data-validationname="QuoteValidation" style="float:left;max-width:300px;"></div>
                </div>
              </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
class QuoteReport extends FwWebApiReport {
    constructor() {
        super('QuoteReport', 'api/v1/quoteeport', quoteTemplate);
        this.reportOptions.HasDownloadExcel = false;
    }
    getModuleScreen() {
        const screen = {};
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
    openForm() {
        const $form = this.getFrontEnd();
        $form.attr('data-reportname', 'QuoteReport');
        return $form;
    }
    onLoadForm($form) {
        this.load($form, this.reportOptions);
    }
    convertParameters(parameters) {
        const convertedParams = {};
        convertedParams.CustomReportLayoutId = parameters.CustomReportLayoutId;
        convertedParams.OrderId = parameters.QuoteId;
        convertedParams.isQuote = true;
        return convertedParams;
    }
}
;
var QuoteReportController = new QuoteReport();
//# sourceMappingURL=QuoteReportController.js.map