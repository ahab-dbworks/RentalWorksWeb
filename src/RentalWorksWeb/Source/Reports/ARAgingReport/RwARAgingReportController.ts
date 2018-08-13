routes.push({ pattern: /^module\/aragingreport$/, action: function (match: RegExpExecArray) { return RwARAgingReportController.getModuleScreen(); } });

class RwARAgingReport {
    Module: string = 'ARAgingReport';
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
    //----------------------------------------------------------------------------------------------
    openForm() {
        var $form;

        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function (request) {
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

        let location = JSON.parse(sessionStorage.getItem('location'));
        let today = FwFunc.getDate();

        FwFormField.setValueByDataField($form, 'ToDate', today);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    };
    //----------------------------------------------------------------------------------------------
    beforeValidateDeal($browse: any, $form: any, request: any) {
        request.uniqueids = {
            CustomerId: FwFormField.getValueByDataField($form, 'CustomerId')
            , DealTypeId: FwFormField.getValueByDataField($form, 'DealTypeId')
            , DealCsrId: FwFormField.getValueByDataField($form, 'CsrId')
        };

    };
};
var RwARAgingReportController: any = new RwARAgingReport();

