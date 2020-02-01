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

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Transfer In Session', 'S8ybdjuN7MU', (e: JQuery.ClickEvent) => {
            try {
                this.cancelTransferIn(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    cancelTransferIn($form: JQuery): void {
        try {
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId != '') {
                const $confirmation = FwConfirmation.renderConfirmation('Cancel Transfer In Session', 'Cancelling this Transfer In Session will cause all transacted items to be cancelled. Continue?');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                FwConfirmation.addButton($confirmation, 'No', true);

                $yes.on('click', () => {
                    try {
                        const request: any = { ContractId: contractId };
                        FwAppData.apiMethod(true, 'POST', `${this.apiurl}/cancelcontract`, request, FwServices.defaultTimeout,
                            response => {
                                FwConfirmation.destroyConfirmation($confirmation);
                                this.resetForm($form);
                                FwNotification.renderNotification('SUCCESS', 'Session succesfully cancelled.');
                            },
                            ex => FwFunc.showError(ex),
                            $confirmation.find('.fwconfirmationbox'));
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
        //----------------------------------------------------------------------------------------------
    //beforeValidateOrder($browse: any, $form: any, request: any) {
    //    const warehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
    //    request.miscfields = {
    //        TransferIn: true,
    //        TransferInWarehouseId: warehouseId
    //    }
    //}
    //----------------------------------------------------------------------------------------------
}

var TransferInController = new TransferIn();