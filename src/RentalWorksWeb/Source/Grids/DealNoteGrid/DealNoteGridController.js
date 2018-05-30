var DealNoteGrid = (function () {
    function DealNoteGrid() {
        this.Module = 'Deal Note Grid';
        this.apiurl = 'api/v1/dealnote';
    }
    DealNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.value').val(usersid);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.text').val(name);
    };
    return DealNoteGrid;
}());
var DealNoteGridController = new DealNoteGrid();
//# sourceMappingURL=DealNoteGridController.js.map