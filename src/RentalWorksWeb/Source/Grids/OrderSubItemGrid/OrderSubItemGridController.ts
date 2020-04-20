class OrderSubItemGrid {
    Module: string = 'OrderSubItemGrid';
    apiurl: string = 'api/v1/ordersubitem';

    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, response => {
                for (let key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, ex => {
                FwFunc.showError(ex);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }

}
//----------------------------------------------------------------------------------------------
var OrderSubItemGridController = new OrderSubItemGrid();