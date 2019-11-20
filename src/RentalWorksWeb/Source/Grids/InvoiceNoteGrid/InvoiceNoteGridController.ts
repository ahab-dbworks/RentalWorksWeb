class InvoiceNoteGrid {
    Module: string = 'InvoiceNoteGrid';
    apiurl: string = 'api/v1/invoicenote';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');                 // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        const name = sessionStorage.getItem('name');


        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    }

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        const $form = $gridbrowse.closest('.fwform');
        const invoiceId = FwFormField.getValueByDataField($form, 'InvoiceId');
        const validationName = request.module;

        switch (validationName) {
            case 'OrderValidation':
                request.uniqueids = {
                    InvoiceId: invoiceId
                }
                break;
        }
    }
}

var InvoiceNoteGridController = new InvoiceNoteGrid();
//----------------------------------------------------------------------------------------------