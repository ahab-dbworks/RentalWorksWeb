routes.push({ pattern: /^module\/creditsonaccount$/, action: function (match) { return RwCreditsOnAccountController.getModuleScreen(); } });
var RwCreditsOnAccount = (function () {
    function RwCreditsOnAccount() {
        this.Module = 'CreditsOnAccount';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }
    RwCreditsOnAccount.prototype.getModuleScreen = function () {
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
    RwCreditsOnAccount.prototype.openForm = function () {
        var $form;
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });
        return $form;
    };
    ;
    RwCreditsOnAccount.prototype.onLoadForm = function ($form) {
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
        FwFormField.setValue($form, 'div[data-datafield="IncludeRemainingBalance"]', 'T');
    };
    ;
    return RwCreditsOnAccount;
}());
;
var RwCreditsOnAccountController = new RwCreditsOnAccount();
//# sourceMappingURL=RwCreditsOnAccountController.js.map