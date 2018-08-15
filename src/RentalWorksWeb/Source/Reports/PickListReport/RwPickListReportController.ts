routes.push({ pattern: /^module\/picklistreport$/, action: function (match: RegExpExecArray) { return RwPickListReportController.getModuleScreen(); } });

class RwPickListReport {
    Module: string = 'PickListReport';
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
        var appOptions = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };
        //FwReport.getData($form, request, function(response) {
        //    try {
        //        FwFormField.loadItems($form.find('div[data-datafield="orderbylist"]'), response.orderbylist);
        //        $form.data('locationid', response.locationid);
        //    } catch(ex) {
        //        FwFunc.showError(ex);
        //    }
        //});
    };
    //----------------------------------------------------------------------------------------------

};
var RwPickListReportController = new RwPickListReport();

