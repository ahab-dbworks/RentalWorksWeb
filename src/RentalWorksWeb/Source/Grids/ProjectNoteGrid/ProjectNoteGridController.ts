﻿class ProjectNoteGrid {
    Module: string = 'ProjectNoteGrid';
    apiurl: string = 'api/v1/projectnote';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');                 // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        const name = sessionStorage.getItem('name');

        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'UserId', { value: usersid, text: name });
    }
}

var ProjectNoteGridController = new ProjectNoteGrid();
//----------------------------------------------------------------------------------------------