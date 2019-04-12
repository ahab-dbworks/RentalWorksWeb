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
            .find('.programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span><span style="font-size:14px;vertical-align:super;">&#174;</span></div>');
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
            .on('click', '.btnLogin', function (e) {
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
                    var apiRequest = {
                        UserName: $email.val(),
                        Password: $password.val()
                    };
                    FwAppData.apiMethod(false, "POST", "api/v1/jwt", apiRequest, null, function onSuccess(responseRestApi) {
                        if ((responseRestApi.statuscode == 0) && (typeof responseRestApi.access_token !== 'undefined')) {
                            sessionStorage.setItem('apiToken', responseRestApi.access_token);
                            var request = {
                                email: $email.val(),
                                password: $password.val()
                            };
                            FwServices.account.getAuthToken(request, $loginWindow, function (responseOriginalApi) {
                                try {
                                    if (typeof responseOriginalApi.exception !== 'undefined') {
                                        if (applicationConfig.debugMode) {
                                            $loginWindow.find('.errormessage').html(responseOriginalApi.exception + responseOriginalApi.stacktrace).show();
                                        }
                                        else {
                                            $loginWindow.find('.errormessage').html(responseOriginalApi.exception).show();
                                        }
                                    }
                                    else {
                                        if ((responseOriginalApi.errNo === 0) && (typeof responseOriginalApi.authToken !== 'undefined')) {
                                            localStorage.setItem('email', request.email);
                                            sessionStorage.setItem('email', responseOriginalApi.webUser.email);
                                            sessionStorage.setItem('authToken', responseOriginalApi.authToken);
                                            sessionStorage.setItem('fullname', responseOriginalApi.webUser.fullname);
                                            sessionStorage.setItem('name', responseOriginalApi.webUser.name);
                                            sessionStorage.setItem('usersid', responseOriginalApi.webUser.usersid);
                                            sessionStorage.setItem('browsedefaultrows', responseOriginalApi.webUser.browsedefaultrows);
                                            sessionStorage.setItem('applicationtheme', responseOriginalApi.webUser.applicationtheme);
                                            sessionStorage.setItem('lastLoggedIn', new Date().toLocaleTimeString());
                                            sessionStorage.setItem('serverVersion', responseOriginalApi.serverVersion);
                                            sessionStorage.setItem('applicationOptions', JSON.stringify(responseOriginalApi.applicationOptions));
                                            sessionStorage.setItem('userType', responseOriginalApi.webUser.usertype);
                                            sessionStorage.setItem('applicationtree', JSON.stringify(responseOriginalApi.applicationtree.Result));
                                            sessionStorage.setItem('siteName', responseOriginalApi.site.name);
                                            sessionStorage.setItem('clientCode', responseOriginalApi.clientcode);
                                            sessionStorage.setItem('location', JSON.stringify(responseOriginalApi.webUser.location));
                                            sessionStorage.setItem('defaultlocation', JSON.stringify(responseOriginalApi.webUser.location));
                                            sessionStorage.setItem('warehouse', JSON.stringify(responseOriginalApi.webUser.warehouse));
                                            sessionStorage.setItem('department', JSON.stringify(responseOriginalApi.webUser.department));
                                            sessionStorage.setItem('userid', JSON.stringify(responseOriginalApi.webUser.webusersid));
                                            FwAppData.apiMethod(true, 'GET', `api/v1/usersettings/${responseOriginalApi.webUser.webusersid.webusersid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                                                let sounds = {}, homePage = {};
                                                sounds.successSoundFileName = response.SuccessSoundFileName;
                                                sounds.errorSoundFileName = response.ErrorSoundFileName;
                                                sounds.notificationSoundFileName = response.NotificationSoundFileName;
                                                homePage.guid = response.HomeMenuGuid;
                                                homePage.path = response.HomeMenuPath;
                                                sessionStorage.setItem('sounds', JSON.stringify(sounds));
                                                sessionStorage.setItem('homePage', JSON.stringify(homePage));
                                                FwAppData.apiMethod(true, 'GET', 'api/v1/customfield/', null, FwServices.defaultTimeout, function onSuccess(response) {
                                                    var customFields = [];
                                                    for (let i = 0; i < response.length; i++) {
                                                        if (customFields.indexOf(response[i].ModuleName) === -1) {
                                                            customFields.push(response[i].ModuleName);
                                                        }
                                                    }
                                                    sessionStorage.setItem('customFields', JSON.stringify(customFields));
                                                    let homePagePath = JSON.parse(sessionStorage.getItem('homePage')).path;
                                                    if (homePagePath !== null && homePagePath !== '') {
                                                        program.navigate(`${homePagePath}`);
                                                    }
                                                    else {
                                                        program.navigate('home');
                                                    }
                                                }, function onError(response) {
                                                    FwFunc.showError(response);
                                                    program.navigate('home');
                                                }, null);
                                            }, function onError(response) {
                                                FwFunc.showError(response);
                                            }, null);
                                            let customformrequest = {};
                                            customformrequest.uniqueids = {
                                                WebUserId: responseOriginalApi.webUser.webusersid.webusersid
                                            };
                                            FwAppData.apiMethod(true, 'POST', `api/v1/assignedcustomform/browse`, customformrequest, FwServices.defaultTimeout, function onSuccess(response) {
                                                let baseFormIndex = response.ColumnIndex.BaseForm;
                                                let customFormIdIndex = response.ColumnIndex.CustomFormId;
                                                let htmlIndex = response.ColumnIndex.Html;
                                                let activeCustomForms = [];
                                                for (let i = 0; i < response.Rows.length; i++) {
                                                    let customForm = response.Rows[i];
                                                    let baseform = customForm[baseFormIndex];
                                                    activeCustomForms.push({ 'BaseForm': baseform, 'CustomFormId': customForm[customFormIdIndex] });
                                                    jQuery('head').append(`<template id="tmpl-custom-${baseform}">${customForm[htmlIndex]}</template>`);
                                                }
                                                if (activeCustomForms.length > 0) {
                                                    sessionStorage.setItem('customForms', JSON.stringify(activeCustomForms));
                                                }
                                            }, function onError(response) {
                                                FwFunc.showError(response);
                                            }, null);
                                            customformrequest.uniqueids.OfficeLocationId = responseOriginalApi.webUser.location.locationid;
                                            FwAppData.apiMethod(true, 'POST', `api/v1/browseactiveviewfields/browse`, customformrequest, FwServices.defaultTimeout, function onSuccess(response) {
                                                const moduleNameIndex = response.ColumnIndex.ModuleName;
                                                const activeViewFieldsIndex = response.ColumnIndex.ActiveViewFields;
                                                const idIndex = response.ColumnIndex.Id;
                                                for (let i = 0; i < response.Rows.length; i++) {
                                                    let controller = `${response.Rows[i][moduleNameIndex]}Controller`;
                                                    window[controller].ActiveViewFields = JSON.parse(response.Rows[i][activeViewFieldsIndex]);
                                                    window[controller].ActiveViewFieldsId = response.Rows[i][idIndex];
                                                }
                                            }, function onError(response) {
                                                FwFunc.showError(response);
                                            }, null);
                                        }
                                        else if (responseOriginalApi.errNo !== 0) {
                                            $loginWindow.find('.errormessage').html('').html(responseOriginalApi.errMsg).show();
                                        }
                                        else if (typeof responseOriginalApi.authToken === 'undefined') {
                                            if (applicationConfig.debugMode) {
                                                $loginWindow.find('.errormessage').html('').html('FwServices.account.getAuthToken: responseOriginalApi.authToken is undefined.').show();
                                            }
                                            else {
                                                $loginWindow.find('.errormessage').html('').html('There is an issue with the authorization token.').show();
                                            }
                                        }
                                    }
                                }
                                catch (ex) {
                                    FwFunc.showError(ex);
                                }
                            });
                        }
                        else if (responseRestApi.statuscode !== 0) {
                            $loginWindow.find('.errormessage').html('').html(responseRestApi.statusmessage).show();
                        }
                    }, null, $loginWindow);
                }
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .on('click', '.btnCancel', function (e) {
            try {
                program.navigate('default');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        })
            .find('.programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span><span style="font-size:14px;vertical-align:super;">&#174;</span></div>');
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
            .find('#programlogo').empty().html('<div class="bgothm">Trakit<span class="tiwred">Works<span><span style="font-size:14px;vertical-align:super;">&#174;</span></div>');
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