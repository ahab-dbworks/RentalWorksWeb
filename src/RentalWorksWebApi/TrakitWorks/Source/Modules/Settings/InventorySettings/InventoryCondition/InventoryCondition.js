class InventoryCondition {
    constructor() {
        this.Module = 'InventoryCondition';
        this.apiurl = 'api/v1/inventorycondition';
        this.caption = Constants.Modules.Settings.children.InventorySettings.children.InventoryCondition.caption;
        this.nav = Constants.Modules.Settings.children.InventorySettings.children.InventoryCondition.nav;
        this.id = Constants.Modules.Settings.children.InventorySettings.children.InventoryCondition.id;
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
        $form.find('div.fwformfield[data-datafield="InventoryConditionId"] input').val(uniqueids.InventoryConditionId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
    }
}
var InventoryConditionController = new InventoryCondition();
//# sourceMappingURL=InventoryCondition.js.map