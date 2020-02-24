class Reports {
    Module: string = 'Reports';
    caption: string = Constants.Modules.Administrator.children.Reports.caption;
    nav: string = Constants.Modules.Administrator.children.Reports.nav
    id: string = Constants.Modules.Administrator.children.Reports.id;
    reportsMenuId = Constants.MainMenu.Reports.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen: any = {};
        var $reports: any = {};
        let self = this;

        let combinedViewModel = {
            captionPageTitle: "Reports"
        };
        screen.$view = FwModule.getModuleControl(this.Module + "Controller");
        screen.properties = {};
        screen.moduleCaptions = {};
        $reports = this.openReports();

        screen.load = () => {
            FwModule.openModuleTab($reports, this.caption, false, 'REPORTS', true)
            //var nodeReports = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Reports');
            const rootConstNodeReports = Constants.Modules.Reports;
            var moduleArray = [];

            for (let keyCategory in rootConstNodeReports.children) {
                const constNodeCategory = rootConstNodeReports.children[keyCategory];
                const secNodeCategory = FwApplicationTree.getNodeById(FwApplicationTree.tree, constNodeCategory.id);
                if (secNodeCategory !== null && secNodeCategory.properties.visible === 'T') {
                    for (let keyReport in constNodeCategory.children) {
                        const constNodeReport = constNodeCategory.children[keyReport];
                        const secNodeReport = FwApplicationTree.getNodeById(FwApplicationTree.tree, constNodeReport.id);
                        if (secNodeReport === null) { console.error(`${keyReport} not found. Check report API controller and/ or security ids`); }
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
    //----------------------------------------------------------------------------------------------
    openReports() {
        var $reports: any = {};

        $reports = jQuery(jQuery('#tmpl-modules-' + this.Module).html());
        FwControl.renderRuntimeControls($reports.find(".fwreports"));

        return $reports;
    }
}
//----------------------------------------------------------------------------------------------
var ReportsController = new Reports();