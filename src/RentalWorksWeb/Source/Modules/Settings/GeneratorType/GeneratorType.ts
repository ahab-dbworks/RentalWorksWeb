class GeneratorType {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'GeneratorType';
        this.apiurl = 'api/v1/generatortype';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Generator Type', false, 'BROWSE', true);
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

    disableFields(): void {
        jQuery('.disablefield').attr('data-required', 'false');
    }

    renderGrids($form: any) {
        var $generatorTypeWarehouseGrid, $generatorTypeWarehouseControl;

        $generatorTypeWarehouseGrid = $form.find('div[data-grid="GeneratorTypeWarehouseGrid"]');
        $generatorTypeWarehouseControl = jQuery(jQuery('#tmpl-grids-GeneratorTypeWarehouseGridBrowse').html());
        $generatorTypeWarehouseGrid.empty().append($generatorTypeWarehouseControl);
        $generatorTypeWarehouseControl.data('ondatabind', function (request) {
            request.uniqueids = {
                GeneratorTypeId: $form.find('div.fwformfield[data-datafield="GeneratorTypeId"] input').val()
            }
        });
        FwBrowse.init($generatorTypeWarehouseControl);
        FwBrowse.renderRuntimeHtml($generatorTypeWarehouseControl);
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
        $form.find('div.fwformfield[data-datafield="GeneratorTypeId"] input').val(uniqueids.GeneratorTypeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="GeneratorTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $generatorTypeWarehouseGrid;

        $generatorTypeWarehouseGrid = $form.find('[data-name="GeneratorTypeWarehouseGrid"]');
        FwBrowse.search($generatorTypeWarehouseGrid);

        this.disableFields();
    }
}

var GeneratorTypeController = new GeneratorType();