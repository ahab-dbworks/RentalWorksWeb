routes.push({
    pattern: /^reports\/receiptbatchreport/, action: function (match: RegExpExecArray) {
        return RwReceiptBatchReportController.getModuleScreen();
    }
});

var receiptBatchTemplateFrontEnd = `
    <div class="fwcontrol fwcontainer fwform fwreport receiptbatchreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Receipt Invoice Batch" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwReceiptBatchReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Batch Number">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Receipt Processing Batch Number" data-datafield="BatchId" data-displayfield="BatchNumber" data-validationname="ReceiptProcessBatchValidation" style="float:left;max-width:300px;"></div>
                </div>
              </div>
            </div> 
            <div class="flexcolumn" style="max-width:210px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Data By">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="OrderBy" data-control="FwFormField" data-checkboxlist="persist" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-sortable="true" data-orderby="true" style="float:left;width:500px;margin-top:-2px"></div>
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
class RwReceiptBatchReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ReceiptBatchReport', 'api/v1/receiptbatchreport', receiptBatchTemplateFrontEnd);
        //this.reportOptions.HasDownloadExcel = true;
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(`Rw${this.Module}Controller`);
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
        this.loadLists($form);
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="OrderBy"]'),
            [
                { value: "CUSTOMER", text: "Customer", selected: "T" },
                { value: "DEAL", text: "Deal", selected: "T" },
                { value: "INVOICENO", text: "Invoice No.", selected: "T" },
                { value: "ORDERNO", text: "Order No.", selected: "T" },
            ]);
    }
    //----------------------------------------------------------------------------------------------
};

var RwReceiptBatchReportController: any = new RwReceiptBatchReportClass();
//----------------------------------------------------------------------------------------------