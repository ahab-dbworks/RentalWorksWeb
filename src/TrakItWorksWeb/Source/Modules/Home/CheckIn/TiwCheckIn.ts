﻿class TiwCheckIn extends CheckIn {
  constructor() {
    super();
    this.id = '3D1EB9C4-95E2-440C-A3EF-10927C4BDC65';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="checkinform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Check-In" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="CheckInController">
      <div id="checkinform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs">
          <div data-type="tab" id="checkintab" class="checkintab tab" data-tabpageid="checkintabpage" data-caption="Check-In"></div>
          <div data-type="tab" id="orderstab" class="orderstab tab" data-tabpageid="orderstabpage" data-caption="Orders" style="display:none;"></div>
          <div data-type="tab" id="quantityitemstab" class="quantityitemstab tab" data-tabpageid="quantityitemstabpage" data-caption="Quantity Items"></div>
          <div data-type="tab" id="swapitemtab" class="swapitemtab tab" data-tabpageid="swapitemtabpage" data-caption="Swapped Items" style="display:none;"></div>
          <div data-type="tab" id="exceptionstab" class="exceptionstab tab" data-tabpageid="exceptionstabpage" data-caption="Exceptions"></div>
        </div>
        <div class="tabpages">
          <div data-type="tabpage" id="checkintabpage" class="tabpage" data-tabid="checkintab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Check-In">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 450px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ContractId" data-datafield="ContractId" style="display:none; flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderId" data-formbeforevalidate="beforeValidate" data-displayfield="OrderNumber" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 250px;" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 450px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" style="flex:0 1 350px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:0 1 200px;" data-enabled="false"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 850px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code / I-Code" data-datafield="BarCode" style="flex:1 1 300px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="ICode" style="flex:1 1 300px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="InventoryDescription" style="flex:1 1 400px;" data-enabled="false"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 850px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" style="flex:0 1 100px; margin-right:256px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ordered" data-datafield="QuantityOrdered" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub" data-datafield="QuantitySub" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Out" data-datafield="QuantityOut" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Staged" data-datafield="QuantityStaged" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="In" data-datafield="QuantityIn" style="flex:0 1 100px;" data-enabled="false"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="QuantityRemaining" style="flex:0 1 100px;" data-enabled="false"></div>
                      </div>
                    </div>
                  </div>
                  <div class="errormsg"></div>
                  <div class="fwformcontrol addordertocontract" data-type="button" style="display:none; flex:0 1 150px;margin:15px 0 0 10px;text-align:center;">Add Order To Contract</div>
                  <div class="fwformcontrol swapitem" data-type="button" style="display:none; flex:0 1 150px;margin:15px 0 0 10px;text-align:center;">Swap Item</div>
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CheckedInItemGrid" data-securitycaption=""></div>
                  </div>
                  <div class="formrow">
                    <div class="fwformcontrol orderstatus" data-type="button" style="float:left; margin-left:10px;">Order Status</div>
                    <div class="fwformcontrol createcontract" data-type="button" style="float:right;">Create Contract</div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div data-type="tabpage" id="orderstabpage" class="tabpage" data-tabid="orderstab">
            <div class="flexpage">
              <div class="flexrow">
                <div data-control="FwGrid" data-grid="CheckInOrderGrid" data-securitycaption=""></div>
              </div>
            </div>
          </div>
          <div data-type="tabpage" id="quantityitemstabpage" class="tabpage" data-tabid="quantityitemstab">
            <div class="flexpage">
              <div class="flexrow">
                <div data-control="FwGrid" data-grid="CheckInQuantityItemsGrid" data-securitycaption=""></div>
              </div>
              <div class="formrow">
                <div class="fwformcontrol optionsbutton" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Select None</div>
              </div>
              <div class="flexrow optionlist" style="display:none;">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show all ACTIVE Orders for this Deal" data-datafield="AllOrdersForDeal" style="flex:0 1 350px;"></div>
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Specific Order" data-datafield="SpecificOrder" style="flex:0 1 150px;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="SpecificOrderId" data-displayfield="SpecificOrderNumber" data-validationname="OrderValidation" data-formbeforevalidate="beforeValidateSpecificOrder" style="flex:0 1 175px;" data-enabled="false"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="SpecificDescription" style="flex:1 1 250px;" data-enabled="false"></div>
              </div>
              <div class="flexrow optionlist" style="display:none;">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only show items with Quantity Out" data-datafield="ShowQuantityOut"></div>
              </div>
            </div>
          </div>
          <div data-type="tabpage" id="swapitemtabpage" class="tabpage" data-tabid="swapitemtab">
            <div class="flexpage">
              <div class="flexrow">
                <div data-control="FwGrid" data-grid="CheckInSwapGrid" data-securitycaption=""></div>
              </div>
            </div>
          </div>
          <div data-type="tabpage" id="exceptionstabpage" class="tabpage" data-tabid="exceptionstab">
            <div class="flexpage">
              <div class="flexrow">
                <div data-control="FwGrid" data-grid="CheckInExceptionGrid" data-securitycaption=""></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwCheckInController = new TiwCheckIn();
