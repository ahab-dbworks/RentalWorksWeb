class OrderTypeActivityDatesGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderTypeActivityDatesGrid';
        this.apiurl = 'api/v1/ordertypedatetype';
    }
}

(<any>window).OrderTypeActivityDatesGridController = new OrderTypeActivityDatesGrid();
//----------------------------------------------------------------------------------------------