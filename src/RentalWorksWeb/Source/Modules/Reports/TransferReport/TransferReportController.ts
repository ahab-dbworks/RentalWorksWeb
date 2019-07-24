routes.push({
    pattern: /^reports\/transferreport/, action: function (match: RegExpExecArray) {
        return TransferReportController.getModuleScreen();
    }
});

const transferReportTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TransferReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" style="display:flex;flex-wrap:wrap;" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From:" data-datafield="FromDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To:" data-datafield="ToDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:400px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Type">
                <div data-datafield="IsSummary" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="true" data-caption="Summary"></div>
                  <div data-value="false" data-caption="Detail"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:115px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-datafield="Statuses" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="From Warehouse" data-datafield="FromWarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="To Warehouse" data-datafield="ToWarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Transfer Order" data-datafield="TransferId" data-displayfield="TransferNumber" data-validationname="TransferOrderValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="RentalCategoryValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub-Category" data-datafield="SubCategoryId" data-formbeforevalidate="beforeValidate" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" data-validationpeek="false" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-formbeforevalidate="beforeValidate" data-displayfield="ICode" data-validationname="RentalInventoryValidation" style="float:left;min-width:400px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
`;
//----------------------------------------------------------------------------------------------
class TransferReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('TransferReport', 'api/v1/transferreport', transferReportTemplate);
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

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="FromWarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        this.loadLists($form);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="Statuses"]'), [
            { value: "NEW", text: "New", selected: "T" },
            { value: "CONFIRMED", text: "Confirmed", selected: "T" },
            { value: "ACTIVE", text: "Active", selected: "T" },
            { value: "COMPLETE", text: "Complete", selected: "T" },
        ]);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $form, request) {
        const validationName = request.module;
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
                    };
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
                    };
                    if (subCategoryId !== "") {
                        request.uniqueids.SubCategoryId = subCategoryId;
                    };
                    break;
            }
        }
    }
    //----------------------------------------------------------------------------------------------
};

var TransferReportController: any = new TransferReport();