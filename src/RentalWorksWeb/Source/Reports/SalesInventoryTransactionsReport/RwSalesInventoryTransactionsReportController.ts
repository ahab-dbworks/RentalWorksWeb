routes.push({ pattern: /^module\/salesinventorytransactionreport$/, action: function (match: RegExpExecArray) { return RwSalesInventoryTransactionsReportController.getModuleScreen(); } });

class RwSalesInventoryTransactionsReport {
  Module: string = 'SalesInventoryTransactionsReport';
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

    //$form
    //    .on('change', 'div[data-datafield="filterdate"] input.fwformfield-value', function (event) {
    //        var thisischecked = FwFormField.getValue($form, 'div[data-datafield="filterdate"]') == 'T';
    //        FwFormField.setValue($form, 'div[data-datafield="fromdate"]', '');
    //        FwFormField.setValue($form, 'div[data-datafield="todate"]', '');
    //        FwFormField.toggle($form.find('div[data-datafield="fromdate"]'), thisischecked);
    //        FwFormField.toggle($form.find('div[data-datafield="todate"]'), thisischecked);
    //        FwFormField.toggle($form.find('div[data-datafield="onlyshoworderswith"]'), thisischecked);
    //    });

    return $form;
  };
  //----------------------------------------------------------------------------------------------
  onLoadForm($form) {
    var appOptions: any = program.getApplicationOptions();
    var request: any = { method: "LoadForm" };
    
    FwReport.load($form, this.ModuleOptions.ReportOptions);
    this.loadLists($form);
  };

  loadLists($form) {
    FwFormField.loadItems($form.find('div[data-datafield="transtypelist"]'), [{ value: "PURCHASE", text: "Purchase", selected: "T" }, { value: "VENDOR RETURN", text: "Vendor Return", selected: "T" }, { value: "SALES", text: "Sales", selected: "T" }, { value: "CUSTOMER RETURN", text: "Customer Return", selected: "T" }, { value: "ADJUSTMENT", text: "Adjustment", selected: "T" }, { value: "TRANSFER", text: "Transfer", selected: "T" }]);
    }
};

var RwSalesInventoryTransactionsReportController = new RwSalesInventoryTransactionsReport();