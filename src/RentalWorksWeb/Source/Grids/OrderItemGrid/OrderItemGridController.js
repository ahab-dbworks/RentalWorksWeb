var OrderItemGrid = (function () {
    function OrderItemGrid() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
    }
    OrderItemGrid.prototype.onRowNewMode = function ($control, $tr) {
        var $form = $control.closest('.fwform');
        var grid = $tr.parents('[data-grid="OrderItemGrid"]');
        if ($form[0].dataset.controller !== "TemplateController") {
            var toDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
            var fromDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
            var pickDate = FwFormField.getValueByDataField($form, 'PickDate');
        }
        ;
        if (grid.hasClass('R')) {
            $tr.find('.field[data-browsedatafield="RecType"] input').val("R");
        }
        else if (grid.hasClass('S')) {
            $tr.find('.field[data-browsedatafield="RecType"] input').val("S");
        }
        else if (grid.hasClass('M')) {
            $tr.find('.field[data-browsedatafield="RecType"] input').val("M");
        }
        else if (grid.hasClass('L')) {
            $tr.find('.field[data-browsedatafield="RecType"] input').val("L");
        }
        if ($form[0].dataset.controller !== "TemplateController") {
            $tr.find('.field[data-browsedatafield="FromDate"] input').val(fromDate);
            $tr.find('.field[data-browsedatafield="ToDate"] input').val(toDate);
            $tr.find('.field[data-browsedatafield="PickDate"] input').val(pickDate);
        }
    };
    OrderItemGrid.prototype.generateRow = function ($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
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
        $generatedtr.find('div[data-browsedatafield="DiscountPercentDisplay"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Extended', 'DiscountPercent');
        });
        $generatedtr.find('div[data-browsedatafield="UnitExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'UnitExtended');
        });
        $generatedtr.find('div[data-browsedatafield="WeeklyExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'WeeklyExtended');
        });
        $generatedtr.find('div[data-browsedatafield="MonthlyExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'MonthlyExtended');
        });
        $generatedtr.find('div[data-browsedatafield="PeriodExtended"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'PeriodExtended');
        });
        $generatedtr.find('div[data-browsedatafield="UnitDiscountAmount"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'UnitDiscountAmount');
        });
        $generatedtr.find('div[data-browsedatafield="WeeklyDiscountAmount"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'WeeklyDiscountAmount');
        });
        $generatedtr.find('div[data-browsedatafield="MonthlyDiscountAmount"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'MonthlyDiscountAmount');
        });
        $generatedtr.find('div[data-browsedatafield="PeriodDiscountAmount"]').on('change', 'input.value', function ($tr) {
            calculateExtended('Discount', 'PeriodDiscountAmount');
        });
        function calculateExtended(type, field) {
            var rateType, recType, fromDate, toDate, quantity, rate, daysPerWeek, discountPercent, weeklyExtended, unitExtended, periodExtended, monthlyExtended, unitDiscountAmount, weeklyDiscountAmount, monthlyDiscountAmount, periodDiscountAmount;
            rateType = $form.find('[data-datafield="RateType"] input').val();
            recType = $generatedtr.find('.field[data-browsedatafield="RecType"] input').val();
            fromDate = $generatedtr.find('.field[data-browsedatafield="FromDate"] input').val();
            toDate = $generatedtr.find('.field[data-browsedatafield="ToDate"] input').val();
            quantity = $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val();
            rate = $generatedtr.find('.field[data-browsedatafield="Price"] input').val();
            daysPerWeek = $generatedtr.find('.field[data-browsedatafield="DaysPerWeek"] input').val();
            if (field == "DiscountPercent") {
                discountPercent = $generatedtr.find('.field[data-browsedatafield="DiscountPercentDisplay"] input').val();
            }
            else {
                discountPercent = $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val();
            }
            weeklyExtended = $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val();
            unitExtended = $generatedtr.find('.field[data-browsedatafield="UnitExtended"] input').val();
            periodExtended = $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val();
            monthlyExtended = $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val();
            unitDiscountAmount = $generatedtr.find('.field[data-browsedatafield="UnitDiscountAmount"] input').val();
            weeklyDiscountAmount = $generatedtr.find('.field[data-browsedatafield="WeeklyDiscountAmount"] input').val();
            monthlyDiscountAmount = $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val();
            periodDiscountAmount = $generatedtr.find('.field[data-browsedatafield="PeriodDiscountAmount"] input').val();
            var apiurl = "api/v1/orderitem/";
            if (type == "Extended") {
                apiurl += "calculateextended?RateType=";
            }
            else if (type == "Discount") {
                apiurl += "calculatediscountpercent?RateType=";
            }
            apiurl +=
                rateType
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
            if (type == 'Extended') {
                apiurl += "&DiscountPercent=" + discountPercent;
                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val(response.DiscountPercent);
                    $generatedtr.find('.field[data-browsedatafield="UnitExtended"] input').val(response.UnitExtended);
                    $generatedtr.find('.field[data-browsedatafield="UnitDiscountAmount"] input').val(response.UnitDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val(response.WeeklyExtended);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyDiscountAmount"] input').val(response.WeeklyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val(response.MonthlyExtended);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val(response.MonthlyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val(response.PeriodExtended);
                    $generatedtr.find('.field[data-browsedatafield="PeriodDiscountAmount"] input').val(response.PeriodDiscountAmount);
                }, null, null);
            }
            if (type == 'Discount') {
                switch (field) {
                    case 'UnitExtended':
                        apiurl += "&UnitExtended=" + (+unitExtended.substring(1).replace(',', ''));
                        break;
                    case 'WeeklyExtended':
                        apiurl += "&WeeklyExtended=" + (+weeklyExtended.substring(1).replace(',', ''));
                        break;
                    case 'MonthlyExtended':
                        apiurl += "&MonthlyExtended=" + (+monthlyExtended.substring(1).replace(',', ''));
                        break;
                    case 'PeriodExtended':
                        apiurl += "&PeriodExtended=" + (+periodExtended.substring(1).replace(',', ''));
                        break;
                    case 'UnitDiscountAmount':
                        apiurl += "&UnitDiscountAmount=" + (+unitDiscountAmount.substring(1).replace(',', ''));
                        break;
                    case 'WeeklyDiscountAmount':
                        apiurl += "&WeeklyDiscountAmount=" + (+weeklyDiscountAmount.substring(1).replace(',', ''));
                        break;
                    case 'MonthlyDiscountAmount':
                        apiurl += "&MonthlyDiscountAmount=" + (+monthlyDiscountAmount.substring(1).replace(',', ''));
                        break;
                    case 'PeriodDiscountAmount':
                        apiurl += "&PeriodDiscountAmount=" + (+periodDiscountAmount.substring(1).replace(',', ''));
                        break;
                }
                FwAppData.apiMethod(true, 'GET', apiurl, null, FwServices.defaultTimeout, function onSuccess(response) {
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val(response.DiscountPercent);
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercentDisplay"] input').val(response.DiscountPercent);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyExtended"] input').val(response.WeeklyExtended);
                    $generatedtr.find('.field[data-browsedatafield="UnitExtended"] input').val(response.UnitExtended);
                    $generatedtr.find('.field[data-browsedatafield="PeriodExtended"] input').val(response.PeriodExtended);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyExtended"] input').val(response.MonthlyExtended);
                    $generatedtr.find('.field[data-browsedatafield="UnitDiscountAmount"] input').val(response.UnitDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="WeeklyDiscountAmount"] input').val(response.WeeklyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="MonthlyDiscountAmount"] input').val(response.MonthlyDiscountAmount);
                    $generatedtr.find('.field[data-browsedatafield="PeriodDiscountAmount"] input').val(response.PeriodDiscountAmount);
                }, null, null);
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