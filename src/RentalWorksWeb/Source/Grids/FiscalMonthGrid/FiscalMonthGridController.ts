class FiscalMonthGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FiscalMonthGrid';
        this.apiurl = 'api/v1/fiscalmonth';
    }
}

var FiscalMonthGridController = new FiscalMonthGrid();
//----------------------------------------------------------------------------------------------