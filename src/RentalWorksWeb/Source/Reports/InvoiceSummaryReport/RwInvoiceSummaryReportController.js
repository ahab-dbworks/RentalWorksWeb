routes.push({ pattern: /^module\/invoicesummaryreport$/, action: function (match) { return RwInvoiceSummaryReportController.getModuleScreen(); } });
var RwInvoiceSummaryReport = (function () {
    function RwInvoiceSummaryReport() {
        this.Module = 'InvoiceSummaryReport';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }
    RwInvoiceSummaryReport.prototype.getModuleScreen = function () {
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
    RwInvoiceSummaryReport.prototype.openForm = function () {
        var $form;
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });
        return $form;
    };
    ;
    RwInvoiceSummaryReport.prototype.onLoadForm = function ($form) {
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
        this.loadLists($form);
        var department = JSON.parse(sessionStorage.getItem('department'));
        var location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    };
    ;
    RwInvoiceSummaryReport.prototype.loadLists = function ($form) {
        FwFormField.loadItems($form.find('div[data-datafield="statuslist"]'), [{ value: "NEW", text: "New", selected: "T" }, { value: "RETURNED", text: "Returned", selected: "T" }, { value: "REVISED", text: "Revised", selected: "T" }, { value: "APPROVED", text: "Approved", selected: "T" }, { value: "PROCESSED", text: "Processed", selected: "T" }, { value: "CLOSED", text: "Closed", selected: "T" }, { value: "VOID", text: "Void", selected: "T" }]);
    };
    return RwInvoiceSummaryReport;
}());
;
var RwInvoiceSummaryReportController = new RwInvoiceSummaryReport();
//# sourceMappingURL=RwInvoiceSummaryReportController.js.map