class OrderSnapshotGrid {
    Module: string = 'OrderSnapshotGrid';
    apiurl: string = 'api/v1/ordersnapshot';

    viewSnapshotGrid(event) {
        const $form = jQuery(event.currentTarget).closest('.fwform');
        const $orderSnapshotGrid = $form.find(`[data-name="OrderSnapshotGrid"]`);
        const $selectedCheckBoxes = $orderSnapshotGrid.find('tbody .cbselectrow:checked');

        try {
            if ($selectedCheckBoxes.length !== 0) {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    const snapshotId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="SnapshotId"]').attr('data-originalvalue');
                    const orderNumber = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderNumber"]').attr('data-originalvalue');
                    const orderInfo: any = {};
                    orderInfo.OrderId = snapshotId;
                    const $orderForm = OrderController.loadForm(orderInfo);
                    FwModule.openSubModuleTab($form, $orderForm);
                    jQuery('.tab.submodule.active').find('.caption').html(`Snapshot for Order ${orderNumber}`);
                }
            } else {
                FwNotification.renderNotification('WARNING', 'Select rows in Order Snapshot Grid in order to perform this function.');
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
}


var OrderSnapshotGridController = new OrderSnapshotGrid();
//----------------------------------------------------------------------------------------------