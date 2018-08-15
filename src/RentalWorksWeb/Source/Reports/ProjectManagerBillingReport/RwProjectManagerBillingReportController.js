routes.push({ pattern: /^module\/projectmanagerbillingreport$/, action: function (match) { return RwProjectManagerBillingReportController.getModuleScreen(); } });
class RwProjectManagerBillingReport {
    constructor() {
        this.Module = 'ProjectManagerBillingReport';
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
        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    ;
    beforeValidate($browse, $grid, request) {
        const validationName = request.module;
        const CustomerValue = jQuery($grid.find('[data-validationname="CustomerValidation"] input')).val();
        switch (validationName) {
            case 'DealValidation':
                if (CustomerValue != '') {
                    request.uniqueids = {
                        CustomerId: CustomerValue,
                    };
                }
                break;
        }
        ;
    }
    ;
}
;
var RwProjectManagerBillingReportController = new RwProjectManagerBillingReport();
//# sourceMappingURL=RwProjectManagerBillingReportController.js.map