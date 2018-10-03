class Program extends FwApplication {
    constructor() {
        super();
        var me;
        me = this;
        me.name = 'TrakItWorks';
        FwApplicationTree.currentApplicationId = 'D901DE93-EC22-45A1-BB4A-DD282CAF59FB';
    }
}
var program = new Program();
jQuery(function () {
    program.load();
    program.loadDefaultPage();
});
routes.push({ pattern: /^module\/country$/, action: function (match) { return CountryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/state$/, action: function (match) { return StateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customerstatus$/, action: function (match) { return CustomerStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customertype$/, action: function (match) { return CustomerTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/glaccount$/, action: function (match) { return GlAccountController.getModuleScreen(); } });
routes.push({ pattern: /^module\/billingcycle$/, action: function (match) { return BillingCycleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/paymenttype$/, action: function (match) { return PaymentTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/paymentterms$/, action: function (match) { return PaymentTermsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customersettings/, action: function (match) { return RwCustomerSettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/ordertype$/, action: function (match) { return OrderTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/usersettings/, action: function (match) { return UserSettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorclass$/, action: function (match) { return VendorClassController.getModuleScreen(); } });
routes.push({ pattern: /^module\/warehouse$/, action: function (match) { return WarehouseController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customercategory$/, action: function (match) { return CustomerCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/creditstatus$/, action: function (match) { return CreditStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealtype$/, action: function (match) { return DealTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealstatus$/, action: function (match) { return DealStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/department$/, action: function (match) { return DepartmentController.getModuleScreen(); } });
routes.push({ pattern: /^module\/productiontype$/, action: function (match) { return ProductionTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/officelocation$/, action: function (match) { return OfficeLocationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/documenttype$/, action: function (match) { return DocumentTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/setsurface$/, action: function (match) { return SetSurfaceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/building$/, action: function (match) { return BuildingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/walltype$/, action: function (match) { return WallTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/walldescription$/, action: function (match) { return WallDescriptionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poapprovalstatus/, action: function (match) { return POApprovalStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/setopening$/, action: function (match) { return SetOpeningController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorygroup$/, action: function (match) { return InventoryGroupController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilityrate$/, action: function (match) { return FacilityRateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/potype$/, action: function (match) { return POTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/sapvendorinvoicestatus$/, action: function (match) { return SapVendorInvoiceStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectdrawings$/, action: function (match) { return ProjectDrawingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/eventcategory$/, action: function (match) { return EventCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/personneltype$/, action: function (match) { return PersonnelTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/photographytype$/, action: function (match) { return PhotographyTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/organizationtype$/, action: function (match) { return OrganizationTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poclassification$/, action: function (match) { return POClassificationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/region$/, action: function (match) { return RegionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventoryadjustmentreason$/, action: function (match) { return InventoryAdjustmentReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/attribute$/, action: function (match) { return AttributeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorystatus$/, action: function (match) { return InventoryStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/retiredreason$/, action: function (match) { return RetiredReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/unretiredreason$/, action: function (match) { return UnretiredReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/discountreason$/, action: function (match) { return DiscountReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/contactevent$/, action: function (match) { return ContactEventController.getModuleScreen(); } });
routes.push({ pattern: /^module\/contacttitle$/, action: function (match) { return ContactTitleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/maillist$/, action: function (match) { return MailListController.getModuleScreen(); } });
routes.push({ pattern: /^module\/currency$/, action: function (match) { return CurrencyController.getModuleScreen(); } });
routes.push({ pattern: /^module\/scheduletype$/, action: function (match) { return ScheduleTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poimportance$/, action: function (match) { return POImportanceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/crewschedulestatus$/, action: function (match) { return CrewScheduleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehicleschedulestatus$/, action: function (match) { return VehicleScheduleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclecolor$/, action: function (match) { return VehicleColorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilityschedulestatus$/, action: function (match) { return FacilityScheduleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/unit$/, action: function (match) { return UnitController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poapprover$/, action: function (match) { return POApproverController.getModuleScreen(); } });
routes.push({ pattern: /^module\/userstatus$/, action: function (match) { return UserStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/crewstatus/, action: function (match) { return CrewStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclestatus$/, action: function (match) { return VehicleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilitystatus$/, action: function (match) { return FacilityStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/blackoutstatus$/, action: function (match) { return BlackoutStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/coverletter$/, action: function (match) { return CoverLetterController.getModuleScreen(); } });
routes.push({ pattern: /^module\/termsconditions$/, action: function (match) { return TermsConditionsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobecare$/, action: function (match) { return WardrobeCareController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobecolor$/, action: function (match) { return WardrobeColorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobegender$/, action: function (match) { return WardrobeGenderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobelabel$/, action: function (match) { return WardrobeLabelController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobematerial$/, action: function (match) { return WardrobeMaterialController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclemake$/, action: function (match) { return VehicleMakeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorytype$/, action: function (match) { return InventoryTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/propscondition$/, action: function (match) { return PropsConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatorwatts$/, action: function (match) { return GeneratorWattsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilitytype$/, action: function (match) { return FacilityTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/labortype$/, action: function (match) { return LaborTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/misctype$/, action: function (match) { return MiscTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/propscondition$/, action: function (match) { return PropsConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobecondition$/, action: function (match) { return WardrobeConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorycondition$/, action: function (match) { return InventoryConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/repairitemstatus$/, action: function (match) { return RepairItemStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/licenseclass$/, action: function (match) { return LicenseClassController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatorfueltype$/, action: function (match) { return GeneratorFuelTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatormake$/, action: function (match) { return GeneratorMakeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatorrating$/, action: function (match) { return GeneratorRatingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclefueltype$/, action: function (match) { return VehicleFuelTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclerating$/, action: function (match) { return VehicleRatingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobeperiod$/, action: function (match) { return WardrobePeriodController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobepattern$/, action: function (match) { return WardrobePatternController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poapproverrole$/, action: function (match) { return POApproverRoleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobesource$/, action: function (match) { return WardrobeSourceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/formdesign$/, action: function (match) { return FormDesignController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorcatalog$/, action: function (match) { return VendorCatalogController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorinvoiceapprover$/, action: function (match) { return VendorInvoiceApproverController.getModuleScreen(); } });
routes.push({ pattern: /^module\/partscategory$/, action: function (match) { return PartsCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/salescategory$/, action: function (match) { return SalesCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalcategory$/, action: function (match) { return RentalCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/warehousecatalog$/, action: function (match) { return WarehouseCatalogController.getModuleScreen(); } });
routes.push({ pattern: /^module\/presentationlayer$/, action: function (match) { return PresentationLayerController.getModuleScreen(); } });
routes.push({ pattern: /^module\/taxoption$/, action: function (match) { return TaxOptionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/source$/, action: function (match) { return SourceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/shipvia$/, action: function (match) { return ShipViaController.getModuleScreen(); } });
routes.push({ pattern: /^module\/setcondition$/, action: function (match) { return SetConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/salesinventory$/, action: function (match) { return SalesInventoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalinventory$/, action: function (match) { return RentalInventoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectdropshipitems$/, action: function (match) { return ProjectDropShipItemsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectdeposit$/, action: function (match) { return ProjectDepositController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectcommissioning$/, action: function (match) { return ProjectCommissioningController.getModuleScreen(); } });
routes.push({ pattern: /^module\/presentationlayeractivity$/, action: function (match) { return PresentationLayerActivityController.getModuleScreen(); } });
routes.push({ pattern: /^module\/ordersetno$/, action: function (match) { return OrderSetNoController.getModuleScreen(); } });
routes.push({ pattern: /^module\/orderlocation$/, action: function (match) { return OrderLocationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/porejectreason$/, action: function (match) { return PORejectReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilitycategory$/, action: function (match) { return FacilityCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/barcoderange$/, action: function (match) { return BarCodeRangeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/fiscalyear$/, action: function (match) { return FiscalYearController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectasbuild$/, action: function (match) { return ProjectAsBuildController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectitemsordered$/, action: function (match) { return ProjectItemsOrderedController.getModuleScreen(); } });
routes.push({ pattern: /^module\/deal$/, action: function (match) { return DealController.getModuleScreen(); } });
routes.push({ pattern: /^module\/deal\/(\S+)\/(\S+)/, action: function (match) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return DealController.getModuleScreen(filter); } });
routes.push({ pattern: /^module\/vehicletype$/, action: function (match) { return VehicleTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/laborcategory$/, action: function (match) { return LaborCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/misccategory$/, action: function (match) { return MiscCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatortype$/, action: function (match) { return GeneratorTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/miscrate$/, action: function (match) { return MiscRateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/laborrate$/, action: function (match) { return LaborRateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/holiday$/, action: function (match) { return HolidayController.getModuleScreen(); } });
routes.push({ pattern: /^module\/discounttemplate$/, action: function (match) { return DiscountTemplateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/spacetype$/, action: function (match) { return SpaceTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventoryrank$/, action: function (match) { return InventoryRankController.getModuleScreen(); } });
routes.push({ pattern: /^module\/laborposition$/, action: function (match) { return LaborPositionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/eventtype$/, action: function (match) { return EventTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/template$/, action: function (match) { return TemplateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/gldistribution$/, action: function (match) { return GlDistributionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/crew$/, action: function (match) { return CrewController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote$/, action: function (match) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/widget$/, action: function (match) { return WidgetController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dashboard$/, action: function (match) { return DashboardController.loadDashboard(); } });
routes.push({ pattern: /^module\/contract$/, action: function (match) { return ContractController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealoutstanding/, action: function (match) { return RwDealOutstandingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/invoicesummaryreport/, action: function (match) { return RwInvoiceSummaryReportController.getModuleScreen(); } });
routes.push({ pattern: /^module\/chargeprocessing/, action: function (match) { return RwChargeProcessingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receiptprocessing/, action: function (match) { return RwReceiptProcessingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorinvoiceprocessing/, action: function (match) { return RwVendorInvoiceProcessingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/control$/, action: function (match) { return ControlController.getModuleScreen(); } });
routes.push({ pattern: /^module\/group/, action: function (match) { return GroupController.getModuleScreen(); } });
routes.push({ pattern: /^module\/integration/, action: function (match) { return RwIntegrationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/user/, action: function (match) { return UserController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customfield/, action: function (match) { return CustomFieldController.getModuleScreen(); } });
routes.push({ pattern: /^module\/duplicaterule/, action: function (match) { return DuplicateRuleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/settingspage$/, action: function (match) { return SettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/designer$/, action: function (match) { return DesignerController.loadDesigner(); } });
routes.push({ pattern: /^module\/example/, action: function (match) { return RwExampleController.getModuleScreen(); } });
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