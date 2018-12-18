class CheckedInItemGrid {
    Module: string = 'CheckedInItemGrid';
    apiurl: string = 'api/v1/checkedinitem';

    generateRow($control, $generatedtr) {
        let $form, errorSound, $quantityColumn;
        $form = $control.closest('.fwform');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            $tr.find('.browsecontextmenucell').css('pointer-events', 'none'); // disables contextmenu on grid row
        });
    }

    addLegend($control) {
        FwBrowse.addLegend($control, 'Swapped Item', '#dc407e');
    }
}

var CheckedInItemGridController = new CheckedInItemGrid();
//----------------------------------------------------------------------------------------------