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
                    FwAppData.apiMethod(requiresAuthToken, method, url, apiRequest, timeoutSeconds, function onSuccess(response) {
                        if ((response.statuscode == 0) && (typeof response.access_token !== 'undefined')) {
                            sessionStorage.setItem('apiToken', response.access_token);
                            // get a token to connect to RentalWorksWeb
                            var request = {
                                email:    $email.val(),
                                password: $password.val()
                            };
                            FwServices.account.getAuthToken(request, $loginWindow, function (response) {
                                try {
                                    if (typeof response.exception !== 'undefined') {
                                        if (applicationConfig.debugMode) {
                                            //throw new Error(response.exception + response.stacktrace);
                                            $loginWindow.find('.errormessage').html('').html(response.exception + response.stacktrace).show();
                                        } else {
                                            //throw new Error(response.exception);
                                            $loginWindow.find('.errormessage').html('').html(response.exception).show();
                                        }
                                    } else {
                                        if ((response.errNo === 0) && (typeof response.authToken !== 'undefined')) {
                                            localStorage.setItem('email',                request.email);
                                            sessionStorage.setItem('authToken',          response.authToken);
                                            sessionStorage.setItem('fullname',           response.webUser.fullname);
                                            sessionStorage.setItem('browsedefaultrows',  response.webUser.browsedefaultrows);
                                            sessionStorage.setItem('applicationtheme',   response.webUser.applicationtheme);
                                            sessionStorage.setItem('lastLoggedIn',       new Date().toLocaleTimeString());
                                            sessionStorage.setItem('serverVersion',      response.serverVersion);
                                            sessionStorage.setItem('applicationOptions', JSON.stringify(response.applicationOptions));
                                            sessionStorage.setItem('userType',           response.webUser.usertype);
                                            sessionStorage.setItem('applicationtree',    JSON.stringify(response.applicationtree.Result));
                                            sessionStorage.setItem('siteName',           response.site.name);
                                            sessionStorage.setItem('clientCode',         response.clientcode);
                                            sessionStorage.setItem('location',           JSON.stringify(response.webUser.location));
                                            jQuery('html').removeClass('theme-material'); 
                                            program.navigate('home');
                                        } else if (response.errNo !== 0) {
                                            //throw new Error(response.errMsg);
                                            $loginWindow.find('.errormessage').html('').html(response.errMsg).show();
                                        } else if (typeof response.authToken === 'undefined') {
                                            if (applicationConfig.debugMode) {
                                                //throw new Error('FwServices.account.getAuthToken: response.authToken is undefined.');
                                                $loginWindow.find('.errormessage').html('').html('FwServices.account.getAuthToken: response.authToken is undefined.').show();
                                            } else {
                                                //throw new Error(response.exception);
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
