class SalesInventorySubstituteGrid {
    Module: string = 'SalesInventorySubstituteGrid';
    apiurl: string = 'api/v1/inventorysubstitute';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="SubstituteInventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

var SalesInventorySubstituteGridController = new SalesInventorySubstituteGrid();
//----------------------------------------------------------------------------------------------