class AlternativeDescriptionGrid {
    Module: string = 'AlternativeDescriptionGrid';
    apiurl: string = 'api/v1/alternativedescription';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            if (FwBrowse.getValueByDataField($control, $tr, 'IsPrimary') == 'true') {
                $tr.css('background-color', 'orange');
            }
        });
    }
}

var AlternativeDescriptionGridController = new AlternativeDescriptionGrid();
//----------------------------------------------------------------------------------------------