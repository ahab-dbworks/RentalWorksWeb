routes.push({ pattern: /^module\/aragingreport$/, action: function (match) { return RwARAgingReportController.getModuleScreen(); } });
class RwARAgingReport {
    constructor() {
        this.Module = 'ARAgingReport';
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
        let location = JSON.parse(sessionStorage.getItem('location'));
        let today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    ;
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
var RwARAgingReportController = new RwARAgingReport();
//# sourceMappingURL=RwARAgingReportController.js.map