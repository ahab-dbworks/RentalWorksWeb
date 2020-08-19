class OrderStatus extends OrderStatusBase {
    Module:  string = 'OrderStatus';
    apiurl:  string = 'api/v1/orderstatus'
    caption: string = Constants.Modules.Warehouse.children.OrderStatus.caption;
    nav:     string = Constants.Modules.Warehouse.children.OrderStatus.nav;
    id:      string = Constants.Modules.Warehouse.children.OrderStatus.id;
    Type:    string = 'Order';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
}
var OrderStatusController = new OrderStatus();