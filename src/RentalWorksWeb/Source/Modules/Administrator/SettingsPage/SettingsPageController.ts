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
        screen.moduleCaptions = {};
        $settings = this.openSettings();



        screen.load = function () {
            FwModule.openModuleTab($settings, 'Settings', false, 'SETTINGS', true)
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
                } else {                
                    for (var j = 0; j < node.children[i].children.length; j++) {
                        var moduleObj = [];
                        moduleObj.push(node.children[i].children[j].properties.caption, node.children[i].children[j].properties.controller.slice(0, -10), node.children[i].properties.caption.slice(0, -9), j);
                        moduleArray.push(moduleObj);
                    }
                }
            }

            //for (var k = 0; k < moduleMenu.length; k++) {
            //    for (var l = 0; l < moduleMenu[k].children.length; l++) {
            //        var moduleObj = [];
            //        moduleObj.push(moduleMenu[k].children[l].properties.caption, moduleMenu[k].children[l].properties.controller.slice(0, -10), moduleMenu[k].properties.caption.slice(0, -9), l);
            //        moduleArray.push(moduleObj);
            //    }
            //}
            for (var k = 0; k < moduleArray.length; k++) {
                FwSettings.renderModuleHtml($settings.find(".fwsettings"), moduleArray[k][0], moduleArray[k][1], modules[moduleArray[k][3]].properties.color, moduleArray[k][1], moduleArray[k][2]);
                FwSettings.renderRecordHtml($settings, moduleArray[k][1]);
            }
            FwSettings.getCaptions(screen);

            //FwAppData.apiMethod(false, 'GET', applicationConfig.appbaseurl + applicationConfig.appvirtualdirectory + 'SettingsPage.json', null, null, function onSuccess(response) {
            //    response.Modules.forEach(function (module) {
            //        console.log(module);
            //        FwSettings.renderModuleHtml($settings.find(".fwsettings"), module.Caption, module.Title)
            //        FwSettings.renderRecordHtml($settings, module.Title)
            //    })
            //}, null, jQuery('html'));

            $settings.on('keypress', '#settingsSearch', function (e) {                
                if (e.which === 13) {
                    e.preventDefault();
                    var $settings, val, $control;
                    $settings = jQuery('small#description');
                    $control = jQuery('a#title');
                    val = jQuery.trim(this.value).toUpperCase();
                    if (val === "") {
                        $settings.closest('div.panel-group').show();
                    } else {
                        var results = [];
                        $settings.closest('div.panel-group').hide();
                        for (var caption in screen.moduleCaptions) {
                            if (caption.indexOf(val) !== -1) {
                                for (var moduleName in screen.moduleCaptions[caption]) {
                                    results.push(moduleName.toUpperCase());
                                }
                            }
                        }
                        for (var i = 0; i < results.length; i++) {
                            $settings.filter(function () {
                                return -1 != jQuery(this).text().toUpperCase().indexOf(results[i]);
                            }).closest('div.panel-group').show()
                        }
                        $control.filter(function () {
                            return -1 != jQuery(this).text().toUpperCase().indexOf(val);
                        }).closest('div.panel-group').show();
                    }
                }      
            });

            $settings.find('.fwcontrol .fwmenu').on('click', '.')
            //$control.on('keypress', '#settingsSearch', function (e) {
            //    if (e.which === 13) {
            //        e.preventDefault();
            //        var $settings, val;
            //        $settings = jQuery('a#title');
            //        val = jQuery.trim(this.value).toUpperCase();
            //        if (val === "") {
            //            $settings.parent().show();
            //        } else {
            //            $settings.closest('div.panel-group').hide();
            //            $settings.filter(function () {
            //                return -1 != jQuery(this).text().toUpperCase().indexOf(val);
            //            }).closest('div.panel-group').show();
            //        }
            //    }
            //});

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