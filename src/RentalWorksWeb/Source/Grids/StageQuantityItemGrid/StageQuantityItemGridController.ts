﻿class StageQuantityItemGrid {
    Module: string = 'StageQuantityItemGrid';
    apiurl: string = 'api/v1/stagequantityitem';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    barCodedItemIncreased: boolean = false;

    generateRow($control, $generatedtr) {
        let $form, errorSound, successSound, $quantityColumn;
        $form = $control.closest('.fwform'),
            $quantityColumn = $generatedtr.find('.quantity');
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;

        let self = this;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            let trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            let quantityColorIndex = dt.ColumnIndex.QuantityColor;
            let color = dt.Rows[rowIndex][quantityColorIndex];

            if (trackedByValue === 'QUANTITY') {
                //if (color == "") {
                color = 'transparent';
                //}
                var $grid = $tr.parents('[data-grid="StageQuantityItemGrid"]');

                let $oldElement = $quantityColumn.find('div');
                let html: any = [];
                html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
                html.push('<div style="position:relative">');
                html.push('     <div class="cellcolor" style="pointer-events:none"></div>');
                html.push('     <input class="fieldvalue" type="number" style="height:1.5em; width:40px; text-align:center;" value="' + originalquantity + '">');
                html.push('</div>');
                html.push('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
                jQuery($oldElement).replaceWith(html.join(''));

                let cellToColor = $generatedtr.find('.cellcolor');
                cellToColor.css({
                    'border-left': '20px solid',
                    'border-right': '20px solid transparent',
                    'border-bottom': '20px solid transparent',
                    'left': '0',
                    'top': '0',
                    'height': '0',
                    'width': '0',
                    'position': 'absolute',
                    'right': '0px',
                    'border-left-color': color
                });

                $quantityColumn.data({
                    interval: {},
                    increment: function () {
                        var $value = $quantityColumn.find('.fieldvalue');
                        var oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                        if ((typeof $quantityColumn.attr('data-maxvalue') !== 'undefined') && ($quantityColumn.attr('data-maxvalue') <= oldval)) {

                        } else {
                            $value.val(++oldval);
                        }
                    },
                    decrement: function () {
                        var $value = $quantityColumn.find('.fieldvalue');
                        var oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                        if ((typeof $quantityColumn.attr('data-minvalue') !== 'undefined') && ($quantityColumn.attr('data-minvalue') >= oldval)) {

                        } else {
                            if (oldval > 0) {
                                $value.val(--oldval);
                            }
                        }
                    }
                });

                if (jQuery('html').hasClass('desktop')) {
                    $quantityColumn
                        .on('mousedown', '.incrementQuantity', function () {
                            $quantityColumn.data('increment')();
                            $quantityColumn.data('interval', setInterval(function () { $quantityColumn.data('increment')(); }, 200));
                        })
                        .on('mouseup mouseleave', '.incrementQuantity', function () {
                            clearInterval($quantityColumn.data('interval'));
                            $quantityColumn.find('.fieldvalue').change();
                        })
                        .on('mousedown', '.decrementQuantity', function () {
                            $quantityColumn.data('decrement')();
                            $quantityColumn.data('interval', setInterval(function () { $quantityColumn.data('decrement')(); }, 200));
                        })
                        .on('mouseup mouseleave', '.decrementQuantity', function () {
                            clearInterval($quantityColumn.data('interval'));
                            $quantityColumn.find('.fieldvalue').change();
                        });
                };

                $quantityColumn.on('change', '.fieldvalue', e => {
                    let request: any = {},
                        code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue'),
                        orderId = FwFormField.getValueByDataField($form, 'OrderId'),
                        newValue = jQuery(e.currentTarget).val(),
                        oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                        quantity = Number(newValue) - Number(oldValue);

                    request = {
                        OrderId: orderId,
                        Code: code,
                        Quantity: quantity
                    }
                    if (quantity != 0) {
                        FwAppData.apiMethod(true, 'POST', "api/v1/checkout/stageitem", request, FwServices.defaultTimeout,
                            function onSuccess(response) {
                                let errormsg = $form.find('.errormsg');
                                errormsg.html('');
                                if (response.success) {
                                    $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                    FwBrowse.setFieldValue($grid, $tr, 'QuantityStaged', { value: response.QuantityStaged });
                                    if (response.QuantityColor) {
                                        $quantityColumn.find('.cellcolor').css('border-left', `20px solid ${response.QuantityColor}`);
                                    } else {
                                        $quantityColumn.find('.cellcolor').css('border-left', `20px solid transparent`);
                                    }
                                } else {
                                    errorSound.play();
                                    errormsg.html(`<div style="margin-left:8px; margin-top: 10px;"><span>${response.msg}</span></div>`);
                                    $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                                }

                                let $itemsTrackedByBarcode = $control.find('[data-browsedatafield="TrackedBy"][data-originalvalue="BARCODE"]');
                                for (let i = 0; i < $itemsTrackedByBarcode.length; i++) {
                                    let barcodeQuantity = jQuery($itemsTrackedByBarcode[i]).parents('tr').find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                                    self.barCodedItemIncreased = false;
                                    if (+barcodeQuantity > 0) {
                                        self.barCodedItemIncreased = true;
                                        break;
                                    }
                                }
                                if (self.barCodedItemIncreased) {
                                    $form.find('.createcontract[data-type="button"]').hide();
                                    $form.find('.createcontract[data-type="btnmenu"]').show();
                                } else {
                                    $form.find('.createcontract[data-type="button"]').show();
                                    $form.find('.createcontract[data-type="btnmenu"]').hide();
                                }
                            },
                            null, null);
                    }
                });
            } else {
                $tr.find('.quantity').text('');
                $tr.find('[data-browsedatafield="Quantity"]').attr('data-formreadonly', 'true');
            } //end of trackedBy conditional
        });
    }
}

var StageQuantityItemGridController = new StageQuantityItemGrid();
//----------------------------------------------------------------------------------------------