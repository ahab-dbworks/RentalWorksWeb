class InventoryQcGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryQcGrid';
        this.apiurl = 'api/v1/inventorywarehouse';
    }
}

var InventoryQcGridController = new InventoryQcGrid();
//----------------------------------------------------------------------------------------------