routes.push({ pattern: /^module\/rentalinventorycatalog$/, action: function (match) { return RwRentalInventoryCatalogController.getModuleScreen(); } });
var RwRentalInventoryCatalog = (function () {
    function RwRentalInventoryCatalog() {
        this.Module = 'RentalInventoryCatalog';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.beforeValidate = function ($browse, $form, request) {
            var validationName = request.module;
            if (validationName != null) {
                var InventoryTypeValue = FwFormField.getValueByDataField($form, 'InventoryTypeId');
                var CategoryTypeId = FwFormField.getValueByDataField($form, 'CategoryId');
                var SubCategoryTypeId = FwFormField.getValueByDataField($form, 'SubCategoryId');
                switch (validationName) {
                    case 'InventoryTypeValidation':
                        request.uniqueids = {
                            Rental: true
                        };
                        break;
                    case 'RentalCategoryValidation':
                        if (InventoryTypeValue !== "") {
                            request.uniqueids = {
                                InventoryTypeId: InventoryTypeValue
                            };
                        }
                        break;
                    case 'SubCategoryValidation':
                        if (InventoryTypeValue !== "") {
                            request.uniqueids = {
                                InventoryTypeId: InventoryTypeValue
                            };
                        }
                        if (CategoryTypeId !== "") {
                            request.uniqueids = {
                                CategoryId: CategoryTypeId
                            };
                        }
                        break;
                    case 'RentalInventoryValidation':
                        if (InventoryTypeValue !== "") {
                            request.uniqueids = {
                                InventoryTypeId: InventoryTypeValue
                            };
                        }
                        ;
                        if (CategoryTypeId !== "") {
                            request.uniqueids = {
                                CategoryId: CategoryTypeId
                            };
                        }
                        ;
                        if (SubCategoryTypeId !== "") {
                            request.uniqueids = {
                                SubCategoryId: SubCategoryTypeId
                            };
                        }
                        ;
                        break;
                }
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
        var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
    };
    ;
    RwRentalInventoryCatalog.prototype.loadLists = function ($form) {
        FwFormField.loadItems($form.find('div[data-datafield="classificationlist"]'), [{ value: "I", text: "Item", selected: "T" }, { value: "A", text: "Accessory", selected: "T" }, { value: "C", text: "Complete", selected: "T" }, { value: "K", text: "Kit", selected: "T" }, { value: "N", text: "Container", selected: "T" }, { value: "M", text: "Miscellaneous", selected: "F" }]);
        FwFormField.loadItems($form.find('div[data-datafield="trackedbylist"]'), [{ value: "BARCODE", text: "Barcode", selected: "T" }, { value: "QUANTITY", text: "Quantity", selected: "T" }, { value: "SERIALNO", text: "Serial Number", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="ranklist"]'), [{ value: "A", text: "A", selected: "T" }, { value: "B", text: "B", selected: "T" }, { value: "C", text: "C", selected: "T" }, { value: "D", text: "D", selected: "T" }, { value: "E", text: "E", selected: "T" }, { value: "F", text: "F", selected: "T" }, { value: "G", text: "G", selected: "T" }]);
    };
    return RwRentalInventoryCatalog;
}());
;
var RwRentalInventoryCatalogController = new RwRentalInventoryCatalog();
//# sourceMappingURL=RwRentalInventoryCatalogController.js.map