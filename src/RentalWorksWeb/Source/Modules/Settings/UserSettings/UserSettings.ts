declare var FwFormField;

class UserSettings {
    Module: string;

    constructor() {
        this.Module = 'UserSettings';
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.loadForm();

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

        $form = jQuery(jQuery('#tmpl-modules-UserSettingsForm').html());
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
        ]);

        $applicationtheme = $form.find('.applicationtheme');
        FwFormField.loadItems($applicationtheme, [
            { value: 'theme-default', text: 'Default' },
            { value: 'theme-material', text: 'Material' },
            { value: 'theme-materialmobile', text: 'Material Mobile' },
            { value: 'theme-classic', text: 'Classic' }
        ]);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm() {
        var $form = this.openForm('EDIT');
        var request: any = {};
        request.method = 'LoadSettings';
        FwModule.getData($form, request, function (response) {
            FwFormField.setValue($form, 'div[data-datafield="#settings.browsedefaultrows"]', response.usersettings.browsedefaultrows);
            FwFormField.setValue($form, 'div[data-datafield="#settings.applicationtheme"]', response.usersettings.applicationtheme);
        }, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, closetab: boolean, navigationpath: string) {
        var $fwformfields = $form.find('.fwformfield');
        var fields = {};
        $fwformfields.each(function (index, element) {
            var $fwformfield, originalValue, dataField, value, isValidDataField, getAllFields, isBlank, isCalculatedField;

            $fwformfield = jQuery(element);
            dataField = $fwformfield.attr('data-datafield');
            value = FwFormField.getValue2($fwformfield);

            var field = {
                datafield: dataField,
                value: value
            };
            fields[dataField] = field;
        });

        var request = {
            method: 'SaveSettings',
            fields: fields
        }
        request.method = 'SaveSettings';
        request.fields = fields;
        FwModule.getData($form, request, function (response) {
            $form.attr('data-modified', false);
            $form.find('.btn[data-type="SaveMenuBarButton"]').addClass('disabled');
            jQuery('#' + $form.parent().attr('data-tabid')).find('.modified').html('');
            sessionStorage.setItem('browsedefaultrows', response.usersettings.browsedefaultrows);
            sessionStorage.setItem('applicationtheme', response.usersettings.applicationtheme);
            //jQuery('html').removeClassPrefix('theme-').addClass(response.usersettings.applicationtheme)
            location.reload();
        }, $form);
    }
}
(window as any).UserSettingsController = new UserSettings();