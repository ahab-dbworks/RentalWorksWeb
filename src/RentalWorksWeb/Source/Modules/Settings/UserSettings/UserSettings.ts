class UserSettings {
    Module: string = 'UserSettings';
    apiurl: string = 'api/v1/usersettings';

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('NEW');

        screen.load = function () {
            FwModule.openModuleTab($form, 'User Settings', false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        var $form, $browsedefaultrows, $applicationtheme;

        const userId = JSON.parse(sessionStorage.getItem('userid'));

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $browsedefaultrows = $form.find('.browsedefaultrows');
        FwFormField.loadItems($browsedefaultrows, [
            { value: '5', text: '5' },
            { value: '10', text: '10' },
            { value: '15', text: '15' },
            { value: '20', text: '20' },
            { value: '25', text: '25' },
            { value: '30', text: '30' },
            { value: '35', text: '35' },
            { value: '40', text: '40' },
            { value: '45', text: '45' },
            { value: '50', text: '50' },
            { value: '100', text: '100' },
            { value: '200', text: '200' },
            { value: '500', text: '500' },
            { value: '1000', text: '1000' }
        ], true);

        $applicationtheme = $form.find('.applicationtheme');
        FwFormField.loadItems($applicationtheme, [
            { value: 'theme-default', text: 'Default' },
            { value: 'theme-material', text: 'Material' },
            { value: 'theme-materialmobile', text: 'Material Mobile' },
            { value: 'theme-classic', text: 'Classic' }
        ], true);

        $form.find('div.fwformfield[data-datafield="UserId"] input').val(userId.webusersid);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);

        const browseDefaultRows = jQuery($form.find('[data-datafield="BrowseDefaultRows"] select')).val().toString();
        const applicationTheme = jQuery($form.find('[data-datafield="ApplicationTheme"] select')).val().toString();

        sessionStorage.setItem('browsedefaultrows', browseDefaultRows);
        sessionStorage.setItem('applicationtheme', applicationTheme);

        setTimeout(function () {
            location.reload();
        }, 1000);
    }

    afterLoad($form) {
        const browserows = sessionStorage.getItem('browsedefaultrows');
        const theme = sessionStorage.getItem('applicationtheme');
        jQuery($form.find('div.fwformfield[data-datafield="BrowseDefaultRows"] select')).val(browserows);
        jQuery($form.find('div.fwformfield[data-datafield="ApplicationTheme"] select')).val(theme);
    }
    //----------------------------------------------------------------------------------------------
}
var UserSettingsController = new UserSettings();