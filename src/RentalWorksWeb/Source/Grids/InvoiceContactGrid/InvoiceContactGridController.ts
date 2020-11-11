class InvoiceContactGrid {
    Module: string = 'InvoiceContactGrid';
    apiurl: string = 'api/v1/invoicecontact';

    addLegend($control) {
        FwBrowse.addLegend($control, 'Ordered By', '#00c400');
    }
}

var InvoiceContactGridController = new InvoiceContactGrid();
//----------------------------------------------------------------------------------------------