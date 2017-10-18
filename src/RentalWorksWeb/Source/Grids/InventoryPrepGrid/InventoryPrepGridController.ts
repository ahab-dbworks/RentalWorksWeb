class InventoryPrepGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryPrepGrid';
        this.apiurl = 'api/v1/inventoryprep';
    }
}

(<any>window).InventoryPrepGridController = new InventoryPrepGrid();
//----------------------------------------------------------------------------------------------