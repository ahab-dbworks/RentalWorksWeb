﻿class SalesInventoryWarehousePricingGrid {
    Module: string = 'InventoryWarehouseGrid';  //justin 04/10/2019 potential issue with shared Module value. See Issue #401
    //apiurl: string = 'api/v1/inventorywarehouse';
    apiurl: string = 'api/v1/pricing';
}

var SalesInventoryWarehousePricingGridController = new SalesInventoryWarehousePricingGrid();
//----------------------------------------------------------------------------------------------