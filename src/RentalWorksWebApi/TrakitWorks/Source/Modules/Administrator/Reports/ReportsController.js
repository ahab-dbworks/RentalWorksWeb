class Reports {
    constructor() {
        this.Module = 'Reports';
        this.caption = Constants.Modules.Administrator.children.Reports.caption;
        this.nav = Constants.Modules.Administrator.children.Reports.nav;
        this.id = Constants.Modules.Administrator.children.Reports.id;
        this.reportsMenuId = Constants.MainMenu.Reports.id;
    }
    getModuleScreen() {
        var screen = {};
        var $reports = {};
        let self = this;
        let combinedViewModel = {
            captionPageTitle: "Reports"
        };
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        screen.moduleCaptions = {};
        $reports = this.openReports();
        screen.load = () => {
            FwModule.openModuleTab($reports, this.caption, false, 'REPORTS', true);
            const rootConstNodeReports = Constants.Modules.Reports;
            var moduleArray = [];
            for (let keyCategory in rootConstNodeReports.children) {
                const constNodeCategory = rootConstNodeReports.children[keyCategory];
                const secNodeCategory = FwApplicationTree.getNodeById(FwApplicationTree.tree, constNodeCategory.id);
                if (secNodeCategory.properties.visible === 'T') {
                    for (let keyReport in constNodeCategory.children) {
                        const constNodeReport = constNodeCategory.children[keyReport];
                        const secNodeReport = FwApplicationTree.getNodeById(FwApplicationTree.tree, constNodeReport.id);
                        if (secNodeReport !== null && secNodeReport.properties.visible === 'T') {
                            var moduleObj = [];
                            moduleObj.push(constNodeReport.caption, keyReport, constNodeCategory.caption, constNodeReport.description, constNodeCategory.caption);
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