var OrderNoteGrid = (function () {
    function OrderNoteGrid() {
        this.Module = 'OrderNoteGrid';
        this.apiurl = 'api/v1/ordernote';
    }
    OrderNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'UserName', { value: usersid, text: name });
    };
    return OrderNoteGrid;
}());
var OrderNoteGridController = new OrderNoteGrid();
//# sourceMappingURL=OrderNoteGridController.js.map