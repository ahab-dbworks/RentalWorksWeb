class SalesInventoryWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SalesInventoryWarehouseGrid';
        this.apiurl = 'api/v1/inventorywarehouse';
    }
}

(<any>window).SalesInventoryWarehouseGridController = new SalesInventoryWarehouseGrid();
//----------------------------------------------------------------------------------------------