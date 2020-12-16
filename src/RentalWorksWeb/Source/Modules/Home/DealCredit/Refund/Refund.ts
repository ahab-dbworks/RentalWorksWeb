﻿//----------------------------------------------------------------------------------------------
class Refund {
    static showRefundPopup($browse: JQuery, request: RequestRefund) {
        let html =
`<div style="min-width:350px;max-width:450px;font-size:.8em;">
    <div class="flexrow">
        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer No" data-datafield="CustomerNo"  data-enabled="false"></div>
        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Name" data-datafield="Customer" data-enabled="false"></div>
    </div>
    <div class="flexrow">
        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="DealNo" data-datafield="DealNo"  data-enabled="false"></div>
        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal Name" data-datafield="Deal" data-enabled="false"></div>
    </div>
    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PIN Pad" style="max-width:700px">
        <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PIN Pad Code" data-datafield="PINPad_Code" data-displayfield="Code" data-validationname="CreditCardPinPadValidation" data-required="true"></div>
        </div>
        <div class="flexrow">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="PINPad_Description" data-enabled="false"></div>
        </div>
        <div class="flexrow">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="PINPad_Type" data-enabled="false"></div>
        </div>
    </div>
    <div class="flexrow">
        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Currency" data-datafield="CurrencyId" data-displayfield="CurrencyCode" data-validationname="CurrencyValidation" data-enabled="true" style="flex:1 0 150px;" data-required="true"></div>
    </div>  
    <div class="flexrow">
        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deposit Total" data-datafield="DepositTotal" data-required="false" data-enabled="false" style="flex:1 0 150px;"></div>
    </div>  
    <div class="flexrow">
        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount Applied" data-datafield="AmountApplied" data-required="false" data-enabled="false" style="flex:1 0 150px;"></div>
    </div>  
    <div class="flexrow">
        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Remaining Amount" data-datafield="RemainingAmount" data-required="false" data-enabled="false" style="flex:1 0 150px;"></div>
    </div>  
    <div class="flexrow">
        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Refund Amount" data-datafield="RefundAmount" data-required="true" style="flex:1 0 150px;"></div>
    </div>  
</div>`;
        const $refund = jQuery(html);
        const $confirmation = FwConfirmation.renderConfirmation('Refund', '');
        FwConfirmation.addJqueryControl($confirmation, $refund);
        const $btnProcess = FwConfirmation.addButton($confirmation, 'Process Credit Card', false);
        const location = JSON.parse(sessionStorage.getItem('location'));
        const today = FwFunc.getDate();

        FwFormField.setValueByDataField($confirmation, "Customer", request.customer);
        FwFormField.setValueByDataField($confirmation, "Deal", request.deal);
        FwFormField.setValueByDataField($confirmation, "DepositTotal", request.amount);
        FwFormField.setValueByDataField($confirmation, "AmountApplied", request.applied);
        FwFormField.setValueByDataField($confirmation, "RemainingAmount", request.remaining);

        FwFormField.setValueByDataField($confirmation, "ReceiptDate", today);
        FwFormField.setValueByDataField($confirmation, 'CurrencyId', location.defaultcurrencyid, location.defaultcurrencycode);
        const $paymentTypeId = FwFormField.getDataField($confirmation, 'PaymentTypeId');
        $paymentTypeId.data('beforevalidate', ($validationbrowse: JQuery, $object: JQuery, request: any, datafield: string, $tr: JQuery) => {
            request.filterfields = {
                'PaymentTypeType': 'DEPLETING DEPOSIT'
            }
        })

        $btnProcess.on('click', async (e: JQuery.ClickEvent) => {
            try {
                const requestrefund = new FwAjaxRequest();
                requestrefund.httpMethod = 'POST';
                requestrefund.setWebApiUrl('/api/v1/receipt/refund');
                requestrefund.data = {
                    ReceiptId: request.receiptId,
                    LocationId: location.locationid,
                    CurrencyId: FwFormField.getValueByDataField($refund, 'CurrencyId'),
                    DealId: FwFormField.getValueByDataField($refund, 'DealId'),
                    ReceiptDate: FwFormField.getValueByDataField($refund, 'ReceiptDate'),
                    PaymentTypeId: FwFormField.getValueByDataField($refund, 'PaymentTypeId'),
                    CheckNumber: FwFormField.getValueByDataField($refund, 'CheckNumber'),
                    PaymentAmount: FwFormField.getValueByDataField($refund, 'PaymentAmount'),
                    PaymentMemo: FwFormField.getValueByDataField($refund, 'PaymentMemo')
                };
                requestrefund.$elementToBlock = jQuery('body');
                var response = await FwAjax.callWebApi<any, any>(requestrefund);
                if (requestrefund.xmlHttpRequest.status === 200) {
                    if (response.Status === 'ERROR') {
                        FwFunc.showWebApiError(requestrefund.xmlHttpRequest.status, requestrefund.xmlHttpRequest.statusText, requestrefund.xmlHttpRequest.responseText, requestrefund.url);
                        return;
                    }
                    console.log(response);
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.databind($browse);
                } else {
                    FwFunc.showWebApiError(requestrefund.xmlHttpRequest.status, requestrefund.xmlHttpRequest.statusText, requestrefund.xmlHttpRequest.responseText, requestrefund.url);
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