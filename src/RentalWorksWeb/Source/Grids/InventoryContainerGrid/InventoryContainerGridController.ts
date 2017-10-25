class InventoryContainerGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryContainerGrid';
        this.apiurl = 'api/v1/inventorycontaineritem';
    }
}

(<any>window).InventoryContainerGridController = new InventoryContainerGrid();
//----------------------------------------------------------------------------------------------