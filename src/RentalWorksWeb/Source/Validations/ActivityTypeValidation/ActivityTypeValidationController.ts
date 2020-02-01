class ActivityTypeValidation {
    Module: string = 'ActivityTypeValidation';
    apiurl: string = 'api/v1/activitytype';

    addLegend($control) {

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
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

var ActivityTypeValidationController = new ActivityTypeValidation();