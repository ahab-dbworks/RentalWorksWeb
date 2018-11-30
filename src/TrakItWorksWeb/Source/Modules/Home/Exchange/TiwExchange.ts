﻿class TiwExchange extends Exchange {
  constructor() {
    super();
    this.id = 'F9012ABC-B97E-433B-A604-F1DADFD6D7B7';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="exchangeform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Exchange" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="TiwExchangeController">
      <div class="flexpage notabs">
        <div class="flexrow">
          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order / Deal">
            <div class="flexrow">
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:1 1 125px;" data-formbeforevalidate="beforeValidateOrder"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:2 1 250px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="flex:2 1 250px;"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 200px;" data-enabled="false"></div>
            </div>
          </div>
        </div>
        <div class="flexrow">  
          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-In">
            <div class="flexrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield in" data-caption="Bar Code / Serial No. / I-Code" data-datafield="BarCodeIn" style="flex:1 1 225px;"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICodeIn" style="flex:1 1 150px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DescriptionIn" style="flex:2 1 350px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="QuantityIn" style="flex:1 1 75px;" data-enabled="false"></div>
            </div>
            <div class="flexrow">
              <!--
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="" style="flex:0 1 200px;visibility:hidden;"></div>
              -->
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor / Consignor" data-datafield="VendorIdIn" data-displayfield="VendorIn" data-validationname="VendorValidation" style="flex:2 1 350px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderIdIn" data-displayfield="PoNumberIn" data-validationname="PurchaseOrderValidation" style="flex:1 1 125px;" data-enabled="false"></div>
              <!--
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Consignor" data-datafield="ConsignorIdIn" data-displayfield="ConsignorIn" data-validationname="ConsignorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
              -->
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseIdIn" data-displayfield="WarehouseIn" data-validationname="WarehouseValidation" style="flex:2 1 250px;" data-enabled="false"></div>
            </div>
            <div class="flexrow error-msg-in" style="margin:10px 0 10px 0;"></div>
          </div>
        </div>
        <div class="flexrow">
          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-Out">
            <div class="flexrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield out" data-caption="Bar Code / Serial No. / I-Code" data-datafield="BarCodeOut" style="flex:1 1 225px;"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICodeOut" style="flex:1 1 150px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DescriptionOut" style="flex:2 1 350px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="QuantityIn" style="flex:1 1 75px;" data-enabled="false"></div>
            </div>
            <div class="flexrow">
              <!--
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="" style="flex:0 1 200px;visibility:hidden;"></div>
              -->
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorIdOut" data-displayfield="VendorOut" data-validationname="VendorValidation" style="flex:2 1 350px;" data-enabled="false"></div>
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderIdOut" data-displayfield="PoNumberOut" data-validationname="PurchaseOrderValidation" style="flex:1 1 125px;" data-enabled="false"></div>
              <!--
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Consignor" data-datafield="ConsignorIdOut" data-displayfield="ConsignorOut" data-validationname="ConsignorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
              -->
              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseIdOut" data-displayfield="WarehouseOut" data-validationname="WarehouseValidation" style="flex:2 1 250px;" data-enabled="false"></div>                    
            </div>
            <div class="flexrow error-msg-out"></div>
          </div>
        </div>                  
        <div class="flexrow">
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
          <div class="flexcolumn">########## ADD EXCHANGE ITEM GRID HERE ##########
            <div class="flexrow" style="border:1px solid black;min-height:250px;margin:10px;">
              <div data-control="FwGrid" data-grid="ExchangeItemGrid" data-securitycaption="Exchange Item"></div>
            </div>
            <div class="flexrow">
              <div class="createcontract fwformcontrol" data-type="button" style="flex: 0 1 140px;">Create Contract</div>
            </div>
          </div>
        </div>
        </div>
      </div>
     </div>`;
  }
}
var TiwExchangeController = new TiwExchange();
