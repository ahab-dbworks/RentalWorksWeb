class OrderPickListGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderPickListGrid';
        this.apiurl = 'api/v1/picklist';
    }
}

var OrderPickListGridController = new OrderPickListGrid();
//----------------------------------------------------------------------------------------------