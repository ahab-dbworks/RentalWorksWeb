class OrderTypeNoteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Order Type Note Grid';
        this.apiurl = 'api/v1/ordertypenote';
    }
}

(<any>window).OrderTypeNoteGridController = new OrderTypeNoteGrid();
//----------------------------------------------------------------------------------------------