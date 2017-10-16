class InventoryCompleteKitGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryCompleteKitGrid';
        this.apiurl = 'api/v1/inventorycompletekit';
    }
}

(<any>window).InventoryCompleteKitGridController = new InventoryCompleteKitGrid();
//----------------------------------------------------------------------------------------------