routes.push({
    pattern: /^reports\/physicalinventoryrecountanalysisreport/, action: function (match: RegExpExecArray) {
        return PhysicalInventoryRecountAnalysisReportController.getModuleScreen();
    }
});

const physInvRecountAnalysisTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Print Recount Analysis" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PhysicalInventoryRecountAnalysisReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:300px;">
               <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Physical Inventory Recount Analysis">
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
class PhysicalInventoryRecountAnalysisReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('PhysicalInventoryRecountAnalysisReport', 'api/v1/physicalinventoryrecountanalysisreport', physInvRecountAnalysisTemplate);
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
};

var PhysicalInventoryRecountAnalysisReportController: any = new PhysicalInventoryRecountAnalysisReport();
//----------------------------------------------------------------------------------------------