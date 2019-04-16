class VehicleType {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'VehicleType';
        this.apiurl = 'api/v1/vehicletype';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Vehicle Type', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    disableFields(): void {
        jQuery('.disablefield').attr('data-required', 'false');
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    renderGrids($form: any) {
        const $vehicleTypeWarehouseGrid = $form.find('div[data-grid="VehicleTypeWarehouseGrid"]');
        const $vehicleTypeWarehouseControl = FwBrowse.loadGridFromTemplate('VehicleTypeWarehouseGrid');
        $vehicleTypeWarehouseGrid.empty().append($vehicleTypeWarehouseControl);
        $vehicleTypeWarehouseControl.data('ondatabind', request => {
            request.uniqueids = {
                VehicleTypeId: FwFormField.getValueByDataField($form, 'VehicleTypeId')
            }
        });
        FwBrowse.init($vehicleTypeWarehouseControl);
        FwBrowse.renderRuntimeHtml($vehicleTypeWarehouseControl);
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
        $form.find('div.fwformfield[data-datafield="VehicleTypeId"] input').val(uniqueids.VehicleTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="VehicleTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        const $vehicleTypeWarehouseGrid = $form.find('[data-name="VehicleTypeWarehouseGrid"]');
        FwBrowse.search($vehicleTypeWarehouseGrid);

        this.disableFields();
    }

    beforeValidate = function ($browse, $grid, request) {
        request.uniqueids = {
            Transportation: true
        };
    }
}

var VehicleTypeController = new VehicleType();