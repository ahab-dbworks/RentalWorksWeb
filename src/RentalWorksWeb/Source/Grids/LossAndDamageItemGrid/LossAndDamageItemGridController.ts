class LossAndDamageItemGrid {
    Module: string = 'LossAndDamageItemGrid';
    apiurl: string = 'api/v1/lossanddamageitem';
    errorSoundFileName: string;
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        let $form, errorSound, $quantityColumn;
        $form = $control.closest('.fwform');
        $quantityColumn = $generatedtr.find('.quantity');
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        errorSound = new Audio(this.errorSoundFileName);

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            let trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            let itemClassValue = $tr.find('[data-browsedatafield="ItemClass"]').attr('data-originalvalue');
            let preventBubble = true;
            let $oldElement = $quantityColumn.find('div');
            let html: any = [];
            let $grid = $tr.parents('[data-grid="LossAndDamageItemGrid"]');
            let sessionId = $grid.data('sessionId');

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
                        .on('mousedown', '.incrementQuantity', function () {
                            $quantityColumn.data('increment')();
                            $quantityColumn.data('interval', setInterval(function () { $quantityColumn.data('increment')(); }, 200));
                        })
                        .on('mouseup', '.incrementQuantity, .decrementQuantity', function () {
                            preventBubble = false;
                            clearInterval($quantityColumn.data('interval'));
                            $quantityColumn.find('.fieldvalue').change();
                        })
                        .on('mousedown', '.decrementQuantity', function () {
                            $quantityColumn.data('decrement')();
                            $quantityColumn.data('interval', setInterval(function () { $quantityColumn.data('decrement')(); }, 200));
                        })
                        .on('mouseleave', '.incrementQuantity, .decrementQuantity', function () {
                            clearInterval($quantityColumn.data('interval'));
                            if (preventBubble) {
                                $quantityColumn.find('.fieldvalue').change();
                            }
                        });
                };

                $quantityColumn.on('change', '.fieldvalue', e => {
                    let request: any = {},
                        code = $tr.find('[data-browsedatafield="BarCode"]').attr('data-originalvalue'),
                        orderItemId = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue'),
                        orderId = $tr.find('[data-browsedatafield="OrderId"]').attr('data-originalvalue'),
                        newValue = jQuery(e.currentTarget).val(),
                        oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                        quantity = Number(newValue) - Number(oldValue);

                    request = {
                        OrderId: orderId,
                        BarCode: code,
                        Quantity: quantity,
                        OrderItemId: orderItemId,
                        SessionId: sessionId
                    }
                    if (quantity != 0) {
                        FwAppData.apiMethod(true, 'POST', "api/v1/lossanddamage/updateitem", request, FwServices.defaultTimeout,
                            function onSuccess(response) {
                                let errormsg = $form.find('.error-msg');
                                errormsg.html('');
                                if (response.success) {
                                    $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                } else {
                                    errorSound.play();
                                    errormsg.html(`<div style="margin:0px 0px 0px 8px;"><span style="padding:0px 4px 0px 4px;font-size:22px;border-radius:2px;background-color:red;color:white;">${response.msg}</span></div>`);
                                    $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                                }
                                preventBubble = true;
                            },
                            function onError(response) {
                                $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                            }, $form);
                    }
                });
            } else {
                $tr.find('.quantity').text('');
                $tr.find('[data-browsedatafield="Quantity"]').attr('data-formreadonly', 'true');
            } //end of trackedBy conditional
        });
    }
}

var LossAndDamageItemGridController = new LossAndDamageItemGrid();
//----------------------------------------------------------------------------------------------