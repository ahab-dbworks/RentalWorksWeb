var CustomerNoteGrid = (function () {
    function CustomerNoteGrid() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/customernote';
    }
    CustomerNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    };
    return CustomerNoteGrid;
}());
var CustomerNoteGridController = new CustomerNoteGrid();
//# sourceMappingURL=CustomerNoteGridController.js.map