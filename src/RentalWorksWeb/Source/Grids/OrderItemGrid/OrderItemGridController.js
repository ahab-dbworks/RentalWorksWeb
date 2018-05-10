var OrderItemGrid = (function () {
    function OrderItemGrid() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
    }
    OrderItemGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            var $form = $control.closest('.fwform');
            if ($form[0].dataset.controller !== "TemplateController") {
                var toDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
                var fromDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
            }
            var warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
            var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            var warehouseCode = $form.find('[data-datafield="WarehouseCode"] input').val();
            if ($generatedtr.hasClass("newmode")) {
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                if ($form[0].dataset.controller !== "TemplateController") {
                    $generatedtr.find('.field[data-browsedatafield="FromDate"] input').val(fromDate);
                    $generatedtr.find('.field[data-browsedatafield="ToDate"] input').val(toDate);
                }
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);
            }
        });
    };
    ;
    return OrderItemGrid;
}());
var OrderItemGridController = new OrderItemGrid();
//# sourceMappingURL=OrderItemGridController.js.map