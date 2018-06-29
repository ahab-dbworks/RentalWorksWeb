var Base = (function () {
    function Base() {
    }
    Base.prototype.getDefaultScreen = function () {
        var viewModel = {
            captionProgramTitle: 'RentalWorks',
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version
        };
        var screen = {};
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
            .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
        ;
        return screen;
    };
    ;
    Base.prototype.getLoginScreen = function () {
        var viewModel = {
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
        var screen = {};
        if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
            screen = window['Rw' + applicationConfig.client + 'Controller']['getLoginScreen']();
        }
        else {
            screen = FwBasePages.getLoginScreen(viewModel);
        }
        screen.$view
            .on('click', '.btnLogin', function (e) {
            var $email, $password, exception, $loginWindow;
            try {
                $loginWindow = screen.$view.find('.login-container');
                $email = screen.$view.find('#email');
                $password = screen.$view.find('#password');
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
                                            $loginWindow.find('.errormessage').html('').html(responseOriginalApi.exception + responseOriginalApi.stacktrace).show();
                                        }
                                        else {
                                            $loginWindow.find('.errormessage').html('').html(responseOriginalApi.exception).show();
                                        }
                                    }
                                    else {
                                        if ((responseOriginalApi.errNo === 0) && (typeof responseOriginalApi.authToken !== 'undefined')) {
                                            localStorage.setItem('email', request.email);
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
                                            sessionStorage.setItem('warehouse', JSON.stringify(responseOriginalApi.webUser.warehouse));
                                            sessionStorage.setItem('department', JSON.stringify(responseOriginalApi.webUser.department));
                                            sessionStorage.setItem('userid', JSON.stringify(responseOriginalApi.webUser.webusersid));
                                            jQuery('html').removeClass('theme-material');
                                            FwAppData.apiMethod(true, 'GET', 'api/v1/customfield/', null, FwServices.defaultTimeout, function onSuccess(response) {
                                                var customFields = [];
                                                var customFieldsBrowse = [];
                                                for (var i = 0; i < response.length; i++) {
                                                    if (!customFields.includes(response[i].ModuleName)) {
                                                        customFields.push(response[i].ModuleName);
                                                    }
                                                    if (response[i].ShowInBrowse && !customFieldsBrowse.includes(response[i].ModuleName)) {
                                                        customFieldsBrowse.push({
                                                            'moduleName': response[i].ModuleName,
                                                            'fieldName': response[i].FieldName,
                                                            'browsewidth': response[i].BrowseSizeInPixels
                                                        });
                                                    }
                                                }
                                                sessionStorage.setItem('customFields', JSON.stringify(customFields));
                                                sessionStorage.setItem('customFieldsBrowse', JSON.stringify(customFieldsBrowse));
                                                program.navigate('home');
                                            }, function onError(response) {
                                                program.navigate('home');
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
            .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
        ;
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
    };
    ;
    Base.prototype.getAboutScreen = function () {
        var viewModel = {};
        var properties = {};
        var screen = {};
        screen = FwBasePages.getAboutScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        screen.$view
            .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
        ;
        return screen;
    };
    ;
    Base.prototype.getSupportScreen = function () {
        var viewModel = {};
        var properties = {};
        var screen = {};
        screen = FwBasePages.getSupportScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    };
    ;
    Base.prototype.getPasswordRecoveryScreen = function () {
        var viewModel = {};
        var properties = {};
        var screen = {};
        screen = FwBasePages.getPasswordRecoveryScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    };
    ;
    return Base;
}());
var RwBaseController = new Base();
//# sourceMappingURL=Base.js.map