routes.push({ pattern: /^module\/transferin$/, action: function (match: RegExpExecArray) { return TransferInController.getModuleScreen(); } });

class TransferIn extends CheckInBase {
    Module:                    string = 'TransferIn';
    apiurl:                    string = 'api/v1/transferin'
    caption:                   string = Constants.Modules.Transfers.children.TransferIn.caption;
    nav:                       string = Constants.Modules.Transfers.children.TransferIn.nav;
    id:                        string = Constants.Modules.Transfers.children.TransferIn.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateOrder($browse: any, $form: any, request: any) {
        const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
        request.miscfields = {
            TransferIn: true,
            TransferInWarehouseId: warehouseId
        }
    }
    //----------------------------------------------------------------------------------------------
}

var TransferInController = new TransferIn();