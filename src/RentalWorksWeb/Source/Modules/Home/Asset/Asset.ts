declare var FwModule: any;
declare var FwBrowse: any;

class RwAsset {
    Module: string;
    apiurl: string;
    caption: string;
    nameItemAttributeValueGrid: string;
    nameItemQcGrid: string;

    constructor() {
        this.Module = 'Asset';
        this.apiurl = 'api/v1/item';
        this.caption = 'Asset';
        this.nameItemAttributeValueGrid = 'ItemAttributeValueGrid';
        this.nameItemQcGrid = 'ItemQcGrid';
    }

    getModuleScreen() {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }
     
    openForm(mode: string) {
        var $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ItemId', uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: JQuery, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: JQuery) {
        var uniqueid = FwFormField.getValueByDataField($form, 'ItemId');
        FwModule.loadAudit($form, uniqueid);
    }

    renderGrids($form: JQuery) {
        var $itemAttributeValueGrid : JQuery = $form.find('div[data-grid="' + this.nameItemAttributeValueGrid + '"]');
        var $itemAttributeValueGridControl: JQuery = FwBrowse.loadGridFromTemplate(this.nameItemAttributeValueGrid);
        $itemAttributeValueGrid.empty().append($itemAttributeValueGridControl);
        $itemAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        });
        $itemAttributeValueGridControl.data('beforesave', function (request) {
            request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');
        })
        FwBrowse.init($itemAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($itemAttributeValueGridControl);

        var $itemQcGrid: JQuery = $form.find('div[data-grid="' + this.nameItemQcGrid + '"]');
        var $itemQcGridControl: JQuery = jQuery(jQuery('#tmpl-grids-' + this.nameItemQcGrid + 'Browse').html());
        $itemQcGrid.empty().append($itemQcGridControl);
        $itemQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: FwFormField.getValueByDataField($form, 'ItemId')
            };
        })
        FwBrowse.init($itemQcGridControl);
        FwBrowse.renderRuntimeHtml($itemQcGridControl);
    
    }

    afterLoad($form: JQuery) {
        var $itemAttributeValueGrid: JQuery = $form.find('[data-name="' + this.nameItemAttributeValueGrid + '"]');
        FwBrowse.search($itemAttributeValueGrid);

        var $itemQcGrid: JQuery = $form.find('[data-name="' + this.nameItemQcGrid + '"]');
        FwBrowse.search($itemQcGrid);

        var status: string = FwFormField.getValueByDataField($form, 'StatusType');
        if (status === "IN") {
            FwFormField.enable($form.find('.ifin'));
        }
    }
}

(<any>window).AssetController = new RwAsset();