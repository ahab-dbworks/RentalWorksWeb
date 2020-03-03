routes.push({ pattern: /^module\/availabilitysettings$/, action: function (match: RegExpExecArray) { return AvailabilitySettingsController.getModuleScreen(); } });

class AvailabilitySettings {
    Module: string = 'AvailabilitySettings';
    apiurl: string = 'api/v1/availabilitysettings';
    caption: string = Constants.Modules.Settings.children.SystemSettings.children.AvailabilitySettings.caption;
    nav: string = Constants.Modules.Settings.children.SystemSettings.children.AvailabilitySettings.nav;
    id: string = Constants.Modules.Settings.children.SystemSettings.children.AvailabilitySettings.id;
    //---------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
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
        $form.find('[data-datafield="KeepAvailabilityCacheCurrent"] .fwformfield-value').on('change', function () {
            let $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('[data-datafield="DaysToCache"]'));
                FwFormField.enable($form.find('[data-datafield="KeepCurrentSeconds"]'));
            }
            else {
                FwFormField.disable($form.find('[data-datafield="DaysToCache"]'));
                FwFormField.disable($form.find('[data-datafield="KeepCurrentSeconds"]'));
            }
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ControlId"] input').val(uniqueids.ControlId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery): void {
        const $availabilityHistoryGrid = $form.find('div[data-grid="AvailabilityHistoryGrid"]');
        const $availabilityHistoryGridControl = FwBrowse.loadGridFromTemplate('AvailabilityHistoryGrid');
        $availabilityHistoryGrid.empty().append($availabilityHistoryGridControl);
        $availabilityHistoryGridControl.data('ondatabind', request => {
            request.uniqueids = {
                Id: FwFormField.getValueByDataField($form, 'ControlId')
            };
        });
        FwBrowse.init($availabilityHistoryGridControl);
        FwBrowse.renderRuntimeHtml($availabilityHistoryGridControl);
        // ----------
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        if ($form.find('[data-datafield="KeepAvailabilityCacheCurrent"] .fwformfield-value').prop('checked')) {
            FwFormField.enable($form.find('div[data-datafield="DaysToCache"]'))
            FwFormField.enable($form.find('div[data-datafield="KeepCurrentSeconds"]'))
        } else {
            FwFormField.disable($form.find('div[data-datafield="DaysToCache"]'))
            FwFormField.disable($form.find('div[data-datafield="KeepCurrentSeconds"]'))
        }

        // Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });
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

var AvailabilitySettingsController = new AvailabilitySettings();