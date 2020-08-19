class Attribute {
    constructor() {
        this.Module = 'Attribute';
        this.apiurl = 'api/v1/attribute';
        this.caption = Constants.Modules.Settings.children.InventorySettings.children.Attribute.caption;
        this.nav = Constants.Modules.Settings.children.InventorySettings.children.Attribute.nav;
        this.id = Constants.Modules.Settings.children.InventorySettings.children.Attribute.id;
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
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
        FwBrowse.renderGrid({
            nameGrid: 'AttributeValueGrid',
            gridSecurityId: '2uvN8jERScu',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request) => {
                request.uniqueids = {
                    AttributeId: FwFormField.getValueByDataField($form, 'AttributeId'),
                };
            },
            beforeSave: (request) => {
                request.AttributeId = FwFormField.getValueByDataField($form, 'AttributeId');
            },
        });
    }
    afterLoad($form) {
        const $attributeValueGrid = $form.find('[data-name="AttributeValueGrid"]');
        FwBrowse.search($attributeValueGrid);
    }
}
var AttributeController = new Attribute();
//# sourceMappingURL=Attribute.js.map