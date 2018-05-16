class OrderNoteGrid {
    Module: string = 'OrderNoteGrid';
    apiurl: string = 'api/v1/ordernote';

    generateRow($control, $generatedtr) {
        const today = new Date(Date.now()).toLocaleString().split(',')[0]; 
        $generatedtr.find('.buttonbar').data('click', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="NoteDate"] input.value').val(today);
            //$generatedtr.find('.field[data-browsedatafield="ContactTitleId"] input.text').val($tr.find('.field[data-browsedatafield="ContactTitle"]').attr('data-originalvalue'));
            //$generatedtr.find('.field[data-browsedatafield="OfficePhone"] input').val($tr.find('.field[data-browsedatafield="OfficePhone"]').attr('data-originalvalue'));
            //$generatedtr.find('.field[data-browsedatafield="Email"] input').val($tr.find('.field[data-browsedatafield="Email"]').attr('data-originalvalue'));
        });
    };


}

var OrderNoteGridController = new OrderNoteGrid();
//----------------------------------------------------------------------------------------------