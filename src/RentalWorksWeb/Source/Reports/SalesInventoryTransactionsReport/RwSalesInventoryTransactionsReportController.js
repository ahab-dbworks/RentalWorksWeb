routes.push({ pattern: /^module\/salesinventorytransactionreport$/, action: function (match) { return RwSalesInventoryTransactionsReportController.getModuleScreen(); } });
var RwSalesInventoryTransactionsReport = (function () {
    function RwSalesInventoryTransactionsReport() {
        this.Module = 'SalesInventoryTransactionsReport';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }
    RwSalesInventoryTransactionsReport.prototype.getModuleScreen = function () {
        var screen, $form;
        screen = {};
        screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $form = this.openForm();
        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () {
        };
        return screen;
    };
    ;
    RwSalesInventoryTransactionsReport.prototype.openForm = function () {
        var $form;
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });
        return $form;
    };
    ;
    RwSalesInventoryTransactionsReport.prototype.onLoadForm = function ($form) {
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
        FwReport.load($form, this.ModuleOptions.ReportOptions);
    };
    ;
    return RwSalesInventoryTransactionsReport;
}());
;
var RwSalesInventoryTransactionsReportController = new RwSalesInventoryTransactionsReport();
//# sourceMappingURL=RwSalesInventoryTransactionsReportController.js.map