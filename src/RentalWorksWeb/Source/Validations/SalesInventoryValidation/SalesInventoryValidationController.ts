class SalesInventoryValidation {
    Module: string = 'SalesInventoryValidation';
    apiurl: string = 'api/v1/salesinventory';

    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `api/v1/legend/salesinventory`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $control)
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
}

var SalesInventoryValidationController = new SalesInventoryValidation();