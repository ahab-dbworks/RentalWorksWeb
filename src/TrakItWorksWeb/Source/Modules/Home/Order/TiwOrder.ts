class TiwOrder extends Order {

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
            <div class="field" data-isuniqueid="true" data-datafield="OrderId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="OrderTypeId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="false" data-datafield="OfficeLocationId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="WarehouseId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="DepartmentId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="CustomerId" data-browsedatatype="key"></div>
            <div class="field" data-isuniqueid="false" data-datafield="DealId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Order No." data-datafield="OrderNumber" data-cellcolor="OrderNumberColor" data-browsedatatype="text" data-sort="desc" data-sortsequence="2" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Date" data-datafield="OrderDate" data-browsedatatype="date" data-sortsequence="1" data-sort="desc"></div>
          </div>
          <div class="column" data-width="350px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-cellcolor="DescriptionColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Deal No." data-datafield="DealNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-cellcolor="StatusColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Status As Of" data-datafield="StatusDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
          <div class="column" data-width="180px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="50px" data-visible="true">
            <div class="field" data-caption="Warehouse" data-datafield="WarehouseCode" data-cellcolor="WarehouseColor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>          
    `;
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
          <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
          <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
          <div data-type="tab" id="picklisttab" class="tab submodule" data-tabpageid="picklisttabpage" data-caption="Pick List"></div>
          <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contract"></div>
          <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
          <div data-type="tab" id="invoicetab" class="tab submodule" data-tabpageid="invoicetabpage" data-caption="Invoice"></div>        
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
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No." data-datafield="OrderNumber" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:2 1 250px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div class="flexcolumn" style="flex:1 1 550px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield dealnumber" data-caption="Deal No." data-datafield="DealNumber" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="DealId" data-displayfield="Deal" data-validationname="DealValidation" data-formbeforevalidate="beforeValidate" data-required="true" style="flex:2 1 250px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 125px;display:none;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="OrderTypeId" data-displayfield="OrderType" data-validationname="OrderTypeValidation" data-required="true" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="Pick Date" data-datafield="PickDate" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Pick Time" data-datafield="PickTime" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="From Date" data-datafield="EstimatedStartDate" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="timepicker" data-timeformat="24" class="fwcontrol fwformfield" data-caption="From Time" data-datafield="EstimatedStartTime" data-required="false" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield pick_date_validation" data-caption="To Date" data-datafield="EstimatedStopDate" style="flex:1 1 125px;"></div>
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
                <div class="flexcolumn" style="flex:1 1 300px;">
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
                <div class="flexcolumn" style="flex:1 1 300px;">
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
                <div class="flexcolumn" style="flex:1 1 250px;">
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

          <!-- ##### BILLING tab ##### -->
            <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
              <!-- Billing Period -->
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Details">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_start_date" data-caption="Start" data-datafield="BillingStartDate" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_end_date" data-caption="Stop" data-datafield="BillingEndDate" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingWeeks week_or_month_field" data-caption="Weeks" data-datafield="BillingWeeks" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingMonths week_or_month_field" data-caption="Months" data-datafield="BillingMonths" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Delay Billing Search Until" data-datafield="DelayBillingSearchUntil" style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lock Billing Dates" data-datafield="LockBillingDates" style="flex:1 1 150px;padding-left:25px;margin-top:10px;"></div>
                  </div>
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="validation" data-validationname="BillingCycleValidation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" style="flex:1 1 250px;" data-required="true"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTermsValidation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" style="flex:1 1 250px;"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="PaymentTypeValidation" class="fwcontrol fwformfield" data-caption="Pay Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" style="flex:1 1 250px;"></div>
                    <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:1 1 250px;"></div>
                  </div>
                </div>
              </div>
              <!-- Bill Based On / Labor Fees / Contact Confirmation -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Determine Quantities to Bill Based on">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DetermineQuantitiesToBillBasedOn" style="flex:1 1 250px;">
                        <div data-value="CONTRACT" data-caption="Contract Activity"></div>
                        <div data-value="ORDER" data-caption="Order Quantity"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Prep Fees">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="IncludePrepFeesInRentalRate" style="flex:1 1 400px;">
                        <div data-value="false" data-caption="Add Prep Fees as Labor Charges"></div>
                        <div data-value="true" data-caption="Add Prep Fees into the Rental Item Rate"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Confirmation">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Require Contact Confirmation" data-datafield="RequireContactConfirmation" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Tax Rates / Order Group / Contact Confirmation -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="TaxOptionValidation" class="fwcontrol fwformfield" data-caption="Tax Option" data-datafield="TaxOptionId" data-displayfield="TaxOption" style="flex:1 1 250px"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="RentalTaxRate1" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="SalesTaxRate1" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="percent" data-digits="4" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="LaborTaxRate1" style="flex:1 1 75px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="HiatusDiscountFrom" style="flex:1 1 200px;">
                        <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                        <div data-value="ORDER" data-caption="This Order" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Group">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Group?" data-datafield="InGroup" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Group No" data-datafield="GroupNumber" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Issue To / Bill To Address -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 15%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Address">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="PrintIssuedToAddressFrom" style="flex:1 1 150px;">
                        <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;"></div>
                        <div data-value="CUSTOMER" data-caption="Customer" style="flex:1 1 100px;"></div>
                        <div data-value="ORDER" data-caption="Order" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 42.5%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Issue To">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="IssuedToName" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="IssuedToAttention" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAttention2" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="IssuedToAddress1" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="IssuedToAddress2" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="IssuedToCity" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="fwcontrol fwformfield" data-caption="State/Province" data-datafield="IssuedToState" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="IssuedToZipCode" style="flex:1 1 100px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="IssuedToCountryId" data-displayfield="IssuedToCountry" style="flex:1 1 250px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 42.5%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Bill To">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Different Than Issue To Address" data-datafield="BillToAddressDifferentFromIssuedToAddress" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Name" data-datafield="BillToName" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Attention" data-datafield="BillToAttention" data-enabled="false" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Address" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="StateValidation" class="differentaddress fwcontrol fwformfield" data-caption="State/Province" data-datafield="BillToState" data-enabled="false" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="text" class="differentaddress fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="CountryValidation" class="differentaddress fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-displayfield="BillToCountry" data-enabled="false" style="flex:1 1 250px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Options -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 400px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="No Charge">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="No Charge" data-datafield="NoCharge" style="flex:1 1 75px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Reason" data-datafield="NoChargeReason" style="flex:1 1 350px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 675px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Rental Items to go Out again after being Checked-In without increasing the Order quantity" data-datafield="RoundTripRentals" style="flex:1 1 650px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" style="flex:1 1 250px; float:left;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>


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
          <!-- INVOICE tab -->
          <div data-type="tabpage" id="invoicetabpage" class="tabpage invoiceSubModule" data-tabid="invoicetab">
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