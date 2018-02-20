class OrderStatusSummaryGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderStatusSummaryGrid';
        this.apiurl = 'api/v1/orderstatussummary';
    }
}

var OrderStatusSummaryGridController = new OrderStatusSummaryGrid();
//----------------------------------------------------------------------------------------------