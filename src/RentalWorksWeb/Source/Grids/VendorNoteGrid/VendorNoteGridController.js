var VendorNoteGrid = (function () {
    function VendorNoteGrid() {
        this.Module = 'VendorNoteGrid';
        this.apiurl = 'api/v1/vendornote';
    }
    VendorNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'NotesById', { value: usersid, text: name });
    };
    return VendorNoteGrid;
}());
var VendorNoteGridController = new VendorNoteGrid();
//# sourceMappingURL=VendorNoteGridController.js.map