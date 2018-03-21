var RepairCostGrid = (function () {
    function RepairCostGrid() {
        this.Module = 'RepairCostGrid';
        this.apiurl = 'api/v1/repaircost';
    }
    RepairCostGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="RateId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Unit"] input').val($tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
        });
    };
    ;
    return RepairCostGrid;
}());
var RepairCostGridController = new RepairCostGrid();
//# sourceMappingURL=RepairCostGridController.js.map