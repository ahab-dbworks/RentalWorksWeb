﻿class OrderStatusRentalDetailGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderStatusRentalDetailGrid';
        this.apiurl = 'api/v1/orderstatusdetail';
    }
}

(<any>window).OrderStatusRentalDetailGridController = new OrderStatusRentalDetailGrid();
//----------------------------------------------------------------------------------------------