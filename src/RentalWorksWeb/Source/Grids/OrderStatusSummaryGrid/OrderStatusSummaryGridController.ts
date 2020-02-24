class OrderStatusSummaryGrid {
    Module: string = 'OrderStatusSummaryGrid';
    apiurl: string = 'api/v1/orderstatussummary';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const recType = FwBrowse.getValueByDataField($control, $tr, 'RecType');
            const $td = $tr.find('[data-browsedatafield="InventoryId"]');
            let peekForm;
            switch (recType) {
                case 'R':
                    peekForm = 'RentalInventory';
                    break;
                case 'S':
                    peekForm = 'SalesInventory';
                    break;
                case 'P':
                    peekForm = 'PartsInventory';
                    break;
            }
            $td.attr('data-peekForm', peekForm);
        });
    }
}

var OrderStatusSummaryGridController = new OrderStatusSummaryGrid();
//----------------------------------------------------------------------------------------------