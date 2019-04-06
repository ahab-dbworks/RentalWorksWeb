class InventoryContainerGrid {
    Module: string = 'InventoryContainerGrid';
    apiurl: string = 'api/v1/inventorycontaineritem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val(1);
        });
    };
}

var InventoryContainerGridController = new InventoryContainerGrid();
//----------------------------------------------------------------------------------------------