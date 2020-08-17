class Program extends FwApplication {
    name = Constants.appCaption;
    title = Constants.appTitle;
    //---------------------------------------------------------------------------------
    constructor() {
        super();
        FwApplicationTree.currentApplicationId = Constants.appId;
    }
}
//---------------------------------------------------------------------------------
var program: Program = new Program();
//---------------------------------------------------------------------------------
jQuery(function () {
    jQuery('html').css('background-color', 'initial');
    //add a catch all 404 route at the end of the routes array
    routes.push({
        pattern: /.*/,
        action: function (match: RegExpExecArray) {
            //if we have okta stuff, we are redirecting to localhost/rentalworksweb/ as we cant redirect to a url with hash fragment per oauth.
            if (window.location.href === "http://localhost/rentalworksweb/" && (localStorage.getItem('okta-token-storage') !== null)) {
                program.navigate('login');
            } else {
                FwConfirmation.showMessage('404', `Not Found: ${window.location.href}`, false, true, 'OK', (event) => {
                    program.navigate('home');
                });
            }
        }
    });
    setTimeout(() => {
        program.load();
        program.loadCustomFormsAndBrowseViews();
        program.loadDefaultPage();
    }, 2000);
});
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
        if (!FwAppData.verifyHasAuthToken()) {
            return RwBaseController.getLoginScreen();
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
//Home Modules
routes.push({ pattern: /^module\/assignbarcodes$/, action: function (match: RegExpExecArray) { return AssignBarCodesController.getModuleScreen(); } });
routes.push({ pattern: /^module\/checkin$/, action: function (match: RegExpExecArray) { return CheckInController.getModuleScreen(); } });
routes.push({ pattern: /^module\/checkout$/, action: function (match: RegExpExecArray) { return StagingCheckoutController.getModuleScreen(); } });
routes.push({ pattern: /^module\/completeqc$/, action: function (match: RegExpExecArray) { return CompleteQcController.getModuleScreen(); } });
routes.push({ pattern: /^module\/contact$/, action: function (match: RegExpExecArray) { return ContactController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customer$/, action: function (match: RegExpExecArray) { return CustomerController.getModuleScreen(); } });
routes.push({ pattern: /^module\/deal$/, action: function (match: RegExpExecArray) { return DealController.getModuleScreen(); } });
routes.push({ pattern: /^module\/exchange$/, action: function (match: RegExpExecArray) { return ExchangeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/invoice$/, action: function (match: RegExpExecArray) { return InvoiceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/item$/, action: function (match: RegExpExecArray) { return AssetController.getModuleScreen(); } });
routes.push({ pattern: /^module\/order$/, action: function (match: RegExpExecArray) { return OrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/orderstatus$/, action: function (match: RegExpExecArray) { return OrderStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/picklist$/, action: function (match: RegExpExecArray) { return PickListController.getModuleScreen(); } });
routes.push({ pattern: /^module\/purchaseorder$/, action: function (match: RegExpExecArray) { return PurchaseOrderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receivefromvendor$/, action: function (match: RegExpExecArray) { return ReceiveFromVendorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/returntovendor$/, action: function (match: RegExpExecArray) { return ReturnToVendorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/repair$/, action: function (match: RegExpExecArray) { return RepairController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendor$/, action: function (match: RegExpExecArray) { return VendorController.getModuleScreen(); } });

routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/rentalinventory$/,        action: function(match: RegExpExecArray) { return RwRentalInventoryController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/salesinventory$/,         action: function(match: RegExpExecArray) { return RwSalesInventoryController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/barcodeditems$/,          action: function(match: RegExpExecArray) { return RwBarCodedItemsController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/serialitems$/,            action: function(match: RegExpExecArray) { return RwSerialItemsController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/repair$/,            action: function(match: RegExpExecArray) { return RwRepairController.getModuleScreen(); } });
routes.push({ pattern: /^module\/purchasehistory$/, action: function (match: RegExpExecArray) { return PurchaseHistoryController.getModuleScreen(); } });

//Settings Modules
routes.push({ pattern: /^module\/documentbarcodesettings$/, action: function (match: RegExpExecArray) { return DocumentBarCodeSettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/country$/, action: function (match: RegExpExecArray) { return CountryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/state$/, action: function (match: RegExpExecArray) { return StateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customerstatus$/, action: function (match: RegExpExecArray) { return CustomerStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customertype$/, action: function (match: RegExpExecArray) { return CustomerTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/glaccount$/, action: function (match: RegExpExecArray) { return GlAccountController.getModuleScreen(); } });
routes.push({ pattern: /^module\/billingcycle$/, action: function (match: RegExpExecArray) { return BillingCycleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/paymenttype$/, action: function (match: RegExpExecArray) { return PaymentTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/paymentterms$/, action: function (match: RegExpExecArray) { return PaymentTermsController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/customersettings$/, action: function (match: RegExpExecArray) { return RwCustomerSettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/activitytype$/, action: function (match: RegExpExecArray) { return ActivityTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/ordertype$/, action: function (match: RegExpExecArray) { return OrderTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/userprofile$/, action: function (match: RegExpExecArray) { return UserProfileController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorclass$/, action: function (match: RegExpExecArray) { return VendorClassController.getModuleScreen(); } });
routes.push({ pattern: /^module\/warehouse$/, action: function (match: RegExpExecArray) { return WarehouseController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customercategory$/, action: function (match: RegExpExecArray) { return CustomerCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/creditstatus$/, action: function (match: RegExpExecArray) { return CreditStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealtype$/, action: function (match: RegExpExecArray) { return DealTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealstatus$/, action: function (match: RegExpExecArray) { return DealStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dealclassification$/, action: function (match: RegExpExecArray) { return DealClassificationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/department$/, action: function (match: RegExpExecArray) { return DepartmentController.getModuleScreen(); } });
routes.push({ pattern: /^module\/productiontype$/, action: function (match: RegExpExecArray) { return ProductionTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/officelocation$/, action: function (match: RegExpExecArray) { return OfficeLocationController.getModuleScreen(); } });

//Settings Modules
routes.push({ pattern: /^module\/documentbarcodesettings$/, action: function (match: RegExpExecArray) { return DocumentBarCodeSettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/documenttype$/, action: function (match: RegExpExecArray) { return DocumentTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/setsurface$/, action: function (match: RegExpExecArray) { return SetSurfaceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/building$/, action: function (match: RegExpExecArray) { return BuildingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/walltype$/, action: function (match: RegExpExecArray) { return WallTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/walldescription$/, action: function (match: RegExpExecArray) { return WallDescriptionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poapprovalstatus$/, action: function (match: RegExpExecArray) { return POApprovalStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/setopening$/, action: function (match: RegExpExecArray) { return SetOpeningController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorygroup$/, action: function (match: RegExpExecArray) { return InventoryGroupController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilityrate$/, action: function (match: RegExpExecArray) { return FacilityRateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/potype$/, action: function (match: RegExpExecArray) { return POTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/sapvendorinvoicestatus$/, action: function (match: RegExpExecArray) { return SapVendorInvoiceStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectdrawings$/, action: function (match: RegExpExecArray) { return ProjectDrawingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/eventcategory$/, action: function (match: RegExpExecArray) { return EventCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/personneltype$/, action: function (match: RegExpExecArray) { return PersonnelTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/photographytype$/, action: function (match: RegExpExecArray) { return PhotographyTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/organizationtype$/, action: function (match: RegExpExecArray) { return OrganizationTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poclassification$/, action: function (match: RegExpExecArray) { return POClassificationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/region$/, action: function (match: RegExpExecArray) { return RegionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventoryadjustmentreason$/, action: function (match: RegExpExecArray) { return InventoryAdjustmentReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/attribute$/, action: function (match: RegExpExecArray) { return AttributeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorystatus$/, action: function (match: RegExpExecArray) { return InventoryStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/retiredreason$/, action: function (match: RegExpExecArray) { return RetiredReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/unretiredreason$/, action: function (match: RegExpExecArray) { return UnretiredReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/discountreason$/, action: function (match: RegExpExecArray) { return DiscountReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/contactevent$/, action: function (match: RegExpExecArray) { return ContactEventController.getModuleScreen(); } });
routes.push({ pattern: /^module\/contacttitle$/, action: function (match: RegExpExecArray) { return ContactTitleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/maillist$/, action: function (match: RegExpExecArray) { return MailListController.getModuleScreen(); } });
routes.push({ pattern: /^module\/currency$/, action: function (match: RegExpExecArray) { return CurrencyController.getModuleScreen(); } });
routes.push({ pattern: /^module\/scheduletype$/, action: function (match: RegExpExecArray) { return ScheduleTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poimportance$/, action: function (match: RegExpExecArray) { return POImportanceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/crewschedulestatus$/, action: function (match: RegExpExecArray) { return CrewScheduleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehicleschedulestatus$/, action: function (match: RegExpExecArray) { return VehicleScheduleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclecolor$/, action: function (match: RegExpExecArray) { return VehicleColorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilityschedulestatus$/, action: function (match: RegExpExecArray) { return FacilityScheduleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/unit$/, action: function (match: RegExpExecArray) { return UnitController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poapprover$/, action: function (match: RegExpExecArray) { return POApproverController.getModuleScreen(); } });
routes.push({ pattern: /^module\/userstatus$/, action: function (match: RegExpExecArray) { return UserStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/crewstatus$/, action: function (match: RegExpExecArray) { return CrewStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclestatus$/, action: function (match: RegExpExecArray) { return VehicleStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilitystatus$/, action: function (match: RegExpExecArray) { return FacilityStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/blackoutstatus$/, action: function (match: RegExpExecArray) { return BlackoutStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/coverletter$/, action: function (match: RegExpExecArray) { return CoverLetterController.getModuleScreen(); } });
routes.push({ pattern: /^module\/termsconditions$/, action: function (match: RegExpExecArray) { return TermsConditionsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobecare$/, action: function (match: RegExpExecArray) { return WardrobeCareController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobecolor$/, action: function (match: RegExpExecArray) { return WardrobeColorController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobegender$/, action: function (match: RegExpExecArray) { return WardrobeGenderController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobelabel$/, action: function (match: RegExpExecArray) { return WardrobeLabelController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobematerial$/, action: function (match: RegExpExecArray) { return WardrobeMaterialController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclemake$/, action: function (match: RegExpExecArray) { return VehicleMakeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorytype$/, action: function (match: RegExpExecArray) { return InventoryTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/propscondition$/, action: function (match: RegExpExecArray) { return PropsConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatorwatts$/, action: function (match: RegExpExecArray) { return GeneratorWattsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilitytype$/, action: function (match: RegExpExecArray) { return FacilityTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/labortype$/, action: function (match: RegExpExecArray) { return LaborTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/misctype$/, action: function (match: RegExpExecArray) { return MiscTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/propscondition$/, action: function (match: RegExpExecArray) { return PropsConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobecondition$/, action: function (match: RegExpExecArray) { return WardrobeConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventorycondition$/, action: function (match: RegExpExecArray) { return InventoryConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/repairitemstatus$/, action: function (match: RegExpExecArray) { return RepairItemStatusController.getModuleScreen(); } });
routes.push({ pattern: /^module\/licenseclass$/, action: function (match: RegExpExecArray) { return LicenseClassController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatorfueltype$/, action: function (match: RegExpExecArray) { return GeneratorFuelTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatormake$/, action: function (match: RegExpExecArray) { return GeneratorMakeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatorrating$/, action: function (match: RegExpExecArray) { return GeneratorRatingController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehiclefueltype$/, action: function (match: RegExpExecArray) { return VehicleFuelTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobeperiod$/, action: function (match: RegExpExecArray) { return WardrobePeriodController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobepattern$/, action: function (match: RegExpExecArray) { return WardrobePatternController.getModuleScreen(); } });
routes.push({ pattern: /^module\/poapproverrole$/, action: function (match: RegExpExecArray) { return POApproverRoleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/wardrobesource$/, action: function (match: RegExpExecArray) { return WardrobeSourceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/formdesign$/, action: function (match: RegExpExecArray) { return FormDesignController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorcatalog$/, action: function (match: RegExpExecArray) { return VendorCatalogController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorinvoiceapprover$/, action: function (match: RegExpExecArray) { return VendorInvoiceApproverController.getModuleScreen(); } });
routes.push({ pattern: /^module\/partscategory$/, action: function (match: RegExpExecArray) { return PartsCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/salescategory$/, action: function (match: RegExpExecArray) { return SalesCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalcategory$/, action: function (match: RegExpExecArray) { return RentalCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/warehousecatalog$/, action: function (match: RegExpExecArray) { return WarehouseCatalogController.getModuleScreen(); } });
routes.push({ pattern: /^module\/presentationlayer$/, action: function (match: RegExpExecArray) { return PresentationLayerController.getModuleScreen(); } });
routes.push({ pattern: /^module\/taxoption$/, action: function (match: RegExpExecArray) { return TaxOptionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/source$/, action: function (match: RegExpExecArray) { return SourceController.getModuleScreen(); } });
routes.push({ pattern: /^module\/shipvia$/, action: function (match: RegExpExecArray) { return ShipViaController.getModuleScreen(); } });
routes.push({ pattern: /^module\/setcondition$/, action: function (match: RegExpExecArray) { return SetConditionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/salesinventory$/, action: function (match: RegExpExecArray) { return SalesInventoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/rentalinventory$/, action: function (match: RegExpExecArray) { return RentalInventoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectdropshipitems$/, action: function (match: RegExpExecArray) { return ProjectDropShipItemsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectdeposit$/, action: function (match: RegExpExecArray) { return ProjectDepositController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectcommissioning$/, action: function (match: RegExpExecArray) { return ProjectCommissioningController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/presentationlayeractivity$/, action: function (match: RegExpExecArray) { return PresentationLayerActivityController.getModuleScreen(); } });
routes.push({ pattern: /^module\/ordersetno$/, action: function (match: RegExpExecArray) { return OrderSetNoController.getModuleScreen(); } });
routes.push({ pattern: /^module\/orderlocation$/, action: function (match: RegExpExecArray) { return OrderLocationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/porejectreason$/, action: function (match: RegExpExecArray) { return PORejectReasonController.getModuleScreen(); } });
routes.push({ pattern: /^module\/facilitycategory$/, action: function (match: RegExpExecArray) { return FacilityCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/barcoderange$/, action: function (match: RegExpExecArray) { return BarCodeRangeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/fiscalyear$/, action: function (match: RegExpExecArray) { return FiscalYearController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectasbuild$/, action: function (match: RegExpExecArray) { return ProjectAsBuildController.getModuleScreen(); } });
routes.push({ pattern: /^module\/projectitemsordered$/, action: function (match: RegExpExecArray) { return ProjectItemsOrderedController.getModuleScreen(); } });
routes.push({ pattern: /^module\/deal$/, action: function (match: RegExpExecArray) { return DealController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vehicletype$/, action: function (match: RegExpExecArray) { return VehicleTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/laborcategory$/, action: function (match: RegExpExecArray) { return LaborCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/misccategory$/, action: function (match: RegExpExecArray) { return MiscCategoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/generatortype$/, action: function (match: RegExpExecArray) { return GeneratorTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/miscrate$/, action: function (match: RegExpExecArray) { return MiscRateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/laborrate$/, action: function (match: RegExpExecArray) { return LaborRateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/holiday$/, action: function (match: RegExpExecArray) { return HolidayController.getModuleScreen(); } });
routes.push({ pattern: /^module\/discounttemplate$/, action: function (match: RegExpExecArray) { return DiscountTemplateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/spacetype$/, action: function (match: RegExpExecArray) { return SpaceTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/inventoryrank$/, action: function (match: RegExpExecArray) { return InventoryRankController.getModuleScreen(); } });
routes.push({ pattern: /^module\/laborposition$/, action: function (match: RegExpExecArray) { return LaborPositionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/eventtype$/, action: function (match: RegExpExecArray) { return EventTypeController.getModuleScreen(); } });
routes.push({ pattern: /^module\/template$/, action: function (match: RegExpExecArray) { return TemplateController.getModuleScreen(); } });
routes.push({ pattern: /^module\/gldistribution$/, action: function (match: RegExpExecArray) { return GlDistributionController.getModuleScreen(); } });
routes.push({ pattern: /^module\/crew$/, action: function (match: RegExpExecArray) { return CrewController.getModuleScreen(); } });
routes.push({ pattern: /^module\/quote$/, action: function (match: RegExpExecArray) { return QuoteController.getModuleScreen(); } });
routes.push({ pattern: /^module\/contract$/, action: function (match: RegExpExecArray) { return ContractController.getModuleScreen(); } });
routes.push({ pattern: /^module\/physicalinventory$/, action: function (match: RegExpExecArray) { return PhysicalInventoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/dataexportformat$/, action: function (match: RegExpExecArray) { return DataExportFormatController.getModuleScreen(); } });
routes.push({ pattern: /^module\/marketsegment$/, action: function (match: RegExpExecArray) { return MarketSegmentController.getModuleScreen(); } });

//Utilities Modules                                   
routes.push({ pattern: /^module\/invoiceprocessbatch$/, action: function (match: RegExpExecArray) { return InvoiceProcessBatchController.getModuleScreen(); } });
routes.push({ pattern: /^module\/receiptprocessbatch$/, action: function (match: RegExpExecArray) { return ReceiptProcessBatchController.getModuleScreen(); } });
routes.push({ pattern: /^module\/vendorinvoiceprocessbatch$/, action: function (match: RegExpExecArray) { return VendorInvoiceProcessBatchController.getModuleScreen(); } });

//Administrator
routes.push({ pattern: /^module\/group$/, action: function (match: RegExpExecArray) { return GroupController.getModuleScreen(); } });
routes.push({ pattern: /^module\/integration$/, action: function (match: RegExpExecArray) { return IntegrationController.getModuleScreen(); } });
routes.push({ pattern: /^module\/user$/, action: function (match: RegExpExecArray) { return UserController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customfield$/, action: function (match: RegExpExecArray) { return CustomFieldController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customreportlayout$/, action: function (match: RegExpExecArray) { return CustomReportLayoutController.getModuleScreen(); } });
routes.push({ pattern: /^module\/emailhistory$/, action: function (match: RegExpExecArray) { return EmailHistoryController.getModuleScreen(); } });
routes.push({ pattern: /^module\/duplicaterules$/, action: function (match: RegExpExecArray) { return DuplicateRuleController.getModuleScreen(); } });
routes.push({ pattern: /^module\/settings$/, action: function (match: RegExpExecArray) { return SettingsController.getModuleScreen(); } });
routes.push({ pattern: /^module\/reports$/, action: function (match: RegExpExecArray) { return ReportsController.getModuleScreen(); } });
//routes.push({ pattern: /^module\/designer$/, action: function (match: RegExpExecArray) { return DesignerController.loadDesigner(); } });
//Exports                                             
//routes.push({ pattern: /^module\/example$/, action: function (match: RegExpExecArray) { return RwExampleController.getModuleScreen(); } });
//---------------------------------------------------------------------------------
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

