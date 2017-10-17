declare var FwModule: any;
declare var FwBrowse: any;

class RwAsset {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Asset';
        this.apiurl = 'api/v1/item';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Asset', false, 'BROWSE', true);
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
        $form.find('div.fwformfield[data-datafield="ItemId"] input').val(uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="ItemId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: any) {
        var $itemAttributeValueGrid: any;
        var $itemAttributeValueGridControl: any;

        $itemAttributeValueGrid = $form.find('div[data-grid="ItemAttributeValueGrid"]');
        $itemAttributeValueGridControl = jQuery(jQuery('#tmpl-grids-ItemAttributeValueGridBrowse').html());
        $itemAttributeValueGrid.empty().append($itemAttributeValueGridControl);
        $itemAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: $form.find('div.fwformfield[data-datafield="ItemId"] input').val()
            };
        })
        FwBrowse.init($itemAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($itemAttributeValueGridControl);
    
    }

    afterLoad($form: any) {
        var $itemAttributeValueGrid: any;

        $itemAttributeValueGrid = $form.find('[data-name="ItemAttributeValueGrid"]');
        FwBrowse.search($itemAttributeValueGrid);
    }
}

(<any>window).AssetController = new RwAsset();