class AttributeValueGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'AttributeValueGrid';
        this.apiurl = 'api/v1/attributevalue';
    }
}

(<any>window).AttributeValueGridController = new AttributeValueGrid();
//----------------------------------------------------------------------------------------------