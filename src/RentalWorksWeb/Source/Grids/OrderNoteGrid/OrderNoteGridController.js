var OrderNoteGrid = (function () {
    function OrderNoteGrid() {
        this.Module = 'OrderNoteGrid';
        this.apiurl = 'api/v1/ordernote';
    }
    OrderNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = new Date(Date.now()).toLocaleString().split(',')[0];
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
    };
    return OrderNoteGrid;
}());
var OrderNoteGridController = new OrderNoteGrid();
//# sourceMappingURL=OrderNoteGridController.js.map