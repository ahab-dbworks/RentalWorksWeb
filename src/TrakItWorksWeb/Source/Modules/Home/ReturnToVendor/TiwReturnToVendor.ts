﻿class TiwReturnToVendor extends ReturnToVendor {
  constructor() {
    super();
    this.id = '79EAD1AF-3206-42F2-A62B-DA1C44092A7F';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="returntovendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Return To Vendor" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ReturnToVendorController">
      <div id="returntovendorform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabpages">
          <div class="flexpage">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Return To Vendor">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:0 1 735px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidate" style="flex:0 1 175px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:0 1 175px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displyfield="Vendor" data-validationname="VendorValidation" style="flex:1 1 300px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="Date" style="flex:0 1 125px;" data-enabled="false"></div>
                    </div>
                    <div class="flexrow" style="margin-left:174px;">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 100px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="time" class="fwcontrol fwformfield" data-caption="Time" data-datafield="Time" style="flex:0 1 125px;" data-enabled="false"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="flexrow" style="max-width:1800px;">
              <div class="flexcolumn" style="max-width:1225px;">
                <div class="POReturnItemGrid">
                  <div data-control="FwGrid" data-grid="POReturnItemGrid" data-securitycaption="Return Items"></div>
                </div>
              </div>
              <div class="flexcolumn" style="max-width: 575px;">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code / Serial Number" data-datafield="BarCode" style="width:200px;"></div>
                <div class="errormsg"></div>
                <div class="POReturnBarCodeGrid" data-control="FwGrid" data-grid="POReturnBarCodeGrid" data-securitycaption="Bar Code Items"></div>
              </div>
            </div>
            <div class="formrow" style="max-width:1200px;">
              <div class="selectall fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select All</div>
              <div class="selectnone fwformcontrol" data-type="button" style="float:left; margin-left:10px;">Select None</div>
              <div class="createcontract fwformcontrol" data-type="button" style="float:right;">Create Contract</div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwReturnToVendorController = new TiwReturnToVendor();
