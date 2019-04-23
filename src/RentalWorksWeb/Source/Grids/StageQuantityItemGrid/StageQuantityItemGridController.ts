class StageQuantityItemGrid {
    Module: string = 'StageQuantityItemGrid';
    apiurl: string = 'api/v1/stagequantityitem';
    errorSoundFileName: string;
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('.quantity-staged');
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        const errorSound = new Audio(this.errorSoundFileName);

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const originalquantity = $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue');
            const trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            const itemClassValue = $tr.find('[data-browsedatafield="ItemClass"]').attr('data-originalvalue');
            const $oldElement = $quantityColumn.find('div');
            const html: any = [];
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
                };

                $quantityColumn.on('change', '.fieldvalue', e => {
                    const type = $grid.attr('data-moduletype');
                    const errorMsg = $form.find('.error-msg.qty');
                    let request: any = {},
                        code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue'),
                        orderItemId = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue'),
                        orderId = FwFormField.getValueByDataField($form, `${type}Id`),
                        newValue = jQuery(e.currentTarget).val(),
                        oldValue = $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue'),
                        quantity = Number(newValue) - Number(oldValue);

                    request = {
                        OrderId: orderId,
                        Code: code,
                        Quantity: quantity,
                        OrderItemId: orderItemId
                    }
                    if (quantity != 0) {
                        FwAppData.apiMethod(true, 'POST', "api/v1/checkout/stageitem", request, FwServices.defaultTimeout,
                            function onSuccess(response) {
                                errorMsg.html('');
                                if (response.success) {
                                    $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-originalvalue', Number(newValue));
                                    FwBrowse.setFieldValue($grid, $tr, 'QuantityRemaining', { value: response.InventoryStatus.QuantityRemaining });
                                }
                                if (response.ShowAddItemToOrder === true) {
                                    errorSound.play();
                                    StagingCheckoutController.showAddItemToOrder = true;
                                    errorMsg.html(`<div><span>${response.msg}</span></div>`);
                                    $form.find('div.add-item-qty').html(`<div class="formrow fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div>`);
                                }
                                if (response.ShowAddCompleteToOrder === true) {
                                    $form.find('div.add-item-qty').html(`<div class="formrow"><div class="fwformcontrol" onclick="StagingCheckoutController.addItemToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 8px;">Add Item To Order</div><div class="fwformcontrol" onclick="StagingCheckoutController.addCompleteToOrder(this)" data-type="button" style="float:left; margin:6px 0px 0px 4px;">Add Complete To Order</div></div>`)
                                }
                                if (response.success === false && response.ShowAddCompleteToOrder === false && response.ShowAddItemToOrder === false) {
                                        errorSound.play();
                                        errorMsg.html(`<div><span>${response.msg}</span></div>`);
                                        $tr.find('[data-browsedatafield="QuantityStaged"] input').val(Number(oldValue));
                                    }
                            },
                            function onError(response) {
                                $tr.find('[data-browsedatafield="QuantityStaged"] input').val(Number(oldValue));
                            }, $form);
                    }
                });
            } else {
                $tr.find('.quantity-staged').text('');
                $tr.find('[data-browsedatafield="QuantityStaged"]').attr('data-formreadonly', 'true');
            } //end of trackedBy conditional
        });
    }
}

var StageQuantityItemGridController = new StageQuantityItemGrid();
//----------------------------------------------------------------------------------------------