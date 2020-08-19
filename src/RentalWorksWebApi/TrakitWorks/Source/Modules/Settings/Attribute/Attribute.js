class Attribute {
    constructor() {
        this.Module = 'Attribute';
        this.apiurl = 'api/v1/attribute';
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Attribute', false, 'BROWSE', true);
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
    openForm(mode) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="AttributeId"] input').val(uniqueids.AttributeId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    renderGrids($form) {
        const $attributeValueGrid = $form.find('div[data-grid="AttributeValueGrid"]');
        const $attributeValueGridControl = FwBrowse.loadGridFromTemplate('AttributeValueGrid');
        $attributeValueGrid.empty().append($attributeValueGridControl);
        $attributeValueGridControl.data('ondatabind', request => {
            request.uniqueids = {
                AttributeId: FwFormField.getValueByDataField($form, 'AttributeId')
            };
        });
        $attributeValueGridControl.data('beforesave', request => {
            request.AttributeId = FwFormField.getValueByDataField($form, 'AttributeId');
        });
        FwBrowse.init($attributeValueGridControl);
        FwBrowse.renderRuntimeHtml($attributeValueGridControl);
    }
    afterLoad($form) {
        const $attributeValueGrid = $form.find('[data-name="AttributeValueGrid"]');
        FwBrowse.search($attributeValueGrid);
    }
}
var AttributeController = new Attribute();
//# sourceMappingURL=Attribute.js.map