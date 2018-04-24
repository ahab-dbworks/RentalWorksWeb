routes.push({ pattern: /^module\/invoicesummaryreport$/, action: function (match: RegExpExecArray) { return RwInvoiceSummaryReportController.getModuleScreen(); } });



class RwInvoiceSummaryReport {
    Module: string = 'InvoiceSummaryReport';
    ModuleOptions: any = {
        ReportOptions: {
            HasDownloadExcel: true
        }
    };

    constructor() {
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }

    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $form;
        screen            = {};
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
    //----------------------------------------------------------------------------------------------
    openForm() {
        var $form;
    
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function(request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        var appOptions: any = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };
        this.loadLists($form);

        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));

        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid,  department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid,  location.location);
    };
    //----------------------------------------------------------------------------------------------
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="statuslist"]'), [{ value: "NEW", text: "New", selected: "T" }, { value: "RETURNED", text: "Returned", selected: "T" }, { value: "REVISED", text: "Revised", selected: "T" }, { value: "APPROVED", text: "Approved", selected: "T" }, { value: "PROCESSED", text: "Processed", selected: "T" }, { value: "CLOSED", text: "Closed", selected: "T" }, { value: "VOID", text: "Void", selected: "T" }]);
    }
};
var RwInvoiceSummaryReportController: any = new RwInvoiceSummaryReport();

