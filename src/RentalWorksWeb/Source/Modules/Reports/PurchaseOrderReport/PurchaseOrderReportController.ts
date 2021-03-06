﻿routes.push({
    pattern: /^reports\/purchaseorderreport/, action: function (match: RegExpExecArray) {
        return PurchaseOrderReportController.getModuleScreen();
    }
});

const purchaseOrderTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport printorder" data-control="FwContainer" data-type="form" data-version="1" data-caption="Print Purchase Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PurchaseOrderReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:500px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Order">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield order-contact-field" data-caption="Purchase Order" data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-savesetting="false" data-required="true" style="float:left;max-width:300px;"></div>
                </div>
                 <div data-datafield="IsSummary" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" style="margin-top:1rem">
                  <div data-value="true" data-caption="Summary - Hide no-cost Complete/Kit accessories"></div>
                  <div data-value="false" data-caption="Detail - Show all items"></div>
                </div>
               <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="CompanyIdField" data-savesetting="false" style="display:none;"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;

//----------------------------------------------------------------------------------------------
class PurchaseOrderReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('PurchaseOrderReport', 'api/v1/purchaseorderreport', purchaseOrderTemplate);
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
        $form.find('div[data-datafield="PurchaseOrderId"]').data('onchange', $tr => {
            $form.attr('data-caption', `Purchase Order ${$tr.find('.field[data-formdatafield="PurchaseOrderNumber"]').attr('data-originalvalue')} ${$tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue')}`);

            //set CompanyId value for filtering contact list
            FwFormField.setValueByDataField($form, 'CompanyIdField', FwBrowse.getValueByDataField($tr, $tr, 'VendorId'));
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
            case 'PurchaseOrderId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepurchaseorder`);
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

var PurchaseOrderReportController: any = new PurchaseOrderReport();
//----------------------------------------------------------------------------------------------