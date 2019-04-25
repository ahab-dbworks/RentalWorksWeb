class StageQuantityItemGrid {
    constructor() {
        this.Module = 'StageQuantityItemGrid';
        this.apiurl = 'api/v1/stagequantityitem';
        this.addItemRequest = {};
    }
    generateRow($control, $generatedtr) {
        this.$form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('.quantity-staged');
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.errorSound = new Audio(this.errorSoundFileName);
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.successSound = new Audio(this.successSoundFileName);
        this.errorMsg = this.$form.find('.error-msg.qty');
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            const originalquantity = $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue');
            const trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            const itemClassValue = $tr.find('[data-browsedatafield="ItemClass"]').attr('data-originalvalue');
            const $oldElement = $quantityColumn.find('div');
            const html = [];
            const $grid = $tr.parents('[data-grid="StageQuantityItemGrid"]');
            if (trackedByValue === 'QUANTITY' && itemClassValue !== 'K') {
                html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
                html.push('<div style="position:relative">');
                html.push(`     <input class="fieldvalue" type="number" style="height:1.5em; width:40px; text-align:center;" value="${originalquantity}">`);
                html.push('</div>');
                html.push('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
                jQuery($oldElement).replaceWith(html.join(''));
                $quantityColumn.data({
                    interval: {},
                    increment: function () {
                        const $value = $quantityColumn.find('.fieldvalue');
                        let oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                        $value.val(++oldval);
                    },
                    decrement: function () {
                        const $value = $quantityColumn.find('.fieldvalue');
                        let oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                        if (oldval > 0) {
                            $value.val(--oldval);
                        }
                    }
                });
                if (jQuery('html').hasClass('desktop')) {
                    $quantityColumn
                        .on('click', '.incrementQuantity', function () {
                        $quantityColumn.data('increment')();
                        $quantityColumn.find('.fieldvalue').change();
                    })
                        .on('click', '.decrementQuantity', function () {
                        $quantityColumn.data('decrement')();
                        $quantityColumn.find('.fieldvalue').change();
                    });
                }
                ;
                $quantityColumn.on('change', '.fieldvalue', e => {
                    const type = $grid.attr('data-moduletype');
                    let request = {}, code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue'), orderItemId = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue'), orderId = FwFormField.getValueByDataField(this.$form, `${type}Id`), newValue = jQuery(e.currentTarget).val(), oldValue = $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue'), quantity = Number(newValue) - Number(oldValue);
                    request = {
                        OrderId: orderId,
                        Code: code,
                        Quantity: quantity,
                        OrderItemId: orderItemId
                    };
                    if (quantity != 0) {
                        FwAppData.apiMethod(true, 'POST', "api/v1/checkout/stageitem", request, FwServices.defaultTimeout, response => {
                            this.errorMsg.html('');
                            this.addItemRequest = {
                                OrderId: orderId,
                                Code: code,
                                Quantity: quantity,
                                AddItemToOrder: true
                            };
                            if (response.success) {
                                $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue', Number(newValue));
                                FwBrowse.setFieldValue($grid, $tr, 'QuantityRemaining', { value: response.InventoryStatus.QuantityRemaining });
                            }
                            if (response.ShowAddItemToOrder === true) {
                                this.errorSound.play();
                                StagingCheckoutController.showAddItemToOrder = true;
                                this.errorMsg.html(`<div><span>${response.msg}</span></div>`);
                                this.$form.find('div.add-item-qty').html(`<div class="formrow fwformcontrol" onclick="StageQuantityItemGridController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`);
                            }
                            if (response.ShowAddCompleteToOrder === true) {
                                this.$form.find('div.add-item-qty').html(`<div class="formrow"><div class="fwformcontrol" onclick="StageQuantityItemGridController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StageQuantityItemGridController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`);
                            }
                            if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                                this.errorSound.play();
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
                }
                else {
                }
                this.errorMsg.html('');
                this.$form.find('div.add-item-qty').html('');
                this.successSound.play();
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        }, null, this.$form);
    }
}
var StageQuantityItemGridController = new StageQuantityItemGrid();
//# sourceMappingURL=StageQuantityItemGridController.js.map