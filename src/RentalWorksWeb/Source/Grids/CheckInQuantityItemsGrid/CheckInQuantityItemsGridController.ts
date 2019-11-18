class CheckInQuantityItemsGrid {
    Module: string = 'CheckInQuantityItemsGrid';
    apiurl: string = 'api/v1/checkinquantityitem';

    generateRow($control, $generatedtr) {
        let $form, $quantityColumn;
        $form = $control.closest('.fwform'),
            $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let allowQuantityVal = $tr.find('[data-browsedatafield="AllowQuantity"]').attr('data-originalvalue');
            let $grid = $tr.parents('[data-grid="CheckInQuantityItemsGrid"]');
        
            //Hides Quantity increment/decrement controls if allowQuantityVal is false
            if (allowQuantityVal == "false") {
                $quantityColumn
                    .hide()
                    .parents('td')
                    .css('background-color', 'rgb(245,245,245)');
            }

            $quantityColumn.on('change', '.value', e => {
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
                    Quantity: quantity,
                    ModuleType: 'O'
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