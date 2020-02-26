routes.push({
    pattern: /^reports\/createinvoiceprocessreport/, action: function (match: RegExpExecArray) {
        return CreateInvoiceProcessReportController.getModuleScreen();
    }
});

const createInvoiceProcessTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport createinvoiceprocessreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Create Invoice Process" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="CreateInvoiceProcessReportController">
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
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Invoice Creation Batch Number" data-datafield="InvoiceCreationBatchId" data-displayfield="BatchNumber" data-validationname="InvoiceCreationBatchValidation" data-savesetting="false" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Exceptions Only" data-datafield="ExceptionsOnly" style="float:left;max-width:300px;"></div>
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

//----------------------------------------------------------------------------------------------
class CreateInvoiceProcessReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('CreateInvoiceProcessReport', 'api/v1/createinvoiceprocessreport', createInvoiceProcessTemplate);
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
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InvoiceCreationBatchId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinvoicecreationbatch`);
                break;
        }
    }
};

var CreateInvoiceProcessReportController: any = new CreateInvoiceProcessReport();
//----------------------------------------------------------------------------------------------