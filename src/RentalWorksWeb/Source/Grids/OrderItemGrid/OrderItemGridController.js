var OrderItemGrid = (function () {
    function OrderItemGrid() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
    }
    OrderItemGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return OrderItemGrid;
}());
var OrderItemGridController = new OrderItemGrid();
//# sourceMappingURL=OrderItemGridController.js.map