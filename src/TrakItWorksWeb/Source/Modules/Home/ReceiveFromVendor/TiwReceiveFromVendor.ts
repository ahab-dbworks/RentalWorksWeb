class TiwReceiveFromVendor extends ReceiveFromVendor {
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
    <div id="receivefromvendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Receive From Vendor" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="TiwReceiveFromVendorController">
      <div class="flexpage notabs">

        <div class="flexrow">
          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Receive From Vendor">
            <div class="flexrow">
              <div class=flexcolumn" style="flex:1 1 150px;">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidate" style="flex:1 1 150px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:0 1 175px;"></div>
                </div>                
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Packing Slip" data-datafield="PackingSlip" style="flex:1 1 150px;"></div>
                </div>  
              </div>    
              <div class=flexcolumn" style="flex:2 1 575px;">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displyfield="Vendor" data-validationname="VendorValidation" style="flex:2 1 400px;" data-enabled="false"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 1 150px;" data-enabled="false"></div>                  
                </div>                
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:2 1 550px;" data-enabled="false"></div>              
                </div> 
              </div>    
              <div class=flexcolumn" style="flex:1 1 125px;">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="Date" style="flex:1 1 150px;" data-enabled="false"></div>              
                </div>                
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="time" class="fwcontrol fwformfield" data-caption="Time" data-datafield="Time" style="flex:1 1 150px;" data-enabled="false"></div>              
                </div> 
              </div>
            </div>
            <div class="flexrow errormsg"></div>
          </div>
        </div>
        <div class="flexrow POReceiveItemGrid" style="border:1px solid black;min-height:250px;margin:10px;">##### ADD RECEIVE ITEMS GRID HERE #####
          <div data-control="FwGrid" data-grid="POReceiveItemGrid" data-securitycaption="Receive Items"></div>
        </div>
        <div class="flexrow">
          <div class="optiontoggle fwformcontrol" data-type="button" style="flex:0 1 90px;margin-left:10px;">Options &#8675;</div>
          <div class="selectall fwformcontrol" data-type="button" style="flex:0 1 90px;margin-left:10px;">Select All</div>
          <div class="selectnone fwformcontrol" data-type="button" style="flex:0 1 100px;margin-left:10px;">Select None</div>
          <div class="createcontract fwformcontrol" data-type="button" style="flex:0 1 140px;margin-left:10px;">Create Contract</div>
          <div class="createcontract fwformcontrol" data-type="btnmenu" style="display:none;flex:0 1 140px;" data-caption="Create Contract"></div>
        </div>
        <div class="flexrow">
          <div data-control="FwFormField" data-type="checkbox" class="options fwcontrol fwformfield" data-caption="Automatically Create CHECK-OUT Contract for Sub Rental and Sub Sale items received" data-datafield="AutomaticallyCreateCheckOut" style="flex:1 1 150px;"></div>
        </div>
      </div>
    </div>`;
  }
}
var TiwReceiveFromVendorController = new TiwReceiveFromVendor();
