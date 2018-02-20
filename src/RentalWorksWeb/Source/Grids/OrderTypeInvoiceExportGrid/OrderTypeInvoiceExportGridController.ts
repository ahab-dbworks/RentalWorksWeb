class OrderTypeInvoiceExportGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderTypeInvoiceExportGrid';
        this.apiurl = 'api/v1/ordertypelocation';
    }
}

var OrderTypeInvoiceExportGridController = new OrderTypeInvoiceExportGrid();
//----------------------------------------------------------------------------------------------