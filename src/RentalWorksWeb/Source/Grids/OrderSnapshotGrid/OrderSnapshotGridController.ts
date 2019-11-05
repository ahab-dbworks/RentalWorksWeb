class OrderSnapshotGrid {
    Module: string = 'OrderSnapshotGrid';
    apiurl: string = 'api/v1/ordersnapshot';
}
//---------------------------------------------------------------------------------
// "View Snapshot" grid hamburger menu item
FwApplicationTree.clickEvents[Constants.Grids.OrderSnapshotGrid.menuItems.ViewSnapshot.id] = function (event: JQuery.ClickEvent) {
    let $form, $orderForm, $selectedCheckBoxes, $orderSnapshotGrid, snapshotId, orderNumber;

    $form = jQuery(this).closest('.fwform');
    $orderSnapshotGrid = $form.find(`[data-name="OrderSnapshotGrid"]`);
    $selectedCheckBoxes = $orderSnapshotGrid.find('tbody .cbselectrow:checked');

    try {
        if ($selectedCheckBoxes.length !== 0) {
            for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                snapshotId = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="SnapshotId"]').attr('data-originalvalue');
                orderNumber = $selectedCheckBoxes.eq(i).closest('tr').find('[data-formdatafield="OrderNumber"]').attr('data-originalvalue');
                var orderInfo: any = {};
                orderInfo.OrderId = snapshotId;
                $orderForm = OrderController.loadForm(orderInfo);
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
};

var OrderSnapshotGridController = new OrderSnapshotGrid();
//----------------------------------------------------------------------------------------------