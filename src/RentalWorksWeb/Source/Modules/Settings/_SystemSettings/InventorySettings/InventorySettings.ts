routes.push({ pattern: /^module\/inventorysettings$/, action: function (match: RegExpExecArray) { return InventorySettingsController.getModuleScreen(); } });

class InventorySettings {
    Module: string = 'InventorySettings';
    apiurl: string = 'api/v1/inventorysettings';
    caption: string = Constants.Modules.Settings.InventorySettings.caption;
    nav: string = Constants.Modules.Settings.InventorySettings.nav;
    id: string = Constants.Modules.Settings.InventorySettings.id;
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
        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="InventorySettingsId"] input').val(uniqueids.InventorySettingsId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const userAssignedICodes = FwFormField.getValueByDataField($form, 'UserAssignedICodes');
        if (userAssignedICodes) {
            FwFormField.disable($form.find('[data-datafield="NextICode"]'));
            FwFormField.disable($form.find('[data-datafield="ICodePrefix"]'));
        }
        else {
            FwFormField.enable($form.find('[data-datafield="NextICode"]'));
            FwFormField.enable($form.find('[data-datafield="ICodePrefix"]'));
        }
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form.find('[data-datafield="UserAssignedICodes"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                FwFormField.disable($form.find('[data-datafield="NextICode"]'));
                FwFormField.disable($form.find('[data-datafield="ICodePrefix"]'));
            }
            else {
                FwFormField.enable($form.find('[data-datafield="NextICode"]'));
                FwFormField.enable($form.find('[data-datafield="ICodePrefix"]'));
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
}
//----------------------------------------------------------------------------------------------
var InventorySettingsController = new InventorySettings();