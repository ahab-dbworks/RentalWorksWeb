class Base {
    //----------------------------------------------------------------------------------------------
    getDefaultScreen() {
        var viewModel = {
            captionProgramTitle: 'RentalWorks',
            valueYear:           new Date().getFullYear(),
            valueVersion:        applicationConfig.version
        };
        var screen: any = {};
        screen = FwBasePages.getDefaultScreen(viewModel);

        screen.$view
            .on('click', '.btnLogin', function() {
                try {
                    program.navigate('login');
                } catch(ex) {
                    FwFunc.showError(ex);
                }
            })
            //.find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
            .find('.programlogo').empty().html('<div class="bgothm">Rental<span class="rwpurple">Works<span></div>')
        ;

        return screen;
    };
    //----------------------------------------------------------------------------------------------
    getLoginScreen() {
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
        var screen: any = {};
        if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
            screen = window['Rw' + applicationConfig.client + 'Controller']['getLoginScreen']();
        } else {
            screen = FwBasePages.getLoginScreen(viewModel);
        }

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
                        var apiRequest = {
                            UserName: $email.val(),
                            Password: $password.val()
                        };
                        FwAppData.apiMethod(false, "POST", "api/v1/jwt", apiRequest, null, function onSuccess(responseRestApi) {
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
                                                localStorage.setItem('email', request.email); // mv 2018-08-19 I suspect that this is really not the email, it's the email OR the regular user login the user entered.
                                                sessionStorage.setItem('email',              responseOriginalApi.webUser.email);
                                                sessionStorage.setItem('authToken',          responseOriginalApi.authToken);
                                                sessionStorage.setItem('fullname',           responseOriginalApi.webUser.fullname);
                                                sessionStorage.setItem('name',               responseOriginalApi.webUser.name);  //justin 05/06/2018
                                                sessionStorage.setItem('usersid',            responseOriginalApi.webUser.usersid);  //justin 05/25/2018  //C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
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
                                        
                                                //J.Pace 08/14/2018 user's sound settings\
                                                //setTimeout(() => {
                                                    FwAppData.apiMethod(true, 'GET', `api/v1/usersettings/${responseOriginalApi.webUser.webusersid.webusersid}`, null, FwServices.defaultTimeout, function onSuccess(response) {
                                                        let sounds: any = {};
                                                        sounds.successSoundFileName = response.SuccessSoundFileName;
                                                        sounds.errorSoundFileName = response.ErrorSoundFileName;
                                                        sounds.notificationSoundFileName = response.NotificationSoundFileName;
                                                        sessionStorage.setItem('sounds', JSON.stringify(sounds));

                                                        // Get custom fields and assign to session storage
                                                        FwAppData.apiMethod(true, 'GET', 'api/v1/customfield/', null, FwServices.defaultTimeout, function onSuccess(response) {
                                                            var customFields = [];
                                                            var customFieldsBrowse = [];
                                                            for (var i = 0; i < response.length; i++) {
                                                                if (customFields.indexOf(response[i].ModuleName) === -1) {
                                                                    customFields.push(response[i].ModuleName);
                                                                }
                                                                if (response[i].ShowInBrowse && customFieldsBrowse.indexOf(response[i].ModuleName) === -1) {
                                                                    var customFieldObject = {
                                                                        'moduleName': response[i].ModuleName,
                                                                        'fieldName': response[i].FieldName,
                                                                        'browsewidth': response[i].BrowseSizeInPixels,
                                                                        'digits': response[i].FloatDecimalDigits,
                                                                        'datatype': response[i].ControlType
                                                                    };
                                                                    customFieldsBrowse.push(customFieldObject);
                                                                }
                                                            }
                                                            sessionStorage.setItem('customFields', JSON.stringify(customFields));
                                                            sessionStorage.setItem('customFieldsBrowse', JSON.stringify(customFieldsBrowse));
                                                            program.navigate('home');
                                                        }, function onError(response) {
                                                            FwFunc.showError(response);
                                                            program.navigate('home');
                                                        }, null);

                                                    }, function onError(response) {
                                                        FwFunc.showError(response);
                                                    }, null);
                                                //}, 0);

                                                let webformrequest:any = {};
                                                webformrequest.uniqueids = {
                                                    WebUserId: responseOriginalApi.webUser.webusersid.webusersid
                                                };
                                                FwAppData.apiMethod(true, 'POST', `api/v1/webform/browse`, webformrequest, FwServices.defaultTimeout, function onSuccess(response) {
                                                    let webForms = [];
                                                    for (let i = 0; i < response.Rows.length; i++) {
                                                        webForms.push({
                                                            'WebFormId': response.Rows[i][0],
                                                            'BaseForm': response.Rows[i][1],
                                                            'Html': response.Rows[i][4]
                                                        });
                                                    }
                                                    sessionStorage.setItem('webform', JSON.stringify(webForms));
                                                }, function onError(response) {
                                                    FwFunc.showError(response);
                                                }, null);

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
                                    } catch (ex) {
                                        FwFunc.showError(ex);
                                    }
                                });
                            } else if (responseRestApi.statuscode !== 0) {
                                $loginWindow.find('.errormessage').html('').html(responseRestApi.statusmessage).show();
                            }
                        }, null, $loginWindow);
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
            //.find('#programlogo').attr('src', 'theme/images/rentalworkslogo.png');
            .find('.programlogo').empty().html('<div class="bgothm">Rental<span class="rwpurple">Works<span></div>')
            ;

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
