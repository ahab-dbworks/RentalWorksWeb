routes.push({ pattern: /^module\/salesinventorytransactionreport$/, action: function (match) { return RwSalesInventoryTransactionsReportController.getModuleScreen(); } });
var RwSalesInventoryTransactionsReport = (function () {
    function RwSalesInventoryTransactionsReport() {
        this.Module = 'SalesInventoryTransactionsReport';
        this.ModuleOptions = {
            ReportOptions: {
                HasDownloadExcel: true
            }
        };
        this.beforeValidate = function ($browse, $grid, request) {
            var validationName = request.module;
            var warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
            var InventoryTypeValue = jQuery($grid.find('[data-validationname="InventoryTypeValidation"] input')).val();
            var CategoryTypeId = jQuery($grid.find('[data-validationname="SalesCategoryValidation"] input')).val();
            var SubCategoryTypeId = jQuery($grid.find('[data-validationname="SubCategoryValidation"] input')).val();
            switch (validationName) {
                case 'InventoryTypeValidation':
                    request.uniqueids = {
                        Sales: true
                    };
                    break;
                case 'SalesCategoryValidation':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue
                    };
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids = {
                        Sales: true,
                        TypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId,
                    };
                    break;
                case 'SalesInventoryValidation':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId,
                        SubCategoryId: SubCategoryTypeId,
                    };
                    break;
            }
            ;
        };
        this.ModuleOptions = jQuery.extend({}, FwReport.ModuleOptions, this.ModuleOptions);
    }
    RwSalesInventoryTransactionsReport.prototype.getModuleScreen = function () {
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
    RwSalesInventoryTransactionsReport.prototype.openForm = function () {
        var $form;
        $form = FwReport.getFrontEnd('Rw', this.Module, 'tmpl-reports-' + this.Module + 'FrontEnd');
        $form.data('getexportrequest', function (request) {
            request.parameters = FwReport.getParameters($form);
            return request;
        });
        return $form;
    };
    ;
    RwSalesInventoryTransactionsReport.prototype.onLoadForm = function ($form) {
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
        FwReport.load($form, this.ModuleOptions.ReportOptions);
        this.loadLists($form);
    };
    ;
    RwSalesInventoryTransactionsReport.prototype.loadLists = function ($form) {
        FwFormField.loadItems($form.find('div[data-datafield="transtypelist"]'), [{ value: "PURCHASE", text: "Purchase", selected: "T" }, { value: "VENDOR RETURN", text: "Vendor Return", selected: "T" }, { value: "SALES", text: "Sales", selected: "T" }, { value: "CUSTOMER RETURN", text: "Customer Return", selected: "T" }, { value: "ADJUSTMENT", text: "Adjustment", selected: "T" }, { value: "TRANSFER", text: "Transfer", selected: "T" }]);
    };
    return RwSalesInventoryTransactionsReport;
}());
;
var RwSalesInventoryTransactionsReportController = new RwSalesInventoryTransactionsReport();
//# sourceMappingURL=RwSalesInventoryTransactionsReportController.js.map