class OrderStatusSalesDetailGrid {
    constructor() {
        this.Module = 'OrderStatusSalesDetailGrid';
        this.apiurl = 'api/v1/orderstatusdetail';
    }
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr, dt, rowIndex) => {
            let isSuspendIn = $tr.find('[data-browsedatafield="IsSuspendIn"]').attr('data-originalvalue');
            let isSuspendOut = $tr.find('[data-browsedatafield="IsSuspendOut"]').attr('data-originalvalue');
            if (isSuspendIn === 'true') {
                $tr.find('[data-browsedatafield="InContractId"] div.btnpeek').hide();
            }
            if (isSuspendOut === 'true') {
                $tr.find('[data-browsedatafield="OutContractId"] div.btnpeek').hide();
            }
        });
    }
}
var OrderStatusSalesDetailGridController = new OrderStatusSalesDetailGrid();
//# sourceMappingURL=OrderStatusSalesDetailGridController.js.map