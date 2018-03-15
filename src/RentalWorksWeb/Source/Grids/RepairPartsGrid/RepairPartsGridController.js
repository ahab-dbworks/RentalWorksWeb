var RepairPartsGrid = (function () {
    function RepairPartsGrid() {
        this.Module = 'RepairPartsGrid';
        this.apiurl = 'api/v1/repairpart';
    }
    RepairPartsGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="ICode"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return RepairPartsGrid;
}());
var RepairPartsGridController = new RepairPartsGrid();
//# sourceMappingURL=RepairPartsGridController.js.map