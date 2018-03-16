routes.push({ pattern: /^module\/rentalinventorycatalog$/, action: function (match) { return RwRentalInventoryCatalogController.getModuleScreen(); } });
var RwRentalInventoryCatalog = (function () {
    function RwRentalInventoryCatalog() {
        this.Module = 'RentalInventoryCatalog';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }
    RwRentalInventoryCatalog.prototype.getModuleScreen = function () {
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
    RwRentalInventoryCatalog.prototype.openForm = function () {
        var $form;
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });
        return $form;
    };
    ;
    RwRentalInventoryCatalog.prototype.onLoadForm = function ($form) {
        var request, appOptions;
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        appOptions = program.getApplicationOptions();
        request = { method: "LoadForm" };
        this.loadLists($form);
    };
    ;
    RwRentalInventoryCatalog.prototype.loadLists = function ($form) {
        FwFormField.loadItems($form.find('div[data-datafield="classificationlist"]'), [{ value: "I", text: "Item", selected: "T" }, { value: "A", text: "Accessory", selected: "T" }, { value: "Comp", text: "Complete", selected: "T" }, { value: "K", text: "Kit", selected: "T" }, { value: "Cont", text: "Container", selected: "T" }, { value: "M", text: "Miscellaneous", selected: "F" }]);
        FwFormField.loadItems($form.find('div[data-datafield="trackedbylist"]'), [{ value: "Barcode", text: "Bar Code", selected: "T" }, { value: "Quantity", text: "Quantity", selected: "T" }, { value: "Serialno", text: "Serial Number", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="ranklist"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }]);
    };
    return RwRentalInventoryCatalog;
}());
;
var RwRentalInventoryCatalogController = new RwRentalInventoryCatalog();
//# sourceMappingURL=RwRentalInventoryCatalogController.js.map