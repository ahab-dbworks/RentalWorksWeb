routes.push({ pattern: /^module\/defaultsettings$/, action: function (match: RegExpExecArray) { return DefaultSettingsController.getModuleScreen(); } });

class DefaultSettings {
    Module: string = 'DefaultSettings';
    apiurl: string = 'api/v1/defaultsettings';
    caption: string = Constants.Modules.Settings.children.SystemSettings.children.DefaultSettings.caption;
    nav: string = Constants.Modules.Settings.children.SystemSettings.children.DefaultSettings.nav;
    id: string = Constants.Modules.Settings.children.SystemSettings.children.DefaultSettings.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
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
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DefaultSettingsId"] input').val(uniqueids.DefaultSettingsId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'DefaultCustomerStatusId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedefaultcustomerstatus`);
                break;
            case 'DefaultDealStatusId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedefaultdealstatus`);
                break;
            case 'DefaultDealBillingCycleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedefaultdealbillingcycle`);
                break;
            case 'DefaultUnitId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedefaultunit`);
                break;
            case 'DefaultRank':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedefaultrank`);
                break;
            case 'DefaultNonRecurringBillingCycleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedefaultnonrecurringbillingcycle`);
                break;
            case 'DefaultContactGroupId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedefaultcontactgroup`);
                break;
        }
    }
}
//----------------------------------------------------------------------------------------------
var DefaultSettingsController = new DefaultSettings();