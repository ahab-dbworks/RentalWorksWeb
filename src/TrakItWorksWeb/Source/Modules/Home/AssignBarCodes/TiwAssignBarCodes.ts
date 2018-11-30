class TiwAssignBarCodes extends AssignBarCodes {
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
    <div id="assignbarcodesform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Assign Bar Codes" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="TiwAssignBarCodesController">
      <div class="flexpage notabs">
        <div class="flexrow">
          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bar Codes / Serial Numbers" style="flex:1 1 750px;">
            <div class="flexrow">
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderId" data-displayfield="PurchaseOrderNumber" data-validationname="PurchaseOrderValidation" data-formbeforevalidate="beforeValidatePONumber" style="flex:1 1 125px;"></div>
              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PODate" style="flex:1 1 125px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" style="flex:2 1 350px;" data-enabled="false"></div>
            </div>
            <div class="flexrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:2 1 350px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;" data-enabled="false"></div>
            </div>
            <div class="flexrow" style="border:1px solid black;min-height:250px;margin-top:25px;">########## ADD ITEMS WITHOUT BAR CODES / SERIAL NUMBERS GRID ##########
              <div data-control="FwGrid" data-grid="POReceiveBarCodeGrid" data-securitycaption="Purchase Order Receive Bar Code"></div>
            </div>
            <div class="flexrow" style="margin-top:15px;">
              <div class="flexcolumn" style="flex:0 1 250px;">
                <div class="flexrow" style="margin-left:auto;margin-right:auto;">
                  <div class="fwformcontrol assignbarcodes" data-type="button" style="flex:0 1 100px;">Assign Bar Codes</div>
                </div>
              </div>
              <div class="flexcolumn" style="flex:0 1 125px;"></div>
              <div class="flexcolumn" style="flex:0 1 125px;"></div>
              <div class="flexcolumn" style="flex:0 1 250px;">
                <div class="flexrow" margin-left:auto;margin-right:auto;">
                  <div class="fwformcontrol additems" data-type="button" style="flex:0 1 100px;">Add Items</div>
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
