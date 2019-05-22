routes.push({
    pattern: /^reports\/picklistreport/, action: function (match: RegExpExecArray) {
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
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;

//----------------------------------------------------------------------------------------------
class PickListReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('PickListReport', 'api/v1/picklistreport', pickListTemplate);
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

var PickListReportController: any = new PickListReport();
//----------------------------------------------------------------------------------------------