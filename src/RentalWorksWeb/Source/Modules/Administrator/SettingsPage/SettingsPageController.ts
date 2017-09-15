declare var FwModule: any;
declare var FwBrowse: any;
declare var FwHybridMasterController: any;
declare var FwAppData: any;
declare var applicationConfig: any;
declare var FwControl: any;
declare var FwSettings: any;
declare var FwApplicationTree: any;

class SettingsPage {
    Module: string;

    constructor() {
        this.Module = 'SettingsPage';
    }

    getModuleScreen() {
        var combinedViewModel: any;
        var screen: any = {};
        var $settings: any = {};

        combinedViewModel = {
            captionPageTitle: "SettingsPage"
        };
        //combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-modules-' + this.Module).html(), combinedViewModel);

        //screen.$view = FwHybridMasterController.getMasterView(combinedViewModel);
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        $settings = this.openSettings();



        screen.load = function () {
            FwModule.openModuleTab($settings, 'Settings', false, 'SETTINGS', true)
            var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');
            var modules = FwApplicationTree.getChildrenByType(node, 'Module');
            console.log(modules)
            for (var i = 0; i < modules.length; i++) {
                var moduleName = modules[i].properties.controller.slice(0, -10)
                FwSettings.renderModuleHtml($settings.find(".fwsettings"), modules[i].properties.caption, moduleName, modules[i].properties.color)
                FwSettings.renderRecordHtml($settings, moduleName)
            }

            //FwAppData.apiMethod(false, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'SettingsPage.json', null, null, function onSuccess(response) {
            //    response.Modules.forEach(function (module) {
            //        console.log(module);
            //        FwSettings.renderModuleHtml($settings.find(".fwsettings"), module.Caption, module.Title)
            //        FwSettings.renderRecordHtml($settings, module.Title)
            //    })
            //}, null, jQuery('html'));
            

        };
        screen.unload = function () {

        };

        return screen;
    }

    openSettings() {
        var $settings: any = {};

        $settings = jQuery(jQuery('#tmpl-modules-' + this.Module).html());
        FwControl.renderRuntimeControls($settings.find(".fwsettings"))

        return $settings;
    }
}

(window as any).SettingsPageController = new SettingsPage();