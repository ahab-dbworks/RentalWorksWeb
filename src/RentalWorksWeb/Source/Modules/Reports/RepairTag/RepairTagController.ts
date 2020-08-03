routes.push({
    pattern: /^reports\/repairtag/, action: function (match: RegExpExecArray) {
        return RepairTagController.getModuleScreen();
    }
});

const repairTagTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RepairTagController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="formcolumn" style="width:260px">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair Number">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Repair Number" data-datafield="RepairId" data-displayfield="RepairNumber" data-formbeforevalidate="beforeValidate" data-validationname="RepairValidation" data-savesetting="false" data-required="true" style="float:left;max-width:300px;"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;

class RepairTag extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RepairTag', 'api/v1/repairtag', repairTagTemplate);
        this.reportOptions.HasDownloadExcel = false;
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
    beforeValidate($browse, $form, request) {
        //const validationName = request.module;
        //request.uniqueids = {};

        //switch (validationName) {
        //    case 'ContractValidation':
        //        request.uniqueids.ContractType = 'RETURN';
        //        break;
        //};
    };
    //----------------------------------------------------------------------------------------------
};
var RepairTagController: any = new RepairTag();