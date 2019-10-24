class Base {
    //----------------------------------------------------------------------------------------------
    getDefaultScreen() {
        let viewModel = {
            captionProgramTitle: 'RentalWorks',
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version
        };
        let screen: any = {};
        screen = FwBasePages.getDefaultScreen(viewModel);

        screen.$view
            .on('click', '.btnLogin', function () {
                try {
                    program.navigate('login');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .find('.programlogo').empty().html('<div class="bgothm">Rental<span class="rwpurple">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span></div>');

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    getLoginScreen() {
        let viewModel = {
            captionPanelLogin: 'RentalWorks Login',
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
        let screen: any = {};
        if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
            screen = window[`Rw${applicationConfig.client}Controller`]['getLoginScreen']();
        } else {
            screen = FwBasePages.getLoginScreen(viewModel);
        }

        screen.$view
            .on('click', '.btnLogin', async (e: JQuery.ClickEvent) => {
                try {
                    var $loginWindow = screen.$view.find('.login-container');
                    var $email = screen.$view.find('#email');
                    var $password = screen.$view.find('#password');
                    $email.parent().removeClass('error');
                    $password.parent().removeClass('error');
                    if ($email.val() == '') {
                        $email.parent().addClass('error');
                    } else if ($password.val() == '') {
                        $password.parent().addClass('error');
                    } else {
                        sessionStorage.clear();

                        // Ajax for a jwt token
                        const responseJwt = await FwAjax.callWebApi<any, any>({
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
                            localStorage.setItem('email', $email.val()); // mv 5/10/19 - this is the email or login and cannot be used to display the email address
                            if (typeof responseJwt.exception !== 'undefined') {
                                if (applicationConfig.debugMode) {
                                    $loginWindow.find('.errormessage').html('').html(responseJwt.exception + responseJwt.stacktrace).show();
                                } else {
                                    $loginWindow.find('.errormessage').html('').html(responseJwt.exception).show();
                                }
                            } else {
                                // get session info
                                const responseSessionInfo = await FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/account/session?applicationid=${FwApplicationTree.currentApplicationId}`,
                                    $elementToBlock: $loginWindow
                                });
                                sessionStorage.setItem('email', responseSessionInfo.webUser.email);
                                sessionStorage.setItem('fullname', responseSessionInfo.webUser.fullname);
                                sessionStorage.setItem('name', responseSessionInfo.webUser.name);  //justin 05/06/2018
                                sessionStorage.setItem('usersid', responseSessionInfo.webUser.usersid);  //justin 05/25/2018  //C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
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

                                // run several AJAX calls in parallel

                                const promiseGetUserSettings = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/usersettings/${responseSessionInfo.webUser.webusersid}`,
                                    $elementToBlock: $loginWindow
                                });

                                const promiseGetCustomFields = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/customfield`,
                                    $elementToBlock: $loginWindow
                                });

                                const promiseGetCustomForms = FwAjax.callWebApi<BrowseRequest, any>({
                                    httpMethod: 'POST',
                                    url: `${applicationConfig.apiurl}api/v1/assignedcustomform/browse`,
                                    $elementToBlock: $loginWindow,
                                    data: {
                                        uniqueids: {
                                            WebUserId: responseSessionInfo.webUser.webusersid
                                        }
                                    }
                                });

                                const promiseGetDefaultSettings = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/defaultsettings/1`,
                                    $elementToBlock: $loginWindow
                                });
                                const promiseGetInventorySettings = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/inventorysettings/1`,
                                    $elementToBlock: $loginWindow
                                });
                                const promiseGetSystemSettings = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/systemsettings/1`,
                                    $elementToBlock: $loginWindow
                                });

                                const promiseGetUser = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/user/${responseSessionInfo.webUser.usersid}`,
                                    $elementToBlock: $loginWindow
                                });
                                const promiseGetDepartment = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/department/${responseSessionInfo.department.departmentid}`,
                                    $elementToBlock: $loginWindow
                                });

                                const promiseGetDocumentBarCodeSettings = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/documentbarcodesettings/1`,
                                    $elementToBlock: $loginWindow
                                });


                                // wait for all the queries to finish
                                await Promise.all([
                                    promiseGetUserSettings,             // 00
                                    promiseGetCustomFields,             // 01
                                    promiseGetCustomForms,              // 02
                                    promiseGetDefaultSettings,          // 03
                                    promiseGetInventorySettings,        // 04
                                    promiseGetSystemSettings,           // 05
                                    promiseGetUser,                     // 06
                                    promiseGetDepartment,               // 07
                                    promiseGetDocumentBarCodeSettings,  // 08
                                ])
                                    .then((values: any) => {
                                        const responseGetUserSettings = values[0];
                                        const responseGetCustomFields = values[1];
                                        const responseGetCustomForms = values[2];
                                        const responseGetDefaultSettings = values[3];
                                        const responseGetInventorySettings = values[4];
                                        const responseGetSystemSettings = values[5];
                                        const responseGetUser = values[6];
                                        const responseGetDepartment = values[7];
                                        const responseGetDocumentBarCodeSettings = values[8];

                                        let sounds: any = {}, homePage: any = {}, toolbar: any = {};
                                        sounds.successSoundFileName = responseGetUserSettings.SuccessSoundFileName;
                                        sounds.errorSoundFileName = responseGetUserSettings.ErrorSoundFileName;
                                        sounds.notificationSoundFileName = responseGetUserSettings.NotificationSoundFileName;
                                        homePage.guid = responseGetUserSettings.HomeMenuGuid;
                                        homePage.path = responseGetUserSettings.HomeMenuPath;
                                        toolbar = responseGetUserSettings.ToolBarJson;
                                        sessionStorage.setItem('sounds', JSON.stringify(sounds));
                                        sessionStorage.setItem('homePage', JSON.stringify(homePage));
                                        sessionStorage.setItem('toolbar', toolbar);
                                        // Web admin - temp security for peek validation show / hide   J.Pace 7/12/19
                                        const userid = JSON.parse(sessionStorage.getItem('userid'));
                                        //if (values[4].WebAdministrator === true) {
                                        if (responseGetUser.WebAdministrator === true) {
                                            userid.webadministrator = 'true';
                                        } else {
                                            userid.webadministrator = 'false';
                                        }
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
                                        const htmlIndex = responseGetCustomForms.ColumnIndex.Html;
                                        const activeCustomForms: any = [];
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
                                            defaultdealstatusid: responseGetDefaultSettings.DefaultDealStatusId
                                            , defaultdealstatus: responseGetDefaultSettings.DefaultDealStatus
                                            , defaultcustomerstatusid: responseGetDefaultSettings.DefaultCustomerStatusId
                                            , defaultcustomerstatus: responseGetDefaultSettings.DefaultCustomerStatus
                                            , defaultdealbillingcycleid: responseGetDefaultSettings.DefaultDealBillingCycleId
                                            , defaultdealbillingcycle: responseGetDefaultSettings.DefaultDealBillingCycle
                                            , defaultunitid: responseGetDefaultSettings.DefaultUnitId
                                            , defaultunit: responseGetDefaultSettings.DefaultUnit
                                            , defaulticodemask: responseGetInventorySettings.ICodeMask
                                            , sharedealsacrossofficelocations: responseGetSystemSettings.ShareDealsAcrossOfficeLocations
                                            , systemname: responseGetSystemSettings.SystemName
                                            , companyname: responseGetSystemSettings.CompanyName
                                            , documentbarcodestyle: responseGetDocumentBarCodeSettings.DocumentBarCodeStyle
                                        }
                                        sessionStorage.setItem('controldefaults', JSON.stringify(controlDefaults));

                                        // set redirectPath to navigate user to default home page, still need to go to the home page to run startup code if the user refreshes the browser
                                        let homePagePath = JSON.parse(sessionStorage.getItem('homePage')).path;
                                        if (homePagePath !== null && homePagePath !== '') {
                                            sessionStorage.setItem('redirectPath', homePagePath);
                                        }
                                        program.navigate('home');
                                    });
                            }

                        } else if (responseJwt.statuscode !== 0) {
                            $loginWindow.find('.errormessage').html('').html(responseJwt.statusmessage).show();
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnCancel', function (e) {
                try {
                    program.navigate('default');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .find('.programlogo').empty().html('<div class="bgothm">Rental<span class="rwpurple">Works<span style="font-size:14px;vertical-align:super;">&#174;</span></span></div>');

        screen.load = function () {
            setTimeout(function () {
                if (screen.$view.find('#email').val() == '') {
                    screen.$view.find('#email').focus();
                } else {
                    screen.$view.find('#password').focus();
                }
            }, 0);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    getAboutScreen() {
        let viewModel = {};
        let properties = {};
        let screen: any = {};
        screen = FwBasePages.getAboutScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        screen.$view
            .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
        ;
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    getSupportScreen() {
        let viewModel = {};
        let properties = {};
        let screen: any = {};
        screen = FwBasePages.getSupportScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    getPasswordRecoveryScreen() {
        let viewModel = {};
        let properties = {};
        let screen: any = {};
        screen = FwBasePages.getPasswordRecoveryScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    }
    //----------------------------------------------------------------------------------------------
}
var RwBaseController = new Base();
