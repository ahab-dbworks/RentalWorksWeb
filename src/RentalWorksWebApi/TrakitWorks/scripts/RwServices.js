//----------------------------------------------------------------------------------------------
var RwServices = {
    account: {},
    module:  {},
    session: {}
};
//----------------------------------------------------------------------------------------------
RwServices.session.updatelocation = function(request, doneCallback) { FwAppData.jsonPost(true, 'services.ashx?path=/session/updatelocation', request, FwServices.defaultTimeout, doneCallback); };
//----------------------------------------------------------------------------------------------