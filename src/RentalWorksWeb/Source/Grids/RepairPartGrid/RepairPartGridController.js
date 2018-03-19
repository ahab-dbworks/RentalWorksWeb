var RepairPartGrid = (function () {
    function RepairPartGrid() {
        this.Module = 'RepairPartGrid';
        this.apiurl = 'api/v1/repairpart';
    }
    RepairPartGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="ICode"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return RepairPartGrid;
}());
var RepairPartGridController = new RepairPartGrid();
//# sourceMappingURL=RepairPartGridController.js.map