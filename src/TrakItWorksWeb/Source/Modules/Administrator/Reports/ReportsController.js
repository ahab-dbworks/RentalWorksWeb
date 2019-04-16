class Reports {
    constructor() {
        this.Module = 'Reports';
        this.caption = 'Reports';
        this.nav = 'module/reports';
        this.id = '3C5C7603-9E7B-47AB-A722-B29CA09B3B8C';
    }
    getModuleScreen() {
        var combinedViewModel;
        var screen = {};
        var $reports = {};
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
            var moduleArray = [];
            for (var i = 0; i < node.children.length; i++) {
                if (node.children[i].properties.nodetype === 'Module' && node.children[i].properties.visible === 'T') {
                    var moduleObj = [];
                    moduleObj.push(node.children[i].properties.caption, node.children[i].properties.controller.slice(0, -10), node.children[i].properties.caption, node.children[i].properties.description);
                    moduleArray.push(moduleObj);
                }
                else {
                    for (var j = 0; j < node.children[i].children.length; j++) {
                        if (node.children[i].children[j].properties.visible === 'T') {
                            var moduleObj = [];
                            moduleObj.push(node.children[i].children[j].properties.caption, node.children[i].children[j].properties.controller.slice(0, -10), node.children[i].properties.caption, node.children[i].children[j].properties.description, node.children[i].properties.caption);
                            moduleArray.push(moduleObj);
                        }
                    }
                }
            }
            for (var k = 0; k < moduleArray.length; k++) {
                moduleArray[k][5] = moduleArray[k][1] + 'Id';
                FwReportsPage.renderModuleHtml($reports.find(".fwreports"), moduleArray[k][0], moduleArray[k][1], moduleArray[k][3], moduleArray[k][2], moduleArray[k][4], moduleArray[k][5]);
            }
            $reports.find('#reportsSearch').focus();
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