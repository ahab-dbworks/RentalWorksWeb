routes.push({ pattern: /^module\/rentalinventorycatalog$/, action: function (match: RegExpExecArray) { return RwRentalInventoryCatalogController.getModuleScreen(); } });

class RwRentalInventoryCatalog {
    Module: string = 'RentalInventoryCatalog';
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
        var request: any, appOptions: any;
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        appOptions = program.getApplicationOptions();
        request = { method: "LoadForm"};

        this.loadLists($form);

    };
//----------------------------------------------------------------------------------------------
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="classificationlist"]'), [{ value: "I", text: "Item", selected: "T" }, { value: "A", text: "Accessory", selected: "T" }, { value: "C", text: "Complete", selected: "T" }, { value: "K", text: "Kit", selected: "T" }, { value: "N", text: "Container", selected: "T" }, { value: "M", text: "Miscellaneous", selected: "F" }]);
        FwFormField.loadItems($form.find('div[data-datafield="trackedbylist"]'), [{ value: "BARCODE", text: "Barcode", selected: "T" }, { value: "QUANTITY", text: "Quantity", selected: "T" }, { value: "SERIALNO", text: "Serial Number", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="ranklist"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }]);
    }
};
var RwRentalInventoryCatalogController = new RwRentalInventoryCatalog();