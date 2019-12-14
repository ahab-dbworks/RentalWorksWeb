routes.push({
    pattern: /^reports\/contractrevisionreport/, action: function (match: RegExpExecArray) {
        return ContractRevisionReportController.getModuleScreen();
    }
});

const contractRevisionTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Contract Revision Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ContractRevisionReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From:" data-required="true" data-datafield="FromDate" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To:" data-required="true" data-datafield="ToDate" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:300px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Revision Type">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="RevisionTypes" data-control="FwFormField" data-type="checkboxlist" class="fwcontrol fwformfield" data-caption="" style="float:left;max-width:200px;margin-left:10px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:480px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="fwcontrol fwcontainer flexrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-datafield="FilterDates" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield filter-dates" data-caption="Don't include changes to Contract Billing dates when change is" style="flex:0 1 379px;"></div>
                  <div data-datafield="DaysChanged" data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="" data-enabled="false" style="flex:0 1 53px;"></div>
                  <div data-datafield="" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield filter-caption" data-caption="days or less than original Contract Billing Date." style="flex:0 1 290px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
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
class ContractRevisionReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ContractRevisionReport', 'api/v1/contractrevisionreport', contractRevisionTemplate);
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
        this.loadLists($form);

        const filterDates = $form.find('div[data-datafield="FilterDates"] input');
        filterDates.on('change', e => {
            if (jQuery(e.currentTarget).prop('checked')) {
                FwFormField.enable($form.find('div[data-datafield="DaysChanged"]'));
            } else {
                FwFormField.disable($form.find('div[data-datafield="DaysChanged"]'));
            }
        })
        filterDates.change();
        $form.find('div[data-datafield="FilterDates"]').children(":first").css('padding-top', '12px');
        $form.find('.filter-caption input').remove();
        $form.find('.filter-caption label').css('padding-bottom', '42px');

        // Default settings for first time running
        const location = JSON.parse(sessionStorage.getItem('location'));
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        const aMonthAgo = FwFunc.getDate(today, -30);
        FwFormField.setValueByDataField($form, 'FromDate', aMonthAgo);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    loadLists($form: JQuery): void {
        FwFormField.loadItems($form.find('div[data-datafield="RevisionTypes"]'),
            [
                { value: "UNASSIGNED", text: "Unassigned", selected: "T" },
                { value: "ASSIGNED", text: "Assigned", selected: "T" },
                { value: "I-CODE CHANGE", text: "I-Code Change", selected: "T" },
                { value: "RENTAL START DATE", text: "Rental Start Date", selected: "T" },
                { value: "RENTAL STOP DATE", text: "Rental Stop Date", selected: "T" },
                { value: "VOID", text: "Voids", selected: "T" }
            ]);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case '':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewebusers`);
        }
    }
};

var ContractRevisionReportController: any = new ContractRevisionReport();
//----------------------------------------------------------------------------------------------