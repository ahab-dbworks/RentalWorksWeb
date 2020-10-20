class ContractSummaryGrid {
    Module: string = 'ContractSummaryGrid';
    apiurl: string = 'api/v1/contractitemsummary';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            //set validation dynamically
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

var ContractSummaryGridController = new ContractSummaryGrid();
//----------------------------------------------------------------------------------------------