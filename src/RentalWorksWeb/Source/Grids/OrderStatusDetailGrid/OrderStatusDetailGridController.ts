class OrderStatusDetailGrid {
    Module: string = 'OrderStatusDetailGrid';
    apiurl: string = 'api/v1/orderstatusdetail';

    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let isSuspendIn = $tr.find('[data-browsedatafield="IsSuspendIn"]').attr('data-originalvalue');
            let isSuspendOut = $tr.find('[data-browsedatafield="IsSuspendOut"]').attr('data-originalvalue');

            if (isSuspendIn === 'true') {
                $tr.find('[data-browsedatafield="InContractId"] div.btnpeek').hide();
            }

            if (isSuspendOut === 'true') {
                $tr.find('[data-browsedatafield="OutContractId"] div.btnpeek').hide();
            }

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

var OrderStatusDetailGridController = new OrderStatusDetailGrid();
//----------------------------------------------------------------------------------------------