class DealType {
    constructor() {
        this.Module = 'DealType';
        this.apiurl = 'api/v1/dealtype';
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Deal Type', false, 'BROWSE', true);
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
        $form.find('div.fwformfield[data-datafield="DealTypeId"] input').val(uniqueids.DealTypeId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
    }
}
var DealTypeController = new DealType();
//# sourceMappingURL=DealType.js.map