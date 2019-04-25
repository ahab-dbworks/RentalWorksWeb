class OrderItemGrid {
    constructor() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
        this.beforeValidateItem = function ($browse, $grid, request, datafield, $tr) {
            var rate = $tr.find('div[data-browsedatafield="RecType"] input.value').val();
            if (rate !== null) {
                switch (rate) {
                    case 'R':
                        request.uniqueIds = { AvailFor: 'R' };
                        break;
                    case 'S':
                        request.uniqueIds = { AvailFor: 'S' };
                        break;
                    case 'M':
                        request.uniqueIds = { AvailFor: 'M' };
                        break;
                    case 'L':
                        request.uniqueIds = { AvailFor: 'L' };
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
        else if ($grid.hasClass('P')) {
            FwBrowse.setFieldValue($grid, $tr, 'RecType', { value: 'P' });
            inventoryType = 'Parts';
        }
        if ($form[0].dataset.controller !== "TemplateController" && $form[0].dataset.controller !== "PurchaseOrderController") {
            let daysPerWeek, discountPercent;
            FwBrowse.setFieldValue($grid, $tr, 'PickDate', { value: pickDate });
            FwBrowse.setFieldValue($grid, $tr, 'PickTime', { value: pickTime });
            FwBrowse.setFieldValue($grid, $tr, 'FromDate', { value: fromDate });
            FwBrowse.setFieldValue($grid, $tr, 'FromTime', { value: fromTime });
            FwBrowse.setFieldValue($grid, $tr, 'ToDate', { value: toDate });
            FwBrowse.setFieldValue($grid, $tr, 'ToTime', { value: toTime });
        }
    }
    generateRow($control, $generatedtr) {
        var $form = $control.closest('.fwform');
        if ($form.attr('data-controller') === 'OrderController' || $form.attr('data-controller') === 'QuoteController' || $form.attr('data-controller') === 'PurchaseOrderController') {
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
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                let isRentalGrid = jQuery($control).parent('[data-grid="OrderItemGrid"]').hasClass('R');
                $generatedtr.find('.field[data-browsedatafield="ItemId"] input').val('');
                $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
                if ($generatedtr.hasClass("newmode")) {
                    $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
                    $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                    $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                    $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                    $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);
                    if ($form.attr('data-controller') === 'PurchaseOrderController') {
                        $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouse);
                    }
                }
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
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input').val(warehouseId);
                $generatedtr.find('.field[data-browsedatafield="WarehouseId"] input.text').val(warehouseCode);
                $generatedtr.find('.field[data-browsedatafield="ReturnToWarehouseId"] input.text').val(warehouseCode);
            });
        }
    }
    ;
    toggleOrderItemView($form, event, module) {
        let $element, $orderItemGrid, recType, isSummary, orderId, isSubGrid;
        $element = jQuery(event.currentTarget);
        recType = $element.closest('[data-grid="OrderItemGrid"]').attr('class');
        isSubGrid = $element.closest('[data-grid="OrderItemGrid"]').attr('data-issubgrid');
        orderId = FwFormField.getValueByDataField($form, `${module}Id`);
        if (recType === 'A') {
            recType = undefined;
        }
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
    orderItemGridBoldUnbold($browse, event) {
        let orderId, $selectedCheckBoxes, boldItems = [];
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');
        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let order = {};
            let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
            let description = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Description"]').attr('data-originalvalue');
            let quantityOrdered = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue');
            let recType = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RecType"]').attr('data-originalvalue');
            order.OrderItemId = orderItemId;
            order.OrderId = orderId;
            order.Description = description;
            order.QuantityOrdered = quantityOrdered;
            order.RecType = recType;
            if (orderId != null) {
                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Bold"]').attr('data-originalvalue') === 'true') {
                    order.Bold = false;
                    boldItems.push(order);
                }
                else {
                    order.Bold = true;
                    boldItems.push(order);
                }
            }
        }
        boldUnboldItem(boldItems);
        function boldUnboldItem(orders) {
            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, orders, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        }
        ;
    }
    ;
    orderItemGridLockUnlock($browse, event) {
        let orderId, $selectedCheckBoxes, lockedItems = [];
        orderId = $browse.find('.selected [data-browsedatafield="OrderId"]').attr('data-originalvalue');
        $selectedCheckBoxes = $browse.find('.cbselectrow:checked');
        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let order = {};
            let orderItemId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            let orderId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
            let description = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Description"]').attr('data-originalvalue');
            let quantityOrdered = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="QuantityOrdered"]').attr('data-originalvalue');
            let recType = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="RecType"]').attr('data-originalvalue');
            order.OrderItemId = orderItemId;
            order.OrderId = orderId;
            order.Description = description;
            order.QuantityOrdered = quantityOrdered;
            order.RecType = recType;
            if (orderId != null) {
                if ($selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="Locked"]').attr('data-originalvalue') === 'true') {
                    order.Locked = false;
                    lockedItems.push(order);
                }
                else {
                    order.Locked = true;
                    lockedItems.push(order);
                }
            }
        }
        lockUnlockItem(lockedItems);
        function lockUnlockItem(orders) {
            FwAppData.apiMethod(true, 'POST', `api/v1/orderitem/many`, orders, FwServices.defaultTimeout, function onSuccess(response) {
                FwBrowse.databind($browse);
            }, function onError(response) {
                FwFunc.showError(response);
                FwBrowse.databind($browse);
            }, $browse);
        }
        ;
    }
    ;
}
FwApplicationTree.clickEvents['{77E511EC-5463-43A0-9C5D-B54407C97B15}'] = function (e) {
    let grid = jQuery(e.currentTarget).parents('[data-control="FwGrid"]');
    let search, $form, orderId, quoteId, purchaseOrderId, templateId;
    $form = jQuery(this).closest('.fwform');
    let controllerName = $form.attr('data-controller');
    search = new SearchInterface();
    let gridInventoryType;
    if (grid.hasClass('R')) {
        gridInventoryType = 'Rental';
    }
    else if (grid.hasClass('S')) {
        gridInventoryType = 'Sales';
    }
    else if (grid.hasClass('L')) {
        gridInventoryType = 'Labor';
    }
    else if (grid.hasClass('M')) {
        gridInventoryType = 'Misc';
    }
    else if (grid.hasClass('P')) {
        gridInventoryType = 'Parts';
    }
    if ($form.attr('data-mode') === 'NEW') {
        let isValid = FwModule.validateForm($form);
        if (isValid) {
            let activeTabId = jQuery($form.find('[data-type="tab"].active')).attr('id');
            if (controllerName === "OrderController") {
                OrderController.saveForm($form, { closetab: false });
            }
            else if (controllerName === "QuoteController") {
                QuoteController.saveForm($form, { closetab: false });
            }
            $form.attr('data-opensearch', 'true');
            $form.attr('data-searchtype', gridInventoryType);
            $form.attr('data-activetabid', activeTabId);
        }
        return;
    }
    switch (controllerName) {
        case 'OrderController':
            orderId = FwFormField.getValueByDataField($form, 'OrderId');
            if (orderId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                search.renderSearchPopup($form, orderId, 'Order', gridInventoryType);
            }
            break;
        case 'QuoteController':
            quoteId = FwFormField.getValueByDataField($form, 'QuoteId');
            if (quoteId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                search.renderSearchPopup($form, quoteId, 'Quote', gridInventoryType);
            }
            break;
        case 'PurchaseOrderController':
            purchaseOrderId = FwFormField.getValueByDataField($form, 'PurchaseOrderId');
            if (purchaseOrderId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                search.renderSearchPopup($form, purchaseOrderId, 'PurchaseOrder', gridInventoryType);
            }
            break;
        case 'TemplateController':
            templateId = FwFormField.getValueByDataField($form, 'TemplateId');
            if (templateId == '') {
                FwNotification.renderNotification('WARNING', 'Save the record before performing this function');
            }
            else {
                search.renderSearchPopup($form, templateId, 'Template', gridInventoryType);
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
FwApplicationTree.clickEvents['{87C47D00-E950-4724-8A8B-4528D0B41124}'] = function (event) {
    let $form;
    $form = jQuery(this).closest('.fwform');
    let module = $form.attr('data-controller').replace('Controller', '');
    try {
        OrderItemGridController.toggleOrderItemView($form, event, module);
        jQuery(document).trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{B6B68464-B95C-4A4C-BAF2-6AA59B871468}'] = function (e) {
    const $form = jQuery(this).closest('.fwform');
    const $grid = jQuery(this).closest('[data-name="OrderItemGrid"]');
    let recType;
    recType = jQuery(this).closest('[data-grid="OrderItemGrid"]');
    if (recType.hasClass('R')) {
        recType = 'R';
    }
    else if (recType.hasClass('S')) {
        recType = 'S';
    }
    else if (recType.hasClass('L')) {
        recType = 'L';
    }
    else if (recType.hasClass('M')) {
        recType = 'M';
    }
    else if (recType.hasClass('P')) {
        recType = 'P';
    }
    else if (recType.hasClass('A')) {
        recType = '';
    }
    else if (recType.hasClass('RS')) {
        recType = 'RS';
    }
    let module = $form.attr('data-controller').replace('Controller', '');
    const HTML = [];
    HTML.push(`<div class="fwcontrol fwcontainer fwform popup template-popup" data-control="FwContainer" data-type="form">
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div style="float:right;" class="close-modal"><i class="material-icons">clear</i><div class="btn-text">Close</div></div>
                <div class="tabpages">
                  <div class="formpage">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section">
                      <div class="formrow">
                        <div class="formcolumn" style="width:100%;margin-top:5px;">
                          <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                            <div class="fwform-section-title" style="margin-bottom:10px;">Copy Template to ${module}</div>
                            <div data-control="FwGrid" class="container"></div>
                          </div>
                        </div>
                      </div>
                      <div class="formrow add-button">
                        <div class="select-items fwformcontrol" data-type="button" style="float:right;">Add to ${module}</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>`);
    const addTemplateBrowse = () => {
        let $browse = FwBrowse.loadBrowseFromTemplate('Template');
        $browse.attr('data-hasmultirowselect', 'true');
        $browse = FwModule.openBrowse($browse);
        $browse.find('.fwbrowse-menu').hide();
        $browse.data('ondatabind', function (request) {
            request.pagesize = 20;
            request.orderby = "Description asc";
        });
        return $browse;
    };
    const $popupHtml = HTML.join('');
    const $popup = FwPopup.renderPopup(jQuery($popupHtml), { ismodal: true });
    FwPopup.showPopup($popup);
    const $templateBrowse = addTemplateBrowse();
    $popup.find('.container').append($templateBrowse);
    const $templatePopup = $popup.find('.template-popup');
    $popup.find('.close-modal').one('click', e => {
        FwPopup.destroyPopup($popup);
        jQuery(document).find('.fwpopup').off('click');
        jQuery(document).off('keydown');
    });
    $popup.on('click', '.select-items', e => {
        const $selectedCheckBoxes = $popup.find('[data-control="FwGrid"] tbody .cbselectrow:checked');
        let templateIds = [];
        for (let i = 0; i < $selectedCheckBoxes.length; i++) {
            let $this = jQuery($selectedCheckBoxes[i]);
            let id;
            id = $this.closest('tr').find('div[data-browsedatafield="TemplateId"]').attr('data-originalvalue');
            templateIds.push(id);
        }
        ;
        let request = {};
        request = {
            TemplateIds: templateIds,
            RecType: recType,
            OrderId: FwFormField.getValueByDataField($form, `${module}Id`)
        };
        FwAppData.apiMethod(true, 'POST', `api/v1/order/copytemplate`, request, FwServices.defaultTimeout, function onSuccess(response) {
            $popup.find('.close-modal').click();
            FwBrowse.search($grid);
        }, null, $templatePopup);
    });
    FwBrowse.search($templateBrowse);
};
FwApplicationTree.clickEvents['{9476D532-5274-429C-A563-FE89F5B89B01}'] = function (e) {
    const $orderItemGrid = jQuery(this).closest('[data-name="OrderItemGrid"]');
    let recType;
    recType = jQuery(this).closest('[data-grid="OrderItemGrid"]');
    if (recType.hasClass('R')) {
        recType = 'R';
    }
    else if (recType.hasClass('S')) {
        recType = 'S';
    }
    else if (recType.hasClass('L')) {
        recType = 'L';
    }
    else if (recType.hasClass('M')) {
        recType = 'M';
    }
    else if (recType.hasClass('P')) {
        recType = 'P';
    }
    else if (recType.hasClass('A')) {
        recType = '';
    }
    else if (recType.hasClass('RS')) {
        recType = 'RS';
    }
    const pageNumber = $orderItemGrid.attr('data-pageno');
    const onDataBind = $orderItemGrid.data('ondatabind');
    if (typeof onDataBind == 'function') {
        $orderItemGrid.data('ondatabind', function (request) {
            onDataBind(request);
            request.uniqueids.RefreshAvailability = true;
            request.pageno = parseInt(pageNumber);
        });
    }
    FwBrowse.search($orderItemGrid);
    $orderItemGrid.attr('data-pageno', pageNumber);
    $orderItemGrid.data('ondatabind', onDataBind);
    jQuery(document).trigger('click');
};
FwApplicationTree.clickEvents['{E2DF5CB4-CD18-42A0-AE7C-18C18E6C4646}'] = function (event) {
    const $browse = jQuery(this).closest('.fwbrowse');
    try {
        OrderItemGridController.orderItemGridBoldUnbold($browse, event);
        jQuery(document).trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
FwApplicationTree.clickEvents['{BC467EF9-F255-4F51-A6F2-57276D8824A3}'] = function (event) {
    const $browse = jQuery(this).closest('.fwbrowse');
    try {
        OrderItemGridController.orderItemGridLockUnlock($browse, event);
        jQuery(document).trigger('click');
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
var OrderItemGridController = new OrderItemGrid();
//# sourceMappingURL=OrderItemGridController.js.map