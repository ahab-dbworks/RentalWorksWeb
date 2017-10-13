class VendorGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'VendorGrid';
        this.apiurl = 'api/v1/vendor';
    }
}

(<any>window).VendorGridController = new VendorGrid();
//----------------------------------------------------------------------------------------------