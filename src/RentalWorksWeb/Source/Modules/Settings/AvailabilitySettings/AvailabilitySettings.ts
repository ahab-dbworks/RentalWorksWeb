routes.push({ pattern: /^module\/availabilitysettings$/, action: function (match: RegExpExecArray) { return AvailabilitySettingsController.getModuleScreen(); } });

class AvailabilitySettings {
    Module: string = 'AvailabilitySettings';
    apiurl: string = 'api/v1/availabilitysettings';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Availability Settings', false, 'BROWSE', true);
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
                FwFormField.enable($form.find('[data-datafield="KeepCurrentSeconds"]'));
            }
            else {
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
                ControlId: FwFormField.getValueByDataField($form, 'ControlId')
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
            FwFormField.enable($form.find('div[data-datafield="KeepCurrentSeconds"]'))
        } else {
            FwFormField.disable($form.find('div[data-datafield="KeepCurrentSeconds"]'))
        }
    }
    //----------------------------------------------------------------------------------------------
}

var AvailabilitySettingsController = new AvailabilitySettings();