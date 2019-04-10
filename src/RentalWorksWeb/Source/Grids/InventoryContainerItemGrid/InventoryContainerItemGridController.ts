class InventoryContainerItemGrid {
    Module: string = 'InventoryContainerItemGrid';
    apiurl: string = 'api/v1/inventorycontaineritem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val(1);
            $generatedtr.find('.field[data-browsedatafield="TrackedBy"]').text($tr.find('.field[data-browsedatafield="TrackedBy"]').attr('data-originalvalue'));
        });
    };
}

var InventoryContainerItemGridController = new InventoryContainerItemGrid();
//----------------------------------------------------------------------------------------------