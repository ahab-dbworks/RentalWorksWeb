var PartsInventoryCompatibilityGrid = (function () {
    function PartsInventoryCompatibilityGrid() {
        this.Module = 'PartsInventoryCompatibilityGrid';
        this.apiurl = 'api/v1/inventorycompatible';
        this.generateRow = function ($control, $generatedtr) {
            $generatedtr.find('div[data-browsedatafield="CompatibleWithInventoryId"]').data('onchange', function ($tr) {
                $generatedtr.find('.field[data-browsedatafield="CompatibleWithDescription"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            });
        };
    }
    return PartsInventoryCompatibilityGrid;
}());
var PartsInventoryCompatibilityGridController = new PartsInventoryCompatibilityGrid();
//# sourceMappingURL=PartsInventoryCompatibilityGridController.js.map