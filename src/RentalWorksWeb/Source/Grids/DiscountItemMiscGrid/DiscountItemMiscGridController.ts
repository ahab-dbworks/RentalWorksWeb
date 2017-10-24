class DiscountItemMiscGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemMiscGrid';
        this.apiurl = 'api/v1/discountitem';
    }
}

(<any>window).DiscountItemMiscGridController = new DiscountItemMiscGrid();
//----------------------------------------------------------------------------------------------