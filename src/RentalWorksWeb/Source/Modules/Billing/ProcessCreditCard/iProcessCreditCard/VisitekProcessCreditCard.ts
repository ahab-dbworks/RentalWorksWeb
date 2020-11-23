//----------------------------------------------------------------------------------------------
class VisitekProcessCreditCard implements IProcessCreditCard {
    process($form: JQuery) {
        let html =
            `<div class="request" style="min-width:350px;font-size:.8em;">
  <div class="flexrow">
    <div style="flex:0 0 130px;">Customer Name:</div>
    <div>${FwFormField.getValueByDataField($form, 'Customer')}</div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">Order:</div>
    <div>${FwFormField.getValueByDataField($form, 'OrderDescription')}</div>
  </div>
  <div class="flexrow">
    <div style="flex:0 0 130px;">PIN Pad Code:</div>
    <div>${FwFormField.getValueByDataField($form, 'PINPad_Code')}</div>
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
    <div class="flexrow">
      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="AID" data-datafield="AID" data-enabled="false"></div>
    </div>
    <div class="flexrow">
      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="Type" data-enabled="false"></div>
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
                request.setWebApiUrl('/api/v1/processcreditcard/processcreditcard');
                //const datafields = FwReport.getParameters($form);
                request.data = {
                    PINPad_Code: FwFormField.getValueByDataField($form, 'PINPad_Code'),
                    Payment_AmountToPay: FwFormField.getValueByDataField($form, 'Payment_AmountToPay'),
                    OrderNo: FwFormField.getValueByDataField($form, 'OrderNo'),
                    StoreCode: '',
                    SalesPersonCode: '',
                    CustomerNo: FwFormField.getValueByDataField($form, 'CustomerNo')
                };
                request.$elementToBlock = jQuery('body');
                var response = await FwAjax.callWebApi<any, any>(request);
                console.log(response);
                $confirmation.find('.title').text('PIN Pad');
                $confirmation.find('.request').hide();
                $confirmation.find('.response').show();
                if (response.Message && response.Message === 'APPROVED') {
                    $confirmation.find('.Result .fwformfield-value').css({
                        backgroundColor: '#a6d785'
                    })
                }
                else if (response.Message && response.Message === 'DECLINED') {
                    $confirmation.find('.Result .fwformfield-value').css({
                        backgroundColor: '#ff8a80'
                    })
                }
                $btnProcess.remove();
                $btnCancel.text('Close');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });

        const $btnCancel = FwConfirmation.addButton($confirmation, 'Cancel', true);
    }
}