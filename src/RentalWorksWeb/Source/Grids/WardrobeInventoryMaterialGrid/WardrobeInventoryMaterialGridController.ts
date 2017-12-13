class WardrobeInventoryMaterialGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WardrobeInventoryMaterial';
        this.apiurl = 'api/v1/inventorymaterial';
    }
}

(<any>window).WardrobeInventoryMaterialGridController = new WardrobeInventoryMaterialGrid();
//----------------------------------------------------------------------------------------------