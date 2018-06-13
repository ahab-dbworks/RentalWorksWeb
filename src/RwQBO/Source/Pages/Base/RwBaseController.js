//----------------------------------------------------------------------------------------------
var RwBaseController = {};
//----------------------------------------------------------------------------------------------
RwBaseController.getDefaultScreen = function() {
    var viewModel = {
        captionProgramTitle: 'RentalWorks',
        valueYear:           new Date().getFullYear(),
        valueVersion:        applicationConfig.version
    };
    var screen = {};
    screen = FwBasePages.getDefaultScreen(viewModel);

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
RwBaseController.getLoginScreen = function() {
    var viewModel = {
        captionPanelLogin:       'RentalWorks Login',
        captionEmail:            RwLanguages.translate('E-mail / Username'),
        valueEmail:              (localStorage.getItem('email') ? localStorage.getItem('email') : ''),
        captionPassword:         RwLanguages.translate('Password'),
        valuePassword:           '',
        captionBtnLogin:         RwLanguages.translate('Sign In'),
        captionBtnCancel:        RwLanguages.translate('Cancel'),
        captionPasswordRecovery: RwLanguages.translate('Recover Password'),
        captionAbout:            RwLanguages.translate('About'),
        captionSupport:          RwLanguages.translate('Support'),
        valueYear:               new Date().getFullYear(),
        valueVersion:            applicationConfig.version
    };
    var screen = {};
    if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
        screen = window['Rw' + applicationConfig.client + 'Controller']['getLoginScreen']();
    } else {
        screen = FwBasePages.getLoginScreen(viewModel);
    }

    screen.$view
        .on('click', '.btnLogin', function(e) {
            var request, $email, $password, exception, $loginWindow;
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
                    request = {
                        email:    $email.val(),
                        password: $password.val()
                    };
                    FwServices.account.getAuthToken(request, $loginWindow, function(response) {
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
                                    sessionStorage.setItem('warehouse',          JSON.stringify(response.webUser.warehouse));
                                    sessionStorage.setItem('department',         JSON.stringify(response.webUser.department));
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
