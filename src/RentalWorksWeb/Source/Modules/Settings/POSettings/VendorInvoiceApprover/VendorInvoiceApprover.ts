class VendorInvoiceApprover {
    Module: string = 'VendorInvoiceApprover';
    apiurl: string = 'api/v1/vendorinvoiceapprover';
    caption: string = Constants.Modules.Settings.children.POSettings.children.POType.caption;
    nav: string = Constants.Modules.Settings.children.POSettings.children.POType.nav;
    id: string = Constants.Modules.Settings.children.POSettings.children.POType.id;
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

    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="VendorInvoiceApproverId"] input').val(uniqueids.VendorInvoiceApproverId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'LocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelocation`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'UsersId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateuser`);
                break;
        }
    }
}

var VendorInvoiceApproverController = new VendorInvoiceApprover();