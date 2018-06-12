routes.push({ pattern: /^module\/printorder$/, action: function (match) { return RwPrintOrderController.getModuleScreen(); } });
var PrintOrder = (function () {
    function PrintOrder() {
        this.Module = 'PrintOrder';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }
    PrintOrder.prototype.getModuleScreen = function () {
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
    ;
    PrintOrder.prototype.openForm = function (type, recordTitle) {
        var $form;
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        if (type == 'Quote') {
            $form.attr('data-caption', 'Quote ' + recordTitle);
        }
        else if (type == 'Order') {
            $form.attr('data-caption', 'Order ' + recordTitle);
        }
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });
        return $form;
    };
    ;
    PrintOrder.prototype.onLoadForm = function ($form) {
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
    };
    ;
    return PrintOrder;
}());
var RwPrintOrderController = new PrintOrder();
//# sourceMappingURL=RwPrintOrderController.js.map