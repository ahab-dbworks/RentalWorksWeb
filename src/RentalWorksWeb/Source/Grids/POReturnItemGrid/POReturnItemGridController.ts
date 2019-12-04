class POReturnItemGrid {
    Module: string = 'POReturnItemGrid';
    apiurl: string = 'api/v1/purchaseorderreturnitem';

    generateRow($control, $generatedtr) {
        let $form = $control.closest('.fwform'),
            $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let $grid = $tr.parents('[data-grid="POReturnItemGrid"]');
            let trackedBy = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            //Hides Quantity controls if item is tracked by barcode
            if (trackedBy === "BARCODE") {
                $quantityColumn
                    .hide()
                    .parents('td')
                    .css('background-color', 'rgb(245,245,245)');
            }
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