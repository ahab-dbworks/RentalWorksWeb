class DiscountItemRentalGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemRentalGrid';
        this.apiurl = 'api/v1/discountitem';
    }
}

(<any>window).DiscountItemRentalGridController = new DiscountItemRentalGrid();
//----------------------------------------------------------------------------------------------