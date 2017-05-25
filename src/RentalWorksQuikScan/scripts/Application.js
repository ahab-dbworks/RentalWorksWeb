var application, ScannerDevice, LineaScanner;
Application.prototype = new FwApplication;
Application.constructor = Application;
//---------------------------------------------------------------------------------
function Application() { FwApplication.call(this);
    var me;
    
    me = this;
    FwApplicationTree.currentApplicationId = '8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A';
    me.name = 'RentalWorks';
    me.htmlname = '<span class="bgothm" style="color:#2f2f2f;">Rental</span><span class="bgothm" style="color:#6f30b3;">Works</span>';
    me.didLoadApplication = false;
    me.audioMode = 'html5';
    me.audioSuccessArray = [1200, 300];
    me.audioErrorArray   = [800, 200, 600, 200];
    me.localstorageprefix = 'rwqs_';
    me.localstorageitems = {
        rfidstaging_batchtimeout: me.localstorageprefix + 'rfidstaging_batchtimeout',
        rfidstaging_scanagain: me.localstorageprefix + 'rfidstaging_scanagain'
    };

    jQuery('body')
        .on('touchstart', '.fwmenu-item', 
            function() {
                jQuery(this).addClass('active');
            }
        )
        .on('touchstart', '.button.default', 
            function() {
                jQuery(this).addClass('active');
            }
        )
        .on('touchend', '.fwmenu-item', 
            function() {
                jQuery(this).removeClass('active');
            }
        )
        .on('touchend', '.button.default', 
            function() {
                jQuery(this).removeClass('active');
            }
        )
    ;
}
//---------------------------------------------------------------------------------
Application.prototype.setScanTarget = function(selector) {
    this.activeTextBox = selector;
    sessionStorage.setItem('scanTarget', selector);
    jQuery('.textbox').removeClass('scanTarget');
    if (selector !== '') {
        jQuery(this.activeTextBox).addClass('scanTarget');
    }
};
//---------------------------------------------------------------------------------
Application.prototype.getApplicationOptions = function() {
    return JSON.parse(sessionStorage.getItem('applicationOptions'));
};
//---------------------------------------------------------------------------------
Application.prototype.playStatus = function (isSuccessful) {
    var me, sound;
    me = this;
    if (isSuccessful) {
        if (typeof window.DTDevices === 'object') {
            window.DTDevices.playSound(me.audioSuccessArray); 
        } else {
            //if (typeof window.Audio === 'object') {
            //    sound = new Audio(applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'audio/success2.wav');
            //    sound.load();
            //    sound.currentTime = 0;
            //    sound.play();
            //}
        }
    } else {
        if (typeof window.DTDevices === 'object') {
            window.DTDevices.playSound(me.audioErrorArray); 
        } else {
            //if (typeof window.Audio === 'object') {
            //    sound = new Audio(applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'audio/error2.wav');
            //    sound.load();
            //    sound.currentTime = 0;
            //    sound.play();
            //}
        }
    }
};
//---------------------------------------------------------------------------------
Application.prototype.navigate = function(path) {
    var me, screen;
    me = this;
    var lowercasepath = path.toLowerCase();
    if (sessionStorage.getItem('sessionLock') === 'true') {
        FwFunc.showError('Navigation is locked.');
    } else {
        sessionStorage.setItem('activePath', path);
        var foundroute = false;
        for (var routeno = 0; routeno < RwRoutes.length; routeno++) {
            var route = RwRoutes[routeno];
            if (route.url === lowercasepath) {
                foundroute = true;
                screen = route.action();
                break;
            }
        }
        if (!foundroute) {
            switch (path.toLowerCase()) {
                case 'account/login':
                    if (!sessionStorage.getItem('authToken')) {
                        screen = RwAccountController.getLoginScreen({}, {});
                    } else {
                        me.navigate('home/home');
                        return;
                    }
                    break;
                case 'account/preferences':
                    screen = RwAccountController.getPreferencesScreen({}, {});
                    break;
                case 'account/privacypolicy':
                    screen = RwAccountController.getPrivacyPolicyScreen({}, {});
                    break;
                case 'account/support':
                    screen = RwAccountController.getSupportScreen({}, {});
                    break;
                case 'home/home':
                    screens = [];
                    screen = RwHome.getHomeScreen({}, {});
                    break;
                case 'staging':
                    screen = RwOrderController.getStagingScreen({}, {
                        moduleType: RwConstants.moduleTypes.Order,
                        activityType: RwConstants.activityTypes.Staging,
                        stagingType: RwConstants.stagingType.Normal
                    });
                    break;
                case 'order/fillcontainer':
                    screen = RwFillContainer.getFillContainerScreen({}, {
                        mode: 'fillcontainer'
                    });
                    break;
                case 'order/checkinmenu':
                    screen = RwOrderController.getCheckInMenuScreen({}, {});
                    break;
                case 'order/itemstatus':
                    screen = RwOrderController.getItemStatusScreen({});
                    break;
                case 'inventory/subreceive':
                    screen = RwSelectPO.getSelectPOScreen({}, {
                        moduleType: RwConstants.moduleTypes.SubReceive,
                        activityType: RwConstants.activityTypes.SubReceive
                    });
                    break;
                case 'inventory/subreturn':
                    screen = RwSelectPO.getSelectPOScreen({}, {
                        moduleType: RwConstants.moduleTypes.SubReturn
                        , activityType: RwConstants.activityTypes.SubReturn
                    });
                    break;
                case 'inventory/repairmenu':
                    screen = RwInventoryController.getRepairMenuScreen({}, {});
                    break;
                case 'inventory/repair/complete':
                    screen = RwInventoryController.getRepairItemScreen({}, {
                        repairMode: RwConstants.repairModes.Complete
                    });
                    break;
                case 'inventory/repair/release':
                    screen = RwInventoryController.getRepairItemScreen({}, {
                        repairMode: RwConstants.repairModes.Release
                    });
                    break;
                case 'inventory/repair/repairorder':
                    screen = RwInventoryController.getRepairOrderScreen({}, { mode: 'repairorder' });
                    break;
                case 'inventory/assetdisposition':
                    screen = RwInventoryController.getAssetDispositionScreen({}, {});
                    break;
                case 'inventory/inventorywebimage':
                    screen = RwInventoryController.getInventoryWebImageScreen({}, {});
                    break;
                case 'order/packagetruck':
                    screen = RwOrderController.getPackageTruckMenuScreen({}, {});
                    break;
                case 'order/packagetruck/staging':
                    screen = RwOrderController.getStagingScreen({}, {
                        moduleType: RwConstants.moduleTypes.Truck,
                        activityType: RwConstants.activityTypes.Staging
                    });
                    break;
                case 'order/packagetruck/checkin':
                    screen = RwSelectOrder.getSelectOrderScreen({}, {
                        moduleType: RwConstants.moduleTypes.Truck,
                        activityType: RwConstants.activityTypes.CheckIn,
                        checkInMode: RwConstants.checkInModes.SingleOrder,
                        checkInType: RwConstants.checkInType.Normal
                    });
                    break;
                case 'utilities/exchange':
                    screen = Exchange.getModuleScreen({}, {});
                    break;
                case 'utilities/physicalinventory':
                    screen = RwSelectPhyInv.getSelectPhyInvScreen({}, {
                        moduleType: RwConstants.moduleTypes.PhyInv
                    });
                    break;
                case 'inventory/qc':
                    screen = RwInventoryController.getQCScreen({}, {});
                    break;
                case 'transferout':
                    screen = RwOrderController.getStagingScreen({}, {
                        moduleType: RwConstants.moduleTypes.Transfer,
                        activityType: RwConstants.activityTypes.Staging,
                        stagingType: RwConstants.stagingType.Normal
                    });
                    break;
                case 'order/transferin':
                    screen = RwOrderController.getTransferInMenuScreen({}, {});
                    break;
                case 'order/transferin/singleorder':
                    screen = RwSelectTransferOrder.getSelectTransferOrderScreen({}, {
                        moduleType: RwConstants.moduleTypes.Transfer,
                        activityType: RwConstants.activityTypes.CheckIn,
                        checkInMode: RwConstants.checkInModes.SingleOrder,
                        checkInType: RwConstants.checkInType.Normal
                    });
                    break;
                case 'order/transferin/session':
                    screen = RwSelectSession.getSelectSessionScreen({}, {
                        moduleType: RwConstants.moduleTypes.Transfer,
                        activityType: RwConstants.activityTypes.CheckIn,
                        checkInMode: RwConstants.checkInModes.Session,
                        checkInType: RwConstants.checkInType.Normal
                    });
                    break;
                case 'inventory/movebclocation':
                    screen = RwInventoryController.getMoveBCLocationScreen({}, {});
                    break;
                case 'account/logoff':
                    FwApplicationTree.tree = null;
                    sessionStorage.clear();
                    me.screens = [];
                    me.navigate('account/login');
                    return;
                case 'quote/quotemenu':
                    screen = RwQuoteMenu.getQuoteMenuScreen({}, {});
                    break;
                case 'quote/quote':
                    screen = RwQuote.getQuoteScreen({}, {});
                    break;
                case 'timelog':
                    screen = TimeLog.getModuleScreen({}, {});
                    break;
                case 'receiveonset':
                    screen = ReceiveOnSet.getModuleScreen({}, {});
                    break;
                case 'assetsetlocation':
                    screen = AssetSetLocation.getModuleScreen({}, {});
                    break;
                case 'rfidstaging':
                    screen = RwSelectOrder.getSelectOrderScreen({}, {
                        moduleType: RwConstants.moduleTypes.Order,
                        activityType: RwConstants.activityTypes.Staging,
                        stagingType: RwConstants.stagingType.RfidPortal
                    });
                    break;
                case 'rfidcheckin':
                    screen = RwSelectOrder.getSelectOrderScreen({}, {
                        moduleType: RwConstants.moduleTypes.Order,
                        activityType: RwConstants.activityTypes.CheckIn,
                        checkInType: RwConstants.checkInType.RfidPortal
                    });
                    break;
                case 'assignitems':
                    screen = AssignItems.getMenuScreen({}, {});
                    break;
                case 'assignitems/newitems':
                    screen = AssignItems.getNewItemsScreen({}, {});
                    break;
                case 'assignitems/existingitems':
                    screen = AssignItems.getExistingItemsScreen({}, {});
                    break;
            }
        }
        // this is how you do the navigate away in a screen
        // instead of returning true/false it makes you manually call a function, so you can use callbacks in beforeNavigateAway
        // screen.beforeNavigateAway = function(navigateAway) {
        //     RwServices.callMethod('Module', 'Method', {}, function(response) {
        //       if (response.iscool) { navigateAway(); }
        //     }}
        // };
        var waitfornavigateaway = false;
        if (me.screens.length > 0) {
            var currentScreen = me.screens[me.screens.length - 1];
            if (typeof currentScreen.beforeNavigateAway === 'function') {
                waitfornavigateaway = true;
                currentScreen.beforeNavigateAway(function navigateaway() {
                    me.pushScreen(screen);
                    if (me.screens.length > 1) {
                        me.screens.splice(0, me.screens.length - 1);
                    }
                });
            }
        }
        if (!waitfornavigateaway) {
            me.pushScreen(screen);
            if (me.screens.length > 1) {
                me.screens.splice(0, me.screens.length - 1);
            }
        }
    }
};
//---------------------------------------------------------------------------------
Application.prototype.loadApplication = function() {
    var me = this;
    if (!me.didLoadApplication) {
        me.didLoadApplication = true;
        jQuery('#index-loading').hide();
        jQuery('#index-loadingInner').hide();
        application.load();
        jQuery('html').addClass('mobile');
        jQuery('html').addClass('theme-classic'); //MY 10/21/2016: Change to default once it is tested.
        if (sessionStorage.getItem('sessionLock') === 'true') {
            sessionStorage.setItem('sessionLock', false);
            me.navigate('account/logoff');
        } else if (sessionStorage.getItem('authToken')) {
            me.navigate('home/home');
        } else {
            me.navigate('account/login');
        }
        jQuery('html').on('focus', '#scanBarcodeView-txtBarcodeData', function (e) {
            jQuery('#scanBarcodeView .clearbarcode').hide();
        });
        jQuery('html').on('blur change', '#scanBarcodeView-txtBarcodeData', function (e) {
            if (this.value.length === 0) {
                setTimeout(function () {
                    jQuery('#scanBarcodeView .clearbarcode').hide();
                }, 100);
            } else {
                jQuery('#scanBarcodeView .clearbarcode').show();
            }
        });
        jQuery('html').on('click', '#scanBarcodeView .clearbarcode', function (e) {
            e.preventDefault();
            e.stopPropagation();
            jQuery('#scanBarcodeView-txtBarcodeData').val('');
            jQuery('#scanBarcodeView .clearbarcode').hide();
            jQuery('#scanBarcodeView-txtBarcodeData').focus();
            
            setTimeout(function () {
                
            }, 100);
        });
    }
};
//---------------------------------------------------------------------------------
Application.prototype.setDeviceConnectionState = function(connectionState) {
    if (jQuery('html').attr('connectionstate') !== connectionState) {
        if (connectionState === 'CONNECTED') {
            jQuery('#master-footer-connectionstate').text('CONNECTED').show();
            setTimeout(function() {
                if (jQuery('#master-footer-connectionstate').text() === 'CONNECTED') {
                    jQuery('#master-footer-connectionstate').hide();
                    jQuery('#master-footer').hide();
                }
            }, 1000);
        } else if (connectionState === 'CONNECTING') {
            jQuery('#master-footer').show();
            jQuery('#master-footer-connectionstate').text('CONNECTING...').show();
            setTimeout(function() {
                if (jQuery('#master-footer-connectionstate').text() === 'CONNECTING...') {
                    jQuery('#master-footer-connectionstate').text('Press scan button to wake Linea...');
                }
            }, 3000);
        } else if (connectionState === 'DISCONNECTED') {
            jQuery('#master-footer-connectionstate').text('DISCONNECTED').show();
            jQuery('#master-footer').show();
        } else if (connectionState === 'DEVICENOTPRESENT') {
            jQuery('#master-footer-connectionstate').text('').hide();
            jQuery('#master-footer').hide();
        }
        jQuery('html').attr('connectionstate', connectionState);
    }
};
//---------------------------------------------------------------------------------
Application.prototype.updateConnectionState = function() {
    // Update the connection state without waiting for a connection state change callback
    try {
        if ((typeof window.DTDevices === 'object') && (typeof window.DTDevices.getDeviceConnectionState === 'function')) {
            window.DTDevices.getDeviceConnectionState(
                function(connectionState) {
                    try {
                        application.setDeviceConnectionState(connectionState);
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                }, 
                function() {
                    try {
                        application.setDeviceConnectionState('DISCONNECTED');
                    } catch(ex) {
                        FwFunc.showError(ex);
                    }
                }
            );
        }
    } catch(ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
application = new Application();
//---------------------------------------------------------------------------------
jQuery(function() {
    if (applicationConfig.debugMode) {
        application.forceReloadCss();
    }
    FastClick.attach(document.body);
    
    application.setScanTarget('#scanBarcodeView-txtBarcodeData');
    application.onBarcodeData = function(barcode, barcodeType) {
        if ((typeof application.activeTextBox === 'undefined') || (jQuery(sessionStorage.getItem('scanTarget')).length === 0)) {
            // do nothing
        } else {
            if (jQuery(sessionStorage.getItem('scanTarget')).hasClass('fwformfield')) {
                FwFormField.setValue(jQuery('html'), sessionStorage.getItem('scanTarget'), barcode, '', true);
            } else {
                jQuery(sessionStorage.getItem('scanTarget')).val(barcode).change();
            }
        }
    };
    setTimeout(function() {
        application.loadApplication();
    }, 2000);
});
//---------------------------------------------------------------------------------
if (typeof document.addEventListener !== 'undefined') {
    document.addEventListener('deviceready', function (){
        if ((typeof window.screen === 'object') && (typeof window.screen.lockOrientation === 'function')) {
            window.screen.lockOrientation('portrait-primary');
        }
        
        if (typeof window.DTDevices === 'object') {
            window.DTDevices.barcodeSetScanBeep(true, [500,50]);
            window.DTDevices.startListening();
            window.DTDevices.registerListener('barcodeData', 'barcodeData_applicationjs', function(barcode, barcodeType) {
                application.onBarcodeData(barcode);
            });
            if (typeof localStorage.scanMode === 'undefined') {
                localStorage.barcodeScanMode = 'MODE_SINGLE_SCAN';
            }
            window.DTDevices.barcodeSetScanMode(localStorage.barcodeScanMode);
        }
        
        if (typeof window.TslReader === 'object') {
            window.TslReader.startListening();
            //window.TslReader.registerListener('epcsReceived', 'epcsReceived_applicationjs', function(epcs) {   
            //    application.onBarcodeData(epcs);
            //});
            window.TslReader.registerListener('barcodeReceived', 'barcodeReceived_applicationjs', function(barcode) {
                application.onBarcodeData(barcode);
            });
            window.TslReader.registerListener('deviceConnected', 'deviceConnected_applicationjs', function() {
                RwRFID.isConnected = true;
            });
            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_applicationjs', function() {
                RwRFID.isConnected = false;
            });
            window.TslReader.connectDevice(function connectDeviceSuccess(isConnected) {
                RwRFID.isConnected = isConnected;
            });
        }

        //if (typeof window.Zebra2DScanner === 'object') {
            //window.Zebra2DScanner.startListening();
            //window.Zebra2DScanner.registerListener('sbtEventBarcode', 'barcodeReceived_applicationjs', function(barcode, barcodetype) {
            //    application.onBarcodeData(barcode);
            //});
            //window.Zebra2DScanner.registerListener('sbtEventScannerDisappeared', 'sbtEventScannerDisappeared_applicationjs', function() {
                
            //});
            //window.Zebra2DScanner.registerListener('sbtEventCommunicationSessionEstablished', 'sbtEventCommunicationSessionEstablished_applicationjs', function() {
                
            //});
            //window.Zebra2DScanner.registerListener('sbtEventCommunicationSessionTerminated', 'sbtEventCommunicationSessionTerminated_applicationjs', function() {
                
            //});
        //}

        //if (typeof window.ZebraRFIDScanner === 'object') {
            //window.ZebraRFIDScanner.startListening();
            //window.ZebraRFIDScanner.isConnected(function(isConnected) {
            //    RwRFID.isConnected = isConnected;
            //});
            //window.ZebraRFIDScanner.registerListener('sbtEventScannerDisappeared', 'sbtEventScannerDisappeared_applicationjs', function() {
                
            //});
            //window.ZebraRFIDScanner.registerListener('sbtEventCommunicationSessionEstablished', 'sbtEventCommunicationSessionEstablished_applicationjs', function() {
                
            //});
            //window.ZebraRFIDScanner.registerListener('sbtEventCommunicationSessionTerminated', 'sbtEventCommunicationSessionTerminated_applicationjs', function() {
                
            //});
        //}

        //set the connection state when it changes
        window.DTDevices.registerListener('connectionState', 'connectionState_applicationjs', function(connectionState) {
            application.setDeviceConnectionState(connectionState);
        });
        // since the initial connection state event usually happens early in the page life cycle, we need to explicity query the value the first time
        application.updateConnectionState();

        setTimeout(function() {
            application.loadApplication();
        }, 0);


    }, false);
}
//---------------------------------------------------------------------------------
jQuery(document)
    .on('click', '#scanBarcodeView-button', function (event) {
        cordova.plugins.barcodeScanner.scan(successCallback, errorCallback);
        function successCallback(result) {
            if (result.text.length > 0) {
                jQuery('#scanBarcodeView-txtBarcodeData').val(result.text).change();
            }
        }
        function errorCallback(ex) {
            FwFunc.showError(ex);
        }
    })
    //2016-10-31 MY: This is causing change events to be triggered twice throughout the system.
    //.on('keydown', '#scanBarcodeView-txtBarcodeData', function(event) {
    //    var $this;
    //    try {
    //        $this = jQuery(this);
    //        if (event.which === 13) {
    //            $this.change();
    //        }
    //    } catch(ex) {
    //        FwFunc.showError(ex);
    //    }
    //})
;
//---------------------------------------------------------------------------------
