class ExchangeItemGrid {
    Module: string = 'ExchangeItemGrid';
    apiurl: string = 'api/v1/exchangeitem';

    addLegend($control) {

        FwBrowse.addLegend($control, 'Pending Exchange', '#FFFF80'); // temp hard-coded until color settings module has been created

        //try {
        //    FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, response => {
        //        for (let key in response) {
        //            FwBrowse.addLegend($control, key, response[key]);
        //        }
        //    }, ex => {
        //        FwFunc.showError(ex);
        //    }, $control);
        //} catch (ex) {
        //    FwFunc.showError(ex);
        //}
    }
}

var ExchangeItemGridController = new ExchangeItemGrid();
//----------------------------------------------------------------------------------------------