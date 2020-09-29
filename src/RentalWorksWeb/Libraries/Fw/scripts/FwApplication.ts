class FwApplication {
    name: string;
    title: string;
    screens: any[] = [];
    audioMode: string;
    audioSuccessArray: number[];
    audioErrorArray: number[];
    audioSuccess: HTMLAudioElement;
    audioError: HTMLAudioElement;
    localStoragePrefix: string;
    //---------------------------------------------------------------------------------
    constructor() {
        this.setAudioMode('NativeAudio');

        // inline templates when debugging by ajaxing for the src url
        var $templates = jQuery('script[data-ajaxload="true"]');
        $templates.each(function () {
            if (jQuery(this).attr("src")) {
                jQuery.ajax(
                    jQuery(this).attr("src"),
                    {
                        async: true,
                        context: this,
                        success: function (data) { jQuery(this).html(data); }
                    }
                );
            }
        });
    }
    //---------------------------------------------------------------------------------
    pushScreen(screen) {
        var me, applicationContainer, viewAnimationContainer, $previousView;

        FwAppData.abortAllRequests();
        me = this;
        applicationContainer = jQuery('#application');
        if ((this.screens.length > 0) && (applicationContainer.children().length > 0)) {
            this.screens[this.screens.length - 1].$view = applicationContainer.children().detach();
            if (typeof this.screens[this.screens.length - 1].unload !== 'undefined') {
                this.screens[this.screens.length - 1].unload();
            }
        }
        screen.$view.hide();
        applicationContainer
            .empty() // added this to get rid of the homepages doubling up in the support site.  Not sure about this line though, probably needs to get fixed in a better way.  MV 10/4/13
            .append(screen.$view)
            .removeClass('hidden');
        me.screens.push(screen);
        screen.$view
            .addClass('active')
            .show(0);
        jQuery(screen.scanTarget).addClass('scanTarget');
        if (typeof screen.load !== 'undefined') {
            screen.load();
        }
        document.body.scrollTop = 0;
    }
    //---------------------------------------------------------------------------------
    updateScreen(screen) {
        var me, applicationContainer, viewAnimationContainer, $previousView;

        FwAppData.abortAllRequests();
        me = this;
        applicationContainer = jQuery('#application');
        if (this.screens.length > 0) {
            if (typeof this.screens[this.screens.length - 1].unload !== 'undefined') {
                this.screens[this.screens.length - 1].unload();
            }

            this.screens[this.screens.length - 1] = screen;
        }
        screen.$view.hide(0, function () {
            screen.$view.hide();
            applicationContainer
                .empty()
                .append(screen.$view)
                .removeClass('hidden');
            screen.$view.show(0);
            if (typeof screen.load !== 'undefined') {
                screen.load();
            }
        });
    }
    //---------------------------------------------------------------------------------
    popScreen() {
        var applicationContainer, oldScreen, screen;
        FwAppData.abortAllRequests();
        applicationContainer = jQuery('#application');
        if (this.screens.length > 1) {
            if (applicationContainer.children().length > 0) {
                oldScreen = this.screens.pop();
                if (typeof oldScreen.unload !== 'undefined') {
                    oldScreen.unload();
                }
                if (this.screens.length > 0) {
                    screen = this.screens[this.screens.length - 1];
                    screen.$view.hide(0, function () {
                        applicationContainer.children().hide();
                        screen.$view.hide();
                        applicationContainer
                            .empty()
                            .append(screen.$view)
                            .removeClass('hidden');
                        if (typeof screen.load !== 'undefined') {
                            screen.load();
                        }
                        screen.$view.show(0);
                    });
                }
            }
        }
    }
    //---------------------------------------------------------------------------------
    setAudioMode(mode: 'none' | 'html5' | 'DTDevices' | 'NativeAudio'): void {
        this.audioMode = mode;
        switch (mode) {
            case 'DTDevices':
                this.audioSuccessArray = [1200, 300];
                this.audioErrorArray = [800, 200, 600, 200];
                break;
            case 'html5': ;
                if (typeof this.audioSuccess === 'undefined') {
                    this.audioSuccess = new Audio('theme/fwaudio/success2.wav');
                    this.audioError = new Audio('theme/fwaudio/error2.wav');
                }
                break;
            case 'NativeAudio':
                if ((<any>window).plugins && (<any>window).plugins.NativeAudio) {
                    // Preload audio resources
                    if (typeof (<any>window).plugins.NativeAudio.preloadedSounds !== 'boolean') {
                        (<any>window).plugins.NativeAudio.preloadedSounds = true;
                        (<any>window).plugins.NativeAudio.preloadSimple('success', 'audio/success.wav', function (msg) {
                        }, function (msg) {
                            //FwFunc.showError(msg);
                        });
                        (<any>window).plugins.NativeAudio.preloadSimple('error', 'audio/error.wav', function (msg) {
                        }, function (msg) {
                            //FwFunc.showError(msg);
                        });
                    }
                }
                break;
        }
    }
    //---------------------------------------------------------------------------------
    playStatus(isSuccessful: boolean): void {
        if (isSuccessful) {
            switch (this.audioMode) {
                case 'DTDevices':
                    (<any>window).DTDevices.playSound(this.audioSuccessArray);
                    break;
                case 'html5':
                    this.audioSuccess.currentTime = 0;
                    this.audioSuccess.play();
                    break;
                case 'NativeAudio':
                    if ((<any>window).plugins && (<any>window).plugins.NativeAudio) {
                        (<any>window).plugins.NativeAudio.play('success');
                    }
                    break;
            }
        } else {
            switch (this.audioMode) {
                case 'DTDevices':
                    (<any>window).DTDevices.playSound(this.audioErrorArray);
                    break;
                case 'html5':
                    this.audioError.currentTime = 0;
                    this.audioError.play();
                    break;
                case 'NativeAudio':
                    if ((<any>window).plugins && (<any>window).plugins.NativeAudio) {
                        (<any>window).plugins.NativeAudio.play('error');
                    }
                    break;
            }
        }
    }
    //---------------------------------------------------------------------------------
    setMedia(media) {
        localStorage.setItem('media', media);
    }
    //---------------------------------------------------------------------------------
    setScanMode(scanMode) {
        localStorage.setItem('scanMode', scanMode);
        if (typeof window['LineaScanner'] !== 'undefined') {
            window['LineaScanner'].setScanMode(parseInt(scanMode));
        }
    }
    //---------------------------------------------------------------------------------
    load() {
        var me = this;
        me.registerScripts();
        window.addEventListener("dragover", function (e) {
            e.preventDefault();
        }, false);
        window.addEventListener("drop", function (e) {
            e.preventDefault();
        }, false);
        // seems like we need to handle another window changing the browser storage area
        // not sure what's needed yet, but this event fires when it changes
        if (!localStorage.getItem('media') || (localStorage.getItem('media').length === 0)) {
            if (/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent)) {
                this.setMedia('mobile');
            } else {
                this.setMedia('desktop');
            }
        } else {
            var media = localStorage.getItem('media');
            this.setMedia(media);
        }

        //2020-09-11 MY: Remove when TrakitWorks is its own application
        if (applicationConfig.appCaption) {
            Constants.appCaption = applicationConfig.appCaption;
            program.name = applicationConfig.appCaption;
        }
        if (applicationConfig.appTitle) {
            Constants.appTitle = applicationConfig.appTitle;
            program.title = applicationConfig.appTitle;
        }
    }
    //---------------------------------------------------------------------------------
    registerScripts() {
        if (applicationConfig.OktaEnabled) {
            jQuery('<script>')
                .attr('type', 'text/javascript')
                .attr('src', 'https://global.oktacdn.com/okta-signin-widget/3.2.0/js/okta-sign-in.min.js')
                .appendTo(jQuery('head'));
            jQuery('<link>')
                .attr('type', 'text/css')
                .attr('rel', 'stylesheet')
                .attr('src', 'https://global.oktacdn.com/okta-signin-widget/3.2.0/css/okta-sign-in.min.css')
                .appendTo(jQuery('head'));
        }
    }
    //---------------------------------------------------------------------------------
    popOutURL(url) {
        let settings: string = 'scrollbars=yes,resizeable=yes,toolbar=no,status=no,menubar=no,directories=no,titlebar=no,location=no,addressbar=yes';
        var newwindow = window.open(url, '', settings);
    };
    //---------------------------------------------------------------------------------
    popOutTab(url) {
        let settings: string = 'scrollbars=yes,resizeable=yes,toolbar=no,status=no,menubar=no,directories=no,titlebar=no,location=yes,addressbar=yes';
        var newwindow = window.open(url, '_blank');
        newwindow.focus();
    };
    //---------------------------------------------------------------------------------
    uniqueId(idlength) {
        var charstoformid = '_0123456789ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz'.split('');
        if (!idlength) {
            idlength = Math.floor(Math.random() * charstoformid.length);
        }
        var uniqid = '';
        for (var i = 0; i < idlength; i++) {
            uniqid += charstoformid[Math.floor(Math.random() * charstoformid.length)];
        }
        // one last step is to check if this ID is already taken by an element before
        if (jQuery("#" + uniqid).length === 0) {
            return uniqid;
        } else {
            return this.uniqueId(idlength);
        }
    };
    //---------------------------------------------------------------------------------
    debugCss(idlength) {
        var html = [], fields = [], formfields, $appendContent;

        jQuery('.application').empty();
        formfields = ['text', 'number', 'password', 'select', 'date', 'time', 'email', 'url', 'phone', 'validation', 'multiselectvalidation', 'combobox', 'money', 'zipcode', 'percent', 'ssn', 'color', 'checkbox', 'toggleswitch', 'radio', 'searchbox', 'textarea'];

        html.push('<div style="display:flex;flex-wrap:wrap;">');
        for (var i = 0; i < formfields.length; i++) {
            var field, fieldname;
            field = formfields[i];
            fieldname = field.toLowerCase().replace(/\b[a-z]/g, function (letter) { return letter.toUpperCase(); });
            html.push(`<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="${fieldname} Section" style="min-width:300px;">`);
            html.push('  <div class="flexrow">');
            html.push(`    <div data-control="FwFormField" data-type="${field}" class="fwcontrol fwformfield" data-caption="${fieldname} Field"></div>`);
            html.push('  </div>');
            html.push('  <div class="flexrow">');
            html.push(`    <div data-control="FwFormField" data-type="${field}" class="fwcontrol fwformfield" data-caption="${fieldname} Field" data-enabled="false"></div>`);
            html.push('  </div>');
            html.push('  <div class="flexrow">');
            html.push(`    <div data-control="FwFormField" data-type="${field}" class="fwcontrol fwformfield" data-caption="${fieldname} Field" data-required="true"></div>`);
            html.push('  </div>');
            html.push('</div>');
        }
        html.push('</div>');

        $appendContent = jQuery(html.join(''));

        //load options for the radio buttons
        $appendContent.find('div[data-type="radio"]').append('<div data-caption="Value1" data-value="A"></div><div data-caption="Value1" data-value="S"></div><div data-caption="Value1" data-value="O"></div>');

        FwControl.renderRuntimeControls($appendContent.find('.fwcontrol'));

        //load options for the selects
        var $selectFields = $appendContent.find('div[data-type="select"]');
        $selectFields.each(function (index, element) {
            var $selectField;
            $selectField = jQuery(element);
            FwFormField.loadItems($selectField, [
                { value: 'A', text: 'ACTIVE' },
                { value: 'D', text: 'DECEASED' },
                { value: 'T', text: 'TERMINATED' },
                { value: 'N', text: 'NO HIRE' },
                { value: 'R', text: 'RETIRED' },
                { value: 'I', text: 'INACTIVE' },
                { value: 'S', text: 'SUSPENDED' }
            ], true);
        });

        jQuery('.application').append($appendContent);
    };
    //---------------------------------------------------------------------------------
    forceReloadCss() {
        var a = document.getElementsByTagName('link');
        for (var i = 0; i < a.length; i++) {
            var s = a[i];
            if (s.rel.toLowerCase().indexOf('stylesheet') >= 0 && s.href) {
                var h = s.href.replace(/(&|%5C?)forceReload=\d+/, '');
                s.href = h + (h.indexOf('?') >= 0 ? '&' : '?') + 'forceReload=' + (new Date().valueOf());
            }
        }
    };
    //---------------------------------------------------------------------------------
    getModule(path: string): void {
        const $bodyContainer = jQuery('#fw-app-body');
        const $modifiedForms = $bodyContainer.find('div[data-type="form"][data-modified="true"]');
        path = path.toLowerCase();
        if ($modifiedForms.length > 0) {
            //if (jQuery($modifiedForms[0]).parent().data('type') === 'settings-row') {
            //    this.navigate(path);
            //}
            const $form = jQuery($modifiedForms[0]);
            const $tab = jQuery(`#${$form.parent().attr('data-tabid')}`);
            $tab.click();
            FwModule.closeForm($form, $tab, path);
        } else {
            this.navigate(path);
        }
    };
    //---------------------------------------------------------------------------------
    getApplicationOptions() {
        return JSON.parse(sessionStorage.getItem('applicationOptions'));
    };
    //---------------------------------------------------------------------------------
    async loadCustomFormsAndBrowseViewsAsync() {
        const self = this;
        try {
            if (sessionStorage.getItem('userid') != null) {
                const WebUserId = JSON.parse(sessionStorage.getItem('userid')).webusersid;

                var requestAssignedCustomForm = new FwAjaxRequest<any>();
                requestAssignedCustomForm.setWebApiUrl('/api/v1/assignedcustomform/browse');
                requestAssignedCustomForm.httpMethod = 'POST';
                requestAssignedCustomForm.$elementToBlock = jQuery('body');
                requestAssignedCustomForm.data = {
                    uniqueids: {
                        WebUserId: WebUserId
                    }
                }
                var responseAssignedCustomForm = await FwAjax.callWebApi<any, any>(requestAssignedCustomForm);
                const baseFormIndex = responseAssignedCustomForm.ColumnIndex.BaseForm;
                const htmlIndex = responseAssignedCustomForm.ColumnIndex.Html;
                for (let i = 0; i < responseAssignedCustomForm.Rows.length; i++) {
                    let customForm = responseAssignedCustomForm.Rows[i];
                    let baseform = customForm[baseFormIndex];
                    jQuery('head').append(`<template id="tmpl-custom-${baseform}">${customForm[htmlIndex]}</template>`);
                }

                if (sessionStorage.getItem('location') != null) {
                    var requestBrowseActiveViewFields = new FwAjaxRequest<any>();
                    requestBrowseActiveViewFields.setWebApiUrl('/api/v1/browseactiveviewfields/browse');
                    requestBrowseActiveViewFields.httpMethod = 'POST';
                    requestBrowseActiveViewFields.$elementToBlock = jQuery('body');
                    requestBrowseActiveViewFields.data = {
                        uniqueids: {
                            WebUserId: WebUserId,
                            OfficeLocationId: JSON.parse(sessionStorage.getItem('location')).locationid
                        }
                    }
                    var responseBrowseActiveViewFields = await FwAjax.callWebApi<any, any>(requestBrowseActiveViewFields);
                    const moduleNameIndex = responseBrowseActiveViewFields.ColumnIndex.ModuleName;
                    const activeViewFieldsIndex = responseBrowseActiveViewFields.ColumnIndex.ActiveViewFields;
                    const idIndex = responseBrowseActiveViewFields.ColumnIndex.Id;
                    for (let i = 0; i < responseBrowseActiveViewFields.Rows.length; i++) {
                        let controller = `${responseBrowseActiveViewFields.Rows[i][moduleNameIndex]}Controller`;
                        if (typeof window[controller] !== 'undefined') {
                            window[controller].ActiveViewFields = JSON.parse(responseBrowseActiveViewFields.Rows[i][activeViewFieldsIndex]);
                            window[controller].ActiveViewFieldsId = responseBrowseActiveViewFields.Rows[i][idIndex];
                        }
                    }
                }
            }
            self.loadDefaultPage();
        } catch (ex) {
            FwFunc.showError(ex);
        };
    }
    //---------------------------------------------------------------------------------
    navigateHashChange(path) {
        var screen: any, $appendToContainer: any;

        FwPopup.destroy(jQuery('.FwPopup-divPopup,.FwPopup-divOverlay')); //mv 2018-02-21 - not sure if this is still needed.  Appears to be cleanup code
        FwAppData.abortAllRequests();

        path = path.toLowerCase();
        var foundmatch = false;
        for (var i = 0; i < routes.length; i++) {
            var match = routes[i].pattern.exec(path);
            if (match != null) {
                foundmatch = true;
                screen = routes[i].action(match);
                break;
            }
        }
        if (foundmatch && typeof screen !== 'undefined' && screen !== null) {
            if ((this.screens.length > 0) && (typeof this.screens[0].unload !== 'undefined')) {
                this.screens[0].unload();
                this.screens = [];
            }
            this.screens[0] = screen;

            var $applicationContainer = jQuery('#application');
            if ((jQuery('#fw-app').length == 0) && FwAppData.verifyHasAuthToken()) {
                $applicationContainer.empty().append(masterController.getMasterView()).removeClass('hidden');
            }

            if (jQuery('#fw-app').length > 0) {
                $appendToContainer = jQuery('#fw-app-body');
            } else {
                $appendToContainer = $applicationContainer;
            }

            $appendToContainer.empty().append(screen.$view);
            if (typeof screen.load !== 'undefined') {
                screen.load();
            }
            document.body.scrollTop = 0;
        }
        if (!foundmatch) {
            const localSubStr = "localhost:";
            const apiurlSubStr = applicationConfig.apiurl.replace('api/', '');
            if (applicationConfig.apiurl.indexOf(localSubStr) !== -1) {
                //okta redirects to the base url without a hash symbol, so if its active and we are there its probaly redirecting with tokens to the base app page, so we redirect to login.
                if ((window.location.href === "http://localhost/rentalworksweb/" || "http://localhost/gateworksweb") && applicationConfig.OktaEnabled === true) {
                    program.navigate('login');
                } else {
                    FwFunc.showError(`404: Not Found - ${path}`);
                }
            } else {
                if ((window.location.href === apiurlSubStr) && applicationConfig.OktaEnabled === true) {
                    program.navigate('login');
                } else {
                    FwFunc.showError(`404: Not Found - ${path}`);
                }
            }
        }
    };
    //---------------------------------------------------------------------------------
    navigate(path: string): void {
        path = path.toLowerCase();
        if (window.location.hash.replace('#/', '') !== path) {
            const url = `/${path}`;
            window.location.hash = url;
            //history.pushState(url, '', url);
        } else {
            this.navigateHashChange(path);
        }
    };
    //---------------------------------------------------------------------------------
    loadDefaultPage() {
        //look for authentication code as query param to see if this is a hubspot redirect
        let callBackParams = new URLSearchParams(window.location.search);
        let callBackCode = callBackParams.get('code');
        if (callBackCode) {
            (async () => {
                const securityTokens:string = await FwAjax.callWebApi({
                    httpMethod: 'POST',
                    url: `${applicationConfig.apiurl}api/v1/hubspot/gettokens`,
                    data: {
                        authorizationCode: callBackCode
                    }
                });
                console.log(JSON.parse(securityTokens));
                //remove callback code after using
            })();
        }
        if (FwAppData.verifyHasAuthToken()) {
            if (window.location.hash.replace('#/', '') !== '' && window.location.hash.replace('#/', '') !== 'home') {
                sessionStorage.setItem('redirectPath', window.location.hash.replace('#/', ''));
            }
            this.navigate('home');
        } else {
            if (applicationConfig.oktaSignIn) {
                if (applicationConfig.oktaSignIn.hasTokensInUrl()) {
                    applicationConfig.oktaSignIn.authClient.token.parseFromUrl().then(
                        function success(tokens) {
                            // Save the tokens for later use, e.g. if the page gets refreshed:
                            // Add the token to tokenManager to automatically renew the token when needed
                            tokens.forEach(token => {
                                if (token.idToken) {

                                    applicationConfig.oktaSignIn.authClient.tokenManager.add('idToken', token);
                                }
                                if (token.accessToken) {
                                    applicationConfig.oktaSignIn.authClient.tokenManager.add('accessToken', token);
                                }
                            });

                            // Say hello to the person who just signed in:
                            var idToken = applicationConfig.oktaSignIn.signIn.tokenManager.get('idToken');
                            console.log('Hello, ' + idToken.claims.email);

                            // Remove the tokens from the window location hash
                            window.location.hash = '';
                        },
                        function error(err) {
                            // handle errors as needed
                            console.error(err);
                        }
                    );
                }
            }
            this.navigate('default');
        }
    }
    //---------------------------------------------------------------------------------
    setApplicationTheme(setTheme: string) {
        jQuery('html').removeClass(function (index, className) {
            return (className.match(/(^|\s)theme-\S+/g) || []).join(' ');
        });
        jQuery('html').addClass(setTheme);
    }
    //---------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------
window.onhashchange = function () {
    program.navigateHashChange(window.location.hash.replace('#/', ''));
};
//---------------------------------------------------------------------------------
//fires when the browser history is changed through the history api and maybe other ways too
//window.addEventListener('popstate', function(e) {
//    // e.state contains the url of the module
//    this.navigateHashChange(history.state);
//});
//---------------------------------------------------------------------------------
// Handle uncaught exceptions
window.addEventListener("error", e => {
    // if logged in on the desktop, but the master section doesn't come up, need to logout to clear the error
    // this can avoid the user getting a blank white screen under certain error conditions
    if (jQuery('#fw-app').length === 0 && jQuery('html.desktop').length === 1 && sessionStorage.getItem('apiToken') !== null) {
        sessionStorage.clear();
        window.location.reload(true);
    } else {
        FwFunc.showError(e);
    }
});
//---------------------------------------------------------------------------------
// Handle uncaught promise rejections
window.addEventListener("unhandledrejection", e => {
    // if logged in on the desktop, but the master section doesn't come up, need to logout to clear the error
    // this can avoid the user getting a blank white screen under certain error conditions
    if (jQuery('#fw-app').length === 0 && jQuery('html.desktop').length === 1 && sessionStorage.getItem('apiToken') !== null) {
        sessionStorage.clear();
        window.location.reload(true);
    } else {
        FwFunc.showError(e.reason);
    }
});
