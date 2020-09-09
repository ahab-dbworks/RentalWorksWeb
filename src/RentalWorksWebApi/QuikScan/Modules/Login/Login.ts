//----------------------------------------------------------------------------------------------
RwAccountController.getLoginScreen = function(viewModel, properties) {
    var screen, combinedViewModel, valueEmail;
    if (localStorage.getItem('email')) {
        valueEmail = localStorage.getItem('email');
    }
    else {
        valueEmail = '';
    }
    combinedViewModel = jQuery.extend({
        captionLogin:         'RentalWorks QuikScan<span style="vertical-align:super;font-size:8px;">&reg;</span>'
      , captionEmail:         RwLanguages.translate('Login')
      , valueEmail:           valueEmail
      , captionPassword:      RwLanguages.translate('Password')
      , valuePassword:        ''
      , captionBtnLogin:      RwLanguages.translate('Login')
      , captionHome:          RwLanguages.translate('Home')
      , captionSupport:       RwLanguages.translate('Support')
      , captionPrivacyPolicy: RwLanguages.translate('Privacy Policy')
      , captionRefresh:       RwLanguages.translate('Refresh')
    }, viewModel);
    //combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-account-login').html(), combinedViewModel);
    screen = {};
    screen.viewModel = viewModel;
    screen.properties = properties;
    screen = FwBasePages.getMobileLoginScreen(combinedViewModel);

    if (applicationConfig.demoMode) {
        //screen.$view.find('#loginView-btnRefresh').hide();
        screen.$view.find('.mobilelogin-email-value')
            .addClass('readonly')
            .prop('readonly', true)
            .val(applicationConfig.demoEmail)
        ;
        screen.$view.find('.mobilelogin-password-value')
            .addClass('readonly')
            .prop('readonly', true)
            .val(applicationConfig.demoPassword)
        ;
    } else {
        screen.$view.find('#loginView-btnHome').hide();
    }
    screen.$view
        .on('click', '#mobilelogin-btnLogin', async () => {
            var request, $email, $password, exception;
            try {
                $email    = jQuery('.mobilelogin-email-value');
                $password = jQuery('.mobilelogin-password-value');
                if ($email.val().length == 0) {
                    $email.parent().addClass('error');
                } else if ($password.val().length == 0) {
                    $password.parent().addClass('error');
                } else {
                    localStorage.setItem('email', $email.val());
                    sessionStorage.clear();
                    //-------------------------
                    var getJwtTokenRequest = new FwAjaxRequest();
                    //getJwtTokenRequest.$elementToBlock = screen.$view;
                    getJwtTokenRequest.addAuthorizationHeader = false;
                    getJwtTokenRequest.httpMethod = "POST";
                    getJwtTokenRequest.setWebApiUrl('/api/v1/jwt');
                    getJwtTokenRequest.data = {
                        UserName: $email.val(),
                        Password: $password.val()
                    };
                    var getJwtTokenResponse = await FwAjax.callWebApi<any, any>(getJwtTokenRequest);
                    if ((getJwtTokenResponse.statuscode == 0) && (typeof getJwtTokenResponse.access_token !== 'undefined')) {
                        sessionStorage.setItem('apiToken', getJwtTokenResponse.access_token);
                        sessionStorage.setItem('app', 'quikscan');
                        localStorage.setItem('email', $email.val()); // mv 5/10/19 - this is the email or login and cannot be used to display the email address
                        if (typeof getJwtTokenResponse.exception !== 'undefined') {
                            if (applicationConfig.debugMode) {
                                throw new Error(getJwtTokenResponse.exception + getJwtTokenResponse.stacktrace);
                            } else {
                                throw new Error(getJwtTokenResponse.exception);
                            }
                        } else {
                            // get session info
                            var getSessionRequest = new FwAjaxRequest();
                            getSessionRequest.httpMethod = "GET";
                            getSessionRequest.setWebApiUrl('/api/v1/account/session?applicationid={' + FwApplicationTree.currentApplicationId + '}');
                            var getSessionResponse = await FwAjax.callWebApi<any, any>(getSessionRequest);
                            sessionStorage.setItem('email', getSessionResponse.webUser.email);
                            sessionStorage.setItem('fullname', getSessionResponse.webUser.fullname);
                            sessionStorage.setItem('name', getSessionResponse.webUser.name);
                            sessionStorage.setItem('usersid', getSessionResponse.webUser.usersid);
                            sessionStorage.setItem('browsedefaultrows', getSessionResponse.webUser.browsedefaultrows);
                            sessionStorage.setItem('applicationtheme', getSessionResponse.webUser.applicationtheme);
                            sessionStorage.setItem('lastLoggedIn', new Date().toLocaleTimeString());
                            sessionStorage.setItem('serverVersion', getSessionResponse.serverVersion);
                            sessionStorage.setItem('applicationOptions', JSON.stringify(getSessionResponse.applicationOptions));
                            sessionStorage.setItem('userType', getSessionResponse.webUser.usertype);
                            sessionStorage.setItem('applicationtree', JSON.stringify(getSessionResponse.applicationtree));
                            sessionStorage.setItem('clientCode', getSessionResponse.clientcode);
                            sessionStorage.setItem('location', JSON.stringify(getSessionResponse.location));
                            sessionStorage.setItem('defaultlocation', JSON.stringify(getSessionResponse.location));
                            sessionStorage.setItem('warehouse', JSON.stringify(getSessionResponse.warehouse));
                            sessionStorage.setItem('department', JSON.stringify(getSessionResponse.department));
                            sessionStorage.setItem('webusersid', getSessionResponse.webUser.webusersid);
                            sessionStorage.setItem('userid', JSON.stringify(getSessionResponse.webUser));
                            sessionStorage.setItem('systemSettings', JSON.stringify(getSessionResponse.systemSettings));
                            if (getSessionResponse.webUser.usertype == 'CONTACT') {
                                sessionStorage.setItem('deal', JSON.stringify(getSessionResponse.deal));
                            }
                            let stagingSuspendedSessionEnabled = false;
                            if (getSessionResponse.systemSettings.QuikScanStageBySession !== undefined) {
                                stagingSuspendedSessionEnabled = getSessionResponse.systemSettings.QuikScanStageBySession;
                            }
                            sessionStorage.setItem('stagingSuspendedSessionsEnabled', stagingSuspendedSessionEnabled.toString());

                            var barcodeskipprefixes = '';
                            if (typeof getSessionResponse.systemSettings.BarcodeSkipPrefixes === 'object') {
                                for (var i = 0; i < getSessionResponse.systemSettings.BarcodeSkipPrefixes.Rows.length; i++) {
                                    var prefix = getSessionResponse.systemSettings.BarcodeSkipPrefixes.Rows[i][0];
                                    if (barcodeskipprefixes.length > 0) {
                                        barcodeskipprefixes += ',';
                                    }
                                    barcodeskipprefixes += prefix;
                                }
                                sessionStorage.setItem('skipBarcodePrefixes', barcodeskipprefixes);
                            }
                            if (getSessionResponse.webUser.usertype === 'USER') {
                                sessionStorage.setItem('users_enablecreatecontract', getSessionResponse.webUser.enablecreatecontract);
                                sessionStorage.setItem('users_qsallowapplyallqtyitems', getSessionResponse.webUser.qsallowapplyallqtyitems);
                            }
                            if (getSessionResponse.clientCode === 'MARVEL') {
                                localStorage.setItem('currentCulture', 'marvel');
                                RwLanguages.currentCulture = localStorage.getItem('currentCulture');
                            } else {
                                localStorage.setItem('currentCulture', 'enUs');
                            }

                            program.navigate('home/home');
                        }
                    } else {
                        throw new Error(getJwtTokenResponse.statusmessage);
                    }
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('keydown', '.mobilelogin-email-value, .mobilelogin-password-value', (e: JQuery.KeyDownEvent) => {
            var $this = jQuery(e.target);
            if ($this.parent().hasClass('error')) {
                $this.parent().removeClass('error');
            }
        })
        .on('click', '#loginView-btnHome', () => {
            window.history.back();
        })
        .on('click', '#loginView-btnSupport', () => {
            program.navigate('account/support');
        })
        .on('click', '#loginView-btnPrivacyPolicy', () => {
            program.navigate('account/privacypolicy');
        })
        .on('click', '#loginView-btnRefresh', () => {
            window.location.reload(true);
        })
        .find('.programlogo').append('<div class="apptitle bgothm center" style="padding-top:2vh;font-size:40px;">Rental<span style="color:#6f30b3;">Works</span><br />QuikScan<span style="vertical-align:super;font-size:8px;">&reg;</span></div>')
    ;

    screen.$view.find('.mobilelogin-footer').append('<div id="copyright">Copyright ' + new Date().getFullYear() + ' <span id="dbworkslink">Database Works</span>.&nbsp;All Rights Reserved.</div><div id="version">RentalWorks QuikScan v' + applicationConfig.version + '</div>');
    
    screen.load = () => {
        var $txtEmail, $txtPassword;
        if (!(<any>window).Modernizr.touch)
        {
            $txtEmail    = jQuery('.mobilelogin-email-value');
            $txtPassword = jQuery('.mobilelogin-password-value');
            if ($txtEmail.val() === '') {
                $txtEmail.focus();
            } else {
                $txtPassword.select();
            }
        }
        screen.$view.find('#mobilelogin-btnLogin').css('background-color', '#6f30b3');
    };
    return screen;
};