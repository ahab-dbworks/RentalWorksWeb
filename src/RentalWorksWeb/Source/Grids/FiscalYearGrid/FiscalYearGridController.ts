class FiscalYearGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FiscalYearGrid';
        this.apiurl = 'api/v1/fiscalyear';
    }
}

(<any>window).FiscalYearGridController = new FiscalYearGrid();
//----------------------------------------------------------------------------------------------