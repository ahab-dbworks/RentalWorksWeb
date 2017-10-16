class InventoryCompatibilityGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryCompatibilityGrid';
        this.apiurl = 'api/v1/inventorycompatible';
    }
}

(<any>window).InventoryCompatibilityGridController = new InventoryCompatibilityGrid();
//----------------------------------------------------------------------------------------------