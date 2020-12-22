﻿//----------------------------------------------------------------------------------------------
class DepletingDeposit {
    static showAddDepletingDepositPopup($browse: JQuery, orderId: string, order: string, dealId: string, deal: string) {
        let html =
`<div style="min-width:350px;font-size:.8em;">
  <div class="flexrow">
    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-enabled="false" style="flex:1 0 150px;" data-required="true"></div>
  </div>
  <div class="flexrow">
    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-enabled="false" data-datafield="ReceiptDate" style="flex:1 0 150px;"></div>
    <div></div>
  </div>
  <div class="flexrow">
    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" data-validationname="PaymentTypeValidation" style="flex:1 0 150px;" data-required="true"></div>
  </div>
  <div class="flexrow">
    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Check/Reference No" data-datafield="CheckNumber" data-required="true" style="flex:1 0 150px;"></div>
  </div>
  <div class="flexrow">
    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Currency" data-datafield="CurrencyId" data-displayfield="CurrencyCode" data-validationname="CurrencyValidation" data-enabled="true" style="flex:1 0 150px;" data-required="true"></div>
  </div>  
<div class="flexrow">
    <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount to Apply" data-datafield="PaymentAmount" data-required="true" style="flex:1 0 150px;"></div>
  </div>
  <div class="flexrow">
    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Payment Memo">
      <div class="flexrow">
        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-maxlength="255" data-caption="" data-datafield="PaymentMemo"></div>
      </div>
    </div>
  </div>
</div>
`;
        const $adddepletingdeposit = jQuery(html);
        const $confirmation = FwConfirmation.renderConfirmation('Add Depleting Deposit', '');
        FwConfirmation.addJqueryControl($confirmation, $adddepletingdeposit);
        const $btnProcess = FwConfirmation.addButton($confirmation, 'OK', false);
        const location = JSON.parse(sessionStorage.getItem('location'));
        const today = FwFunc.getShortIsoDate();

        FwFormField.setValueByDataField($confirmation, "ReceiptDate", today);
        if (dealId.length > 0) {
            FwFormField.setValueByDataField($confirmation, "DealId", dealId, deal);
        } else {
            FwFormField.enableDataField($confirmation, 'DealId');
        }
        FwFormField.setValueByDataField($confirmation, 'CurrencyId', location.defaultcurrencyid, location.defaultcurrencycode);
        const $paymentTypeId = FwFormField.getDataField($confirmation, 'PaymentTypeId');
        $paymentTypeId.data('beforevalidate', ($validationbrowse: JQuery, $object: JQuery, request: any, datafield: string, $tr: JQuery) => {
            request.filterfields = {
                'PaymentTypeType': 'DEPLETING DEPOSIT'
            }
        })

        $btnProcess.on('click', async (e: JQuery.ClickEvent) => {
            try {
                const request = new FwAjaxRequest();
                request.httpMethod = 'POST';
                request.setWebApiUrl('/api/v1/receipt/adddepletingdeposit');
                request.data = {
                    LocationId: location.locationid,
                    CurrencyId: FwFormField.getValueByDataField($adddepletingdeposit, 'CurrencyId'),
                    OrderId: orderId,
                    DealId: FwFormField.getValueByDataField($adddepletingdeposit, 'DealId'),
                    ReceiptDate: FwFormField.getValueByDataField($adddepletingdeposit, 'ReceiptDate'),
                    PaymentTypeId: FwFormField.getValueByDataField($adddepletingdeposit, 'PaymentTypeId'),
                    CheckNumber: FwFormField.getValueByDataField($adddepletingdeposit, 'CheckNumber'),
                    PaymentAmount: FwFormField.getValueByDataField($adddepletingdeposit, 'PaymentAmount'),
                    PaymentMemo: FwFormField.getValueByDataField($adddepletingdeposit, 'PaymentMemo')
                };
                request.$elementToBlock = jQuery('body');
                var response = await FwAjax.callWebApi<any, any>(request);
                if (request.xmlHttpRequest.status === 200) {
                    if (response.Status === 'ERROR') {
                        FwFunc.showWebApiError(request.xmlHttpRequest.status, request.xmlHttpRequest.statusText, request.xmlHttpRequest.responseText, request.url);
                        return;
                    }
                    console.log(response);
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                } else {
                    FwFunc.showWebApiError(request.xmlHttpRequest.status, request.xmlHttpRequest.statusText, request.xmlHttpRequest.responseText, request.url);
                }
                FwConfirmation.destroyConfirmation($confirmation);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const $btnCancel = FwConfirmation.addButton($confirmation, 'Close', true);
    }
}