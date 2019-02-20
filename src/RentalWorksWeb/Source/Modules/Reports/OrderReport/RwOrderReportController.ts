﻿routes.push({
    pattern: /^reports\/orderreport/, action: function (match: RegExpExecArray) {
        return RwOrderReportController.getModuleScreen();
    }
});

const orderTemplateFrontEnd = `
<div class="fwcontrol fwcontainer fwform fwreport printorder" data-control="FwContainer" data-type="form" data-version="1" data-caption="Print Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwOrderReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order" data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="float:left;max-width:300px;"></div>
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
class RwOrderReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('OrderReport', 'api/v1/orderreport', orderTemplateFrontEnd);
        this.reportOptions.HasDownloadExcel = false;
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
        return  this.getFrontEnd();
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        const convertedParams: any = {};

        convertedParams.DateType = parameters.DateType;
        convertedParams.ToDate = parameters.ToDate;
        convertedParams.FromDate = parameters.FromDate;
        convertedParams.IncludeNoCharge = parameters.IncludeNoCharge;
        convertedParams.OfficeLocationId = parameters.OfficeLocationId;
        convertedParams.DepartmentId = parameters.DepartmentId;
        convertedParams.DealId = parameters.DealId;
        convertedParams.AgentId = parameters.UserId;
        convertedParams.CustomerId = 'Testing';
        return convertedParams;
    }
    //----------------------------------------------------------------------------------------------
};

var RwOrderReportController: any = new RwOrderReportClass();
//----------------------------------------------------------------------------------------------