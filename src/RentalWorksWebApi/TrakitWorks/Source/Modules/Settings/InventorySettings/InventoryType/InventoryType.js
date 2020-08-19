class InventoryType {
    constructor() {
        this.Module = 'InventoryType';
        this.apiurl = 'api/v1/inventorytype';
        this.caption = Constants.Modules.Settings.children.InventorySettings.children.InventoryType.caption;
        this.nav = Constants.Modules.Settings.children.InventorySettings.children.InventoryType.nav;
        this.id = Constants.Modules.Settings.children.InventorySettings.children.InventoryType.id;
    }
    getModuleScreen() {
        const screen = {};
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
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    loadForm(uniqueids) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventoryTypeId"] input').val(uniqueids.InventoryTypeId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
}
var InventoryTypeController = new InventoryType();
//# sourceMappingURL=InventoryType.js.map