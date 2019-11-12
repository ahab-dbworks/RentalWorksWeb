routes.push({
    pattern: /^reports\/valueofoutrentalinventoryreport/, action: function (match: RegExpExecArray) {
        return ValueOfOutRentalInventoryReportController.getModuleScreen();
    }
});

const valueOfOutTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Value Of Out Rental Inventory Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ValueOfOutRentalInventoryReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="As Of Date">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As Of Date" data-datafield="AsOfDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:115px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ownership">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="OwnerShips" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:140px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracked By">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="TrackedBys" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
</div>
`;
//----------------------------------------------------------------------------------------------
class ValueOfOutRentalInventoryReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ValueOfOutRentalInventoryReport', 'api/v1/valueofoutrentalinventoryreport', valueOfOutTemplate);
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
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'AsOfDate', today);
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        this.loadLists($form);
    };
    //----------------------------------------------------------------------------------------------
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="TrackedBys"]'), [{ value: "BARCODE", text: "Barcode", selected: "T" }, { value: "QUANTITY", text: "Quantity", selected: "T" }, { value: "SERIALNO", text: "Serial Number", selected: "T" }, { value: "RFID", text: "RFID", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="OwnerShips"]'), [{ value: "OWNED", text: "Owned", selected: "T" }, { value: "CONSIGNED", text: "Consigned", selected: "T" }]);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse: any, $form: any, request: any) {
        const validationName = request.module;
        if (validationName != null) {
            request.uniqueids = {};

            switch (validationName) {
                case 'InventoryTypeValidation':
                    request.uniqueids.Rental = true;
                    break;
            }
        }
    };
    //----------------------------------------------------------------------------------------------
};

var ValueOfOutRentalInventoryReportController: any = new ValueOfOutRentalInventoryReport();