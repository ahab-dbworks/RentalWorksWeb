﻿//routes.push({
//    pattern: /^reports\/dealoutstandingreport/, action: function (match: RegExpExecArray) {
//        return RwDealOutstandingReportController.getModuleScreen();
//    }
//});

//var dealOutstandingTemplateFrontEnd = `
//<div class="fwcontrol fwcontainer fwform fwreport dealoutstanding" data-control="FwContainer" data-type="form" data-version="1" data-caption="Deal Outstanding" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwDealOutstandingController">
//  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
//    <div class="tabs" style="margin-right:10px;">
//      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
//    </div>
//    <div class="tabpages">
//      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
//        <div class="formpage">
//          <div class="row" style="display:flex;flex-wrap:wrap;">
//            <div class="flexcolumn" style="max-width:230px;">
//              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filter Order by Date">
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-datafield="FilterDate" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter Dates" style="float:left;max-width:110px;"></div>
//                  <div data-datafield="FromDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From" data-enabled="false" style="float:left;max-width:160px;"></div>
//                  <div data-datafield="ToDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To" data-enabled="false" style="float:left;max-width:160px;"></div>
//                </div>
//              </div>
//              <div class="flexcolumn" style="max-width:220px;">
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Only Show Orders With">
//                    <div data-datafield="OnlyShowOrdersWith" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-enabled="false">
//                      <div data-value="B" data-caption="Billing Stop Date"></div>
//                      <div data-value="E" data-caption="Estimated Rental Stop Date"></div>
//                    </div>
//                  </div>
//                </div>
//              </div>
//            </div>
//            <div class="flexcolumn" style="max-width:300px;">
//              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;max-width:300px;" data-required="false"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;max-width:300px;" data-required="false"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-required="false" style="float:left;max-width:300px;"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-formbeforevalidate="beforeValidate" data-validationname="DealValidation" data-required="true" style="float:left;max-width:300px;"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-validationname="InventoryTypeValidation" data-required="false" style="float:left;max-width:300px;"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CategoryId" data-displayfield="Category" data-formbeforevalidate="beforeValidate" data-validationname="RentalCategoryValidation" data-required="false" style="float:left;max-width:300px;"></div>
//                </div>
//              </div>
//            </div>
//            <div class="flexcolumn" style="max-width:475px;">
//              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-datafield="ShowBarcodes" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Barcodes / Serial No" style="float:left;max-width:420px;"></div>
//                  <div data-datafield="ShowVendors" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Vendors" style="float:left;max-width:420px;"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-datafield="ShowResponsiblePerson" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Responsible Person" style="float:left;max-width:420px;"></div>
//                  <div data-datafield="IncludePendingExchanges" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Pending Exchanges" style="float:left;max-width:420px;"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-datafield="ShowImages" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Images" style="float:left;max-width:420px;"></div>
//                  <div data-datafield="ShowContainersOnly" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Containers Only" style="float:left;max-width:420px;"></div>
//                </div>
//                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
//                  <div data-datafield="PrintNone" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Print Blank Page When None Outstanding for Deal / Department" style="float:left;max-width:420px;"></div>
//                </div>
//              </div>
//            </div>
//            <div class="flexcolumn" style="max-width:400px;">
//              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Department / Inventory Type">
//                <div data-datafield="ShowDepartmentFrom" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
//                  <div data-value="O" data-caption="Company Department"></div>
//                  <div data-value="I" data-caption="Inventory Type"></div>
//                </div>
//              </div>
//            </div>
//            <div class="flexcolumn" style="max-width:250px;">
//              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items To Include">
//                <div data-datafield="AllEverOut" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
//                  <div data-value="F" data-caption="Items Currently Out"></div>
//                  <div data-value="T" data-caption="All Items Ever Checked-Out"></div>
//                </div>
//              </div>
//            </div>
//            <div class="flexcolumn" style="max-width:250px;">
//              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Include Value / Cost">
//                <div data-datafield="IncludeValueCost" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
//                  <div data-value="" data-caption="None"></div>
//                  <div data-value="R" data-caption="Show Replacement Cost"></div>
//                  <div data-value="U" data-caption="Show Unit Value"></div>
//                  <div data-value="P" data-caption="Show Purchase Amount"></div>
//                </div>
//              </div>
//            </div>
//          </div>
//        </div>
//      </div>
//    </div>
//  </div>
//</div>
//`;

////----------------------------------------------------------------------------------------------
//class RwDealOutstandingReportClass extends FwWebApiReport {
//    //----------------------------------------------------------------------------------------------
//    constructor() {
//        super('DealOutstandingReport', 'api/v1/dealoutstandingreport', dealOutstandingTemplateFrontEnd);
//        //this.reportOptions.HasDownloadExcel = true;
//    }
//    //----------------------------------------------------------------------------------------------
//    getModuleScreen() {
//        let screen: any = {};
//        screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
//        screen.viewModel = {};
//        screen.properties = {};

//        let $form = this.openForm();

//        screen.load = function () {
//            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
//        };
//        screen.unload = function () {
//        };
//        return screen;
//    }
//    //----------------------------------------------------------------------------------------------
//    openForm() {
//        let $form = this.getFrontEnd();
//        $form.data('getexportrequest', request => {
//            request.parameters = this.getParameters($form);
//            return request;
//        });

//        $form.on('change', 'div[data-datafield="FilterDate"] input.fwformfield-value', function (event) {
//            var thisischecked = FwFormField.getValue($form, 'div[data-datafield="FilterDate"]') === 'T';
//            FwFormField.setValue($form, 'div[data-datafield="FromDate"]', '');
//            FwFormField.setValue($form, 'div[data-datafield="ToDate"]', '');
//            FwFormField.toggle($form.find('div[data-datafield="FromDate"]'), thisischecked);
//            FwFormField.toggle($form.find('div[data-datafield="ToDate"]'), thisischecked);
//            FwFormField.toggle($form.find('div[data-datafield="OnlyShowOrdersWith"]'), thisischecked);
//        });

//        return $form;
//    }
//    //----------------------------------------------------------------------------------------------
//    onLoadForm($form) {
//        this.load($form, this.reportOptions);
//        var appOptions: any = program.getApplicationOptions();
//        var request: any = { method: "LoadForm" };

//        const department = JSON.parse(sessionStorage.getItem('department'));
//        const location = JSON.parse(sessionStorage.getItem('location'));

//        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
//        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
//    }
//    //----------------------------------------------------------------------------------------------
//    beforeValidate($browse, $form, request) {
//        const validationName = request.module;
//        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
//        const inventoryTypeId = FwFormField.getValueByDataField($form, 'InventoryTypeId');
//        request.uniqueids = {};

//        switch (validationName) {
//            case 'DealValidation':
//                if (customerId !== "") {
//                    request.uniqueids.CustomerId = customerId;
//                }
//                break;
//            case 'RentalCategoryValidation':
//                if (inventoryTypeId !== "") {
//                    request.uniqueids.InventoryTypeId = inventoryTypeId;
//                    break;
//                }
//        };
//    };
//    //----------------------------------------------------------------------------------------------
//};

//var RwDealOutstandingReportController: any = new RwDealOutstandingReportClass();
////----------------------------------------------------------------------------------------------