class FiscalMonthGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FiscalMonthGrid';
        this.apiurl = 'api/v1/fiscalmonth';
    }
}

(<any>window).FiscalMonthGridController = new FiscalMonthGrid();
//----------------------------------------------------------------------------------------------