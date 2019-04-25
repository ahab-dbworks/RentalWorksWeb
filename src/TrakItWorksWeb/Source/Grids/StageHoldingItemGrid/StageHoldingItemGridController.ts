class StageHoldingItemGrid {
    Module: string = 'StageHoldingItemGrid';
    apiurl: string = 'api/v1/stageholdingitem';
    errorSoundFileName: string;
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('.quantity');
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        const errorSound = new Audio(this.errorSoundFileName);

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            let trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            let itemClassValue = $tr.find('[data-browsedatafield="ItemClass"]').attr('data-originalvalue');
            let $oldElement = $quantityColumn.find('div');
            let html: any = [];
            let $grid = $tr.parents('[data-grid="StageHoldingItemGrid"]');

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
                        let $value = $quantityColumn.find('.fieldvalue');
                        let oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                        $value.val(++oldval);
                    },
                    decrement: function () {
                        let $value = $quantityColumn.find('.fieldvalue');
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
                };

                $quantityColumn.on('change', '.fieldvalue', e => {
                    let request: any = {},
                        code = $tr.find('[data-browsedatafield="BarCode"]').attr('data-originalvalue'),
                        orderItemId = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue'),
                        vendorId = $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'),
                        orderId = FwFormField.getValueByDataField($form, 'OrderId'),
                        newValue = jQuery(e.currentTarget).val(),
                        oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                        quantity = Number(newValue) - Number(oldValue);

                    request = {
                        OrderId: orderId,
                        Code: code,
                        Quantity: quantity,
                        OrderItemId: orderItemId,
                        VendorId: vendorId
                    }
                    if (quantity != 0) {
                        FwAppData.apiMethod(true, 'POST', "api/v1/checkout/stageitem", request, FwServices.defaultTimeout,
                            function onSuccess(response) {
                                $form.find('.error-msg.holding').html('');
                                if (response.success) {
                                    $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                    FwBrowse.setFieldValue($grid, $tr, 'QuantityHolding', { value: response.InventoryStatus.QuantityRemaining });
                                } else {
                                    errorSound.play();
                                    $form.find('.error-msg.holding').html(`<div><span>${response.msg}</span></div>`);
                                    $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                                }
                            },
                            function onError(response) {
                                $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                            }, $form);
                    }
                });
            } else {
                $tr.find('.quantity-holding').text('');
                $tr.find('[data-browsedatafield="Quantity"]').attr('data-formreadonly', 'true');
            } //end of trackedBy conditional
        });
    }
}

var StageHoldingItemGridController = new StageHoldingItemGrid();
//----------------------------------------------------------------------------------------------