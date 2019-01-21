routes.push({
    pattern: /^reports\/picklistreport/, action: function (match: RegExpExecArray) {
        return RwPickListReportController.getModuleScreen();
    }
});

var pickListTemplateFrontEnd = `
    <div class="fwcontrol fwcontainer fwform fwreport picklistreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Pick List" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwPickListReportController">
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
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Pick List" data-datafield="PickListId" data-displayfield="PickListNumber" data-validationname="PickListValidation" style="float:left;max-width:300px;"></div>
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
class RwPickListReportClass extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('PickListReport', 'api/v1/picklistreport', pickListTemplateFrontEnd);
        //this.reportOptions.HasDownloadExcel = true;
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        let screen: any = {};
        screen.$view = FwModule.getModuleControl(`Rw${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        let $form = this.openForm();

        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () {
        };
        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm() {
        let $form = this.getFrontEnd();
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        var appOptions: any = program.getApplicationOptions();
        var request: any = { method: "LoadForm" };
    }
    //----------------------------------------------------------------------------------------------
};

var RwPickListReportController: any = new RwPickListReportClass();
//----------------------------------------------------------------------------------------------