routes.push({
    pattern: /^reports\/picklistreport/,
    action: function (match) {
        return PickListReportController.getModuleScreen();
    }
});
const pickListTemplate = `
    <div class="fwcontrol fwcontainer fwform fwreport picklistreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Pick List" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PickListReportController">
      <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs" style="margin-right:10px;">
          <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
        </div>
        <div class="tabpages">
          <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
            <div class="formpage">
              <div class="row" style="display:flex;flex-wrap:wrap;">
                <div class="flexcolumn" style="max-width:300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Pick List">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Pick List" data-savesetting="false" data-datafield="PickListId" data-displayfield="PickListNumber" data-validationname="PickListValidation" style="float:left;max-width:300px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="New Page for each Inventory Type" data-datafield="NewPagePerType"></div>
                    </div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OrderType" data-savesetting="false" style="display:none;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="BarCodeStyle" data-savesetting="false" style="display:none;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
class PickListReport extends FwWebApiReport {
    constructor() {
        super('PickListReport', 'api/v1/picklistreport', pickListTemplate);
        this.reportOptions.HasDownloadExcel = false;
    }
    getModuleScreen() {
        const screen = {};
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
    openForm() {
        const $form = this.getFrontEnd();
        $form.find('div[data-datafield="PickListId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="OrderType"]', $tr.find('.field[data-formdatafield="OrderType"]').attr('data-originalvalue'));
        });
        return $form;
    }
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        const barCodeStyle = JSON.parse(sessionStorage.getItem('controldefaults')).documentbarcodestyle;
        FwFormField.setValue($form, 'div[data-datafield="BarCodeStyle"]', barCodeStyle);
    }
    convertParameters(parameters) {
        return parameters;
    }
}
;
var PickListReportController = new PickListReport();
//# sourceMappingURL=PickListReportController.js.map