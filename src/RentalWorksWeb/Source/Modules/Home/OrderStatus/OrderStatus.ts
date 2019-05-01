class OrderStatus extends OrderStatusBase {
    Module: string = 'OrderStatus';
    caption: string = Constants.Modules.Home.OrderStatus.caption;
	nav: string = Constants.Modules.Home.OrderStatus.nav;
	id: string = Constants.Modules.Home.OrderStatus.id;
    Type: string = 'Order';
}
var OrderStatusController = new OrderStatus();