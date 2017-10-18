var RwAsset = (function () {
    function RwAsset() {
        this.Module = 'Asset';
        this.apiurl = 'api/v1/item';
    }
    RwAsset.prototype.getModuleScreen = function () {
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
    };
    RwAsset.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    RwAsset.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    RwAsset.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ItemId"] input').val(uniqueids.ItemId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    RwAsset.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    RwAsset.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="ItemId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    RwAsset.prototype.renderGrids = function ($form) {
        var $itemAttributeValueGrid;
        var $itemAttributeValueGridControl;
        var $itemQcGrid;
        var $itemQcGridControl;
        $itemAttributeValueGrid = $form.find('div[data-grid="ItemAttributeValueGrid"]');
        $itemAttributeValueGridControl = jQuery(jQuery('#tmpl-grids-ItemAttributeValueGridBrowse').html());
        $itemAttributeValueGrid.empty().append($itemAttributeValueGridControl);
        $itemAttributeValueGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: $form.find('div.fwformfield[data-datafield="ItemId"] input').val()
            };
        });
        FwBrowse.init($itemAttributeValueGridControl);
        FwBrowse.renderRuntimeHtml($itemAttributeValueGridControl);
        $itemQcGrid = $form.find('div[data-grid="ItemQcGrid"]');
        $itemQcGridControl = jQuery(jQuery('#tmpl-grids-ItemQcGridBrowse').html());
        $itemQcGrid.empty().append($itemQcGridControl);
        $itemQcGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ItemId: $form.find('div.fwformfield[data-datafield="ItemId"] input').val()
            };
        });
        FwBrowse.init($itemQcGridControl);
        FwBrowse.renderRuntimeHtml($itemQcGridControl);
    };
    RwAsset.prototype.afterLoad = function ($form) {
        var $itemAttributeValueGrid;
        var $itemQcGrid;
        $itemAttributeValueGrid = $form.find('[data-name="ItemAttributeValueGrid"]');
        FwBrowse.search($itemAttributeValueGrid);
        $itemQcGrid = $form.find('[data-name="ItemQcGrid"]');
        FwBrowse.search($itemQcGrid);
    };
    return RwAsset;
}());
window.AssetController = new RwAsset();
//# sourceMappingURL=Asset.js.map