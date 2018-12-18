class CheckInExceptionGrid {
    Module: string = 'CheckInExceptionGrid';
    apiurl: string = 'api/v1/checkinexception';

    generateRow($control, $generatedtr) {
        let $form, errorSound, $quantityColumn;
        $form = $control.closest('.fwform');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            $tr.find('.browsecontextmenucell').css('pointer-events', 'none'); // disables contextmenu on grid row
        });
    }
}

var CheckInExceptionGridController = new CheckInExceptionGrid();
//----------------------------------------------------------------------------------------------