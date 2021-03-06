﻿class MigrateItemGrid {
    Module: string = 'MigrateItemGrid';
    apiurl: string = 'api/v1/migrateitem';
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const quantityOutValue = +$tr.find('[data-browsedatafield="QuantityOut"]').attr('data-originalvalue');
            if (quantityOutValue !== 0) {
                $quantityColumn.on('change', '.value', e => {
                    const $grid = $tr.parents('[data-grid="MigrateItemGrid"]');
                    const sessionId = $grid.data('sessionId');
                    let request: any = {},
                        //code = $tr.find('[data-browsedatafield="BarCode"]').attr('data-originalvalue'),
                        code = $tr.find('[data-browsedatafield="ItemId"]').attr('data-originaltext'),
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
                        FwAppData.apiMethod(true, 'POST', "api/v1/migrate/updateitem", request, FwServices.defaultTimeout,
                            function onSuccess(response) {
                                const errorMsg = $form.find('.error-msg:not(.qty)');
                                errorMsg.html('');
                                if (response.success) {
                                    $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                    $tr.find('[data-browsedatafield="Quantity"] .value').val(+response.NewQuantity);
                                } else {
                                    FwFunc.playErrorSound();
                                    errorMsg.html(`<div><span>${response.msg}</span></div>`);
                                    $tr.find('[data-browsedatafield="Quantity"] .value').val(+response.NewQuantity);
                                }
                            },
                            function onError(response) {
                                $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                            }, $form);
                    }
                });
            } else {
                $tr.find('.quantity').text('');
                $tr.find('[data-browsedatafield="Quantity"]').attr('data-formreadonly', 'true');
            } //end of quantityOut conditional
        });
    }
}

var MigrateItemGridController = new MigrateItemGrid();
//----------------------------------------------------------------------------------------------