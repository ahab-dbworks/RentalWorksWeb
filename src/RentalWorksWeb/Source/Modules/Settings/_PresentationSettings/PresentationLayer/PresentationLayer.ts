class PresentationLayer {
    Module: string = 'PresentationLayer';
    apiurl: string = 'api/v1/presentationlayer';
    caption: string = Constants.Modules.Settings.children.PresentationSettings.children.PresentationLayer.caption;
    nav: string = Constants.Modules.Settings.children.PresentationSettings.children.PresentationLayer.nav;
    id: string = Constants.Modules.Settings.children.PresentationSettings.children.PresentationLayer.id;

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Presentation Layer', false, 'BROWSE', true);
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
        $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val(uniqueids.PresentationLayerId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    renderGrids($form: any) {
        FwBrowse.renderGrid({
            nameGrid: 'PresentationLayerActivityGrid',
            gridSecurityId: 'QiLcE27ZUg0sE',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
                };
            },
            beforeSave: (request: any) => {
                request.PresentationLayerId = FwFormField.getValueByDataField($form, 'PresentationLayerId');
            }
        });
        // -----------
        FwBrowse.renderGrid({
            nameGrid: 'PresentationLayerActivityOverrideGrid',
            gridSecurityId: 'HWjX0WDoiG79H',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
                };
            },
            beforeSave: (request: any) => {
                request.PresentationLayerId = FwFormField.getValueByDataField($form, 'PresentationLayerId');
            }
        });
        // -----------
        FwBrowse.renderGrid({
            nameGrid: 'PresentationLayerFormGrid',
            gridSecurityId: 'FcJ0Ld64KSUqv',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
                };
            }
        });
        // -----------
    }

    afterLoad($form: any) {
        const $presentationLayerActivityGrid = $form.find('[data-name="PresentationLayerActivityGrid"]');
        FwBrowse.search($presentationLayerActivityGrid);

        const $presentationLayerActivityOverrideGrid = $form.find('[data-name="PresentationLayerActivityOverrideGrid"]');
        FwBrowse.search($presentationLayerActivityOverrideGrid);

        const $presentationLayerFormGrid = $form.find('[data-name="PresentationLayerFormGrid"]');
        FwBrowse.search($presentationLayerFormGrid);
    }
}

var PresentationLayerController = new PresentationLayer();