class CustomerNoteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/customernote';
    }
}

(<any>window).CustomerNoteGridController = new CustomerNoteGrid();
//----------------------------------------------------------------------------------------------