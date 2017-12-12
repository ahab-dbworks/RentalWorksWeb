class OrderPickListGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderPickListGrid';
        this.apiurl = 'api/v1/picklist';
    }
}

(<any>window).OrderPickListGridController = new OrderPickListGrid();
//----------------------------------------------------------------------------------------------