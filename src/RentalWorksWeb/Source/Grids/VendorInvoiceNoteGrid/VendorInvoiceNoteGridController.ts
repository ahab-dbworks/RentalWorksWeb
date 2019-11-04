﻿class VendorInvoiceNoteGrid {
     Module: string = 'VendorInvoiceNoteGrid';
     apiurl: string = 'api/v1/vendorinvoicenote';

     onRowNewMode($control: JQuery, $tr: JQuery) {
         const today = FwFunc.getDate();
         const usersid = sessionStorage.getItem('usersid');                 // justin hoffman  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
         const name = sessionStorage.getItem('name');

         FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
         FwBrowse.setFieldValue($control, $tr, 'UsersId', { value: usersid, text: name });
     }

}
//----------------------------------------------------------------------------------------------
var VendorInvoiceNoteGridController = new VendorInvoiceNoteGrid();