class CheckInSwapGrid {
    Module: string = 'CheckInSwapGrid';
    apiurl: string = 'api/v1/checkinswap';

    generateRow($control, $generatedtr) {
        let $form, errorSound, $quantityColumn;
        $form = $control.closest('.fwform');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            $tr.find('.browsecontextmenucell').css('pointer-events', 'none'); // disables contextmenu on grid row
        });
    }
}

var CheckInSwapGridController = new CheckInSwapGrid();
//----------------------------------------------------------------------------------------------