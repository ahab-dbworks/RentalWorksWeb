//var program, ScannerDevice, LineaScanner;
class Program extends FwApplication {
    RENTALWORKS = 'RentalWorks';
    TRAKITWORKS = 'TrakitWorks';
    name = this.TRAKITWORKS;
    //---------------------------------------------------------------------------------
    constructor() {
        super();
        FwApplicationTree.currentApplicationId = 'D901DE93-EC22-45A1-BB4A-DD282CAF59FB';
    }
}
//---------------------------------------------------------------------------------
var program: Program = new Program();
//---------------------------------------------------------------------------------
jQuery(function () {
    program.load();
    program.loadDefaultPage();
});
//---------------------------------------------------------------------------------
//Home Modules

//Settings Modules

//Reports

//Utilities Modules

//Administrator
routes.push({ pattern: /^module\/settings$/, action: function (match: RegExpExecArray) { return SettingsController.getModuleScreen(); } });
SettingsController.id = 'AD8656B4-F161-4568-9AFF-64C81A3680E6';
SettingsController.settingsMenuId = 'CA7EDF90-F08A-4E5C-BA6B-87DB6A14D485';

//---------------------------------------------------------------------------------
routes.push({
    pattern: /^home$/,
    action: function (match: RegExpExecArray) {
        program.screens = [];
        if (FwAppData.verifyHasAuthToken()) {
            return HomeController.getHomeScreen();
        } else {
            program.navigate('default');
        }
    }
});
routes.push({
    pattern: /^default/,
    action: function (match: RegExpExecArray) {
        return BaseController.getDefaultScreen();
    }
});
routes.push({
    pattern: /^login$/,
    action: function (match: RegExpExecArray) {
        if (!sessionStorage.getItem('authToken')) {
            return BaseController.getLoginScreen();
        } else {
            program.navigate('home');
            return null;
        }
    }
});
//routes.push({
//    pattern: /^about/,
//    action: function (match: RegExpExecArray) {
//        return BaseController.getAboutScreen();
//    }
//});
//routes.push({
//    pattern: /^support/,
//    action: function (match: RegExpExecArray) {
//        return BaseController.getSupportScreen();
//    }
//});
//routes.push({
//    pattern: /^recoverpassword/,
//    action: function (match: RegExpExecArray) {
//        return BaseController.getPasswordRecoveryScreen();
//    }
//});
routes.push({
    pattern: /^logoff$/,
    action: function (match: RegExpExecArray) {
        sessionStorage.clear();
        location.reload(false);
        return null;
    }
});
