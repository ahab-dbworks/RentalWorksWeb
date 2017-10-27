class SalesInventoryCompatibilityGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SalesInventoryCompatibilityGrid';
        this.apiurl = 'api/v1/inventorycompatible';
    }
}

(<any>window).SalesInventoryCompatibilityGridController = new SalesInventoryCompatibilityGrid();
//----------------------------------------------------------------------------------------------