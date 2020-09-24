routes.push({ pattern: /^module\/systemsettings$/, action: function (match: RegExpExecArray) { return SystemSettingsController.getModuleScreen(); } });

class SystemSettings {
    Module: string = 'SystemSettings';
    apiurl: string = 'api/v1/systemsettings';
    caption: string = Constants.Modules.Settings.children.SystemSettings.children.SystemSettings.caption;
    nav: string = Constants.Modules.Settings.children.SystemSettings.children.SystemSettings.nav;
    id: string = Constants.Modules.Settings.children.SystemSettings.children.SystemSettings.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
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
        this.events($form);

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
        $form.find('div.fwformfield[data-datafield="SystemSettingsId"] input').val(uniqueids.SystemSettingsId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form: any) {
        $form.find('[data-datafield="IsVendorNumberAssignedByUser"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                FwFormField.disable($form.find('[data-datafield="LastVendorNumber"]'));
            }
            else {
                FwFormField.enable($form.find('[data-datafield="LastVendorNumber"]'));
            }
        });

        $form.find('[data-datafield="EnableReceipts"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                $form.find('.receiptwarning').hide();
            }
            else {
                $form.find('.receiptwarning').show();
            }
        });

        $form.find('[data-datafield="EnablePayments"] input').on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                $form.find('.paymentwarning').hide();
            }
            else {
                $form.find('.paymentwarning').show();
            }
        });


    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const userAssignedVendorNumber = FwFormField.getValueByDataField($form, 'IsVendorNumberAssignedByUser');
        if (userAssignedVendorNumber) {
            FwFormField.disable($form.find('[data-datafield="LastVendorNumber"]'));
        }
        else {
            FwFormField.enable($form.find('[data-datafield="LastVendorNumber"]'));
        }
    }
}
//----------------------------------------------------------------------------------------------
var SystemSettingsController = new SystemSettings();