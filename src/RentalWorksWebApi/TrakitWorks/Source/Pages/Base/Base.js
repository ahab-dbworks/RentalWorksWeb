var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
class Base {
    getDefaultScreen() {
        let viewModel = {
            captionProgramTitle: 'TrakitWorks',
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version
        };
        let screen = {};
        screen = FwBasePages.getDefaultScreen(viewModel);
        screen.$view
            .on('click', '.btnLogin', function () {
            try {
                program.navigate('login');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .find('.programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span></div>');
        return screen;
    }
    getLoginScreen() {
        let viewModel = {
            captionPanelLogin: 'TrakitWorks Login',
            captionEmail: RwLanguages.translate('E-mail / Username'),
            valueEmail: (localStorage.getItem('email') ? localStorage.getItem('email') : ''),
            captionPassword: RwLanguages.translate('Password'),
            valuePassword: '',
            captionBtnLogin: RwLanguages.translate('Sign In'),
            captionBtnCancel: RwLanguages.translate('Cancel'),
            captionPasswordRecovery: RwLanguages.translate('Recover Password'),
            captionAbout: RwLanguages.translate('About'),
            captionSupport: RwLanguages.translate('Support'),
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version
        };
        let screen = {};
        if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
            screen = window[`Rw${applicationConfig.client}Controller`]['getLoginScreen']();
        }
        else {
            screen = FwBasePages.getLoginScreen(viewModel);
        }
        screen.$view
            .on('click', '.btnLogin', (e) => __awaiter(this, void 0, void 0, function* () {
            try {
                var $loginWindow = screen.$view.find('.login-container');
                var $email = screen.$view.find('#email');
                var $password = screen.$view.find('#password');
                $email.parent().removeClass('error');
                $password.parent().removeClass('error');
                if ($email.val() == '') {
                    $email.parent().addClass('error');
                }
                else if ($password.val() == '') {
                    $password.parent().addClass('error');
                }
                else {
                    sessionStorage.clear();
                    const responseJwt = yield FwAjax.callWebApi({
                        httpMethod: 'POST',
                        url: `${applicationConfig.apiurl}api/v1/jwt`,
                        $elementToBlock: $loginWindow,
                        data: {
                            UserName: $email.val(),
                            Password: $password.val()
                        }
                    });
                    if ((responseJwt.statuscode == 0) && (typeof responseJwt.access_token !== 'undefined')) {
                        sessionStorage.setItem('apiToken', responseJwt.access_token);
                        localStorage.setItem('email', $email.val());
                        if (typeof responseJwt.exception !== 'undefined') {
                            if (applicationConfig.debugMode) {
                                $loginWindow.find('.errormessage').html('').html(responseJwt.exception + responseJwt.stacktrace).show();
                            }
                            else {
                                $loginWindow.find('.errormessage').html('').html(responseJwt.exception).show();
                            }
                        }
                        else {
                            const responseSessionInfo = yield FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/account/session?applicationid=${FwApplicationTree.currentApplicationId}`,
                                $elementToBlock: $loginWindow
                            });
                            sessionStorage.setItem('email', responseSessionInfo.webUser.email);
                            sessionStorage.setItem('fullname', responseSessionInfo.webUser.fullname);
                            sessionStorage.setItem('name', responseSessionInfo.webUser.name);
                            sessionStorage.setItem('usersid', responseSessionInfo.webUser.usersid);
                            sessionStorage.setItem('browsedefaultrows', responseSessionInfo.webUser.browsedefaultrows);
                            sessionStorage.setItem('applicationtheme', responseSessionInfo.webUser.applicationtheme);
                            sessionStorage.setItem('lastLoggedIn', new Date().toLocaleTimeString());
                            sessionStorage.setItem('serverVersion', responseSessionInfo.serverVersion);
                            sessionStorage.setItem('applicationOptions', JSON.stringify(responseSessionInfo.applicationOptions));
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
                            const promiseGetUserSettings = FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/usersettings/${responseSessionInfo.webUser.webusersid}`,
                                $elementToBlock: $loginWindow
                            });
                            const promiseGetCustomFields = FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/customfield`,
                                $elementToBlock: $loginWindow
                            });
                            const promiseGetCustomForms = FwAjax.callWebApi({
                                httpMethod: 'POST',
                                url: `${applicationConfig.apiurl}api/v1/assignedcustomform/browse`,
                                $elementToBlock: $loginWindow,
                                data: {
                                    uniqueids: {
                                        WebUserId: responseSessionInfo.webUser.webusersid
                                    }
                                }
                            });
                            const promiseGetDefaultSettings = FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/defaultsettings/1`,
                                $elementToBlock: $loginWindow
                            });
                            const promiseGetInventorySettings = FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/inventorysettings/1`,
                                $elementToBlock: $loginWindow
                            });
                            const promiseGetSystemSettings = FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/systemsettings/1`,
                                $elementToBlock: $loginWindow
                            });
                            const promiseGetDepartment = FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/department/${responseSessionInfo.department.departmentid}`,
                                $elementToBlock: $loginWindow
                            });
                            const promiseGetDocumentBarCodeSettings = FwAjax.callWebApi({
                                httpMethod: 'GET',
                                url: `${applicationConfig.apiurl}api/v1/documentbarcodesettings/1`,
                                $elementToBlock: $loginWindow
                            });
                            yield Promise.all([
                                promiseGetUserSettings,
                                promiseGetCustomFields,
                                promiseGetCustomForms,
                                promiseGetDefaultSettings,
                                promiseGetInventorySettings,
                                promiseGetSystemSettings,
                                promiseGetDepartment,
                                promiseGetDocumentBarCodeSettings,
                            ])
                                .then((values) => {
                                const responseGetUserSettings = values[0];
                                const responseGetCustomFields = values[1];
                                const responseGetCustomForms = values[2];
                                const responseGetDefaultSettings = values[3];
                                const responseGetInventorySettings = values[4];
                                const responseGetSystemSettings = values[5];
                                const responseGetDepartment = values[6];
                                const responseGetDocumentBarCodeSettings = values[7];
                                let sounds = {}, homePage = {}, toolbar = {};
                                sounds.successSoundFileName = responseGetUserSettings.SuccessSoundFileName;
                                sounds.errorSoundFileName = responseGetUserSettings.ErrorSoundFileName;
                                sounds.notificationSoundFileName = responseGetUserSettings.NotificationSoundFileName;
                                homePage.guid = responseGetUserSettings.HomeMenuGuid;
                                homePage.path = responseGetUserSettings.HomeMenuPath;
                                toolbar = responseGetUserSettings.ToolBarJson;
                                sessionStorage.setItem('sounds', JSON.stringify(sounds));
                                sessionStorage.setItem('homePage', JSON.stringify(homePage));
                                sessionStorage.setItem('toolbar', toolbar);
                                const department = JSON.parse(sessionStorage.getItem('department'));
                                const defaultActivities = [];
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
                                const htmlIndex = responseGetCustomForms.ColumnIndex.Html;
                                const activeCustomForms = [];
                                for (let i = 0; i < responseGetCustomForms.Rows.length; i++) {
                                    const customForm = responseGetCustomForms.Rows[i];
                                    const baseform = customForm[baseFormIndex];
                                    activeCustomForms.push({ 'BaseForm': baseform, 'CustomFormId': customForm[customFormIdIndex], 'Description': customForm[customFormDescIndex] });
                                    jQuery('head').append(`<template id="tmpl-custom-${baseform}">${customForm[htmlIndex]}</template>`);
                                }
                                if (activeCustomForms.length > 0) {
                                    sessionStorage.setItem('customForms', JSON.stringify(activeCustomForms));
                                }
                                const controlDefaults = {
                                    defaultdealstatusid: responseGetDefaultSettings.DefaultDealStatusId,
                                    defaultdealstatus: responseGetDefaultSettings.DefaultDealStatus,
                                    defaultcustomerstatusid: responseGetDefaultSettings.DefaultCustomerStatusId,
                                    defaultcustomerstatus: responseGetDefaultSettings.DefaultCustomerStatus,
                                    defaultdealbillingcycleid: responseGetDefaultSettings.DefaultDealBillingCycleId,
                                    defaultdealbillingcycle: responseGetDefaultSettings.DefaultDealBillingCycle,
                                    defaultunitid: responseGetDefaultSettings.DefaultUnitId,
                                    defaultunit: responseGetDefaultSettings.DefaultUnit,
                                    defaultrank: responseGetDefaultSettings.DefaultRank,
                                    defaulticodemask: responseGetInventorySettings.ICodeMask,
                                    userassignedicodes: responseGetInventorySettings.UserAssignedICodes,
                                    sharedealsacrossofficelocations: responseGetSystemSettings.ShareDealsAcrossOfficeLocations,
                                    systemname: responseGetSystemSettings.SystemName,
                                    companyname: responseGetSystemSettings.CompanyName,
                                    documentbarcodestyle: responseGetDocumentBarCodeSettings.DocumentBarCodeStyle
                                };
                                sessionStorage.setItem('controldefaults', JSON.stringify(controlDefaults));
                                let homePagePath = JSON.parse(sessionStorage.getItem('homePage')).path;
                                if (homePagePath !== null && homePagePath !== '') {
                                    sessionStorage.setItem('redirectPath', homePagePath);
                                }
                                program.navigate('home');
                            });
                        }
                    }
                    else if (responseJwt.statuscode !== 0) {
                        $loginWindow.find('.errormessage').html('').html(responseJwt.statusmessage).show();
                    }
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }))
            .on('click', '.btnCancel', function (e) {
            try {
                program.navigate('default');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .find('.programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span></div>');
        screen.load = function () {
            setTimeout(function () {
                if (screen.$view.find('#email').val() == '') {
                    screen.$view.find('#email').focus();
                }
                else {
                    screen.$view.find('#password').focus();
                }
            }, 0);
        };
        return screen;
    }
    getAboutScreen() {
        let viewModel = {};
        let properties = {};
        let screen = {};
        screen = FwBasePages.getAboutScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        screen.$view
            .find('#programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span></div>');
        ;
        return screen;
    }
    getSupportScreen() {
        let viewModel = {};
        let properties = {};
        let screen = {};
        screen = FwBasePages.getSupportScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    }
    getPasswordRecoveryScreen() {
        let viewModel = {};
        let properties = {};
        let screen = {};
        screen = FwBasePages.getPasswordRecoveryScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    }
}
var BaseController = new Base();
//# sourceMappingURL=Base.js.map