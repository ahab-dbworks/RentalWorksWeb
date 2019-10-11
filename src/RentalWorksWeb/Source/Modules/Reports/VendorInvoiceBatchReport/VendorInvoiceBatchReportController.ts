﻿routes.push({
    pattern: /^reports\/vendorinvoicebatchreport/, action: function (match: RegExpExecArray) {
        return VendorInvoiceBatchReportController.getModuleScreen();
    }
});

const vendorInvoiceBatchTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport vendorinvoicebatchreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Vendor Invoice Batch" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="VendorInvoiceBatchReportController">
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
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor Invoice Processing Batch Number" data-datafield="BatchId" data-displayfield="BatchNumber" data-savesetting="false" data-validationname="VendorInvoiceProcessBatchValidation" style="float:left;max-width:300px;"></div>
                </div>
                 <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield batchNumber" data-caption="Batch Number" data-datafield="BatchNumber" data-savesetting="false" style="display:none;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield batchDate" data-caption="Batch Date" data-datafield="BatchDate" data-savesetting="false" style="display:none;"></div>
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
class VendorInvoiceBatchReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('VendorInvoiceBatchReport', 'api/v1/vendorinvoicebatchreport', vendorInvoiceBatchTemplate);
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
        $form.find('[data-datafield="BatchId"]').data('onchange', e => {
            const batchNumber = FwFormField.getTextByDataField($form, 'BatchId');
            FwFormField.setValueByDataField($form, 'BatchNumber', batchNumber);
            const batchDate = jQuery(e).find('[data-browsedatafield="BatchDate"]').attr('data-originalvalue');
            FwFormField.setValueByDataField($form, 'BatchDate', batchDate);
        })
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
};

var VendorInvoiceBatchReportController: any = new VendorInvoiceBatchReport();
//----------------------------------------------------------------------------------------------