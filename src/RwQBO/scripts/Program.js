var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var Program = /** @class */ (function (_super) {
    __extends(Program, _super);
    //---------------------------------------------------------------------------------
    function Program() {
        var _this = _super.call(this) || this;
        var me;
        me = _this;
        me.name = 'RentalWorks';
        FwApplicationTree.currentApplicationId = '0A5F2584-D239-480F-8312-7C2B552A30BA';
        return _this;
    }
    return Program;
}(FwApplication));
//---------------------------------------------------------------------------------
var program = new Program();
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
    }
    else {
        start();
    }
});
//---------------------------------------------------------------------------------
//---------------------------------------------------------------------------------
//Home Modules
//Settings Modules
routes.push({ pattern: /^module\/customersettings/, action: function (match) { return RwCustomerSettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/ordertype$/, action: function (match) { return OrderTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/usersettings/, action: function (match) { return UserSettingsController.getModuleScreen(); } });
//Reports
routes.push({ pattern: /^module\/dealoutstanding/, action: function (match) { return RwDealOutstandingController.getModuleScreen(); } });
//Utilities Modules
routes.push({ pattern: /^module\/chargeprocessing/, action: function (match) { return RwChargeProcessingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receiptprocessing/, action: function (match) { return RwReceiptProcessingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorinvoiceprocessing/, action: function (match) { return RwVendorInvoiceProcessingController.getModuleScreen(); } });
//Administrator
routes.push({ pattern: /^module\/group/, action: function (match) { return GroupController.getModuleScreen(); } });
routes.push({ pattern: /^module\/integration/, action: function (match) { return RwIntegrationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/user/, action: function (match) { return UserController.getModuleScreen(); } });
//Exports                                             
routes.push({ pattern: /^module\/example/, action: function (match) { return RwExampleController.getModuleScreen(); } });
//---------------------------------------------------------------------------------
routes.push({
    pattern: /^home$/,
    action: function (match) {
        program.screens = [];
        if (FwAppData.verifyHasAuthToken()) {
            return RwHomeController.getHomeScreen();
        }
        else {
            program.navigate('default');
        }
    }
});
routes.push({
    pattern: /^default/,
    action: function (match) {
        return RwBaseController.getDefaultScreen();
    }
});
routes.push({
    pattern: /^login$/,
    action: function (match) {
        if (!sessionStorage.getItem('authToken')) {
            return RwBaseController.getLoginScreen();
        }
        else {
            program.navigate('home');
            return null;
        }
    }
});
routes.push({
    pattern: /^about/,
    action: function (match) {
        return RwBaseController.getAboutScreen();
    }
});
routes.push({
    pattern: /^support/,
    action: function (match) {
        return RwBaseController.getSupportScreen();
    }
});
routes.push({
    pattern: /^recoverpassword/,
    action: function (match) {
        return RwBaseController.getPasswordRecoveryScreen();
    }
});
routes.push({
    pattern: /^logoff$/,
    action: function (match) {
        sessionStorage.clear();
        location.reload(false);
        return null;
    }
});
//# sourceMappingURL=Program.js.map