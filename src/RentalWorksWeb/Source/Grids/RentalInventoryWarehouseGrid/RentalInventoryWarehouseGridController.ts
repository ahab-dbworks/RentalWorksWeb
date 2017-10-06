class RentalInventoryWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'RentalInventoryWarehouseGrid';
        this.apiurl = 'api/v1/itemwarehouse';
    }
}

(<any>window).RentalInventoryWarehouseGridController = new RentalInventoryWarehouseGrid();
//----------------------------------------------------------------------------------------------