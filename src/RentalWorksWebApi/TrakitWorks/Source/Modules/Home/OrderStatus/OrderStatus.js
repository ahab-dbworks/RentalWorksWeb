class OrderStatus extends OrderStatusBase {
    constructor() {
        super(...arguments);
        this.Module = 'OrderStatus';
        this.caption = Constants.Modules.Home.OrderStatus.caption;
        this.nav = Constants.Modules.Home.OrderStatus.nav;
        this.id = Constants.Modules.Home.OrderStatus.id;
        this.Type = 'Order';
    }
}
var OrderStatusController = new OrderStatus();
//# sourceMappingURL=OrderStatus.js.map