﻿class VendorNoteGrid {
    Module: string = 'VendorNoteGrid';
    apiurl: string = 'api/v1/vendornote';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');                 // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        const name = sessionStorage.getItem('name');

        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    }
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'NotesById':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatenotesby`);
                break;
        }
    }
}

var VendorNoteGridController = new VendorNoteGrid();
//----------------------------------------------------------------------------------------------