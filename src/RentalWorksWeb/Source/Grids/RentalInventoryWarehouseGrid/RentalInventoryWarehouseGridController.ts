﻿class RentalInventoryWarehouseGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'RentalInventoryWarehouseGrid';
        this.apiurl = 'api/v1/inventorywarehouse';
    }
}

(<any>window).RentalInventoryWarehouseGridController = new RentalInventoryWarehouseGrid();
//----------------------------------------------------------------------------------------------