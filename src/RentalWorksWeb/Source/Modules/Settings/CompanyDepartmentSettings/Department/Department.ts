class Department {
    Module: string = 'Department';
    apiurl: string = 'api/v1/department';
    caption: string = Constants.Modules.Settings.children.DepartmentSettings.children.Department.caption;
    nav: string = Constants.Modules.Settings.children.DepartmentSettings.children.Department.nav;
    id: string = Constants.Modules.Settings.children.DepartmentSettings.children.Department.id;
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

        if (mode === 'NEW') {
            FwFormField.setValueByDataField($form, 'SalesBillingRule', 'BILLINGDATE');
        }

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DepartmentId"] input').val(uniqueids.DepartmentId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------------------
    renderGrids($form) {
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'DepartmentInventoryTypeGrid',
            gridSecurityId: 'TEiHWtIOkGrX0',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DepartmentId: FwFormField.getValueByDataField($form, 'DepartmentId')
                };
            }
        });
    }
    //---------------------------------------------------------------------------------------------
    afterLoad($form: any) {
    }
    //---------------------------------------------------------------------------------------------
}

var DepartmentController = new Department();