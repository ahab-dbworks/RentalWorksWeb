//----------------------------------------------------------------------------------------------
class FwServicesClass {
    defaultTimeout: number = null;
    defaultOnError: () => void = null;
    utilities = {
        creditCardSale: function (request: any, $elementToBlock: JQuery, onSuccess: () => void) {
            FwAppData.jsonPost(false, 'services.ashx?path=/fwutilities/creditcardsale', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, $elementToBlock);
        }
    };
    account = {
        getAuthToken: function (request: any, $elementToBlock: JQuery, onSuccess: (response: any) => void) {
            FwAppData.jsonPost(false, 'services.ashx?path=/account/getauthtoken', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, $elementToBlock);
        },
        authPassword: function (request: any, onSuccess: (response: any) => void) {
            FwAppData.jsonPost(false, 'services.ashx?path=/account/authpassword', request, FwServices.defaultTimeout, onSuccess, FwServices.defaultOnError, null);
        }
    };
    module = {
        method: function (request: any, module: string, method: string, $form: JQuery, onSuccess: (response: any) => void, onError?: (error: any) => void) {
            var controller = window[module + 'Controller'];
            if (typeof controller === 'undefined') {
                throw module + 'Controller is not defined.'
            }
            let url = '';
            if (typeof $form.data('getapiurl') === 'function') {
                url = $form.data('getapiurl')(method);
            }
            if (url.length === 0 && typeof controller.apiurl !== 'undefined' || typeof $form.data('getbaseapiurl') === 'function') {
                if (typeof $form.data('getbaseapiurl') !== 'undefined') {
                    url = $form.data('getbaseapiurl')();
                }
                else if (typeof controller.apiurl !== 'undefined' && controller.apiurl.length > 0) {
                    url = controller.apiurl;
                }
                else {
                    throw `No apiurl defined for Module: ${module}`;
                }
                if (method === 'Load') {
                    var ids: any = [];
                    for (var key in request.ids) {
                        if (request.ids[key].value.length === 0) {
                            throw `Primary key ${key} is required.`;
                        }
                        ids.push(request.ids[key].value);
                    }
                    ids = ids.join('~');
                    url += '/' + ids
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
                    url += '/' + ids;
                }
                else if (method === 'Save') {
                    // do nothing
                } else {
                    url += '/' + method.toLowerCase()
                }
            }
            if (url.length === 0) {
                throw `apiurl property or getApiUrl function is required on: ${module}Controller`
            }
            if (method === 'Load') {
                FwAppData.apiMethod(true, 'GET', url, null, FwServices.defaultTimeout, onSuccess, onError, $form);
            }
            else if (method === 'Delete') {
                FwAppData.apiMethod(true, 'DELETE', url, null, FwServices.defaultTimeout, onSuccess, onError, $form);
            }
            else if (method === 'Save') {
                const controllerName = $form.attr('data-controller');
                const controller = (<any>window)[controllerName];
                const securityId = controller.id;
                const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, securityId);
                const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return (node.nodetype === 'ModuleAction' && node.properties.action === 'New');
                });
                const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return (node.nodetype === 'ModuleAction' && node.properties.action === 'Edit');
                });
                const nodeSave = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    return (node.nodetype === 'ModuleAction' && node.properties.action === 'Save');
                });
                const mode = $form.attr('data-mode');
                if (mode == 'NEW' && nodeNew !== null && nodeNew.properties.visible === 'T') {
                    FwAppData.apiMethod(true, 'POST', url, request, FwServices.defaultTimeout, onSuccess, onError, $form);
                    return;
                }
                else if (mode === 'EDIT' && nodeEdit !== null && nodeEdit.properties.visible === 'T') {
                    let putUrl = url;
                    switch ($form.attr('data-mode')) {
                        case 'EDIT':
                            const $uniqueIdFields = $form.data('uniqueids');
                            if (typeof $form.data('getapiurl') !== 'function' && $uniqueIdFields.length === 1) {
                                putUrl += `/${FwFormField.getValue2($uniqueIdFields.eq(0))}`;
                            } else if (typeof $form.data('getapiurl') !== 'function' && $uniqueIdFields.length > 1) {
                                throw 'Need to define a function $form.data(getapiurl) to define a custom url for this module which has multiple primary keys'
                            }
                            break;
                    }
                    FwAppData.apiMethod(true, 'PUT', putUrl, request, FwServices.defaultTimeout, onSuccess, onError, $form);
                    return;
                }
                else if ((mode === 'NEW' || mode === 'EDIT') && nodeSave != null && nodeSave.properties.visible === 'T') {
                    FwAppData.apiMethod(true, 'POST', url, request, FwServices.defaultTimeout, onSuccess, onError, $form);
                }
            }
            else {
                FwAppData.jsonPost(true, url, request, FwServices.defaultTimeout, onSuccess, onError, $form);
            }
        }
    };
    grid = {
        method: function (request: any, module: string, method: string, $grid: JQuery, onSuccess: (response: any) => void, onError?: (error: any) => void): void {
            var controller = window[module + 'Controller'];
            if (typeof controller === 'undefined') {
                throw module + 'Controller is not defined.'
            }
            let url = '';
            if (typeof $grid.data('getapiurl') === 'function') {
                url = controller.data('getapiurl')(method);
            }
            if (url.length === 0 && typeof controller.apiurl !== 'undefined' || typeof $grid.data('getbaseapiurl') === 'function') {
                if (typeof $grid.data('getbaseapiurl') !== 'undefined') {
                    url = $grid.data('getbaseapiurl')();
                }
                else if (typeof controller.apiurl !== 'undefined' && controller.apiurl.length > 0) {
                    url = controller.apiurl;
                }
                else {
                    throw `No apiurl defined for Grid: ${module}`;
                }
                if (method === 'Load') {
                    var ids: any = [];
                    for (var key in request.ids) {
                        ids.push(request.ids[key].value);
                    }
                    ids = ids.join('~');
                    if (ids.length === 0) {
                        throw 'primary key id(s) cannot be blank';
                    }
                    url += '/' + ids;
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
                    url += '/' + ids;
                }
                else if (method === 'Save' || method === 'Insert' || method === 'Update') {
                    // do nothing
                }
                else {
                    url += '/' + method.toLowerCase();
                }
            }
            if (url.length === 0) {
                throw `apiurl property or getApiUrl function is required on: ${module}Controller`
            }
            if (method === 'Load') {
                FwAppData.apiMethod(true, 'GET', url, null, FwServices.defaultTimeout, onSuccess, onError, $grid);
            }
            else if (method === 'Delete') {
                FwAppData.apiMethod(true, 'DELETE', url, null, FwServices.defaultTimeout, onSuccess, onError, $grid);
            }
            else if (method === 'Insert' || method === 'Update' || method === 'Save') {
                const secid = $grid.data('secid');
                if (typeof secid !== 'string' || secid.length === 0) {
                    let moduleName = $grid.find('.menucaption').text();
                    throw `Unable to perform '${method}' on Grid '${moduleName}, Grid is missing attribute secid.'`;
                }
                const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, $grid.data('secid'));
                if (nodeModule === null) {
                    let moduleName = $grid.find('.menucaption').text();
                    throw `Unable to perform '${method}' on Grid '${moduleName}, Unable to find security node for id '${secid}'`;
                }
                const nodeNew = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    if (nodeModule.nodetype === 'Module') {
                        return (node.nodetype === 'ModuleAction' && node.properties.action === 'New');
                    }
                    else if (nodeModule.nodetype === 'Control') {
                        return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlNew');
                    }
                    else {
                        return false;
                    }
                });
                const nodeEdit = FwApplicationTree.getNodeByFuncRecursive(nodeModule, {}, (node: any, args: any) => {
                    if (nodeModule.nodetype === 'Module') {
                        return (node.nodetype === 'ModuleAction' && node.properties.action === 'Edit');
                    }
                    else if (nodeModule.nodetype === 'Control') {
                        return (node.nodetype === 'ControlAction' && node.properties.action === 'ControlEdit');
                    }
                    else {
                        return false;
                    }
                });
                if (method === 'Save' || (method === 'Insert' && nodeNew !== null && nodeNew.properties.visible === 'T')) {
                    FwAppData.apiMethod(true, 'POST', url, request, FwServices.defaultTimeout, onSuccess, onError, $grid);
                }
                else if (method === 'Update' && nodeEdit !== null && nodeEdit.properties.visible === 'T') {
                    const $tr = $grid.find('.editrow').eq(0);
                    const $uniqueIdFields = $tr.find('.field[data-isuniqueid="true"]');
                    if (typeof $grid.data('getapiurl') !== 'function' && $uniqueIdFields.length === 1) {
                        url = `${url}/${FwBrowse.getValueByDataField($grid, $tr, $uniqueIdFields.data('formdatafield'))}`;
                    } else if (typeof $grid.data('getapiurl') !== 'function' && $uniqueIdFields.length > 1) {
                        //throw 'Need to define a function form.data(getapiurl) to define a custom url for this module which has multiple primary keys'
                        //justin hoffman 12/12/2019 commented above temporary to allow composite keys while we transition
                        var ids: any = [];
                        for (let i = 0; i < $uniqueIdFields.length; i++) {
                            let id = FwBrowse.getValueByDataField($grid, $tr, $uniqueIdFields.eq(i).data('formdatafield'));
                            ids.push(id);
                        }
                        ids = ids.join('~');
                        if (ids.length === 0) {
                            throw 'primary key id(s) cannot be blank';
                        }
                        url += '/' + ids
                    }
                    FwAppData.apiMethod(true, 'PUT', url, request, FwServices.defaultTimeout, onSuccess, onError, $grid);
                } else {
                    let moduleName = $grid.find('.menucaption').text();
                    throw `Unable to perform '${method}' on Grid '${moduleName}'`;
                }
            }
            else if (method === 'ValidateDuplicate') {
                FwAppData.jsonPost(true, url, request, FwServices.defaultTimeout, onSuccess, onError, null);
            } else {
                FwAppData.jsonPost(true, url, request, FwServices.defaultTimeout, onSuccess, onError, $grid);
            }
        }
    };
    validation = {
        method: function (request: any, module: string, method: string, $validationbrowse: JQuery, onSuccess: (response: any) => void, onError?: (error: any) => void) {
            var controller = window[module + 'Controller'];
            if (typeof controller === 'undefined') {
                throw module + 'Controller is not defined.'
            }
            if (typeof $validationbrowse.attr('data-apiurl') === 'string') {
                FwAppData.jsonPost(true, $validationbrowse.attr('data-apiurl') + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, $validationbrowse);
            }
            else if (typeof controller.apiurl !== 'undefined') {
                FwAppData.jsonPost(true, controller.apiurl + '/' + method.toLowerCase(), request, FwServices.defaultTimeout, onSuccess, onError, $validationbrowse);
            }
            else {
                FwAppData.jsonPost(true, 'services.ashx?path=/validation/' + module + '/' + method, request, FwServices.defaultTimeout, onSuccess, onError, $validationbrowse);
            }
        }
    };

    getholidayevents(request: any, $elementToBlock: JQuery, onSuccess: () => void, onError?: () => void): void {
        FwAppData.jsonPost(true, 'services.ashx?path=/fwscheduler/getholidayevents', request, FwServices.defaultTimeout, onSuccess, onError, $elementToBlock);
    }
    //----------------------------------------------------------------------------------------------
    callMethod(servicename: string, methodname: string, request: string, timeout: number, $elementToBlock: JQuery, onSuccess: () => void, onError: () => void): void {
        FwAppData.jsonPost(true, 'services.ashx?path=/services/' + servicename + '/' + methodname, request, timeout, onSuccess, onError, $elementToBlock);
    }
}
//----------------------------------------------------------------------------------------------
var FwServices = new FwServicesClass();