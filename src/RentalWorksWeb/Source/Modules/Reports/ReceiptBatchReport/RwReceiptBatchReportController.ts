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
                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield batchNumber" data-caption="Batch Number" data-datafield="BatchNumber" style="display:none;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield batchDate" data-caption="Batch Date" data-datafield="BatchDate" style="display:none;"></div>
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
        $form.find('[data-datafield="BatchId"]').data('onchange', e => {
            let batchNumber = FwFormField.getTextByDataField($form, 'BatchId');
            let batchDate = jQuery(e).find('[data-browsedatafield="BatchDate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'BatchNumber', batchNumber);
            FwFormField.setValueByDataField($form, 'BatchDate', batchDate);
        })
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        var appOptions: any = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };
    }
    //----------------------------------------------------------------------------------------------
};

var RwReceiptBatchReportController: any = new RwReceiptBatchReportClass();
//----------------------------------------------------------------------------------------------