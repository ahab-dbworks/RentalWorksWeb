var InventoryCompatibilityGrid = (function () {
    function InventoryCompatibilityGrid() {
        this.Module = 'InventoryCompatibilityGrid';
        this.apiurl = 'api/v1/inventorycompatible';
    }
    InventoryCompatibilityGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="CompatibleWithInventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="CompatibleWithDescription"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventoryCompatibilityGrid;
}());
window.InventoryCompatibilityGridController = new InventoryCompatibilityGrid();
//# sourceMappingURL=InventoryCompatibilityGridController.js.map