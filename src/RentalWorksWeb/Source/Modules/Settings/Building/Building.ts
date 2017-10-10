declare var FwModule: any;
declare var FwBrowse: any;

class RwBuilding {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Building';
        this.apiurl = 'api/v1/building';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Building', false, 'BROWSE', true);
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
     
    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="BuildingId"] input').val(uniqueids.BuildingId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="BuildingId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $floorGrid: any;
        var $floorGridControl: any;
       
        $floorGrid = $form.find('div[data-grid="FloorGrid"]');
        $floorGridControl = jQuery(jQuery('#tmpl-grids-FloorGridBrowse').html());
        $floorGrid.empty().append($floorGridControl);
        $floorGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                BuildingId: $form.find('div.fwformfield[data-datafield="BuildingId"] input').val()
            };
        })
        FwBrowse.init($floorGridControl);
        FwBrowse.renderRuntimeHtml($floorGridControl);

    }

    afterLoad($form: any) {
        var $floorGrid: any;

        $floorGrid = $form.find('[data-name="FloorGrid"]');
        FwBrowse.search($floorGrid);

    }

 
}

(<any>window).BuildingController = new RwBuilding();