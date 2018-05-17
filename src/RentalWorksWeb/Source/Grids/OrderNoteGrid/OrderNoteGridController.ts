class OrderNoteGrid {
    Module: string = 'OrderNoteGrid';
    apiurl: string = 'api/v1/ordernote';

    onRowNewMode($control: JQuery, $tr: JQuery) {
            const today = new Date(Date.now()).toLocaleString().split(',')[0];
            $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
            //$tr.find('[data-formdatafield="NotesBy"] input.value').val();
            //$tr.find('[data-formdatafield="NotesBy"] input.text').val();
    }
}

var OrderNoteGridController = new OrderNoteGrid();
//----------------------------------------------------------------------------------------------