class OrderNoteGrid {
    Module: string = 'OrderNoteGrid';
    apiurl: string = 'api/v1/ordernote';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        const today = FwLocale.getDate();
        const usersid = sessionStorage.getItem('usersid');                 // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        const name = sessionStorage.getItem('name');

        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'UserId', { value: usersid, text: name });
    }
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'UserId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
        }
    }
}

var OrderNoteGridController = new OrderNoteGrid();
//----------------------------------------------------------------------------------------------