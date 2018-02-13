var InventoryKitGrid = (function () {
    function InventoryKitGrid() {
        this.Module = 'InventoryKitGrid';
        this.apiurl = 'api/v1/inventorypackageinventory';
    }
    InventoryKitGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventoryKitGrid;
}());
window.InventoryKitGridController = new InventoryKitGrid();
//# sourceMappingURL=InventoryKitGridController.js.map