class InventorySubstituteGrid {
    Module: string = 'InventorySubstituteGrid';
    apiurl: string = 'api/v1/inventorysubstitute';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

var InventorySubstituteGridController = new InventorySubstituteGrid();
//----------------------------------------------------------------------------------------------