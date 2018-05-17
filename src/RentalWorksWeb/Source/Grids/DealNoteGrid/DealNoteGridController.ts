class DealNoteGrid {
    Module: string = 'Deal Note Grid';
    apiurl: string = 'api/v1/dealnote';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        const today = new Date(Date.now()).toLocaleString().split(',')[0];
        $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
        //$tr.find('[data-formdatafield="NotesBy"] input.value').val();
        //$tr.find('[data-formdatafield="NotesBy"] input.text').val();
    }
}

var DealNoteGridController = new DealNoteGrid();
//----------------------------------------------------------------------------------------------