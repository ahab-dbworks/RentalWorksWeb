class OrderStatusHistoryGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderStatusHistoryGrid';
        this.apiurl = 'api/v1/orderstatushistory';
    }
}

(<any>window).OrderStatusHistoryGridController = new OrderStatusHistoryGrid();
//----------------------------------------------------------------------------------------------