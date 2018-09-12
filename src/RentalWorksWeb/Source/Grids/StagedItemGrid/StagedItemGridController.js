class StagedItemGrid {
    constructor() {
        this.Module = 'StagedItemGrid';
        this.apiurl = 'api/v1/stageditem';
    }
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            $tr.dblclick(() => {
                moveStagedItemToOut($control, $tr);
            });
        });
        function moveStagedItemToOut($control, $tr) {
            let $form, $stagedItemGrid, $checkedOutItemGrid, barCode, iCode, orderItemId, vendorId, request = {};
            $form = $control.closest('.fwform');
            $form.find('.right-arrow').addClass('arrow-clicked');
            $form.find('.left-arrow').removeClass('arrow-clicked');
            $checkedOutItemGrid = $form.find('[data-name="CheckedOutItemGrid"]');
            $stagedItemGrid = $form.find('[data-name="StagedItemGrid"]');
            barCode = $tr.find('[data-formdatafield="BarCode"]').attr('data-originalvalue');
            iCode = $tr.find('[data-formdatafield="ICode"]').attr('data-originalvalue');
            orderItemId = $tr.find('[data-formdatafield="OrderItemId"]').attr('data-originalvalue');
            vendorId = $tr.find('[data-formdatafield="VendorId"]').attr('data-originalvalue');
            request.OrderId = $tr.find('[data-formdatafield="OrderId"]').attr('data-originalvalue');
            request.Quantity = +$tr.find('[data-formdatafield="Quantity"]').attr('data-originalvalue');
            request.ContractId = $control.data('ContractId');
            if (barCode !== '') {
                request.Code = barCode;
            }
            else {
                request.Code = iCode;
                request.OrderItemId = orderItemId;
                request.VendorId = vendorId;
            }
            if (typeof $control.data('ContractId') !== 'undefined') {
                FwAppData.apiMethod(true, 'POST', `api/v1/checkout/movestageditemtoout`, request, FwServices.defaultTimeout, response => {
                    FwBrowse.search($checkedOutItemGrid);
                    FwBrowse.search($stagedItemGrid);
                }, function onError(response) {
                    FwFunc.showError(response);
                }, null);
                $form.find('.partial-contract-barcode input').val('');
                $form.find('.partial-contract-quantity input').val('');
                $form.find('.partial-contract-barcode input').focus();
            }
        }
    }
    ;
}
var StagedItemGridController = new StagedItemGrid();
//# sourceMappingURL=StagedItemGridController.js.map