class DiscountItemLaborGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemLaborGrid';
        this.apiurl = 'api/v1/discountitem';
    }
}

(<any>window).DiscountItemLaborGridController = new DiscountItemLaborGrid();
//----------------------------------------------------------------------------------------------