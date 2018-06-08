var OrderItemGrid = (function () {
    function OrderItemGrid() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
    }
    OrderItemGrid.prototype.generateRow = function ($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
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
        $generatedtr.find('div[data-browsedatafield="FromDate"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="ToDate"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="QuantityOrdered"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="Price"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="DaysPerWeek"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="DiscountPercent"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended');
        });
        $generatedtr.find('div[data-browsedatafield="UnitExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount');
        });
        $generatedtr.find('div[data-browsedatafield="WeeklyExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount');
        });
        $generatedtr.find('div[data-browsedatafield="MonthlyExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount');
        });
        $generatedtr.find('div[data-browsedatafield="PeriodExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount');
        });
        function calculateExtended(calculatedColumn) {
            var rateType, recType, fromDate, toDate, quantity, rate, daysPerWeek, discountPercent, weeklyExtended;
            rateType = $form.find('[data-datafield="RateType"] input').val();
            recType = $generatedtr.find('.field[data-browsedatafield="RecType"] input').val();
            fromDate = $generatedtr.find('.field[data-browsedatafield="FromDate"] input').val();
            toDate = $generatedtr.find('.field[data-browsedatafield="ToDate"] input').val();
            quantity = $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val();
            rate = $generatedtr.find('.field[data-browsedatafield="Price"] input').val();
            daysPerWeek = $generatedtr.find('.field[data-browsedatafield="DaysPerWeek"] input').val();
            discountPercent = $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val();
            weeklyExtended = $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val();
            var apiurl = "api/v1/orderitem/calculateextended?RateType="
                + rateType
                + "&RecType="
                + recType
                + "&FromDate="
                + fromDate
                + "&ToDate="
                + toDate
                + "&Quantity="
                + quantity
                + "&Rate="
                + (+(rate.substring(1).replace(',', '')))
                + "&DaysPerWeek="
                + daysPerWeek;
            if (calculatedColumn == 'Extended') {
                apiurl += "&DiscountPercent=" + discountPercent;
                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val(response.WeeklyExtended);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyDiscountAmount"] input').val(response.WeeklyDiscount);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val(response.MonthlyExtended);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val(response.MonthlyDiscount);
                    $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val(response.PeriodExtended);
                    $generatedtr.find('.field[data-browsedatafield="PeriodDiscountAmount"] input').val(response.PeriodDiscount);
                }, null, null);
            }
            if (calculatedColumn == 'Discount') {
                apiurl += "&WeeklyExtended=" + (+weeklyExtended.substring(1).replace(',', ''));
            }
        }
    };
    ;
    return OrderItemGrid;
}());
FwApplicationTree.clickEvents['{77E511EC-5463-43A0-9C5D-B54407C97B15}'] = function (e) {
    var grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');
    var search, $form, orderId, quoteId, $popup;
    $form = jQuery(this).closest('.fwform');
    var gridInventoryType;
    if (grid.hasClass('R')) {
        gridInventoryType = 'Rental';
    }
    if (grid.hasClass('S')) {
        gridInventoryType = 'Sales';
    }
    if (grid.hasClass('L')) {
        gridInventoryType = 'Labor';
    }
    if (grid.hasClass('M')) {
        gridInventoryType = 'Misc';
    }
    search = new SearchInterface();
    if ($form.attr('data-controller') === 'OrderController') {
        orderId = FwFormField.getValueByDataField($form, 'OrderId');
        if (orderId == '') {
            FwNotification.renderNotification('WARNING', 'Please save the record before performing this function');
        }
        else {
            $popup = search.renderSearchPopup($form, orderId, 'Order', gridInventoryType);
        }
    }
    else {
        quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        if (quoteId == '') {
            FwNotification.renderNotification('WARNING', 'Please save the record before performing this function');
        }
        else {
            $popup = search.renderSearchPopup($form, quoteId, 'Quote', gridInventoryType);
        }
    }
    ;
};
var OrderItemGridController = new OrderItemGrid();
//# sourceMappingURL=OrderItemGridController.js.map