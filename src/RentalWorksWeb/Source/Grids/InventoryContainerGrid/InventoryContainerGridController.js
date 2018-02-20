var InventoryContainerGrid = (function () {
    function InventoryContainerGrid() {
        this.Module = 'InventoryContainerGrid';
        this.apiurl = 'api/v1/inventorycontaineritem';
    }
    InventoryContainerGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventoryContainerGrid;
}());
var InventoryContainerGridController = new InventoryContainerGrid();
//# sourceMappingURL=InventoryContainerGridController.js.map