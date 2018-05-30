var CustomerNoteGrid = (function () {
    function CustomerNoteGrid() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/customernote';
    }
    CustomerNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.value').val(usersid);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.text').val(name);
    };
    return CustomerNoteGrid;
}());
var CustomerNoteGridController = new CustomerNoteGrid();
//# sourceMappingURL=CustomerNoteGridController.js.map