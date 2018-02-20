class InventoryConsignmentGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryConsignmentGrid';
        this.apiurl = 'api/v1/inventoryconsignor';
    }
}

var InventoryConsignmentGridController = new InventoryConsignmentGrid();
//----------------------------------------------------------------------------------------------