class CompanyResaleGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyResaleGrid';
        this.apiurl = 'api/v1/companytaxresale';
    }
}

(<any>window).CompanyResaleGridController = new CompanyResaleGrid();
//----------------------------------------------------------------------------------------------