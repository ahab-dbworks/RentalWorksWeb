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
    if (FwAppData.useWebApi) {
        var controller = window[module + 'Controller'];
        if (typeof controller === 'object' && method.toLowerCase() !== 'load') {
            FwAppData.jsonPost(true, controller.apiurl + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        } else {
            var ids = [];
            for (var key in request.ids) {
                ids.push(request.ids[key].value);
            }
            ids = ids.join(',');
            if (ids.length === 0) {
                throw 'primary key id(s) cannot be blank';
            }
            FwAppData.apiGet(true, controller.apiurl + '/' + ids, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
        }
    } else {
        FwAppData.jsonPost(true, 'services.ashx?path=/module/' + module + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    } 
};
//----------------------------------------------------------------------------------------------
FwServices.grid.method = function(request, grid, method, $elementToBlock, onSuccess, onError) {
    if (FwAppData.useWebApi) {
        FwAppData.jsonPost(true, grid + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    } else {
        FwAppData.jsonPost(true, 'services.ashx?path=/grid/' + grid + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    }
};
//----------------------------------------------------------------------------------------------
FwServices.validation.method = function(request, validation, method, $elementToBlock, onSuccess, onError) {
    if (FwAppData.useWebApi) {
        FwAppData.jsonPost(true, grid + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    } else {
        FwAppData.jsonPost(true, 'services.ashx?path=/validation/' + validation + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
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