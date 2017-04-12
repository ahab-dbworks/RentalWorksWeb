RwDealOutstandingController = {
    Module: 'DealOutstanding',
    ModuleOptions: {
        ReportOptions: {
            HasDownloadExcel: true
        }
    }
};
RwDealOutstandingController.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, RwDealOutstandingController.ModuleOptions);
//----------------------------------------------------------------------------------------------
RwDealOutstandingController.getModuleScreen = function(viewModel, properties) {
    var screen, $form;
    screen            = {};
    screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
    screen.viewModel  = viewModel;
    screen.properties = properties;

    $form = RwDealOutstandingController.openForm();

    screen.load = function () {
        FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
    };
    screen.unload = function () {
    };
    return screen;
};
//----------------------------------------------------------------------------------------------
RwDealOutstandingController.openForm = function() {
    var $form;
    

    $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
    $form.data('getexportrequest', function(request) {
        request.parameters = FwReport.getParameters($form);
        return request;
    });

    $form
        .on('change', 'div[data-datafield="filterdate"] input.fwformfield-value', function (event) {
            var thisischecked = (FwFormField.getValue($form, 'div[data-datafield="filterdate"]') == 'T');
            FwFormField.setValue($form, 'div[data-datafield="fromdate"]', '');
            FwFormField.setValue($form, 'div[data-datafield="todate"]', '');
            FwFormField.toggle($form.find('div[data-datafield="fromdate"]'), thisischecked);
            FwFormField.toggle($form.find('div[data-datafield="todate"]'), thisischecked);
            FwFormField.toggle($form.find('div[data-datafield="onlyshoworderswith"]'), thisischecked);
        })
    ;

    return $form;
};
//----------------------------------------------------------------------------------------------
RwDealOutstandingController.onLoadForm = function($form) {
    var request = {}, appOptions;
    FwReport.load($form, RwDealOutstandingController.ModuleOptions.ReportOptions);
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
