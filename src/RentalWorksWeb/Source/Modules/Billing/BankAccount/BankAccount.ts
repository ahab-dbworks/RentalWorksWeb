routes.push({ pattern: /^module\/bankaccount$/, action: function (match: RegExpExecArray) { return BankAccountController.getModuleScreen(); } });
class BankAccount {
    Module: string = 'BankAccount';
    apiurl: string = 'api/v1/bankaccount';
    caption: string = Constants.Modules.Billing.children.BankAccount.caption;
    nav: string = Constants.Modules.Billing.children.BankAccount.nav;
    id: string = Constants.Modules.Billing.children.BankAccount.id;
    ActiveViewFields: any = {};
    ActiveViewFieldsId: string;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        FwMenu.addBrowseMenuButtons(options);

        //Location Filter
        const $allLocations = FwMenu.generateDropDownViewBtn('ALL Locations', false, "ALL");
        const location = JSON.parse(sessionStorage.getItem('location'));
        const $userLocation = FwMenu.generateDropDownViewBtn(location.location, true, location.locationid);

        if (typeof this.ActiveViewFields["LocationId"] == 'undefined') {
            this.ActiveViewFields.LocationId = [location.locationid];
        }

        const viewLocation = [];
        viewLocation.push($userLocation, $allLocations);
        FwMenu.addViewBtn(options.$menu, 'Location', viewLocation, true, "LocationId");
    }
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

        $browse.data('ondatabind', request => {
            request.activeviewfields = this.ActiveViewFields;
        });


        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const office = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', office.locationid, office.location);
            FwFormField.setValue($form, 'div[data-datafield="CurrencyId"]', office.defaultcurrencyid, office.defaultcurrencycode);
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BankAccountId"] input').val(uniqueids.BankAccountId);
        FwModule.loadForm(this.Module, $form);

        const nodePayment = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Y7YC6NpLqX8kx');
        if (nodePayment !== undefined && nodePayment.properties.visible === 'T') {
            FwTabs.showTab($form.find('#paymenttab'));
            const $submodulepaymentBrowse = this.openPaymentBrowse($form);
            $form.find('.paymentSubModule').append($submodulepaymentBrowse);
        }

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {

    }
    //---------------------------------------------------------------------------------------------
    openPaymentBrowse($form) {
        const bankAccountId = FwFormField.getValueByDataField($form, 'BankAccountId');
        const $browse = PaymentController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PaymentController.ActiveViewFields;
            request.uniqueids = {
                BankAccountId: bankAccountId
            };
        });
        FwBrowse.search($browse);
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
}

var BankAccountController = new BankAccount();