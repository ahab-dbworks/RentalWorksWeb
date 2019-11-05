routes.push({
    pattern: /^reports\/repairorderstatusreport/, action: function (match: RegExpExecArray) {
        return RepairOrderStatusReportController.getModuleScreen();
    }
});

const repairOrderStatusTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Repair Order Status" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RepairOrderStatusReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" style="display:flex;flex-wrap:wrap;" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:400px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Type">
                <div data-datafield="IsSummary" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="true" data-caption="Summary"></div>
                  <div data-value="false" data-caption="Detail"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billable">
                <div data-datafield="Billable" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="true" data-caption="Yes"></div>
                  <div data-value="false" data-caption="No"></div>
                  <div data-value="ALL" data-caption="All"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billed">
                <div data-datafield="Billed" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="true" data-caption="Yes"></div>
                  <div data-value="false" data-caption="No"></div>
                  <div data-value="ALL" data-caption="All"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Type">
                <div data-datafield="Owned" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="true" data-caption="Owned"></div>
                  <div data-value="false" data-caption="Not Owned"></div>
                  <div data-value="ALL" data-caption="All"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:175px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair Order Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="RepairOrderStatus" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:175px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:97px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Priority">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="Priority" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:96px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:220px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="IncludeDamageNotes" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Damage Notes" style="float:left;max-width:420px;"></div>
                  <div data-datafield="IncludeOutsideRepairsOnly" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Outside Repairs Only" style="float:left;max-width:420px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:350px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Days in Repair">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="DaysInRepairFilterMode" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield toggle-enable" data-caption="" style="float:left;">
                    <div data-value="ALL" data-caption="All"></div>
                    <div data-value="LTE" data-caption="<="></div>
                    <div data-value="GT" data-caption=">"></div>
                  </div>
                  <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield toggle-enable filter-amount" data-caption="" data-datafield="DaysInRepair" style="float:left; margin-left:20px; width:70px"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;min-width:400px;"></div>
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
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Item Status" data-datafield="RepairItemStatusId" data-displayfield="RepairItemStatus" data-validationname="RepairItemStatusValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-formbeforevalidate="beforeValidate" data-displayfield="Vendor" data-validationname="VendorValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Vendor Item Status" data-datafield="VendorRepairItemStatusId" data-displayfield="VendorRepairItemStatus" data-validationname="RepairItemStatusValidation" style="float:left;min-width:400px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
//----------------------------------------------------------------------------------------------
class RepairOrderStatusReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RepairOrderStatusReport', 'api/v1/rentalinventoryvaluereport', repairOrderStatusTemplate);
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
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);

        this.loadLists($form);

        $form.find('div[data-datafield="DaysInRepairFilterMode"]').change(() => {
            const daysFilterMode = FwFormField.getValueByDataField($form, 'DaysInRepairFilterMode');
            if (daysFilterMode === 'ALL') {
                FwFormField.disable($form.find('div[data-datafield="DaysInRepair"]'));
                FwFormField.setValueByDataField($form, 'DaysInRepair', "");
                $form.find('div[data-datafield="DaysInRepair"]').attr('data-required', 'false').removeClass('error');
                $form.find('div[data-caption="General"]').removeClass('error');
            } else {
                FwFormField.enable($form.find('div[data-datafield="DaysInRepair"]'));
                $form.find('div[data-datafield="DaysInRepair"]').attr('data-required', 'true');
            }
        });
        $form.on('change', 'div[data-datafield="IsSummary"] input', e => {
            if (FwFormField.getValueByDataField($form, 'IsSummary') === 'true') {
                $form.find('div[data-datafield="IncludeDamageNotes"] input').prop('checked', false);
                FwFormField.disable($form.find('div[data-datafield="IncludeDamageNotes"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="IncludeDamageNotes"]'));
            }
        });
        // Mutually exclusive Damage Notes
        $form.on('change', 'div[data-datafield="IncludeOutsideRepairsOnly"] input', e => {
            if (FwFormField.getValueByDataField($form, 'IncludeOutsideRepairsOnly') === true) {
                $form.find('div[data-datafield="IncludeDamageNotes"] input').prop('checked', false);
            }
        });
        $form.on('change', 'div[data-datafield="IncludeDamageNotes"] input', e => {
            if (FwFormField.getValueByDataField($form, 'IncludeDamageNotes') === true) {
                $form.find('div[data-datafield="IncludeOutsideRepairsOnly"] input').prop('checked', false);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        if (parameters.Billable === 'ALL') {
            delete parameters.Billable;
        }
        if (parameters.Billed === 'ALL') {
            delete parameters.Billed;
        }
        if (parameters.Owned === 'ALL') {
            delete parameters.Owned;
        }
        return parameters;
    }
    afterLoad($form) {
        const daysFilterMode = FwFormField.getValueByDataField($form, 'DaysInRepairFilterMode');
        if (daysFilterMode === 'ALL') {
            FwFormField.disable($form.find('div[data-datafield="DaysInRepair"]'));
            FwFormField.setValueByDataField($form, 'DaysInRepair', "");
            $form.find('div[data-datafield="DaysInRepair"]').attr('data-required', 'false').removeClass('error');
            $form.find('div[data-caption="General"]').removeClass('error');
        } else {
            FwFormField.enable($form.find('div[data-datafield="DaysInRepair"]'));
            $form.find('div[data-datafield="DaysInRepair"]').attr('data-required', 'true');
        }
        FwFormField.setValueByDataField($form, 'Billed', 'ALL');
        FwFormField.setValueByDataField($form, 'Billable', 'ALL');
        FwFormField.setValueByDataField($form, 'Owned', 'ALL');
        if (FwFormField.getValueByDataField($form, 'IsSummary') === 'true') {
            FwFormField.setValueByDataField($form, 'IncludeDamageNotes', false);
            FwFormField.disable($form.find('div[data-datafield="IncludeDamageNotes"]'));
            $form.find('div[data-datafield="IncludeDamageNotes"]').change();
        } else {
            FwFormField.enable($form.find('div[data-datafield="IncludeDamageNotes"]'));

        }
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form) {
        FwFormField.loadItems($form.find('div[data-datafield="RepairOrderStatus"]'), [{ value: "NEW", text: "New", selected: "T" }, { value: "PENDING", text: "Pending", selected: "T" }, { value: "ESTIMATE", text: "Estimated", selected: "T" }, { value: "COMPLETE", text: "Complete", selected: "T" }, { value: "VOID", text: "Void", selected: "T" }]);
        FwFormField.loadItems($form.find('div[data-datafield="Priority"]'), [
            { value: "HIG", text: "High", selected: "T" },
            { value: "MED", text: "Medium", selected: "T" },
            { value: "LOW", text: "Low", selected: "T" }
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

var RepairOrderStatusReportController: any = new RepairOrderStatusReport();