class DiscountItemGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/discountitem';
    }
}

var DiscountItemGridController = new DiscountItemGrid();
//----------------------------------------------------------------------------------------------