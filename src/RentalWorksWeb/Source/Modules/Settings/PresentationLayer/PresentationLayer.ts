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
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
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
        var $presentationLayerActivityGrid: any;
        var $presentationLayerActivityGridControl: any;
        var $presentationLayerActivityOverrideGrid: any;
        var $presentationLayerActivityOverrideGridControl: any;
        var $presentationLayerFormGrid: any;
        var $presentationLayerFormGridControl: any;

        // load presentationlayeractivity Grid
        $presentationLayerActivityGrid = $form.find('div[data-grid="PresentationLayerActivityGrid"]');
        $presentationLayerActivityGridControl = jQuery(jQuery('#tmpl-grids-PresentationLayerActivityGridBrowse').html());
        $presentationLayerActivityGrid.empty().append($presentationLayerActivityGridControl);
        $presentationLayerActivityGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PresentationLayerId: $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val()
            };
        });
        $presentationLayerActivityGridControl.data('beforesave', function (request) {
            request.PresentationLayerId = FwFormField.getValueByDataField($form, 'PresentationLayerId');
        });
        FwBrowse.init($presentationLayerActivityGridControl);
        FwBrowse.renderRuntimeHtml($presentationLayerActivityGridControl);

        // load presentationlayeractivityoverride Grid
        $presentationLayerActivityOverrideGrid = $form.find('div[data-grid="PresentationLayerActivityOverrideGrid"]');
        $presentationLayerActivityOverrideGridControl = jQuery(jQuery('#tmpl-grids-PresentationLayerActivityOverrideGridBrowse').html());
        $presentationLayerActivityOverrideGrid.empty().append($presentationLayerActivityOverrideGridControl);
        $presentationLayerActivityOverrideGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PresentationLayerId: $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val()
            };
        });
        $presentationLayerActivityOverrideGridControl.data('beforesave', function (request) {
            request.PresentationLayerId = FwFormField.getValueByDataField($form, 'PresentationLayerId');
        });
        FwBrowse.init($presentationLayerActivityOverrideGridControl);
        FwBrowse.renderRuntimeHtml($presentationLayerActivityOverrideGridControl);


        // load presentationlayerform Grid
        $presentationLayerFormGrid = $form.find('div[data-grid="PresentationLayerFormGrid"]');
        $presentationLayerFormGridControl = jQuery(jQuery('#tmpl-grids-PresentationLayerFormGridBrowse').html());
        $presentationLayerFormGrid.empty().append($presentationLayerFormGridControl);
        $presentationLayerFormGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                PresentationLayerId: $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val()
            };
        });
        FwBrowse.init($presentationLayerFormGridControl);
        FwBrowse.renderRuntimeHtml($presentationLayerFormGridControl);


    }

    afterLoad($form: any) {
        var $presentationLayerActivityGrid: any;
        var $presentationLayerActivityOverrideGrid: any;
        var $presentationLayerFormGrid: any;

        $presentationLayerActivityGrid = $form.find('[data-name="PresentationLayerActivityGrid"]');
        FwBrowse.search($presentationLayerActivityGrid);

        $presentationLayerActivityOverrideGrid = $form.find('[data-name="PresentationLayerActivityOverrideGrid"]');
        FwBrowse.search($presentationLayerActivityOverrideGrid);

        $presentationLayerFormGrid = $form.find('[data-name="PresentationLayerFormGrid"]');
        FwBrowse.search($presentationLayerFormGrid);


    }
}

var PresentationLayerController = new PresentationLayer();