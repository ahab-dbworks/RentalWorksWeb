class LaborCategoryGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'LaborCategoryGrid';
        this.apiurl = 'api/v1/laborcategory';
    }
}

(<any>window).LaborCategoryGridController = new LaborCategoryGrid();
//----------------------------------------------------------------------------------------------