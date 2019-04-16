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
// Home Modules
//---------------------------------------------------------------------------------

//---------------------------------------------------------------------------------
// Settings Modules
//---------------------------------------------------------------------------------
// Address Settings
routes.push({ pattern: /^module\/country$/, action: function (match: RegExpExecArray) { return CountryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/state$/, action: function (match: RegExpExecArray) { return StateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/department$/, action: function (match: RegExpExecArray) { return DepartmentController.getModuleScreen(); } });

// Contact Settings
routes.push({ pattern: /^module\/contacttitle$/, action: function (match: RegExpExecArray) { return ContactTitleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/currency$/, action: function (match: RegExpExecArray) { return CurrencyController.getModuleScreen(); } });

// Deal Settings
routes.push({ pattern: /^module\/dealclassification$/, action: function (match: RegExpExecArray) { return DealClassificationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealtype$/, action: function (match: RegExpExecArray) { return DealTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealstatus$/, action: function (match: RegExpExecArray) { return DealStatusController.getModuleScreen(); } });

// Inventory Settings
routes.push({ pattern: /^module\/attribute$/, action: function (match: RegExpExecArray) { return AttributeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorycondition$/, action: function (match: RegExpExecArray) { return InventoryConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventoryrank$/, action: function (match: RegExpExecArray) { return InventoryRankController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorytype$/, action: function (match: RegExpExecArray) { return InventoryTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalcategory$/, action: function (match: RegExpExecArray) { return RentalCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/unit$/, action: function (match: RegExpExecArray) { return UnitController.getModuleScreen(); } });

routes.push({ pattern: /^module\/officelocation$/, action: function (match: RegExpExecArray) { return OfficeLocationController.getModuleScreen(); } });

// Order Settings
routes.push({ pattern: /^module\/ordertype$/, action: function (match: RegExpExecArray) { return OrderTypeController.getModuleScreen(); } });

// PO Settings
routes.push({ pattern: /^module\/poclassification$/, action: function (match: RegExpExecArray) { return POClassificationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/potype$/, action: function (match: RegExpExecArray) { return POTypeController.getModuleScreen(); } });

routes.push({ pattern: /^module\/repairitemstatus$/, action: function (match: RegExpExecArray) { return RepairItemStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/shipvia$/, action: function (match: RegExpExecArray) { return ShipViaController.getModuleScreen(); } });

// Vendor Settings
routes.push({ pattern: /^module\/organizationtype$/, action: function (match: RegExpExecArray) { return OrganizationTypeController.getModuleScreen(); } });

routes.push({ pattern: /^module\/warehouse$/, action: function (match: RegExpExecArray) { return WarehouseController.getModuleScreen(); } });
//---------------------------------------------------------------------------------
// Reports
//---------------------------------------------------------------------------------

//---------------------------------------------------------------------------------
// Utilities Modules
//---------------------------------------------------------------------------------

//---------------------------------------------------------------------------------
// Administrator
//---------------------------------------------------------------------------------
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
