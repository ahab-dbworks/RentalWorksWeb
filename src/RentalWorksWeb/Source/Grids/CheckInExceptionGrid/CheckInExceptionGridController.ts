class CheckInExceptionGrid {
    Module: string = 'CheckInExceptionGrid';
    apiurl: string = 'api/v1/checkinexception';

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
                case 'M':
                    peekForm = 'MiscRate';
                    break;
                case 'L':
                    peekForm = 'LaborRate';
                    break;
            }
            $td.attr('data-peekForm', peekForm);
        });
    }
}

var CheckInExceptionGridController = new CheckInExceptionGrid();
//----------------------------------------------------------------------------------------------