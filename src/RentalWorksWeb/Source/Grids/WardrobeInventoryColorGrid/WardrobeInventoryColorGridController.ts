class WardrobeInventoryColorGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WardrobeInventoryColor';
        this.apiurl = 'api/v1/inventorycolor';
    }
}

(<any>window).WardrobeInventoryColorGridController = new WardrobeInventoryColorGrid();
//----------------------------------------------------------------------------------------------