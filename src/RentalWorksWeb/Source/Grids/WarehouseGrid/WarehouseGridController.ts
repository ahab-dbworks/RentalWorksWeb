class WarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Warehouse';
        this.apiurl = 'api/v1/warehouse';
    }
}

(<any>window).WarehouseGridController = new WarehouseGrid();
//----------------------------------------------------------------------------------------------