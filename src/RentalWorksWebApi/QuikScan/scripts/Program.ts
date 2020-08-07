var program: Program;
var application: Program;
//---------------------------------------------------------------------------------
class Program extends FwApplication {
    activeTextBox:      string;
    activeNearfieldTextBox: string;
    htmlname:           string;
    didLoadApplication: boolean;
    onBarcodeData:      (data: string, type?: string) => void;
    onScanBarcode:      (data: string, type?: string) => void;
    onLpNearfieldData:      (uid: string, uidType?: string) => void;
    onScanLpNearfield:      (uid: string, uidType?: string) => void;
    localstorageprefix: string;
    localstorageitems:  any;
    runningInCordova: boolean = false;
    browserVersionString: string = '';
    browserVersionMajor: number = 0;
    browserVersionMinor: number = 0;
    browserVersionRevision: number = 0;
    browserVersionBuild: number = 0;
    online: boolean = true;
    lineaPro_BatteryStatus_Level: string = '---%';
    lineaPro_BatteryStatus_IsPlugged: boolean = false;
    lineaPro_BatteryStatus_Status: 'unknown' | 'critical' | 'low' | 'ok' = 'unknown';
    showRfidStatusIcon: boolean = false;
    hasHfRfidApplicationOption: boolean = true;
    hasCamera: boolean = false;
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
        me.onBarcodeData = function (barcode, barcodeType) {
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

        me.setScanTargetLpNearfield('');
        me.onLpNearfieldData = function(uid: string, uidType: string) {
            if (typeof me.onScanLpNearfield === 'function') {
                me.onScanLpNearfield(uid, uidType);
            } else {
                var scanTargetLpNearfield = sessionStorage.getItem('scanTargetLpNearfield');
                var $scantarget = jQuery(scanTargetLpNearfield);
                if ((typeof me.activeNearfieldTextBox === 'undefined') || ($scantarget.length === 0)) {
                    // do nothing
                } else {
                    const updateTextbox = function($scantarget: JQuery, scanTargetLpNearfieldm, code: string, uidType: string) {
                        if ($scantarget.hasClass('fwformfield')) {
                            FwFormField.setValue(jQuery('html'), scanTargetLpNearfield, code, '', true);
                        } else {
                            $scantarget.val(code).change();
                        }
                    };
                    if (sessionStorage.getItem('scanTargetLpNearfieldReplaceWithBC') === null) {
                        sessionStorage.setItem('scanTargetLpNearfieldReplaceWithBC', 'true');
                    }
                    if (sessionStorage.getItem('scanTargetLpNearfieldReplaceWithBC') === 'true') {
                        RwServices.inventory.getBarcodeFromRfid({ rfid: uid }, (response: any) => {
                            updateTextbox($scantarget, scanTargetLpNearfield, response.barcode, uidType);
                        });
                    } else {
                        updateTextbox($scantarget, scanTargetLpNearfield, uid, uidType);
                    }
                    
                }
            }
        };

        setTimeout(function() {
            me.loadApplication();
        }, 2000);
        if (typeof document.addEventListener !== 'undefined') {
            document.addEventListener('deviceready', () => {
                //FwNotification.renderNotification('INFO', 'Device Ready');
                me.runningInCordova = true;
                document.addEventListener("offline", function () {
                    program.online = false;
                    FwMobileMasterController.generateDeviceStatusIcons();
                    //FwNotification.renderNotification('ERROR', 'Network Disconnected');
                }, false);
                document.addEventListener("online", function () {
                    program.online = true;
                    FwMobileMasterController.generateDeviceStatusIcons();
                    //FwNotification.renderNotification('SUCCESS', 'Network Connected');
                }, false);

                // Patches a bug in Android RentalWorks 2018.1.3 where it didn't check if the TslReader plugin exists, which it doesn't in that version, 
                // but does in later versions.  DwCordovaFunc.getBrowserVersion will fail without this hack unless the user has a later version of the Android app.
                if (typeof window.TslReader !== 'object') {
                    window.TslReader = {
                        debug: false
                    };
                }
                DwCordovaFunc.getBrowserVersion((args: Array<any>) => {
                    var versionString = args[0];
                    let version = versionString.split('.');
                    let major: number = parseInt(version[0]);
                    let minor: number = parseInt(version[1]);
                    let revision: number = parseInt(version[2]);
                    let build: number = parseInt(version[3]);
                    program.browserVersionString = versionString;
                    program.browserVersionMajor = major;
                    program.browserVersionMinor = minor;
                    program.browserVersionRevision = revision;
                    program.browserVersionBuild = build;

                    //fires when the device's battery percentage changes
                    window.addEventListener("batterystatus", (status) => {
                        let batteryStatus: BatteryStatusEvent = <any>status;
                        program.lineaPro_BatteryStatus_Level = batteryStatus.level + '%';
                        program.lineaPro_BatteryStatus_IsPlugged = batteryStatus.isPlugged;
                        program.lineaPro_BatteryStatus_Status = batteryStatus.status;
                    }, false);

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

                    // for backwards compatibility we'll set hasCamera to true when app is running in Cordova
                    this.hasCamera = true;
                    if (DwCordovaFunc !== undefined && DwCordovaFunc.getHasCamera !== undefined) {
                        DwCordovaFunc.getHasCamera((args: Array<any>) => {
                            this.hasCamera = args[0];
                        });
                    }

                    if (typeof DTDevices === 'object') {
                        DTDevices.barcodeSetScanBeep(true, [500,50]);
                        DTDevices.startListening();
                        DTDevices.registerListener('barcodeData', 'barcodeData_applicationjs', (barcode, barcodeType) => {
                            program.setAudioMode('DTDevices');
                            me.onBarcodeData(barcode);
                        });
                        if (typeof localStorage.getItem('barcodeScanMode') === 'undefined') {
                            localStorage.setItem('barcodeScanMode', 'MODE_SINGLE_SCAN');
                        } else if (localStorage.getItem('barcodeScanMode') !== 'MODE_SINGLE_SCAN' && localStorage.getItem('barcodeScanMode') !== 'MODE_MULTI_SCAN_NO_DUPLICATES') {
                            if (localStorage.getItem('barcodeScanMode') === 'MODE_MULTI_SCAN') {
                                localStorage.setItem('barcodeScanMode', 'MODE_MULTI_SCAN_NO_DUPLICATES');
                            } else {
                                localStorage.setItem('barcodeScanMode', 'MODE_SINGLE_SCAN');
                            }
                        }
                        DTDevices.barcodeSetScanMode(localStorage.getItem('barcodeScanMode'));

                        // set event handlers for nearfield scanner
                        NearfieldRfidScanner.init((uid: string, uidType: string) => {
                            try {
                                program.setAudioMode('DTDevices');
                                this.onLpNearfieldData(uid, uidType);
                                //FwNotification.renderNotification('INFO', `UID: ${uid}. UID Type: ${uidType}`);
                            } catch (ex) {
                                FwFunc.showError(ex);
                            }
                        });

                        //set the connection state when it changes
                        DTDevices.registerListener('connectionState', 'connectionState_applicationjs', (connectionState) => {
                            if (connectionState === 'CONNECTED') {
                                program.setScanMode('DTDevices');
                            } else {
                                program.setScanMode('NativeAudio');
                            }
                            me.setDeviceConnectionState(connectionState);
                        });
                        // since the initial connection state event usually happens early in the page life cycle, we need to explicity query the value the first time
                        me.updateConnectionState();
                    }

                    if (typeof window.TslReader === 'object') {
                        if (typeof window.TslReader.startListening === 'function') {
                            window.TslReader.startListening();
                            window.TslReader.registerListener('deviceConnected', 'deviceConnected_programts', function () {
                                RwRFID.isTsl = true;
                                RwRFID.isConnected = true;
                                program.showRfidStatusIcon = true;
                                FwMobileMasterController.generateDeviceStatusIcons();
                                //FwNotification.renderNotification('SUCCESS', 'RFID Reader Connected');
                            });
                            window.TslReader.registerListener('deviceDisconnected', 'deviceDisconnected_programts', function () {
                                if ((program.browserVersionMajor > 2018) ||
                                    (program.browserVersionMajor === 2018 && program.browserVersionMinor > 1) ||
                                    (program.browserVersionMajor === 2018 && program.browserVersionMinor === 1 && program.browserVersionRevision > 4) ||
                                    (program.browserVersionMajor === 2018 && program.browserVersionMinor === 1 && program.browserVersionRevision === 4 && program.browserVersionBuild >= 2)) {
                                    //FwNotification.renderNotification('ERROR', 'RFID Reader Disconnected');
                                    RwRFID.isConnected = false;
                                    RwRFID.isTsl = false;
                                } else {
                                    // the TSL plugin was firing this event for any connected device, so this was firing incorrectly when linea was unplugged.
                                    if (RwRFID.isConnected === true) {
                                        //FwNotification.renderNotification('ERROR', 'RFID Reader Disconnected');
                                        RwRFID.isConnected = false;
                                    }
                                }
                                program.showRfidStatusIcon = false;
                                FwMobileMasterController.generateDeviceStatusIcons();
                            });
                            window.TslReader.connectDevice(function connectDeviceSuccess(isConnected) {
                                program.showRfidStatusIcon = true;
                                RwRFID.isConnected = isConnected;
                                FwMobileMasterController.generateDeviceStatusIcons();
                            }, function () {
                                RwRFID.isConnected = false;
                                program.showRfidStatusIcon = false;
                            });
                        } else {
                            delete window.TslReader;
                        }
                    }

                    if (typeof window.ZebraRFIDAPI3 === 'object' && typeof window.ZebraRFIDAPI3.startListening === 'function') {
                        window.ZebraRFIDAPI3.startListening();
                        window.ZebraRFIDAPI3.registerListener('deviceConnected', 'deviceConnected_programts', function () {
                            RwRFID.isRFIDAPI3 = true;
                            RwRFID.isConnected = true;
                            program.showRfidStatusIcon = true;
                            FwMobileMasterController.generateDeviceStatusIcons();
                            //FwNotification.renderNotification('SUCCESS', 'RFID Reader Connected');
                        });
                        window.ZebraRFIDAPI3.registerListener('deviceDisconnected', 'deviceDisconnected_programts', function () {
                            RwRFID.isConnected = false;
                            RwRFID.isRFIDAPI3 = false;
                            program.showRfidStatusIcon = false;
                            FwMobileMasterController.generateDeviceStatusIcons();
                        });
                        window.ZebraRFIDAPI3.connectDevice(
                            function success(isConnected) {
                                program.showRfidStatusIcon = true;
                                RwRFID.isConnected = isConnected;
                                RwRFID.isRFIDAPI3 = isConnected;
                                FwMobileMasterController.generateDeviceStatusIcons();
                            },
                            function error() {
                                RwRFID.isConnected = false;
                                RwRFID.isRFIDAPI3 = false;
                                program.showRfidStatusIcon = false;
                            }
                        );
                    }

                    RwRFID.registerBarcodeEvents();

                    setTimeout(function() {
                        me.loadApplication();
                    }, 0);
                });
            }, false);
        }
    };
    //---------------------------------------------------------------------------------
    setScanTarget(selector: string) {
        this.activeTextBox = selector;
        sessionStorage.setItem('scanTarget', selector);
        jQuery('input[type="text"]').removeClass('scanTarget');
        jQuery(this.activeTextBox).addClass('scanTarget');
    };
    //---------------------------------------------------------------------------------
    setScanTargetLpNearfield(selector: string, replaceWithBC?: boolean) {
        this.activeNearfieldTextBox = selector;
        sessionStorage.setItem('scanTargetLpNearfield', selector);
        if (replaceWithBC === undefined) {
            replaceWithBC = true;
        }
        sessionStorage.setItem('scanTargetLpNearfieldReplaceWithBC', replaceWithBC.toString());
        jQuery('input[type="text"]').removeClass('scanTargetLpNearfield');
        jQuery(this.activeNearfieldTextBox).addClass('scanTargetLpNearfield');
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
                    case 'login':
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
                        screen = StagingController.getStagingScreen({}, {
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
                        screen = StagingController.getStagingScreen({}, {
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
                    case 'order/quikin':
                        screen = QuikIn.getModuleScreen({}, {});
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
                        screen = StagingController.getStagingScreen({}, {
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
                    case 'logoff':
                        FwApplicationTree.tree = null;
                        sessionStorage.clear();
                        me.screens = [];
                        me.navigate('login');
                        return;
                    case 'quikpick':
                        screen = QuikPick.getQuikPickScreen();
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
                me.navigate('logoff');
            } else if (sessionStorage.getItem('authToken')) {
                me.navigate('home/home');
            } else {
                me.navigate('login');
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
        this.setAudioMode('NativeAudio');
    };
    //---------------------------------------------------------------------------------
    setDeviceConnectionState(connectionState: string) {
        NearfieldRfidScanner.disable();
        const oldConnectionState = jQuery('html').attr('connectionstate');
        if (typeof oldConnectionState === 'undefined' || oldConnectionState !== connectionState) {
            if (connectionState == 'CONNECTED') {
                jQuery('#master-footer-connectionstate').text('CONNECTED').show();
                setTimeout(function() {
                    NearfieldRfidScanner.enable();
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
//---------------------------------------------------------------------------------
interface BatteryStatusEvent{
    isPlugged: boolean;
    level: number;
    status: 'low' | 'critical' | 'ok';
}