class StageHoldingItemGrid {
    Module: string = 'StageHoldingItemGrid';
    apiurl: string = 'api/v1/stageholdingitem';
    errorSoundFileName: string;
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        const errorSound = new Audio(this.errorSoundFileName);

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let trackedByValue = $tr.find('[data-browsedatafield="TrackedBy"]').attr('data-originalvalue');
            let itemClassValue = $tr.find('[data-browsedatafield="ItemClass"]').attr('data-originalvalue');
            let $grid = $tr.parents('[data-grid="StageHoldingItemGrid"]');

            if (trackedByValue === 'QUANTITY' && itemClassValue !== 'K') {
                $quantityColumn.on('change', '.value', e => {
                    const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));

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
                        WarehouseId: warehouse.warehouseid,
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