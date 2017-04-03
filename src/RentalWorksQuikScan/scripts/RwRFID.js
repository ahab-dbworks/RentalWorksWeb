var RwRFID = {
    isConnected: false
};
//----------------------------------------------------------------------------------------------
RwRFID.init = function() {
    var applicationOptions;
    applicationOptions = application.getApplicationOptions();
    if ((typeof window.TslReader === 'object') && (typeof applicationOptions.rfid !== 'undefined') && (applicationOptions.rfid.enabled)) {
        window.TslReader.isConnected(function isConnectedSuccess(result) {
            RwRFID.isConnected = result[1];
        });
    }
};
//----------------------------------------------------------------------------------------------
RwRFID.registerEvents = function(callbackfunction) {
    if (RwRFID.isConnected) {
        if (typeof window.TslReader != 'undefined') {
            window.TslReader.registerListener('epcsReceived', 'epcsReceived_rwrfidjs', function(epcs) {
                callbackfunction(epcs);
                if (jQuery('.tagCountPopup').length) {
                    FwConfirmation.destroyConfirmation(jQuery('.tagCountPopup'));
                }
            });
            window.TslReader.registerListener('epcReceived', 'epcReceived_rwrfidjs', function(epc, count) {
                if (jQuery('.tagCountPopup').length) {
                    jQuery('.tagCount').html(count);
                } else {
                    var $confirmation;
                    $confirmation = FwConfirmation.renderConfirmation('Tags Scanned', '<div class="tagCount" style="color:black;font-weight:bold;text-align:center;font-size:100px;"></div>');
                    $confirmation.addClass('tagCountPopup');
                    $confirmation.find('.tagCount').html(count);
                }
            });
        }

        if (typeof window.ZebraRFIDScanner !== 'undefined') {
            window.ZebraRFIDScanner.registerListener('epcsReceived', 'epcsReceived_rwrfidjs', function(epcs) {
                callbackfunction(epcs);
                if (jQuery('.tagCountPopup').length) {
                    FwConfirmation.destroyConfirmation(jQuery('.tagCountPopup'));
                }
            });
            window.ZebraRFIDScanner.registerListener('epcReceived', 'epcReceived_rwrfidjs', function(epc, count) {
                if (jQuery('.tagCountPopup').length) {
                    jQuery('.tagCount').html(count);
                } else {
                    var $confirmation;
                    $confirmation = FwConfirmation.renderConfirmation('Tags Scanned', '<div class="tagCount" style="color:black;font-weight:bold;text-align:center;font-size:100px;"></div>');
                    $confirmation.addClass('tagCountPopup');
                    $confirmation.find('.tagCount').html(count);
                }
            });
        }
    }
};
//----------------------------------------------------------------------------------------------
RwRFID.unregisterEvents = function() {
    if (RwRFID.isConnected) {
        window.TslReader.unregisterListener('epcsReceived', 'epcsReceived_rwrfidjs');
        window.TslReader.unregisterListener('epcReceived',  'epcReceived_rwrfidjs');
    }
};
//----------------------------------------------------------------------------------------------
