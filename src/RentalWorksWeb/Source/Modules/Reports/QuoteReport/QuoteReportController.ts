routes.push({
    pattern: /^reports\/quotereport/, action: function (match: RegExpExecArray) {
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

//----------------------------------------------------------------------------------------------
class QuoteReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('QuoteReport', 'api/v1/quotereport', quoteTemplate);
        this.reportOptions.HasDownloadExcel = false;
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
        $form.attr('data-reportname', 'QuoteReport');
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        parameters.OrderId = parameters.QuoteId;
        parameters.isQuote = true;
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'QuoteId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatequote`);
                break;
        }
    }
};

var QuoteReportController: any = new QuoteReport();
//----------------------------------------------------------------------------------------------