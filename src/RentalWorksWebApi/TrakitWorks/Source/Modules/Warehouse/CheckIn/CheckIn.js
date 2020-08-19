class CheckIn extends CheckInBase {
    constructor() {
        super(...arguments);
        this.Module = 'CheckIn';
        this.apiurl = 'api/v1/checkin';
        this.caption = Constants.Modules.Warehouse.children.CheckIn.caption;
        this.nav = Constants.Modules.Warehouse.children.CheckIn.nav;
        this.id = Constants.Modules.Warehouse.children.CheckIn.id;
    }
    addFormMenuItems(options) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Check In', '', (e) => {
            try {
                this.cancelCheckIn(options.$form);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    cancelCheckIn($form) {
        try {
            let me = this;
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId != '') {
                const $confirmation = FwConfirmation.renderConfirmation('Cancel Check-In', 'Cancelling this Check-In Session will cause all transacted items to be cancelled. Continue?');
                const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
                const $no = FwConfirmation.addButton($confirmation, 'No', true);
                $yes.on('click', () => {
                    try {
                        const request = { ContractId: contractId };
                        FwAppData.apiMethod(true, 'POST', `api/v1/contract/cancelcontract`, request, FwServices.defaultTimeout, response => {
                            FwConfirmation.destroyConfirmation($confirmation);
                            me.resetForm($form);
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
var CheckInController = new CheckIn();
//# sourceMappingURL=CheckIn.js.map