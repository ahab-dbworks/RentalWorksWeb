class Settings {
    constructor() {
        this.Module = 'Settings';
        this.caption = 'Settings';
        this.nav = 'module/settings';
        this.id = '57150967-486A-42DE-978D-A2B0F843341A';
        this.settingsMenuId = '730C9659-B33B-493E-8280-76A060A07DCE';
    }
    getModuleScreen() {
        var combinedViewModel;
        var screen = {};
        var $settings = {};
        var self = this;
        combinedViewModel = {
            captionPageTitle: "Settings"
        };
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        screen.moduleCaptions = {};
        $settings = this.openSettings();
        screen.load = function () {
            FwModule.openModuleTab($settings, 'Settings', false, 'SETTINGS', true);
            var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, self.settingsMenuId);
            var modules = FwApplicationTree.getChildrenByType(node, 'SettingsModule');
            var moduleMenu = FwApplicationTree.getChildrenByType(node, 'SettingsMenu');
            var moduleTemplates = {};
            var moduleArray = [];
            for (var i = 0; i < node.children.length; i++) {
                if (node.children[i].properties.nodetype === 'SettingsModule' && node.children[i].properties.visible === 'T') {
                    var moduleObj = [];
                    moduleObj.push(node.children[i].properties.caption, node.children[i].properties.controller.slice(0, -10), node.children[i].properties.caption, node.children[i].properties.description);
                    moduleArray.push(moduleObj);
                }
                else {
                    for (var j = 0; j < node.children[i].children.length; j++) {
                        if (node.children[i].children[j].properties.visible === 'T') {
                            var moduleObj = [];
                            moduleObj.push(node.children[i].children[j].properties.caption, node.children[i].children[j].properties.controller.slice(0, -10), node.children[i].properties.caption.slice(0, -9), node.children[i].children[j].properties.description, node.children[i].properties.caption);
                            moduleArray.push(moduleObj);
                        }
                    }
                }
            }
            for (var k = 0; k < moduleArray.length; k++) {
                if (moduleArray[k][1] === 'FacilityCategory' || moduleArray[k][1] === 'PartsCategory' || moduleArray[k][1] === 'RentalCategory' || moduleArray[k][1] === 'SalesCategory' || moduleArray[k][1] === 'LaborCategory' || moduleArray[k][1] === 'MiscCategory') {
                    moduleArray[k].push('InventoryCategoryId');
                }
                else if (moduleArray[k][1] === 'FacilityScheduleStatus' || moduleArray[k][1] === 'CrewScheduleStatus' || moduleArray[k][1] === 'VehicleScheduleStatus') {
                    moduleArray[k].push('ScheduleStatusId');
                }
                else if (moduleArray[k][1] === 'FacilityRate' || moduleArray[k][1] === 'LaborRate' || moduleArray[k][1] === 'MiscRate') {
                    moduleArray[k].push('RateId');
                }
                else if (moduleArray[k][1] === 'OfficeLocation') {
                    moduleArray[k].push('LocationId');
                }
                else {
                    moduleArray[k].push(moduleArray[k][1] + 'Id');
                }
                FwSettings.renderModuleHtml($settings.find(".fwsettings"), moduleArray[k][0], moduleArray[k][1], moduleArray[k][3], moduleArray[k][2], moduleArray[k][4], moduleArray[k][5]);
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