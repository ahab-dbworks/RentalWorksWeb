var FwAppData = (function () {
    function FwAppData() {
    }
    FwAppData.init = function () {
        FwAppData.services = {
            account: {}
        };
        FwAppData.jqXHR = {};
        FwAppData.autoLogoutTimeout = null;
        FwAppData.autoLogoutWarningTimeout = null;
        FwAppData.autoLogoutMinutes = 0;
        FwAppData.reportTimeout = 7200;
        FwAppData.useWebApi = false;
        FwAppData.loadingTimeout = null;
    };
    FwAppData.jsonPost = function (requiresAuthToken, url, request, timeoutSeconds, onSuccess, onError, $elementToBlock) {
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
                program.navigate('account/login');
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
        var ajaxOptions = {
            type: 'POST',
            url: fullurl,
            contentType: 'application/json',
            dataType: 'json',
            timeout: timeoutSeconds * 1000,
            data: JSON.stringify(request),
            beforeSend: function (jqXHR, settings) {
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        $overlay = FwOverlay.showPleaseWaitOverlay($elementToBlock, request.requestid);
                    }
                }
                else if (ismobile) {
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
            }
            else if (ismobile) {
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
            }
            else if (response.exception) {
                if (typeof onError === 'function') {
                    onError(response.exception);
                }
                else {
                    FwFunc.showError(response.exception);
                }
            }
            else if ((typeof response.authTokenExpired === 'boolean') && (response.authTokenExpired)) {
                sessionStorage.clear();
                window.location.reload(false);
            }
            else {
                if (typeof onSuccess === 'function') {
                    onSuccess(response);
                }
            }
        })
            .fail(function (jqXHR, textStatus, errorThrown) {
            var errorContent = jqXHR.responseText;
            if (jqXHR.status === 404) {
                errorThrown = 'Not Found';
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
            }
            else if (ismobile) {
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
            }
            else if (errorThrown !== 'abort') {
                if (useWebApi) {
                    FwFunc.showWebApiError(jqXHR.status, errorThrown, errorContent, fullurl);
                }
                else {
                    console.log(jqXHR.responseText);
                    FwFunc.showError(errorThrown);
                }
            }
        });
        FwAppData.jqXHR[request.requestid] = jqXHRobj;
        return request.requestid;
    };
    ;
    FwAppData.apiMethod = function (requiresAuthToken, method, url, request, timeoutSeconds, onSuccess, onError, $elementToBlock) {
        var $overlay;
        var isdesktop = jQuery('html').hasClass('desktop');
        var ismobile = jQuery('html').hasClass('mobile');
        var data = (method === 'GET') ? null : JSON.stringify(request);
        var me = this;
        clearTimeout(FwAppData.autoLogoutTimeout);
        clearTimeout(FwAppData.autoLogoutWarningTimeout);
        if (requiresAuthToken) {
            if (!FwAppData.verifyHasAuthToken()) {
                program.navigate('account/login');
                return;
            }
        }
        if (timeoutSeconds === null) {
            timeoutSeconds = applicationConfig.ajaxTimeoutSeconds;
        }
        var fullurl = applicationConfig.apiurl + url;
        var ajaxOptions = {
            method: method,
            url: fullurl,
            contentType: 'application/json',
            dataType: 'json',
            timeout: timeoutSeconds * 1000,
            context: {
                requestid: FwAppData.generateUUID()
            },
            beforeSend: function (jqXHR, settings) {
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        $overlay = FwOverlay.showPleaseWaitOverlay($elementToBlock, settings.context.requestid);
                    }
                }
                else if (ismobile) {
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
        if (method !== 'GET') {
            ajaxOptions.data = JSON.stringify(request);
        }
        if (requiresAuthToken) {
            ajaxOptions.headers = {
                Authorization: 'Bearer ' + sessionStorage.getItem('apiToken')
            };
        }
        var jqXHRobj = jQuery.ajax(ajaxOptions)
            .done(function (response, textStatus, jqXHR) {
            if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                    FwOverlay.hideOverlay($overlay);
                }
            }
            else if (ismobile) {
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
            if (typeof onSuccess === 'function') {
                onSuccess(response);
            }
        })
            .fail(function (jqXHR, textStatus, errorThrown) {
            var errorContent = jqXHR.responseText;
            if (jqXHR.status === 404) {
                errorThrown = 'Not Found';
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
            }
            else if (ismobile) {
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
            }
            else if (errorThrown !== 'abort') {
                if (url.indexOf('api/') === 0) {
                    FwFunc.showWebApiError(jqXHR.status, errorThrown, errorContent, fullurl);
                }
                else {
                    FwFunc.showError(errorThrown);
                }
            }
        });
        return null;
    };
    ;
    FwAppData.verifyHasAuthToken = function () {
        var hasAuthToken;
        hasAuthToken = false;
        if (sessionStorage.getItem('authToken')) {
            hasAuthToken = true;
        }
        return hasAuthToken;
    };
    ;
    FwAppData.updateAutoLogout = function (response) {
        clearTimeout(FwAppData.autoLogoutTimeout);
        clearTimeout(FwAppData.autoLogoutWarningTimeout);
        if (response) {
            if (typeof response.autoLogoutMinutes === 'number') {
                FwAppData.autoLogoutMinutes = response.autoLogoutMinutes;
            }
            if ((typeof response.autoLogoutMinutes === 'number') && (response.autoLogoutMinutes !== 0) && (Object.keys(FwAppData.jqXHR).length === 0)) {
                FwAppData.autoLogoutTimeout = setTimeout(function () {
                    sessionStorage.clear();
                    window.location.reload(false);
                }, response.autoLogoutMinutes * 60000);
                FwAppData.autoLogoutWarningTimeout = setTimeout(function () {
                    FwNotification.renderNotification('WARNING', 'Auto-logout for inactivity in 30 seconds...');
                }, (response.autoLogoutMinutes - .5) * 60000);
            }
        }
        else {
            if (FwAppData.autoLogoutMinutes > 0) {
                FwAppData.autoLogoutTimeout = setTimeout(function () {
                    sessionStorage.clear();
                    window.location.reload(false);
                }, FwAppData.autoLogoutMinutes * 60000);
                FwAppData.autoLogoutWarningTimeout = setTimeout(function () {
                    FwNotification.renderNotification('WARNING', 'Auto-logout for inactivity in 30 seconds...');
                }, (FwAppData.autoLogoutMinutes - .5) * 60000);
            }
        }
    };
    ;
    FwAppData.generateUUID = function () {
        var uuid;
        uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
        return uuid;
    };
    FwAppData.abortRequest = function (requestid) {
        for (var item in FwAppData.jqXHR) {
            if (item === requestid) {
                FwAppData.jqXHR[item].abort();
                delete FwAppData.jqXHR[item];
            }
        }
    };
    ;
    FwAppData.abortAllRequests = function () {
        for (var item in FwAppData.jqXHR) {
            FwAppData.jqXHR[item].abort();
            delete FwAppData.jqXHR[item];
        }
    };
    ;
    return FwAppData;
}());
FwAppData.init();
//# sourceMappingURL=FwAppData.js.map