class SalesInventoryWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SalesInventoryWarehouseGrid';
        this.apiurl = 'api/v1/inventorywarehouse';
    }
}

var SalesInventoryWarehouseGridController = new SalesInventoryWarehouseGrid();
//----------------------------------------------------------------------------------------------