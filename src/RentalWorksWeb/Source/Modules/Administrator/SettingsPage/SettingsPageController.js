var SettingsPage = (function () {
    function SettingsPage() {
        this.Module = 'SettingsPage';
    }
    SettingsPage.prototype.getModuleScreen = function () {
        var combinedViewModel;
        var screen = {};
        var $settings = {};
        combinedViewModel = {
            captionPageTitle: "SettingsPage"
        };
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        screen.moduleCaptions = {};
        $settings = this.openSettings();
        screen.load = function () {
            FwModule.openModuleTab($settings, 'Settings', false, 'SETTINGS', true);
            var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');
            var modules = FwApplicationTree.getChildrenByType(node, 'Module');
            var moduleMenu = FwApplicationTree.getChildrenByType(node, 'Lv2ModuleMenu');
            var moduleTemplates = {};
            var moduleArray = [];
            console.log(node);
            console.log(modules);
            console.log(moduleMenu);
            for (var i = 0; i < node.children.length; i++) {
                if (node.children[i].properties.nodetype === 'Module') {
                    var moduleObj = [];
                    moduleObj.push(node.children[i].properties.caption, node.children[i].properties.controller.slice(0, -10), node.children[i].properties.caption, j);
                    moduleArray.push(moduleObj);
                }
                else {
                    for (var j = 0; j < node.children[i].children.length; j++) {
                        var moduleObj = [];
                        moduleObj.push(node.children[i].children[j].properties.caption, node.children[i].children[j].properties.controller.slice(0, -10), node.children[i].properties.caption.slice(0, -9), j);
                        moduleArray.push(moduleObj);
                    }
                }
            }
            for (var k = 0; k < moduleArray.length; k++) {
                FwSettings.renderModuleHtml($settings.find(".fwsettings"), moduleArray[k][0], moduleArray[k][1], modules[moduleArray[k][3]].properties.color, moduleArray[k][1], moduleArray[k][2]);
            }
        };
        screen.unload = function () {
        };
        return screen;
    };
    SettingsPage.prototype.openSettings = function () {
        var $settings = {};
        $settings = jQuery(jQuery('#tmpl-modules-' + this.Module).html());
        FwControl.renderRuntimeControls($settings.find(".fwsettings"));
        return $settings;
    };
    return SettingsPage;
}());
var SettingsPageController = new SettingsPage();
//# sourceMappingURL=SettingsPageController.js.map