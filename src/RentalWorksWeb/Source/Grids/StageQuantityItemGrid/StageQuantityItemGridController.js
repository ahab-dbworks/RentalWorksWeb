class StageQuantityItemGrid {
    constructor() {
        this.Module = 'StageQuantityItemGrid';
        this.apiurl = 'api/v1/stagequantityitem';
        this.barCodedItemIncreased = false;
    }
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
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            let originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            let trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            let quantityColorIndex = dt.ColumnIndex.QuantityColor;
            let color = dt.Rows[rowIndex][quantityColorIndex];
            if (trackedByValue === 'QUANTITY') {
                color = 'transparent';
                var $grid = $tr.parents('[data-grid="StageQuantityItemGrid"]');
                let $oldElement = $quantityColumn.find('div');
                let html = [];
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
                        }
                        else {
                            $value.val(++oldval);
                        }
                    },
                    decrement: function () {
                        var $value = $quantityColumn.find('.fieldvalue');
                        var oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
                        if ((typeof $quantityColumn.attr('data-minvalue') !== 'undefined') && ($quantityColumn.attr('data-minvalue') >= oldval)) {
                        }
                        else {
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
                }
                ;
                $quantityColumn.on('change', '.fieldvalue', e => {
                    let request = {}, contractId = $tr.find('[data-browsedatafield="ContractId"]').attr('data-originalvalue'), itemId = $tr.find('[data-browsedatafield="PurchaseOrderItemId"]').attr('data-originalvalue'), poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId'), newValue = jQuery(e.currentTarget).val(), oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'), quantity = Number(newValue) - Number(oldValue);
                    request = {
                        ContractId: contractId,
                        PurchaseOrderItemId: itemId,
                        PurchaseOrderId: poId,
                        Quantity: quantity
                    };
                    if (quantity != 0) {
                        FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorderreceiveitem/receiveitems", request, FwServices.defaultTimeout, function onSuccess(response) {
                            let errormsg = $form.find('.errormsg');
                            errormsg.html('');
                            if (response.success) {
                                $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                FwBrowse.setFieldValue($grid, $tr, 'QuantityReceived', { value: response.QuantityReceived });
                                if (response.QuantityColor) {
                                    $quantityColumn.find('.cellcolor').css('border-left', `20px solid ${response.QuantityColor}`);
                                }
                                else {
                                    $quantityColumn.find('.cellcolor').css('border-left', `20px solid transparent`);
                                }
                            }
                            else {
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
                            }
                            else {
                                $form.find('.createcontract[data-type="button"]').show();
                                $form.find('.createcontract[data-type="btnmenu"]').hide();
                            }
                        }, null, null);
                    }
                });
            }
            else {
                $tr.find('[data-browsedatafield="Quantity"] fieldvalue').val('');
                $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', '');
                $tr.find('[data-browsedatafield="Quantity"]').attr('data-formreadonly', 'true');
            }
        });
    }
}
var StageQuantityItemGridController = new StageQuantityItemGrid();
//# sourceMappingURL=StageQuantityItemGridController.js.map