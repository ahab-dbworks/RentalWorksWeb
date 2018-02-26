var InventoryPrepGrid = (function () {
    function InventoryPrepGrid() {
        this.Module = 'InventoryPrepGrid';
        this.apiurl = 'api/v1/inventoryprep';
    }
    InventoryPrepGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="PrepRateId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="PrepDescription"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="PrepUnit"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventoryPrepGrid;
}());
var InventoryPrepGridController = new InventoryPrepGrid();
//# sourceMappingURL=InventoryPrepGridController.js.map