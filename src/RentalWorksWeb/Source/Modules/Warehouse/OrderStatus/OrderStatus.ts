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

        FwMenu.addSubMenuItem(options.$groupOptions, 'Print Order Status Summary', 'Y79RJy4CGwnD', (e: JQuery.ClickEvent) => {
            try {
                this.printOrderStatusSummary(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
}
var OrderStatusController = new OrderStatus();