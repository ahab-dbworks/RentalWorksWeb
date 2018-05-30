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
            var inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
            var officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
            var rateType = $form.find('[data-datafield="RateType"] input').val();
            var inventoryType = $generatedtr.find('[data-browsedatafield="InventoryId"]').attr('data-validationname');
            var discountPercent, daysPerWeek;
            daysPerWeek = FwFormField.getValueByDataField($form, 'RentalDaysPerWeek');
            switch (inventoryType) {
                case 'RentalInventoryValidation':
                    discountPercent = FwFormField.getValueByDataField($form, 'RentalDiscountPercent');
                    break;
                case 'SalesInventoryValidation':
                    discountPercent = FwFormField.getValueByDataField($form, 'SalesDiscountPercent');
                    break;
                case 'LaborRateValidation':
                    discountPercent = FwFormField.getValueByDataField($form, 'LaborDiscountPercent');
                    break;
                case 'MiscRateValidation':
                    discountPercent = FwFormField.getValueByDataField($form, 'MiscDiscountPercent');
                    break;
            }
            if ($generatedtr.hasClass("newmode")) {
                FwAppData.apiMethod(true, 'GET', "api/v1/pricing/" + inventoryId + "/" + warehouseId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    switch (rateType) {
                        case 'DAILY':
                            $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].DailyRate);
                            break;
                        case 'WEEKLY':
                            $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].WeeklyRate);
                            break;
                        case 'MONTHLY':
                            $generatedtr.find('[data-browsedatafield="Price"] input').val(response[0].MonthlyRate);
                            break;
                    }
                }, null, $form);
                FwAppData.apiMethod(true, 'GET', "api/v1/taxable/" + inventoryId + "/" + officeLocationId, null, FwServices.defaultTimeout, function onSuccess(response) {
                    if (response[0].Taxable) {
                        $generatedtr.find('.field[data-browsedatafield="Taxable"] input').prop('checked', 'true');
                    }
                }, null, $form);
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
                $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val(discountPercent);
                $generatedtr.find('.field[data-browsedatafield="DaysPerWeek"] input').val(daysPerWeek);
            }
        });
    };
    ;
    return OrderItemGrid;
}());
var OrderItemGridController = new OrderItemGrid();
//# sourceMappingURL=OrderItemGridController.js.map