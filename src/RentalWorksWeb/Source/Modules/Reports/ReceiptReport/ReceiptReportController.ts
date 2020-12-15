routes.push({
    pattern: /^reports\/receiptreport/, action: function (match: RegExpExecArray) {
        return ReceiptReportController.getModuleScreen();
    }
});

const receiptTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport printreceipt" data-control="FwContainer" data-type="form" data-caption="Print Receipt" data-hasaudit="false" data-controller="ReceiptReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depleting Deposit">
                <!--div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order" data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" data-savesetting="false" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="DealNumber" data-validationname="DealValidation" data-savesetting="false" style="float:left;max-width:300px;"></div>
                </div-->
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Receipt" data-datafield="ReceiptId" data-displayfield="CheckNumber" data-validationname="ReceiptValidation" data-savesetting="false" style="float:left;max-width:300px;"></div>
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
class ReceiptReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ReceiptReport', 'api/v1/receiptreport', receiptTemplate);
        this.reportOptions.HasDownloadExcel = false;
        this.designerProvisioned = true;
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
            case 'OrderId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorder`);
                break;
            case 'DealId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'ReceiptId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatereceipt`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
};
var ReceiptReportController: any = new ReceiptReport();
//----------------------------------------------------------------------------------------------