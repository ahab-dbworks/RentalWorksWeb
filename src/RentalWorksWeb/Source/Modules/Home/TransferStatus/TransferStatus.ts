routes.push({ pattern: /^module\/transferstatus$/, action: function (match: RegExpExecArray) { return TransferStatusController.getModuleScreen(); } });

class TransferStatus extends OrderStatusBase {
    Module: string = 'TransferStatus';
    caption: string = Constants.Modules.Home.TransferStatus.caption;
	nav: string = Constants.Modules.Home.TransferStatus.nav;
	id: string = Constants.Modules.Home.TransferStatus.id;
    Type: string = 'Transfer';
}
var TransferStatusController = new TransferStatus();