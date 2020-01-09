class InventoryKitGrid {
    Module: string = 'InventoryPackageInventory';
    apiurl: string = 'api/v1/inventorypackageinventory';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="DefaultQuantity"] input').val('1');
        });
    };

    addLegend($control) {
        FwBrowse.addLegend($control, 'Complete', '#8888ff');
        FwBrowse.addLegend($control, 'Kit', '#56d64d');
        FwBrowse.addLegend($control, 'Percentage Item', '#FFA500');
    }
}

var InventoryKitGridController = new InventoryKitGrid();