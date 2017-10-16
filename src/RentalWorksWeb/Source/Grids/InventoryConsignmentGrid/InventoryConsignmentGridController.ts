class InventoryConsignmentGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryConsignmentGrid';
        this.apiurl = 'api/v1/inventoryconsignor';
    }
}

(<any>window).InventoryConsignmentGridController = new InventoryConsignmentGrid();
//----------------------------------------------------------------------------------------------