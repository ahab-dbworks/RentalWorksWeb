routes.push({ pattern: /^reports\/latereturnduebackreport$/, action: function (match) { return RwLateReturnDueBackReportController.getModuleScreen(); } });
var lateReturnDueBackFrontEnd = `
    <div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Late Return / Due Back" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RwLateReturnDueBackReportController">
      <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs" style="margin-right:10px;">
          <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
          <div id="exporttab" class="tab exporttab" data-tabpageid="exporttabpage" data-caption="Export"></div>
        </div>
        <div class="tabpages">
          <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
            <div class="formpage">
              <div class="row" style="display:flex;flex-wrap:wrap;">
                <div class="flexcolumn" style="max-width:400px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Report Type">
                    <div class="flexrow">
                      <div class="flexcolumn" style="max-width:150px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="LateReturns" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Late Returns" style="float:left;max-width:100px;padding-top:8px"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBack" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Due Back" style="float:left;max-width:100px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBackOn" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Due Back on" style="float:left;max-width:100px;"></div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="max-width:150px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DaysPastDue" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield late" data-caption="Days Past Due" style="float:left;max-width:100px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBackFewer" data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dueback" data-caption="or Fewer Days" style="float:left;max-width:100px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-datafield="DueBackDate" data-control="FwFormField" data-type="date" class="fwcontrol fwformfield duebackon" data-caption="" style="float:left;max-width:150px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="max-width:250px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-datafield="ShowUnit" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Unit Value" style="float:left;max-width:420px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-datafield="ShowReplacement" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Replacement Cost" style="float:left;max-width:420px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-datafield="ShowBarCode" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Bar Codes" style="float:left;max-width:420px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-datafield="ShowSerial" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Serial Numbers" style="float:left;max-width:420px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-datafield="ShowRfid" data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show RFIDs" style="float:left;max-width:420px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="max-width:300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;max-width:300px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;max-width:300px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="float:left;max-width:300px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidate" style="float:left;max-width:300px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Inventory Type" data-datafield="InventoryTypeId" data-displayfield="InventoryType" data-formbeforevalidate="beforeValidate" data-validationname="InventoryTypeValidation" style="float:left;max-width:300px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Ordered By" data-datafield="ContactId" data-displayfield="Contact" data-validationname="ContactValidation" style="float:left;max-width:300px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div id="exporttabpage" class="tabpage exporttabpage" data-tabid="exporttab"></div>
        </div>
      </div>
    </div>`;
class RwLateReturnDueBackReport extends FwWebApiReport {
    constructor() {
        super('LateReturnsReport', 'api/v1/latereturnsreport', lateReturnDueBackFrontEnd);
    }
    getModuleScreen() {
        var screen, $form;
        screen = {};
        screen.$view = FwModule.getModuleControl('Rw' + this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $form = this.openForm();
        screen.load = function () {
            FwModule.openModuleTab($form, $form.attr('data-caption'), false, 'REPORT', true);
        };
        screen.unload = function () {
        };
        return screen;
    }
    ;
    openForm() {
        let $form = this.getFrontEnd();
        $form.data('getexportrequest', request => {
            request.parameters = this.getParameters($form);
            return request;
        });
        return $form;
    }
    ;
    onLoadForm($form) {
        this.load($form, this.reportOptions);
        var appOptions = program.getApplicationOptions();
        var request = { method: "LoadForm" };
        var lateReturn, dueBack, dueBackOn;
        let today = FwFunc.getDate();
        const department = JSON.parse(sessionStorage.getItem('department'));
        const location = JSON.parse(sessionStorage.getItem('location'));
        lateReturn = $form.find('div[data-datafield="LateReturns"] input');
        dueBack = $form.find('div[data-datafield="DueBack"] input');
        dueBackOn = $form.find('div[data-datafield="DueBackOn"] input');
        FwFormField.setValueByDataField($form, 'DaysPastDue', 1);
        FwFormField.setValueByDataField($form, 'DueBackFewer', 0);
        FwFormField.setValueByDataField($form, 'DueBackDate', today);
        lateReturn.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                dueBack.prop('checked', false);
                dueBackOn.prop('checked', false);
                FwFormField.disable($form.find('.duebackon'));
                FwFormField.disable($form.find('.dueback'));
                FwFormField.enable($form.find('.late'));
            }
        });
        dueBack.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lateReturn.prop('checked', false);
                dueBackOn.prop('checked', false);
                FwFormField.disable($form.find('.late'));
                FwFormField.disable($form.find('.duebackon'));
                FwFormField.enable($form.find('.dueback'));
            }
        });
        dueBackOn.on('change', (e) => {
            if (jQuery(e.currentTarget).prop('checked')) {
                lateReturn.prop('checked', false);
                dueBack.prop('checked', false);
                FwFormField.disable($form.find('.late'));
                FwFormField.disable($form.find('.dueback'));
                FwFormField.enable($form.find('.duebackon'));
            }
        });
        FwFormField.setValue($form, 'div[data-datafield="DepartmentId"]', department.departmentid, department.department);
        FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', location.locationid, location.location);
    }
    ;
    beforeValidate($browse, $form, request) {
        const validationName = request.module;
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        request.uniqueids = {};
        switch (validationName) {
            case 'DealValidation':
                if (customerId != '') {
                    request.uniqueids.CustomerId = customerId;
                }
                break;
            case 'InventoryTypeValidation':
                request.uniqueids.Rental = true;
        }
        ;
    }
    ;
}
;
var RwLateReturnDueBackReportController = new RwLateReturnDueBackReport();
//# sourceMappingURL=RwLateReturnDueBackReportController.js.map