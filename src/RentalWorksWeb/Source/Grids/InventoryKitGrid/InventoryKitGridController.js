class InventoryKitGrid {
    constructor() {
        this.Module = 'InventoryKitGrid';
        this.apiurl = 'api/v1/inventorypackageinventory';
    }
    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    }
    ;
}
var InventoryKitGridController = new InventoryKitGrid();
//# sourceMappingURL=InventoryKitGridController.js.map