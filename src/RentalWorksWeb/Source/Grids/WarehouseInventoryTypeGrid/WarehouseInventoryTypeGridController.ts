class WarehouseInventoryTypeGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WarehouseInventoryType';
        this.apiurl = 'api/v1/warehouseinventorytype';
    }
}

(<any>window).WarehouseInventoryTypeGridController = new WarehouseInventoryTypeGrid();
//----------------------------------------------------------------------------------------------