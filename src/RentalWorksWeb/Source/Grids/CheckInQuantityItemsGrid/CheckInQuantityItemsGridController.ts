class CheckInQuantityItemsGrid {
    Module: string = 'CheckInQuantityItemsGrid';
    apiurl: string = 'api/v1/checkinquantityitem';

    generateRow($control, $generatedtr) {
        let self = this;
        let $form, $quantityColumn;
        $form = $control.closest('.fwform'),
            $quantityColumn = $generatedtr.find('.quantity');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let allowQuantityVal = $tr.find('[data-browsedatafield="AllowQuantity"]').attr('data-originalvalue');
            let originalquantity = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
            let $grid = $tr.parents('[data-grid="CheckInQuantityItemsGrid"]');
            let $oldElement = $quantityColumn.find('div');

            let html: any = [];
            html.push('<button class="decrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">-</button>');
            html.push('<div style="position:relative">');
            html.push(`     <input class="fieldvalue" type="number" style="height:1.5em; width:40px; text-align:center;" value="${originalquantity}">`);
            html.push('</div>');
            html.push('<button class="incrementQuantity" tabindex="-1" style="padding: 5px 0px; float:left; width:25%; border:none;">+</button>');
            jQuery($oldElement).replaceWith(html.join(''));

            //Hides Quantity increment/decrement controls if allowQuantityVal is false
            if (allowQuantityVal == "false") {
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
                    $value.val(++oldval);
                },
                decrement: function () {
                    var $value = $quantityColumn.find('.fieldvalue');
                    var oldval = jQuery.isNumeric(parseFloat($value.val())) ? parseFloat($value.val()) : 0;
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
                    contractId = FwFormField.getValueByDataField($form, 'ContractId'),
                    orderItemId = $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue'),
                    code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue'),
                    orderItemIdComment,
                    codeComment,
                    newValue = jQuery(e.currentTarget).val(),
                    oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                    quantity = Number(newValue) - Number(oldValue);

                request = {
                    ContractId: contractId,
                    OrderItemId: orderItemId,
                    Code: code,
                    Quantity: quantity
                };

                if (orderItemIdComment) {
                    request.OrderItemIdComment = orderItemIdComment;
                };

                if (codeComment) {
                    request.CodeComment = codeComment;
                };

                if (quantity != 0) {
                    FwAppData.apiMethod(true, 'POST', "api/v1/checkin/checkinitem", request, FwServices.defaultTimeout,
                        function onSuccess(response) {
                            if (response.success) {
                                $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                FwBrowse.setFieldValue($grid, $tr, 'QuantityOut', { value: response.InventoryStatus.QuantityOut });
                            } else {
                                $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                            }
                        },
                        function onError(response) {
                            $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                        }
                        , $form);
                }
            });
        });
    }
    addLegend($control) {
        FwBrowse.addLegend($control, 'Sub Vendor', '#ebb58e');
        FwBrowse.addLegend($control, 'Consignor', '#857cfa');
    }
}

var CheckInQuantityItemsGridController = new CheckInQuantityItemsGrid();
//----------------------------------------------------------------------------------------------