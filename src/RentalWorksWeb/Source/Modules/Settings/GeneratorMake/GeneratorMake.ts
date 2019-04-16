class GeneratorMake {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'GeneratorMake';
        this.apiurl = 'api/v1/generatormake';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Generator Make', false, 'BROWSE', true);
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
        $form.find('div.fwformfield[data-datafield="GeneratorMakeId"] input').val(uniqueids.GeneratorMakeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="GeneratorMakeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        const $generatorMakeModelGrid = $form.find('div[data-grid="GeneratorMakeModelGrid"]');
        const $generatorMakeModelGridControl = FwBrowse.loadGridFromTemplate('GeneratorMakeModelGrid');
        $generatorMakeModelGrid.empty().append($generatorMakeModelGridControl);
        $generatorMakeModelGridControl.data('ondatabind', request => {
            request.uniqueids = {
                GeneratorMakeId: FwFormField.getValueByDataField($form, 'GeneratorMakeId')
            };
        });
        $generatorMakeModelGridControl.data('beforesave', request => {
            request.GeneratorMakeId = FwFormField.getValueByDataField($form, 'GeneratorMakeId')
        });
        FwBrowse.init($generatorMakeModelGridControl);
        FwBrowse.renderRuntimeHtml($generatorMakeModelGridControl);
    }

    afterLoad($form: any) {
        const $generatorMakeModelGrid = $form.find('[data-name="GeneratorMakeModelGrid"]');
        FwBrowse.search($generatorMakeModelGrid);
    }
}

var GeneratorMakeController = new GeneratorMake();