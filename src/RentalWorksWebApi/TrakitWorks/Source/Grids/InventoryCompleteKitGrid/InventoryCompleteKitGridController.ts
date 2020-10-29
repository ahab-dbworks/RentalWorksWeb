class InventoryCompleteKitGrid {
    Module: string = 'InventoryCompleteKitGrid';
    apiurl: string = 'api/v1/inventorycompletekit';

    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        const availFor = FwFormField.getValueByDataField($form, 'AvailFor');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const icodetd = $tr.find('[data-validationname="InventoryValidation"]');
            const $categorytd = $tr.find('[data-validationname="CategoryValidation"]');
            let inventoryPeekForm;
            let categoryPeekForm;
            switch (availFor) {
                case 'R':
                    inventoryPeekForm = 'RentalInventory';
                    categoryPeekForm = 'RentalCategory';
                    break;
                case 'S':
                    inventoryPeekForm = 'SalesInventory';
                    categoryPeekForm = 'SalesCategory';
                    break;
                case 'P':
                    inventoryPeekForm = 'PartsInventory';
                    categoryPeekForm = 'PartsCategory';
                    break;
            }
            icodetd.attr('data-peekForm', inventoryPeekForm);
            $categorytd.attr('data-peekForm', categoryPeekForm);
        });
    }

}
var InventoryCompleteKitGridController = new InventoryCompleteKitGrid();
//----------------------------------------------------------------------------------------------