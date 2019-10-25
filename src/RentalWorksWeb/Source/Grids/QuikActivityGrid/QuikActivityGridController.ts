class QuikActivityGrid {
    Module: string = 'QuikActivityGrid';
    apiurl: string = 'api/v1/quikactivity';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const orderTypeController = FwBrowse.getValueByDataField($control, $tr, 'OrderTypeController')
            $tr.find('[data-browsedisplayfield="OrderNumber"]').attr('data-validationname', `${orderTypeController}Validation`);
        });
    }
}

var QuikActivityGridController = new QuikActivityGrid();
//----------------------------------------------------------------------------------------------