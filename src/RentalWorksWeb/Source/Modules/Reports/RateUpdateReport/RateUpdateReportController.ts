routes.push({
    pattern: /^reports\/rateupdatereport/, action: function (match: RegExpExecArray) {
        return RateUpdateReportController.getModuleScreen();
    }
});

const rateUpdateTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Rate Update Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RateUpdateReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:300px;">
               <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rate Update">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending Modifications Only" data-datafield="PendingModificationsOnly" data-required="true" style="float:left;max-width:300px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rate Update Batch Number" data-datafield="RateUpdateBatchId" data-displayfield="RateUpdateBatchNumber" data-savesetting="false" data-required="true" data-validationname="RateUpdateBatchValidation" style="float:left;max-width:300px;"></div>
                </div>
              </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;

//----------------------------------------------------------------------------------------------
class RateUpdateReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('RateUpdateReport', 'api/v1/rateupdatereport', rateUpdateTemplate);
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

        $form.on('change', '[data-datafield="PendingModificationsOnly"]', e => {
            const val = FwFormField.getValueByDataField($form, 'PendingModificationsOnly');
            if (val) {
                FwFormField.disable($form.find('[data-datafield="RateUpdateBatchId"]'));
            } else {
                FwFormField.enable($form.find('[data-datafield="RateUpdateBatchId"]'));
            }
        });

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
            case 'RateUpdateBatchId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebatch`);
                break;
        }
    }
};

var RateUpdateReportController: any = new RateUpdateReport();
//----------------------------------------------------------------------------------------------