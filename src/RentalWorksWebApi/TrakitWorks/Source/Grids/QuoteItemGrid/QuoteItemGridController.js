class QuoteItemGrid {
    constructor() {
        this.Module = 'QuoteItemGrid';
        this.apiurl = 'api/v1/orderitem';
        this.beforeValidateItem = function ($browse, $grid, request, datafield, $tr) {
            var rate = $tr.find('div[data-browsedatafield="RecType"] input.value').val();
            if (rate !== null) {
                request.uniqueIds = { AvailFor: 'R' };
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
        let $grid = $tr.parents('[data-grid="QuoteItemGrid"]');
        var fromDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
        var fromTime = FwFormField.getValueByDataField($form, 'EstimatedStartTime');
        var toDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
        var toTime = FwFormField.getValueByDataField($form, 'EstimatedStopTime');
        FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'R' });
        FwBrowse.setFieldValue($grid, $tr, 'FromDate', { value: fromDate });
        FwBrowse.setFieldValue($grid, $tr, 'FromTime', { value: fromTime });
        FwBrowse.setFieldValue($grid, $tr, 'ToDate', { value: toDate });
        FwBrowse.setFieldValue($grid, $tr, 'ToTime', { value: toTime });
    }
    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            if ($tr.find('.order-item-bold').text() === 'true') {
                $tr.css('font-weight', "bold");
            }
        });
        FwBrowse.setAfterRenderFieldCallback($control, ($tr, $td, $field, dt, rowIndex, colIndex) => {
            if ($tr.find('.order-item-lock').text() === 'true') {
                $tr.find('.field-to-lock').css('background-color', "#f5f5f5");
                $tr.find('.field-to-lock').attr('data-formreadonly', 'true');
                if ($control.attr('data-enabled') === 'false') {
                    $tr.find('.field-to-lock').css('background-color', 'transparent');
                }
            }
        });
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            var warehouse = FwFormField.getTextByDataField($form, 'WarehouseId');
            var warehouseId = FwFormField.getValueByDataField($form, 'WarehouseId');
            let warehouseCode = $form.find('[data-datafield="WarehouseCode"] input').val();
            let inventoryId = $generatedtr.find('div[data-browsedatafield="InventoryId"] input').val();
            let officeLocationId = FwFormField.getValueByDataField($form, 'OfficeLocationId');
            let rateType = $form.find('[data-datafield="RateType"] input').val();
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
            $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
        });
    }
    toggleOrderItemView($form, event, module) {
        let $element, recType, isSummary, isSubGrid;
        $element = jQuery(event.currentTarget);
        recType = $element.closest('[data-grid="OrderItemGrid"]').attr('class');
        isSubGrid = $element.closest('[data-grid="OrderItemGrid"]').attr('data-issubgrid');
        let quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        if (recType === 'A') {
            recType = undefined;
        }
        let $quoteItemGrid = $element.closest('[data-name="QuoteItemGrid"]');
        if ($quoteItemGrid.data('isSummary') === false) {
            isSummary = true;
            $quoteItemGrid.data('isSummary', true);
            $element.children().text('Detail View');
        }
        else {
            isSummary = false;
            $quoteItemGrid.data('isSummary', false);
            $element.children().text('Summary View');
        }
        $quoteItemGrid.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: quoteId,
                Summary: isSummary,
                RecType: recType
            };
            request.orderby = "RowNumber,RecTypeDisplay";
            if (isSubGrid === "true") {
                request.uniqueids.Subs = true;
            }
        });
        $quoteItemGrid.data('beforesave', request => {
            request.OrderId = quoteId;
            request.RecType = recType;
            request.Summary = isSummary;
        });
        FwBrowse.search($quoteItemGrid);
    }
    ;
    orderItemGridBoldUnbold($browse, event) {
        let orderId, $selectedCheckBoxes, boldItems = [];
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');
        if ($selectedCheckBoxes.length > 0) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                let $row = $selectedCheckBoxes.eq(i).closest('tr');
                let item = {
                    OrderItemId: $row.find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue'),
                    OrderId: $row.find('[data-formdatafield="OrderId"]').attr('data-originalvalue'),
                    Description: $row.find('[data-formdatafield="Description"]').attr('data-originalvalue'),
                    QuantityOrdered: $row.find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue'),
                    RecType: $row.find('[data-formdatafield="RecType"]').attr('data-originalvalue'),
                    Bold: ($row.find('[data-formdatafield="Bold"]').attr('data-originalvalue') === 'true') ? false : true
                };
                boldItems.push(item);
            }
            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, boldItems, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        }
    }
    ;
}
FwApplicationTree.clickEvents[Constants.Grids.QuoteItemGrid.menuItems.SummaryView.id] = function (event) {
    let $form = jQuery(this).closest('.fwform');
    let module = $form.attr('data-controller').replace('Controller', '');
    try {
        QuoteItemGridController.toggleOrderItemView($form, event, module);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents[Constants.Grids.QuoteItemGrid.menuItems.RefreshAvailability.id] = function (e) {
    const $quoteItemGrid = jQuery(this).closest('[data-name="QuoteItemGrid"]');
    const pageNumber = $quoteItemGrid.attr('data-pageno');
    const onDataBind = $quoteItemGrid.data('ondatabind');
    if (typeof onDataBind == 'function') {
        $quoteItemGrid.data('ondatabind', function (request) {
            onDataBind(request);
            request.uniqueids.RefreshAvailability = true;
            request.pageno = parseInt(pageNumber);
        });
    }
    FwBrowse.search($quoteItemGrid);
    $quoteItemGrid.attr('data-pageno', pageNumber);
    $quoteItemGrid.data('ondatabind', onDataBind);
};
FwApplicationTree.clickEvents[Constants.Grids.QuoteItemGrid.menuItems.BoldUnBoldSelected.id] = function (event) {
    const $browse = jQuery(this).closest('.fwbrowse');
    try {
        QuoteItemGridController.orderItemGridBoldUnbold($browse, event);
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
var QuoteItemGridController = new QuoteItemGrid();
//# sourceMappingURL=QuoteItemGridController.js.map