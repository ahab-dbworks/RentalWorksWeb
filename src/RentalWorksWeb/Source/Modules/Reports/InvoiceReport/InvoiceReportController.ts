routes.push({
    pattern: /^reports\/invoicereport/, action: function (match: RegExpExecArray) {
        return InvoiceReportController.getModuleScreen();
    }
});

const invoiceTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport print-invoice" data-control="FwContainer" data-type="form" data-version="1" data-caption="Print Invoice" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="InvoiceReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:500px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Invoice" data-savesetting="false" data-required="true" data-datafield="InvoiceId" data-displayfield="InvoiceNumber" data-validationname="InvoiceValidation" style="float:left;max-width:300px;"></div>
                </div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="CompanyIdField" data-savesetting="false" style="display:none;"></div>
                <div data-datafield="IsSummary" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" style="margin-top:1rem">
                  <div data-value="true" data-caption="Summary - Hide no-cost Complete/Kit accessories"></div>
                  <div data-value="false" data-caption="Detail - Show all items"></div>
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
class InvoiceReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('InvoiceReport', 'api/v1/invoicereport', invoiceTemplate);
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

        // Store info for emailing subject line
        $form.find('div[data-datafield="InvoiceId"]').data('onchange', $tr => {
            $form.attr('data-caption', `Invoice ${$tr.find('.field[data-formdatafield="InvoiceNumber"]').attr('data-originalvalue')} ${$tr.find('.field[data-formdatafield="InvoiceDescription"]').attr('data-originalvalue')}`);

            //set CompanyId value for filtering contact list
            FwFormField.setValueByDataField($form, 'CompanyIdField', FwBrowse.getValueByDataField($tr, $tr, 'DealId'));
        });

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
            case 'InvoiceId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinvoice`);
                break;
            case 'tousers':
            case 'ccusers':
                const companyId = FwFormField.getValueByDataField($form, 'CompanyIdField');
                if (companyId != '') {
                    request.uniqueids = { CompanyId: companyId };
                }
                break;
        }
    }
};

var InvoiceReportController: any = new InvoiceReport();
//----------------------------------------------------------------------------------------------