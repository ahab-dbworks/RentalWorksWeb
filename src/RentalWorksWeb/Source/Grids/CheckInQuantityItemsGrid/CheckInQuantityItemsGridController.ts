﻿class CheckInQuantityItemsGrid {
    Module: string = 'CheckInQuantityItemsGrid';
    apiurl: string = 'api/v1/checkinquantityitem';

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            //Hides Quantity increment/decrement controls if allowQuantityVal is false
            const allowQuantityVal = $tr.find('[data-browsedatafield="AllowQuantity"]').attr('data-originalvalue');
            if (allowQuantityVal == "false") {
                $quantityColumn
                    .hide()
                    .parents('td')
                    .css('background-color', 'rgb(245,245,245)');
            }

            $quantityColumn.on('change', '.value', e => {
                const newValue = jQuery(e.currentTarget).val();
                const oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                const quantity = Number(newValue) - Number(oldValue);

                const request: any = {
                    ContractId: FwFormField.getValueByDataField($form, 'ContractId'),
                    OrderItemId: $tr.find('[data-browsedatafield="OrderItemId"]').attr('data-originalvalue'),
                    Code: $tr.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext'),
                    Quantity: quantity,
                    ModuleType: 'O',
                    VendorId: $tr.find('[data-browsedatafield="VendorId"]').attr('data-originalvalue'),
                };

                if (quantity != 0) {
                    const errorMsg = $form.find('.error-msg.qty');
                    FwAppData.apiMethod(true, 'POST', "api/v1/checkin/checkinitem", request, FwServices.defaultTimeout,
                        function onSuccess(response) {
                            errorMsg.html('');
                            if (response.success) {
                                $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                const $grid = $tr.parents('[data-grid="CheckInQuantityItemsGrid"]');
                                FwBrowse.setFieldValue($grid, $tr, 'QuantityOut', { value: response.InventoryStatus.QuantityOut });
                            } else if (response.success === false) {
                                FwFunc.playErrorSound();
                                errorMsg.html(`<div><span>${response.msg}</span></div>`);
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