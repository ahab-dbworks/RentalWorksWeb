class InventoryWarehouseStagingGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryWarehouseStagingGrid';
        this.apiurl = 'api/v1/inventorywarehouse';
    }
}

(<any>window).InventoryWarehouseStagingGridController = new InventoryWarehouseStagingGrid();
//----------------------------------------------------------------------------------------------