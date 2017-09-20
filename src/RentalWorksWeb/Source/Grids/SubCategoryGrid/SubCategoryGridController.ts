class SubCategoryGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SubCategoryGrid';
        this.apiurl = 'api/v1/subcategory';
    }
}

(<any>window).SubCategoryGridController = new SubCategoryGrid();
//----------------------------------------------------------------------------------------------