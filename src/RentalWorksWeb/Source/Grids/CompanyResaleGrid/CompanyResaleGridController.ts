class CompanyResaleGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyResaleGrid';
        this.apiurl = 'api/v1/companytaxresale';
    }
}

var CompanyResaleGridController = new CompanyResaleGrid();
//----------------------------------------------------------------------------------------------