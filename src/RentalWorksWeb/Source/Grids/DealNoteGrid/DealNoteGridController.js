var DealNoteGrid = (function () {
    function DealNoteGrid() {
        this.Module = 'Deal Note Grid';
        this.apiurl = 'api/v1/dealnote';
    }
    DealNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = new Date(Date.now()).toLocaleString().split(',')[0];
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
    };
    return DealNoteGrid;
}());
var DealNoteGridController = new DealNoteGrid();
//# sourceMappingURL=DealNoteGridController.js.map