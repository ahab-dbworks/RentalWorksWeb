class GeneratorTypeWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FiscalYearGrid';
        this.apiurl = 'api/v1/generatortypewarehouse';
    }
}

(<any>window).FiscalYearGridController = new FiscalYearGrid();
//----------------------------------------------------------------------------------------------