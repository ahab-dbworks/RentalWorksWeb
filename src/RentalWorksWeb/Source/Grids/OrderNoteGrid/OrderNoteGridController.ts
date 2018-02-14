class OrderNoteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderNoteGrid';
        this.apiurl = 'api/v1/ordernote';
    }
}

(<any>window).OrderNoteGridController = new OrderNoteGrid();
//----------------------------------------------------------------------------------------------