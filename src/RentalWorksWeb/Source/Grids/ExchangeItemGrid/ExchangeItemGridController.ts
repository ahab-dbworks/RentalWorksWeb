class ExchangeItemGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ExchangeItemGrid';
        this.apiurl = 'api/v1/exchangeitem';
    }
}

var ExchangeItemGridController = new ExchangeItemGrid();
//----------------------------------------------------------------------------------------------