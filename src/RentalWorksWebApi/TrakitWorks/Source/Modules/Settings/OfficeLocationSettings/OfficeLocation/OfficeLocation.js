class OfficeLocation {
    constructor() {
        this.Module = 'OfficeLocation';
        this.apiurl = 'api/v1/officelocation';
        this.caption = Constants.Modules.Settings.children.OfficeLocationSettings.children.OfficeLocation.caption;
        this.nav = Constants.Modules.Settings.children.OfficeLocationSettings.children.OfficeLocation.nav;
        this.id = Constants.Modules.Settings.children.OfficeLocationSettings.children.OfficeLocation.id;
    }
    addBrowseMenuItems(options) {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Office Location', false, 'BROWSE', true);
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
        $form.find('div.fwformfield[data-datafield="LocationId"] input').val(uniqueids.LocationId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
    }
}
var OfficeLocationController = new OfficeLocation();
//# sourceMappingURL=OfficeLocation.js.map