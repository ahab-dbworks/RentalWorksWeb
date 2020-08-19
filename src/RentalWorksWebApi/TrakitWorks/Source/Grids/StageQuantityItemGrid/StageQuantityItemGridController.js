class StageQuantityItemGrid {
    constructor() {
        this.Module = 'StageQuantityItemGrid';
        this.apiurl = 'api/v1/stagequantityitem';
        this.addItemRequest = {};
    }
    generateRow($control, $generatedtr) {
        this.$form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');
        this.errorMsg = this.$form.find('.error-msg.qty');
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            const trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            const itemClassValue = $tr.find('[data-browsedatafield="ItemClass"]').attr('data-originalvalue');
            const $grid = $tr.parents('[data-grid="StageQuantityItemGrid"]');
            if (trackedByValue === 'QUANTITY' && itemClassValue !== 'K') {
                $quantityColumn.on('change', '.value', e => {
                    const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
                    const type = $grid.attr('data-moduletype');
                    let request = {}, code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue'), orderItemId = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue'), orderId = FwFormField.getValueByDataField(this.$form, `${type}Id`), newValue = jQuery(e.currentTarget).val(), oldValue = $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue'), quantity = Number(newValue) - Number(oldValue);
                    request = {
                        OrderId: orderId,
                        Code: code,
                        WarehouseId: warehouse.warehouseid,
                        Quantity: quantity,
                        OrderItemId: orderItemId
                    };
                    if (quantity != 0) {
                        FwAppData.apiMethod(true, 'POST', "api/v1/checkout/stageitem", request, FwServices.defaultTimeout, response => {
                            this.errorMsg.html('');
                            if (response.success) {
                                $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue', Number(newValue));
                                FwBrowse.setFieldValue($grid, $tr, 'QuantityRemaining', { value: response.InventoryStatus.QuantityRemaining });
                            }
                            if (response.ShowAddItemToOrder === true) {
                                FwFunc.playErrorSound();
                                StagingCheckoutController.showAddItemToOrder = true;
                                this.addItemRequest = {
                                    OrderId: orderId,
                                    Code: code,
                                    WarehouseId: warehouse.warehouseid,
                                    Quantity: quantity,
                                    AddItemToOrder: true
                                };
                                this.$trForAddItem = $tr;
                                this.errorMsg.html(`<div><span>${response.msg}</span></div>`);
                                this.$form.find('div.add-item-qty').html(`<div class="formrow fwformcontrol" onclick="StageQuantityItemGridController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`);
                            }
                            if (response.ShowAddCompleteToOrder === true) {
                                this.$form.find('div.add-item-qty').html(`<div class="formrow"><div class="fwformcontrol" onclick="StageQuantityItemGridController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol add-complete" onclick="StageQuantityItemGridController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`);
                            }
                            if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                                FwFunc.playErrorSound();
                                this.errorMsg.html(`<div><span>${response.msg}</span></div>`);
                                $tr.find('[data-browsedatafield="QuantityStaged"] input').val(Number(oldValue));
                            }
                        }, function onError(response) {
                            $tr.find('[data-browsedatafield="QuantityStaged"] input').val(Number(oldValue));
                        }, this.$form);
                    }
                });
            }
            else {
                $tr.find('.quantity-staged').text('');
                $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-formreadonly', 'true');
            }
        });
    }
    addItemToOrder(element) {
        FwAppData.apiMethod(true, 'POST', `api/v1/checkout/stageitem`, this.addItemRequest, FwServices.defaultTimeout, response => {
            try {
                if (response.success === true) {
                    this.$trForAddItem.find('div[data-browsedatafield="QuantityOrdered"] .fieldvalue').val(response.InventoryStatus.QuantityOrdered);
                    this.$trForAddItem.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue', response.QuantityStaged);
                    FwFunc.playSuccessSound();
                }
                else {
                }
                this.errorMsg.html('');
                this.$form.find('div.add-item-qty').html('');
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, this.$form);
    }
}
var StageQuantityItemGridController = new StageQuantityItemGrid();
//# sourceMappingURL=StageQuantityItemGridController.js.map