routes.push({
    pattern: /^reports\/changeauditreport/, action: function (match: RegExpExecArray) {
        return ChangeAuditReportController.getModuleScreen();
    }
});

const changeAuditReportTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Change Audit Report" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="ChangeAuditReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" style="display:flex;flex-wrap:wrap;" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:350px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div class="flexcolumn">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="From:" data-datafield="FromDate" data-required="true"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="To:" data-datafield="ToDate" data-required="true"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield modules" data-caption="Module" data-datafield="ModuleName" style="min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="User" data-datafield="WebUsersId" data-displayfield="UserName" data-validationname="WebUserValidation" data-showinactivemenu="true" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Keyword, field name, value" data-datafield="Keyword"></div>
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
class ChangeAuditReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('ChangeAuditReport', 'api/v1/changeauditreport', changeAuditReportTemplate);
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

        // Load Modules dropdown with sorted list of Modules and Grids
        const modules = FwApplicationTree.getAllModules(false, false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasView: boolean, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('apiurl') && moduleController.Module != 'BlankHomePage') {
                if (hasNew || hasEdit) {
                    modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl });
                }
            }
        });
        const grids = FwApplicationTree.getAllGrids(false, (modules: any[], moduleCaption: string, moduleName: string, category: string, currentNode: any, nodeModule: IGroupSecurityNode, hasNew: boolean, hasEdit: boolean, moduleController: any) => {
            if (moduleController.hasOwnProperty('apiurl')) {
                if (hasNew || hasEdit) {
                    modules.push({ value: moduleName, text: moduleCaption, apiurl: moduleController.apiurl });
                }
            }
        });

        const allModules = modules.concat(grids);
        FwApplicationTree.sortModules(allModules);
        let $moduleSelect = $form.find('.modules');
        FwFormField.loadItems($moduleSelect, allModules);

        // Default settings for first time running
        const today = FwLocale.getDate();
        FwFormField.setValueByDataField($form, 'ToDate', today);
        const aMonthAgo = FwLocale.getDate(today, null, { Quantity: -1, ObjectModified: 'months' });
        FwFormField.setValueByDataField($form, 'FromDate', aMonthAgo);
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'WebUsersId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewebusers`);
        }
    }
};

var ChangeAuditReportController: any = new ChangeAuditReport();