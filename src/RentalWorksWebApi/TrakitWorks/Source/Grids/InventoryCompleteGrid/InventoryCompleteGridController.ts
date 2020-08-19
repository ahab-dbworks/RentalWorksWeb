class InventoryCompleteGrid {
    Module: string = 'InventoryCompleteGrid';
    apiurl: string = 'api/v1/inventorypackageinventory';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="DefaultQuantity"] input').val('1');
        });
    };
}

var InventoryCompleteGridController = new InventoryCompleteGrid();
//----------------------------------------------------------------------------------------------