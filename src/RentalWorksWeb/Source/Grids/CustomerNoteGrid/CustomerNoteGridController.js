var CustomerNoteGrid = (function () {
    function CustomerNoteGrid() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/customernote';
    }
    CustomerNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
    };
    return CustomerNoteGrid;
}());
window.CustomerNoteGridController = new CustomerNoteGrid();
//# sourceMappingURL=CustomerNoteGridController.js.map