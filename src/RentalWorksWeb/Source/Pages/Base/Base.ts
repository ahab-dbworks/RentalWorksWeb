class Base {
    //----------------------------------------------------------------------------------------------
    getDefaultScreen() {
        var viewModel = {
            captionProgramTitle: 'RentalWorks'
        };
        var properties = {}
        var screen: any = {};
        screen = FwBasePages.getDefaultScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;

        var $footerView = RwMasterController.getFooterView(viewModel, properties);
        screen.$view.find('#master-footer').append($footerView);

        screen.$view
            .on('click', '.btnLogin', function() {
                try {
                    program.navigate('login');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
        ;

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    getLoginScreen() {
        var valueEmail;
        if (localStorage.getItem('email')) {
            valueEmail = localStorage.getItem('email');
        } else {
            valueEmail = '';
        }
        var viewModel = {
            captionPanelLogin:       'RentalWorks Login'
          , captionEmail:            RwLanguages.translate('E-mail / Username')
          , valueEmail:              valueEmail
          , captionPassword:         RwLanguages.translate('Password')
          , valuePassword:           ''
          , captionBtnLogin:         RwLanguages.translate('Sign In')
          , captionBtnCancel:        RwLanguages.translate('Cancel')
          , valueVersion:            applicationConfig.version
          , captionPasswordRecovery: RwLanguages.translate('Recover Password')
          , captionAbout:            RwLanguages.translate('About')
          , captionSupport:          RwLanguages.translate('Support')
        };
        var properties = {};
        var screen: any = {};
        screen.viewModel = viewModel;
        screen.properties = properties;
        if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
            screen = window['Rw' + applicationConfig.client + 'Controller']['getLoginScreen']();
        } else {
            screen = FwBasePages.getLoginScreen(viewModel);
        }

        var $footerView = RwMasterController.getFooterView(viewModel, properties);
        screen.$view.find('#master-footer').append($footerView);

        screen.$view
            .on('click', '.btnLogin', function(e) {
                var $email, $password, exception, $loginWindow;
                try {
                    $loginWindow = screen.$view.find('.login-container');
                    $email       = screen.$view.find('#email');
                    $password    = screen.$view.find('#password');
                    $email.parent().removeClass('error');
                    $password.parent().removeClass('error');
                    if ($email.val() == '') {
                        $email.parent().addClass('error');
                    } else if ($password.val() == '') {
                        $password.parent().addClass('error');
                    } else {
                        sessionStorage.clear();

                        // get a token to connect to RentalWorksWebApi
                        var requiresAuthToken = false;
                        var apiRequest = {
                            UserName: $email.val(),
                            Password: $password.val()
                        };
                        var onError = null;
                        var $elementToBlock = $loginWindow;
                        FwAppData.apiMethod(requiresAuthToken, "POST", "api/v1/jwt", apiRequest, null, function onSuccess(responseRestApi) {
                            if ((responseRestApi.statuscode == 0) && (typeof responseRestApi.access_token !== 'undefined')) {
                                sessionStorage.setItem('apiToken', responseRestApi.access_token);
                                // get a token to connect to RentalWorksWeb
                                var request = {
                                    email:    $email.val(),
                                    password: $password.val()
                                };
                                FwServices.account.getAuthToken(request, $loginWindow, function (responseOriginalApi) {
                                    try {
                                        if (typeof responseOriginalApi.exception !== 'undefined') {
                                            if (applicationConfig.debugMode) {
                                                //throw new Error(responseOriginalApi.exception + responseOriginalApi.stacktrace);
                                                $loginWindow.find('.errormessage').html('').html(responseOriginalApi.exception + responseOriginalApi.stacktrace).show();
                                            } else {
                                                //throw new Error(responseOriginalApi.exception);
                                                $loginWindow.find('.errormessage').html('').html(responseOriginalApi.exception).show();
                                            }
                                        } else {
                                            if ((responseOriginalApi.errNo === 0) && (typeof responseOriginalApi.authToken !== 'undefined')) {
                                                localStorage.setItem('email',                request.email);
                                                sessionStorage.setItem('authToken',          responseOriginalApi.authToken);
                                                sessionStorage.setItem('fullname',           responseOriginalApi.webUser.fullname);
                                                sessionStorage.setItem('browsedefaultrows',  responseOriginalApi.webUser.browsedefaultrows);
                                                sessionStorage.setItem('applicationtheme',   responseOriginalApi.webUser.applicationtheme);
                                                sessionStorage.setItem('lastLoggedIn',       new Date().toLocaleTimeString());
                                                sessionStorage.setItem('serverVersion',      responseOriginalApi.serverVersion);
                                                sessionStorage.setItem('applicationOptions', JSON.stringify(responseOriginalApi.applicationOptions));
                                                sessionStorage.setItem('userType',           responseOriginalApi.webUser.usertype);
                                                sessionStorage.setItem('applicationtree',    JSON.stringify(responseOriginalApi.applicationtree.Result));
                                                sessionStorage.setItem('siteName',           responseOriginalApi.site.name);
                                                sessionStorage.setItem('clientCode',         responseOriginalApi.clientcode);
                                                sessionStorage.setItem('location',           JSON.stringify(responseOriginalApi.webUser.location));
                                                sessionStorage.setItem('warehouse',          JSON.stringify(responseOriginalApi.webUser.warehouse));
                                                sessionStorage.setItem('department',         JSON.stringify(responseOriginalApi.webUser.department));
                                                sessionStorage.setItem('userid',             JSON.stringify(responseOriginalApi.webUser.webusersid));
                                                jQuery('html').removeClass('theme-material'); 
                                                program.navigate('home');
                                            } else if (responseOriginalApi.errNo !== 0) {
                                                //throw new Error(responseOriginalApi.errMsg);
                                                $loginWindow.find('.errormessage').html('').html(responseOriginalApi.errMsg).show();
                                            } else if (typeof responseOriginalApi.authToken === 'undefined') {
                                                if (applicationConfig.debugMode) {
                                                    //throw new Error('FwServices.account.getAuthToken: responseOriginalApi.authToken is undefined.');
                                                    $loginWindow.find('.errormessage').html('').html('FwServices.account.getAuthToken: responseOriginalApi.authToken is undefined.').show();
                                                } else {
                                                    //throw new Error(responseOriginalApi.exception);
                                                    $loginWindow.find('.errormessage').html('').html('There is an issue with the authorization token.').show();
                                                }
                                            }
                                        }
                                    } catch(ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } else if (responseRestApi.statuscode !== 0) {
                                $loginWindow.find('.errormessage').html('').html(responseRestApi.statusmessage).show();
                            }
                        }, onError, $elementToBlock);
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnCancel', function(e) {
                try {
                    program.navigate('default');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
        ;

        screen.load = function() {
            setTimeout(function() {
                if (screen.$view.find('#email').val() == '') {
                    screen.$view.find('#email').focus();
                } else {
                    screen.$view.find('#password').focus();
                }
            }, 0);
        };

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    getAboutScreen() {
        var viewModel = {};
        var properties = {};
        var screen: any = {};
        screen = FwBasePages.getAboutScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        screen.$view
            .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
        ;
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    getSupportScreen() {
        var viewModel = {};
        var properties = {};
        var screen: any = {};
        screen = FwBasePages.getSupportScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    };
    //----------------------------------------------------------------------------------------------
    getPasswordRecoveryScreen() {
        var viewModel = {};
        var properties = {};
        var screen: any = {};
        screen = FwBasePages.getPasswordRecoveryScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        return screen;
    };
    //----------------------------------------------------------------------------------------------
}
var RwBaseController = new Base();
