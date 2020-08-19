class POClassification {
    constructor() {
        this.Module = 'POClassification';
        this.apiurl = 'api/v1/poclassification';
        this.caption = Constants.Modules.Settings.children.POSettings.children.POClassification.caption;
        this.nav = Constants.Modules.Settings.children.POSettings.children.POClassification.nav;
        this.id = Constants.Modules.Settings.children.POSettings.children.POClassification.id;
        this.Module = 'POClassification';
        this.apiurl = 'api/v1/poclassification';
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
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
        $form.find('div.fwformfield[data-datafield="PoClassificationId"] input').val(uniqueids.PoClassificationId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
    }
}
var POClassificationController = new POClassification();
//# sourceMappingURL=POClassification.js.map