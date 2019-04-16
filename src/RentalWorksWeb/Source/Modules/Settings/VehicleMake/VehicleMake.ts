class VehicleMake {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'VehicleMake';
        this.apiurl = 'api/v1/vehiclemake';
    }

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

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="VehicleMakeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        const $vehicleMakeModelGrid = $form.find('div[data-grid="VehicleMakeModelGrid"]');
        const $vehicleMakeModelGridControl = FwBrowse.loadGridFromTemplate('VehicleMakeModelGrid');
        $vehicleMakeModelGrid.empty().append($vehicleMakeModelGridControl);
        $vehicleMakeModelGridControl.data('ondatabind', request => {
            request.uniqueids = {
                VehicleMakeId: FwFormField.getValueByDataField($form, 'VehicleMakeId')
            };
        });
        $vehicleMakeModelGridControl.data('beforesave', request => {
            request.VehicleMakeId = FwFormField.getValueByDataField($form, 'VehicleMakeId');
        });
        FwBrowse.init($vehicleMakeModelGridControl);
        FwBrowse.renderRuntimeHtml($vehicleMakeModelGridControl);
    }

    afterLoad($form: any) {
        const $vehicleMakeModelGrid = $form.find('[data-name="VehicleMakeModelGrid"]');
        FwBrowse.search($vehicleMakeModelGrid);
    }
}

var VehicleMakeController = new VehicleMake();