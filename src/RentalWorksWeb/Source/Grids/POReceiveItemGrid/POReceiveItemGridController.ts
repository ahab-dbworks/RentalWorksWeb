class POReceiveItemGrid {
    Module: string = 'POReceiveItemGrid';
    apiurl: string = 'api/v1/purchaseorderreceiveitem';

    generateRow($control, $generatedtr) {
        let $form = $control.closest('.fwform'),
            $quantityColumn = $generatedtr.find('.quantity');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            let quantityColorIndex = dt.ColumnIndex.QuantityColor;
            let color = dt.Rows[rowIndex][quantityColorIndex];
            if (color == "") {
                color = 'transparent';
            }
            let $grid = $tr.parents('[data-grid="POReceiveItemGrid"]');

            //$quantityColumn
            //    .prepend('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>')
            //    .append('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>')
            //    .find('.fieldvalue').css('text-align', 'center');

            let $oldElement = $quantityColumn.find('div');
            let html: any = [];
            html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
            html.push('<div style="position:relative">');
            html.push('     <div class="cellcolor"></div>');
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
                'border-left-color': color,
                'z-index': '2'
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
                    contractId = $tr.find('[data-browsedatafield="ContractId"]').attr('data-originalvalue'),
                    itemId = $tr.find('[data-browsedatafield="PurchaseOrderItemId"]').attr('data-originalvalue'),
                    poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    newValue = jQuery(e.currentTarget).val(),
                    oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                    quantity = Number(newValue) - Number(oldValue);

                request = {
                    ContractId: contractId,
                    PurchaseOrderItemId: itemId,
                    PurchaseOrderId: poId,
                    Quantity: quantity
                }

                if (quantity != 0) {
                    $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                    FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorderreceiveitem/receiveitems", request, FwServices.defaultTimeout,
                        function onSuccess(response) {
                            FwBrowse.setFieldValue($grid, $tr, 'QuantityReceived', { value: response.QuantityReceived });

                            if (response.QuantityColor) {
                                $quantityColumn.find('.cellcolor').css('border-left', `20px solid ${response.QuantityColor}`);
                            } else {
                                $quantityColumn.find('.cellcolor').css('border-left', `20px solid transparent`);
                            }
                        },
                        function onError(response) {
                            FwFunc.showError(response);
                            $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                        }
                        , null);
                }
            });
        });
    }
    addLegend($control) {
        FwBrowse.addLegend($control, 'Positive Conflict', '#239B56');
        FwBrowse.addLegend($control, 'Items Needing Bar Code/Serial No./RFID', '#6C3483');

    }
}

var POReceiveItemGridController = new POReceiveItemGrid();
//----------------------------------------------------------------------------------------------