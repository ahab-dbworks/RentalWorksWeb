var ProjectNoteGrid = (function () {
    function ProjectNoteGrid() {
        this.Module = 'ProjectNoteGrid';
        this.apiurl = 'api/v1/projectnote';
    }
    ProjectNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        $tr.find('[data-browsedisplayfield="UserName"] input.value').val(usersid);
        $tr.find('[data-browsedisplayfield="UserName"] input.text').val(name);
    };
    return ProjectNoteGrid;
}());
var ProjectNoteGridController = new ProjectNoteGrid();
//# sourceMappingURL=ProjectNoteGridController.js.map