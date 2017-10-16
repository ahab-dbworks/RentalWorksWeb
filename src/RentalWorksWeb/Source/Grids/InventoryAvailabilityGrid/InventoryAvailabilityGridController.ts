class InventoryAvailabilityGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryAvailabilityGrid';
        this.apiurl = 'api/v1/inventorywarehouse';
    }
}

(<any>window).InventoryAvailabilityGridController = new InventoryAvailabilityGrid();
//----------------------------------------------------------------------------------------------