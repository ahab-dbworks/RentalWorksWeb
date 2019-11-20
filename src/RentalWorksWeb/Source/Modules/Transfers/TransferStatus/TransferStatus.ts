routes.push({ pattern: /^module\/transferstatus$/, action: function (match: RegExpExecArray) { return TransferStatusController.getModuleScreen(); } });

class TransferStatus extends OrderStatusBase {
    Module:  string = 'TransferStatus';
    apiurl:  string = 'api/v1/transferstatus'
    caption: string = Constants.Modules.Transfers.children.TransferStatus.caption;
    nav:     string = Constants.Modules.Transfers.children.TransferStatus.nav;
    id:      string = Constants.Modules.Transfers.children.TransferStatus.id;
    Type:    string = 'Transfer';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
}
var TransferStatusController = new TransferStatus();