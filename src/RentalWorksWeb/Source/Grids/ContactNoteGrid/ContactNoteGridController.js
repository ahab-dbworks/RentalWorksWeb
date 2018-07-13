var ContactNoteGrid = (function () {
    function ContactNoteGrid() {
        this.Module = 'ContactNoteGrid';
        this.apiurl = 'api/v1/contactnote';
    }
    ContactNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    };
    return ContactNoteGrid;
}());
var ContactNoteGridController = new ContactNoteGrid();
//# sourceMappingURL=ContactNoteGridController.js.map