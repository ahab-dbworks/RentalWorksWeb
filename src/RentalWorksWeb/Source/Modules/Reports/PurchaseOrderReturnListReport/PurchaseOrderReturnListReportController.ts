routes.push({
    pattern: /^reports\/purchaseorderreturnlist/, action: function (match: RegExpExecArray) {
        return PurchaseOrderReturnListController.getModuleScreen();
    }
});

const purchaseOrderReturnListTemplate = `
<div class="fwcontrol fwcontainer fwform fwreport" data-control="FwContainer" data-type="form" data-version="1" data-caption="Purchase Order Return List" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PurchaseOrderReturnListController">
  <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs" style="margin-right:10px;">
      <div id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="formpage">
          <div class="row" style="display:flex;flex-wrap:wrap;">
            <div class="flexcolumn" style="max-width:500px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor Return List">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Purchase Order" data-savesetting="false" data-required="true" data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" style="float:left;max-width:300px;"></div>
                </div>
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-savesetting="false" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;max-width:300px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="BarCodeStyle" data-savesetting="false" style="display:none;"></div>
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
class PurchaseOrderReturnList extends FwWebApiReport {
    //----------------------------------------------------------------------------------------------
    constructor() {
        super('PurchaseOrderReturnList', 'api/v1/purchaseorderreturnlist', purchaseOrderReturnListTemplate);
        this.reportOptions.HasDownloadExcel = true;
        this.designerProvisioned = true;
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

        const barCodeStyle = JSON.parse(sessionStorage.getItem('controldefaults')).documentbarcodestyle;
        FwFormField.setValue($form, 'div[data-datafield="BarCodeStyle"]', barCodeStyle);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
    }
    //----------------------------------------------------------------------------------------------
    convertParameters(parameters: any) {
        return parameters;
    }
    //----------------------------------------------------------------------------------------------
};

var PurchaseOrderReturnListController: any = new PurchaseOrderReturnList();