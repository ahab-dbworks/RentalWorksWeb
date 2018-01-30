//----------------------------------------------------------------------------------------------
var RwBaseController = {};
//----------------------------------------------------------------------------------------------
RwBaseController.getDefaultScreen = function(viewModel, properties) {
    var combinedViewModel, screen, $footerView;
    combinedViewModel = jQuery.extend({
        captionProgramTitle: 'RentalWorks'
    }, viewModel);
    //combinedViewModel.htmlPageBody = Mustache.render('', combinedViewModel);
    screen = {};
    screen = FwBasePages.getDefaultScreen(combinedViewModel);
    screen.viewModel = viewModel;
    screen.properties = properties;

    $footerView = RwMasterController.getFooterView();
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
RwBaseController.getLoginScreen = function(viewModel, properties) {
    var combinedViewModel, screen, valueEmail;
    if (localStorage.getItem('email')) {
        valueEmail = localStorage.getItem('email');
    } else {
        valueEmail = '';
    }
    combinedViewModel = jQuery.extend({
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
    }, viewModel);
    //combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-base-login').html(), combinedViewModel);
    screen = {};
    screen.viewModel = viewModel;
    screen.properties = properties;
    if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
        screen = window['Rw' + applicationConfig.client + 'Controller']['getLoginScreen']();
    } else {
        screen = FwBasePages.getLoginScreen(combinedViewModel);
    }

    $footerView = RwMasterController.getFooterView();
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
                    var method = "POST";
                    var url = "api/v1/jwt";
                    var apiRequest = {
                        UserName: $email.val(),
                        Password: $password.val()
                    };
                    var timeoutSeconds = null;
                    var onError = null;
                    var $elementToBlock = $loginWindow;
                    FwAppData.apiMethod(requiresAuthToken, method, url, apiRequest, timeoutSeconds, function onSuccess(responseRestApi) {
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
                        } else if (response.statuscode !== 0) {
                            $loginWindow.find('.errormessage').html('').html(response.statusmessage).show();
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
        .on('click', '#fwlogin-btnPasswordRecovery', function(e) {
            try {
                alert('Not Implemented');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#fwlogin-btnAbout', function(e) {
            try {
                alert('Not Implemented');
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('click', '#fwlogin-btnSupport', function(e) {
            try {
                alert('Not Implemented');
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
RwBaseController.getAboutScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({

        }, viewModel);
    //combinedViewModel.htmlPageBody = Mustache.render('', combinedViewModel);
    screen = {};
    screen = FwBasePages.getAboutScreen(combinedViewModel);
    screen.viewModel = viewModel;
    screen.properties = properties;

    screen.$view
        .find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
    ;

    return screen;
};
//----------------------------------------------------------------------------------------------
RwBaseController.getSupportScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({

        }, viewModel);
    //combinedViewModel.htmlPageBody = Mustache.render('', combinedViewModel);
    screen = {};
    screen = FwBasePages.getSupportScreen(combinedViewModel);
    screen.viewModel = viewModel;
    screen.properties = properties;

    screen.$view

    ;

    return screen;
};
//----------------------------------------------------------------------------------------------
RwBaseController.getPasswordRecoveryScreen = function(viewModel, properties) {
    var combinedViewModel, screen;
    combinedViewModel = jQuery.extend({

        }, viewModel);
    //combinedViewModel.htmlPageBody = Mustache.render('', combinedViewModel);
    screen = {};
    screen = FwBasePages.getPasswordRecoveryScreen(combinedViewModel);
    screen.viewModel = viewModel;
    screen.properties = properties;

    screen.$view

    ;

    return screen;
};
//----------------------------------------------------------------------------------------------
