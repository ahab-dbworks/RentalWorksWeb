routes.push({ pattern: /^module\/rentalinventorycatalog$/, action: function (match) { return RwRentalInventoryCatalogController.getModuleScreen(); } });
class RwRentalInventoryCatalog {
    constructor() {
        this.Module = 'RentalInventoryCatalog';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.beforeValidate = function ($browse, $form, request) {
            var validationName = request.module;
            if (validationName != null) {
                const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
                const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
                const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
                request.uniqueids = {};
                switch (validationName) {
                    case 'InventoryTypeValidation':
                        request.uniqueids.Rental = true;
                        break;
                    case 'RentalCategoryValidation':
                        if (inventoryTypeId !== "") {
                            request.uniqueids.InventoryTypeId = inventoryTypeId;
                        }
                        break;
                    case 'SubCategoryValidation':
                        request.uniqueids.Rental = true;
                        if (inventoryTypeId !== "") {
                            request.uniqueids.InventoryTypeId = inventoryTypeId;
                        }
                        if (categoryId !== "") {
                            request.uniqueids.CategoryId = categoryId;
                        }
                        break;
                    case 'RentalInventoryValidation':
                        if (inventoryTypeId !== "") {
                            request.uniqueids.InventoryTypeId = inventoryTypeId;
                        }
                        ;
                        if (categoryId !== "") {
                            request.uniqueids.CategoryId = categoryId;
                        }
                        ;
                        if (subCategoryId !== "") {
                            request.uniqueids.SubCategoryId = subCategoryId;
                        }
                        ;
                        break;
                }
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
        var request, appOptions;
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        appOptions = program.getApplicationOptions();
        request = { method: "LoadForm" };
        this.loadLists($form);
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
    }
    ;
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="classificationlist"]'), [{ value: "I", text: "Item", selected: "T" }, { value: "A", text: "Accessory", selected: "T" }, { value: "C", text: "Complete", selected: "T" }, { value: "K", text: "Kit", selected: "T" }, { value: "N", text: "Container", selected: "T" }, { value: "M", text: "Miscellaneous", selected: "F" }]);
        FwFormField.loadItems($form.find('div[data-datafield="trackedbylist"]'), [{ value: "BARCODE", text: "Barcode", selected: "T" }, { value: "QUANTITY", text: "Quantity", selected: "T" }, { value: "SERIALNO", text: "Serial Number", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="ranklist"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }, { value: "E", text: "E", selected: "T" }, { value: "F", text: "F", selected: "T" }, { value: "G", text: "G", selected: "T" }]);
    }
}
;
var RwRentalInventoryCatalogController = new RwRentalInventoryCatalog();
//# sourceMappingURL=RwRentalInventoryCatalogController.js.map