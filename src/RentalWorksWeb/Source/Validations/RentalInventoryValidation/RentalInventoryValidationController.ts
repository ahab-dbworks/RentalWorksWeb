class RentalInventoryValidation {
    Module: string = 'RentalInventoryValidation';
    apiurl: string = 'api/v1/rentalinventory';



    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `api/v1/legend/rentalinventory`, null, FwServices.defaultTimeout, function onSuccess(response) {
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

var RentalInventoryValidationController = new RentalInventoryValidation();