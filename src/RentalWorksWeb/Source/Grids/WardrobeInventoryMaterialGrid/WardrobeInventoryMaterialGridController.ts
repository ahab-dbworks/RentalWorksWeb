class WardrobeInventoryMaterialGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WardrobeInventoryMaterial';
        this.apiurl = 'api/v1/inventorymaterial';
    }
}

var WardrobeInventoryMaterialGridController = new WardrobeInventoryMaterialGrid();
//----------------------------------------------------------------------------------------------