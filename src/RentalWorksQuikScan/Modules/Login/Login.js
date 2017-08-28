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
        captionLogin:         'RentalWorks QuikScan'
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
        screen.$view.find('#loginView-txtEmail')
            .addClass('readonly')
            .prop('readonly', true)
            .val(applicationConfig.demoEmail)
        ;
        screen.$view.find('#loginView-txtPassword')
            .addClass('readonly')
            .prop('readonly', true)
            .val(applicationConfig.demoPassword)
        ;
    } else {
        screen.$view.find('#loginView-btnHome').hide();
    }
    screen.$view
        .on('click', '#mobilelogin-btnLogin', function() {
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

                    request = {
                        email:    $email.val(),
                        password: $password.val()
                    };
                    RwServices.account.getAuthToken(request, function(response) {
                        try {
                            if (response.errNo !== 0) {
                                throw new Error(response.errMsg);
                            }
                            if (typeof response.exception !== 'undefined') {
                                if (applicationConfig.debugMode) {
                                    throw new Error(response.exception + response.stacktrace);
                                } else {
                                    throw new Error(response.exception);
                                }
                            }
                            if (typeof response.authToken === 'undefined') {
                                throw new Error('RwServices.account.getAuthToken: response.authToken is undefined.');
                            }
                            //localStorage.setItem('email', request.email);
                            sessionStorage.setItem('authToken', response.authToken);
                            sessionStorage.setItem('fullname', response.webUser.fullname);
                            sessionStorage.setItem('lastLoggedIn', new Date().toLocaleTimeString());
                            sessionStorage.setItem('serverVersion', response.serverVersion);
                            sessionStorage.setItem('clientcode', response.clientcode);
                            sessionStorage.setItem('iscrew', response.webUser.iscrew);
                            sessionStorage.setItem('applicationOptions', JSON.stringify(response.applicationOptions));
                            sessionStorage.setItem('stagingSuspendedSessionsEnabled', response.stagingSuspendedSessionsEnabled);
                            sessionStorage.setItem('applicationtree', JSON.stringify(response.applicationtree.Result));
                            var barcodeskipprefixes = '';
                            if (typeof response.barcodeskipprefixes === 'object') {
                                for (var i = 0; i < response.barcodeskipprefixes.Rows.length; i++) {
                                    var prefix = response.barcodeskipprefixes.Rows[i][0];
                                    if (barcodeskipprefixes.length > 0) {
                                        barcodeskipprefixes += ',';
                                    }
                                    barcodeskipprefixes += prefix;
                                }
                                sessionStorage.setItem('skipBarcodePrefixes', barcodeskipprefixes);
                            }
                            sessionStorage.setItem('userType', response.webUser.usertype);
                            if (response.webUser.usertype === 'USER') {
                                sessionStorage.setItem('users_enablecreatecontract', (response.user.enablecreatecontract === 'T') ? 'T' : 'F');
                                sessionStorage.setItem('users_qsallowapplyallqtyitems', (response.user.qsallowapplyallqtyitems === 'T') ? 'T' : 'F');
                            }
                            //else if (response.webUser.usertype === 'CONTACT') {}
                            if (response.clientcode === 'MARVEL') {
                                localStorage.setItem('currentCulture', 'marvel');
                                RwLanguages.currentCulture = localStorage.getItem('currentCulture');
                            } else {
                                localStorage.setItem('currentCulture', 'enUs');
                            }
                            sessionStorage.setItem('siteName', response.site.name);
                            application.navigate('home/home');
                        } catch(ex) {
                            FwFunc.showError(ex);
                        }
                    });
                }
            } catch(ex) {
                FwFunc.showError(ex);
            }
        })
        .on('keydown', '.mobilelogin-email-value, .mobilelogin-password-value', function() {
            var $this = jQuery(this);
            if ($this.parent().hasClass('error')) {
                $this.parent().removeClass('error');
            }
        })
        .on('click', '#loginView-btnHome', function() {
            window.history.back(1);
        })
        .on('click', '#loginView-btnSupport', function() {
            application.navigate('account/support');
        })
        .on('click', '#loginView-btnPrivacyPolicy', function() {
            application.navigate('account/privacypolicy');
        })
        .on('click', '#loginView-btnRefresh', function() {
            window.location.reload(true);
        })
        .find('.programlogo').append('<div class="apptitle bgothm center" style="padding-top:2vh;font-size:40px;">Rental<span style="color:#6f30b3;">Works</span><br />QuikScan</div>')
    ;

    screen.$view.find('.mobilelogin-footer').append('<div id="copyright">Copyright ' + new Date().getFullYear() + ' <span id="dbworkslink">Database Works</span>.&nbsp;All Rights Reserved.</div><div id="version">RentalWorks QuikScan v' + applicationConfig.version + '</div>');
    
    screen.load = function() {
        var $txtEmail, $txtPassword;
        if (!Modernizr.touch)
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