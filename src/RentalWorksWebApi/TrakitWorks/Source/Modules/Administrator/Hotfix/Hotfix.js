routes.push({ pattern: /^module\/hotfix$/, action: function (match) { return HotfixController.getModuleScreen(); } });
class Hotfix {
    constructor() {
        this.Module = 'Hotfix';
        this.apiurl = 'api/v1/hotfix';
        this.caption = Constants.Modules.Administrator.children.Hotfix.caption;
        this.nav = Constants.Modules.Administrator.children.Hotfix.nav;
        this.id = Constants.Modules.Administrator.children.Hotfix.id;
    }
    addBrowseMenuItems(options) {
        options.hasNew = false;
        options.hasEdit = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    addFormMenuItems(options) {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
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
        screen.unload = () => {
            FwBrowse.screenunload($browse);
        };
        return screen;
    }
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            let descriptionField = $tr.find('.field[data-formdatafield="Description"]');
            descriptionField.css({ 'width': '520px', 'overflow': 'hidden' });
            descriptionField.parent().css({ 'width': '520px' });
        });
        return $browse;
    }
    openForm(mode) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        else {
            FwFormField.disable($form.find('.ifnew'));
        }
        return $form;
    }
    loadForm(uniqueids) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="HotfixId"] input').val(uniqueids.HotfixId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    afterLoad($form) {
    }
}
var HotfixController = new Hotfix();
//# sourceMappingURL=Hotfix.js.map