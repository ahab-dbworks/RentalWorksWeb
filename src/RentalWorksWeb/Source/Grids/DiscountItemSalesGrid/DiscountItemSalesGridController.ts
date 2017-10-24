class DiscountItemSalesGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemSalesGrid';
        this.apiurl = 'api/v1/discountitem';
    }
}

(<any>window).DiscountItemSalesGridController = new DiscountItemSalesGrid();
//----------------------------------------------------------------------------------------------