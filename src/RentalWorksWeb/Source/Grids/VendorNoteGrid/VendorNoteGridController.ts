class VendorNoteGrid {
    Module: string = 'VendorNoteGrid';
    apiurl: string = 'api/v1/vendornote';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        const today = new Date(Date.now()).toLocaleString().split(',')[0];
        const usersid = sessionStorage.getItem('usersid');                 // J. Pace 5/25/18  C4E0E7F6-3B1C-4037-A50C-9825EDB47F44
        const name = sessionStorage.getItem('name');

        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.value').val(usersid);
        $tr.find('[data-browsedisplayfield="NotesBy"] input.text').val(name);
    }
}

var VendorNoteGridController = new VendorNoteGrid();
//----------------------------------------------------------------------------------------------