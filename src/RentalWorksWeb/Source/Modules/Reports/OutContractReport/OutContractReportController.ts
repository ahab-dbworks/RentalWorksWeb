﻿routes.push({
    pattern: /^reports\/outcontractreport/, action: function (match: RegExpExecArray) {
        return OutContractReportController.getModuleScreen();
    }
});

const outContractReportTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport outcontractreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="OutContractReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="formcolumn" style="width:260px">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Out Contract">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield contractid" data-caption="Contract Number" data-datafield="ContractId"  data-validationname="ContractValidation" data-savesetting="false" data-required="true" style="float:left;max-width:300px;"></div>
              </div>
            </div>
          </div>
          <div class="flexcolumn" style="max-width:210px;">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Asset Details">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-datafield="IncludeBarCodes" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Bar Codes" style="float:left;max-width:420px;"></div>
              </div>
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-datafield="IncludeSerialNumbers" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Serial Numbers" style="float:left;max-width:420px;"></div>
              </div>
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-datafield="IncludeRfids" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="RFIDs" style="float:left;max-width:420px;"></div>
              </div>
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-datafield="IncludeManufacturerPartNumbers" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Manufacturer Part Numbers" style="float:left;max-width:420px;"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;

class OutContractReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('OutContractReport', 'api/v1/outcontractreport', outContractReportTemplate);
        this.reportOptions.HasDownloadExcel = false;
        this.designerProvisioned = true;
    };
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
    };
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        request.uniqueids = {};

        switch (datafield) {
            case 'ContractId':
                request.uniqueids.ContractType = 'OUT';
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontract`);
                break;
        };
    };
    //----------------------------------------------------------------------------------------------
};
var OutContractReportController: any = new OutContractReport();