routes.push({
    pattern: /^reports\/rentalinventoryattributesreport/, action: function (match: RegExpExecArray) {
        return RwRentalInventoryAttributesReportController.getModuleScreen();
    }
});

const rentalInventoryAttributesTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Rental Inventory Attributes Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwRentalInventoryAttributesReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:210px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sort By">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-datafield="SortBy" data-control="FwFormField" data-checkboxlist="persist" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" data-sortable="true" data-orderby="true" style="float:left;width:500px;margin-top:-2px"></div>
                  </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" data-required="false" style="max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="RentalCategoryValidation" data-required="false" style="max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sub Category" data-datafield="SubCategoryId" data-displayfield="SubCategory" data-formbeforevalidate="beforeValidate" data-validationname="SubCategoryValidation" data-required="false" style="max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-formbeforevalidate="beforeValidate" data-validationname="RentalInventoryValidation" data-required="false" style="max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Attribute" data-datafield="AttributeId" data-displayfield="Attribute" data-validationname="AttributeValidation" style="max-width:300px;" data-required="false"></div>
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
class RwRentalInventoryAttributesReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RentalInventoryAttributesReport', 'api/v1/rentalinventoryattributesreport', rentalInventoryAttributesTemplate);
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
        this.loadLists($form);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = ($browse, $form, request) => {
        const validationName = request.module;
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
                    break;
                }
            case 'SubCategoryValidation':
                request.uniqueids.Rental = true;

                if (inventoryTypeId !== "") {
                    request.uniqueids.TypeId = inventoryTypeId;
                }
                if (categoryId !== "") {
                    request.uniqueids.CategoryId = categoryId;
                }
                break;
            case 'RentalInventoryValidation':
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
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="SortBy"]'),
            [
                { value: "INVENTORYTYPE", text: "Inventory Type", selected: "T" },
                { value: "CATEGORY", text: "Category", selected: "T" },
                { value: "SUBCATEGORY", text: "Sub-Category", selected: "T" },
                { value: "ICODE", text: "I-Code", selected: "T" },
                { value: "ATTRIBUTE", text: "Attribute", selected: "T" }
            ]);
    }
    //----------------------------------------------------------------------------------------------
};

var RwRentalInventoryAttributesReportController: any = new RwRentalInventoryAttributesReportClass();
//----------------------------------------------------------------------------------------------