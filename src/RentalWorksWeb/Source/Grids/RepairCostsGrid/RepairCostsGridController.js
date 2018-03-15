var RepairCostsGrid = (function () {
    function RepairCostsGrid() {
        this.Module = 'RepairCostsGrid';
        this.apiurl = 'api/v1/repaircost';
    }
    RepairCostsGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="ICode"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="RateId"] input').val($tr.find('.field[data-browsedatafield="RateId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return RepairCostsGrid;
}());
var RepairCostsGridController = new RepairCostsGrid();
//# sourceMappingURL=RepairCostsGridController.js.map