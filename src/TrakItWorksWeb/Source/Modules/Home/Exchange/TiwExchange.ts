﻿class TiwExchange extends Exchange {
  constructor() {
    super();
    this.id = '76A62932-CBBA-403E-8BF2-0C2283BBAD8D';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="exchangeform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Exchange" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ExchangeController">
          <div id="exchangeform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
            </div>
            <div class="tabpages">
              <div class="flexpage">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="flex:1 1 175px;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:1 1 125px;" data-formbeforevalidate="beforeValidateOrder"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 250px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 200px;" data-enabled="false"></div>
                  </div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-In">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield in" data-caption="Bar Code / Serial No. / I-Code" data-datafield="BarCodeIn" style="flex:0 1 200px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICodeIn" style="flex:1 1 125px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DescriptionIn" style="flex:1 1 250px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseIdIn" data-displayfield="WarehouseIn" data-validationname="WarehouseValidation" style="flex:1 1 125px;" data-enabled="false"></div>
                  </div>
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="" style="flex:0 1 200px;visibility:hidden;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorIdIn" data-displayfield="VendorIn" data-validationname="VendorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderIdIn" data-displayfield="PoNumberIn" data-validationname="PurchaseOrderValidation" style="flex:1 1 50px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Consignor" data-datafield="ConsignorIdIn" data-displayfield="ConsignorIn" data-validationname="ConsignorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                  </div>
                  <div class="flexrow error-msg-in"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-Out">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield out" data-caption="Bar Code / Serial No. / I-Code" data-datafield="BarCodeOut" style="flex:0 1 200px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICodeOut" style="flex:1 1 125px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DescriptionOut" style="flex:1 1 250px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseIdOut" data-displayfield="WarehouseOut" data-validationname="WarehouseValidation" style="flex:1 1 125px;" data-enabled="false"></div>
                  </div>
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="" style="flex:0 1 200px;visibility:hidden;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorIdOut" data-displayfield="VendorOut" data-validationname="VendorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderIdOut" data-displayfield="PoNumberOut" data-validationname="PurchaseOrderValidation" style="flex:1 1 50px;" data-enabled="false"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Consignor" data-datafield="ConsignorIdOut" data-displayfield="ConsignorOut" data-validationname="ConsignorValidation" style="flex:1 1 150px;" data-enabled="false"></div>
                  </div>
                  <div class="flexrow error-msg-out"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Exchange">
                  <div class="flexcolumn" style="max-width:1600px">
                    <div class="flexrow" style="max-width:1600px">
                      <div data-control="FwGrid" data-grid="ExchangeItemGrid" data-securitycaption="Exchange Item"></div>
                    </div>
                    <div class="formrow" style="max-width:1600px;margin-left:auto;">
                      <div class="createcontract fwformcontrol" data-type="button" style="flex: 0 1 140px;">Create Contract</div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
  }
}
var TiwExchangeController = new TiwExchange();
