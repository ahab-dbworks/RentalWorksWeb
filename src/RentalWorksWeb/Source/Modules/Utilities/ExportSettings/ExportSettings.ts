﻿routes.push({ pattern: /^module\/exportsettings/, action: function (match: RegExpExecArray) { return ExportSettingsController.getModuleScreen(); } });
class ExportSettings {
    Module: string = 'ExportSettings';
    caption: string = 'Export Settings';
    apiurl: string = 'api/v1/exportsettings';
    nav: string = 'module/exportsettings';
    id: string = '70CEC5BB-2FD9-4C68-9BE2-F8A3C6A17BB7';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        var $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, "Export Settings", false, 'BROWSE', true);
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
        var $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'ExportSettingsId', uniqueids.ExportSettingsId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
}
var ExportSettingsController = new ExportSettings();