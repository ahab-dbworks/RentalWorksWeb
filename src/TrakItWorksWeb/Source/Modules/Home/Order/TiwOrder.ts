﻿class TiwOrder extends Order {

  constructor() {
    super();
    this.id = '68B3710E-FE07-4461-9EFD-04E0DBDAF5EA';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  //##################################################
  getBrowseTemplate(): string {
    return `
    <div data-name="Order" data-control="FwBrowse" data-type="Browse" id="OrderBrowse" class="fwcontrol fwbrowse" data-orderby="OrderNumber" data-controller="TiwOrderController" data-hasinactive="false">
      <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="OrderId" data-browsedatatype="key" ></div>
      </div>
      <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="false" data-datafield="OfficeLocationId" data-browsedatatype="key" ></div>
          <div class="field" data-isuniqueid="false" data-datafield="WarehouseId" data-browsedatatype="key" ></div>
          <div class="field" data-isuniqueid="false" data-datafield="DepartmentId" data-browsedatatype="key" ></div>
          <div class="field" data-isuniqueid="false" data-datafield="CustomerId" data-browsedatatype="key" ></div>
          <div class="field" data-isuniqueid="false" data-datafield="DealId" data-browsedatatype="key" ></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="Order No." data-datafield="OrderNumber" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="Date" data-datafield="OrderDate" data-browsedatatype="text" data-sort="desc"></div>
      </div>
      <div class="column" data-width="350px" data-visible="true">
          <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="250px" data-visible="true">
          <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="Deal No." data-datafield="DealNumber" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px" data-visible="true">
          <div class="field" data-caption="Status" data-datafield="Status" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="Status As Of" data-datafield="StatusDate" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="PO No." data-datafield="PoNumber" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
          <div class="field" data-caption="Total" data-datafield="Total" data-browsedatatype="number" data-digits="2" data-formatnumeric="true" data-sort="off"></div>
      </div>
      <div class="column" data-width="180px" data-visible="true">
          <div class="field" data-caption="Agent" data-datafield="Agent" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="50px" data-visible="true">
          <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="auto" data-visible="true"></div>
  </div>`;
  }
  //##################################################
  getFormTemplate(): string {
    return `
    <div id="orderform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TiwOrderController">
      <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield OrderId" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="OrderId"></div>
      <div id="orderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs">
          <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="Order"></div>
          <div data-type="tab" id="rentaltab" class="notcombinedtab tab" data-tabpageid="rentaltabpage" data-caption="Rental"></div>
          <div data-type="tab" id="usedsaletab" class="notcombinedtab tab" data-tabpageid="usedsaletabpage" data-caption="Used Sale"></div>
          <div data-type="tab" id="lossdamagetab" class="tab" data-tabpageid="lossdamagetabpage" data-caption="Loss and Damage"></div>
          <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
          <div data-type="tab" id="picklisttab" class="tab submodule" data-tabpageid="picklisttabpage" data-caption="Pick List"></div>
          <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contract"></div>
          <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
          <div data-type="tab" id="notetab" class="tab" data-tabpageid="notetabpage" data-caption="Notes"></div>
          <div data-type="tab" id="historytab" class="tab" data-tabpageid="historytabpage" data-caption="History"></div>
        </div>
        <div class="tabpages">
          <!-- ##### ORDER tab ##### -->
          <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
            <div class="formpage">
              <!-- Order / Status section-->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 700px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order">
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 550px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 550px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dealnumber" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidate" data-required="true" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Pick Time" data-datafield="PickTime" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="From Date" data-datafield="EstimatedStartDate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="From Time" data-datafield="EstimatedStartTime" data-required="false" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="To Date" data-datafield="EstimatedStopDate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="To Time" data-datafield="EstimatedStopTime" data-required="false" style="flex:1 1 100px;"></div>
                    </div>
                    <div class="flexrow" style="display:none;">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Rental" data-datafield="DisableEditingRentalRate" style="float:left;width:150px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Used Sale" data-datafield="DisableEditingUsedSaleRate" style="float:left;width:150px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Disable Editing Loss and Damage" data-datafield="DisableEditingLossAndDamageRate" style="float:left;width:150px;"></div>
                    </div>
                  </div>
                </div>
              
                <!-- Status section -->
                <div class="flexcolumn" style="flex:1 1 150px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Status" data-enabled="false" style="flex:1 0 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="As of" data-datafield="StatusDate" data-enabled="false" style="flex:1 0 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Reference No." data-datafield="ReferenceNumber" style="flex:1 0 125px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              
              <!-- Location -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 250px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location"></div>
                  </div>
                </div>
                <div class=flexcolumn style="flex:1 1 400px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:0 1 125px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Print">
                    <div class="print fwformcontrol" data-type="button" style="flex:1 1 50px;margin:15px 0 0 10px;">Print</div>
                  </div>
                </div>
              </div>

              <!-- Activity section -->
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity" style="flex:1 1 770px">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD" data-caption="Rental" data-datafield="Rental" style="flex:1 1 100px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield anti-LD" data-caption="Used Sale" data-datafield="RentalSale" style="flex:1 1 100px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Loss and Damage" data-datafield="LossAndDamage" style="flex:1 1 100px;"></div>
                  </div>
                  <div class="flexrow" style="visibility:hidden;display:none;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLossAndDamageItem" data-datafield="HasLossAndDamageItem" style="flex:1 1 100px;"></div>
                  </div>
                </div>
              </div>

              <div class="flexrow">
                <div class="flexcolumn">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Value / Replacement Cost">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Value" data-datafield="" data-framedatafield="ValueTotal" data-formreadonly="true" style="flex: 1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Total Replacement" data-datafield="" data-framedatafield="ReplacementCostTotal" data-formreadonly="true" style="flex:1 1 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Value" data-datafield="" data-framedatafield="ValueOwned" data-formreadonly="true" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Owned Replacement" data-datafield="" data-framedatafield="ReplacementCostOwned" data-formreadonly="true" style="flex:1 1 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Value" data-datafield="" data-framedatafield="ValueSubs" data-formreadonly="true" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="money" class="formcolumn fwcontrol fwformfield frame" data-caption="Sub Replacement" data-datafield="" data-framedatafield="ReplacementCostSubs" data-formreadonly="true" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
                <!-- US Customary Weight -->
                <div class="flexcolumn">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="U.S. Customary Weight">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds" data-datafield="" data-framedatafield="WeightPounds" data-formreadonly="true" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces" data-datafield="" data-framedatafield="WeightOunces" data-formreadonly="true" style="flex:1 1 70px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Pounds (In Case)" data-datafield="" data-framedatafield="WeightInCasePounds" data-formreadonly="true" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Ounces (In Case)" data-datafield="" data-framedatafield="WeightInCaseOunces" data-formreadonly="true" style="flex:1 1 70px;"></div>
                    </div>
                  </div>
                </div>
                <!-- Metric Weight 
                <div class="flexcolumn">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Metric Weight">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kilograms" data-datafield="" data-framedatafield="WeightKilograms" data-formreadonly="true" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams" data-datafield="" data-framedatafield="WeightGrams" data-formreadonly="true" style="flex:1 1 70px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Kikograms (In Case)" data-datafield="" data-framedatafield="WeightInCaseKilograms" data-formreadonly="true" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield frame" data-caption="Grams (In Case)" data-datafield="" data-framedatafield="WeightInCaseGrams" data-formreadonly="true" style="flex:1 1 70px;"></div>
                    </div>
                  </div>
                </div>
                -->
                <!-- Office / Warehouse -->
                <div class="flexcolumn">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Office Location / Warehouse">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-enabled="false" style="flex:1 1 175px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-enabled="false" style="flex:1 1 175px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="WarehouseCode" data-datafield="WarehouseCode" data-formreadonly="true" data-enabled="false" style="display:none"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- ##### RENTAL tab ##### -->
          <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab" data-render="false">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
              <div class="wideflexrow">
                <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
              </div>
            </div>
          </div>

          <!-- ##### USED SALE tab ##### -->
          <div data-type="tabpage" id="usedsaletabpage" class="usedsalegrid notcombined tabpage" data-tabid="usedsaletab" data-render="false">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Used Sale Items">
              <div class="wideflexrow">
                <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Used Sale Items"></div>
              </div>
            </div>
          </div>

          <!-- ##### LOSS AND DAMAGE tab ##### -->
          <div data-type="tabpage" id="lossdamagetabpage" class="lossdamagegrid tabpage" data-tabid="lossdamagetab">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Loss and Damage">
              <div class="wideflexrow">
                <div data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Loss Damage Items"></div>
              </div>
            </div>
          </div>

          <!-- ##### CONTACTS tab ##### -->
          <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                <div class="flexrow">########## ADD CONTACTS GRID HERE ##########
                  <div data-control="FwGrid" data-grid="OrderContactGrid" data-securitycaption="Contacts"></div>
                </div>
              </div>
            </div>
          </div>

          <!-- ##### PICK LIST tab ##### -->
          <div data-type="tabpage" id="picklisttabpage" class="tabpage submodule picklist" data-tabid="picklisttab"></div>

          <!-- ##### CONTRACT tab ##### -->
          <div data-type="tabpage" id="contracttabpage" class="tabpage submodule contract" data-tabid="contracttab"></div>

          <!-- ##### DELIVER/SHIP tab ##### -->
          <div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 425px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield outtype delivery-delivery" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 175px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="OutDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="OutDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="OutDeliveryRequiredTime" style="flex:1 1 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="OutDeliveryToContact" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="OutDeliveryToContactPhone" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" data-formbeforevalidate="beforeValidateOutShipVia"></div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 125px;">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield OutDeliveryAddressType delivery-type-radio" data-caption="Address" data-datafield="OutDeliveryAddressType">
                        <div data-value="DEAL" data-caption="Deal"></div>
                        <div data-value="OTHER" data-caption="Other"></div>
                        <div data-value="VENUE" data-caption="Venue"></div>
                        <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="width:75%;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="OutDeliveryToLocation"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="OutDeliveryToAttention"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="OutDeliveryToAddress1"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="OutDeliveryToAddress2"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="OutDeliveryToCity"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="OutDeliveryToState"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="OutDeliveryToZipCode"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="OutDeliveryToCountryId" data-displayfield="OutDeliveryToCountry"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="OutDeliveryToCrossStreets"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="OutDeliveryDeliveryNotes"></div>
                      </div>
                    </div>
                  </div>
                </div>

                <!-- Incoming Section -->
                <div class="flexcolumn" style="flex:1 1 425px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield intype delivery-delivery" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 175px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="InDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="InDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="InDeliveryRequiredTime" style="flex:1 1 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="InDeliveryToContact" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InDeliveryToContactPhone" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="InDeliveryCarrierId" data-displayfield="InDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="InDeliveryShipViaId" data-displayfield="InDeliveryShipVia" data-formbeforevalidate="beforeValidateInShipVia"></div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 125px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield InDeliveryAddressType delivery-type-radio" data-caption="Address" data-datafield="InDeliveryAddressType">
                          <div data-value="DEAL" data-caption="Deal"></div>
                          <div data-value="OTHER" data-caption="Other"></div>
                          <div data-value="VENUE" data-caption="Venue"></div>
                          <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="copy fwformcontrol" data-type="button" style="flex:0 1 50px;margin:15px 0 0 10px;text-align:center;">Copy</div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="width:75%;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="InDeliveryToLocation"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="InDeliveryToAttention"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="InDeliveryToAddress1"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="InDeliveryToAddress2"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InDeliveryToCity"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="InDeliveryToState"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InDeliveryToZipCode"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-validationname="CountryValidation" data-datafield="InDeliveryToCountryId" data-displayfield="InDeliveryToCountry"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Cross Streets" data-datafield="InDeliveryToCrossStreets"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Notes" data-datafield="InDeliveryDeliveryNotes"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- ##### NOTES tab ##### -->
          <div data-type="tabpage" id="notetabpage" class="tabpage" data-tabid="notetab">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                <div class="flexrow">########## ADD NOTES GRID HERE ##########
                  <div data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                </div>
              </div>
            </div>
          </div>

          <!-- ##### HISTORY tab ##### -->
          <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Status History" style="flex:0 0 500px;">
                <div class="flexrow">########## ADD ORDER STATUS HISTORY GRID HERE ##########
                  <div data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Order Status History"></div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Snapshot" style="flex:0 0 175px;">
                <div class="flexrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Snapshot No." data-datafield="OrderSnapshot"></div>
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

var TiwOrderController = new TiwOrder();