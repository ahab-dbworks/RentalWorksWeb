﻿class InventoryAttributeValueGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryAttributeValueGrid';
        this.apiurl = 'api/v1/inventoryattributevalue';
    }
}

var InventoryAttributeValueGridController = new InventoryAttributeValueGrid();
//----------------------------------------------------------------------------------------------