var RwRFID = {
    isConnected: false,
    isPerformingSoftwareSinglePress: false
};
//----------------------------------------------------------------------------------------------
RwRFID.init = function() {
    var applicationOptions;
    applicationOptions = program.getApplicationOptions();
    if ((typeof window.TslReader === 'object') && (typeof applicationOptions.rfid !== 'undefined') && (applicationOptions.rfid.enabled)) {
        window.TslReader.isConnected(function isConnectedSuccess(result) {
            RwRFID.isConnected = result[1];
            if (!RwRFID.isConnected) {
                window.TslReader.connectDevice();
            }
        });
    }
};
//----------------------------------------------------------------------------------------------
RwRFID.registerEvents = function(callbackfunction) {
    var me = this;
    if (typeof window.TslReader !== 'undefined') {
        //window.TslReader.registerListener('deviceConnected', 'deviceConnected_rwrfidjs', function() {
        //    //RwRFID.isConnected = true;
        //    FwMobileMasterController.generateDeviceStatusIcons();
        //    //FwNotification.renderNotification('SUCCESS', 'RFID Reader Connected');
        //});
        //window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_rwrfidjs', function() {
        //    if ((program.browserVersionMajor > 2018) ||
        //        (program.browserVersionMajor === 2018 && program.browserVersionMinor > 1) ||
        //        (program.browserVersionMajor === 2018 && program.browserVersionMinor === 1 && program.browserVersionRevision > 4) ||
        //        (program.browserVersionMajor === 2018 && program.browserVersionMinor === 1 && program.browserVersionRevision === 4 && program.browserVersionBuild >= 2)) {
        //        //FwNotification.renderNotification('ERROR', 'RFID Reader Disconnected');
        //        RwRFID.isConnected = false;
        //    } else {
        //        // the TSL plugin was firing this event for any connected device, so this was firing incorrectly when linea was unplugged.
        //        if (RwRFID.isConnected === true) {
        //            //FwNotification.renderNotification('ERROR', 'RFID Reader Disconnected');
        //            RwRFID.isConnected = false;
        //        }
        //    }
        //    FwMobileMasterController.generateDeviceStatusIcons();
        //});
        window.TslReader.registerListener('barcodeReceived', 'epcsReceived_rwrfidjs', function(barcode) {
            program.onBarcodeData(barcode);
        });
        window.TslReader.registerListener('epcsReceived', 'epcsReceived_rwrfidjs', function (epcs) {
            RwRFID.isConnected = true;
            callbackfunction(epcs);
            if (jQuery('.tagCountPopup').length) {
                FwConfirmation.destroyConfirmation(jQuery('.tagCountPopup'));
            }
        });
        window.TslReader.registerListener('epcReceived', 'epcReceived_rwrfidjs', function(epc, count) {
            RwRFID.isConnected = true;
            if (jQuery('.tagCountPopup').length > 0) {
                jQuery('.tagCount').html(count);
                //var epcs = jQuery('.tagCountPopup').data('epcs');
                //epcs.push(epc);
                //jQuery('.rwrfid-epc').html(epcs.join('<br>'));
            } else {
                var html = [];
                html.push('<div class="tagCount" style="color:black;font-weight:bold;text-align:center;font-size:100px;"></div>');
                //html.push('<div class="rwrfid-epc" style="text-align:center;"></div>');
                var $confirmation = FwConfirmation.renderConfirmation('Tags Scanned', html.join('\n'));
                var $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
                $confirmation.data('epcs', [epc]);
                $confirmation.addClass('tagCountPopup');
                $confirmation.find('.tagCount').html(count);
                $confirmation.find('.epc').html(epc);
                if (me.isPerformingSoftwareSinglePress) {
                    var $btnstop = FwConfirmation.addButton($confirmation, 'Stop', true);
                    $btnstop.on('click', function () {
                        try {
                            me.tslAbort();
                        } catch (ex) {
                            FwFunc.show(ex);
                        }
                    });
                }
            }
        });
    }

    if (typeof window.ZebraRFIDAPI3 !== 'undefined') {
        window.ZebraRFIDAPI3.registerListener('barcodeReceived', 'barcodeReceived_rwrfidjs', function (barcode, barcodetype, source, platform) {
            program.onBarcodeData(barcode);
        });
        window.ZebraRFIDAPI3.registerListener('epcsReceived', 'epcsReceived_rwrfidjs', function (epcs, count) {
            RwRFID.isConnected = true;
            callbackfunction(epcs);
            if (jQuery('.tagCountPopup').length) {
                FwConfirmation.destroyConfirmation(jQuery('.tagCountPopup'));
            }
        });
        window.ZebraRFIDAPI3.registerListener('epcReceived', 'epcReceived_rwrfidjs', function (epc, count) {
            RwRFID.isConnected = true;
            if (jQuery('.tagCountPopup').length > 0) {
                jQuery('.tagCount').html(count);
                //var epcs = jQuery('.tagCountPopup').data('epcs');
                //epcs.push(epc);
                //jQuery('.rwrfid-epc').html(epcs.join('<br>'));
            } else {
                var html = [];
                html.push('<div class="tagCount" style="color:black;font-weight:bold;text-align:center;font-size:100px;"></div>');
                //html.push('<div class="rwrfid-epc" style="text-align:center;"></div>');
                var $confirmation = FwConfirmation.renderConfirmation('Tags Scanned', html.join('\n'));
                var $btnClose = FwConfirmation.addButton($confirmation, 'Close', true);
                $confirmation.data('epcs', [epc]);
                $confirmation.addClass('tagCountPopup');
                $confirmation.find('.tagCount').html(count);
                $confirmation.find('.epc').html(epc);
                if (me.isPerformingSoftwareSinglePress) {
                    var $btnstop = FwConfirmation.addButton($confirmation, 'Stop', true);
                    $btnstop.on('click', function () {
                        try {
                            me.tslAbort();
                        } catch (ex) {
                            FwFunc.show(ex);
                        }
                    });
                }
            }
        });
    }

    //if (typeof window.ZebraRFIDScanner !== 'undefined') {
    //    window.ZebraRFIDScanner.registerListener('epcsReceived', 'epcsReceived_rwrfidjs', function(epcs) {
    //        callbackfunction(epcs);
    //        if (jQuery('.tagCountPopup').length) {
    //            FwConfirmation.destroyConfirmation(jQuery('.tagCountPopup'));
    //        }
    //    });
    //    window.ZebraRFIDScanner.registerListener('epcReceived', 'epcReceived_rwrfidjs', function(epc, count) {
    //        if (jQuery('.tagCountPopup').length) {
    //            jQuery('.tagCount').html(count);
    //        } else {
    //            var $confirmation;
    //            $confirmation = FwConfirmation.renderConfirmation('Tags Scanned', '<div class="tagCount" style="color:black;font-weight:bold;text-align:center;font-size:100px;"></div>');
    //            $confirmation.addClass('tagCountPopup');
    //            $confirmation.find('.tagCount').html(count);
    //        }
    //    });
    //}
};
//----------------------------------------------------------------------------------------------
RwRFID.unregisterEvents = function () {
    if (typeof window.TslReader !== 'undefined') {
        window.TslReader.unregisterListener('epcsReceived', 'epcsReceived_rwrfidjs');
        window.TslReader.unregisterListener('epcReceived', 'epcReceived_rwrfidjs');
        window.TslReader.unregisterListener('epcReceived', 'deviceConnected_rwrfidjs');
        window.TslReader.unregisterListener('epcReceived', 'deviceDisconnected_rwrfidjs');
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
RwRFID.tslSwitchSinglePress = function (duration) {
    if (typeof window.TslReader === 'object' && typeof window.TslReader.switchSinglePress === 'function') {
        this.isPerformingSoftwareSinglePress = true;
        window.TslReader.switchSinglePress(duration);
    }
};
//----------------------------------------------------------------------------------------------
RwRFID.tslAbort = function () {
    if (typeof window.TslReader === 'object' && typeof window.TslReader.abort === 'function') {
        this.isPerformingSoftwareSinglePress = false;
        window.TslReader.abort(function success(args) { });
    }
};
//----------------------------------------------------------------------------------------------
