﻿class QuoteItemGrid {
    Module: string = 'QuoteItemGrid';
    apiurl: string = 'api/v1/orderitem';
    //----------------------------------------------------------------------------------------------
    onRowNewMode($control: JQuery, $tr: JQuery) {
        let $form    = $control.closest('.fwform');
        let $grid    = $tr.parents('[data-grid="QuoteItemGrid"]');
        var fromDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
        var fromTime = FwFormField.getValueByDataField($form, 'EstimatedStartTime');
        var toDate   = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
        var toTime   = FwFormField.getValueByDataField($form, 'EstimatedStopTime');

        FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'R' });
        FwBrowse.setFieldValue($grid, $tr, 'FromDate', { value: fromDate });
        FwBrowse.setFieldValue($grid, $tr, 'FromTime', { value: fromTime });
        FwBrowse.setFieldValue($grid, $tr, 'ToDate', { value: toDate });
        FwBrowse.setFieldValue($grid, $tr, 'ToTime', { value: toTime });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateItem = function ($browse, $grid, request, datafield, $tr) {
        var rate = $tr.find('div[data-browsedatafield="RecType"] input.value').val();
        if (rate !== null) {
            request.uniqueIds = { AvailFor: 'R' };
        }
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateBarcode = function ($browse, $grid, request, datafield, $tr) {
        let inventoryId = $tr.find('.field[data-browsedatafield="InventoryId"] input').val();
        if (inventoryId != '') {
            request.uniqueIds = {
                InventoryId: inventoryId
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');

        // Bold Row
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            if ($tr.find('.order-item-bold').text() === 'true') {
                $tr.css('font-weight', "bold");
            }
        });

        // Lock Fields
        FwBrowse.setAfterRenderFieldCallback($control, ($tr: JQuery, $td: JQuery, $field: JQuery, dt: FwJsonDataTable, rowIndex: number, colIndex: number) => {
            if ($tr.find('.order-item-lock').text() === 'true') {
                $tr.find('.field-to-lock').css('background-color', "#f5f5f5");
                $tr.find('.field-to-lock').attr('data-formreadonly', 'true');
                // disabled grids were rendering with different shade background color
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
    //----------------------------------------------------------------------------------------------
    toggleOrderItemView($form: any, event: any, module) {
        // Toggle between Detail and Summary view in all OrderItemGrid
        let $element, recType, isSummary, isSubGrid;
        $element = jQuery(event.currentTarget);
        //recType = $element.parentsUntil('.flexrow').eq(9).attr('class');
        recType = $element.closest('[data-grid="OrderItemGrid"]').attr('class');
        isSubGrid = $element.closest('[data-grid="OrderItemGrid"]').attr('data-issubgrid');
        let quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
        //const totalFields = ['WeeklyExtendedNoDiscount', 'WeeklyDiscountAmount', 'WeeklyExtended', 'WeeklyTax', 'WeeklyTotal', 'MonthlyExtendedNoDiscount', 'MonthlyDiscountAmount', 'MonthlyExtended', 'MonthlyTax', 'MonthlyTotal', 'PeriodExtendedNoDiscount', 'PeriodDiscountAmount', 'PeriodExtended', 'PeriodTax', 'PeriodTotal',]
        //combined grid logic -- rec type should be removed from request and not A.
        if (recType === 'A') {
            recType = undefined;
        }

        let $quoteItemGrid = $element.closest('[data-name="QuoteItemGrid"]');

        if ($quoteItemGrid.data('isSummary') === false) {
            isSummary = true;
            $quoteItemGrid.data('isSummary', true);
            $element.children().text('Detail View')
        } else {
            isSummary = false;
            $quoteItemGrid.data('isSummary', false);
            $element.children().text('Summary View')
        }

        $quoteItemGrid.data('ondatabind', request => {
            request.uniqueids = {
                OrderId: quoteId,
                Summary: isSummary,
                RecType: recType
            }
            request.orderby = "RowNumber,RecTypeDisplay"
            //request.totalfields = totalFields;
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
    };
    //----------------------------------------------------------------------------------------------
    orderItemGridBoldUnbold($browse: any, event: any): void {
        let orderId, $selectedCheckBoxes, boldItems = [];
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');

        if ($selectedCheckBoxes.length > 0) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                let $row = $selectedCheckBoxes.eq(i).closest('tr');
                let item: any = {
                    OrderItemId:     $row.find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue'),
                    OrderId:         $row.find('[data-formdatafield="OrderId"]').attr('data-originalvalue'),
                    Description:     $row.find('[data-formdatafield="Description"]').attr('data-originalvalue'),
                    QuantityOrdered: $row.find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue'),
                    RecType:         $row.find('[data-formdatafield="RecType"]').attr('data-originalvalue'),
                    Bold:            ($row.find('[data-formdatafield="Bold"]').attr('data-originalvalue') === 'true') ? false : true
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
    };
}
//----------------------------------------------------------------------------------------------
//Search Interface
//FwApplicationTree.clickEvents[Constants.Grids.QuoteItemGrid.menuItems.Search.id] = function (e) {
//    let grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');
//    let search, $form, orderId, quoteId, purchaseOrderId, templateId;

//    $form = jQuery(this).closest('.fwform');
//    let controllerName = $form.attr('data-controller');
//    search = new SearchInterface();

//    let gridInventoryType;
//    if (grid.hasClass('R')) {
//        gridInventoryType = 'Rental';
//    } else if (grid.hasClass('S')) {
//        gridInventoryType = 'Sales';
//    } else if (grid.hasClass('L')) {
//        gridInventoryType = 'Labor';
//    } else if (grid.hasClass('M')) {
//        gridInventoryType = 'Misc';
//    } else if (grid.hasClass('P')) {
//        gridInventoryType = 'Parts';
//    }

//    if ($form.attr('data-mode') === 'NEW') {
//        let isValid = FwModule.validateForm($form);
//        if (isValid) {
//            let activeTabId = jQuery($form.find('[data-type="tab"].active')).attr('id');
//            if (controllerName === "OrderController") {
//                OrderController.saveForm($form, { closetab: false });
//            } else if (controllerName === "QuoteController") {
//                QuoteController.saveForm($form, { closetab: false });
//            }
//            $form.attr('data-opensearch', 'true');
//            $form.attr('data-searchtype', gridInventoryType);
//            $form.attr('data-activetabid', activeTabId);
//        }
//        return;
//    }

//    switch (controllerName) {
//        case 'OrderController':
//            orderId = FwFormField.getValueByDataField($form, 'OrderId');
//            if (orderId == '') {
//                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//            } else {
//                search.renderSearchPopup($form, orderId, 'Order', gridInventoryType);
//            }
//            break;
//        case 'QuoteController':
//            quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
//            if (quoteId == '') {
//                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//            } else {
//                search.renderSearchPopup($form, quoteId, 'Quote', gridInventoryType);
//            }
//            break;
//        case 'PurchaseOrderController':
//            purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
//            if (purchaseOrderId == '') {
//                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//            } else {
//                search.renderSearchPopup($form, purchaseOrderId, 'PurchaseOrder', gridInventoryType);
//            }
//            break;
//        case 'TemplateController':
//            templateId = FwFormField.getValueByDataField($form, 'TemplateId');
//            if (templateId == '') {
//                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
//            } else {
//                search.renderSearchPopup($form, templateId, 'Template', gridInventoryType);
//            }
//            break;
//    }
//};
//----------------------------------------------------------------------------------------------
//Sub worksheet
//FwApplicationTree.clickEvents[Constants.Grids.QuoteItemGrid.menuItems.SubWorksheet.id] = function (e) {
//    var $form, $subWorksheetForm, subWorksheetData: any = {};
//    let $grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');

//    if ($grid.hasClass('A')) {
//        subWorksheetData.RecType = ''
//    } else if ($grid.hasClass('R')) {
//        subWorksheetData.RecType = 'R'
//    } else if ($grid.hasClass('S')) {
//        subWorksheetData.RecType = 'S'
//    } else if ($grid.hasClass('M')) {
//        subWorksheetData.RecType = 'M'
//    } else if ($grid.hasClass('L')) {
//        subWorksheetData.RecType = 'L'
//    } else if ($grid.hasClass('RS')) {
//        subWorksheetData.RecType = 'RS'
//    }

//    try {
//        $form = jQuery(this).closest('.fwform');
//        subWorksheetData.OrderId = FwFormField.getValueByDataField($form, 'OrderId');
//        subWorksheetData.EstimatedStartDate = FwFormField.getValueByDataField($form, 'EstimatedStartDate');
//        subWorksheetData.EstimatedStopDate = FwFormField.getValueByDataField($form, 'EstimatedStopDate');
//        subWorksheetData.EstimatedStartTime = FwFormField.getValueByDataField($form, 'EstimatedStartTime');
//        $subWorksheetForm = SubWorksheetController.openForm('EDIT', subWorksheetData);
//        FwModule.openSubModuleTab($form, $subWorksheetForm);
//        jQuery('.tab.submodule.active').find('.caption').html('Sub Worksheet');
//    } catch (ex) {
//        FwFunc.showError(ex);
//    }
//};
//----------------------------------------------------------------------------------------------
// Toggle between Detail and Summary view
FwApplicationTree.clickEvents[Constants.Grids.QuoteItemGrid.menuItems.SummaryView.id] = function (event) {
    let $form = jQuery(this).closest('.fwform');

    let module = $form.attr('data-controller').replace('Controller', '');
    try {
        QuoteItemGridController.toggleOrderItemView($form, event, module);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
//Refresh Availability
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
    //resets ondatabind
    $quoteItemGrid.data('ondatabind', onDataBind);
}
//---------------------------------------------------------------------------------
// Bold Selected
FwApplicationTree.clickEvents[Constants.Grids.QuoteItemGrid.menuItems.BoldUnBoldSelected.id] = function (event) {
    const $browse = jQuery(this).closest('.fwbrowse');

    try {
        QuoteItemGridController.orderItemGridBoldUnbold($browse, event);
    } catch (ex) {
        FwFunc.showError(ex);
    }
};
//---------------------------------------------------------------------------------
var QuoteItemGridController = new QuoteItemGrid();