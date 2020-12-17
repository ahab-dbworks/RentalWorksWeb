//----------------------------------------------------------------------------------------------
class VistekProcessCreditCard implements IProcessCreditCard {
    process($form: JQuery) {
        let html =
            `<div class="request" style="min-width:350px;font-size:.8em;">
  <div class="flexrow">
    <div style="flex:0 0 130px;">Deal:</div>
    <div>${FwFormField.getValueByDataField($form, 'Deal')}</div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">Order:</div>
    <div>${FwFormField.getValueByDataField($form, 'OrderDescription')}</div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">PIN Pad Code:</div>
    <div>${FwFormField.getTextByDataField($form, 'PINPad_Code')}</div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">PIN Pad Type:</div>
    <div>${FwFormField.getValueByDataField($form, 'PINPad_Type')}</div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">Amount to Pay:</div>
    <div>$${FwFormField.getValueByDataField($form, 'Payment_AmountToPay')}</div>
  </div>
</div>
<div class="response" style="display:none">
  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Response">
    <div class="flexrow">
      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield Result" data-caption="Result" data-datafield="Result"  data-enabled="false"></div>
    </div>
    <div class="flexrow">
      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Authorization Code" data-datafield="AuthorizationCode" data-enabled="false"></div>
    </div>
    <div class="flexrow">
      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Card Entry Mode" data-datafield="CardEntryMode" data-enabled="false"></div>
    </div>
    <!--div class="flexrow">
      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="AID" data-datafield="AID" data-enabled="false"></div>
    </div-->
    <div class="flexrow">
      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="CardType" data-enabled="false"></div>
    </div>
    <div class="flexrow">
      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="Amount" data-enabled="false"></div>
    </div>
  </div>
<div>
`;
        const $processCreditCardScreen = jQuery(html);

        const $confirmation = FwConfirmation.renderConfirmation('Process Credit Card Payment?', '');
        FwConfirmation.addJqueryControl($confirmation, $processCreditCardScreen);
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
                    DealNumber: FwFormField.getValueByDataField($form, 'DealNumber')
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