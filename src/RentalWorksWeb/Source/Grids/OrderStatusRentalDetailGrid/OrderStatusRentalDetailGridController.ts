class OrderStatusRentalDetailGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderStatusRentalDetailGrid';
        this.apiurl = 'api/v1/orderstatussummary';
    }
}

(<any>window).OrderStatusRentalDetailGridController = new OrderStatusRentalDetailGrid();
//----------------------------------------------------------------------------------------------