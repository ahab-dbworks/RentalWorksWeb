class RwSapVendorInvoiceStatus {
    Module: string = 'SapVendorInvoiceStatus';
    apiurl: string = 'api/v1/sapvendorinvoicestatus';
    caption: string = Constants.Modules.Settings.children.VendorSettings.children.SapVendorInvoiceStatus.caption;
    nav: string = Constants.Modules.Settings.children.VendorSettings.children.SapVendorInvoiceStatus.nav;
    id: string = Constants.Modules.Settings.children.VendorSettings.children.SapVendorInvoiceStatus.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'SAP Vendor Invoice Status', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="SapVendorInvoiceStatusId"] input').val(uniqueids.SapVendorInvoiceStatusId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
    }
}

var SapVendorInvoiceStatusController = new RwSapVendorInvoiceStatus();