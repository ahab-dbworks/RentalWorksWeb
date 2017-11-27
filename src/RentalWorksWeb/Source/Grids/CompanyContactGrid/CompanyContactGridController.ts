class CompanyContactGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CompanyContactGrid';
        this.apiurl = 'api/v1/companycontact';
    }
}

(<any>window).CompanyContactGridController = new CompanyContactGrid();
//----------------------------------------------------------------------------------------------