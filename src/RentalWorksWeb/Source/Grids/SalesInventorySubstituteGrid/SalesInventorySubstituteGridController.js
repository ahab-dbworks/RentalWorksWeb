var SalesInventorySubstituteGrid = /** @class */ (function () {
    function SalesInventorySubstituteGrid() {
        this.Module = 'SalesInventorySubstituteGrid';
        this.apiurl = 'api/v1/inventorysubstitute';
    }
    SalesInventorySubstituteGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="SubstituteInventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return SalesInventorySubstituteGrid;
}());
window.SalesInventorySubstituteGridController = new SalesInventorySubstituteGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=SalesInventorySubstituteGridController.js.map