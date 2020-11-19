class Program extends FwApplication {
    name = Constants.appCaption;
    title = Constants.appTitle;
    //---------------------------------------------------------------------------------
    constructor() {
        super();
        FwApplicationTree.currentApplicationId = Constants.appId;
    }
    //---------------------------------------------------------------------------------
    async loadSession($elementToBlock: JQuery) {
        const responseSessionInfo = await FwAjax.callWebApi<any, any>({
            httpMethod:      'GET',
            url:             `${applicationConfig.apiurl}api/v1/account/session?applicationid=${FwApplicationTree.currentApplicationId}`,
            $elementToBlock: $elementToBlock
        });
        sessionStorage.setItem('email',              responseSessionInfo.webUser.email);
        sessionStorage.setItem('fullname',           responseSessionInfo.webUser.fullname);
        sessionStorage.setItem('name',               responseSessionInfo.webUser.name);  //justin 05/06/2018
        sessionStorage.setItem('usersid',            responseSessionInfo.webUser.usersid);  //justin 05/25/2018  //C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        sessionStorage.setItem('browsedefaultrows',  responseSessionInfo.webUser.browsedefaultrows);
        sessionStorage.setItem('applicationtheme',   responseSessionInfo.webUser.applicationtheme);
        sessionStorage.setItem('lastLoggedIn',       new Date().toLocaleTimeString());
        sessionStorage.setItem('serverVersion',      responseSessionInfo.serverVersion);
        sessionStorage.setItem('applicationOptions', JSON.stringify(responseSessionInfo.applicationOptions));  // justin hoffman 04/29/2020 - only used by QuikScan currently
        sessionStorage.setItem('userType',           responseSessionInfo.webUser.usertype);
        sessionStorage.setItem('applicationtree',    JSON.stringify(responseSessionInfo.applicationtree));
        sessionStorage.setItem('clientCode',         responseSessionInfo.clientcode);
        sessionStorage.setItem('location',           JSON.stringify(responseSessionInfo.location));
        sessionStorage.setItem('defaultlocation',    JSON.stringify(responseSessionInfo.location));
        sessionStorage.setItem('warehouse',          JSON.stringify(responseSessionInfo.warehouse));
        sessionStorage.setItem('department',         JSON.stringify(responseSessionInfo.department));
        sessionStorage.setItem('webusersid',         responseSessionInfo.webUser.webusersid);
        sessionStorage.setItem('userid',             JSON.stringify(responseSessionInfo.webUser));
        if (responseSessionInfo.webUser.usertype == 'CONTACT') {
            sessionStorage.setItem('deal', JSON.stringify(responseSessionInfo.deal));
        }
        jQuery('html').removeClass('theme-material');

        // run several AJAX calls in parallel

        const promiseGetUserSettings = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/userprofile/${responseSessionInfo.webUser.webusersid}`,
            $elementToBlock: $elementToBlock
        });

        const promiseGetCustomFields = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/customfield`,
            $elementToBlock: $elementToBlock
        });

        const promiseGetCustomForms = FwAjax.callWebApi<BrowseRequest, any>({
            httpMethod: 'POST',
            url: `${applicationConfig.apiurl}api/v1/assignedcustomform/browse`,
            $elementToBlock: $elementToBlock,
            data: {
                uniqueids: {
                    WebUserId: responseSessionInfo.webUser.webusersid
                }
            }
        });

        const promiseGetDefaultSettings = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/defaultsettings/1`,
            $elementToBlock: $elementToBlock
        });
        const promiseGetInventorySettings = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/inventorysettings/1`,
            $elementToBlock: $elementToBlock
        });
        const promiseGetSystemSettings = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/systemsettings/1`,
            $elementToBlock: $elementToBlock
        });
        const promiseGetDepartment = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/department/${responseSessionInfo.department.departmentid}`,
            $elementToBlock: $elementToBlock
        });

        const promiseGetDocumentBarCodeSettings = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/documentbarcodesettings/1`,
            $elementToBlock: $elementToBlock
        });

        const promiseGetSystemNumbers = FwAjax.callWebApi<BrowseRequest, any>({
            httpMethod: 'POST',
            url: `${applicationConfig.apiurl}api/v1/systemnumber/browse`,
            $elementToBlock: $elementToBlock,
            data: {
                uniqueids: {
                    LocationId: responseSessionInfo.location.locationid
                }
            }
        });

        const promiseGetWarehouses = FwAjax.callWebApi<BrowseRequest, any>({
            httpMethod: 'POST',
            url: `${applicationConfig.apiurl}api/v1/warehouse/browse`,
            $elementToBlock: $elementToBlock,
            data: {
                searchfieldoperators: ["<>"],
                searchfields: ["Inactive"],
                searchfieldvalues: ["T"]
            }
        });

        const promiseGetIsTraining = FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/utilityfunctions/istraining`,
            $elementToBlock: $elementToBlock
        });

        // wait for all the queries to finish
        await Promise.all([
            promiseGetUserSettings,             // 00
            promiseGetCustomFields,             // 01
            promiseGetCustomForms,              // 02
            promiseGetDefaultSettings,          // 03
            promiseGetInventorySettings,        // 04
            promiseGetSystemSettings,           // 05
            promiseGetDepartment,               // 06
            promiseGetDocumentBarCodeSettings,  // 07
            promiseGetSystemNumbers,            // 08
            promiseGetIsTraining,               // 09
            promiseGetWarehouses                // 10
        ])
            .then(async (values: any) => {
                const responseGetUserSettings            = values[0];
                const responseGetCustomFields            = values[1];
                const responseGetCustomForms             = values[2];
                const responseGetDefaultSettings         = values[3];
                const responseGetInventorySettings       = values[4];
                const responseGetSystemSettings          = values[5];
                const responseGetDepartment              = values[6];
                const responseGetDocumentBarCodeSettings = values[7];
                const responseGetSystemNumbers           = values[8];
                const responseGetIsTraining              = values[9];
                const responseGetWarehouses              = values[10];

                // Load sounds into DOM for use elsewhere
                FwFunc.getBase64Sound('Success', responseGetUserSettings);
                FwFunc.getBase64Sound('Error', responseGetUserSettings);
                FwFunc.getBase64Sound('Notification', responseGetUserSettings);

                const homePage: any = {};
                homePage.guid = responseGetUserSettings.HomeMenuGuid;
                homePage.path = responseGetUserSettings.HomeMenuPath;

                const favorites = responseGetUserSettings.FavoritesJson;
                sessionStorage.setItem('homePage', JSON.stringify(homePage));
                sessionStorage.setItem('favorites', favorites);
                sessionStorage.setItem('emailsignature', responseGetUserSettings.EmailSignature);

                // Web admin - security for peek validation show / hide   J.Pace 7/12/19
                //justin hoffman 12/16/2019 re-added after merge
                const userid = JSON.parse(sessionStorage.getItem('userid'));
                if (responseGetUserSettings.WebAdministrator === true) {
                    userid.webadministrator = 'true';
                } else {
                    userid.webadministrator = 'false';
                }
                userid.reportsnavexpanded = `${responseGetUserSettings.ReportsNavigationMenuVisible}`;
                userid.settingsnavexpanded = `${responseGetUserSettings.SettingsNavigationMenuVisible}`;
                userid.mainmenupinned = responseGetUserSettings.MainMenuPinned;
                userid.locale = responseGetUserSettings.Locale;
                userid.defaultquikactivitysetting = responseGetUserSettings.QuikActivitySetting;
                sessionStorage.setItem('userid', JSON.stringify(userid));

                FwLocale.setLocale(userid.locale);

                // Include department's default activity selection in sessionStorage for use in Quote / Order
                const department = JSON.parse(sessionStorage.getItem('department'));
                const defaultActivities: Array<string> = [];
                for (let key in responseGetDepartment) {
                    if (key.startsWith('DefaultActivity') && responseGetDepartment[key] === true) {
                        defaultActivities.push(key.slice(15));
                    }
                }
                department.activities = defaultActivities;
                sessionStorage.setItem('department', JSON.stringify(department));

                const customFields = [];
                for (let i = 0; i < responseGetCustomFields.length; i++) {
                    if (customFields.indexOf(responseGetCustomFields[i].ModuleName) === -1) {
                        customFields.push(responseGetCustomFields[i].ModuleName);
                    }
                }
                sessionStorage.setItem('customFields', JSON.stringify(customFields));

                const baseFormIndex          = responseGetCustomForms.ColumnIndex.BaseForm;
                const customFormIdIndex      = responseGetCustomForms.ColumnIndex.CustomFormId;
                const customFormDescIndex    = responseGetCustomForms.ColumnIndex.Description;
                const thisUserOnlyIndex      = responseGetCustomForms.ColumnIndex.ThisUserOnly;
                const htmlIndex              = responseGetCustomForms.ColumnIndex.Html;
                const assignToIndex          = responseGetCustomForms.ColumnIndex.AssignTo;
                const activeCustomForms: any = [];
                const baseForms:         any = [];
                const duplicateForms:    any = [];
                for (let i = 0; i < responseGetCustomForms.Rows.length; i++) {
                    const customForm = responseGetCustomForms.Rows[i];
                    const baseform = customForm[baseFormIndex];
                    const assignTo = customForm[assignToIndex];
                    activeCustomForms.push({ 'BaseForm': baseform, 'CustomFormId': customForm[customFormIdIndex], 'Description': customForm[customFormDescIndex], 'ThisUserOnly': customForm[thisUserOnlyIndex] });
                    jQuery('head').append(`<template id="tmpl-custom-${baseform}">${customForm[htmlIndex]}</template>`);
                    if (typeof baseForms.find(obj => obj.BaseForm == baseform) == 'undefined') {
                        baseForms.push({ 'BaseForm': baseform, 'RowIndex': i, 'AssignTo': assignTo });
                    } else {
                        const customForms = baseForms.filter(obj => obj.BaseForm == baseform && obj.AssignTo == assignTo);
                        if (!customForms.length) {
                            baseForms.push({ 'BaseForm': baseform, 'RowIndex': i, 'AssignTo': assignTo });
                        } else {
                            const dupeIndex = customForms[0].RowIndex;
                            duplicateForms.push({ 'BaseForm': baseform, 'Desc1': responseGetCustomForms.Rows[dupeIndex][customFormDescIndex], 'Desc2': customForm[customFormDescIndex] })
                        }
                    }
                }
                if (duplicateForms.length) {
                    sessionStorage.setItem('duplicateforms', JSON.stringify(duplicateForms));
                }

                if (activeCustomForms.length > 0) {
                    sessionStorage.setItem('customForms', JSON.stringify(activeCustomForms));
                }

                const systemNumberModuleIndex = responseGetSystemNumbers.ColumnIndex.Module;
                const systemNumberIsAssignedByUserIndex = responseGetSystemNumbers.ColumnIndex.IsAssignByUser;
                let userassignedcustnum: boolean = false;
                let userassigneddealnum: boolean = false;
                for (let i = 0; i < responseGetSystemNumbers.Rows.length; i++) {
                    const moduleSystemNumber = responseGetSystemNumbers.Rows[i];
                    const module = moduleSystemNumber[systemNumberModuleIndex];
                    const isAssignedByUser = moduleSystemNumber[systemNumberIsAssignedByUserIndex];
                    if (module === 'CUSTOMER') {
                        userassignedcustnum = isAssignedByUser;
                    }
                    else if (module === 'DEAL') {
                        userassigneddealnum = isAssignedByUser;
                    }
                }

                const controlDefaults = {
                    defaultdealstatusid:                 responseGetDefaultSettings.DefaultDealStatusId,
                    defaultdealstatus:                   responseGetDefaultSettings.DefaultDealStatus,
                    defaultcreditstatus:                 responseGetDefaultSettings.DefaultCreditStatus,
                    defaultcreditstatusid:               responseGetDefaultSettings.DefaultCreditStatusId,
                    defaultcustomerstatusid:             responseGetDefaultSettings.DefaultCustomerStatusId,
                    defaultcustomerstatus:               responseGetDefaultSettings.DefaultCustomerStatus,
                    defaultcustomerpaymenttermsid:       responseGetDefaultSettings.DefaultCustomerPaymentTermsId,
                    defaultcustomerpaymentterms:         responseGetDefaultSettings.DefaultCustomerPaymentTerms,
                    defaultdealbillingcycleid:           responseGetDefaultSettings.DefaultDealBillingCycleId,
                    defaultdealbillingcycle:             responseGetDefaultSettings.DefaultDealBillingCycle,
                    defaultdealporequired:               responseGetDefaultSettings.DefaultDealPoRequired,
                    defaultdealpotype:                   responseGetDefaultSettings.DefaultDealPoType,
                    defaultunitid:                       responseGetDefaultSettings.DefaultUnitId,
                    defaultunit:                         responseGetDefaultSettings.DefaultUnit,
                    defaultrank:                         responseGetDefaultSettings.DefaultRank,
                    defaulticodemask:                    responseGetInventorySettings.ICodeMask,
                    userassignedicodes:                  responseGetInventorySettings.UserAssignedICodes,
                    enable3weekpricing:                  responseGetInventorySettings.Enable3WeekPricing,
                    defaultrentalsaleretiredreasonid:    responseGetInventorySettings.DefaultRentalSaleRetiredReasonId,
                    defaultrentalsaleretiredreason:      responseGetInventorySettings.DefaultRentalSaleRetiredReason,
                    defaultlossanddamageretiredreasonid: responseGetInventorySettings.DefaultLossAndDamageRetiredReasonId,
                    defaultlossanddamageretiredreason:   responseGetInventorySettings.DefaultLossAndDamageRetiredReason,
                    enableconsignment:                   responseGetInventorySettings.EnableConsignment,
                    enablelease:                         responseGetInventorySettings.EnableLease,
                    defaultrentalquantityinventorycostcalculation:  responseGetInventorySettings.DefaultRentalQuantityInventoryCostCalculation,
                    defaultsalesquantityinventorycostcalculation:   responseGetInventorySettings.DefaultSalesQuantityInventoryCostCalculation,
                    defaultpartsquantityinventorycostcalculation:   responseGetInventorySettings.DefaultPartsQuantityInventoryCostCalculation,
                    enablereceipts:                      responseGetSystemSettings.EnableReceipts,
                    enablepayments:                      responseGetSystemSettings.EnablePayments,
                    sharedealsacrossofficelocations:     responseGetSystemSettings.ShareDealsAcrossOfficeLocations,
                    systemname:                          responseGetSystemSettings.SystemName,
                    companyname:                         responseGetSystemSettings.CompanyName,
                    documentbarcodestyle:                responseGetDocumentBarCodeSettings.DocumentBarCodeStyle,
                    userassignedvendornumber:            responseGetSystemSettings.IsVendorNumberAssignedByUser,
                    userassignedcustomernumber:          userassignedcustnum,
                    userassigneddealnumber:              userassigneddealnum,
                    multiwarehouse: (responseGetWarehouses.Rows.length > 1),
                    allowdeleteinvoices:                 responseGetSystemSettings.AllowDeleteInvoices,
                    allowinvoicedatechange:              responseGetSystemSettings.AllowInvoiceDateChange,
                }
                sessionStorage.setItem('controldefaults', JSON.stringify(controlDefaults));

                if (responseGetIsTraining === true) {
                    sessionStorage.setItem('istraining', "true");
                } else {
                    sessionStorage.setItem('istraining', "false");
                }
            });
    }
    //---------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------
var program: Program = new Program();
//---------------------------------------------------------------------------------
jQuery(function () {
    jQuery('html').css('background-color', 'initial');

    if (sessionStorage.getItem('app') !== null && sessionStorage.getItem('app') !== 'rentalworks') {
        program.navigate('logoff');
        return;
    }

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
    setTimeout(async () => {
        program.load();
        await program.loadCustomFormsAndBrowseViewsAsync();
        //program.loadDefaultPage();
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
//change pw screen
routes.push({
    pattern: /^changepassword$/,
    action: function (match: RegExpExecArray) {
        return RwBaseController.getChangePasswordScreen();
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
routes.push({ pattern: /^module\/creditcardpinpad$/, action: function (match: RegExpExecArray) { return CreditCardPinPadController.getModuleScreen(); } });
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

