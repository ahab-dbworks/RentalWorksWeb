﻿class OrderItemGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
    }
}

var OrderItemGridController = new OrderItemGrid();
//----------------------------------------------------------------------------------------------