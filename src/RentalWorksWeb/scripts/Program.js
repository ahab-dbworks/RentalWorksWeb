var program, ScannerDevice, LineaScanner;
Program.prototype = new FwApplication;
Program.constructor = Program;
//---------------------------------------------------------------------------------
function Program() { FwApplication.call(this);
    var me;
    
    me = this;
    FwApplicationTree.currentApplicationId = '0A5F2584-D239-480F-8312-7C2B552A30BA';
}
//---------------------------------------------------------------------------------
Program.prototype.getApplicationOptions = function() {
    return JSON.parse(sessionStorage.getItem('applicationOptions'));
};
//---------------------------------------------------------------------------------
Program.prototype.modules = [
  //Home Modules
  //  { urlpattern: /^module\/quote$/,                  getScreen: function() { return RwQuoteController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/order$/,                  getScreen: function() { return RwOrderController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/customer$/,               getScreen: function() { return RwCustomerController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/deal$/,                   getScreen: function() { return RwDealController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/vendor$/,                 getScreen: function() { return RwVendorController.getModuleScreen({}, {}); } }
  { urlpattern: /^module\/contact$/,                      getScreen: function () { return ContactController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vendor$/,                     getScreen: function () { return VendorController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/contact$/,                  getScreen: function() { return ContactController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/customer$/,                 getScreen: function() { return CustomerController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/rentalinventory$/,        getScreen: function() { return RwRentalInventoryController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/salesinventory$/,         getScreen: function() { return RwSalesInventoryController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/barcodeditems$/,          getScreen: function() { return RwBarCodedItemsController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/serialitems$/,            getScreen: function() { return RwSerialItemsController.getModuleScreen({}, {}); } }
  //, { urlpattern: /^module\/repairorder$/,            getScreen: function() { return RwRepairOrderController.getModuleScreen({}, {}); } }
    //Settings Modules
  , { urlpattern: /^module\/country$/,                    getScreen: function() { return CountryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/state$/,                      getScreen: function() { return StateController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/customerstatus$/,             getScreen: function() { return CustomerStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/customertype$/,               getScreen: function() { return CustomerTypeController.getModuleScreen(); } }
  , { urlpattern: /^module\/glaccount$/,                  getScreen: function() { return GlAccountController.getModuleScreen(); } }
  , { urlpattern: /^module\/billingcycle$/,               getScreen: function() { return BillingCycleController.getModuleScreen(); } }
  , { urlpattern: /^module\/paymenttype$/,                getScreen: function() { return PaymentTypeController.getModuleScreen(); } }
  , { urlpattern: /^module\/paymentterms$/,               getScreen: function() { return PaymentTermsController.getModuleScreen(); } }
  , { urlpattern: /^module\/customersettings/,            getScreen: function() { return RwCustomerSettingsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/ordertype$/,                  getScreen: function() { return RwOrderTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/usersettings/,                getScreen: function() { return UserSettingsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vendorclass$/,                getScreen: function() { return VendorClassController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/warehouse$/,                  getScreen: function() { return WarehouseController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/customercategory$/,           getScreen: function() { return CustomerCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/creditstatus$/,               getScreen: function() { return CreditStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/dealtype$/,                   getScreen: function() { return DealTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/dealstatus$/,                 getScreen: function() { return DealStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/department$/,                 getScreen: function() { return DepartmentController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/productiontype$/,             getScreen: function() { return ProductionTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/officelocation$/,             getScreen: function() { return OfficeLocationController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/dealclassification$/,         getScreen: function() { return DealClassificationController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/documenttype$/,               getScreen: function () { return DocumentTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/setsurface$/,                 getScreen: function () { return SetSurfaceController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/building$/,                   getScreen: function () { return BuildingController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/walltype$/,                   getScreen: function () { return WallTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/walldescription$/,            getScreen: function () { return WallDescriptionController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/poapprovalstatus/,            getScreen: function () { return POApprovalStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/setopening$/,                 getScreen: function () { return SetOpeningController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/facilityrate$/,               getScreen: function () { return FacilityRateController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/sapvendorinvoicestatus$/,     getScreen: function () { return SapVendorInvoiceStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/projectdrawings$/,            getScreen: function () { return ProjectDrawingsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/eventcategory$/,              getScreen: function() { return EventCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/personneltype$/,              getScreen: function() { return PersonnelTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/photographytype$/,            getScreen: function() { return PhotographyTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/organizationtype$/,           getScreen: function() { return OrganizationTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/poclassification$/,           getScreen: function() { return POClassificationController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/region$/,                     getScreen: function() { return RegionController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/inventoryadjustmentreason$/,  getScreen: function() { return InventoryAdjustmentReasonController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/inventoryattribute$/,         getScreen: function() { return InventoryAttributeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/inventorystatus$/,            getScreen: function() { return InventoryStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/retiredreason$/,              getScreen: function() { return RetiredReasonController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/unretiredreason$/,            getScreen: function() { return UnretiredReasonController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/discountreason$/,             getScreen: function() { return DiscountReasonController.getModuleScreen({}, {}); } } 
  , { urlpattern: /^module\/contactevent$/,               getScreen: function() { return ContactEventController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/contacttitle$/,               getScreen: function() { return ContactTitleController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/maillist$/,                   getScreen: function() { return MailListController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/currency$/,                   getScreen: function() { return CurrencyController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/scheduletype$/,               getScreen: function() { return ScheduleTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/poimportance$/,               getScreen: function() { return POImportanceController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/crewschedulestatus$/,         getScreen: function() { return CrewScheduleStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vehicleschedulestatus$/,      getScreen: function() { return VehicleScheduleStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vehiclecolor$/,               getScreen: function() { return VehicleColorController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/facilityschedulestatus$/,     getScreen: function() { return FacilityScheduleStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/unit$/,                       getScreen: function () { return UnitController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/poapprover$/,                 getScreen: function () { return POApproverController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/userstatus$/,                 getScreen: function () { return UserStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/crewstatus/,                  getScreen: function () { return CrewStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vehiclestatus$/,              getScreen: function () { return VehicleStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/facilitystatus$/,             getScreen: function () { return FacilityStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/blackoutstatus$/,             getScreen: function () { return BlackoutStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/coverletter$/,                getScreen: function() { return CoverLetterController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/termsconditions$/,            getScreen: function() { return TermsConditionsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobecare$/,               getScreen: function() { return WardrobeCareController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobecolor$/,              getScreen: function() { return WardrobeColorController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobegender$/,             getScreen: function() { return WardrobeGenderController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobelabel$/,              getScreen: function() { return WardrobeLabelController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vehiclemake$/,                getScreen: function () { return VehicleMakeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/inventorytype$/,              getScreen: function() { return InventoryTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/propscondition$/,             getScreen: function() { return PropsConditionController.getModuleScreen({}, {}); } } 
  , { urlpattern: /^module\/generatorwatts$/,             getScreen: function () { return GeneratorWattsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/facilitytype$/,               getScreen: function() { return FacilityTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/labortype$/,                  getScreen: function() { return LaborTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/misctype$/,                   getScreen: function() { return MiscTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/propscondition$/,             getScreen: function() { return PropsConditionController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobecondition$/,          getScreen: function() { return WardrobeConditionController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/inventorycondition$/,         getScreen: function() { return InventoryConditionController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/repairitemstatus$/,           getScreen: function () { return RepairItemStatusController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/licenseclass$/,               getScreen: function () { return LicenseClassController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/generatorfueltype$/,          getScreen: function () { return GeneratorFuelTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/generatormake$/,              getScreen: function () { return GeneratorMakeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/generatorrating$/,            getScreen: function () { return GeneratorRatingController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vehiclefueltype$/,            getScreen: function () { return VehicleFuelTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vehiclerating$/,              getScreen: function () { return VehicleRatingController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobeperiod$/,             getScreen: function () { return WardrobePeriodController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobepattern$/,            getScreen: function () { return WardrobePatternController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/poapproverrole$/,             getScreen: function() { return POApproverRoleController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/wardrobesource$/,             getScreen: function() { return WardrobeSourceController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/formdesign$/,                 getScreen: function() { return FormDesignController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vendorcatalog$/,              getScreen: function() { return VendorCatalogController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/partscategory$/,              getScreen: function () { return PartsCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/salescategory$/,              getScreen: function () { return SalesCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/rentalcategory$/,             getScreen: function () { return RentalCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/warehousecatalog$/,           getScreen: function() { return WarehouseCatalogController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/presentationlayer$/,          getScreen: function() { return PresentationLayerController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/taxoption$/,                  getScreen: function () { return TaxOptionController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/source$/,                     getScreen: function () { return SourceController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/shipvia$/,                    getScreen: function () { return ShipViaController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/setcondition$/,               getScreen: function() { return SetConditionController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/salesinventory$/,             getScreen: function() { return SalesInventoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/rentalinventory$/,            getScreen: function() { return RentalInventoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/projectdropshipitems$/,       getScreen: function () { return ProjectDropShipItemsController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/projectdeposit$/,             getScreen: function() { return ProjectDepositController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/projectcommissioning$/,       getScreen: function() { return ProjectCommissioningController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/presentationlayeractivity$/,  getScreen: function() { return PresentationLayerActivityController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/ordersetno$/,                 getScreen: function () { return OrderSetNoController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/orderlocation$/,              getScreen: function () { return OrderLocationController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/porejectreason$/,             getScreen: function () { return PORejectReasonController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/facilitycategory$/,           getScreen: function () { return FacilityCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/barcoderange$/,               getScreen: function () { return BarCodeRangeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/fiscalyear$/,                 getScreen: function () { return FiscalYearController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/projectasbuild$/,             getScreen: function () { return ProjectAsBuildController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/projectitemsordered$/,        getScreen: function () { return ProjectItemsOrderedController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/deal$/,                       getScreen: function () { return DealController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vehicletype$/,                getScreen: function () { return VehicleTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/laborcategory$/,              getScreen: function () { return LaborCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/misccategory$/,               getScreen: function () { return MiscCategoryController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/generatortype$/,              getScreen: function () { return GeneratorTypeController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/miscrate$/,                   getScreen: function () { return MiscRateController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/laborrate$/,                  getScreen: function () { return LaborRateController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/holiday$/,                    getScreen: function () { return HolidayController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/spacetype$/,                  getScreen: function () { return SpaceTypeController.getModuleScreen({}, {}); } }

    //Reports                                             
  , { urlpattern: /^module\/dealoutstanding/,             getScreen: function() { return RwDealOutstandingController.getModuleScreen({}, {}); } }
    //Utilities Modules                                   
  , { urlpattern: /^module\/chargeprocessing/,            getScreen: function() { return RwChargeProcessingController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/receiptprocessing/,           getScreen: function() { return RwReceiptProcessingController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/vendorinvoiceprocessing/,     getScreen: function() { return RwVendorInvoiceProcessingController.getModuleScreen({}, {}); } }
    //Administrator                                       
  , { urlpattern: /^module\/group/,                       getScreen: function() { return GroupController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/integration/,                 getScreen: function() { return RwIntegrationController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/user/,                        getScreen: function() { return UserController.getModuleScreen({}, {}); } }
  , { urlpattern: /^module\/settingspage$/,               getScreen: function () { return SettingsPageController.getModuleScreen({}, {}); } }
    //Exports                                             
  , { urlpattern: /^module\/example/,                     getScreen: function() { return RwExampleController.getModuleScreen({}, {}); } }
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
            return null;
        }
    }
];
//---------------------------------------------------------------------------------
Program.prototype.navigateHashChange = function(path) {
    var me, screen, match;
    me = this;
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
jQuery(function () {
    program = new Program();
    program.load();
    program.loadDefaultPage();
});
//---------------------------------------------------------------------------------
window.onhashchange = function() {
    program.navigateHashChange(window.location.hash.replace('#/',''));
};
//---------------------------------------------------------------------------------

