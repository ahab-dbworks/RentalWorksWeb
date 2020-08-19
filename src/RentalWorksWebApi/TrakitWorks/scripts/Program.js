class Program extends FwApplication {
    constructor() {
        super();
        this.name = Constants.appCaption;
        this.title = Constants.appTitle;
        FwApplicationTree.currentApplicationId = Constants.appId;
    }
}
var program = new Program();
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
            } while ($templates.length > 0);
        }
        else {
            loadApp();
        }
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
routes.push({
    pattern: /^home$/,
    action: function (match) {
        program.screens = [];
        if (FwAppData.verifyHasAuthToken()) {
            return HomeController.getHomeScreen();
        }
        else {
            program.navigate('default');
        }
    }
});
routes.push({
    pattern: /^default/,
    action: function (match) {
        return BaseController.getDefaultScreen();
    }
});
routes.push({
    pattern: /^login$/,
    action: function (match) {
        if (!FwAppData.verifyHasAuthToken()) {
            return BaseController.getLoginScreen();
        }
        else {
            program.navigate('home');
            return null;
        }
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
routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match) { return AssignBarCodesController.getModuleScreen(); } });
routes.push({ pattern: /^module\/checkin$/, action: function (match) { return CheckInController.getModuleScreen(); } });
routes.push({ pattern: /^module\/checkout$/, action: function (match) { return StagingCheckoutController.getModuleScreen(); } });
routes.push({ pattern: /^module\/exchange$/, action: function (match) { return ExchangeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/orderstatus$/, action: function (match) { return OrderStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match) { return ReceiveFromVendorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalinventory$/, action: function (match) { return InventoryItemController.getModuleScreen(); } });
routes.push({ pattern: /^module\/returntovendor$/, action: function (match) { return ReturnToVendorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/country$/, action: function (match) { return CountryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/state$/, action: function (match) { return StateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/department$/, action: function (match) { return DepartmentController.getModuleScreen(); } });
routes.push({ pattern: /^module\/contacttitle$/, action: function (match) { return ContactTitleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/currency$/, action: function (match) { return CurrencyController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealclassification$/, action: function (match) { return DealClassificationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealtype$/, action: function (match) { return DealTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealstatus$/, action: function (match) { return DealStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/attribute$/, action: function (match) { return AttributeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorycondition$/, action: function (match) { return InventoryConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventoryrank$/, action: function (match) { return InventoryRankController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorytype$/, action: function (match) { return InventoryTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalcategory$/, action: function (match) { return RentalCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/unit$/, action: function (match) { return UnitController.getModuleScreen(); } });
routes.push({ pattern: /^module\/officelocation$/, action: function (match) { return OfficeLocationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/ordertype$/, action: function (match) { return OrderTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poclassification$/, action: function (match) { return POClassificationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/potype$/, action: function (match) { return POTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/repairitemstatus$/, action: function (match) { return RepairItemStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/shipvia$/, action: function (match) { return ShipViaController.getModuleScreen(); } });
routes.push({ pattern: /^module\/organizationtype$/, action: function (match) { return OrganizationTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/warehouse$/, action: function (match) { return WarehouseController.getModuleScreen(); } });
routes.push({ pattern: /^module\/group$/, action: function (match) { return GroupController.getModuleScreen(); } });
routes.push({ pattern: /^module\/user$/, action: function (match) { return UserController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customfield$/, action: function (match) { return CustomFieldController.getModuleScreen(); } });
routes.push({ pattern: /^module\/emailhistory$/, action: function (match) { return EmailHistoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/duplicaterules$/, action: function (match) { return DuplicateRuleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/settings$/, action: function (match) { return SettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/reports$/, action: function (match) { return ReportsController.getModuleScreen(); } });
//# sourceMappingURL=Program.js.map