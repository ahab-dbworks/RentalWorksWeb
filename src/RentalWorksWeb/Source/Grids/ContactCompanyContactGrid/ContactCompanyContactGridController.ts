class ContactCompanyContactGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ContactCompanyContactGrid';
        this.apiurl = 'api/v1/companycontact';
    }


}

(<any>window).ContactCompanyContactGridController = new ContactCompanyContactGrid();
//----------------------------------------------------------------------------------------------