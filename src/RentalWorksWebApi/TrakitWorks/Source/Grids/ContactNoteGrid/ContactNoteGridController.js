class ContactNoteGrid {
    constructor() {
        this.Module = 'ContactNoteGrid';
        this.apiurl = 'api/v1/contactnote';
    }
    onRowNewMode($control, $tr) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');
        const name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    }
}
var ContactNoteGridController = new ContactNoteGrid();
//# sourceMappingURL=ContactNoteGridController.js.map