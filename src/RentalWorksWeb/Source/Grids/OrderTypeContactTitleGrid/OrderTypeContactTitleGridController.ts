class OrderTypeContactTitleGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Order Type Contact Title Grid';
        this.apiurl = 'api/v1/ordertypecontacttitle';
    }
}

(<any>window).OrderTypeContactTitleGridController = new OrderTypeContactTitleGrid();
//----------------------------------------------------------------------------------------------