class StagingCheckout extends StagingCheckoutBase {
    Module:                    string  = 'StagingCheckout';
    apiurl:                    string  = 'api/v1/checkout';
    caption:                   string  = Constants.Modules.Warehouse.children.StagingCheckout.caption;
    nav:                       string  = Constants.Modules.Warehouse.children.StagingCheckout.nav;
    id:                        string  = Constants.Modules.Warehouse.children.StagingCheckout.id;
    showAddItemToOrder:        boolean = false;
    successSoundFileName:      string;
    errorSoundFileName:        string;
    notificationSoundFileName: string;
    contractId:                string;
    isPendingItemGridView:     boolean = false;
    Type:                      string  = 'Order';
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Staging / Check-Out', '', (e: JQuery.ClickEvent) => {
            try {
                this.CancelCheckOut(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    CancelCheckOut($form: JQuery): void {
        try {
            const contractId    = StagingCheckoutController.contractId;
            if (contractId != '') {
                const $confirmation = FwConfirmation.renderConfirmation('Cancel Staging/Check-Out', 'Cancelling this Staging/Check-Out Session will cause all transacted items to be cancelled.  Continue?');
                const $yes          = FwConfirmation.addButton($confirmation, 'Yes', false);
                const $no           = FwConfirmation.addButton($confirmation, 'No', true);

                $yes.on('click', () => {
                    try {
                        const request: any = {};
                        request.ContractId = contractId;
                        FwAppData.apiMethod(true, 'POST', `api/v1/contract/cancelcontract`, request, FwServices.defaultTimeout,
                            response => {
                                FwConfirmation.destroyConfirmation($confirmation);
                                StagingCheckoutController.resetForm($form);
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
}
var StagingCheckoutController = new StagingCheckout();