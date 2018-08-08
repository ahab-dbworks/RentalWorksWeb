class Reports {
    constructor() {
        this.Module = 'Reports';
    }
    getModuleScreen() {
        var combinedViewModel;
        var screen = {};
        var $reports = {};
        var self = this;
        combinedViewModel = {
            captionPageTitle: "Reports"
        };
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        screen.moduleCaptions = {};
        $reports = this.openReports();
        screen.load = function () {
            FwModule.openModuleTab($reports, 'Reports', false, 'REPORTS', true);
            var node = FwApplicationTree.getNodeById(FwApplicationTree.tree, '7FEC9D55-336E-44FE-AE01-96BF7B74074C');
            var modules = FwApplicationTree.getChildrenByType(node, 'Module');
            var moduleMenu = FwApplicationTree.getChildrenByType(node, 'Lv2ModuleMenu');
            var moduleTemplates = {};
            var moduleArray = [];
            for (var i = 0; i < node.children.length; i++) {
                if (node.children[i].properties.nodetype === 'Module') {
                    var moduleObj = [];
                    moduleObj.push(node.children[i].properties.caption, node.children[i].properties.controller.slice(0, -10), node.children[i].properties.caption, node.children[i].properties.description);
                    moduleArray.push(moduleObj);
                }
                else {
                    for (var j = 0; j < node.children[i].children.length; j++) {
                        var moduleObj = [];
                        moduleObj.push(node.children[i].children[j].properties.caption, node.children[i].children[j].properties.controller.slice(0, -10), node.children[i].properties.caption, node.children[i].children[j].properties.description);
                        moduleArray.push(moduleObj);
                    }
                }
            }
            for (var k = 0; k < moduleArray.length; k++) {
                if (moduleArray[k][1] === 'FacilityCategory' || moduleArray[k][1] === 'PartsCategory' || moduleArray[k][1] === 'RentalCategory' || moduleArray[k][1] === 'SalesCategory' || moduleArray[k][1] === 'LaborCategory' || moduleArray[k][1] === 'MiscCategory') {
                    moduleArray[k][4] = 'InventoryCategoryId';
                }
                else if (moduleArray[k][1] === 'FacilityScheduleStatus' || moduleArray[k][1] === 'CrewScheduleStatus' || moduleArray[k][1] === 'VehicleScheduleStatus') {
                    moduleArray[k][4] = 'ScheduleStatusId';
                }
                else if (moduleArray[k][1] === 'FacilityRate' || moduleArray[k][1] === 'LaborRate' || moduleArray[k][1] === 'MiscRate') {
                    moduleArray[k][4] = 'RateId';
                }
                else if (moduleArray[k][1] === 'OfficeLocation') {
                    moduleArray[k][4] = 'LocationId';
                }
                else {
                    moduleArray[k][4] = moduleArray[k][1] + 'Id';
                }
                FwReportsPage.renderModuleHtml($reports.find(".fwreports"), moduleArray[k][0], moduleArray[k][1], moduleArray[k][3], moduleArray[k][2], moduleArray[k][4]);
            }
            $reports.find('#settingsSearch').focus();
            screen.$view.find('.tabs').hide();
        };
        screen.unload = function () {
        };
        return screen;
    }
    openReports() {
        var $reports = {};
        $reports = jQuery(jQuery('#tmpl-modules-' + this.Module).html());
        FwControl.renderRuntimeControls($reports.find(".fwreports"));
        return $reports;
    }
}
var ReportsController = new Reports();
//# sourceMappingURL=ReportsController.js.map