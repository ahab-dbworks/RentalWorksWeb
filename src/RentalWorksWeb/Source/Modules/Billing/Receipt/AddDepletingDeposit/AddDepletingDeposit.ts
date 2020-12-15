//----------------------------------------------------------------------------------------------
class AddDepletingDeposit {
process(dealId: string, deal: string) {
let html =
`<div class="request" style="min-width:350px;font-size:.8em;">
  <div class="flexrow">
    <div style="flex:0 0 130px;">Deal Name:</div>
    <div>${deal}</div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">Date:</div>
    <div></div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">Payment Type:</div>
    <div></div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">Check/Reference No.:</div>
    <div></div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">Amount to Apply:</div>
    <div></div>
  </div>
</div>
`;
        const $adddepletingdeposit = jQuery(html);

        const $confirmation = FwConfirmation.renderConfirmation('Process Credit Card Payment?', '');
        FwConfirmation.addJqueryControl($confirmation, $adddepletingdeposit);
        const $btnProcess = FwConfirmation.addButton($confirmation, 'Process', false);
        $btnProcess.on('click', async (e: JQuery.ClickEvent) => {
            try {
                const request = new FwAjaxRequest();
                request.httpMethod = 'POST';
                request.setWebApiUrl('/api/v1/processcreditcard/processcreditcardpayment');
                //const datafields = FwReport.getParameters($form);
                request.data = {
                    PINPadCode: FwFormField.getValueByDataField($form, 'PINPad_Code'),
                    PaymentAmount: FwFormField.getValueByDataField($form, 'Payment_AmountToPay'),
                    OrderId: FwFormField.getValueByDataField($form, 'OrderId'),
                    CustomerNo: FwFormField.getValueByDataField($form, 'CustomerNo')
                };
                request.$elementToBlock = jQuery('body');
                var response = await FwAjax.callWebApi<any, any>(request);
                if (request.xmlHttpRequest.status === 200) {
                    if (response.Status === 'ERROR') {
                        FwFunc.showWebApiError(request.xmlHttpRequest.status, request.xmlHttpRequest.statusText, request.xmlHttpRequest.responseText, request.url);
                        return;
                    }
                    console.log(response);
                    $confirmation.find('.title').text('PIN Pad');
                    $confirmation.find('.request').hide();
                    $confirmation.find('.response').show();
                    FwFormField.setValueByDataField($confirmation, 'Result', response.StatusText);
                    FwFormField.setValueByDataField($confirmation, 'AuthorizationCode', response.AuthorizationCode);
                    FwFormField.setValueByDataField($confirmation, 'CardEntryMode', response.CardEntryMode);
                    FwFormField.setValueByDataField($confirmation, 'CardType', response.CardType);
                    FwFormField.setValueByDataField($confirmation, 'Amount', response.Amount);
                    if (response.Status.toUpperCase() === 'APPROVED') {
                        $confirmation.find('.response .Result .fwformfield-value')
                            .val('APPROVED')
                            .css({
                                backgroundColor: '#a6d785'
                            });
                    }
                    else if (response.Status.toUpperCase() === 'DECLINED') {
                        $confirmation.find('.response .Result .fwformfield-value').css({
                            backgroundColor: '#ff8a80'
                        });
                    }
                    $btnProcess.remove();
                    $btnCancel.text('Close');
                } else {
                    FwFunc.showWebApiError(request.xmlHttpRequest.status, request.xmlHttpRequest.statusText, request.xmlHttpRequest.responseText, request.url);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const $btnCancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
    }
}