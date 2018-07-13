var DealNoteGrid = (function () {
    function DealNoteGrid() {
        this.Module = 'Deal Note Grid';
        this.apiurl = 'api/v1/dealnote';
    }
    DealNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    };
    return DealNoteGrid;
}());
var DealNoteGridController = new DealNoteGrid();
//# sourceMappingURL=DealNoteGridController.js.map