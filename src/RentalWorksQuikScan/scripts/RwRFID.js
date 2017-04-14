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
RwRFID.setTslRfidPowerLevel = function () {
    if (typeof window.TslReader === 'object' && typeof window.TslReader.getPowerLevel === 'function') {
        var htmlSlider = [];
        htmlSlider.push('<div>');
        htmlSlider.push('  <div id="slider" style="margin:48px 32px 32px 32px;"></div>');
        htmlSlider.push('</div>');
        var $rfidmenu = FwConfirmation.renderConfirmation('RFID Antenna Power (dB)', htmlSlider.join('\n'));
        var $btnCancel = FwConfirmation.addButton($rfidmenu, 'Close', true);
        window.TslReader.getPowerLevel(
            function success(args) {
                var outputPower = args[1];
                var minPower = args[2];
                var maxPower = args[3];
                var rfidPowerLevelSlider = $rfidmenu.find('#slider').get(0);
                noUiSlider.create(rfidPowerLevelSlider, {
                    start: [outputPower],
                    range: {
                        'min': [minPower],
                        'max': [maxPower]
                    },
                    tooltips: [wNumb({ decimals: 0 })]
                });
                rfidPowerLevelSlider.noUiSlider.on('change', function () {
                    var rfidPowerLevel = parseFloat(rfidPowerLevelSlider.noUiSlider.get());
                    if (typeof window.TslReader === 'object') {
                        window.TslReader.setPowerLevel(rfidPowerLevel);
                    }
                });
                jQuery('.tslpanel').show();
            })
        ;
    }
};
//----------------------------------------------------------------------------------------------
