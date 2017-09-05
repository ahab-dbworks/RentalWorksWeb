class CompanyTaxGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyTaxGrid';
        this.apiurl = 'api/v1/companytaxoption';
    }
}

(<any>window).CompanyTaxGridController = new CompanyTaxGrid();
//----------------------------------------------------------------------------------------------