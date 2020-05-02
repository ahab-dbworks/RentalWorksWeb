var RwRFID = {
    isConnected: false,
    isPerformingSoftwareSinglePress: false,
    zebraTriggerMode: 'BARCODE',
    isRFIDAPI3: true
    isTsl: false
};
//----------------------------------------------------------------------------------------------
RwRFID.init = function() {
    var applicationOptions;
    applicationOptions = program.getApplicationOptions();
    if ((typeof window.TslReader === 'object') && (typeof applicationOptions.rfid !== 'undefined') && (applicationOptions.rfid.enabled)) {
        window.TslReader.isConnected(function isConnectedSuccess(result) {
            RwRFID.isTsl = true;
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

    // ZebraRFIDAPI3
    if (typeof window.ZebraRFIDAPI3 !== 'undefined') {
        window.ZebraRFIDAPI3.registerListener('beginResume', 'beginResume_rwrfidjs', function (deviceManufacturer, deviceModel, androidSdkVersion) {
            try {
                if (deviceManufacturer === 'Zebra Technologies' && deviceModel === 'MC33') {
                    RwRFID.isRFIDAPI3 = true;
                    RwRFID.showRFIDNotification('Configuring scanner...', false);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        window.ZebraRFIDAPI3.registerListener('endResume', 'endResume_rwrfidjs', function (deviceManufacturer, deviceModel, androidSdkVersion) {
            try {
                if (deviceManufacturer === 'Zebra Technologies' && deviceModel === 'MC33') {
                    RwRFID.showRFIDNotification('Scanner ready', true);
                }
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        window.ZebraRFIDAPI3.registerListener('emdkScannerStatus', 'endResume_rwrfidjs', function (scannerStatus) {
            //RwRFID.showRFIDNotification(scannerStatus, true);
        });
        window.ZebraRFIDAPI3.registerListener('emdkKeyPressed', 'emdkKeyPressed_rwrfidjs', function (keyCode) {
            switch (keyCode) {
                case "MC33-P1":
                    window.ZebraRFIDAPI3.toggleTriggerMode(
                        /* successs */
                        (args) => {
                            const triggerMode = args[0];
                            window.ZebraRFIDAPI3.speak(triggerMode, () => {});
                            RwRFID.zebraTriggerMode = triggerMode;
                            RwRFID.showRFIDNotification(triggerMode, true);
                        }, null
                    );
                    break;
                case "MC33-P2":
                    if (jQuery('.fwcontextmenu').length > 0) {
                        jQuery('.fwcontextmenu').remove();
                    } else {
                        const $contextmenu = FwContextMenu.render("RFID", null);
                        FwContextMenu.addMenuItem($contextmenu, 'Power Level', function () {
                            try {
                                var htmlSlider = [];
                                htmlSlider.push('<div>');
                                htmlSlider.push('  <div id="slider" style="margin:48px 32px 32px 32px;"></div>');
                                htmlSlider.push('</div>');
                                var $rfidmenu = FwConfirmation.renderConfirmation('RFID Antenna Power (dB)', htmlSlider.join('\n'));
                                var $btnCancel = FwConfirmation.addButton($rfidmenu, 'Close', true);
                                window.ZebraRFIDAPI3.getPowerLevel(
                                    function success(args) {
                                        var outputPower = args[0];
                                        var minPower = args[1];
                                        var maxPower = args[2];
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
                                            window.ZebraRFIDAPI3.setPowerLevel(rfidPowerLevel,
                                                function (args) {
                                                    var outputPower = args[0];
                                                    rfidPowerLevelSlider.noUiSlider.set(outputPower);
                                                });
                                        });
                                        //jQuery('.tslpanel').show();
                                    });
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });
                        //FwContextMenu.addMenuItem($contextmenu, 'Tag Finder', function () {
                        //    try {

                        //    } catch (ex) {
                        //        FwFunc.showError(ex);
                        //    }
                        //});
                    }
                    break;
            }
        });
        window.ZebraRFIDAPI3.registerListener('triggerPress', 'triggerPulled_rwrfidjs', function (pressed) {
            if (RwRFID.zebraTriggerMode === 'RFID') {
                if (pressed) {
                    window.ZebraRFIDAPI3.startInventory();
                } else {
                    window.ZebraRFIDAPI3.stopInventory();
                }
            }
        });
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
            if (count > 0) {
                window.ZebraRFIDAPI3.beepAndSpeak(count.toString());
            }
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
        window.ZebraRFIDAPI3.registerListener('tagFinder', 'tagFinder_rwrfidjs', function (tag, distance) {
            RwRFID.updateTagFinderNotification(tag, distance);
        });
        window.ZebraRFIDAPI3.registerListener('tagFinderStopped', 'tagFinderStopped_rwrfidjs', function () {
            RwRFID.hideTagFinderNotification();
        });
    }
};
//----------------------------------------------------------------------------------------------
RwRFID.hideTagFinderNotification = function () {
    jQuery('.tagfinder-notification').remove();
};
//----------------------------------------------------------------------------------------------
RwRFID.startTagFinder = function (tag) {
    RwRFID.hideRFIDNotification();
    let $notificationWrapper = jQuery('<div class="tagfinder-notification"></div>')
        .css({
            'position': 'absolute',
            'top': window.scrollY + 'px',
            'right': '0',
            'bottom': '0',
            'left': '0',
            'display': 'flex',
            'flex-direction': 'column',
            'align-items': 'center',
            'justify-content': 'center',
            'z-index': FwFunc.getMaxZ('*')
        });
    let html = [];
    html.push('<div style="text-align:center">');
    html.push(`  <div>Tag Finder</div>`);
    html.push(`  <div class="tag" style="font-size:9px;">${tag}</div>`);
    html.push(`  <div>Distance: <span class="distance">0</span>%</div>`);
    html.push(`  <progress class="progress" value="0" max="100"></progress>`);
    html.push('  <div id="slider" style="margin:48px 32px 32px 32px;"></div>');
    html.push(`  <div><button class="btnStop" style="height:44px;font-size:16px;width:100px;">Stop</button></div>`);
    html.push('</div>');
    html = html.join('\n');
    let $notification = jQuery(html)
        .css({
            'background-color': '#000000',
            'font-size': '22px',
            'color': '#ffffff',
            'padding': '10px',
            'opacity': '.95'    
        })
        .on('click', '.btnStop', e => {
            window.ZebraRFIDAPI3.stopTagFinder(() => {
                $notification.remove();
            });
        });
    $notificationWrapper.append($notification);
    window.ZebraRFIDAPI3.getPowerLevel(
        function success(args) {
            var outputPower = args[0];
            var minPower = args[1];
            var maxPower = args[2];
            var rfidPowerLevelSlider = $notificationWrapper.find('#slider').get(0);
            noUiSlider.create(rfidPowerLevelSlider, {
                start: [outputPower],
                range: {
                    'min': [minPower],
                    'max': [maxPower]
                },
                tooltips: [wNumb({ decimals: 0 })]
            });
            rfidPowerLevelSlider.noUiSlider.on('change', function () {
                window.ZebraRFIDAPI3.stopTagFinder(() => {
                    var rfidPowerLevel = parseFloat(rfidPowerLevelSlider.noUiSlider.get());
                    window.ZebraRFIDAPI3.setPowerLevel(rfidPowerLevel,
                        function (args) {
                            var outputPower = args[0];
                            rfidPowerLevelSlider.noUiSlider.set(outputPower);
                            RwRFID.startTagFinder(tag);
                        });
                });
            });
            window.ZebraRFIDAPI3.startTagFinder(tag);
        });
    jQuery('body').append($notificationWrapper);

}
//----------------------------------------------------------------------------------------------
RwRFID.updateTagFinderNotification = function (tag, distance) {
    jQuery('.tagfinder-notification .tag').text(tag);
    jQuery('.tagfinder-notification .distance').text(distance);
    jQuery('.tagfinder-notification .progress').attr('value', distance);
};
//----------------------------------------------------------------------------------------------
RwRFID.hideRFIDNotification = function () {
    jQuery('.rfid-notification').remove();
};
//----------------------------------------------------------------------------------------------
RwRFID.showRFIDNotification = function (text, autoHide) {
    RwRFID.hideRFIDNotification();
    let $notificationWrapper = jQuery('<div class="rfid-notification"></div>')
        .css({
            'position': 'absolute',
            'top': '0',
            'right': '0',
            'bottom': '0',
            'left': '0',
            'display': 'flex',
            'flex-direction': 'column',
            'align-items': 'center',
            'justify-content': 'center',
            'z-index': FwFunc.getMaxZ('*')
        });
    let $notification = jQuery(`<div>${text}</div>`)
        .css({
            'background-color': '#000000',
            'font-size': '22px',
            'color': '#ffffff',
            'padding': '10px',
            'opacity': '.8'
        })
        .click(e => {
            $notification.remove();
        });
    $notificationWrapper.append($notification);
    jQuery('body').append($notificationWrapper);
    if (autoHide) {
        window.setTimeout(() => {
            $notificationWrapper.remove();
        }, 500);
    }
}
//----------------------------------------------------------------------------------------------
RwRFID.unregisterEvents = function () {
    if (typeof window.TslReader !== 'undefined') {
        window.TslReader.unregisterListener('epcsReceived', 'epcsReceived_rwrfidjs');
        window.TslReader.unregisterListener('epcReceived', 'epcReceived_rwrfidjs');
        window.TslReader.unregisterListener('epcReceived', 'deviceConnected_rwrfidjs');
        window.TslReader.unregisterListener('epcReceived', 'deviceDisconnected_rwrfidjs');
    }
    if (typeof window.ZebraRFIDAPI3 !== 'undefined') {
        window.ZebraRFIDAPI3.unregisterListener('beginResume', 'beginResume_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('endResume', 'endResume_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('emdkScannerStatus', 'emdkScannerStatus_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('emdkKeyPressed', 'emdkKeyPressed_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('triggerPress', 'triggerPulled_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('barcodeReceived', 'barcodeReceived_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('epcsReceived', 'epcsReceived_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('epcReceived', 'epcReceived_rwrfidjs');
        window.ZebraRFIDAPI3.unregisterListener('tagFinder', 'tagFinder_rwrfidjs');
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
