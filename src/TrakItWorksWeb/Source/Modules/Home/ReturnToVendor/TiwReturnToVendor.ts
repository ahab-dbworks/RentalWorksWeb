class TiwReturnToVendor extends ReturnToVendor {
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
    <div id="returntovendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Return To Vendor" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="TiwReturnToVendorController">
      <div class="flexpage notabs">
        <div class="flexrow">
          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Order" style="margin-top:25px;">
            <div class="flexrow">
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidate" style="flex:1 1 125px;"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:1 1 175px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displyfield="Vendor" data-validationname="VendorValidation" style="flex:2 1 250px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:2 1 250px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="Date" style="flex:1 1 125px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="time" class="fwcontrol fwformfield" data-caption="Time" data-datafield="Time" style="flex:0 1 125px;" data-enabled="false"></div>
            </div>
          </div>
        </div>
        <div class="flexrow">
          <div class="flexcolumn" style="flex:1 1 600px;">
            <div class="POReturnItemGrid" style="border:1px solid black;min-height:300px;margin:10px;">##### ADD RETURN ITEMS GRID HERE #####
              <div data-control="FwGrid" data-grid="POReturnItemGrid" data-securitycaption="Return Items" style="border:1px solid black;min-height:250px;margin:10px;"></div>
            </div>
          </div>
          <div class="flexcolumn" style="flex:1 1 250px;">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code / Serial Number" data-datafield="BarCode"></div>
            <div class="errormsg"></div>
            <div class="POReturnBarCodeGrid" style="border:1px solid black;min-height:248px;margin:10px;">## ADD BAR CODE ITEMS GRID HERE ##
              <div data-control="FwGrid" data-grid="POReturnBarCodeGrid" data-securitycaption="Bar Code Items" style="border:1px solid black;min-height:200px;margin:10px;"></div>
            </div>
          </div>
        </div>
        <div class="flexrow">
          <div class="selectall fwformcontrol" data-type="button" style="flex:0 1 90px;margin-left:10px;">Select All</div>
          <div class="selectnone fwformcontrol" data-type="button" style="flex:0 1 100px;margin-left:10px;">Select None</div>
          <div class="createcontract fwformcontrol" data-type="button" style="flex:0 1 145px;margin-left:10px;">Create Contract</div>
        </div>
      </div>
    </div>`;
  }
}
var TiwReturnToVendorController = new TiwReturnToVendor();
