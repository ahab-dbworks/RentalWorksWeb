﻿class RentalInventoryWarehouseGrid {
    Module: string = 'InventoryWarehouseGrid';  //justin 04/10/2019 potential issue with shared Module value. See Issue #401
    apiurl: string = 'api/v1/inventorywarehouse';
}

var RentalInventoryWarehouseGridController = new RentalInventoryWarehouseGrid();
//----------------------------------------------------------------------------------------------