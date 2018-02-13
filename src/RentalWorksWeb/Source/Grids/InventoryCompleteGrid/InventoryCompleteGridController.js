var InventoryCompleteGrid = (function () {
    function InventoryCompleteGrid() {
        this.Module = 'InventoryCompleteGrid';
        this.apiurl = 'api/v1/inventorypackageinventory';
    }
    InventoryCompleteGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventoryCompleteGrid;
}());
window.InventoryCompleteGridController = new InventoryCompleteGrid();
//# sourceMappingURL=InventoryCompleteGridController.js.map