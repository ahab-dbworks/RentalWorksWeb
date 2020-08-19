routes.push({ pattern: /^module\/hotfix$/, action: function (match: RegExpExecArray) { return HotfixController.getModuleScreen(); } });

class Hotfix implements IModule {
    Module:  string = 'Hotfix';
    apiurl:  string = 'api/v1/hotfix';
    caption: string = Constants.Modules.Administrator.children.Hotfix.caption;
    nav:     string = Constants.Modules.Administrator.children.Hotfix.nav;
    id:      string = Constants.Modules.Administrator.children.Hotfix.id;
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        options.hasEdit = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //---------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //---------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: IModuleScreen = {};
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
    //---------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        // Truncates unusually long description strings in browse
        FwBrowse.setAfterRenderRowCallback($browse, function ($tr, dt, rowIndex) {
            let descriptionField = $tr.find('.field[data-formdatafield="Description"]');
            descriptionField.css({ 'width': '520px','overflow': 'hidden' });
            descriptionField.parent().css({ 'width': '520px' });
        });

        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'))
        } else {
            FwFormField.disable($form.find('.ifnew'))
        }

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="HotfixId"] input').val(uniqueids.HotfixId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
    }
}
//---------------------------------------------------------------------------------------------
var HotfixController = new Hotfix();