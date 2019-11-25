routes.push({ pattern: /^module\/transferout$/, action: function (match: RegExpExecArray) { return TransferOutController.getModuleScreen(); } });

class TransferOut extends StagingCheckoutBase{
    Module: string = 'TransferOut';
    caption: string = Constants.Modules.Home.TransferOut.caption;
	nav: string = Constants.Modules.Home.TransferOut.nav;
	id: string = Constants.Modules.Home.TransferOut.id;
    showAddItemToOrder: boolean = false;
    contractId: string;
    isPendingItemGridView: boolean = false;
    Type: string = 'Transfer';
}
var TransferOutController = new TransferOut();