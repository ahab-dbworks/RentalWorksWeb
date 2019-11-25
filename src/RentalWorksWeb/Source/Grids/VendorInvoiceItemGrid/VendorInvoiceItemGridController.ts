﻿class VendorInvoiceItemGrid {
    Module: string = 'VendorInvoiceItemGrid';
     apiurl: string = 'api/v1/vendorinvoiceitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="FromDate"]').on('change', 'input.value', function ($tr) {
        });
     }
     beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
         switch (datafield) {
             case 'GlAccountId':
                 $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateglaccount`);
                 break;
         }
     }
}
//----------------------------------------------------------------------------------------------
var VendorInvoiceItemGridController = new VendorInvoiceItemGrid();