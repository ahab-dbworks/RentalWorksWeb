﻿routes.push({
    pattern: /^reports\/agentbillingreport/, action: function (match: RegExpExecArray) {
        return RwAgentBillingReportController.getModuleScreen();
    }
});

const agentBillingTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport agentbillingreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Agent Billing" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwAgentBillingReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
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
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Type">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield datatype" data-caption="From:" data-datafield="DateType" style="float:left;max-width:200px;">
                    <div data-value="INVOICE_DATE" data-caption="Invoice Date"></div>
                    <div data-value="BILLING_START_DATE" data-caption="Billing Start Date"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:250px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="IncludeNoCharge" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include &quot;No Charge&quot; Invoices" style="float:left;max-width:420px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="User" data-validationname="UserValidation" data-multiwordseparator="|" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-formbeforevalidate="beforeValidate" data-validationname="DealValidation" style="float:left;min-width:400px;"></div>
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
class RwAgentBillingReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('AgentBillingReport', 'api/v1/agentbillingreport', agentBillingTemplate);
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
    convertParameters(parameters: any): any {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));

        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        request.uniqueids = {};

        switch (validationName) {
            case 'DealValidation':
                if (customerId !== "") {
                    request.uniqueids.CustomerId = customerId;
                }
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
}

var RwAgentBillingReportController: any = new RwAgentBillingReportClass();
//----------------------------------------------------------------------------------------------