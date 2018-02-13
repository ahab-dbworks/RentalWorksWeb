var InventorySubstituteGrid = (function () {
    function InventorySubstituteGrid() {
        this.Module = 'InventorySubstituteGrid';
        this.apiurl = 'api/v1/inventorysubstitute';
    }
    InventorySubstituteGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventorySubstituteGrid;
}());
window.InventorySubstituteGridController = new InventorySubstituteGrid();
//# sourceMappingURL=InventorySubstituteGridController.js.map