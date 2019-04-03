class BaseInventoryValidation {
    constructor() {
        this.Module = 'BaseInventoryValidation';
        this.apiurl = 'api/v1/baseinventory';
    }
    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $control);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
}
//# sourceMappingURL=BaseInventoryValidationController.js.map