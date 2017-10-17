class ItemAttributeValueGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ItemAttributeValueGrid';
        this.apiurl = 'api/v1/itemattributevalue';
    }
}

(<any>window).ItemAttributeValueGridController = new ItemAttributeValueGrid();
//----------------------------------------------------------------------------------------------