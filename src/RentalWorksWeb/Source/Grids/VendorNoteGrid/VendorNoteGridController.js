var VendorNoteGrid = (function () {
    function VendorNoteGrid() {
        this.Module = 'VendorNoteGrid';
        this.apiurl = 'api/v1/vendornote';
    }
    VendorNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.value').val(usersid);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.text').val(name);
    };
    return VendorNoteGrid;
}());
var VendorNoteGridController = new VendorNoteGrid();
//# sourceMappingURL=VendorNoteGridController.js.map