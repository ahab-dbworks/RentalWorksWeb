class Program extends FwApplication {
    //---------------------------------------------------------------------------------
    constructor() {
        super();
        var me: Program;
        me                                     = this;
        me.name                                = 'RentalWorks';
        FwApplicationTree.currentApplicationId = '0A5F2584-D239-480F-8312-7C2B552A30BA';
    }
}
//---------------------------------------------------------------------------------
var program: Program = new Program();
//---------------------------------------------------------------------------------
jQuery(function () {
    function start() {
        program.load();
        program.loadDefaultPage();
    }
    if (applicationConfig.debugMode) {
        setTimeout(function () {
            start();
        }, 1000);
    } else {
        start();
    }
});
//---------------------------------------------------------------------------------
//---------------------------------------------------------------------------------
//Home Modules

//Settings Modules
routes.push({ pattern: /^module\/customersettings/, action: function (match: RegExpExecArray) { return RwCustomerSettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/ordertype$/, action: function (match: RegExpExecArray) { return OrderTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/usersettings/, action: function (match: RegExpExecArray) { return UserSettingsController.getModuleScreen(); } });
//Reports
routes.push({ pattern: /^module\/dealoutstanding/, action: function (match: RegExpExecArray) { return RwDealOutstandingController.getModuleScreen(); } });
//Utilities Modules
routes.push({ pattern: /^module\/chargeprocessing/, action: function (match: RegExpExecArray) { return RwChargeProcessingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receiptprocessing/, action: function (match: RegExpExecArray) { return RwReceiptProcessingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorinvoiceprocessing/, action: function (match: RegExpExecArray) { return RwVendorInvoiceProcessingController.getModuleScreen(); } });
//Administrator
routes.push({ pattern: /^module\/group/, action: function (match: RegExpExecArray) { return GroupController.getModuleScreen(); } });
routes.push({ pattern: /^module\/integration/, action: function (match: RegExpExecArray) { return RwIntegrationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/user/, action: function (match: RegExpExecArray) { return UserController.getModuleScreen(); } });
//Exports                                             
routes.push({ pattern: /^module\/example/, action: function (match: RegExpExecArray) { return RwExampleController.getModuleScreen(); } });
//---------------------------------------------------------------------------------
routes.push({
    pattern: /^home$/,
    action: function (match: RegExpExecArray) {
        program.screens = [];
        if (FwAppData.verifyHasAuthToken()) {
            return RwHomeController.getHomeScreen();
        } else {
            program.navigate('default');
        }
    }
});
routes.push({
    pattern: /^default/,
    action: function (match: RegExpExecArray) {
        return RwBaseController.getDefaultScreen();
    }
});
routes.push({
    pattern: /^login$/,
    action: function (match: RegExpExecArray) {
        if (!sessionStorage.getItem('authToken')) {
            return RwBaseController.getLoginScreen();
        } else {
            program.navigate('home');
            return null;
        }
    }
});
routes.push({
    pattern: /^about/,
    action: function (match: RegExpExecArray) {
        return RwBaseController.getAboutScreen();
    }
});
routes.push({
    pattern: /^support/,
    action: function (match: RegExpExecArray) {
        return RwBaseController.getSupportScreen();
    }
});
routes.push({
    pattern: /^recoverpassword/,
    action: function (match: RegExpExecArray) {
        return RwBaseController.getPasswordRecoveryScreen();
    }
});
routes.push({
    pattern: /^logoff$/,
    action: function (match: RegExpExecArray) {
        sessionStorage.clear();
        location.reload(false);
        return null;
    }
});
