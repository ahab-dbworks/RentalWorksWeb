routes.push({
    pattern: /^reports\/purchaseordermasterreport/, action: function (match: RegExpExecArray) {
        return PurchaseOrderReportController.getModuleScreen();
    }
});

const purchaseOrderMasterReportTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Purchase Order Master Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PurchaseOrderMasterReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:450px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From" data-datafield="FromDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To" data-datafield="ToDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:115px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="Statuses" style="float:left;max-width:115px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:175px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activities">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="Activities" style="float:left;max-width:175px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;min-height:350px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div id="exporttabpage" class="tabpage exporttabpage" data-tabid="exporttab">
      </div>
    </div>
  </div>
</div>`;
//----------------------------------------------------------------------------------------------
class PurchaseOrderMasterReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('PurchaseOrderMasterReport', 'api/v1/purchaseordermasterreport', purchaseOrderMasterReportTemplate);
        this.reportOptions.HasDownloadExcel = true;
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm();

        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () { };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        this.loadLists($form);

        // Default settings for first time running
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="Department"]', department.departmentid, department.department);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        const aMonthAgo = FwFunc.getDate(today, -30);
        FwFormField.setValueByDataField($form, 'FromDate', aMonthAgo);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="Statuses"]'), [
            { value: "NEW", text: "New", selected: "T" },
            { value: "OPEN", text: "Open", selected: "T" },
            { value: "RECEIVED", text: "Received", selected: "T" },
            { value: "COMPLETE", text: "Complete", selected: "T" },
            { value: "CLOSED", text: "Closed", selected: "T" }
        ]);
        FwFormField.loadItems($form.find('div[data-datafield="Activities"]'), [
            { value: "RENTAL", text: "Rental Inventory", selected: "T" },
            { value: "SALES", text: "Sales Inventory", selected: "T" },
            { value: "PARTS", text: "Parts Inventory", selected: "T" },
            { value: "LABOR", text: "Labor", selected: "T" },
            { value: "MISC", text: "Miscellaneous", selected: "T" },
            { value: "REPAIR", text: "Repair", selected: "T" },
            { value: "VEHICLE", text: "Vehicle", selected: "T" },
            { value: "SUBRENT", text: "Sub-Rent", selected: "T" },
            { value: "SUBSALE", text: "Sub-Sale", selected: "T" },
            { value: "SUBLABOR", text: "Sub-Labor", selected: "T" },
            { value: "SUBMISC", text: "Sub-Misc", selected: "T" },
            { value: "SUBVEHICLE", text: "Sub-Vehicle", selected: "T" },
            { value: "CONSIGNMENT", text: "Consignment", selected: "T" },
        ]);
    }
    //----------------------------------------------------------------------------------------------
};

var PurchaseOrderMasterReportController: any = new PurchaseOrderMasterReport();