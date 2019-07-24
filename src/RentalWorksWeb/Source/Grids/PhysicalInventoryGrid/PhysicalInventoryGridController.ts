class PhysicalInventoryGrid {
    Module: string = 'PhysicalInventoryGrid';
    apiurl: string = 'api/v1/physicalinventoryinventory';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="AvailableFor"]').text($tr.find('.field[data-browsedatafield="AvailFor"]').attr('data-originalvalue'));
        });
    }
}

var PhysicalInventoryGridController = new PhysicalInventoryGrid();
//----------------------------------------------------------------------------------------------