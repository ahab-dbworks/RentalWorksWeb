class OrderSnapshotGrid {
    Module: string = 'OrderSnapshotGrid';
    apiurl: string = 'api/v1/ordersnapshot';
}
//---------------------------------------------------------------------------------
// "View Snapshot" grid hamburger menu item
FwApplicationTree.clickEvents['{C6633D9A-3800-41F2-8747-BC780663E22F}'] = function (event) {
    let $form, $orderForm, $selectedCheckBoxes, $orderSnapshotGrid, snapshotId;

    $form = jQuery(this).closest('.fwform');
    $orderSnapshotGrid = $form.find(`[data-name="OrderSnapshotGrid"]`);
    $selectedCheckBoxes = $orderSnapshotGrid.find('.cbselectrow:checked');

    try {
        if ($selectedCheckBoxes.length !== 0) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                snapshotId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="SnapshotId"]').attr('data-originalvalue');
                var orderInfo: any = {};
                orderInfo.OrderId = snapshotId;
                $orderForm = OrderController.openForm('EDIT', orderInfo);
                FwModule.openSubModuleTab($form, $orderForm);
                jQuery('.tab.submodule.active').find('.caption').html(`${snapshotId}`);
            }
        } else {
            FwNotification.renderNotification('WARNING', 'Select rows in Order Snapshot Grid in order to perform this function.');
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};

var OrderSnapshotGridController = new OrderSnapshotGrid();
//----------------------------------------------------------------------------------------------