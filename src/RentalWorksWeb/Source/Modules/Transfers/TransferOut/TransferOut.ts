routes.push({ pattern: /^module\/transferout$/, action: function (match: RegExpExecArray) { return TransferOutController.getModuleScreen(); } });

class TransferOut extends StagingCheckoutBase {
    Module:                    string  = 'TransferOut';
    apiurl:                    string  = 'api/v1/transferout'
    caption:                   string  = Constants.Modules.Transfers.children.TransferOut.caption;
    nav:                       string  = Constants.Modules.Transfers.children.TransferOut.nav;
    id:                        string  = Constants.Modules.Transfers.children.TransferOut.id;
    showAddItemToOrder:        boolean = false;
    isPendingItemGridView:     boolean = false;
    Type:                      string  = 'Transfer';
    contractId:                string;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
}

var TransferOutController = new TransferOut();