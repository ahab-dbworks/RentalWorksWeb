class FiscalYearGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FiscalYearGrid';
        this.apiurl = 'api/v1/fiscalyear';
    }
}

var FiscalYearGridController = new FiscalYearGrid();
//----------------------------------------------------------------------------------------------