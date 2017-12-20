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
        //combinedViewModel.htmlPageBody = Mustache.render(jQuery('#tmpl-modules-' + this.Module).html(), combinedViewModel);
        //screen.$view = FwHybridMasterController.getMasterView(combinedViewModel);
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        screen.moduleCaptions = {};
        $settings = this.openSettings();
        screen.load = function () {
            FwModule.openModuleTab($settings, 'Settings', false, 'SETTINGS', true);
            var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '730C9659-B33B-493E-8280-76A060A07DCE');
            var modules = FwApplicationTree.getChildrenByType(node, 'Module');
            var moduleTemplates = {};
            var moduleArray = [];
            for (var i = 0; i < modules.length; i++) {
                var moduleObj = [];
                moduleObj.push(modules[i].properties.caption, modules[i].properties.controller.slice(0, -10), i);
                moduleArray.push(moduleObj);
            }
            moduleArray.sort();
            for (var j = 0; j < moduleArray.length; j++) {
                FwSettings.renderModuleHtml($settings.find(".fwsettings"), moduleArray[j][0], moduleArray[j][1], modules[moduleArray[j][2]].properties.color, moduleArray[j][1]);
                FwSettings.renderRecordHtml($settings, moduleArray[j][1]);
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
                    }
                    else {
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
                            }).closest('div.panel-group').show();
                        }
                        $control.filter(function () {
                            return -1 != jQuery(this).text().toUpperCase().indexOf(val);
                        }).closest('div.panel-group').show();
                    }
                }
            });
            $settings.find('.fwcontrol .fwmenu').on('click', '.');
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
    };
    SettingsPage.prototype.openSettings = function () {
        var $settings = {};
        $settings = jQuery(jQuery('#tmpl-modules-' + this.Module).html());
        FwControl.renderRuntimeControls($settings.find(".fwsettings"));
        return $settings;
    };
    return SettingsPage;
}());
window.SettingsPageController = new SettingsPage();
//# sourceMappingURL=SettingsPageController.js.map