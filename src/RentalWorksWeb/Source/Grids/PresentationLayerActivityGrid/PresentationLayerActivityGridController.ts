class PresentationLayerActivityGrid {
    Module: string = 'PresentationLayerActivityGrid';
    apiurl: string = 'api/v1/presentationlayeractivity';

    addLegend($control: any) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
}

var PresentationLayerActivityGridController = new PresentationLayerActivityGrid();
//----------------------------------------------------------------------------------------------