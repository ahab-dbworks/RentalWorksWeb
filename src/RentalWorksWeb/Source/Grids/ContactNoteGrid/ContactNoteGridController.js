var ContactNoteGrid = (function () {
    function ContactNoteGrid() {
        this.Module = 'ContactNoteGrid';
        this.apiurl = 'api/v1/contactnote';
    }
    ContactNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.value').val(usersid);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.text').val(name);
    };
    return ContactNoteGrid;
}());
var ContactNoteGridController = new ContactNoteGrid();
//# sourceMappingURL=ContactNoteGridController.js.map