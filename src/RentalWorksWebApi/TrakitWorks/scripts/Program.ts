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
    //---------------------------------------------------------------------------------
    async loadSession($elementToBlock: JQuery) {
        const responseSessionInfo = await FwAjax.callWebApi<any, any>({
            httpMethod: 'GET',
            url: `${applicationConfig.apiurl}api/v1/account/session?applicationid=${FwApplicationTree.currentApplicationId}`,
            $elementToBlock: $elementToBlock
        });
        sessionStorage.setItem('email', responseSessionInfo.webUser.email);
        sessionStorage.setItem('fullname', responseSessionInfo.webUser.fullname);
        sessionStorage.setItem('name', responseSessionInfo.webUser.name);  //justin 05/06/2018
        sessionStorage.setItem('usersid', responseSessionInfo.webUser.usersid);  //justin 05/25/2018  //C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        sessionStorage.setItem('browsedefaultrows', responseSessionInfo.webUser.browsedefaultrows);
        sessionStorage.setItem('applicationtheme', responseSessionInfo.webUser.applicationtheme);
        sessionStorage.setItem('lastLoggedIn', new Date().toLocaleTimeString());
        sessionStorage.setItem('serverVersion', responseSessionInfo.serverVersion);
        sessionStorage.setItem('applicationOptions', JSON.stringify(responseSessionInfo.applicationOptions));  // justin hoffman 04/29/2020 - only used by QuikScan currently
        sessionStorage.setItem('userType', responseSessionInfo.webUser.usertype);
        sessionStorage.setItem('applicationtree', JSON.stringify(responseSessionInfo.applicationtree));
        sessionStorage.setItem('clientCode', responseSessionInfo.clientcode);
        sessionStorage.setItem('location', JSON.stringify(responseSessionInfo.location));
        sessionStorage.setItem('defaultlocation', JSON.stringify(responseSessionInfo.location));
        sessionStorage.setItem('warehouse', JSON.stringify(responseSessionInfo.warehouse));
        sessionStorage.setItem('department', JSON.stringify(responseSessionInfo.department));
        sessionStorage.setItem('webusersid', responseSessionInfo.webUser.webusersid);
        sessionStorage.setItem('userid', JSON.stringify(responseSessionInfo.webUser));
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
                const responseGetUserSettings = values[0];
                const responseGetCustomFields = values[1];
                const responseGetCustomForms = values[2];
                const responseGetDefaultSettings = values[3];
                const responseGetInventorySettings = values[4];
                const responseGetSystemSettings = values[5];
                const responseGetDepartment = values[6];
                const responseGetDocumentBarCodeSettings = values[7];
                const responseGetSystemNumbers = values[8];
                const responseGetIsTraining = values[9];
                const responseGetWarehouses = values[10];

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
                userid.defaultquikactivitysetting = responseGetUserSettings.QuikActivitySetting;
                sessionStorage.setItem('userid', JSON.stringify(userid));

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

                const baseFormIndex = responseGetCustomForms.ColumnIndex.BaseForm;
                const customFormIdIndex = responseGetCustomForms.ColumnIndex.CustomFormId;
                const customFormDescIndex = responseGetCustomForms.ColumnIndex.Description;
                const thisUserOnlyIndex = responseGetCustomForms.ColumnIndex.ThisUserOnly;
                const htmlIndex = responseGetCustomForms.ColumnIndex.Html;
                const assignToIndex = responseGetCustomForms.ColumnIndex.AssignTo;
                const activeCustomForms: any = [];
                const baseForms: any = [];
                const duplicateForms: any = [];
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
                    defaultdealstatusid: responseGetDefaultSettings.DefaultDealStatusId,
                    defaultdealstatus: responseGetDefaultSettings.DefaultDealStatus,
                    defaultcreditstatus: responseGetDefaultSettings.DefaultCreditStatus,
                    defaultcreditstatusid: responseGetDefaultSettings.DefaultCreditStatusId,
                    defaultcustomerstatusid: responseGetDefaultSettings.DefaultCustomerStatusId,
                    defaultcustomerstatus: responseGetDefaultSettings.DefaultCustomerStatus,
                    defaultcustomerpaymenttermsid: responseGetDefaultSettings.DefaultCustomerPaymentTermsId,
                    defaultcustomerpaymentterms: responseGetDefaultSettings.DefaultCustomerPaymentTerms,
                    defaultdealbillingcycleid: responseGetDefaultSettings.DefaultDealBillingCycleId,
                    defaultdealbillingcycle: responseGetDefaultSettings.DefaultDealBillingCycle,
                    defaultdealporequired: responseGetDefaultSettings.DefaultDealPoRequired,
                    defaultdealpotype: responseGetDefaultSettings.DefaultDealPoType,
                    defaultunitid: responseGetDefaultSettings.DefaultUnitId,
                    defaultunit: responseGetDefaultSettings.DefaultUnit,
                    defaultrank: responseGetDefaultSettings.DefaultRank,
                    defaulticodemask: responseGetInventorySettings.ICodeMask,
                    userassignedicodes: responseGetInventorySettings.UserAssignedICodes,
                    enable3weekpricing: responseGetInventorySettings.Enable3WeekPricing,
                    defaultrentalsaleretiredreasonid: responseGetInventorySettings.DefaultRentalSaleRetiredReasonId,
                    defaultrentalsaleretiredreason: responseGetInventorySettings.DefaultRentalSaleRetiredReason,
                    defaultlossanddamageretiredreasonid: responseGetInventorySettings.DefaultLossAndDamageRetiredReasonId,
                    defaultlossanddamageretiredreason: responseGetInventorySettings.DefaultLossAndDamageRetiredReason,
                    enablereceipts: responseGetSystemSettings.EnableReceipts,
                    enablepayments: responseGetSystemSettings.EnablePayments,
                    sharedealsacrossofficelocations: responseGetSystemSettings.ShareDealsAcrossOfficeLocations,
                    systemname: responseGetSystemSettings.SystemName,
                    companyname: responseGetSystemSettings.CompanyName,
                    documentbarcodestyle: responseGetDocumentBarCodeSettings.DocumentBarCodeStyle,
                    userassignedvendornumber: responseGetSystemSettings.IsVendorNumberAssignedByUser,
                    userassignedcustomernumber: userassignedcustnum,
                    userassigneddealnumber: userassigneddealnum,
                    multiwarehouse: (responseGetWarehouses.Rows.length > 1),
                    allowdeleteinvoices: responseGetSystemSettings.AllowDeleteInvoices,
                    allowinvoicedatechange: responseGetSystemSettings.AllowInvoiceDateChange,
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
jQuery(async () => {
    const startAsync = async () => {
        const loadAppAsync = async () => {
            program.load();
            program.loadCustomFormsAndBrowseViewsAsync();
        }
        var $templates = jQuery('script[data-ajaxload="true"]');
        if ($templates.length > 0) {
            do {
                setTimeout(async () => {
                    $templates = jQuery('script[data-ajaxload="true"]');
                    if ($templates.length === 0) {
                        await loadAppAsync();
                    }
                }, 250);
            } while ($templates.length > 0)
        } else {
            await loadAppAsync();
        }
    }
    if (applicationConfig.debugMode) {
        setTimeout(async () => {
            await startAsync();
        }, 1000);
    } else {
        await startAsync();

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
routes.push({ pattern: /^module\/customreportlayout$/, action: function (match: RegExpExecArray) { return CustomReportLayoutController.getModuleScreen(); } });
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

