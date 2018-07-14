class ProjectNoteGrid {
    constructor() {
        this.Module = 'ProjectNoteGrid';
        this.apiurl = 'api/v1/projectnote';
    }
    onRowNewMode($control, $tr) {
        const today = FwFunc.getDate();
        const usersid = sessionStorage.getItem('usersid');
        const name = sessionStorage.getItem('name');
        FwBrowse.setFieldValue($control, $tr, 'NoteDate', { value: today });
        FwBrowse.setFieldValue($control, $tr, 'UserId', { value: usersid, text: name });
    }
}
var ProjectNoteGridController = new ProjectNoteGrid();
//# sourceMappingURL=ProjectNoteGridController.js.map