class VehicleMake {
    Module: string = 'VehicleMake';
    apiurl: string = 'api/v1/vehiclemake';
    caption: string = Constants.Modules.Settings.children.VehicleSettings.children.VehicleMake.caption;
    nav: string = Constants.Modules.Settings.children.VehicleSettings.children.VehicleMake.nav;
    id: string = Constants.Modules.Settings.children.VehicleSettings.children.VehicleMake.id;

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Vehicle Make', false, 'BROWSE', true);
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
        $form.find('div.fwformfield[data-datafield="VehicleMakeId"] input').val(uniqueids.VehicleMakeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid:         'VehicleMakeModelGrid',
            gridSecurityId:   'kPPx0KctQjlXx',
            moduleSecurityId: this.id,
            $form:            $form,
            //addGridMenu: (options: IAddGridMenuOptions) => {
            //    options.hasNew    = false;
            //    options.hasEdit   = false;
            //    options.hasDelete = false;
            //},
            onDataBind: (request: any) => {
                request.uniqueids = {
                    VehicleMakeId: FwFormField.getValueByDataField($form, 'VehicleMakeId')
                };
            },
            beforeSave: (request: any) => {
                request.VehicleMakeId = FwFormField.getValueByDataField($form, 'VehicleMakeId');
            }
        });
    }

    afterLoad($form: any) {
        const $vehicleMakeModelGrid = $form.find('[data-name="VehicleMakeModelGrid"]');
        FwBrowse.search($vehicleMakeModelGrid);
    }
}

var VehicleMakeController = new VehicleMake();