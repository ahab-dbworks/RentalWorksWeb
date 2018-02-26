var InventoryGroupInvGrid = (function () {
    function InventoryGroupInvGrid() {
        this.Module = 'InventoryGroupInvGrid';
        this.apiurl = 'api/v1/inventorygroupinventory';
    }
    InventoryGroupInvGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Category"]').text($tr.find('.field[data-browsedatafield="Category"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Rank"]').text($tr.find('.field[data-browsedatafield="Rank"]').attr('data-originalvalue'));
        });
    };
    ;
    return InventoryGroupInvGrid;
}());
var InventoryGroupInvGridController = new InventoryGroupInvGrid();
//# sourceMappingURL=InventoryGroupInvGridController.js.map