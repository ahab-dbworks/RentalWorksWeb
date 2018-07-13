var ProjectNoteGrid = (function () {
    function ProjectNoteGrid() {
        this.Module = 'ProjectNoteGrid';
        this.apiurl = 'api/v1/projectnote';
    }
    ProjectNoteGrid.prototype.onRowNewMode = function ($control, $tr) {
        var today = FwFunc.getDate();
        var usersid = sessionStorage.getItem('usersid');
        var name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'UserId', { value: usersid, text: name });
    };
    return ProjectNoteGrid;
}());
var ProjectNoteGridController = new ProjectNoteGrid();
//# sourceMappingURL=ProjectNoteGridController.js.map