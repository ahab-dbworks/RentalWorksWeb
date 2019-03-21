class DealNoteGrid {
    constructor() {
        this.Module = 'DealNoteGrid';
        this.apiurl = 'api/v1/dealnote';
    }
    onRowNewMode($control, $tr) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');
        const name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    }
}
var DealNoteGridController = new DealNoteGrid();
//# sourceMappingURL=DealNoteGridController.js.map