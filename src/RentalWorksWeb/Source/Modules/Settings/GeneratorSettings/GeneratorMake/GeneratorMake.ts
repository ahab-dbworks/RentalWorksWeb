class GeneratorMake {
    Module: string = 'GeneratorMake';
    apiurl: string = 'api/v1/generatormake';
    caption: string = Constants.Modules.Settings.children.GeneratorSettings.children.GeneratorMake.caption;
    nav: string = Constants.Modules.Settings.children.GeneratorSettings.children.GeneratorMake.nav;
    id: string = Constants.Modules.Settings.children.GeneratorSettings.children.GeneratorMake.id;
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
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="GeneratorMakeId"] input').val(uniqueids.GeneratorMakeId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
        // Generator Make Model Grid
        //const $generatorMakeModelGrid = $form.find('div[data-grid="GeneratorMakeModelGrid"]');
        //const $generatorMakeModelGridControl = FwBrowse.loadGridFromTemplate('GeneratorMakeModelGrid');
        //$generatorMakeModelGrid.empty().append($generatorMakeModelGridControl);
        //$generatorMakeModelGridControl.data('ondatabind', request => {
        //    request.uniqueids = {
        //        GeneratorMakeId: FwFormField.getValueByDataField($form, 'GeneratorMakeId')
        //    };
        //});
        //$generatorMakeModelGridControl.data('beforesave', request => {
        //    request.GeneratorMakeId = FwFormField.getValueByDataField($form, 'GeneratorMakeId')
        //});
        //FwBrowse.init($generatorMakeModelGridControl);
        //FwBrowse.renderRuntimeHtml($generatorMakeModelGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'GeneratorMakeModelGrid',
            gridSecurityId: 'CFnh3uxNiWZy',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    GeneratorMakeId: FwFormField.getValueByDataField($form, 'GeneratorMakeId'),
                };
            },
            beforeSave: (request: any) => {
                request.GeneratorMakeId = FwFormField.getValueByDataField($form, 'GeneratorMakeId');
            },
        });
    }



    afterLoad($form: any) {
        const $generatorMakeModelGrid = $form.find('[data-name="GeneratorMakeModelGrid"]');
        FwBrowse.search($generatorMakeModelGrid);
    }
}

var GeneratorMakeController = new GeneratorMake();