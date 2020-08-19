class RepairItemStatus {
    constructor() {
        this.Module = 'RepairItemStatus';
        this.apiurl = 'api/v1/repairitemstatus';
        this.caption = Constants.Modules.Settings.children.RepairSettings.children.RepairItemStatus.caption;
        this.nav = Constants.Modules.Settings.children.RepairSettings.children.RepairItemStatus.nav;
        this.id = Constants.Modules.Settings.children.RepairSettings.children.RepairItemStatus.id;
    }
    getModuleScreen() {
        const screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        const $browse = this.openBrowse();
        screen.load = function () {
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
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="RepairItemStatusId"] input').val(uniqueids.RepairItemStatusId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
    }
}
var RepairItemStatusController = new RepairItemStatus();
//# sourceMappingURL=RepairItemStatus.js.map