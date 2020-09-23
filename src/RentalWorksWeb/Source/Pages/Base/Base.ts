class Base {
    //----------------------------------------------------------------------------------------------
    getDefaultScreen() {
        const viewModel = {
            captionProgramTitle: Constants.appCaption,
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version
        };
        const screen = FwBasePages.getDefaultScreen(viewModel);
        document.title = Constants.appCaption;
        screen.$view
            .on('click', '.btnLogin', function () {
                try {
                    program.navigate('login');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .find('.programlogo').empty().html(`<div class="bgothm">${Constants.appTitle}</div>`);

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    getLoginScreen() {
        const viewModel = {
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
            valueVersion: applicationConfig.version,
            OktaEnabled: applicationConfig.OktaEnabled
        };
        let screen: any = {};
        if ((typeof applicationConfig.customLogin != 'undefined') && (applicationConfig.customLogin == true)) {
            screen = window[`Rw${applicationConfig.client}Controller`]['getLoginScreen']();
        } else {
            screen = FwBasePages.getLoginScreen(viewModel);
        }

        if (viewModel.OktaEnabled) {
            OktaLoginInstance.loadOktaLogin();
        } else {
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
                                },
                                timeout: 90000   //04/27/2020 justin hoffman GH#1888 - the first login for the day will take longer as the hosted API needs to cold start
                            });

                            if ((responseJwt.statuscode == 0) && (typeof responseJwt.access_token !== 'undefined')) {
                                sessionStorage.setItem('app', 'rentalworks');
                                localStorage.setItem('email', $email.val()); // mv 5/10/19 - this is the email or login and cannot be used to display the email address
                                if (typeof responseJwt.exception !== 'undefined') {
                                    if (applicationConfig.debugMode) {
                                        $loginWindow.find('.errormessage').html('').html(responseJwt.exception + responseJwt.stacktrace).show();
                                    } else {
                                        $loginWindow.find('.errormessage').html('').html(responseJwt.exception).show();
                                    }
                                } else {
                                    if (responseJwt.resetpassword) {
                                        sessionStorage.setItem('resetPasswordToken', responseJwt.access_token);
                                        sessionStorage.setItem('redirectPath', 'changepassword');
                                        program.navigate('changepassword');
                                    } else {
                                        sessionStorage.setItem('apiToken', responseJwt.access_token);
                                        await program.loadSession($loginWindow);
                                        // set redirectPath to navigate user to default home page, still need to go to the home page to run startup code if the user refreshes the browser
                                        let homePagePath = JSON.parse(sessionStorage.getItem('homePage')).path;
                                        if (homePagePath !== null && homePagePath !== '') {
                                            sessionStorage.setItem('redirectPath', homePagePath);
                                        }
                                        program.navigate('home');
                                    }
                                }
                            } else if (responseJwt.statuscode !== 0) {
                                $loginWindow.find('.errormessage').html('').html(responseJwt.statusmessage).show();
                            }
                        }
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                .on('click', '.btnCancel', e => {
                    try {
                        program.navigate('default');
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                })
                .find('.programlogo').empty().html(`<div class="bgothm">${Constants.appTitle}</div>`);
        }

        screen.load = function () {
            setTimeout(() => {
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
    getHubSpotInstallScreen() {
        let viewModel = {};
        let properties = {};
        let screen: any = {};
        let installUrl = "https://app.hubspot.com/oauth/authorize?scope=contacts&redirect_uri=http://localhost:57949/webdev/#/hubspotoauth&client_id=dca2d2a7-e71e-4311-b8bc-c49083f03194";

        screen = FwBasePages.getHubSpotInstallScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;

        let installBtn = screen.$view.find('.btnHubSpot');

        installBtn.click(() => {
            window.location.href = installUrl;
        })

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    getHubSpotOauthCallbackScreen() {
        let viewModel = {};
        let properties = {};
        let screen: any = {};
        // get query param code to send in a request to exchange for an auth token
        let callBackParams = new URLSearchParams(window.location.search);
        let callBackCode = callBackParams.get('code');

        console.log(callBackCode);

        screen = FwBasePages.getHubSpotOauthCallback(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;


        return program.navigate('default');
    }
    //----------------------------------------------------------------------------------------------
    getChangePasswordScreen() {
        let viewModel = {
            captionEmail: RwLanguages.translate('E-mail / Username'),
            captionPassword: RwLanguages.translate('Password'),
            valueEmail: (localStorage.getItem('email') ? localStorage.getItem('email') : ''),
            valuePassword: '',
            captionBtnSubmit: RwLanguages.translate('Submit'),
            captionBtnCancel: RwLanguages.translate('Cancel'),
            valueYear: new Date().getFullYear(),
            valueVersion: applicationConfig.version,
        };
        let properties = {};
        let screen: any = {};
        screen = FwBasePages.getChangePasswordScreen(viewModel);
        screen.viewModel = viewModel;
        screen.properties = properties;
        screen.$view.find('.programlogo').empty().html(`<div class="bgothm">${Constants.appTitle}</div>`);

        screen.$view
            .on('click', '.btnSubmit', async (e: JQuery.ClickEvent) => {
                try {
                    const $loginWindow = screen.$view.find('.login-container');
                    const $newPassword = screen.$view.find('#new-password');
                    const $confirmNewPassword = screen.$view.find('#confirm-new-password');

                    $newPassword.parent().removeClass('error');
                    $confirmNewPassword.parent().removeClass('error');
                    if ($newPassword.val() == '') {
                        $newPassword.parent().addClass('error');
                    } else if ($confirmNewPassword.val == '') {
                        $confirmNewPassword.parent().addClass('error');
                    } else if ($newPassword.val() !== $confirmNewPassword.val()) {
                        $confirmNewPassword.parent().addClass('error');
                        $loginWindow.find('.errormessage').html('').html("Password does not match!").show(); 
                    } else {
                        const responseReset = await FwAjax.callWebApi<any, any>({
                            httpMethod:      'POST',
                            url:             `${applicationConfig.apiurl}api/v1/account/resetpassword`,
                            $elementToBlock: $loginWindow,
                            data: {
                                Password: $newPassword.val()
                            },
                            addAuthorizationHeader: false,
                            requestHeaders: {
                                'Authorization': 'Bearer ' + sessionStorage.getItem('resetPasswordToken')
                            }
                        });

                        if (responseReset.Status == 0) {
                            sessionStorage.clear();
                            const responseJwt = await FwAjax.callWebApi<any, any>({
                                httpMethod: 'POST',
                                url: `${applicationConfig.apiurl}api/v1/jwt`,
                                $elementToBlock: $loginWindow,
                                data: {
                                    UserName: localStorage.getItem('email'),
                                    Password: $newPassword.val()
                                }
                            });

                            if ((responseJwt.statuscode == 0) && (typeof responseJwt.access_token !== 'undefined')) {
                                if (typeof responseJwt.exception !== 'undefined') {
                                    if (applicationConfig.debugMode) {
                                        $loginWindow.find('.errormessage').html('').html(responseJwt.exception + responseJwt.stacktrace).show();
                                    } else {
                                        $loginWindow.find('.errormessage').html('').html(responseJwt.exception).show();
                                    }
                                } else {
                                    sessionStorage.setItem('apiToken', responseJwt.access_token);
                                    await program.loadSession($loginWindow);

                                    // set redirectPath to navigate user to default home page, still need to go to the home page to run startup code if the user refreshes the browser
                                    let homePagePath = JSON.parse(sessionStorage.getItem('homePage')).path;
                                    if (homePagePath !== null && homePagePath !== '') {
                                        sessionStorage.setItem('redirectPath', homePagePath);
                                    }
                                    program.navigate('home');
                                }
                            } else if (responseJwt.statuscode !== 0) {
                                $loginWindow.find('.errormessage').html('').html(responseJwt.statusmessage).show();
                            }
                        } else {
                            $loginWindow.find('.errormessage').html('').html(responseReset.statusmessage).show();
                        }
                    }
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            })
            .on('click', '.btnCancel', function (e) {
                try {
                    sessionStorage.clear();
                    program.navigate('default');
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            });

        return screen;
    }
    //----------------------------------------------------------------------------------------------
}
var RwBaseController = new Base();
