class InventoryVendorGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryVendorGrid';
        this.apiurl = 'api/v1/inventoryvendor';
    }
}

var InventoryVendorGridController = new InventoryVendorGrid();
//----------------------------------------------------------------------------------------------