var FwAppData = {
    services: {
        account: {}
    }
  , jqXHR: {}
  , autoLogoutTimeout: null
  , autoLogoutWarningTimeout: null
  , autoLogoutMinutes: 0
  , reportTimeout: 7200 // 2 hours
  , useWebApi: false
};
//----------------------------------------------------------------------------------------------
FwAppData.jsonPost = function(requiresAuthToken, url, request, timeoutSeconds, onSuccess, onError, $elementToBlock ) {
    var me, xhr, $overlay, jqXHRobj;
    var isdesktop = jQuery('html').hasClass('desktop');
    var ismobile  = jQuery('html').hasClass('mobile');

    me = this;
    clearTimeout(FwAppData.autoLogoutTimeout);
    clearTimeout(FwAppData.autoLogoutWarningTimeout);
    if (requiresAuthToken) {
        if (!FwAppData.verifyHasAuthToken()) {
            program.navigate('account/login');
            return;
        }
        request.authToken = sessionStorage.getItem('authToken');
    }
    request.requestid = FwAppData.generateUUID();
    if (timeoutSeconds === null) {
        timeoutSeconds = applicationConfig.ajaxTimeoutSeconds;
    }
    request.clientVersion = applicationConfig.version;
    var fullurl = '';
    fullurl = applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + url;
    if (FwAppData.useWebApi && url.indexOf('api/') === 0) {
        fullurl = applicationConfig.apiurl + url;   
    }
    jqXHRobj = jQuery.ajax({
            type:        'POST'
          , url:         fullurl
          , contentType: 'application/json; charset=utf-8'
          , dataType:    'json'
          , cache:       false
          , crossDomain: true
          , timeout:     timeoutSeconds * 1000
          , data:        JSON.stringify(request)
          , beforeSend: function(jqXHR, settings) {
                if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                    if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                        $overlay = FwOverlay.showPleaseWaitOverlay($elementToBlock, request.requestid);
                    }
                } else if (ismobile) {
                    var maxZIndex;
                    jQuery('#index-loadingInner').hide();
                    maxZIndex = FwFunc.getMaxZ('*');
                    jQuery('#index-loading').css('z-index', maxZIndex).show();
                    me.loadingTimeout = setTimeout(function() {
                        me.loadingTimeout = null;
                        jQuery('#index-loadingInner').stop().fadeIn(50);
                    }, 0);
                }
          }
        })
        .done(function(response, textStatus, jqXHR) {
            if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                    FwOverlay.hideOverlay($overlay);
                }
            } else if (ismobile) {
                if (me.loadingTimeout) {
                    clearTimeout(me.loadingTimeout);
                }
                jQuery('#index-loadingInner').stop().fadeOut(50, function() {
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
            if (applicationConfig.version !== response.serverVersion && FwAppData.useWebApi === 0) {
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
        .fail(function(jqXHR, textStatus, errorThrown) {
            console.log(jqXHR.responseText);
            if (isdesktop || (ismobile && ($elementToBlock !== null))) {
                if ((typeof $elementToBlock === 'object') && ($elementToBlock !== null)) {
                    FwOverlay.hideOverlay($overlay);
                }
            } else if (ismobile) {
                if (me.loadingTimeout) {
                    clearTimeout(me.loadingTimeout);
                }
                jQuery('#index-loadingInner').stop().fadeOut(50, function() {
                    jQuery('#index-loading').css('z-index', 0).hide();
                });
            }
            delete FwAppData.jqXHR[request.requestid];
            FwAppData.updateAutoLogout(null);
            if (typeof onError === 'function') {
                onError(errorThrown);
            } else if (errorThrown !== 'abort') {
                FwFunc.showError(errorThrown);
            }
        })
    ;
    FwAppData.jqXHR[request.requestid] = jqXHRobj;
    return request.requestid;
};
//----------------------------------------------------------------------------------------------
FwAppData.verifyHasAuthToken = function() {
    var hasAuthToken;
    hasAuthToken = false;
    if (sessionStorage.getItem('authToken')) {
        hasAuthToken = true;
    }
    return hasAuthToken;
};
//----------------------------------------------------------------------------------------------
FwAppData.updateAutoLogout = function(response) {
    // only if autologout is enabled and there's no AJAX requests running do we do the inactivity timeout
    clearTimeout(FwAppData.autoLogoutTimeout);
    clearTimeout(FwAppData.autoLogoutWarningTimeout);
    if (response) {
        if (typeof response.autoLogoutMinutes === 'number') {
            FwAppData.autoLogoutMinutes = response.autoLogoutMinutes;
        }
        if ((typeof response.autoLogoutMinutes === 'number') && (response.autoLogoutMinutes !== 0) && (Object.keys(FwAppData.jqXHR).length === 0)){
            FwAppData.autoLogoutTimeout = setTimeout(function() {
                sessionStorage.clear();
                window.location.reload(false);
            }, response.autoLogoutMinutes * 60000 /* msec/min */);
            // Uncomment this for debugging autologout issues.  This shows the autologout minutes every time it's reset.
            //FwNotification.renderNotification('INFO', 'Auto Logout in ' + response.autoLogoutMinutes + ' minute(s).');
            FwAppData.autoLogoutWarningTimeout = setTimeout(function() {
                FwNotification.renderNotification('WARNING', 'Auto-logout for inactivity in 30 seconds...');
            }, (response.autoLogoutMinutes - .5 ) * 60000 /* msec/min */);
        }
    } else {
        if (FwAppData.autoLogoutMinutes > 0) {
            FwAppData.autoLogoutTimeout = setTimeout(function() {
                sessionStorage.clear();
                window.location.reload(false);
            }, FwAppData.autoLogoutMinutes * 60000 /* msec/min */);
            // Uncomment this for debugging autologout issues.  This shows the autologout minutes every time it's reset.
            //FwNotification.renderNotification('INFO', 'Auto Logout in ' + response.autoLogoutMinutes + ' minute(s).');
            FwAppData.autoLogoutWarningTimeout = setTimeout(function() {
                FwNotification.renderNotification('WARNING', 'Auto-logout for inactivity in 30 seconds...');
            }, (FwAppData.autoLogoutMinutess - .5 ) * 60000 /* msec/min */);
        }
    }
};
//----------------------------------------------------------------------------------------------
FwAppData.generateUUID = function() {
    var uuid;
    uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
        var r = Math.random()*16|0, v = c === 'x' ? r : (r&0x3|0x8);
        return v.toString(16);
    });
    return uuid;
}
//----------------------------------------------------------------------------------------------
FwAppData.abortRequest = function(requestid) {
    for (var item in FwAppData.jqXHR) {
        if (item === requestid) {
            //FwNotification.renderNotification('INFO', 'Request aborted.');
            FwAppData.jqXHR[item].abort();
            delete FwAppData.jqXHR[item];
        }
    }
};
//----------------------------------------------------------------------------------------------
FwAppData.abortAllRequests = function () {
    for (var item in FwAppData.jqXHR) {
        FwAppData.jqXHR[item].abort();
        delete FwAppData.jqXHR[item];
    }
};
//----------------------------------------------------------------------------------------------
