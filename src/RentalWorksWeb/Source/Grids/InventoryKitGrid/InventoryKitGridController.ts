class InventoryKitGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryKitGrid';
        this.apiurl = 'api/v1/inventorypackageinventory';
    }
}

(<any>window).InventoryKitGridController = new InventoryKitGrid();
//----------------------------------------------------------------------------------------------