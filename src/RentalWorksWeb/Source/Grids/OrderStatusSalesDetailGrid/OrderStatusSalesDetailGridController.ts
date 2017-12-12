class OrderStatusSalesDetailGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderStatusSalesDetailGrid';
        this.apiurl = 'api/v1/orderstatusdetail';
    }
}

(<any>window).OrderStatusSalesDetailGridController = new OrderStatusSalesDetailGrid();
//----------------------------------------------------------------------------------------------