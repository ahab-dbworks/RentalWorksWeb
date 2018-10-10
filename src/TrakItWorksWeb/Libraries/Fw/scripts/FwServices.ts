//----------------------------------------------------------------------------------------------
class FwServicesClass {
    defaultTimeout: number = null;
    defaultOnError: () => void = null;
    utilities = {
        creditCardSale: function(request: any, $elementToBlock: JQuery, onSuccess: () => void) {
            FwAppData.jsonPost(false, 'services.ashx?path=/fwutilities/creditcardsale', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, $elementToBlock);
        }
    };
    account = {
        getAuthToken: function(request: any, $elementToBlock: JQuery, onSuccess: (response: any) => void) {
            FwAppData.jsonPost(false, 'services.ashx?path=/account/getauthtoken', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, $elementToBlock);
        },
        authPassword: function(request: any, onSuccess: (response: any) => void) {
            FwAppData.jsonPost(false, 'services.ashx?path=/account/authpassword', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, null);
        }
    };
    module = {
        method: function(request: any, module: string, method: string, $elementToBlock: JQuery, onSuccess: (response: any) => void, onError?: (error: any) => void) {
            var controller = window[module + 'Controller'];
            if (typeof controller === 'undefined') {
                throw module + 'Controller is not defined.'
            }
            if (typeof controller.apiurl !== 'undefined') {
                if (method === 'Load') {
                    var ids: any = [];
                    for (var key in request.ids) {
                        if (request.ids[key].value.length === 0) {
                            throw `Primary key ${key} is required.`;
                        }
                        ids.push(request.ids[key].value);
                    }
                    ids = ids.join('~');
                    FwAppData.apiMethod(true, 'GET', controller.apiurl + '/' + ids, null, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
                }
                else if (method === 'Delete') {
                    var ids: any = [];
                    for (var key in request.ids) {
                        ids.push(request.ids[key].value);
                    }
                    ids = ids.join('~');
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
        }
    };
    grid = {
        method: function(request: any, module: string, method: string, $elementToBlock: JQuery, onSuccess: (response: any) => void, onError?: (error: any) => void): void {
            var controller = window[module + 'Controller'];
            if (typeof controller === 'undefined') {
                throw module + 'Controller is not defined.'
            }
            if (typeof controller.apiurl !== 'undefined') {
                if (method === 'Load') {
                    var ids: any = [];
                    for (var key in request.ids) {
                        ids.push(request.ids[key].value);
                    }
                    ids = ids.join('~');
                    if (ids.length === 0) {
                        throw 'primary key id(s) cannot be blank';
                    }
                    FwAppData.apiMethod(true, 'GET', controller.apiurl + '/' + ids, null, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
                }
                else if (method === 'Delete') {
                    var ids: any = [];
                    for (var key in request.ids) {
                        ids.push(request.ids[key].value);
                    }
                    ids = ids.join('~');
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
        }
    };
    validation = {
        method: function(request: any, module: string, method: string, $control: JQuery, onSuccess: (response: any) => void, onError?: (error: any) => void) {
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
        }
    };

    getholidayevents(request: any, $elementToBlock: JQuery, onSuccess: () => void, onError?: () => void): void {
        FwAppData.jsonPost(true, 'services.ashx?path=/fwscheduler/getholidayevents', request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    }
    //----------------------------------------------------------------------------------------------
    callMethod(servicename: string, methodname: string, request: string, timeout: number, $elementToBlock: JQuery, onSuccess: () => void, onError: () => void): void {
        FwAppData.jsonPost(true,  'services.ashx?path=/services/' + servicename + '/' + methodname, request, timeout, onSuccess, onError, $elementToBlock);
    }
}
//----------------------------------------------------------------------------------------------
var FwServices = new FwServicesClass();