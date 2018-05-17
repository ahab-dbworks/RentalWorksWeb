class OrderNoteGrid {
    Module: string = 'OrderNoteGrid';
    apiurl: string = 'api/v1/ordernote';

    //generateRow($control, $generatedtr) {
    //    const today = new Date(Date.now()).toLocaleString().split(',')[0]; 
    //    $generatedtr.find('.buttonbar').data('click', function ($tr) {
    //        $generatedtr.find('.field[data-browsedatafield="NoteDate"] input.value').val(today);
    //        //$generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($tr.find('.field[data-browsedatafield="ContactTitle"]').attr('data-originalvalue'));
    //        //$generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
    //        //$generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
    //    });
    //};

    onRowNewMode($control: JQuery, $tr: JQuery) {
        // need to default the date and user, haven't tested the date code below, so leaving commented for now
            const today = new Date(Date.now()).toLocaleString().split(',')[0];
            $tr.find('[data-formdatafield="NoteDate"] input.value').val(today);
            $tr.find('[data-formdatafield="NotesBy"] input.value').val();
            $tr.find('[data-formdatafield="NotesBy"] input.text').val();
    }


}

var OrderNoteGridController = new OrderNoteGrid();
//----------------------------------------------------------------------------------------------