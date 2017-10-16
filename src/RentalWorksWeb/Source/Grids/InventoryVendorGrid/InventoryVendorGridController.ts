class InventoryVendorGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryVendorGrid';
        this.apiurl = 'api/v1/inventoryvendor';
    }
}

(<any>window).InventoryVendorGridController = new InventoryVendorGrid();
//----------------------------------------------------------------------------------------------