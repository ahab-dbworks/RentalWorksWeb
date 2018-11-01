﻿class TiwStagingCheckout extends StagingCheckout {
  constructor() {
    super();
    this.id = 'AD92E203-C893-4EB9-8CA7-F240DA855827';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="stagingcheckoutform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Staging / Check-Out" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="StagingCheckoutController">
      <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs">
          <div data-type="tab" id="stagingtab" class="tab staging-tab" data-tabpageid="stagingtabpage" data-caption="Staging"></div>
          <div data-type="tab" id="quantityitemtab" class="tab quantity-items-tab" data-tabpageid="quantityitemtabpage" data-caption="Quantity Items"></div>
        </div>
        <div class="tabpages">
          <div data-type="tabpage" id="stagingtabpage" class="tabpage" data-tabid="stagingtab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Staging / Check-Out">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 850px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Order No." data-datafield="OrderId" data-displayfield="OrderNumber" data-formbeforevalidate="beforeValidate" data-validationname="OrderValidation" style="flex:0 1 175px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Description" data-datafield="Description" data-enabled="false" style="flex:1 1 300px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-enabled="false" style="flex:1 1 300px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Location" data-datafield="Location" data-enabled="false" style="flex:1 1 300px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield clearable" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-visible="false" data-enabled="false" style="flex:1 1 175px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="fwcontrol fwcontainer" data-control="FwContainer" data-type="section" data-caption="Items">
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 300px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Bar Code / I-Code" data-datafield="Code" style="flex:0 1 320px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield quantity clearable" data-caption="Quantity" data-datafield="Quantity" style="flex:0 1 100px;"></div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 850px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="I-Code" data-enabled="false" data-datafield="ICode" style="flex:1 1 300px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield clearable" data-caption="Description" data-enabled="false" data-datafield="InventoryDescription" style="flex:1 1 400px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Ordered" data-enabled="false" data-datafield="QuantityOrdered" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Sub" data-enabled="false" data-datafield="QuantitySub" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Out" data-enabled="false" data-datafield="QuantityOut" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Staged" data-enabled="false" data-datafield="QuantityStaged" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield clearable" data-caption="Remaining" data-enabled="false" data-datafield="QuantityRemaining" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield grid-view-radio" data-caption="" data-datafield="GridView" style="flex:1 1 250px;">
                            <div data-value="STAGE" class="staged-view" data-caption="View Staged" style="margin-top:-5px;"></div>
                            <div data-value="PENDING" class="pending-item-view" data-caption="View Pending Items" style="margin-top:-4px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow error-msg"></div>
                  <div class="formrow AddItemToOrder"></div>
                  <div class="flexrow">
                    <div class="flexcolumn summaryview">
                      <div class="flexrow staged-item-grid">
                          <div data-control="FwGrid" data-grid="StagedItemGrid" data-securitycaption="Staged Items"></div>
                        </div>
                        <div class="flexrow pending-item-grid">
                          <div class="pending-item-grid" data-control="FwGrid" data-grid="CheckOutPendingItemGrid" data-securitycaption=""></div>
                      </div>
                      <div class="flexrow original-buttons" style="display:flex;justify-content:space-between;">
                        <div class="orderstatus fwformcontrol" data-type="button" style="flex:0 1 109px; margin-left:8px;">Order Status</div>
                        <div class="createcontract" data-type="btnmenu" style="flex:0 1 200px;margin-right:7px;" data-caption="Create Contract"></div>
                      </div>
                    </div>
                    <div class="flexcolumn partial-contract" style="max-width:125px;justify-content:center;">
                      <button type="submit" class="dbl-angle right-arrow"><img src="theme/images/icons/integration/dbl-angle-right.svg" alt="Add" /></button>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield partial-contract-inputs partial-contract-barcode clearable" data-caption="Bar Code / I-Code" data-datafield="" style="margin-top:30px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield partial-contract-inputs partial-contract-quantity clearable" data-caption="Quantity" data-datafield="" style="max-width:72px;"></div>
                      <button type="submit" class="dbl-angle left-arrow" style="margin-top:40px;"><img src="theme/images/icons/integration/dbl-angle-left.svg" alt="Remove" /></button>
                    </div>
                    <div class="flexcolumn partial-contract">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CheckedOutItemGrid" data-securitycaption="Contract Items"></div>
                      </div>
                      <div class="flexrow" style="align-items:flex-end;">
                        <div class="fwformcontrol complete-checkout-contract" data-type="button" style="max-width:140px;">Create Contract</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!--Quantity Items Page-->
          <div data-type="tabpage" id="quantityitemtabpage" class="tabpage" data-tabid="quantityitemtab">
            <div class="flexpage">
              <div class="flexrow error-msg-qty"></div>
              <div class="flexrow">
                <div data-control="FwGrid" data-grid="StageQuantityItemGrid" data-securitycaption=""></div>
              </div>
              <div class="formrow">
                <div class="fwformcontrol options-button" data-type="button" style="float:left; margin-left:10px;">Options &#8675;</div>
                <div class="fwformcontrol selectall" data-type="button" style="float:left; margin-left:10px;">Select All</div>
                <div class="fwformcontrol selectnone" data-type="button" style="float:left; margin-left:10px;">Select None</div>
              </div>
              <div class="formrow option-list" style="display:none;">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show items with zero Remaining" data-datafield="IncludeZeroRemaining"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwStagingCheckoutController = new TiwStagingCheckout();
