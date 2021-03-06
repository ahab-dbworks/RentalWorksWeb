﻿routes.push({
    pattern: /^reports\/aragingreport/, action: function (match: RegExpExecArray) {
        return ArAgingReportController.getModuleScreen();
    }
});

const ArAgingTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport aragingreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="A/R Aging" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ArAgingReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="As Of Date">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As Of Date" data-datafield="AsOfDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-showinactivemenu="true" data-validationname="OfficeLocationValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-showinactivemenu="true" data-validationname="CustomerValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal Type" data-datafield="DealTypeId" data-showinactivemenu="true" data-displayfield="DealType" data-validationname="DealTypeValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal CSR" data-datafield="DealCsrId" data-displayfield="Csr" data-showinactivemenu="true" data-validationname="UserValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-showinactivemenu="true" data-validationname="DealValidation" style="float:left;min-width:400px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div id="exporttabpage" class="tabpage exporttabpage" data-tabid="exporttab">
      </div>
    </div>
  </div>
</div>`;

//----------------------------------------------------------------------------------------------
class ArAgingReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ArAgingReport', 'api/v1/aragingreport', ArAgingTemplate);
        this.reportOptions.HasDownloadExcel = true;
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
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        const $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);

        // Default settings for first time running
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        const today = FwLocale.getDate();
        FwFormField.setValueByDataField($form, 'AsOfDate', today);
    };
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any): any {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateDeal($browse: any, $form: any, request: any) {
        request.uniqueids = {};
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const dealTypeId = FwFormField.getValueByDataField($form, 'DealTypeId');
        const dealCsrId = FwFormField.getValueByDataField($form, 'DealCsrId');

        if (customerId) {
            request.uniqueids.CustomerId = customerId;
        }
        if (dealTypeId) {
            request.uniqueids.DealTypeId = dealTypeId;
        }
        if (dealCsrId) {
            request.uniqueids.DealCsrId = dealCsrId;
        }
    };
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const dealTypeId = FwFormField.getValueByDataField($form, 'DealTypeId');
        const dealCsrId = FwFormField.getValueByDataField($form, 'DealCsrId');
        request.uniqueids = {};

        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'CustomerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'DealTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedealtype`);
                break;
            case 'DealCsrId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedealcsr`);
                break;
            case 'DealId':
                if (customerId) {
                    request.uniqueids.CustomerId = customerId;
                }
                if (dealTypeId) {
                    request.uniqueids.DealTypeId = dealTypeId;
                }
                if (dealCsrId) {
                    request.uniqueids.DealCsrId = dealCsrId;
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
        }
    }
};

var ArAgingReportController: any = new ArAgingReport();