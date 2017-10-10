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
        var $spaceGrid: any;
        var $spaceGridControl: any;
        var $spaceRateGrid: any;
        var $spaceRateGridControl: any;
       
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

        $spaceGrid = $form.find('div[data-grid="SpaceGrid"]');
        $spaceGridControl = jQuery(jQuery('#tmpl-grids-SpaceGridBrowse').html());
        $spaceGrid.empty().append($spaceGridControl);
        $spaceGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                BuildingId: $form.find('div.fwformfield[data-datafield="BuildingId"] input').val()
            };
        })
        FwBrowse.init($spaceGridControl);
        FwBrowse.renderRuntimeHtml($spaceGridControl);

        $spaceRateGrid = $form.find('div[data-grid="SpaceRateGrid"]');
        $spaceRateGridControl = jQuery(jQuery('#tmpl-grids-SpaceRateGridBrowse').html());
        $spaceRateGrid.empty().append($spaceRateGridControl);
        $spaceRateGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                SpaceId: $form.find('div.fwformfield[data-datafield="SpaceId"] input').val()
            };
        })
        FwBrowse.init($spaceRateGridControl);
        FwBrowse.renderRuntimeHtml($spaceRateGridControl);


    }

    afterLoad($form: any) {
        var $floorGrid: any;
        var $spaceGrid: any;
        var $spaceRateGrid: any;

        $floorGrid = $form.find('[data-name="FloorGrid"]');
        FwBrowse.search($floorGrid);

        $spaceGrid = $form.find('[data-name="SpaceGrid"]');
        FwBrowse.search($spaceGrid);

        $spaceRateGrid = $form.find('[data-name="SpaceRateGrid"]');
        FwBrowse.search($spaceRateGrid);


    }

 
}

(<any>window).BuildingController = new RwBuilding();