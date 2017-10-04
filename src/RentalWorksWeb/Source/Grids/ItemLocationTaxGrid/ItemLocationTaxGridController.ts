class ItemLocationTaxGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ItemLocationTaxGrid';
        this.apiurl = 'api/v1/itemlocationtax';
    }
}

(<any>window).ItemLocationTaxGridController = new ItemLocationTaxGrid();
//----------------------------------------------------------------------------------------------