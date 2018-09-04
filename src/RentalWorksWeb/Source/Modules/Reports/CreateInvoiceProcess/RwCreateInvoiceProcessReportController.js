routes.push({
    pattern: /^reports\/createinvoiceprocessreport/, action: function (match) {
        return RwCreateInvoiceProcessReportController.getModuleScreen();
    }
});
var createInvoiceProcessTemplateFrontEnd = `
<div class="fwcontrol fwcontainer fwform fwreport createinvoiceprocessreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Create Invoice Process" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwCreateInvoiceProcessReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Batch Number">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Invoice Creation Batch Number" data-datafield="BatchId" data-displayfield="BatchNumber" data-validationname="InvoiceCreationBatchValidation" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Exceptions Only" data-datafield="ShowExceptions" style="float:left;max-width:300px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div id="exporttabpage" class="tabpage exporttabpage" data-tabid="exporttab">
      </div>
    </div>
  </div>
</div>`;
class RwCreateInvoiceProcessReportClass extends FwWebApiReport {
    constructor() {
        super('CreateInvoiceProcessReport', 'api/v1/createinvoiceprocessreport', createInvoiceProcessTemplateFrontEnd);
    }
    getModuleScreen() {
        let screen = {};
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
    openForm() {
        let $form = this.getFrontEnd();
        $form.data('getexportrequest', request => {
            request.parameters = this.getParameters($form);
            return request;
        });
        return $form;
    }
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
    }
}
;
var RwCreateInvoiceProcessReportController = new RwCreateInvoiceProcessReportClass();
//# sourceMappingURL=RwCreateInvoiceProcessReportController.js.map