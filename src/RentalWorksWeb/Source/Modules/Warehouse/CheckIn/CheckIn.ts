//routes.push({ pattern: /^module\/checkin$/, action: function (match: RegExpExecArray) { return CheckInController.getModuleScreen(); } });

class CheckIn extends CheckInBase {
    Module:                    string = 'CheckIn';
    apiurl:                    string = 'api/v1/checkin'
    caption:                   string = Constants.Modules.Warehouse.children.CheckIn.caption;
    nav:                       string = Constants.Modules.Warehouse.children.CheckIn.nav;
    id:                        string = Constants.Modules.Warehouse.children.CheckIn.id;
    Type:                      string;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Cancel Check In', '', (e: JQuery.ClickEvent) => {
            try {
                this.cancelCheckIn(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    cancelCheckIn($form: JQuery): void {
        try {
            let me           = this;
            const contractId = FwFormField.getValueByDataField($form, 'ContractId');
            if (contractId != '') {
                const $confirmation = FwConfirmation.renderConfirmation('Cancel Check-In', 'Cancelling this Check-In Session will cause all transacted items to be cancelled. Continue?');
                const $yes          = FwConfirmation.addButton($confirmation, 'Yes', false);
                const $no           = FwConfirmation.addButton($confirmation, 'No', true);

                $yes.on('click', () => {
                    try {
                        const request: any = { ContractId: contractId };
                        FwAppData.apiMethod(true, 'POST', `api/v1/contract/cancelcontract`, request, FwServices.defaultTimeout,
                            response => {
                                FwConfirmation.destroyConfirmation($confirmation);
                                me.resetForm($form);
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
    //beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {

    //    switch (datafield) {
    //        case 'OrderId':
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorder`);
    //            break;
    //        case 'DealId':
    //            $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
    //            break;
    //    };
    //}
}

var CheckInController = new CheckIn();