class StagingCheckout extends StagingCheckoutBase {
    constructor() {
        super(...arguments);
        this.Module = 'StagingCheckout';
        this.caption = Constants.Modules.Home.StagingCheckout.caption;
        this.nav = Constants.Modules.Home.StagingCheckout.nav;
        this.id = Constants.Modules.Home.StagingCheckout.id;
        this.showAddItemToOrder = false;
        this.isPendingItemGridView = false;
        this.Type = 'Order';
    }
}
var StagingCheckoutController = new StagingCheckout();
//# sourceMappingURL=StagingCheckout.js.map