﻿class TiwAssignBarCodes extends AssignBarCodes {
  constructor() {
    super();
    this.id = '81B0D93C-9765-4340-8B40-63040E0343B8';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="assignbarcodesform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Assign Bar Codes" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="AssignBarCodesController">
      <div id="checkinform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs">
        </div>
        <div class="tabpages">
          <div class="flexpage">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Assign Bar Codes">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidatePONumber" style="float:left; flex:0 1 200px;"></div>
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PODate" style="float:left; flex:0 1 150px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" style="float:left; flex:0 1 250px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="float:left; flex:1 1 250px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left; flex:0 1 225px;" data-enabled="false"></div>
                </div>
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contract No." data-datafield="ContractId" data-displayfield="ContractNumber" data-validationname="ContractValidation" data-formbeforevalidate="beforeValidateContractNumber" style="float:left; flex:0 1 200px;"></div>
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Contract Date" data-datafield="ContractDate" style="float:left;  flex:0 1 150px;" data-enabled="false"></div>
                </div>
                <div class="flexrow" style="min-width:1400px;">
                  <div data-control="FwGrid" data-grid="POReceiveBarCodeGrid" data-securitycaption="Purchase Order Receive Bar Code"></div>
                </div>
                <div class="formrow" style="min-width:1400px;">
                  <div class="fwformcontrol assignbarcodes" data-type="button" style="float:left;">Assign Bar Codes</div>
                  <div class="fwformcontrol additems" data-type="button" style="float:right;">Add Items</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwAssignBarCodesController = new TiwAssignBarCodes();
