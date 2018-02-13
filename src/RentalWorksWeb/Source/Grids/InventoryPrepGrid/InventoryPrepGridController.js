var InventoryPrepGrid = /** @class */ (function () {
    function InventoryPrepGrid() {
        this.Module = 'InventoryPrepGrid';
        this.apiurl = 'api/v1/inventoryprep';
    }
    InventoryPrepGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="PrepRateId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="PrepDescription"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventoryPrepGrid;
}());
window.InventoryPrepGridController = new InventoryPrepGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=InventoryPrepGridController.js.map