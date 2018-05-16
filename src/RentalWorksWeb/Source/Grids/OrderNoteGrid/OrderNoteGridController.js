var OrderNoteGrid = (function () {
    function OrderNoteGrid() {
        this.Module = 'OrderNoteGrid';
        this.apiurl = 'api/v1/ordernote';
    }
    OrderNoteGrid.prototype.generateRow = function ($control, $generatedtr) {
        var today = new Date(Date.now()).toLocaleString().split(',')[0];
        $generatedtr.find('.buttonbar').data('click', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="NoteDate"] input.value').val(today);
        });
    };
    ;
    return OrderNoteGrid;
}());
var OrderNoteGridController = new OrderNoteGrid();
//# sourceMappingURL=OrderNoteGridController.js.map