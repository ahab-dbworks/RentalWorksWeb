class ExchangeItemGrid {
    constructor() {
        this.Module = 'ExchangeItemGrid';
        this.apiurl = 'api/v1/exchangeitem';
    }
    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, response => {
                for (let key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, ex => {
                FwFunc.showError(ex);
            }, $control);
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
}
var ExchangeItemGridController = new ExchangeItemGrid();
//# sourceMappingURL=ExchangeItemGridController.js.map