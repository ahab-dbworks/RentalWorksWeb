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
    FwAppData.jsonPost(true, 'services.ashx?path=/module/' + module + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
};
//----------------------------------------------------------------------------------------------
FwServices.grid.method = function(request, grid, method, $elementToBlock, onSuccess, onError) {
    FwAppData.jsonPost(true, 'services.ashx?path=/grid/' + grid + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
};
//----------------------------------------------------------------------------------------------
FwServices.validation.method = function(request, validation, method, $elementToBlock, onSuccess, onError) {
    FwAppData.jsonPost(true, 'services.ashx?path=/validation/' + validation + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
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