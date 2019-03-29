class StagingCheckout extends StagingCheckoutBase{
    Module: string = 'StagingCheckout';
    caption: string = 'Staging / Check-Out';
    nav: string = 'module/checkout';
    id: string = 'C3B5EEC9-3654-4660-AD28-20DE8FF9044D';
    showAddItemToOrder: boolean = false;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;
    isPendingItemGridView: boolean = false;
    Type: string = 'Order';
}
var StagingCheckoutController = new StagingCheckout();