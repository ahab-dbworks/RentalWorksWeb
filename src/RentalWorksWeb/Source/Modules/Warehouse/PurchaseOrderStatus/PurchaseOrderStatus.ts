routes.push({ pattern: /^module\/purchaseorderstatus$/, action: function (match: RegExpExecArray) { return PurchaseOrderStatusController.getModuleScreen(); } });

class PurchaseOrderStatus extends OrderStatusBase {
    Module:  string = 'PurchaseOrderStatus';
    apiurl:  string = 'api/v1/purchaseorderstatus'
    caption: string = Constants.Modules.Warehouse.children.PurchaseOrderStatus.caption;
    nav:     string = Constants.Modules.Warehouse.children.PurchaseOrderStatus.nav;
    id:      string = Constants.Modules.Warehouse.children.PurchaseOrderStatus.id;
    Type:    string = 'PurchaseOrder';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
}
var PurchaseOrderStatusController = new PurchaseOrderStatus();