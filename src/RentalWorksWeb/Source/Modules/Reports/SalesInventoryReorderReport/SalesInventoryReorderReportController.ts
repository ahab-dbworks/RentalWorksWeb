routes.push({
    pattern: /^reports\/salesinventoryreorderreport/, action: function (match: RegExpExecArray) {
        return RwSalesInventoryReorderReportController.getModuleScreen();
    }
});

const salesInventoryReorderTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport salesinventoryreorderreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Invoice Summary" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwSalesInventoryReorderReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:300px;">
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Reorder Point">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield class-tracked-radio" data-caption="" data-datafield="ReorderPointMode" data-enabled="true">
                      <div data-value="ALL" data-caption="All"></div>
                      <div data-value="LT" data-caption="Items < Reorder Point"></div>
                      <div data-value="LTE" data-caption="Items &le; Reorder Point"></div>
                      <div data-value="E" data-caption="Items = Reorder Point"></div>
                      <div data-value="GTE" data-caption="Items &ge; Reorder Point"></div>
                      <div data-value="GT" data-caption="Items > Reorder Point"></div>
                  </div>
                </div>
              </div>
              <div class="row" style="display:flex;flex-wrap:wrap;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="margin-top:15px">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Items with Reorder Point of Zero" data-datafield="IncludeZeroReorderPoint" style="float:left;max-width:300px;"></div>
                  </div>
                </div>
              </div>
            </div>            
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="SalesCategoryValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Sub-Category" data-datafield="SubCategoryId" data-formbeforevalidate="beforeValidate" data-displayfield="SubCategory" data-validationname="SubCategoryValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-formbeforevalidate="beforeValidate" data-displayfield="ICode" data-validationname="SalesInventoryValidation" style="float:left;min-width:400px;"></div>
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
class RwSalesInventoryReorderClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('SalesInventoryReorderReport', 'api/v1/salesinventoryreorderreport', salesInventoryReorderTemplate);
        this.reportOptions.HasDownloadExcel = true;
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`Rw${this.Module}Controller`);
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
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $form, request) {
        var validationName = request.module;
        if (validationName != null) {
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
                    }
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids.Sales = true;
                    if (inventoryTypeId !== "") {
                        request.uniqueids.InventoryTypeId = inventoryTypeId;
                    }
                    if (categoryId !== "") {
                        request.uniqueids.CategoryId = categoryId;
                    }
                    break;
                case 'SalesInventoryValidation':
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

var RwSalesInventoryReorderReportController: any = new RwSalesInventoryReorderClass();
//----------------------------------------------------------------------------------------------