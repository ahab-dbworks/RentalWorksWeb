var PartsInventorySubstituteGrid = (function () {
    function PartsInventorySubstituteGrid() {
        this.Module = 'PartsInventorySubstituteGrid';
        this.apiurl = 'api/v1/inventorysubstitute';
        this.generateRow = function ($control, $generatedtr) {
            $generatedtr.find('div[data-browsedatafield="SubstituteInventoryId"]').data('onchange', function ($tr) {
                $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            });
        };
    }
    return PartsInventorySubstituteGrid;
}());
var PartsInventorySubstituteGridController = new PartsInventorySubstituteGrid();
//# sourceMappingURL=PartsInventorySubstituteGridController.js.map