class LaborCategoryGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'LaborCategoryGrid';
        this.apiurl = 'api/v1/laborcategory';
    }
}

var LaborCategoryGridController = new LaborCategoryGrid();
//----------------------------------------------------------------------------------------------