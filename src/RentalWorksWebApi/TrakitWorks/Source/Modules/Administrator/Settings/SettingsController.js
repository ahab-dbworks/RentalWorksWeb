class Settings {
    constructor() {
        this.Module = 'Settings';
        this.caption = Constants.Modules.Administrator.children.Settings.caption;
        this.nav = Constants.Modules.Administrator.children.Settings.nav;
        this.id = Constants.Modules.Administrator.children.Settings.id;
        this.settingsMenuId = Constants.MainMenu.Settings.id;
    }
    getModuleScreen() {
        var combinedViewModel;
        var screen = {};
        var $settings = {};
        combinedViewModel = {
            captionPageTitle: "Settings"
        };
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        screen.moduleCaptions = {};
        $settings = this.openSettings();
        screen.load = () => {
            FwModule.openModuleTab($settings, 'Settings', false, 'SETTINGS', true);
            var moduleTemplates = {};
            var moduleArray = [];
            var node = Constants.Modules.Settings.children;
            for (var categoryKey in node) {
                const category = node[categoryKey];
                for (var moduleKey in category.children) {
                    const module = category.children[moduleKey];
                    const nodeModule = FwApplicationTree.getNodeById(FwApplicationTree.tree, module.id);
                    if (nodeModule !== null && nodeModule.properties.visible === 'T') {
                        var moduleObj = [];
                        moduleObj.push(module.caption, moduleKey, module.caption, '', categoryKey);
                        moduleArray.push(moduleObj);
                        FwSettings.renderModuleHtml($settings.find(".fwsettings"), module.caption, moduleKey, module.description, category.caption, category.caption, nodeModule.id);
                    }
                }
            }
            $settings.find('#settingsSearch').focus();
            screen.$view.find('.tabs').hide();
        };
        screen.unload = function () {
        };
        return screen;
    }
    openSettings() {
        var $settings = {};
        $settings = jQuery(jQuery('#tmpl-modules-' + this.Module).html());
        FwControl.renderRuntimeControls($settings.find(".fwsettings"));
        return $settings;
    }
}
var SettingsController = new Settings();
//# sourceMappingURL=SettingsController.js.map