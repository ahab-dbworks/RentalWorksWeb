var CustomerNoteGrid = /** @class */ (function () {
    function CustomerNoteGrid() {
        this.Module = 'CustomerNoteGrid';
        this.apiurl = 'api/v1/customernote';
    }
    CustomerNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        // need to default the date and user, haven't tested the date code below, so leaving commented for now
        //var date = new Date();
        //var formattedDate = date.getMonth() + 1 + "/" + date.getDate() + "/" + date.getFullYear();
        //$tr.find('[data-formdatafield="NoteDate"] input.value').val(formattedDate);
        //$tr.find('[data-formdatafield="NotesBy"] input.value').val();
        //$tr.find('[data-formdatafield="NotesBy"] input.text').val();
    };
    return CustomerNoteGrid;
}());
window.CustomerNoteGridController = new CustomerNoteGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=CustomerNoteGridController.js.map