class MiscCategoryGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'MiscCategoryGrid';
        this.apiurl = 'api/v1/misccategory';
    }
}

(<any>window).MiscCategoryGridController = new MiscCategoryGrid();
//----------------------------------------------------------------------------------------------