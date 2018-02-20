class CompanyTaxOptionGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyTaxOptionGrid';
        this.apiurl = 'api/v1/companytaxoption';
    }
}

var CompanyTaxOptionGridController = new CompanyTaxOptionGrid();
//----------------------------------------------------------------------------------------------