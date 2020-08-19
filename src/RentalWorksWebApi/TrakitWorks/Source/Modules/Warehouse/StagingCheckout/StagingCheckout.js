class StagingCheckout extends StagingCheckoutBase {
    constructor() {
        super(...arguments);
        this.Module = 'StagingCheckout';
        this.apiurl = 'api/v1/checkout';
        this.caption = Constants.Modules.Warehouse.children.StagingCheckout.caption;
        this.nav = Constants.Modules.Warehouse.children.StagingCheckout.nav;
        this.id = Constants.Modules.Warehouse.children.StagingCheckout.id;
        this.showAddItemToOrder = false;
        this.isPendingItemGridView = false;
        this.Type = 'Order';
    }
    addFormMenuItems(options) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Staging / Check-Out', '', (e) => {
            try {
                this.CancelCheckOut(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    CancelCheckOut($form) {
        try {
            const contractId = StagingCheckoutController.contractId;
            if (contractId != '') {
                const $confirmation = FwConfirmation.renderConfirmation('Cancel Staging/Check-Out', 'Cancelling this Staging/Check-Out Session will cause all transacted items to be cancelled.  Continue?');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                const $no = FwConfirmation.addButton($confirmation, 'No', true);
                $yes.on('click', () => {
                    try {
                        const request = {};
                        request.ContractId = contractId;
                        FwAppData.apiMethod(true, 'POST', `api/v1/contract/cancelcontract`, request, FwServices.defaultTimeout, response => {
                            FwConfirmation.destroyConfirmation($confirmation);
                            StagingCheckoutController.resetForm($form);
                            FwNotification.renderNotification('SUCCESS', 'Session succesfully cancelled.');
                        }, ex => FwFunc.showError(ex), $confirmation.find('.fwconfirmationbox'));
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            }
        }
        catch (ex) {
            FwFunc.showError(ex);
        }
    }
}
var StagingCheckoutController = new StagingCheckout();
//# sourceMappingURL=StagingCheckout.js.map