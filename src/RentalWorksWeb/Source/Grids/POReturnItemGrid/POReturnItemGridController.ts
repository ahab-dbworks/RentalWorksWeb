class POReturnItemGrid {
    Module: string = 'POReturnItemGrid';
    apiurl: string = 'api/v1/purchaseorderreturnitem';

    generateRow($control, $generatedtr) {
        let $form = $control.closest('.fwform'),
            $quantityColumn = $generatedtr.find('.quantity');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            let $grid = $tr.parents('[data-grid="POReturnItemGrid"]');

            let $oldElement = $quantityColumn.find('div');
            let html = [];
            html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
            html.push('<input class="fieldvalue" type="number" style="height:1.5em; width:40px; text-align:center;" value="' + originalquantity + '">');
            html.push('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
            jQuery($oldElement).replaceWith(html.join(''));

            let trackedBy = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            //Hides Quantity controls if item is tracked by barcode
            if (trackedBy === "BARCODE") {
                $quantityColumn
                    .hide()
                    .parents('td')
                    .css('background-color', 'rgb(245,245,245)');
            }

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
                    contractId = $tr.find('[data-browsedatafield="ContractId"]').attr('data-originalvalue'),
                    itemId = $tr.find('[data-browsedatafield="PurchaseOrderItemId"]').attr('data-originalvalue'),
                    poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    newValue = jQuery(e.currentTarget).val(),
                    oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                    quantity = +newValue - +oldValue;

                request = {
                    ContractId: contractId,
                    PurchaseOrderItemId: itemId,
                    PurchaseOrderId: poId,
                    Quantity: quantity
                }

                if (quantity != 0) {
                    $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', +newValue);
                    FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorderreturnitem/returnitems", request, FwServices.defaultTimeout,
                        function onSuccess(response) {
                            FwBrowse.setFieldValue($grid, $tr, 'QuantityReturned', { value: response.QuantityReturned });
                        }, function onError(response) {
                            FwFunc.showError(response);
                            $tr.find('[data-browsedatafield="Quantity"] input').val(+oldValue);
                        }, null);
                }
            });
        });
    }
}

var POReturnItemGridController = new POReturnItemGrid();
//----------------------------------------------------------------------------------------------