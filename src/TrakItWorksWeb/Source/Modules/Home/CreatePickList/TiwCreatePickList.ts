﻿class TiwCreatePickList extends CreatePickList {
  id = '1407A536-B5C9-4363-8B54-A56DB8CE902D';

  constructor() {
    super();
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getFormTemplate(): string {
    return `
    <div id="createpicklistform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Create Pick List" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="CreatePickListController">
      <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-saveorder="1" data-caption="" data-datafield="OrderId"></div>
      <div id="createpicklistform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs">
        </div>
        <div class="tabpages">
          <div class="formpage">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Create Pick List">
              <div class="formrow">
                <div class="formcolumn summaryview" style="width:100%;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="PickListUtilityGrid" data-securitycaption="Pick List Utility"></div>
                  </div>
                </div>
              </div>
              <div class="formrow">
                <div class="optiontoggle fwformcontrol" data-type="button" style="float:left">Options &#8675;</div>
                <div class="createpicklist fwformcontrol" data-type="button" style="float:right;">Create Pick List</div>
              </div>
              <br /><br />
              <div class="formrow options">
                <div class="formcolumn" style="width:20%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items by Status">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items Not Yet Staged" data-datafield="ItemsNotYetStaged"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items Staged" data-datafield="ItemsStaged"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items Out" data-datafield="ItemsOut"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Pick Date Range">
                    <div class="formcolumn">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield option" data-caption="From" data-datafield="PickDateFrom"></div>
                    </div>
                    <div class="formcolumn">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield option" data-caption="To" data-datafield="PickDateTo"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:40%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items to Include">
                    <div class="formcolumn">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Rental Items" data-datafield="RentalItems"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Sales Items" data-datafield="SaleItems"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Vendor Items" data-datafield="VendorItems"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Labor Items" data-datafield="LaborItems"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield option" data-caption="Warehouse" data-validationname="WarehouseValidation" data-datafield="WarehouseId" data-displayfield="Warehouse" style="width:50%;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield defaultoptions" data-caption="All" data-datafield=""></div>
                    </div>
                    <div class="formcolumn">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Complete/Kit Main Items" data-datafield="CompleteKitMain"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Complete/Kit Accessories" data-datafield="CompleteKitAccessories"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Complete/Kit Options" data-datafield="CompleteKitOptions"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Stand-Alone Items" data-datafield="StandAloneItems"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Items on Other Pick Lists" data-datafield="ItemsOnOtherPickLists"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option defaultoptions" data-caption="Reduce Qty of Items Already Picked" data-datafield="ReduceQuantityAlreadyPicked"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:40%;">
                  <!--<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Inventory Types">
               
                  </div>-->
                  <br /><br />
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option" data-caption="Summarize By I-Code" data-datafield="SummarizeByICode"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option" data-caption="Summarize Complete/Kit Accessories/Options" data-datafield="SummarizeCompleteKitItems"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield option" data-caption="Show Complete/Kit Accessories/Options in Their Own Inventory Types/Categories" data-datafield="HonorCompleteKitItemTypes"></div>
                  <br /><br />
                  <div class="applyoptions fwformcontrol" data-type="button" style="float:left;">Apply</div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwCreatePickListController = new TiwCreatePickList();
