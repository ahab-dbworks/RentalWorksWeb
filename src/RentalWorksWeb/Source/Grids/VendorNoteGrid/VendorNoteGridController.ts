class VendorNoteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'VendorNoteGrid';
        this.apiurl = 'api/v1/vendornote';
    }
}

(<any>window).VendorNoteGridController = new VendorNoteGrid();
//----------------------------------------------------------------------------------------------