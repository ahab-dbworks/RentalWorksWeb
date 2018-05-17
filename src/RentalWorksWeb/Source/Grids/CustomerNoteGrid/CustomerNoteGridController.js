var CustomerNoteGrid = (function () {
    function CustomerNoteGrid() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/customernote';
    }
    CustomerNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = new Date(Date.now()).toLocaleString().split(',')[0];
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
    };
    return CustomerNoteGrid;
}());
var CustomerNoteGridController = new CustomerNoteGrid();
//# sourceMappingURL=CustomerNoteGridController.js.map