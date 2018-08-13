routes.push({ pattern: /^module\/billingprogressreport$/, action: function (match) { return RwBillingProgressReportController.getModuleScreen(); } });
class RwBillingProgressReport {
    constructor() {
        this.Module = 'BillingProgressReport';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }
    getModuleScreen() {
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
    }
    ;
    openForm() {
        var $form;
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });
        return $form;
    }
    ;
    onLoadForm($form) {
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
        let department = JSON.parse(sessionStorage.getItem('department')), location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        this.loadLists($form);
    }
    ;
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="statuslist"]'), [{ value: "CONFIRMED", text: "Confirmed", selected: "T" },
            { value: "HOLD", text: "Hold", selected: "T" },
            { value: "ACTIVE", text: "Active", selected: "T" },
            { value: "COMPLETE", text: "Complete", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" }]);
    }
    beforeValidateDeal($browse, $form, request) {
        request.uniqueids = {
            CustomerId: FwFormField.getValueByDataField($form, 'CustomerId'),
            DealTypeId: FwFormField.getValueByDataField($form, 'DealTypeId'),
            DealCsrId: FwFormField.getValueByDataField($form, 'CsrId')
        };
    }
    ;
}
;
var RwBillingProgressReportController = new RwBillingProgressReport();
//# sourceMappingURL=RwBillingProgressReportController.js.map