class InventoryAttributeValueGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryAttributeValueGrid';
        this.apiurl = 'api/v1/attributevalue';
    }
}

(<any>window).InventoryAttributeValueGridController = new InventoryAttributeValueGrid();
//----------------------------------------------------------------------------------------------