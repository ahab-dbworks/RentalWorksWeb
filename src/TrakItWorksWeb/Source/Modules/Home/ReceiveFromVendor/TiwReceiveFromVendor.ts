﻿class TiwReceiveFromVendor extends ReceiveFromVendor {
  constructor() {
    super();
    this.id = 'EC4052D5-664E-4C34-8802-78E086920628';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="receivefromvendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Receive From Vendor" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ReceiveFromVendorController">
      <div id="receivefromvendorform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabpages">
          <div class="flexpage">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Receive From Vendor">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 450px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidate" style="flex:0 1 175px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:0 1 175px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displyfield="Vendor" data-validationname="VendorValidation" style="flex:1 1 300px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="Date" style="flex:0 1 175px;" data-enabled="false"></div>
                    </div>
                  </div>
                </div>
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 450px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Packing Slip" data-datafield="PackingSlip" style="flex:0 1 175px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 350px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 1 100px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="time" class="fwcontrol fwformfield" data-caption="Time" data-datafield="Time" style="flex:0 1 175px;" data-enabled="false"></div>
                    </div>
                    <div class="errormsg"></div>
                  </div>
                </div>
                <div class="flexrow POReceiveItemGrid">
                  <div data-control="FwGrid" data-grid="POReceiveItemGrid" data-securitycaption="Receive Items"></div>
                </div>
                <div class="formrow">
                  <div class="optiontoggle fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                  <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                  <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
                  <div class="createcontract fwformcontrol" data-type="button" style="float:right;">Create Contract</div>
                  <div class="createcontract fwformcontrol" data-type="btnmenu" style="display:none; float:right;" data-caption="Create Contract"></div>
                </div>
              </div>
            </div>
            <div class="flexrow">
              <div class="flexcolumn" style="flex:1 1 450px;">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="checkbox" class="options fwcontrol fwformfield" data-caption="Automatically Create CHECK-OUT Contract for Sub Rental and Sub Sale items received" data-datafield="AutomaticallyCreateCheckOut" style="flex:1 1 150px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwReceiveFromVendorController = new TiwReceiveFromVendor();
