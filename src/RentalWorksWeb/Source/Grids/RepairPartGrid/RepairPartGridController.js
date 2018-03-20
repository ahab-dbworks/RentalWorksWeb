var RepairPartGrid = (function () {
    function RepairPartGrid() {
        this.Module = 'RepairPartGrid';
        this.apiurl = 'api/v1/repairpart';
    }
    RepairPartGrid.prototype.generateRow = function ($control, $generatedtr, $form) {
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse')).warehouse;
        var warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        $generatedtr.find('div[data-browsedatafield="ICode"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="WarehouseCode"] input').val(warehouse);
            $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
        });
        console.log(warehouse, "warehouse");
        console.log(warehouseId, "warehouseID");
    };
    ;
    return RepairPartGrid;
}());
var RepairPartGridController = new RepairPartGrid();
//# sourceMappingURL=RepairPartGridController.js.map