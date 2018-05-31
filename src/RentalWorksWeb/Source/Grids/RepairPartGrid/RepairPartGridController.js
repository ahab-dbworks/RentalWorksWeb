var RepairPartGrid = (function () {
    function RepairPartGrid() {
        this.Module = 'RepairPartGrid';
        this.apiurl = 'api/v1/repairpart';
    }
    RepairPartGrid.prototype.generateRow = function ($control, $generatedtr, $form) {
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse')).warehouse;
        var warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedisplayfield="Warehouse"] input.text').val(warehouse);
            $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.value').val(warehouseId);
            $generatedtr.find('.field[data-browsedatafield="Quantity"] input').val("1");
        });
    };
    ;
    return RepairPartGrid;
}());
var RepairPartGridController = new RepairPartGrid();
//# sourceMappingURL=RepairPartGridController.js.map