var program, ScannerDevice, LineaScanner;
Program.prototype = new FwApplication;
Program.constructor = Program;
//---------------------------------------------------------------------------------
function Program() { FwApplication.call(this);
    var me;
    
    me = this;
    FwApplicationTree.currentApplicationId = '0A5F2584-D239-480F-8312-7C2B552A30BA';
    //me.audioMode = '';
    //me.audioSuccessArray = [1200, 300];
    //me.audioErrorArray   = [800, 200, 600, 200];
    //if (typeof window.Audio !== 'undefined') {
    //    try {
    //        me.audioMode = 'html5';
    //        me.audioSuccess = new Audio('audio/success.wav');
    //        me.audioSuccess.load();
    //        me.audioError = new Audio('audio/error1.wav');
    //        me.audioError.load();
    //    } catch (error) {

    //    }
    //}
    //jQuery('body')
    //    .on('touchstart', '.fwmenu-item', 
    //        function() {
    //            jQuery(this).addClass('active');
    //        }
    //    )
    //    .on('touchstart', '.button.default', 
    //        function() {
    //            jQuery(this).addClass('active');
    //        }
    //    )
    //    .on('touchend', '.fwmenu-item', 
    //        function() {
    //            jQuery(this).removeClass('active');
    //        }
    //    )
    //    .on('touchend', '.button.default', 
    //        function() {
    //            jQuery(this).removeClass('active');
    //        }
    //    )
    //;
}
//---------------------------------------------------------------------------------
Program.prototype.getApplicationOptions = function() {
    return JSON.parse(sessionStorage.getItem('applicationOptions'));
};
//---------------------------------------------------------------------------------
//Program.prototype.playStatus = function (isSuccessful) {
//    var me;
//    me = this;
//    if (isSuccessful) {
//        switch(me.audioMode) {
//            case 'LineaBrowser':  LineaBrowser.playSound(100, me.audioSuccessArray); break;
//            case 'ScannerDevice': LineaScanner.playSound(me.audioSuccessArray); break;
//            case 'html5':
//                me.audioSuccess.currentTime = 0;
//                me.audioSuccess.play(); 
//                break;
//        }
//    } else {
//        switch(me.audioMode) {
//            case 'LineaBrowser':  LineaBrowser.playSound(100, me.audioErrorArray); break;
//            case 'ScannerDevice': LineaScanner.playSound(me.audioErrorArray); break;
//            case 'html5':
//                me.audioError.currentTime = 0;
//                me.audioError.play(); 
//                break;
//        }
//    }
//};
//---------------------------------------------------------------------------------
Program.prototype.modules = [
    //Home Modules
    { urlpattern: /^module\/quote$/,                  getScreen: function() { return RwQuoteController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/order$/,                  getScreen: function() { return RwOrderController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/customer$/,               getScreen: function() { return RwCustomerController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/deal$/,                   getScreen: function() { return RwDealController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vendor$/,                 getScreen: function() { return RwVendorController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/contact$/,                getScreen: function() { return RwContactController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/rentalinventory$/,        getScreen: function() { return RwRentalInventoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/salesinventory$/,         getScreen: function() { return RwSalesInventoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/barcodeditems$/,          getScreen: function() { return RwBarCodedItemsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/serialitems$/,            getScreen: function() { return RwSerialItemsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/repairorder$/,            getScreen: function() { return RwRepairOrderController.getModuleScreen({}, {}); } }
    //Settings Modules
  , { urlpattern: /^module\/customersettings/,        getScreen: function() { return RwCustomerSettingsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/ordertype$/,              getScreen: function() { return RwOrderTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/usersettings/,            getScreen: function() { return RwUserSettingsController.getModuleScreen({}, {}); } }
    //Reports
  , { urlpattern: /^module\/dealoutstanding/,         getScreen: function() { return RwDealOutstandingController.getModuleScreen({}, {}); } }
    //Utilities Modules
  , { urlpattern: /^module\/chargeprocessing/,        getScreen: function() { return RwChargeProcessingController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/receiptprocessing/,       getScreen: function() { return RwReceiptProcessingController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vendorinvoiceprocessing/, getScreen: function() { return RwVendorInvoiceProcessingController.getModuleScreen({}, {}); } }
    //Administrator
  , { urlpattern: /^module\/group/,                   getScreen: function() { return RwGroupController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/integration/,             getScreen: function() { return RwIntegrationController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/user/,                    getScreen: function() { return RwUserController.getModuleScreen({}, {}); } }
    //Exports
  , { urlpattern: /^module\/example/,                 getScreen: function() { return RwExampleController.getModuleScreen({}, {}); } }
];
//---------------------------------------------------------------------------------
Program.prototype.routes = [
    {
        urlpattern: /^home$/
      , getScreen: function() {
            program.screens = [];
            if (FwAppData.verifyHasAuthToken()) {
                return RwHomeController.getHomeScreen({}, {}); 
            } else {
                program.navigate('default');
            }
        }
    }
  , {
        urlpattern: /^default/
      , getScreen: function() { 
            return RwBaseController.getDefaultScreen({}, {}); 
        }
    }
  , {
        urlpattern: /^login$/
      , getScreen: function() {
            if (!sessionStorage.getItem('authToken')) {
                return RwBaseController.getLoginScreen({}, {});
            } else {
                program.navigate('home');
                return null;
            }
        }
    }
  , {
        urlpattern: /^about/
      , getScreen: function() { 
            return RwBaseController.getAboutScreen({}, {}); 
        }
    }
  , {
        urlpattern: /^support/
      , getScreen: function() { 
            return RwBaseController.getSupportScreen({}, {}); 
        }
    }
  , {
        urlpattern: /^recoverpassword/
      , getScreen: function() { 
            return RwBaseController.getPasswordRecoveryScreen({}, {}); 
        }
    }
  , {
        urlpattern: /^logoff$/
      , getScreen: function() {
            sessionStorage.clear();
            location.reload(false);
            //program.navigate('default');
            return null;
        }
    }
];
//---------------------------------------------------------------------------------
Program.prototype.navigateHashChange = function(path) {
    var me, screen, match;
    me = this;
    //sessionStorage.setItem('activePath', path);
    path = path.toLowerCase();
    
    for (var i = 0; i < program.routes.length; i++) {
        match = program.routes[i].urlpattern.exec(path);
        if (match != null) {
            screen = program.routes[i].getScreen(match);
            break;
        }
    }

    if (screen != null) {
        me.updateScreen(screen);
    }
};
//---------------------------------------------------------------------------------
Program.prototype.navigate = function(path) {
    var me, screen;
    me = this;
    //sessionStorage.setItem('activePath', path);
    path = path.toLowerCase();
    if (window.location.hash.replace('#/','') !== path) {
        window.location.hash = '/' + path;
    } else {
        program.navigateHashChange(path);
    }
};
//---------------------------------------------------------------------------------
Program.prototype.getModule = function(path) {
    var screen, match, $bodyContainer, $modifiedForms, $form, $tab;

    $bodyContainer = jQuery('#master-body');
    $modifiedForms = $bodyContainer.find('div[data-type="form"][data-modified="true"]');
    path           = path.toLowerCase();
    if ($modifiedForms.length > 0) {
        $form = jQuery($modifiedForms[0]);
        $tab  = jQuery('#' + $form.parent().attr('data-tabid'));
        $tab.click();
        FwModule.closeForm($form, $tab, path);
    } else {
        if (window.location.hash.replace('#/','') !== path) {
            for (var i = 0; i < program.modules.length; i++) {
                match = program.modules[i].urlpattern.exec(path);
                if (match != null) {
                    screen = program.modules[i].getScreen(match);
                    break;
                }
            }
            if (screen != null) {
                window.location.hash = '/' + path;
                if (this.screens.length > 0) {
                    if (typeof this.screens[this.screens.length - 1].unload !== 'undefined') {
                        this.screens[this.screens.length - 1].unload();
                    }
                    this.screens = [];
                }
                FwPopup.destroy(jQuery('.FwPopup-divPopup,.FwPopup-divOverlay')); //Write something better
                $bodyContainer
                    .empty() // added this to get rid of the homepages doubling up in the support site.  Not sure about this line though, probably needs to get fixed in a better way.  MV 10/4/13
                    .append(screen.$view);
                this.screens.push(screen);
                if (typeof screen.load !== 'undefined') {
                    screen.load();
                }
                document.body.scrollTop = 0;
            }
        }
    }
};
//---------------------------------------------------------------------------------
Program.prototype.loadDefaultPage = function() {
    var me = this;
    if (sessionStorage.getItem('authToken')) {
        me.navigate('home');
    } else {
        me.navigate('default');
    }
};
//---------------------------------------------------------------------------------
jQuery(function() {
    //jQuery('#index-loading')
    //    .hide()
    //    .on('click', function() {
    //        jQuery('#index-loadingInner').stop().show();
    //        if (confirm(SuLanguages.translate('Cancel Request') + '?') ) {
    //            if (SuAppData.jqXHR) {
    //                SuAppData.jqXHR.abort();
    //            }
    //            jQuery('#index-loading').hide();
    //        }
    //    })
    //;
    //jQuery('#index-loadingInner')
    //    .hide()
    //;
    program = new Program();
    program.load();
    program.loadDefaultPage();
});
//---------------------------------------------------------------------------------
window.onhashchange = function() {
    program.navigateHashChange(window.location.hash.replace('#/',''));
};
//---------------------------------------------------------------------------------

