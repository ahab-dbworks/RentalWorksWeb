class CompanyTaxResaleGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyTaxGrid';
        this.apiurl = 'api/v1/companytaxresale';
    }
}

(<any>window).CompanyTaxResaleGridController = new CompanyTaxResaleGrid();
//----------------------------------------------------------------------------------------------