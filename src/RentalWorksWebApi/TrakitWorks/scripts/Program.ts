class Program extends FwApplication {
    name = Constants.appCaption;
    title = Constants.appTitle;
    //---------------------------------------------------------------------------------
    constructor() {
        super();
        //FwApplicationTree.currentApplicationId = 'D901DE93-EC22-45A1-BB4A-DD282CAF59FB';
        FwApplicationTree.currentApplicationId = Constants.appId;


        //ReportsController.id = 'F62D2B01-E4C4-4E97-BFAB-6CF2B872A4E4';
        //ReportsController.reportsMenuId = 'F62D2B01-E4C4-4E97-BFAB-6CF2B872A4E4';

        //SettingsController.id = 'AD8656B4-F161-4568-9AFF-64C81A3680E6';
        //SettingsController.settingsMenuId = 'CA7EDF90-F08A-4E5C-BA6B-87DB6A14D485';
    }
}
//---------------------------------------------------------------------------------
var program: Program = new Program();
//---------------------------------------------------------------------------------
jQuery(function () {
    function start() {
        function loadApp() {
            program.load();
            program.loadCustomFormsAndBrowseViews();
        }
        var $templates = jQuery('script[data-ajaxload="true"]');
        if ($templates.length > 0) {
            do {
                setTimeout(() => {
                    $templates = jQuery('script[data-ajaxload="true"]');
                    if ($templates.length === 0) {
                        loadApp();
                    }
                }, 250);
            } while ($templates.length > 0)
        } else {
            loadApp();
        }
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
        if (!FwAppData.verifyHasAuthToken()) {
            return BaseController.getLoginScreen();
        } else {
            program.navigate('home');
            return null;
        }
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
//---------------------------------------------------------------------------------
// Home Modules
//---------------------------------------------------------------------------------
routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match: RegExpExecArray) { return AssignBarCodesController.getModuleScreen(); } });
routes.push({ pattern: /^module\/checkin$/, action: function (match: RegExpExecArray) { return CheckInController.getModuleScreen(); } });
routes.push({ pattern: /^module\/checkout$/, action: function (match: RegExpExecArray) { return StagingCheckoutController.getModuleScreen(); } });
routes.push({ pattern: /^module\/exchange$/, action: function (match: RegExpExecArray) { return ExchangeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/orderstatus$/, action: function (match: RegExpExecArray) { return OrderStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match: RegExpExecArray) { return ReceiveFromVendorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalinventory$/, action: function(match: RegExpExecArray) { return InventoryItemController.getModuleScreen(); } });
routes.push({ pattern: /^module\/returntovendor$/, action: function (match: RegExpExecArray) { return ReturnToVendorController.getModuleScreen(); } });
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
//routes.push({ pattern: /^module\/control$/, action: function (match: RegExpExecArray) { return ControlController.getModuleScreen(); } });
routes.push({ pattern: /^module\/group$/, action: function (match: RegExpExecArray) { return GroupController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/integration$/, action: function (match: RegExpExecArray) { return IntegrationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/user$/, action: function (match: RegExpExecArray) { return UserController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customfield$/, action: function (match: RegExpExecArray) { return CustomFieldController.getModuleScreen(); } });
routes.push({ pattern: /^module\/emailhistory$/, action: function (match: RegExpExecArray) { return EmailHistoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/duplicaterules$/, action: function (match: RegExpExecArray) { return DuplicateRuleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/settings$/, action: function (match: RegExpExecArray) { return SettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/reports$/, action: function (match: RegExpExecArray) { return ReportsController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/designer$/, action: function (match: RegExpExecArray) { return DesignerController.loadDesigner(); } });
//---------------------------------------------------------------------------------
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

