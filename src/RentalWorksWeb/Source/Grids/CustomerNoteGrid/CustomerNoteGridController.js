class CustomerNoteGrid {
    constructor() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/customernote';
    }
    onRowNewMode($control, $tr) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');
        const name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    }
}
var CustomerNoteGridController = new CustomerNoteGrid();
//# sourceMappingURL=CustomerNoteGridController.js.map