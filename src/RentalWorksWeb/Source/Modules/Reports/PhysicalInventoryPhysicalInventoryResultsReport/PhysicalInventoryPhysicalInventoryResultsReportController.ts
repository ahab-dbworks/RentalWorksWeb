routes.push({
    pattern: /^reports\/physicalinventoryresultsreport/, action: function (match: RegExpExecArray) {
        return PhysicalInventoryResultsReportController.getModuleScreen();
    }
});

const physInvResultsTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Print Results" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PhysicalInventoryResultsReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:300px;">
               <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Physical Inventory Results">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Physical Inventory Number" data-datafield="PhysicalInventoryId" data-displayfield="PhysicalInventoryNumber" data-savesetting="false" data-validationname="PhysicalInventoryValidation" style="float:left;max-width:300px;"></div>
                </div>
              </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;

//----------------------------------------------------------------------------------------------
class PhysicalInventoryResultsReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('PhysicalInventoryResultsReport', 'api/v1/physicalinventoryresultsreport', physInvResultsTemplate);
        this.reportOptions.HasDownloadExcel = false;
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
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'PhysicalInventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatephysicalinventory`);
                break;
        }
    }
};

var PhysicalInventoryResultsReportController: any = new PhysicalInventoryResultsReport();
//----------------------------------------------------------------------------------------------