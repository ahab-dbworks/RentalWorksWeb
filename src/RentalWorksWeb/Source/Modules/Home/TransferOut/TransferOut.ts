routes.push({ pattern: /^module\/transferout$/, action: function (match: RegExpExecArray) { return TransferOutController.getModuleScreen(); } });

class TransferOut extends StagingCheckoutBase{
    Module: string = 'TransferOut';
    caption: string = 'Transfer Out';
    nav: string = 'module/transferout';
    id: string = '91E79272-C1CF-4678-A28F-B716907D060C';
    showAddItemToOrder: boolean = false;
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    contractId: string;
    isPendingItemGridView: boolean = false;
    Type: string = 'Transfer';
}
var TransferOutController = new TransferOut();