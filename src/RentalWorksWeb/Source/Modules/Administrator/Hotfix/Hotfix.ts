routes.push({ pattern: /^module\/hotfix$/, action: function (match: RegExpExecArray) { return HotfixController.getModuleScreen(); } });

class Hotfix {
    Module: string = 'Hotfix';
    apiurl: string = 'api/v1/hotfix';
    caption: string = 'Hotfix';
    nav: string = 'module/hotfix';
    id: string = '9D29A5D9-744F-40CE-AE3B-09219611A680';
    //---------------------------------------------------------------------------------------------
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
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        // Truncates unusually long description strings in browse
        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            let descriptionField = $tr.find('.field[data-formdatafield="Description"]');
            descriptionField.css({ 'width': '520px','overflow': 'hidden' });
            descriptionField.parent().css({ 'width': '520px' });
        });

        return $browse;
    };
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
        } else {
            FwFormField.disable($form.find('.ifnew'))
        }

        return $form;
    };
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="HotfixId"] input').val(uniqueids.HotfixId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //---------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
    };
};
//---------------------------------------------------------------------------------------------
var HotfixController = new Hotfix();