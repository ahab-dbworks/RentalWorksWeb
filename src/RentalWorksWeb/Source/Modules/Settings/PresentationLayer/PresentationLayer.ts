class PresentationLayer {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'PresentationLayer';
        this.apiurl = 'api/v1/presentationlayer';
    }

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

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        const $presentationLayerActivityGrid = $form.find('div[data-grid="PresentationLayerActivityGrid"]');
        const $presentationLayerActivityGridControl = FwBrowse.loadGridFromTemplate('PresentationLayerActivityGrid');
        $presentationLayerActivityGrid.empty().append($presentationLayerActivityGridControl);
        $presentationLayerActivityGridControl.data('ondatabind', request => {
            request.uniqueids = {
                PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
            };
        });
        $presentationLayerActivityGridControl.data('beforesave', request => {
            request.PresentationLayerId = FwFormField.getValueByDataField($form, 'PresentationLayerId');
        });
        FwBrowse.init($presentationLayerActivityGridControl);
        FwBrowse.renderRuntimeHtml($presentationLayerActivityGridControl);

        const $presentationLayerActivityOverrideGrid = $form.find('div[data-grid="PresentationLayerActivityOverrideGrid"]');
        const $presentationLayerActivityOverrideGridControl = FwBrowse.loadGridFromTemplate('PresentationLayerActivityOverrideGrid');
        $presentationLayerActivityOverrideGrid.empty().append($presentationLayerActivityOverrideGridControl);
        $presentationLayerActivityOverrideGridControl.data('ondatabind', request => {
            request.uniqueids = {
                PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
            };
        });
        $presentationLayerActivityOverrideGridControl.data('beforesave', request => {
            request.PresentationLayerId = FwFormField.getValueByDataField($form, 'PresentationLayerId');
        });
        FwBrowse.init($presentationLayerActivityOverrideGridControl);
        FwBrowse.renderRuntimeHtml($presentationLayerActivityOverrideGridControl);

        const $presentationLayerFormGrid = $form.find('div[data-grid="PresentationLayerFormGrid"]');
        const $presentationLayerFormGridControl = FwBrowse.loadGridFromTemplate('PresentationLayerFormGrid');
        $presentationLayerFormGrid.empty().append($presentationLayerFormGridControl);
        $presentationLayerFormGridControl.data('ondatabind', request => {
            request.uniqueids = {
                PresentationLayerId: FwFormField.getValueByDataField($form, 'PresentationLayerId')
            };
        });
        FwBrowse.init($presentationLayerFormGridControl);
        FwBrowse.renderRuntimeHtml($presentationLayerFormGridControl);
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