class PurchaseOrderPaymentScheduleGrid {
    Module: string = 'PurchaseOrderPaymentScheduleGrid';
    apiurl: string = 'api/v1/purchaseorderpaymentschedule';

    addLegend($control) {
        FwBrowse.addLegend($control, 'Hiatus', '#008000');
    }
}

var PurchaseOrderPaymentScheduleGridController = new PurchaseOrderPaymentScheduleGrid();
//----------------------------------------------------------------------------------------------