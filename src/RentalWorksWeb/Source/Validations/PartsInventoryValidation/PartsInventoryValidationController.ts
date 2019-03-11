class PartsInventoryValidation {
    Module: string = 'PartsInventoryValidation';
    apiurl: string = 'api/v1/partsinventory';

    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `api/v1/legend/partsinventory`, null, FwServices.defaultTimeout, function onSuccess(response) {
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

var PartsInventoryValidationController = new PartsInventoryValidation();