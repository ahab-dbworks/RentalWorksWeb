class OrderStatus extends OrderStatusBase {
    constructor() {
        super(...arguments);
        this.Module = 'OrderStatus';
        this.apiurl = 'api/v1/orderstatus';
        this.caption = Constants.Modules.Warehouse.children.OrderStatus.caption;
        this.nav = Constants.Modules.Warehouse.children.OrderStatus.nav;
        this.id = Constants.Modules.Warehouse.children.OrderStatus.id;
        this.Type = 'Order';
    }
    addFormMenuItems(options) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
}
var OrderStatusController = new OrderStatus();
//# sourceMappingURL=OrderStatus.js.map