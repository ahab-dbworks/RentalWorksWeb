//---------------------------------------------------------------------------------
var FwEPS = FwEPS || {};
FwEPS.TestCertification = false;
FwEPS.sendAsyncTimeout = null;
FwEPS.transaction = {};
//---------------------------------------------------------------------------------
FwEPS.reset = function() {
    FwEPS.onProcessCharge  = null;
    FwEPS.btnReversalClick = null;
    FwEPS.btnPrintClick    = null;
    FwEPS.btnContinueClick = null;
    FwEPS.btnCompleteClick = null;
    FwEPS.btnCancelClick   = null;
    FwEPS.sendAsyncTimeout = null;
};
//---------------------------------------------------------------------------------
FwEPS.onEPSDeviceConnected = function(event) {
    try {
        if (FwEPS.cardReaderEnabled) {
            jQuery('#FwEPS-CardReaderStatus')
                .css('background-color', '#208E24')
                .html('Please swipe card...')
                .show()
            ;
        }
        if (event.hasOwnProperty('error')) {
            console.log('ERROR ' + event.error.description + ' [FwEPS.js:onEPSDeviceConnected]');
            FwFunc.showError('ERROR ' + event.error.description + ' [FwEPS.js:onEPSDeviceConnected]');
        }
        else {
            console.log(event.data.description + ' [FwEPS.js:onEPSDeviceConnected]');
        }
    }
    catch (ex) {
        console.log(ex + ' [FwEPS.js:onEPSDeviceConnected]')
        FwFunc.showError(ex + ' [FwEPS.js:onEPSDeviceConnected]');
    }
};
//---------------------------------------------------------------------------------
FwEPS.onEPSDeviceDisconnected = function(event) {
    try {
        console.log('[FwEPS.js:onEPSDeviceDisconnected]');
        jQuery('#FwEPS-CardReaderStatus')
            .css('background-color', '#ff0000')
            .html('LINEA PRO DISCONNECTED');
        //EPS.Device.getInstance().cardReader.disable();
    }
    catch (ex) {
        console.log(ex + ' [FwEPS.js:onEPSDeviceDisconnected]');
        FwFunc.showError(ex + ' [FwEPS.js:onEPSDeviceDisconnected]');
    }
};
//---------------------------------------------------------------------------------
FwEPS.enableCardReader = function (status) {
    try {
        window.DTDevices.registerListener('magneticCardEncryptedData', 'magneticCardEncryptedData_FwEPS', FwEPS.processMagneticCardEncryptedData);
        window.DTDevices.emsrSetEncryption('ALG_EH_IDTECH', 'KEY_EH_DUKPT_MASTER1');
    }
    catch (ex) {
        console.log(ex + ' [FwEPS.js:enableCardReader]');
        FwFunc.showError(ex + ' [FwEPS.js:enableCardReader]');
    }
}
//---------------------------------------------------------------------------------
FwEPS.processMagneticCardEncryptedData = function(accountnumber, expirationyear, expirationmonth, magneprintdata, 
                                                  accountid, accounttoken, acceptorid, terminalid, applicationname, applicationversion) {
    var referencenumber, ticketnumber, transactionamount, salestaxamount, request;
    try {
        referencenumber   = FwEPS.getNewTicketNumber();
        ticketnumber      = referencenumber;
        transactionamount = jQuery('#FwEPS-txtTransactionAmount').val();
        salestaxamount    = '0.00';
        for (var i = accountnumber.length - 1; i >= 0; i--) {
            if (i < accountnumber.length - 4) {
                accountnumber = accountnumber.substring(0, i) + '*' + accountnumber.substring(i+1);
            }
        }
        request = {
            applicationid:      applicationname,
            applicationname:    applicationname,
            applicationversion: applicationversion,
            accountid:          accountid,
            accounttoken:       accounttoken,
            acceptorid:         acceptorid,
            terminalid:         terminalid,
            magneprintdata:     magneprintdata,
            referencenumber:    referencenumber,
            ticketnumber:       ticketnumber,
            transactionamount:  transactionamount,
            salestaxamount:     salestaxamount,
            accountnumber:      accountnumber,
            expirationyear:     expirationyear,
            expirationmonth:    expirationmonth
        };
        GwServices.fwcreditcardservice.creditcardsale(request, FwEPS.expressResponse);
    } catch(ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
FwEPS.disableCardReader = function() {
    jQuery('#FwEPS-CardReaderStatus').hide();
    window.DTDevices.unregisterListener('magneticCardData', 'magneticCardData_FwEPS');
};
//---------------------------------------------------------------------------------
FwEPS.showCreditCardSaleScreen = function(totalDue) {
    var html, $screen;
    
    html = [];
    html.push('<div id="FwEPS-CreditCardSale">');
    html.push('  <div id="FwEPS-CreditCardSaleTitle">Credit Card Sale</div>');
    html.push('  <div id="FwEPS-CardReaderStatus" style="background-color:#000000;">Swipe Credit Card...</div>');
    html.push('  <table id="FwEPS-tblTotal">');
    html.push('    <tbody>');
    html.push('    <tr>');
    html.push('      <td><div id="FwEPS-divTotalDueLabel">Total Amount:</div></td>');
    html.push('      <td id="FwEPS-tdTotalDue"><div id="FwEPS-divTotalDue">$<input id="FwEPS-txtTransactionAmount" type="number" data-originalamount="' + totalDue.toFixed(2) + '" value="' + totalDue.toFixed(2) + '" /></div></td>');
    html.push('    </tr>');
    html.push('    <tr id="FwEPS-trRemainingBalance" style="display:none;">');
    html.push('      <td><div id="FwEPS-divRemainingBalanceLabel">Remaining Balance:</div></td>');
    html.push('      <td id="FwEPS-tdRemainingBalance"><div id="FwEPS-divRemainingBalance">$<span id="FwEPS-spanRemainingBalance">' + numberWithCommas(totalDue.toFixed(2)) + '</span></div></td>');
    html.push('    </tr>');
    html.push('    </tbody>');
    html.push('  </table>');
    html.push('  <div id="FwEPS-Response" style="display:none;">');
    html.push('    <table id="FwEPS-tblResponse">');
    html.push('      <tbody>');
    html.push('        <tr id="FwEPS-trMethod">');
    html.push('          <td><div id="FwEPS-lblMethod">Method:</div></td>');
    html.push('          <td><div id="FwEPS-txtMethod"></div></td>');
    html.push('        </tr>');
    html.push('        <tr id="FwEPS-trStatus">');
    html.push('          <td><div id="FwEPS-lblStatus">Status:</div></td>');
    html.push('          <td id="FwEPS-tdStatusValue"><div id="FwEPS-txtStatus"></div></td>');
    html.push('        </tr>');
    html.push('        <tr id="FwEPS-trStatusDescription">');
    html.push('          <td><div id="FwEPS-lblStatusDescription">Description:</div></td>');
    html.push('          <td><div id="FwEPS-txtStatusDescription"></div></td>');
    html.push('        </tr>');
    html.push('        <tr id="FwEPS-trApprovedAmount" style="display:none;">');
    html.push('          <td><div id="FwEPS-lblApprovedAmount">Approved Amount:</div></td>');
    html.push('          <td id="FwEPS-tdApprovedAmountValue">$<span id="FwEPS-spanApprovedAmount"></span></td>');
    html.push('        </tr>');
    html.push('        <tr id="FwEPS-trApprovalCode" style="display:none;">');
    html.push('          <td><div id="FwEPS-lblApprovalCode">Approval No:</div></td>');
    html.push('          <td><div id="FwEPS-txtApprovalCode"></div></td>');
    html.push('        </tr>');
    html.push('        <tr id="FwEPS-trTransactionID">');
    html.push('          <td><div id="FwEPS-lblTransactionID" data-referenceno="" data-ticketno="">Transaction ID:</div></td>');
    html.push('          <td><div id="FwEPS-txtTransactionID"></div></td>');
    html.push('        </tr>');
    html.push('        <tr id="FwEPS-trReferenceNumber">');
    html.push('          <td><div id="FwEPS-lblReferenceNumber">Reference No:</div></td>');
    html.push('          <td><div id="FwEPS-txtReferenceNumber"></div></td>');
    html.push('        </tr>');
    html.push('        <tr id="FwEPS-trTicketNumber">');
    html.push('          <td><div id="FwEPS-lblTicketNumber">Ticket No:</div></td>');
    html.push('          <td><div id="FwEPS-txtTicketNumber"></div></td>');
    html.push('        </tr>');
    html.push('      </tbody>');
    html.push('    </table>');
    html.push('  </div>');
    html.push('  <div id="FwEPS-buttonPanel">' + 
                     '<span class="button default"  id="FwEPS-btnReversal" style="display:none;">Reversal</span>' + 
                     '<span class="button default"  id="FwEPS-btnPrint" style="display:none;">Print</span>' + 
                     '<span class="button default"  id="FwEPS-btnContinue" style="display:none;">Continue</span>' + 
                     '<span class="button default"  id="FwEPS-btnComplete" style="display:none;">Complete</span>' + 
                     '<span class="button default"  id="FwEPS-btnCancel">Cancel</span>' + 
                '</div>');
    html.push('</div>');
    $screen = jQuery(html.join('\n'));
    $screen.on('focus', '#FwEPS-txtTransactionAmount', function(event) {
        var $this;
        try {
            $this = jQuery(this);
            $this.val('');
        } catch(ex) {
            FwFunc.showError(ex + ' [FwEPS.js:FwEPS-txtTransactionAmount.focus]');
        }
    });
    $screen.on('blur', '#FwEPS-txtTransactionAmount', function(event) {
        var $this, value;
        try {
            $this = jQuery(this);
            value = parseFloat($this.val());
            if (($this.val() === '') || (isNaN(value))) {
                $this.val($this.attr('data-originalamount'));
            }
        } catch(ex) {
            FwFunc.showError(ex + ' [FwEPS.js:FwEPS-txtTransactionAmount.focus]');
        }
    });
    $screen.on('click', '#FwEPS-btnReversal', function(event) {
        var transactionid, referencenumber, ticketnumber, transactionamount, accountnumber;
        try {
            //transactionid   = FwEPS.$txtTransactionID.html();
            //referencenumber = FwEPS.$txtTransactionID.attr('data-referenceno');
            //ticketnumber    = FwEPS.$txtTransactionID.attr('data-ticketno');
            //transactionamount = parseFloat(FwEPS.$spanApprovedAmount.html());
            //accountnumber     = ''; //!!!!!NEED TO SAVE ACCOUNT NUMBER SOMEWHERE AFTER CREDITCARDSALE!!!!!!!
            
            var applicationname    = FwEPS.$txtTransactionID.attr('data-applicationname');
            var applicationversion = FwEPS.$txtTransactionID.attr('data-applicationversion');
            var accountid          = FwEPS.$txtTransactionID.attr('data-accountid');
            var accounttoken       = FwEPS.$txtTransactionID.attr('data-accounttoken');
            var acceptorid         = FwEPS.$txtTransactionID.attr('data-acceptorid');
            var terminalid         = FwEPS.$txtTransactionID.attr('data-terminalid');
            var referencenumber    = FwEPS.$txtTransactionID.attr('data-referencenumber');
            var ticketnumber       = FwEPS.$txtTransactionID.attr('data-ticketnumber');
            var transactionamount  = FwEPS.$txtTransactionID.attr('data-transactionamount');
            var salestaxamount     = FwEPS.$txtTransactionID.attr('data-salestaxamount');
            var transactionid      = FwEPS.$txtTransactionID.attr('data-transactionid');
            var accountnumber      = FwEPS.$txtTransactionID.attr('data-accountnumber');
            FwEPS.creditCardFullReversal(applicationname, applicationversion, accountid, accounttoken, acceptorid, terminalid, 
                                         referencenumber, ticketnumber, transactionamount, salestaxamount, transactionid, accountnumber);
        } catch(ex) {
            FwFunc.showError(ex + ' [FwEPS.js:FwEPS-btnReversal.click]');
        }
    });
    $screen.on('click', '#FwEPS-btnPrint', function(event) {
        try {
            if (typeof FwEPS.btnPrintClick === 'function') {
                FwEPS.btnPrintClick();
            }
        } catch(ex) {
            FwFunc.showError(ex + ' [FwEPS.js:FwEPS-btnPrint.click]');
        }
    });
    $screen.on('click', '#FwEPS-btnContinue', function(event) {
        try {
            FwEPS.destroyCreditCardSaleScreen();
            if (typeof FwEPS.btnContinueClick === 'function') {
                FwEPS.btnContinueClick();
            }
        } catch(ex) {
            FwFunc.showError(ex + ' [FwEPS.js:FwEPS-btnContinue.click]');
        }
    });
    $screen.on('click', '#FwEPS-btnComplete', function(event) {
        try {
            FwEPS.destroyCreditCardSaleScreen();
            if (typeof FwEPS.btnCompleteClick === 'function') {
                FwEPS.btnCompleteClick();
            }
        } catch(ex) {
            FwFunc.showError(ex + ' [FwEPS.js:FwEPS-btnComplete.click]');
        }
    });
    $screen.on('click', '#FwEPS-btnCancel', function(event) {
        try {
            FwEPS.destroyCreditCardSaleScreen();
            if (typeof FwEPS.btnCancelClick === 'function') {
                FwEPS.btnCancelClick();
            }
        } catch(ex) {
            FwFunc.showError(ex + ' [FwEPS.js:FwEPS-btnCancel.click]');
        }
    });
    jQuery('#FwEPS-CreditCardSale').data('totaldue', totalDue);
    FwEPS.$btnComplete          = $screen.find('#FwEPS-btnComplete');
    FwEPS.$btnReversal          = $screen.find('#FwEPS-btnReversal');
    FwEPS.$btnPrint             = $screen.find('#FwEPS-btnPrint');
    FwEPS.$btnContinue          = $screen.find('#FwEPS-btnContinue');
    FwEPS.$btnCancel            = $screen.find('#FwEPS-btnCancel');
    FwEPS.$spanRemainingBalance = $screen.find('#FwEPS-spanRemainingBalance');
    FwEPS.$trRemainingBalance   = $screen.find('#FwEPS-trRemainingBalance');
    FwEPS.$txtTransactionAmount = $screen.find('#FwEPS-txtTransactionAmount');
    FwEPS.$tdStatusValue        = $screen.find('#FwEPS-tdStatusValue');
    FwEPS.$txtStatus            = $screen.find('#FwEPS-txtStatus');
    FwEPS.$txtStatusDescription = $screen.find('#FwEPS-txtStatusDescription');
    FwEPS.$trTransactionID      = $screen.find('#FwEPS-trTransactionID');
    FwEPS.$txtTransactionID     = $screen.find('#FwEPS-txtTransactionID');
    FwEPS.$trApprovedAmount     = $screen.find('#FwEPS-trApprovedAmount');
    FwEPS.$spanApprovedAmount   = $screen.find('#FwEPS-spanApprovedAmount');
    FwEPS.$Response             = $screen.find('#FwEPS-Response');
    FwEPS.$txtMethod            = $screen.find('#FwEPS-txtMethod');
    FwEPS.$txtReferenceNumber   = $screen.find('#FwEPS-txtReferenceNumber');
    FwEPS.$txtTicketNumber      = $screen.find('#FwEPS-txtTicketNumber');
    FwEPS.creditCardSalePopup = FwPopup.attach($screen);
    document.body.scrollTop = 0;
    FwPopup.show(FwEPS.creditCardSalePopup);
    
    
    FwEPS.enableCardReader();
    //window.DTDevices.registerListener('magneticCardEncryptedData', 'magneticCardEncryptedData_FwEPS', $screen.processMagneticCardEncryptedData);
    //window.DTDevices.emsrSetEncryption('ALG_EH_IDTECH', 'KEY_EH_DUKPT_MASTER1');
};
//---------------------------------------------------------------------------------
FwEPS.destroyCreditCardSaleScreen = function() {
    FwEPS.disableCardReader();
    //window.DTDevices.unregisterListener('magneticCardData', 'magneticCardData_FwEPS');
    if (typeof FwEPS.creditCardSalePopup !== 'undefined') {
        FwPopup.destroy(FwEPS.creditCardSalePopup);
    }
};
//---------------------------------------------------------------------------------
FwEPS.expressResponse = function(response) {
    var status, statusDescription, statusTextColor, statusBackgroundColor;
    try {
        //FwEPS.acceptorid = response.request.acceptorid;
        FwEPS.$btnComplete.hide();
        FwEPS.$btnReversal.hide();
        FwEPS.$btnPrint.hide();
        FwEPS.$btnContinue.hide();
        FwEPS.$btnCancel.hide();
        switch(response.Express.ExpressResponseType) {
            case 'CreditCardSale':
                FwEPS.$txtMethod.html('Sale');
                break;
            case 'CreditCardFullReversal':
                FwEPS.$txtMethod.html('Reversal');
                break;
        }
        if (response.Express.SystemReversal != null) {
            switch(response.Express.SystemReversal.ExpressResponseCode) {
                case '0': //Approved
                    FwFunc.showError('A communication error occured. Please try the transaction again. (System Reversal was successful)');
                    break;
                default:
                    FwFunc.showError('A communication error occured. Unable to determine if the transaction completed successfully. (System Reversal was not successful)');
                    break;
            }
        }
        switch(response.Express.ExpressResponseCode) {
            case '0': //Approved
            case '5': //PartialApproval
                switch(response.Express.ExpressResponseType) {
                    case 'CreditCardSale':
                        FwEPS.creditCardSaleResponse(response);
                        break;
                    case 'CreditCardFullReversal':
                    case 'CreditCardSystemReversal':
                        FwEPS.creditCardReversalResponse(response);
                        break;
                }
                break;
            default:
                FwEPS.onExpressErrorCode(response);
                break;
        }
    } catch (ex) {
        FwFunc.showError(ex + ' [FwEPS.js:expressResponse]');
    }
};
//---------------------------------------------------------------------------------
FwEPS.onExpressErrorCode = function(response) {
    switch(response.Express.ExpressResponseCode) {
        case '20': //Decline
            FwEPS.$btnComplete.show();
            statusDescription = 'Card issuer has declined the transaction. Return the card and instruct the cardholder to call the card issuer for more information on the status of the account.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '21': //ExpiredCard
            FwEPS.$btnComplete.show();
            statusDescription = 'Card is expired or expiration date is invalid.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '22': //DuplicateApproved
            FwEPS.$btnComplete.show();
            statusDescription = 'Declined as a duplicate. Same card, same amount, within the same batch was previously approved.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '23': //Duplicate
            FwEPS.$btnComplete.show();
            statusDescription = 'Declined as a duplicate. Same card, same amount, within the same batch was previously approved.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '24': //PickUpCard:
            FwEPS.$btnComplete.show();
            statusDescription = 'Issuing bank has declined and wants to recover the card. Inform the customer that you have been instructed to keep the card, and ask for an alternative form of payment. If you feel uncomfortable, simply return the card to the cardholder.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '25': //ReferralCallIssuer:
            FwEPS.$btnComplete.show();
            statusDescription = 'Card issuer needs more information before approving the sale. Contact the authorization center and follow whatever instructions you are given.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '30': //BalanceNotAvailable:
            FwEPS.$btnComplete.show();
            statusDescription = 'Balance is not available.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '90': //EPS.Express.ResponseCode.NotDefined:
            FwEPS.$btnComplete.show();
            statusDescription = 'Undefined.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '101': //InvalidData
            FwEPS.$btnComplete.show();
            statusDescription = 'Invalid data has been submitted.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '102': //InvalidAccount
            FwEPS.$btnComplete.show();
            statusDescription = 'Possible profile configuration issue or incorrect AcceptorID sent. Possible invalid AccountID, AccountToken, or TerminalID sent.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '103': //InvalidRequest
            FwEPS.$btnComplete.show();
            statusDescription = 'Invalid Request.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '104': //AuthorizationFailed
            FwEPS.$btnComplete.show();
            statusDescription = 'Auth failed.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '105': //NotAllowed
            FwEPS.$btnComplete.show();
            statusDescription = 'Card number or card type submitted is invalid. Merchant may not be configured for submitted card type.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '120': //OutOfBalance
            FwEPS.$btnComplete.show();
            statusDescription = 'Out of balance (Merchant-Initiated batching).';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        case '1001': //CommunicationError
            // since this seems to be handled by the shell, this isn't fully implememented
            FwEPS.$btnComplete.show();
            FwEPS.$btnReversal.show();
            statusDescription = 'Communication error. Reversal should be submitted.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            break;
        case '1002': //HostError
            // since this seems to be handled by the shell, this isn't fully implememented
            FwEPS.$btnComplete.show();
            FwEPS.$btnReversal.show();
            statusDescription = 'Host error. Reversal should be submitted.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            break;
        case '10000': //HostedPaymentsCancelled
            FwEPS.$btnComplete.show();
            statusDescription = 'Hosted Payments Cancelled.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
        default:
            FwEPS.$btnComplete.show();
            statusDescription = 'Unknown response code.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
    }
    FwEPS.$tdStatusValue.css({
        'background-color': statusBackgroundColor
    });
    FwEPS.$txtStatus
        .css({
            'color': statusTextColor
        })
        .html(response.Express.ExpressResponseMessage);
    FwEPS.$txtStatusDescription.html(statusDescription);
    FwEPS.$trApprovedAmount.hide();
    if (typeof response.Express.Transaction.TransactionID !== 'undefined') {
        FwEPS.$txtTransactionID.html(response.Express.Transaction.TransactionID);
    }
    FwEPS.$Response.show();
    //FwPopup.position(FwEPS.creditCardSalePopup);
};
//---------------------------------------------------------------------------------
//FwEPS.creditCardSale = function(transactionID, referenceNumber, ticketNumber, transactionAmount, accountNumber) {
//    var request;
//    request = {
//        transactionID:     transactionID,
//        referenceNumber:   referenceNumber,
//        ticketNumber:      ticketNumber,
//        transactionAmount: transactionAmount,
//        accountNumber:     accountNumber
//    };
//    GwServices.fwcreditcardservice.creditcardfullreversal(request, FwEPS.expressResponse);
//};
//---------------------------------------------------------------------------------
FwEPS.creditCardSaleResponse = function(response) {
    var totalDue, remainingBalance, approvedAmount, reversalID, ticketNumber;

    totalDue         = parseFloat(FwEPS.$txtTransactionAmount.val());
    remainingBalance = parseFloat(FwEPS.$spanRemainingBalance.html());
    approvedAmount   = (typeof response.Express.Transaction.ApprovedAmount === 'string') ? parseFloat(response.Express.Transaction.ApprovedAmount) : 0.00;
    remainingBalance = totalDue - approvedAmount;
    reversalID       = '';
    FwEPS.$btnComplete.hide();
    FwEPS.$btnReversal.hide();
    FwEPS.$btnPrint.hide();
    FwEPS.$btnContinue.hide();
    FwEPS.$btnCancel.hide();
    FwEPS.$spanRemainingBalance.html(remainingBalance.toFixed(2));
    FwEPS.$trRemainingBalance.show();
    status         = response.Express.ExpressResponseMessage;
    ticketNumber   = response.Express.Transaction.ReferenceNumber;
    switch(response.Express.ExpressResponseCode) {
        case "0": // APPPROVED
            // we're done
            FwEPS.$btnReversal.show();
            FwEPS.$btnPrint.show();
            FwEPS.$btnComplete.show();
            statusDescription = 'Transaction was approved.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#208E24';
            jQuery('#FwEPS-CardReaderStatus').hide();
            FwEPS.disableCardReader();
            if (typeof FwEPS.onProcessCharge === 'function') {
                FwEPS.onProcessCharge('CREDIT', 
                    response.request.acceptorid, 
                    response.Express.Transaction.TransactionID, 
                    reversalID, 
                    response.Express.Transaction.ApprovalNumber, 
                    approvedAmount, 
                    response.Express.Card.CardLogo, 
                    response.request.accountnumber, 
                    response.Express.ExpressResponseMessage, 
                    response.Express.Transaction.ReferenceNumber, 
                    response.request.ticketnumber);
            }
            break;
        case "5": // PARTIAL APPROVED
            // transaction was partially approved.  We need to go back to the prompt screen to take cash or another credit card
            FwEPS.$btnReversal.show();
            FwEPS.$btnPrint.show();
            FwEPS.$btnContinue.show();
            statusDescription = 'Card issuer has partially approved the transaction. You need to collect the remaining balance using another payment method.';
            statusTextColor       = '#000000';
            statusBackgroundColor = '#FFFF00';
            FwEPS.disableCardReader();
            if (typeof FwEPS.onProcessCharge === 'function') {
                FwEPS.onProcessCharge('CREDIT', 
                    response.request.acceptorId, 
                    response.Express.Transaction.TransactionID, 
                    reversalID, 
                    response.Express.Transaction.ApprovalNumber, 
                    approvedAmount, 
                    response.Express.Card.CardLogo, 
                    response.request.accountnumber, 
                    response.Express.ExpressResponseMessage, 
                    response.Express.Transaction.ReferenceNumber, 
                    response.request.ticketNumber);
            }
            break;
        default:
            FwEPS.$btnCancel.show();
            statusDescription     = response.Express.ExpressResponseMessage;
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
    }
    FwEPS.$tdStatusValue.css({
        'background-color': statusBackgroundColor
    });
    FwEPS.$txtStatus
        .css({
            'color': statusTextColor
        })
        .html(status);
    FwEPS.$txtStatusDescription.html(statusDescription);
    FwEPS.$trApprovedAmount.hide();
    if (typeof response.Express.Transaction.ApprovedAmount !== 'undefined') {
        FwEPS.$spanApprovedAmount.html(approvedAmount.toFixed(2));
        FwEPS.$trApprovedAmount.show();
    }
    if (typeof response.Express.Transaction.TransactionID !== 'undefined') {
        FwEPS.$txtTransactionID.html(response.Express.Transaction.TransactionID);
    }
    // ticket no is the same as reference no, because it's not returned by web service
    
    FwEPS.$txtTransactionID.attr('data-applicationname',    response.request.applicationname);
    FwEPS.$txtTransactionID.attr('data-applicationversion', response.request.applicationversion);
    FwEPS.$txtTransactionID.attr('data-accountid',          response.request.accountid);
    FwEPS.$txtTransactionID.attr('data-accounttoken',       response.request.accounttoken);
    FwEPS.$txtTransactionID.attr('data-acceptorid',         response.request.acceptorid);
    FwEPS.$txtTransactionID.attr('data-terminalid',         response.request.terminalid);
    FwEPS.$txtTransactionID.attr('data-referencenumber',    response.request.referencenumber);
    FwEPS.$txtTransactionID.attr('data-ticketnumber',       response.request.ticketnumber);
    FwEPS.$txtTransactionID.attr('data-transactionamount',  response.request.transactionamount);
    FwEPS.$txtTransactionID.attr('data-salestaxamount',     response.request.salestaxamount);
    FwEPS.$txtTransactionID.attr('data-transactionid',      response.Express.Transaction.TransactionID);
    FwEPS.$txtTransactionID.attr('data-accountnumber',      response.request.accountnumber);
    
    //FwEPS.$txtTransactionID.attr('data-referenceno', response.Express.Transaction.ReferenceNumber);
    //FwEPS.$txtReferenceNumber.html(response.Express.Transaction.ReferenceNumber);
    FwEPS.$txtReferenceNumber.html(response.request.referencenumber);
    //FwEPS.$txtTransactionID.attr('data-ticketno',    response.Express.Transaction.ReferenceNumber);
    //FwEPS.$txtTicketNumber.html(response.Express.Transaction.ReferenceNumber);
    FwEPS.$txtTicketNumber.html(response.request.ticketnumber);
    FwEPS.$Response.show();
    //FwPopup.position(FwEPS.creditCardSalePopup);
};
//---------------------------------------------------------------------------------
FwEPS.creditCardFullReversal = function(applicationname, applicationversion, accountid, accounttoken, acceptorid, terminalid, referencenumber, ticketnumber, 
    transactionamount, salestaxamount, transactionid, accountnumber) {
    var request;
    request = {
        applicationname:     applicationname,
        applicationversion:  applicationversion,
        accountid:           accountid,
        accounttoken:        accounttoken,
        acceptorid:          acceptorid,
        terminalid:          terminalid,
        referencenumber:     referencenumber,
        ticketnumber:        ticketnumber,
        transactionamount:   transactionamount,
        salestaxamount:      salestaxamount,
        transactionid:       transactionid,
        accountnumber:       accountnumber
    };
    GwServices.fwcreditcardservice.creditcardfullreversal(request, FwEPS.expressResponse);
};
//---------------------------------------------------------------------------------
FwEPS.creditCardReversalResponse = function(response) {
    var remainingbalance, transactionamount, reversalAmount, reversaltransid;
    FwEPS.$btnComplete.hide();
    FwEPS.$btnReversal.hide();
    FwEPS.$btnPrint.hide();
    FwEPS.$btnContinue.hide();
    FwEPS.$btnCancel.hide();
    status             = response.Express.ExpressResponseMessage;
    transactionamount  = parseFloat(response.request.transactionamount);
    reversalAmount     = 0 - transactionamount;
    reversaltransid    = response.request.transactionid;
    remainingbalance   = parseFloat(FwEPS.$spanRemainingBalance.html());
    remainingbalance  += transactionamount;
    FwEPS.$spanRemainingBalance.html(remainingbalance.toFixed(2));
    switch(response.Express.ExpressResponseCode) {
        case '0': //Approved
            FwEPS.$btnPrint.show();
            FwEPS.$btnComplete.show();
            statusDescription = 'Transaction was approved.';
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#208E24';
            jQuery('#FwEPS-CardReaderStatus').hide();
            FwEPS.disableCardReader();
            if (typeof FwEPS.onProcessCharge === 'function') {
                FwEPS.onProcessCharge('REVERSAL', 
                                      response.request.acceptorid, 
                                      response.request.transactionid, 
                                      response.Express.Transaction.TransactionID, 
                                      response.Express.Transaction.ApprovalNumber, 
                                      reversalAmount, 
                                      response.Express.Card.CardLogo, 
                                      response.request.accountnumber, 
                                      response.Express.ExpressResponseMessage, 
                                      response.Express.Transaction.ReferenceNumber, 
                                      response.Express.Transaction.ReferenceNumber);
            }
            break;
        case '5': //PartialApproval
            // transaction was partially approved.  We need to go back to the prompt screen to take cash or another credit card
            FwEPS.$btnReversal.show();
            FwEPS.$btnPrint.show();
            FwEPS.$btnContinue.show();
            statusDescription = 'Card issuer has partially approved the transaction. You need to collect the remaining balance using another payment method.';
            statusTextColor       = '#000000';
            statusBackgroundColor = '#FFFF00';
            FwEPS.disableCardReader();
            if (typeof FwEPS.onProcessCharge === 'function') {
                FwEPS.onProcessCharge('REVERSAL', 
                                      response.request.acceptorid, 
                                      response.request.transactionid, 
                                      response.Express.Transaction.TransactionID, 
                                      response.Express.Transaction.ApprovalNumber,
                                      reversalAmount, 
                                      response.Express.Card.CardLogo, 
                                      response.request.accountnumber, 
                                      response.Express.ExpressResponseMessage, 
                                      response.Express.Transaction.ReferenceNumber, 
                                      response.Express.Transaction.ReferenceNumber);
            }
            break;
        default:
            FwEPS.$btnCancel.show();
            statusDescription     = response.Express.ExpressResponseMessage;
            statusTextColor       = '#FFFFFF';
            statusBackgroundColor = '#FF0000';
            FwEPS.enableCardReader();
            break;
    }
    FwEPS.$tdStatusValue.css({
        'background-color': statusBackgroundColor
    });
    FwEPS.$txtStatus
        .css({
            'color': statusTextColor
        })
        .html(status);
    FwEPS.$txtStatusDescription.html(statusDescription);
    FwEPS.$trApprovedAmount.hide();
    if (typeof response.Express.Transaction.TransactionID !== 'undefined') {
        FwEPS.$txtTransactionID.html(response.Express.Transaction.TransactionID);
    }
    // ticket no is the same as reference no, because it's not returned by web service
    FwEPS.$txtTransactionID.attr('data-referenceno', response.Express.Transaction.ReferenceNumber);
    FwEPS.$txtReferenceNumber.html(response.Express.Transaction.ReferenceNumber);
    FwEPS.$txtTransactionID.attr('data-ticketno',    response.Express.Transaction.ReferenceNumber);
    FwEPS.$txtTicketNumber.html(response.Express.Transaction.ReferenceNumber);
    FwEPS.$Response.show();
    //FwPopup.position(FwEPS.creditCardSalePopup);
};
//---------------------------------------------------------------------------------
FwEPS.onEPSDeviceCardReaderError= function(event) {
    try {
        FwFunc.showError(event.error.description + ' [FwEPS.js:onEPSDeviceCardReaderError]');
        FwEPS.enableCardReader('Please reswipe card...');
    }
    catch (ex) {
        FwFunc.showError(ex + ' [FwEPS.js:onEPSDeviceCardReaderError]');
    }
};
//---------------------------------------------------------------------------------
FwEPS.getGuid = function() {
    function _p8(s) {
        var p = (Math.random().toString(16) + "000000000").substr(2,8);
        return s ? "-" + p.substr(0,4) + "-" + p.substr(4,4) : p ;
    }
    return _p8() + _p8(true) + _p8(true) + _p8();
};
//---------------------------------------------------------------------------------
FwEPS.getNewReferenceNumber = function() {
    var referenceNumber;
    referenceNumber = (Math.random().toString(10)).substr(2,16);
    return referenceNumber;
};
//---------------------------------------------------------------------------------
FwEPS.getNewTicketNumber = function() {
    var ticketNumber;
    ticketNumber = (Math.random().toString(10)).substr(2,6);
    return ticketNumber;
};
//---------------------------------------------------------------------------------
