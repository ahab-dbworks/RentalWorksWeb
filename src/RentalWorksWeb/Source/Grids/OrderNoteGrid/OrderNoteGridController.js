class OrderNoteGrid {
    constructor() {
        this.Module = 'OrderNoteGrid';
        this.apiurl = 'api/v1/ordernote';
    }
    onRowNewMode($control, $tr) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');
        const name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'UserId', { value: usersid, text: name });
    }
}
var OrderNoteGridController = new OrderNoteGrid();
//# sourceMappingURL=OrderNoteGridController.js.map