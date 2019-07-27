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

                                const promiseGetControlDefaults = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/control/1`,
                                    $elementToBlock: $loginWindow
                                });

                                const promiseGetUser = FwAjax.callWebApi<any, any>({
                                    httpMethod: 'GET',
                                    url: `${applicationConfig.apiurl}api/v1/user/${responseSessionInfo.webUser.usersid}`,
                                    $elementToBlock: $loginWindow
                                });


                                // wait for all the queries to finish
                                await Promise.all([
                                    promiseGetUserSettings,     // 00
                                    promiseGetCustomFields,     // 01
                                    promiseGetCustomForms,      // 02
                                    promiseGetControlDefaults,  // 03
                                    promiseGetUser   // 04
                                ])
                                    .then((values: any) => {
                                        const responseGetUserSettings = values[0];
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
                                        if (values[4].WebAdministrator === true) {
                                            userid.webadministrator = 'true';
                                        } else {
                                            userid.webadministrator = 'false';
                                        }
                                        sessionStorage.setItem('userid', JSON.stringify(userid));

                                        const responseGetCustomFields = values[1];
                                        var customFields = [];
                                        for (let i = 0; i < responseGetCustomFields.length; i++) {
                                            if (customFields.indexOf(responseGetCustomFields[i].ModuleName) === -1) {
                                                customFields.push(responseGetCustomFields[i].ModuleName);
                                            }
                                        }
                                        sessionStorage.setItem('customFields', JSON.stringify(customFields));

                                        const responseGetCustomForms = values[2];
                                        let baseFormIndex = responseGetCustomForms.ColumnIndex.BaseForm;
                                        let customFormIdIndex = responseGetCustomForms.ColumnIndex.CustomFormId;
                                        let htmlIndex = responseGetCustomForms.ColumnIndex.Html;
                                        let activeCustomForms: any = [];
                                        for (let i = 0; i < responseGetCustomForms.Rows.length; i++) {
                                            let customForm = responseGetCustomForms.Rows[i];
                                            let baseform = customForm[baseFormIndex];
                                            activeCustomForms.push({ 'BaseForm': baseform, 'CustomFormId': customForm[customFormIdIndex] });
                                            jQuery('head').append(`<template id="tmpl-custom-${baseform}">${customForm[htmlIndex]}</template>`);
                                        }
                                        if (activeCustomForms.length > 0) {
                                            sessionStorage.setItem('customForms', JSON.stringify(activeCustomForms));
                                        }

                                        const responseGetControlDefaults = values[3];
                                        let controlDefaults = {
                                            defaultdealstatusid: responseGetControlDefaults.DefaultDealStatusId
                                            , defaultdealstatus: responseGetControlDefaults.DefaultDealStatus
                                            , defaultcustomerstatusid: responseGetControlDefaults.DefaultCustomerStatusId
                                            , defaultcustomerstatus: responseGetControlDefaults.DefaultCustomerStatus
                                            , defaultdealbillingcycleid: responseGetControlDefaults.DefaultDealBillingCycleId
                                            , defaultdealbillingcycle: responseGetControlDefaults.DefaultDealBillingCycle
                                            , defaultunitid: responseGetControlDefaults.DefaultUnitId
                                            , defaultunit: responseGetControlDefaults.DefaultUnit
                                            , defaulticodemask: responseGetControlDefaults.ICodeMask
                                            , systemname: responseGetControlDefaults.SystemName
                                            , companyname: responseGetControlDefaults.CompanyName
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
