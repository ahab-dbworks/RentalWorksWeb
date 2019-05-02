class StagingCheckout extends StagingCheckoutBase{
    Module: string = 'StagingCheckout';
    caption: string = Constants.Modules.Home.StagingCheckout.caption;
	nav: string = Constants.Modules.Home.StagingCheckout.nav;
	id: string = Constants.Modules.Home.StagingCheckout.id;
    showAddItemToOrder: boolean = false;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;
    isPendingItemGridView: boolean = false;
    Type: string = 'Order';
}
var StagingCheckoutController = new StagingCheckout();