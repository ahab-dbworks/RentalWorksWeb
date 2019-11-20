routes.push({ pattern: /^module\/emailsettings$/, action: function (match: RegExpExecArray) { return EmailSettingsController.getModuleScreen(); } });
//----------------------------------------------------------------------------------------------
class EmailSettings {
    Module: string = 'EmailSettings';
    apiurl: string = 'api/v1/emailsettings';
    caption: string = Constants.Modules.Settings.children.SystemSettings.children.EmailSettings.caption;
    nav: string = Constants.Modules.Settings.children.SystemSettings.children.EmailSettings.nav;
    id: string = Constants.Modules.Settings.children.SystemSettings.children.EmailSettings.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: any) {
        var screen: any = {};
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
    };
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode, parentModuleInfo?: any) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any): JQuery {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="EmailSettingsId"] input').val(uniqueids.EmailSettingsId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    saveForm($form: JQuery, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        const authType = FwFormField.getValueByDataField($form, 'AuthenticationType');
        const $loginFields = $form.find('[data-datafield="AccountUsername"], [data-datafield="AccountPassword"]');
        if (authType === "NONE") {
            FwFormField.disable($loginFields);
        } else {
            FwFormField.enable($loginFields);
        }
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.on('change', '[data-datafield="AuthenticationType"] input', e => {
            const authType = jQuery(e.currentTarget).val();
            const $loginFields = $form.find('[data-datafield="AccountUsername"], [data-datafield="AccountPassword"]');
            if (authType === "NONE") {
                FwFormField.disable($loginFields);
            } else {
                FwFormField.enable($loginFields);
            }
        })
    }
}
//----------------------------------------------------------------------------------------------
var EmailSettingsController = new EmailSettings();