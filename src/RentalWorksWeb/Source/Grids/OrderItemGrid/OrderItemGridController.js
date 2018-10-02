class OrderItemGrid {
    constructor() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
        this.beforeValidateItem = function ($browse, $grid, request, datafield, $tr) {
            var rate = $tr.find('div[data-browsedatafield="RecType"] input.value').val();
            if (rate !== null) {
                switch (rate) {
                    case 'R':
                        request.uniqueIds = {
                            AvailFor: 'R'
                        };
                        break;
                    case 'S':
                        request.uniqueIds = {
                            AvailFor: 'S'
                        };
                        break;
                    case 'M':
                        request.uniqueIds = {
                            AvailFor: 'M'
                        };
                        break;
                    case 'L':
                        request.uniqueIds = {
                            AvailFor: 'L'
                        };
                        break;
                }
            }
        };
        this.beforeValidateBarcode = function ($browse, $grid, request, datafield, $tr) {
            let inventoryId = $tr.find('.field[data-browsedatafield="InventoryId"] input').val();
            if (inventoryId != '') {
                request.uniqueIds = {
                    InventoryId: inventoryId
                };
            }
        };
    }
    onRowNewMode($control, $tr) {
        let $form = $control.closest('.fwform');
        let $grid = $tr.parents('[data-grid="OrderItemGrid"]');
        let inventoryType;
        if ($form[0].dataset.controller !== "TemplateController" && $form[0].dataset.controller !== "PurchaseOrderController") {
            var pickDate = FwFormField.getValueByDataField($form, 'PickDate');
            var pickTime = FwFormField.getValueByDataField($form, 'PickTime');
            var fromDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
            var fromTime = FwFormField.getValueByDataField($form, 'EstimatedStartTime');
            var toDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
            var toTime = FwFormField.getValueByDataField($form, 'EstimatedStopTime');
        }
        ;
        if ($grid.hasClass('R')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'R' });
            inventoryType = 'Rental';
        }
        else if ($grid.hasClass('S')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'S' });
            inventoryType = 'Sales';
        }
        else if ($grid.hasClass('M')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'M' });
            inventoryType = 'Misc';
        }
        else if ($grid.hasClass('L')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'L' });
            inventoryType = 'Labor';
        }
        else if ($grid.hasClass('RS')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'RS' });
            inventoryType = 'RentalSales';
        }
        else if ($grid.hasClass('A')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'A' });
            inventoryType = 'Combined';
        }
        let discountPercent = FwFormField.getValueByDataField($form, `${inventoryType}DiscountPercent`);
        let daysPerWeek = FwFormField.getValueByDataField($form, `${inventoryType}DaysPerWeek`);
        if ($form[0].dataset.controller !== "TemplateController" && $form[0].dataset.controller !== "PurchaseOrderController") {
            FwBrowse.setFieldValue($grid, $tr, 'PickDate', { value: pickDate });
            FwBrowse.setFieldValue($grid, $tr, 'PickTime', { value: pickTime });
            FwBrowse.setFieldValue($grid, $tr, 'FromDate', { value: fromDate });
            FwBrowse.setFieldValue($grid, $tr, 'FromTime', { value: fromTime });
            FwBrowse.setFieldValue($grid, $tr, 'ToDate', { value: toDate });
            FwBrowse.setFieldValue($grid, $tr, 'ToTime', { value: toTime });
            FwBrowse.setFieldValue($grid, $tr, 'DiscountPercent', { value: discountPercent });
            FwBrowse.setFieldValue($grid, $tr, 'DiscountPercentDisplay', { value: discountPercent });
            FwBrowse.setFieldValue($grid, $tr, 'DaysPerWeek', { value: daysPerWeek });
        }
    }
    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        if ($form.attr('data-controller') === 'OrderController' || $form.attr('data-controller') === 'QuoteController' || $form.attr('data-controller') === 'PurchaseOrderController') {
            $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
                var warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
                var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                let warehouseCode = $form.find('[data-datafield="WarehouseCode"] input').val();
                let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
                let officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
                let rateType = $form.find('[data-datafield="RateType"] input').val();
                let inventoryType = $generatedtr.find('[data-browsedatafield="InventoryId"]').attr('data-validationname');
                let discountPercent, daysPerWeek;
            });
            FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
                if ($tr.find('.order-item-bold').text() === 'true') {
                    $tr.css('font-weight', "bold");
                }
            });
            FwBrowse.setAfterRenderFieldCallback($control, ($tr, $td, $field, dt, rowIndex, colIndex) => {
                if ($tr.find('.order-item-lock').text() === 'true') {
                    $tr.find('.field-to-lock').css('background-color', "#f5f5f5");
                    $tr.find('.field-to-lock').attr('data-formreadonly', 'true');
                }
            });
            $generatedtr.find('div[data-browsedatafield="ItemId"]').data('onchange', function ($tr) {
                $generatedtr.find('.field[data-browsedatafield="InventoryId"] input').val($tr.find('.field[data-browsedatafield="InventoryId"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="InventoryId"] input.text').val($tr.find('.field[data-browsedatafield="ICode"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
            });
            $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
                var warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
                var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                let warehouseCode = $form.find('[data-datafield="WarehouseCode"] input').val();
                let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
                let officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
                let rateType = $form.find('[data-datafield="RateType"] input').val();
                let inventoryType = $generatedtr.find('[data-browsedatafield="InventoryId"]').attr('data-validationname');
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                let discountPercent, daysPerWeek;
                $generatedtr.find('.field[data-browsedatafield="ItemId"] input').val('');
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
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
                    $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                    $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
                    $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                    $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                    $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                    $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercent"] input').val(discountPercent);
                    $generatedtr.find('.field[data-browsedatafield="DiscountPercentDisplay"] input').val(discountPercent);
                    $generatedtr.find('.field[data-browsedatafield="DaysPerWeek"] input').val(daysPerWeek);
                    if ($form.attr('data-controller') === 'PurchaseOrderController') {
                        $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouse);
                    }
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
        }
        if ($form.attr('data-controller') === 'TemplateController') {
            $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
                var warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
                var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
                let warehouseCode = $form.find('[data-datafield="WarehouseCode"] input').val();
                let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
                let rateType = $form.find('[data-datafield="RateType"] input').val();
                let inventoryType = $generatedtr.find('[data-browsedatafield="InventoryId"]').attr('data-validationname');
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                $generatedtr.find('.field[data-browsedatafield="SubQuantity"] input').val("0");
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);
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
                }
            });
        }
        function calculateExtended(type, field) {
            let rateType, recType, fromDate, toDate, quantity, rate, daysPerWeek, discountPercent, weeklyExtended, unitExtended, periodExtended, monthlyExtended, unitDiscountAmount, weeklyDiscountAmount, monthlyDiscountAmount, periodDiscountAmount;
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
            let apiurl = "api/v1/orderitem/";
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
    }
    ;
    toggleOrderItemView($form, event, module) {
        let $element, $orderItemGrid, recType, isSummary, orderId, isSubGrid;
        let request = {};
        $element = jQuery(event.currentTarget);
        recType = $element.closest('[data-grid="OrderItemGrid"]').attr('class');
        isSubGrid = $element.closest('[data-grid="OrderItemGrid"]').attr('data-issubgrid');
        orderId = FwFormField.getValueByDataField($form, `${module}Id`);
        $orderItemGrid = $element.closest('[data-name="OrderItemGrid"]');
        if ($orderItemGrid.data('isSummary') === false) {
            isSummary = true;
            $orderItemGrid.data('isSummary', true);
            $element.children().text('Detail View');
        }
        else {
            isSummary = false;
            $orderItemGrid.data('isSummary', false);
            $element.children().text('Summary View');
        }
        $orderItemGrid.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: orderId,
                Summary: isSummary,
                RecType: recType
            };
            request.pagesize = 9999;
            request.orderby = "RowNumber,RecTypeDisplay";
            if (isSubGrid === "true") {
                request.uniqueids.Subs = true;
            }
        });
        $orderItemGrid.data('beforesave', request => {
            request.OrderId = orderId;
            request.RecType = recType;
            request.Summary = isSummary;
        });
        FwBrowse.search($orderItemGrid);
    }
    ;
}
FwApplicationTree.clickEvents['{77E511EC-5463-43A0-9C5D-B54407C97B15}'] = function (e) {
    let grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');
    let search, $form, orderId, quoteId, purchaseOrderId, templateId, $popup;
    $form = jQuery(this).closest('.fwform');
    let gridInventoryType;
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
    if (grid.hasClass('P')) {
        gridInventoryType = 'Parts';
    }
    search = new SearchInterface();
    let controllerName = $form.attr('data-controller');
    switch (controllerName) {
        case 'OrderController':
            orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (orderId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                $popup = search.renderSearchPopup($form, orderId, 'Order', gridInventoryType);
            }
            break;
        case 'QuoteController':
            quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
            if (quoteId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                $popup = search.renderSearchPopup($form, quoteId, 'Quote', gridInventoryType);
            }
            break;
        case 'PurchaseOrderController':
            purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            if (purchaseOrderId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                $popup = search.renderSearchPopup($form, purchaseOrderId, 'PurchaseOrder', gridInventoryType);
            }
            break;
        case 'TemplateController':
            templateId = FwFormField.getValueByDataField($form, 'TemplateId');
            if (templateId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                $popup = search.renderSearchPopup($form, templateId, 'Template', gridInventoryType);
            }
            break;
    }
};
FwApplicationTree.clickEvents['{007C4F21-7526-437C-AD1C-4BBB1030AABA}'] = function (e) {
    var $form, $subWorksheetForm, subWorksheetData = {};
    let $grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');
    if ($grid.hasClass('A')) {
        subWorksheetData.RecType = '';
    }
    else if ($grid.hasClass('R')) {
        subWorksheetData.RecType = 'R';
    }
    else if ($grid.hasClass('S')) {
        subWorksheetData.RecType = 'S';
    }
    else if ($grid.hasClass('M')) {
        subWorksheetData.RecType = 'M';
    }
    else if ($grid.hasClass('L')) {
        subWorksheetData.RecType = 'L';
    }
    else if ($grid.hasClass('RS')) {
        subWorksheetData.RecType = 'RS';
    }
    try {
        $form = jQuery(this).closest('.fwform');
        subWorksheetData.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
        subWorksheetData.EstimatedStartDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
        subWorksheetData.EstimatedStopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
        subWorksheetData.EstimatedStartTime = FwFormField.getValueByDataField($form, 'EstimatedStartTime');
        $subWorksheetForm = SubWorksheetController.openForm('EDIT', subWorksheetData);
        FwModule.openSubModuleTab($form, $subWorksheetForm);
        jQuery('.tab.submodule.active').find('.caption').html('Sub Worksheet');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{D27AD4E7-E924-47D1-AF6E-992B92F5A647}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');
    let module = $form.attr('data-controller').replace('Controller', '');
    try {
        OrderItemGridController.toggleOrderItemView($form, event, module);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
var OrderItemGridController = new OrderItemGrid();
//# sourceMappingURL=OrderItemGridController.js.map