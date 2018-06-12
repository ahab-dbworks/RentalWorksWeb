routes.push({ pattern: /^module\/printorder$/, action: function (match: RegExpExecArray) { return RwPrintOrderController.getModuleScreen(); } });

class PrintOrder {
    Module: string = 'PrintOrder';
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
    openForm(type?, recordTitle?) {
        var $form;

        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        if (type == 'Quote') {
            $form.attr('data-caption', 'Quote ' + recordTitle);
        } else if (type == 'Order') {
            $form.attr('data-caption', 'Order ' + recordTitle);
        }

        $form.data('getexportrequest', function (request) {
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
    };
     //----------------------------------------------------------------------------------------------
}
var RwPrintOrderController = new PrintOrder();