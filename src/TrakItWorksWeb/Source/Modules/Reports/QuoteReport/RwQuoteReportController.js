routes.push({
    pattern: /^reports\/orderreport/, action: function (match) {
        return RwQuoteReportController.getModuleScreen();
    }
});
const quoteTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport printorder" data-control="FwContainer" data-type="form" data-version="1" data-caption="Print Quote" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwQuoteReportController">
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
class RwQuoteReportClass extends FwWebApiReport {
    constructor() {
        super('OrderReport', 'api/v1/orderreport', quoteTemplate);
        this.reportOptions.HasDownloadExcel = false;
    }
    getModuleScreen() {
        const screen = {};
        screen.$view = FwModule.getModuleControl(`Rw${this.Module}Controller`);
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
        return $form;
    }
    onLoadForm($form) {
        this.load($form, this.reportOptions);
    }
    convertParameters(parameters) {
        const convertedParams = {};
        convertedParams.OrderId = parameters.QuoteId;
        convertedParams.isQuote = true;
        return convertedParams;
    }
}
;
var RwQuoteReportController = new RwQuoteReportClass();
//# sourceMappingURL=RwQuoteReportController.js.map