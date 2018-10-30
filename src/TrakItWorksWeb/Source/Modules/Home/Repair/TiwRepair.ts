class TiwRepair extends Repair {
  constructor() {
    super();
    this.id = 'D567EC42-E74C-47AB-9CA8-764DC0F02D3B';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  //##################################################
  getBrowseTemplate(): string {
    return `
     <div data-name="Repair" data-control="FwBrowse" data-type="Browse" id="RepairBrowse" class="fwcontrol fwbrowse" data-orderby="RepairId" data-controller="RepairController" data-hasinactive="true">
          <div class="column flexcolumn" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="RepairId" data-browsedatatype="key" ></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Repair No." data-datafield="RepairNumber" data-browsedatatype="text" data-cellcolor="RepairNumberColor" data-sort="off" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="RepairDate" data-browsedatatype="date" data-sort="desc"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Barcode  No." data-datafield="BarCode" data-browsedatatype="text" data-cellcolor="BarCodeColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Serial No." data-datafield="SerialNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="RFID" data-datafield="RfId" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="150px" data-visible="true">
            <div class="field" data-caption="I-Code" data-datafield="ICode" data-browsedatatype="text" data-cellcolor="ICodeColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="450px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="ItemDescription" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="125px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-browsedatatype="text" data-cellcolor="StatusColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Quantity" data-datafield="Quantity" data-browsedatatype="number" data-cellcolor="QuantityColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="As of Date" data-datafield="StatusDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
           <div class="column flexcolumn" max-width="250px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="Warehouse" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Type" data-datafield="AvailForDisplay" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="200px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="DamageDeal" data-browsedatatype="text" data-cellcolor="DamageDealColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="PO No." data-datafield="PoNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="100px" data-visible="true">
            <div class="field" data-caption="Priority" data-datafield="PriorityDescription" data-browsedatatype="text" data-cellcolor="PriorityColor" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" max-width="75px" data-visible="true">
            <div class="field" data-caption="Quantity Released" data-datafield="ReleasedQuantity" data-browsedatatype="number" data-sort="off"></div>
          </div>
          <div class="column flexcolumn" data-width="auto" data-visible="true"></div>
        </div>`;
  }
  //##################################################
  getFormTemplate(): string {
    return `
    <div id="repairform" class="fwcontrol fwcontainer fwform flexpage" data-control="FwContainer" data-type="form" data-version="1" data-caption="Repair Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="RepairController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield flexbox" data-isuniqueid="true" data-saveorder="1" data-caption="Order No." data-datafield="RepairId"></div>
          <div id="orderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs flexbox">
              <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="Repair Ticket"></div>
              <div data-type="tab" id="damagetab" class="tab" data-tabpageid="damagetabpage" data-caption="Damage/Correction"></div>
              <div data-type="tab" id="costtab" class="tab" data-tabpageid="costtabpage" data-caption="Costs"></div>
              <div data-type="tab" id="partstab" class="tab" data-tabpageid="partstabpage" data-caption="Parts"></div>
              <div data-type="tab" id="chargetab" class="tab" data-tabpageid="chargetabpage" data-caption="Charge"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair Ticket">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Repair Number" data-datafield="RepairNumber" data-enabled="false"  style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="RepairDate"  data-enabled="false"             style="flex:1 1 115px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield repairavailforradio" data-caption="Available For" data-datafield="AvailFor" data-enabled="false" style="flex:1 1 115px;">
                            <div data-value="R" data-caption="Rent"></div>
                            <div data-value="S" data-caption="Sell"></div>
                          </div>
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield repairtyperadio" data-caption="Type" data-datafield="RepairType" data-enabled="false" style="flex:1 1 115px;">
                            <div data-value="OWNED" data-caption="Owned"></div>
                            <div data-value="OUTSIDE" data-caption="Not Owned"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 525px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield icoderental icode" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="RentalInventoryValidation" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield icodesales icode" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="SalesInventoryValidation" style="flex:1 1 115px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Item Description" data-datafield="ItemDescription" data-enabled="false" style="flex:1 1 300px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" data-enabled="false" style="flex:1 1 50px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Bar Code" data-datafield="ItemId" data-displayfield="BarCode" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="AssetValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Serial Number" data-datafield="ItemId" data-displayfield="SerialNumber" data-enabled="false" data-validationpeek="true" data-validationname="AssetValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="RFID" data-datafield="ItemId" data-displayfield="RfId" data-enabled="false" data-formbeforevalidate="beforeValidate" data-validationpeek="true" data-validationname="AssetValidation" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 285px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ticket Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As Of Date" data-datafield="StatusDate" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending Repair" data-datafield="PendingRepair" data-enabled="false" style="flex:1 1 125px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Due Date" data-datafield="DueDate" data-enabled="true" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 775px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location / Warehouse / Department">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="Warehouse" data-enabled="false"   style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Office" data-datafield="Location" data-enabled="false"       style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 200px;"></div>
                          <!--Hidden Fields-->
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield locationid" data-caption="" data-datafield="LocationId" data-enabled="false" data-visible="false" style="float:left;width:0px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield warehouseid" data-caption="" data-datafield="BillingWarehouseId" data-enabled="false" data-visible="false" style="float:left;width:0px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield inputbyuserid" data-caption="" data-datafield="InputByUserId" data-enabled="false" data-visible="false" style="float:left;width:0px;"></div>
                        </div>
                     </div>
                   </div>
                    <div class="flexcolumn" style="flex:1 1 125px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Option">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Billable" data-datafield="Billable" style="flex:1 1 75px;margin-top:10px;"></div>
                        </div>
                      </div>
                   </div>
                   <div class="flexcolumn" style="flex:1 1 125px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Priority">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield repairtyperadio" data-caption="" data-datafield="Priority">
                        <div data-value="HIG" data-caption="High" style="margin-top:-15px;"></div>
                        <div data-value="MED" data-caption="Medium"></div>
                        <div data-value="LOW" data-caption="Low"></div>
                      </div>
                    </div>
                   </div>
                 </div>
                 <!-- Last Order / Billable Repair / Item Status -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 600px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Last Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order Number" data-datafield="DamageOrderId" data-displayfield="DamageOrderNumber" data-required="false" data-enabled="false" data-validationname="OrderValidation" style="flex:1 1 136px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Description" data-datafield="DamageOrderDescription" data-enabled="false" style="flex:1 1 232px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DamageDeal" data-enabled="false" style="flex:1 1 232px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contract Number" data-datafield="DamageContractId" data-displayfield="DamageContractNumber" data-required="false" data-enabled="false" data-validationname="ContractValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Contract Date" data-datafield="DamageContractDate" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Scanned By" data-datafield="DamageScannedBy" data-enabled="false" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Loss and Damage Order Number" data-datafield="LossAndDamageOrderId" data-displayfield="LossAndDamageOrderNumber" data-required="false" data-enabled="false" data-validationname="OrderValidation" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Loss and Damage Order Description" data-datafield="LossAndDamageOrderDescription" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Order">
                        <div class="flexrow">
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Pending PO Number" data-datafield="PoPending" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield PoNumber" data-caption="Authorized PO Number" data-datafield="PoNumber" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Outside Repair" data-datafield="OutsideRepair" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RepairItemStatus" data-caption="Item Status" data-datafield="RepairItemStatusId" data-displayfield="RepairItemStatus" data-enabled="true" data-validationname="RepairItemStatusValidation" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!--Estimate / Complete-->
                    <div class="flexcolumn completeestimate"  style="flex:1 1 600px;">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Create Estimate">
                          <div class="flexrow">
                            <div class="fwformcontrol estimate" data-type="button" style="margin:16px 0 0 0;flex:0 1 75px">Estimate</div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="EstimateDate" data-enabled="false"        style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Estimated By" data-datafield="EstimateBy" data-enabled="false"  style="flex:1 1 250px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Complete Repair">
                          <div class="flexrow">
                            <div class="fwformcontrol complete" data-type="button" style="margin:16px 0 0 0;flex:0 1 75px;">Complete</div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date" data-datafield="CompleteDate" data-enabled="false"        style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Completed By" data-datafield="CompleteBy" data-enabled="false"  style="flex:1 1 250px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>

                    <!--Release Grid-->
                    <div class="flexcolumn releasesection" style="flex:1 1 500px;">
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Releases">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield releasetotals" data-caption="Total Released" data-datafield="ReleasedQuantity" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div class="fwformcontrol releaseitems" data-type="button" style="margin:16px 0 0 0;flex:0 1 75px;">Release</div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwGrid" id="RepairReleaseGrid" data-grid="RepairReleaseGrid" data-caption="" data-securitycaption="RepairReleaseGrid" style="flex:1 1 300px;margin-top:5px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!--Damage / Correction Tab-->
            <div data-type="tabpage" id="damagetabpage" class="tabpage" data-tabid="damagetab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Damage Information">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="Damage" data-height="500px"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Correction Information">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="" data-datafield="Correction" data-height="500px"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!--Cost Tab-->
            <div data-type="tabpage" id="costtabpage" class="tabpage" data-tabid="costtab">
              <div class="formpage">
                <div class="formrow" style="width:100%;">
                  <div class="formrow costgrid" style="width:100%;">
                    <div data-control="FwGrid" id="RepairCostGrid" data-grid="RepairCostGrid" data-caption="Costs" data-securitycaption="RepairCostGrid" style="min-width:240px;"></div>
                  </div>
                </div>
                  <div class="formrow" style="display: flex; justify-content: flex-end;">
                    <div class="formcolumn costtotals" style="width:auto;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield=""  data-enabled="false" data-totalfield="GrossTotal" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="SalesTax" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="max-width:125px; float:left;"></div>
                    </div>
                  </div>
              </div>
            </div>
            <!--Parts Tab-->
            <div data-type="tabpage" id="partstabpage" class="tabpage" data-tabid="partstab">
              <div class="formpage">
                <div class="formrow" style="width:100%;">
                  <div class="formrow partgrid" style="width:100%;">
                    <div data-control="FwGrid" id="RepairPartGrid" data-grid="RepairPartGrid" data-caption="Parts" data-securitycaption="RepairPartGrid" style="min-width:240px;"></div>
                  </div>
                </div>
                  <div class="formrow" style="display: flex; justify-content: flex-end;">
                    <div class="formcolumn parttotals" style="width:auto;">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="max-width:125px; float:left"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="max-width:125px; float:left"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="SalesTax" style="max-width:125px; float:left;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="max-width:125px; float:left;"></div>
                    </div>
                  </div>
              </div>
            </div>
            <!--Charge Tab-->
            <div data-type="tabpage" id="chargetabpage" class="tabpage" data-tabid="chargetab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn" style="float:left;max-width:450px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Location">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" style="float:left;max-width:200px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;max-width:200px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="float:left;max-width:535px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billed Order and Invoice">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Number" data-datafield="ChargeOrderNumber" data-enabled="false" style="float:left;width:200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ChargeOrderDescription" data-enabled="false" style="float:left;width:300px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Invoice Number" data-datafield="ChargeInvoiceNumber" data-enabled="false" style="float:left;width:200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="ChargeInvoiceDescription" data-enabled="false" style="float:left;width:300px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="float:left;max-width:400px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax and Currency">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" data-validationname="CurrencyValidation" style="float:left;max-width:150px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" data-validationname="TaxOptionValidation" style="float:left;max-width:450px;"></div>
                        </div>
                      </div>
                    </div>
                      <div class="flexcolumn" style="max-width:200px;">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalTaxRate1" data-enabled="false" style="width:125px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield SalesTaxRate1" data-caption="Sales" data-datafield="SalesTaxRate1" data-enabled="false" style="width:125px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTaxRate1" data-enabled="false" style="width:125px;"></div>
                        </div>
                        </div>
                      </div>
                  </div>
              </div>
            </div>
            <!--Notes Tab-->
            <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-maxlength="255" data-caption="" data-datafield="Notes"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            </div>
          </div>
        </div>`;
  }
  //##################################################
}

var TiwRepairController = new TiwRepair();