class SalesInventorySubstituteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SalesInventorySubstituteGrid';
        this.apiurl = 'api/v1/inventorysubstitute';
    }
}

(<any>window).SalesInventorySubstituteGridController = new SalesInventorySubstituteGrid();
//----------------------------------------------------------------------------------------------