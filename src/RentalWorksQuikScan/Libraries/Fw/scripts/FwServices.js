//----------------------------------------------------------------------------------------------
var FwServices = {
    defaultTimeout: null,
    defaultOnError: null,
    utilities: {},
    account: {},
    module:  {},
    grid:    {},
    validation: {}
};
//----------------------------------------------------------------------------------------------
FwServices.utilities.creditCardSale = function(request, $elementToBlock, onSuccess) {
    FwAppData.jsonPost(false, 'services.ashx?path=/fwutilities/creditcardsale', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, $elementToBlock);
};
//----------------------------------------------------------------------------------------------
FwServices.account.getAuthToken = function(request, $elementToBlock, onSuccess) {
    FwAppData.jsonPost(false, 'services.ashx?path=/account/getauthtoken', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, $elementToBlock);
};
//----------------------------------------------------------------------------------------------
FwServices.account.authPassword = function(request, onSuccess) {
    FwAppData.jsonPost(false, 'services.ashx?path=/account/authpassword', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError);
};
//----------------------------------------------------------------------------------------------
FwServices.module.method = function(request, module, method, $elementToBlock, onSuccess, onError) {
    var controller = window[module + 'Controller'];
    if (typeof controller === 'undefined') {
        throw module + 'Controller is not defined.'
    }
    if (typeof controller.apiurl !== 'undefined') {
        if (method === 'Load') {
            var ids = [];
            for (var key in request.ids) {
                ids.push(request.ids[key].value);
            }
            ids = ids.join(',');
            if (ids.length === 0) {
                throw 'primary key id(s) cannot be blank';
            }
            FwAppData.apiMethod(true, 'GET', controller.apiurl + '/' + ids, null, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
        else if (method === 'Delete') {
            var ids = [];
            for (var key in request.ids) {
                ids.push(request.ids[key].value);
            }
            ids = ids.join(',');
            if (ids.length === 0) {
                throw 'primary key id(s) cannot be blank';
            }
            FwAppData.apiMethod(true, 'DELETE', controller.apiurl + '/' + ids, null, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
        else if (method === 'Save') {
            FwAppData.apiMethod(true, 'POST', controller.apiurl, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
        else {
            FwAppData.jsonPost(true, controller.apiurl + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
    } else {
        FwAppData.jsonPost(true, 'services.ashx?path=/module/' + module + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    } 
};
//----------------------------------------------------------------------------------------------
FwServices.grid.method = function(request, module, method, $elementToBlock, onSuccess, onError) {
    var controller = window[module + 'Controller'];
    if (typeof controller === 'undefined') {
        throw module + 'Controller is not defined.'
    }
    if (typeof controller.apiurl !== 'undefined') {
        if (method === 'Load') {
            var ids = [];
            for (var key in request.ids) {
                ids.push(request.ids[key].value);
            }
            ids = ids.join(',');
            if (ids.length === 0) {
                throw 'primary key id(s) cannot be blank';
            }
            FwAppData.apiMethod(true, 'GET', controller.apiurl + '/' + ids, null, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
        else if (method === 'Delete') {
            var ids = [];
            for (var key in request.ids) {
                ids.push(request.ids[key].value);
            }
            ids = ids.join(',');
            if (ids.length === 0) {
                throw 'primary key id(s) cannot be blank';
            }
            FwAppData.apiMethod(true, 'DELETE', controller.apiurl + '/' + ids, null, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
        else if (method === 'Insert') {
            FwAppData.apiMethod(true, 'POST', controller.apiurl, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
        else if (method === 'Update') {
            FwAppData.apiMethod(true, 'POST', controller.apiurl, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
        else if (method === 'ValidateDuplicate') {
            FwAppData.jsonPost(true, controller.apiurl + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, null);
        } else {
            FwAppData.jsonPost(true, controller.apiurl + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
    } else {
        FwAppData.jsonPost(true, 'services.ashx?path=/grid/' + module + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    }
};
//----------------------------------------------------------------------------------------------
FwServices.validation.method = function(request, module, method, $control, onSuccess, onError) {
    var $elementToBlock = $control;
    var controller = window[module + 'Controller'];
    if (typeof controller === 'undefined') {
        throw module + 'Controller is not defined.'
    }
    if (typeof $control.attr('data-apiurl') === 'string') {
        FwAppData.jsonPost(true, $control.attr('data-apiurl') + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    }
    else if (typeof controller.apiurl !== 'undefined') {
        FwAppData.jsonPost(true, controller.apiurl + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    }
    else {
        FwAppData.jsonPost(true, 'services.ashx?path=/validation/' + module + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    }
};
//----------------------------------------------------------------------------------------------
FwServices.getholidayevents = function(request, $elementToBlock, onSuccess, onError) {
    FwAppData.jsonPost(true, 'services.ashx?path=/fwscheduler/getholidayevents', request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
};
//----------------------------------------------------------------------------------------------
FwServices.callMethod = function(servicename, methodname, request, timeout, $elementToBlock, onSuccess, onError) {
    FwAppData.jsonPost(true,  'services.ashx?path=/services/' + servicename + '/' + methodname, request, timeout, onSuccess, onError, $elementToBlock);
};
//----------------------------------------------------------------------------------------------