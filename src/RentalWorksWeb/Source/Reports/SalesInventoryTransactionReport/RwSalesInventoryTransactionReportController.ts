routes.push({ pattern: /^module\/salesinventorytransactionreport$/, action: function (match: RegExpExecArray) { return RwSalesInventoryTransactionReportController.getModuleScreen(); } });

class RwSalesInventoryTransactionReport {
    Module: string = 'SalesInventoryTransactionReport';
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
        var appOptions: any = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };
    
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        this.loadLists($form);

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid,  warehouse.warehouse);
    };

    //----------------------------------------------------------------------------------------------
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="TransTypeList"]'), [{ value: "PURCHASE", text: "Purchase", selected: "T" }, { value: "VENDOR RETURN", text: "Vendor Return", selected: "T" }, { value: "SALES", text: "Sales", selected: "T" }, { value: "CUSTOMER RETURN", text: "Customer Return", selected: "T" }, { value: "ADJUSTMENT", text: "Adjustment", selected: "T" }, { value: "TRANSFER", text: "Transfer", selected: "T" }]);
    }

    //----------------------------------------------------------------------------------------------
    beforeValidate = ($browse, $form, request) => {
        const validationName = request.module;
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        const categoryId = FwFormField.getValueByDataField($form, 'CategoryId');
        const subCategoryId = FwFormField.getValueByDataField($form, 'SubCategoryId');
        request.uniqueids = {};

        switch (validationName) {
            case 'InventoryTypeValidation':
                request.uniqueids.Sales = true;
                break;
            case 'SalesCategoryValidation':
                if (inventoryTypeId !== "") {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                break;
                }
            case 'SubCategoryValidation':
                request.uniqueids.Sales = true;

                if (inventoryTypeId !== "") {
                    request.uniqueids.TypeId = inventoryTypeId;
                }
                if (categoryId !== "") {
                    request.uniqueids.CategoryId = categoryId;
                }
                break;
            case 'SalesInventoryValidation':
                if (inventoryTypeId !== "") {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                }
                if (categoryId !== "") {
                    request.uniqueids.CategoryId = categoryId;
                }
                if (subCategoryId !== "") {
                    request.uniqueids.SubCategoryId = subCategoryId;
                } 
                break;
        };
    };
};

var RwSalesInventoryTransactionReportController: any = new RwSalesInventoryTransactionReport();