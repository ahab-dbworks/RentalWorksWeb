declare var FwModule: any;
declare var FwBrowse: any;

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
    $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val(uniqueids.PresentationLayerId);
    FwModule.loadForm(this.Module, $form);

        return $form;
    }

saveForm($form: any, closetab: boolean, navigationpath: string)
    {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

loadAudit($form: any)
    {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

renderGrids($form: any) {
    var $presentationLayerActivityGrid: any;
    var $presentationLayerActivityGridControl: any;

    // load AttributeValue Grid
    $presentationLayerActivityGrid = $form.find('div[data-grid="PresentationLayerActivityGrid"]');
    $presentationLayerActivityGridControl = jQuery(jQuery('#tmpl-grids-PresentationLayerActivityGridBrowse').html());
    $presentationLayerActivityGrid.empty().append($presentationLayerActivityGridControl);
    $presentationLayerActivityGridControl.data('ondatabind', function (request) {
        request.uniqueids = {
            InventoryAttributeId: $form.find('div.fwformfield[data-datafield="PresentationLayerId"] input').val()
        };
    });
    FwBrowse.init($presentationLayerActivityGridControl);
    FwBrowse.renderRuntimeHtml($presentationLayerActivityGridControl);
}

afterLoad($form: any) {
    var $presentationLayerActivityGrid: any;

    $presentationLayerActivityGrid = $form.find('[data-name="PresentationLayerActivityGrid"]');
    FwBrowse.search($presentationLayerActivityGrid);

    }
}

(<any>window).PresentationLayerController = new PresentationLayer();