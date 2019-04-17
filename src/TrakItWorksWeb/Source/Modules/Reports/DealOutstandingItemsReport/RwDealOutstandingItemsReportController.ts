routes.push({
    pattern: /^reports\/dealoutstandingitemsreport/, action: function (match: RegExpExecArray) {
        return RwDealOutstandingItemsReportController.getModuleScreen();
    }
});

const dealOutstandingItemsTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport dealoutstandingitems" data-control="FwContainer" data-type="form" data-version="1" data-caption="Deal Outstanding Items" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwDealOutstandingItemsReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:230px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filter Order by Date">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="FilterDate" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield filter-dates" data-caption="Filter Dates" style="float:left;max-width:110px;"></div>
                  <div data-datafield="FromDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From" data-enabled="false" style="float:left;max-width:160px;"></div>
                  <div data-datafield="ToDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To" data-enabled="false" style="float:left;max-width:160px;"></div>
                </div>
              </div>
              <div class="flexcolumn" style="max-width:220px;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Only Show Orders With">
                    <div data-datafield="DateType" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-enabled="false">
                      <div data-value="B" data-caption="Billing Stop Date"></div>
                      <div data-value="E" data-caption="Estimated Rental Stop Date"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-formbeforevalidate="beforeValidate" data-validationname="DealValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-validationname="InventoryTypeValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="RentalCategoryValidation" style="float:left;max-width:300px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:355px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="ShowBarcodes" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Barcodes / Serial No" style="float:left;max-width:420px;"></div>
                  <div data-datafield="ShowVendors" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Vendors" style="float:left;max-width:420px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="ShowResponsiblePerson" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Responsible Person" style="float:left;max-width:420px;"></div>
                  <div data-datafield="IncludeFullImages" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield include-full-images" data-caption="Include Full Images" style="float:left;max-width:420px;"></div>
                  <div data-datafield="IncludeThumbnailImages" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield include-thumbnails" data-caption="Include Thumbnail Images" style="float:left;max-width:420px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:250px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Include Value">
                <div data-datafield="IncludeValueCost" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="NONE" data-caption="None"></div>
                  <div data-value="R" data-caption="Show Replacement Cost"></div>
                  <div data-value="U" data-caption="Show Unit Value"></div>
                  <div data-value="P" data-caption="Show Purchase Amount"></div>
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
class RwDealOutstandingItemsReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('DealOutstandingItemsReport', 'api/v1/dealoutstandingitemsreport', dealOutstandingItemsTemplate);
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
        screen.unload = function () {
        };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();

        // Mutually exclusive Image settings
        $form.on('change', '.include-full-images input[type=checkbox]', e => {
            if (FwFormField.getValueByDataField($form, 'IncludeFullImages') === true) {
                $form.find('div[data-datafield="IncludeThumbnailImages"] input').prop('checked', false);
            }
        });
        $form.on('change', '.include-thumbnails input[type=checkbox]', e => {
            if (FwFormField.getValueByDataField($form, 'IncludeThumbnailImages') === true) {
                $form.find('div[data-datafield="IncludeFullImages"] input').prop('checked', false);
            }
        });
        // Expose date fields if Filter Date
        $form.on('change', '.filter-dates input[type=checkbox]', e => {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            FwFormField.setValueByDataField($form, 'FromDate', '');
            FwFormField.setValueByDataField($form, 'ToDate', '');
            FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), isChecked);
            FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), isChecked);
            FwFormField.toggle($form.find('div[data-datafield="DateType"]'), isChecked);
        });

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValueByDataField($form, 'IncludeValueCost', 'R');
        FwFormField.setValueByDataField($form, 'ShowBarcodes', 'T');
        FwFormField.setValueByDataField($form, 'ShowVendors', 'T');
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }

    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
        request.uniqueids = {};

        switch (validationName) {
            case 'DealValidation':
                if (customerId !== "") {
                    request.uniqueids.CustomerId = customerId;
                }
                break;
            case 'RentalCategoryValidation':
                if (inventoryTypeId !== "") {
                    request.uniqueids.InventoryTypeId = inventoryTypeId;
                    break;
                }
        };
    };
    //----------------------------------------------------------------------------------------------
};

var RwDealOutstandingItemsReportController: any = new RwDealOutstandingItemsReportClass();
//----------------------------------------------------------------------------------------------