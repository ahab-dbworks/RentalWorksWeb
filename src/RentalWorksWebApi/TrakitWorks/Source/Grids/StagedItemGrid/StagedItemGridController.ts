class StagedItemGrid {
    Module: string = 'StagedItemGrid';
    apiurl: string = 'api/v1/stageditem';
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        //----------------------------------------------------------------------------------------------
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            $tr.dblclick(() => {
                moveStagedItemToOut($control, $tr);
            })
        });
        //----------------------------------------------------------------------------------------------
        function moveStagedItemToOut($control, $tr) {
            const $form = $control.closest('.fwform');
            $form.find('.right-arrow').addClass('btn-active');
            $form.find('.left-arrow').removeClass('btn-active');

            const barCode = $tr.find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
            const iCode = $tr.find('[data-formdatafield="ICode"]').attr('data-originalvalue');
            const orderItemId = $tr.find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            const vendorId = $tr.find('[data-formdatafield="VendorId"]').attr('data-originalvalue');
            const request: any = {}
            request.OrderId = $tr.find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
            request.Quantity = +$tr.find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
            request.ContractId = $control.data('ContractId');

            if (barCode !== '') {
                request.Code = barCode;
            } else {
                request.Code = iCode;
                request.OrderItemId = orderItemId;
                request.VendorId = vendorId;
            }

            if (typeof $control.data('ContractId') !== 'undefined') {
                FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
                    const $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
                    FwBrowse.search($checkedOutItemGrid);
                    const $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
                    FwBrowse.search($stagedItemGrid);
                }, function onError(response) {
                    FwFunc.showError(response);
                }, null);
                $form.find('.partial-contract-barcode input').val('');
                $form.find('.partial-contract-quantity input').val('');
                $form.find('.partial-contract-barcode input').focus();
            }
        }
    };
}
//----------------------------------------------------------------------------------------------

var StagedItemGridController = new StagedItemGrid();