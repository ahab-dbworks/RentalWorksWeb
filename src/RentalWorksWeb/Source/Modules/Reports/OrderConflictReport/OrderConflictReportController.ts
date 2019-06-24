routes.push({
    pattern: /^reports\/OrderConflictReport/, action: function (match: RegExpExecArray) {
        return OrderConflictReportController.getModuleScreen();
    }
});

const orderConflictTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="OrderConflictReportController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" style="display:flex;flex-wrap:wrap;" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Date Range">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield date-field" data-caption="From:" data-datafield="FromDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield date-field" data-caption="To:" data-datafield="ToDate" data-required="true" style="float:left;max-width:200px;"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inventory Type">
                <div data-datafield="AvailableFor" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="R" data-caption="Rental"></div>
                  <div data-value="S" data-caption="Sales"></div>
                  <div data-value="" data-caption="All"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:200px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Conflict Type">
                <div data-datafield="ConflictType" data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="">
                  <div data-value="P" data-caption="Positive"></div>
                  <div data-value="N" data-caption="Negative"></div>
                  <div data-value="" data-caption="All"></div>
                </div>
              </div>
            </div>
            <div class="flexcolumn" style="max-width:600px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filters">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="float:left;min-width:400px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="multiselectvalidation" class="fwcontrol fwformfield" data-caption="Order" data-datafield="OrderId" data-formbeforevalidate="beforeValidate" data-displayfield="Order" data-validationname="OrderValidation" data-validationpeek="false" style="float:left;min-width:400px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
`;
//----------------------------------------------------------------------------------------------
class OrderConflictReport extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('OrderConflictReport', 'api/v1/OrderConflictReport', orderConflictTemplate);
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

        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        FwFormField.setValue($form, 'div[data-datafield="WarehouseId"]', warehouse.warehouseid, warehouse.warehouse);
        const today = FwFunc.getDate();
        FwFormField.setValueByDataField($form, 'FromDate', today);

        $form.find('.date-field').on('changeDate', event => {
            this.dateValidation($form, event);
        });
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate = function ($browse, $form, request) {
        const validationName = request.module;
        if (validationName != null) {
            const dealId = FwFormField.getValueByDataField($form, 'DealId');
            request.uniqueids = {};

            switch (validationName) {
                case 'OrderValidation':
                    if (dealId !== "") {
                        request.uniqueids.DealId = dealId;
                    }
                    break;
            }
        }
    }
    //----------------------------------------------------------------------------------------------
    dateValidation = function ($form, event) {
        const $element = jQuery(event.currentTarget);
        const todayParsed = Date.parse(FwFunc.getDate());
        const parsedFromDate = Date.parse(FwFormField.getValueByDataField($form, 'FromDate'));
        const parsedToDate = Date.parse(FwFormField.getValueByDataField($form, 'ToDate'));

        if ($element.attr('data-datafield') === 'FromDate' && parsedFromDate < todayParsed) {
            $form.find('div[data-datafield="FromDate"]').addClass('error date-validation');
            FwNotification.renderNotification('WARNING', "Your chosen 'From Date' is before Today's Date.");
        }
        else if (parsedToDate < parsedFromDate) {
            $form.find('div[data-datafield="ToDate"]').addClass('error date-validation');
            FwNotification.renderNotification('WARNING', "Your chosen 'To Date' is before 'From Date'.");
        }
        else {
            $form.find('div[data-datafield="FromDate"]').removeClass('error date-validation');
            $form.find('div[data-datafield="ToDate"]').removeClass('error date-validation');
        }
    }
    //----------------------------------------------------------------------------------------------
};

var OrderConflictReportController: any = new OrderConflictReport();