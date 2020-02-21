class FwAppData {
    static services: Object;
    static jqXHR: Object;
    static autoLogoutTimeout: number;
    static autoLogoutWarningTimeout: number;
    static autoLogoutMinutes: number;
    static reportTimeout: number; // 2 hours
    static useWebApi: Boolean;
    static loadingTimeout: number;
    //----------------------------------------------------------------------------------------------
    static init() {
        FwAppData.services = {
            account: {}
        };
        FwAppData.jqXHR = {};
        FwAppData.autoLogoutTimeout = null;
        FwAppData.autoLogoutWarningTimeout = null;
        FwAppData.autoLogoutMinutes = 0;
        FwAppData.reportTimeout = 7200; // 2 hours
        FwAppData.useWebApi = false;
        FwAppData.loadingTimeout = null;
    }
    //----------------------------------------------------------------------------------------------
    static jsonPost(requiresAuthToken, url, request, timeoutSeconds, onSuccess, onError, $elementToBlock) {
        var me, xhr, $overlay, jqXHRobj;
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        var useWebApi = false;
        me = this;
        var fullurl = '';
        fullurl = applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + url;
        if (url.indexOf('api/') === 0) {
            useWebApi = true;
            fullurl = applicationConfig.apiurl + url;
        }
        clearTimeout(FwAppData.autoLogoutTimeout);
        clearTimeout(FwAppData.autoLogoutWarningTimeout);
        if (requiresAuthToken) {
            if (!FwAppData.verifyHasAuthToken()) {
                program.navigate('login');
                return;
            }
            if (!useWebApi) {
                request.authToken = sessionStorage.getItem('authToken');
            }
        }
        request.requestid = FwAppData.generateUUID();
        if (timeoutSeconds === null) {
            timeoutSeconds = applicationConfig.ajaxTimeoutSeconds;
        }
        request.clientVersion = applicationConfig.version;
        var ajaxOptions: any = {
            type: 'POST'
            , url: fullurl
            , contentType: 'application/json'
            , dataType: 'json'
            //, cache: false // this may not be needed anymore, because of changes in the web.config to prevent caching
            //, crossDomain: true
            , timeout: timeoutSeconds * 1000
            , data: JSON.stringify(request)
            , beforeSend: function (jqXHR, settings) {
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        $overlay = FwOverlay.showPleaseWaitOverlay($elementToBlock, request.requestid);
                    }
                } else if (ismobile) {
                    var maxZIndex;
                    jQuery('#index-loadingInner').hide();
                    maxZIndex = FwFunc.getMaxZ('*');
                    jQuery('#index-loading').css('z-index', maxZIndex).show();
                    me.loadingTimeout = setTimeout(function () {
                        me.loadingTimeout = null;
                        jQuery('#index-loadingInner').stop().fadeIn(50);
                    }, 0);
                }
            }
        };
        if (requiresAuthToken) {
            ajaxOptions.headers = {
                Authorization: 'Bearer ' + sessionStorage.getItem('apiToken')
            };
        }
        jqXHRobj = jQuery.ajax(ajaxOptions)
            .done(function (response, textStatus, jqXHR) {
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        FwOverlay.hideOverlay($overlay);
                    }
                } else if (ismobile) {
                    if (me.loadingTimeout) {
                        clearTimeout(me.loadingTimeout);
                    }
                    jQuery('#index-loadingInner').stop().fadeOut(50, function () {
                        jQuery('#index-loading').css('z-index', 0).hide();
                    });
                }
                if ((typeof response === 'object') && (typeof response.request === 'object') && (typeof response.request.requestid === 'string')) {
                    delete FwAppData.jqXHR[request.requestid];
                }
                if (typeof response.authToken === 'string') {
                    sessionStorage.setItem('authToken', response.authToken);
                }
                FwAppData.updateAutoLogout(response);
                if (applicationConfig.version !== response.serverVersion && !useWebApi) {
                    alert('The application will be updated to version: ' + response.serverVersion);
                    sessionStorage.clear();
                    window.location.reload(true);
                } else if (response.exception) {
                    if (typeof onError === 'function') {
                        onError(response.exception);
                    } else {
                        FwFunc.showError(response.exception);
                    }
                } else if ((typeof response.authTokenExpired === 'boolean') && (response.authTokenExpired)) {
                    sessionStorage.clear();
                    window.location.reload(false);
                } else {
                    if (typeof onSuccess === 'function') {
                        onSuccess(response);
                    }
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                var errorContent = jqXHR.responseText;
                if (jqXHR.status === 404) {
                    errorThrown = `404 Not Found: ${this.url}`;
                    errorContent = JSON.stringify({
                        StatusCode: 404,
                        Message: 'URL: ' + this.url,
                        StackTrace: ''
                    });
                }
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        FwOverlay.hideOverlay($overlay);
                    }
                } else if (ismobile) {
                    if (me.loadingTimeout) {
                        clearTimeout(me.loadingTimeout);
                    }
                    jQuery('#index-loadingInner').stop().fadeOut(50, function () {
                        jQuery('#index-loading').css('z-index', 0).hide();
                    });
                }
                delete FwAppData.jqXHR[request.requestid];
                FwAppData.updateAutoLogout(null);
                if (typeof onError === 'function') {
                    onError(errorThrown);
                } else if (errorThrown !== 'abort') {
                    if (useWebApi) {
                        FwFunc.showWebApiError(jqXHR.status, errorThrown, errorContent, fullurl);
                    } else {
                        console.log(jqXHR.responseText);
                        FwFunc.showError(errorThrown);
                    }
                }
            });

        FwAppData.jqXHR[request.requestid] = jqXHRobj;
        return request.requestid;
    };
    //----------------------------------------------------------------------------------------------
    static apiMethod(requiresApiToken: boolean, method: 'GET' | 'POST' | 'PUT' | 'DELETE', url: string, request: any, timeoutSeconds: number, onSuccess: (response: any) => void, onError: (response: any) => void, $elementToBlock: JQuery<HTMLElement>, progressBarSessionId?: any) {
        let $overlay: JQuery<HTMLElement>;
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');

        var data = (method === 'GET') ? null : JSON.stringify(request);
        var me = this;
        clearTimeout(FwAppData.autoLogoutTimeout);
        clearTimeout(FwAppData.autoLogoutWarningTimeout);
        if (requiresApiToken) {
            if (!FwAppData.verifyHasAuthToken()) {
                program.navigate('login');
                return;
            }
        }
        if (timeoutSeconds === null) {
            timeoutSeconds = applicationConfig.ajaxTimeoutSeconds;
        }
        var fullurl = applicationConfig.apiurl + url;
        var ajaxOptions: JQuery.AjaxSettings<any> = {
            method: method,
            url: fullurl,
            contentType: 'application/json',
            dataType: 'json',
            //cache: false, // this may not be needed anymore because of changes in the web.config to disable caching
            //crossDomain: true,
            timeout: timeoutSeconds * 1000,
            context: {
                requestid: FwAppData.generateUUID()
            },
            beforeSend: (jqXHR: JQuery.jqXHR<any>, settings: JQuery.AjaxSettings<any>) => {
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {

                        //if (progressBarSessionId !== undefined) {
                        if ((progressBarSessionId !== undefined) && (progressBarSessionId !== null)) {
                            $overlay = FwOverlay.showProgressBarOverlay($elementToBlock, progressBarSessionId);
                        } else {
                            $overlay = FwOverlay.showPleaseWaitOverlay($elementToBlock, settings.context.requestid);
                        }
                    }
                } else if (ismobile) {
                    var maxZIndex;
                    jQuery('#index-loadingInner').hide();
                    maxZIndex = FwFunc.getMaxZ('*');
                    jQuery('#index-loading').css('z-index', maxZIndex).show();
                    me.loadingTimeout = window.setTimeout((args: any[]) => {
                        me.loadingTimeout = null;
                        jQuery('#index-loadingInner').stop().fadeIn(50);
                    }, 0);
                }
            }
        };
        if (method !== 'GET') {
            ajaxOptions.data = JSON.stringify(request);
        }
        if (requiresApiToken) {
            ajaxOptions.headers = {
                Authorization: 'Bearer ' + sessionStorage.getItem('apiToken')
            };
        }
        var jqXHRobj = jQuery.ajax(ajaxOptions)
            .done(function (response, textStatus, jqXHR) {
                // 'this' is the ajaxOptions.context property
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        FwOverlay.hideOverlay($overlay);
                    }
                } else if (ismobile) {
                    if (me.loadingTimeout) {
                        clearTimeout(me.loadingTimeout);
                    }
                    jQuery('#index-loadingInner').stop().fadeOut(50, function () {
                        jQuery('#index-loading').css('z-index', 0).hide();
                    });
                }
                delete FwAppData.jqXHR[this.requestid];
                if (typeof onSuccess === 'function') {
                    onSuccess(response);
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                // 'this' is the ajaxOptions.context property
                var errorContent = jqXHR.responseText;
                if (jqXHR.status === 404) {
                    errorThrown = 'Not Found';
                    errorContent = JSON.stringify({
                        StatusCode: 404,
                        Message: `${ajaxOptions.method}: ${fullurl}`,
                        StackTrace: ''
                    });
                }
                else if (jqXHR.status === 500) {
                    if (errorContent.length > 0 && errorContent[0] === '<') {
                        errorThrown = errorContent;
                    }
                }
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        FwOverlay.hideOverlay($overlay);
                    }
                } else if (ismobile) {
                    if (me.loadingTimeout) {
                        clearTimeout(me.loadingTimeout);
                    }
                    jQuery('#index-loadingInner').stop().fadeOut(50, function () {
                        jQuery('#index-loading').css('z-index', 0).hide();
                    });
                }
                delete FwAppData.jqXHR[this.requestid];
                FwAppData.updateAutoLogout(null);
                const errorMessage = (jqXHR.responseJSON !== undefined && jqXHR.responseJSON.Message !== undefined) ? jqXHR.responseJSON.Message : (jqXHR.responseText !== null) ? jqXHR.responseText : 'An unknown error occured.';
                if (typeof onError === 'function') {
                    if (errorMessage) {
                        onError(errorMessage);
                    } else {
                        onError(errorThrown);
                    }
                } else if (errorThrown !== 'abort') {
                    if (url.indexOf('api/') === 0) {
                        FwFunc.showWebApiError(jqXHR.status, errorThrown, errorContent, fullurl);
                    } else {
                        if (errorMessage) {
                            FwFunc.showError(errorMessage);
                        } else {
                            FwFunc.showError(errorThrown);
                        }
                    }
                }
            });

        FwAppData.jqXHR[ajaxOptions.context.requestid] = jqXHRobj;
        return ajaxOptions.context.requestid;
    };
    //----------------------------------------------------------------------------------------------
    static verifyHasAuthToken() {
        var hasAuthToken;
        hasAuthToken = false;
        if (sessionStorage.getItem('apiToken') || sessionStorage.getItem('authToken')) {
            hasAuthToken = true;
        }
        return hasAuthToken;
    };
    //----------------------------------------------------------------------------------------------
    static updateAutoLogout(response) {
        // only if autologout is enabled and there's no AJAX requests running do we do the inactivity timeout
        clearTimeout(FwAppData.autoLogoutTimeout);
        clearTimeout(FwAppData.autoLogoutWarningTimeout);
        if (response) {
            if (typeof response.autoLogoutMinutes === 'number') {
                FwAppData.autoLogoutMinutes = response.autoLogoutMinutes;
            }
            if ((typeof response.autoLogoutMinutes === 'number') && (response.autoLogoutMinutes !== 0) && (Object.keys(FwAppData.jqXHR).length === 0)) {
                FwAppData.autoLogoutTimeout = window.setTimeout(function () {
                    sessionStorage.clear();
                    window.location.reload(false);
                }, response.autoLogoutMinutes * 60000 /* msec/min */);
                // Uncomment this for debugging autologout issues.  This shows the autologout minutes every time it's reset.
                //FwNotification.renderNotification('INFO', 'Auto Logout in ' + response.autoLogoutMinutes + ' minute(s).');
                FwAppData.autoLogoutWarningTimeout = window.setTimeout(function () {
                    FwNotification.renderNotification('WARNING', 'Auto-logout for inactivity in 30 seconds...');
                }, (response.autoLogoutMinutes - .5) * 60000 /* msec/min */);
            }
        } else {
            if (FwAppData.autoLogoutMinutes > 0) {
                FwAppData.autoLogoutTimeout = window.setTimeout(function () {
                    sessionStorage.clear();
                    window.location.reload(false);
                }, FwAppData.autoLogoutMinutes * 60000 /* msec/min */);
                // Uncomment this for debugging autologout issues.  This shows the autologout minutes every time it's reset.
                //FwNotification.renderNotification('INFO', 'Auto Logout in ' + response.autoLogoutMinutes + ' minute(s).');
                FwAppData.autoLogoutWarningTimeout = window.setTimeout(function () {
                    FwNotification.renderNotification('WARNING', 'Auto-logout for inactivity in 30 seconds...');
                }, (FwAppData.autoLogoutMinutes - .5) * 60000 /* msec/min */);
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    static generateUUID(): string {
        var uuid;
        uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
        return uuid;
    }
    //----------------------------------------------------------------------------------------------
    static abortRequest(requestid) {
        for (var item in FwAppData.jqXHR) {
            if (item === requestid) {
                //FwNotification.renderNotification('INFO', 'Request aborted.');
                FwAppData.jqXHR[item].abort();
                delete FwAppData.jqXHR[item];
            }
        }
    };
    //----------------------------------------------------------------------------------------------
    static abortAllRequests() {
        for (var item in FwAppData.jqXHR) {
            FwAppData.jqXHR[item].abort();
            delete FwAppData.jqXHR[item];
        }
    };
    //----------------------------------------------------------------------------------------------
}
FwAppData.init();