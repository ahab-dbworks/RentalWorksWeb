routes.push({ pattern: /^module\/hotfix$/, action: function (match) { return HotfixController.getModuleScreen(); } });
class Hotfix {
    constructor() {
        this.Module = 'Hotfix';
        this.apiurl = 'api/v1/hotfix';
        this.caption = 'Hotfix';
        this.nav = 'module/hotfix';
        this.id = '9D29A5D9-744F-40CE-AE3B-09219611A680';
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Hotfix', false, 'BROWSE', true);
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
        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            let descriptionField = $tr.find('.field[data-formdatafield="Description"]');
            descriptionField.css({ 'width': '520px', 'overflow': 'hidden' });
            descriptionField.parent().css({ 'width': '520px' });
        });
        return $browse;
    }
    ;
    openForm(mode) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        else {
            FwFormField.disable($form.find('.ifnew'));
        }
        return $form;
    }
    ;
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="HotfixId"] input').val(uniqueids.HotfixId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    ;
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    ;
    loadAudit($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="HotfixId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    ;
    afterLoad($form) {
    }
    ;
}
;
var HotfixController = new Hotfix();
//# sourceMappingURL=Hotfix.js.map