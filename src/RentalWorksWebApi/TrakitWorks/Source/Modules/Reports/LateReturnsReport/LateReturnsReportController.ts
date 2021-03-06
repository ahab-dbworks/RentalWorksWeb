﻿routes.push({ pattern: /^reports\/latereturnsreport$/, action: function (match: RegExpExecArray) { return LateReturnsReportController.getModuleScreen(); } });

const lateReturnsTemplate = `
    <div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Late Return / Due Back" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="LateReturnsReportController">
      <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs" style="margin-right:10px;">
          <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
        </div>
        <div class="tabpages">
          <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
            <div class="formpage">
              <div class="row" style="display:flex;flex-wrap:wrap;">
                <div class="flexcolumn" style="max-width:400px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Type">
                    <div class="flexrow">
                      <div class="flexcolumn" style="max-width:150px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="LateReturns" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Late Returns" style="float:left;max-width:100px;padding-top:8px"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBack" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Due Back" style="float:left;max-width:100px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBackOn" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Due Back on" style="float:left;max-width:100px;"></div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="max-width:150px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DaysPastDue" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield late" data-caption="Days Past Due" style="float:left;max-width:100px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBackFewer" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dueback" data-caption="or Fewer Days" style="float:left;max-width:100px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBackDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield duebackon" data-caption="" style="float:left;max-width:150px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="max-width:600px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation"  data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType"  data-validationname="InventoryTypeValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Ordered By" data-datafield="ContactId" data-displayfield="Contact" data-validationname="ContactValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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

class LateReturnsReport extends FwWebApiReport {
    constructor() {
        super('LateReturnsReport', 'api/v1/latereturnsreport', lateReturnsTemplate);
        this.designerProvisioned = true;
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
    };
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        FwFormField.setValueByDataField($form, 'LateReturns', 'T');
        FwFormField.setValueByDataField($form, 'DaysPastDue', 1);
        FwFormField.setValueByDataField($form, 'DueBackFewer', 0);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'DueBackDate', today)
        FwFormField.setValueByDataField($form, 'ShowUnit', 'T');
        FwFormField.setValueByDataField($form, 'ShowReplacement', 'T');
        FwFormField.setValueByDataField($form, 'ShowBarCode', 'T');
        FwFormField.setValueByDataField($form, 'ShowSerial', 'T');

        const lateReturn = $form.find('div[data-datafield="LateReturns"] input');
        const dueBack = $form.find('div[data-datafield="DueBack"] input');
        const dueBackOn = $form.find('div[data-datafield="DueBackOn"] input');
        lateReturn.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                dueBack.prop('checked', false);
                dueBackOn.prop('checked', false);
                FwFormField.disable($form.find('.duebackon'));
                FwFormField.disable($form.find('.dueback'));
                FwFormField.enable($form.find('.late'))
            }
        });

        dueBack.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lateReturn.prop('checked', false);
                dueBackOn.prop('checked', false);
                FwFormField.disable($form.find('.late'));
                FwFormField.disable($form.find('.duebackon'));
                FwFormField.enable($form.find('.dueback'))
            }
        });

        dueBackOn.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lateReturn.prop('checked', false);
                dueBack.prop('checked', false);
                FwFormField.disable($form.find('.late'));
                FwFormField.disable($form.find('.dueback'));
                FwFormField.enable($form.find('.duebackon'))
            }
        });

        lateReturn.prop('checked', true);
        FwFormField.disable($form.find('.duebackon'));
        FwFormField.disable($form.find('.dueback'));
        const department = JSON.parse(sessionStorage.getItem('department'));
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        };
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        const convertedParams: any = {};

        if (parameters.LateReturns) {
            convertedParams.Type = 'PASTDUE'
            convertedParams.ReportType = 'PAST_DUE';
            convertedParams.Days = parameters.DaysPastDue;
            convertedParams.headerText = `${parameters.DaysPastDue} Days Past Due`;
        }
        if (parameters.DueBack) {
            convertedParams.Type = 'DUEBACK'
            convertedParams.ReportType = 'DUE_IN';
            convertedParams.Days = parameters.DueBackFewer;
            convertedParams.headerText = `Due Back in ${parameters.DueBackFewer} Days`;
        }
        if (parameters.DueBackOn) {
            convertedParams.Type = 'DUEBACK'
            convertedParams.ReportType = 'DUE_DATE';
            convertedParams.headerText = `Due Back on ${parameters.DueBackDate}`;
            convertedParams.DueBackDate = parameters.DueBackDate;
        }
        convertedParams.OrderedByContactId = parameters.ContactId;
        convertedParams.OfficeLocationId = parameters.OfficeLocationId;
        convertedParams.WarehouseId = parameters.WarehouseId;
        convertedParams.DepartmentId = parameters.DepartmentId;
        convertedParams.CustomerId = parameters.CustomerId;
        convertedParams.DealId = parameters.DealId;
        convertedParams.InventoryTypeId = parameters.InventoryTypeId;
        //convertedParams.ShowUnit = parameters.ShowUnit;
        //convertedParams.ShowReplacement = parameters.ShowReplacement;
        //convertedParams.ShowBarCode = parameters.ShowBarCode;
        //convertedParams.ShowSerial = parameters.ShowSerial;

        return convertedParams;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'WarehouseId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'CustomerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'DealId':
                if (customerId !== "") {
                    request.uniqueids.CustomerId = customerId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'InventoryTypeId':
                request.uniqueids.Rental = true;
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                break;
            case 'ContactId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontact`);
                break;
        }
    }
};
var LateReturnsReportController: any = new LateReturnsReport();