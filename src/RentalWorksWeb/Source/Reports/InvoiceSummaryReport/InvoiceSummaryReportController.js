RwInvoiceSummaryReportController = {
    Module: 'InvoiceSummaryReport',
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    }
};
RwInvoiceSummaryReportController.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, RwInvoiceSummaryReportController.ModuleOptions);
//----------------------------------------------------------------------------------------------
RwInvoiceSummaryReportController.getModuleScreen = function (viewModel, properties) {
    var screen, $form;
    screen = {};
    screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
    screen.viewModel = viewModel;
    screen.properties = properties;

    $form = RwInvoiceSummaryReportController.openForm();

    screen.load = function () {
        FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
    };
    screen.unload = function () {
    };
    return screen;
};
//----------------------------------------------------------------------------------------------
RwInvoiceSummaryReportController.openForm = function (mode) {
    var $form;

    $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
    $form.data('getexportrequest', function (request) {
        request.parameters = FwReport.getParameters($form);
        return request;
    });

    return $form;
};
//----------------------------------------------------------------------------------------------
RwInvoiceSummaryReportController.onLoadForm = function ($form) {
    var request = {}, appOptions;
    FwReport.load($form, RwInvoiceSummaryReportController.ModuleOptions.ReportOptions);
    appOptions = program.getApplicationOptions();

    request.method = "LoadForm";
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
