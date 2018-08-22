routes.push({ pattern: /^module\/latereturnduebackreport$/, action: function (match) { return RwLateReturnDueBackReportController.getModuleScreen(); } });
class RwLateReturnDueBackReport {
    constructor() {
        this.Module = 'LateReturnDueBackReport';
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
        var lateReturn, dueBack, dueBackOn;
        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));
        lateReturn = $form.find('div[data-datafield="LateReturns"] input');
        dueBack = $form.find('div[data-datafield="DueBack"] input');
        dueBackOn = $form.find('div[data-datafield="DueBackOn"] input');
        lateReturn.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                dueBack.prop('checked', false);
                dueBackOn.prop('checked', false);
                FwFormField.disable($form.find('.duebackon'));
                FwFormField.disable($form.find('.dueback'));
                FwFormField.enable($form.find('.late'));
            }
        });
        dueBack.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lateReturn.prop('checked', false);
                dueBackOn.prop('checked', false);
                FwFormField.disable($form.find('.late'));
                FwFormField.disable($form.find('.duebackon'));
                FwFormField.enable($form.find('.dueback'));
            }
        });
        dueBackOn.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lateReturn.prop('checked', false);
                dueBack.prop('checked', false);
                FwFormField.disable($form.find('.late'));
                FwFormField.disable($form.find('.dueback'));
                FwFormField.enable($form.find('.duebackon'));
            }
        });
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    ;
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        request.uniqueids = {};
        switch (validationName) {
            case 'DealValidation':
                if (customerId != '') {
                    request.uniqueids.CustomerId = customerId;
                }
                break;
            case 'InventoryTypeValidation':
                request.uniqueids.Rental = true;
        }
        ;
    }
    ;
}
;
var RwLateReturnDueBackReportController = new RwLateReturnDueBackReport();
//# sourceMappingURL=RwLateReturnDueBackReportController.js.map