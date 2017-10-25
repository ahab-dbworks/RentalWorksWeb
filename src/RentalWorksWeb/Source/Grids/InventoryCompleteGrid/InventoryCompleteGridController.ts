class InventoryCompleteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryCompleteGrid';
        this.apiurl = 'api/v1/inventorypackageinventory';
    }
}

(<any>window).InventoryCompleteGridController = new InventoryCompleteGrid();
//----------------------------------------------------------------------------------------------