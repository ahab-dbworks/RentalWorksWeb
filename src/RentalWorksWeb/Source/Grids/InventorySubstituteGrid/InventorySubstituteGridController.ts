class InventorySubstituteGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventorySubstituteGrid';
        this.apiurl = 'api/v1/inventorysubstitute';
    }
}

(<any>window).InventorySubstituteGridController = new InventorySubstituteGrid();
//----------------------------------------------------------------------------------------------