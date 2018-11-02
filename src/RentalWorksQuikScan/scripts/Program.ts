var program: Program;
var application: Program;
//---------------------------------------------------------------------------------
class Program extends FwApplication {
    activeTextBox:      string;
    htmlname:           string;
    didLoadApplication: boolean;
    onBarcodeData:      (data: string, type?: string) => void;
    onScanBarcode:      (data: string, type?: string) => void;
    localstorageprefix: string;
    localstorageitems:  object;
    runningInCordova:   boolean = false;
    //---------------------------------------------------------------------------------
    constructor() {
        super();
        var me = this;
        FwApplicationTree.currentApplicationId = '8D0A5ECF-72D2-4428-BDC8-7E3CC56EDD3A';
        me.name                                = 'RentalWorks';
        me.htmlname                            = '<span class="bgothm" style="color:#2f2f2f;">Rental</span><span class="bgothm" style="color:#6f30b3;">Works</span>';
        me.didLoadApplication                  = false;
        me.localstorageprefix                  = 'rwqs_';
        me.localstorageitems = {
            rfidstaging_batchtimeout: me.localstorageprefix + 'rfidstaging_batchtimeout',
            rfidstaging_scanagain:    me.localstorageprefix + 'rfidstaging_scanagain'
        };

        jQuery('body')
            .on('touchstart', '.fwmenu-item', function() {
                jQuery(this).addClass('active');
            })
            .on('touchstart', '.button.default', function() {
                jQuery(this).addClass('active');
            })
            .on('touchend', '.fwmenu-item', function() {
                jQuery(this).removeClass('active');
            })
            .on('touchend', '.button.default', function() {
                jQuery(this).removeClass('active');
            })
        ;

        if (applicationConfig.debugMode) {
            me.forceReloadCss();
        }
        //FastClick.attach(document.body);
        
        me.setScanTarget('#scanBarcodeView-txtBarcodeData');
        me.onBarcodeData = function(barcode, barcodeType) {
            if (typeof me.onScanBarcode === 'function') {
                me.onScanBarcode(barcode, barcodeType);
            } else {
                if ((typeof me.activeTextBox === 'undefined') || (jQuery(sessionStorage.getItem('scanTarget')).length === 0)) {
                    // do nothing
                } else {
                    if (jQuery(sessionStorage.getItem('scanTarget')).hasClass('fwformfield')) {
                        FwFormField.setValue(jQuery('html'), sessionStorage.getItem('scanTarget'), barcode, '', true);
                    } else {
                        jQuery(sessionStorage.getItem('scanTarget')).val(barcode).change();
                    }
                }
            }
        };
        setTimeout(function() {
            me.loadApplication();
        }, 2000);

        if (typeof document.addEventListener !== 'undefined') {
            document.addEventListener('deviceready', function (){
                me.runningInCordova = true;
                if ((typeof window.screen === 'object') && (typeof (<any>window).screen.lockOrientation === 'function')) {
                    var orientation = localStorage.getItem('orientation');
                    if (typeof orientation !== 'string') {
                        localStorage.setItem('orientation', 'portrait-primary');
                        orientation = localStorage.getItem('orientation');
                    }
                    if (orientation === 'unlocked') {
                        (<any>window).screen.unlockOrientation();
                    } else {
                        (<any>window).screen.lockOrientation(<OrientationLockType | OrientationLockType[]>orientation);
                    }
                }

                if (typeof DTDevices === 'object') {
                    DTDevices.barcodeSetScanBeep(true, [500,50]);
                    DTDevices.startListening();
                    DTDevices.registerListener('barcodeData', 'barcodeData_applicationjs', function (barcode, barcodeType) {
                        program.setAudioMode('DTDevices');
                        me.onBarcodeData(barcode);
                    });
                    if (typeof localStorage.getItem('barcodeScanMode') === 'undefined') {
                        localStorage.setItem('barcodeScanMode', 'MODE_SINGLE_SCAN');
                    }
                    DTDevices.barcodeSetScanMode(localStorage.getItem('barcodeScanMode'));

                    //set the connection state when it changes
                    DTDevices.registerListener('connectionState', 'connectionState_applicationjs', function (connectionState) {
                        if (connectionState === 'CONNECTED') {
                            program.setScanMode('DTDevices');
                        } else {
                            program.setScanMode('none');
                        }
                        me.setDeviceConnectionState(connectionState);
                    });
                    // since the initial connection state event usually happens early in the page life cycle, we need to explicity query the value the first time
                    me.updateConnectionState();
                }

                if (typeof TslReader === 'object') {
                    TslReader.startListening();
                    //TslReader.registerListener('epcsReceived', 'epcsReceived_applicationjs', function(epcs) {
                    //    me.onBarcodeData(epcs);
                    //});
                    TslReader.registerListener('barcodeReceived', 'barcodeReceived_applicationjs', function(barcode) {
                        me.onBarcodeData(barcode);
                    });
                    TslReader.registerListener('deviceConnected', 'deviceConnected_applicationjs', function() {
                        RwRFID.isConnected = true;
                    });
                    TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_applicationjs', function() {
                        RwRFID.isConnected = false;
                        program.setScanMode('none');
                    });
                    TslReader.connectDevice(function connectDeviceSuccess(isConnected) {
                        RwRFID.isConnected = isConnected;
                    });
                }

                //if (typeof window.Zebra2DScanner === 'object') {
                    //window.Zebra2DScanner.startListening();
                    //window.Zebra2DScanner.registerListener('sbtEventBarcode', 'barcodeReceived_applicationjs', function(barcode, barcodetype) {
                    //    me.onBarcodeData(barcode);
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

                setTimeout(function() {
                    me.loadApplication();
                }, 0);
            }, false);
        }
    };
    //---------------------------------------------------------------------------------
    setScanTarget(selector: string) {
        this.activeTextBox = selector;
        sessionStorage.setItem('scanTarget', selector);
        jQuery('.textbox').removeClass('scanTarget');
        jQuery(this.activeTextBox).addClass('scanTarget');
    };
    //---------------------------------------------------------------------------------
    navigate(path: string) {
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
                        screen = RwAccountController.getPreferencesScreen();
                        break;
                    case 'account/privacypolicy':
                        screen = RwAccountController.getPrivacyPolicyScreen({}, {});
                        break;
                    case 'account/support':
                        screen = RwAccountController.getSupportScreen({}, {});
                        break;
                    case 'home/home':
                        //this.screens = [];
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
                            moduleType: RwConstants.moduleTypes.SubReturn,
                            activityType: RwConstants.activityTypes.SubReturn
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
                    case 'physicalinventory':
                        screen = PhysicalInventory.gePhysicalInventoryScreen({}, {
                            moduleType: RwConstants.moduleTypes.PhyInv
                        });
                        break;
                    case 'inventory/qc':
                        screen = QC.getQCScreen({}, {});
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
                        screen = TimeLog.getModuleScreen();
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
                        screen = AssignItems.getMenuScreen();
                        break;
                    case 'assignitems/newitems':
                        screen = AssignItems.getNewItemsScreen();
                        break;
                    case 'assignitems/existingitems':
                        screen = AssignItems.getExistingItemsScreen();
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
    loadApplication() {
        var me: Program;
        me = this;
        if (!me.didLoadApplication) {
            me.didLoadApplication = true;
            jQuery('#index-loading').hide();
            jQuery('#index-loadingInner').hide();
            me.load();
            jQuery('html').addClass('mobile');
            jQuery('html').addClass('theme-material');
            if (sessionStorage.getItem('sessionLock') === 'true') {
                sessionStorage.setItem('sessionLock', 'false');
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
                if ((<string>jQuery(this).val()).length === 0) {
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

                setTimeout(function () { }, 100);
            });
        }
    };
    //---------------------------------------------------------------------------------
    setDeviceConnectionState(connectionState: string) {
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
    updateConnectionState() { // Update the connection state without waiting for a connection state change callback
        var me: Program;
        me = this;
        try {
            if ((typeof DTDevices === 'object') && (typeof DTDevices.getDeviceConnectionState === 'function')) {
                DTDevices.getDeviceConnectionState(
                    function(connectionState) {
                        try {
                            me.setDeviceConnectionState(connectionState);
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    },
                    function() {
                        try {
                            me.setDeviceConnectionState('DISCONNECTED');
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
}
//---------------------------------------------------------------------------------
jQuery(function () {
    program = new Program();
    application = program;
});