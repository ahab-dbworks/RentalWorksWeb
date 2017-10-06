declare var FwModule: any;
declare var FwBrowse: any;

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
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    renderGrids($form: any) {
        var $vehicleTypeWarehouseGrid, $vehicleTypeWarehouseControl;        

        // load vendornote Grid
        $vehicleTypeWarehouseGrid = $form.find('div[data-grid="VehicleTypeWarehouseGrid"]');
        $vehicleTypeWarehouseControl = jQuery(jQuery('#tmpl-grids-VehicleTypeWarehouseGridBrowse').html());
        $vehicleTypeWarehouseGrid.empty().append($vehicleTypeWarehouseControl);
        $vehicleTypeWarehouseControl.data('ondatabind', function (request) {
            request.uniqueids = {
                VehicleTypeId: $form.find('div.fwformfield[data-datafield="VehicleTypeId"] input').val()
            }
        });
        FwBrowse.init($vehicleTypeWarehouseControl);
        FwBrowse.renderRuntimeHtml($vehicleTypeWarehouseControl);

    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
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

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="VehicleTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $vehicleTypeWarehouseGrid;

        $vehicleTypeWarehouseGrid = $form.find('[data-name="VehicleTypeWarehouseGrid"]');
        FwBrowse.search($vehicleTypeWarehouseGrid);
    }
}

(<any>window).VehicleTypeController = new VehicleType();