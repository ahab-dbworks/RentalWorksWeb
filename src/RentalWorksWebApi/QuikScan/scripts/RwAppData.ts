var RwAppData: any = {
    services: {
        account: {}
      , inventory: {}
      , po: {}
      , order: {}
      , reports: {}
    }
  , jqXHR: null
  , loadingTimeout: null
};
//----------------------------------------------------------------------------------------------
RwAppData.error = function(jqXHR, textStatus, errorThrown) {
    var me;

    me = this;
    if (me.loadingTimeout) {
        clearTimeout(me.loadingTimeout);
    }
    jQuery('#index-loading').css('z-index', 0).stop().fadeOut(50);
    if (RwAppData.jqXHR && RwAppData.jqXHR.status) {
        RwAppData.jqXHR = null;
        FwFunc.showError(jqXHR.responseText);
    } else {
        RwAppData.jqXHR = null;
    }
};
//----------------------------------------------------------------------------------------------
RwAppData.jsonPost = function(requiresAuthToken, url, request, doneCallback, timeoutSeconds, async, failCallback) {
    if (timeoutSeconds === null) {
        timeoutSeconds = applicationConfig.ajaxTimeoutSeconds;
    }
    if (typeof failCallback !== 'function') {
        failCallback = null;
    }
    if ((typeof async === 'boolean') && (async === true)) {
        FwAppData.jsonPost(requiresAuthToken, url, request, timeoutSeconds, doneCallback, failCallback, jQuery('<div>'));
    } else {
        FwAppData.jsonPost(requiresAuthToken, url, request, timeoutSeconds, doneCallback, failCallback, null);
    }
};
//----------------------------------------------------------------------------------------------
RwAppData.verifyHasAuthToken = function() {
    var hasAuthToken;
    hasAuthToken = false;
    if (sessionStorage.getItem('authToken')) {
        hasAuthToken = true;
    }
    return hasAuthToken;
};
//----------------------------------------------------------------------------------------------
RwAppData.stripBarcode = function(barcode) {
    var skipbarcodeprefixes, result;
    skipbarcodeprefixes = sessionStorage.getItem('skipBarcodePrefixes').split(',');
    result = barcode.trim();
    if (result.length > 0) {
        result = result.toUpperCase();
        for (var i = 0; i < skipbarcodeprefixes.length; i++) {
            if (skipbarcodeprefixes[i] === result.slice(0, skipbarcodeprefixes[i].length)) {
                result = result.slice(skipbarcodeprefixes[i].length);
                break;
            }
        }
    }
    return result;
};
//----------------------------------------------------------------------------------------------
