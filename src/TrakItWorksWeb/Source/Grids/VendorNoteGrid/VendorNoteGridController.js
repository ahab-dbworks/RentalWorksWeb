class VendorNoteGrid {
    constructor() {
        this.Module = 'VendorNoteGrid';
        this.apiurl = 'api/v1/vendornote';
    }
    onRowNewMode($control, $tr) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');
        const name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    }
}
var VendorNoteGridController = new VendorNoteGrid();
//# sourceMappingURL=VendorNoteGridController.js.map