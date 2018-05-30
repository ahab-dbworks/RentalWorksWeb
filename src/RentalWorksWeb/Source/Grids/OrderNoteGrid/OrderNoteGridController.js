var OrderNoteGrid = (function () {
    function OrderNoteGrid() {
        this.Module = 'OrderNoteGrid';
        this.apiurl = 'api/v1/ordernote';
    }
    OrderNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        $tr.find('[data-browsedisplayfield="UserName"] input.value').val(usersid);
        $tr.find('[data-browsedisplayfield="UserName"] input.text').val(name);
    };
    return OrderNoteGrid;
}());
var OrderNoteGridController = new OrderNoteGrid();
//# sourceMappingURL=OrderNoteGridController.js.map