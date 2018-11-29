﻿class VendorInvoiceItemGrid {
    Module: string = 'VendorInvoiceItemGrid';
     apiurl: string = 'api/v1/vendorinvoiceitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="FromDate"]').on('change', 'input.value', function ($tr) {
        });
    }
}
//----------------------------------------------------------------------------------------------
var VendorInvoiceItemGridController = new VendorInvoiceItemGrid();