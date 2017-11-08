class ContactTitleGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Contact Title Grid';
        this.apiurl = 'api/v1/ordertypecontacttitle';
    }
}

(<any>window).ContactTitleGridController = new ContactTitleGrid();
//----------------------------------------------------------------------------------------------