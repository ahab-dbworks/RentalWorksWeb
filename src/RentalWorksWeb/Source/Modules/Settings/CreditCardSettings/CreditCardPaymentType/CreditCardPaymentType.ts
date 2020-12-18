class CreditCardPaymentType {
    Module: string = 'CreditCardPaymentType';
    apiurl: string = 'api/v1/creditcardpaymenttype';
    caption: string = Constants.Modules.Settings.children.CreditCardSettings.children.CreditCardPaymentType.caption;
    nav: string = Constants.Modules.Settings.children.CreditCardSettings.children.CreditCardPaymentType.nav;
    id: string = Constants.Modules.Settings.children.CreditCardSettings.children.CreditCardPaymentType.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;
        $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'CreditCardPaymentTypeId', uniqueids.CreditCardPaymentTypeId);

        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
    }
    //--------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasInactive = false;
        options.hasNew = false;
        options.hasEdit = true;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ChargePaymentTypeId':
                request.filterfields = {
                    PaymentTypeType: 'CREDIT CARD' 
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymenttype`);
                break;
            case 'RefundPaymentTypeId':
                request.filterfields = {
                    PaymentTypeType: 'REFUND CHECK'
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymenttype`);
        }
    }
    //----------------------------------------------------------------------------------------------
}
var CreditCardPaymentTypeController = new CreditCardPaymentType();