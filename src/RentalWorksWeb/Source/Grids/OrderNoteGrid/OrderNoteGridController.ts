class OrderNoteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderNoteGrid';
        this.apiurl = 'api/v1/ordernote';
    }
}

var OrderNoteGridController = new OrderNoteGrid();
//----------------------------------------------------------------------------------------------