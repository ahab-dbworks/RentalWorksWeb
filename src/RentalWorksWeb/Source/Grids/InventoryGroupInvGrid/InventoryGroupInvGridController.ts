class InventoryGroupInvGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryGroupInvGrid';
        this.apiurl = 'api/v1/inventorygroupinventory';
    }
}

(<any>window).InventoryGroupInvGridController = new InventoryGroupInvGrid();
//----------------------------------------------------------------------------------------------