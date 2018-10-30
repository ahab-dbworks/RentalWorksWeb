﻿class TiwPurchaseOrder extends PurchaseOrder {

  constructor() {
    super();
    this.id = 'DA900327-CEAC-4CB0-9911-CAA2C67059C2';
  }
    //browseModel: any = {};

    //getBrowseTemplate(): void {
    //    //let template = super.getBrowseTemplate();
    //    //return template;
    //}

    getBrowseTemplate(): string {
        return `
        <div data-name="PurchaseOrder" data-control="FwBrowse" data-type="Browse" id="PurchaseOrderBrowse" class="fwcontrol fwbrowse" data-orderby="PurchaseOrderNumber" data-controller="PurchaseOrderController" data-hasinactive="false">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="PurchaseOrderId" data-browsedatatype="key"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-browsedatatype="text" data-sort="off" data-searchfieldoperators="startswith"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="PO Date" data-datafield="PurchaseOrderDate" data-browsedatatype="date" data-sort="desc"></div>
          </div>
          <div class="column" data-width="350px" data-visible="true">
            <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="250px" data-visible="true">
            <div class="field" data-caption="Vendor" data-datafield="Vendor" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Total" data-datafield="Total" data-browsedatatype="number" data-digits="2" data-formatnumeric="true" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="Status" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="As Of" data-datafield="StatusDate" data-browsedatatype="date" data-sort="off"></div>
          </div>
          <div class="column" data-width="100px" data-visible="true">
            <div class="field" data-caption="Order Number" data-datafield="OrderNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="50px" data-visible="true">
            <div class="field" data-caption="Reference Number" data-datafield="ReferenceNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="180px" data-visible="true">
            <div class="field" data-caption="Agent" data-datafield="Agent" data-multiwordseparator="|" data-browsedatatype="text" data-sort="off"></div>
          </div>
        </div>`;
    }

    getFormTemplate(): string {
        return `
        <div id="purchaseorderform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Purchase Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="PurchaseOrderController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="PurchaseOrderId"></div>
          <div id="purchaseorderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
              <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="Purchase Order"></div>
              <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Rental"></div>
              <div data-type="tab" id="salestab" class="tab" data-tabpageid="salestabpage" data-caption="Sales"></div>
              <div data-type="tab" id="parttab" class="tab" data-tabpageid="parttabpage" data-caption="Parts"></div>
              <div data-type="tab" id="misctab" class="notcombinedtab tab" data-tabpageid="misctabpage" data-caption="Misc"></div>
              <div data-type="tab" id="labortab" class="notcombinedtab tab" data-tabpageid="labortabpage" data-caption="Labor"></div>
              <div data-type="tab" id="subrentaltab" class="tab" data-tabpageid="subrentaltabpage" data-caption="Sub-Rental"></div>
              <div data-type="tab" id="subsalestab" class="tab" data-tabpageid="subsalestabpage" data-caption="Sub-Sales"></div>
              <!--<div data-type="tab" id="repairtab" class="tab" data-tabpageid="repairtabpage" data-caption="Repair"></div>-->
              <div data-type="tab" id="submisctab" class="notcombinedtab tab" data-tabpageid="submisctabpage" data-caption="Sub-Misc"></div>
              <div data-type="tab" id="sublabortab" class="notcombinedtab tab" data-tabpageid="sublabortabpage" data-caption="Sub-Labor"></div>
              <!--<div data-type="tab" id="alltab" class="combinedtab tab" data-tabpageid="alltabpage" data-caption="Items"></div>
              <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contract"></div>-->
              <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
              <div data-type="tab" id="notetab" class="tab" data-tabpageid="notetabpage" data-caption="Notes"></div>
              <div data-type="tab" id="historytab" class="tab" data-tabpageid="historytabpage" data-caption="History"></div>
          </div>
          <div class="tabpages">
              <!-- PURCHASE ORDER TAB -->
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
              <div class="formpage">
                  <!-- Order / Status section-->
                  <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 950px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Order">
                      <div class="flexrow">
                          <div class="flexcolumn" style="flex:1 1 550px;">
                          <div class="flexrow">
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 400px;"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PurchaseOrderDate" style="flex:1 1 100px;"></div>
                              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Requisition No." data-datafield="RequisitionNumber" style="flex:1 1 100px;"></div>
                              <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Requisition Date" data-datafield="RequisitionDate" style="flex:1 1 100px;"></div>
                          </div>
                          </div>
                      </div>
                      <div class="flexrow">
                          <div class="flexcolumn" style="flex:1 1 616px;">
                          <div class="flexrow">
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" data-formbeforevalidate="beforeValidate" data-required="true" style="flex:1 1 400px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield RateType" data-caption="Rate" data-datafield="RateType" data-displayfield="RateType" data-validationname="RateTypeValidation" data-validationpeek="false" data-required="true" style="flex:1 1 175px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="PoTypeId" data-displayfield="PoType" data-validationname="POTypeValidation" data-required="true" style="flex:1 1 175px;"></div>
                          </div>
                          </div>
                          <div class="flexcolumn" style="flex:1 1 375px;">
                          <div class="flexrow">
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-validationpeek="false" data-enabled="false" style="flex:1 1 200px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-validationpeek="false" data-enabled="false" style="flex:1 1 200px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>
                          </div>
                          </div>
                      </div>
                      <div class="flexrow"></div>
                      </div>
                  </div>
                  <!-- Status section -->
                  <div class="flexcolumn" style="flex:1 1 125px;">
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
                  <!-- Activity section -->
                  <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity" style="flex:1 1 770px">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="flex:1 1 90px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="Sales" style="flex:1 1 90px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Parts" data-datafield="Parts" style="flex:1 1 90px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Miscellaneous" data-datafield="Miscellaneous" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Labor" data-datafield="Labor" style="flex:1 1 90px;"></div>
                      </div>
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rent" data-datafield="SubRent" style="flex:1 1 90px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sale" data-datafield="SubSale" style="flex:1 1 90px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Repair" data-datafield="Repair" style="flex:1 1 90px;" data-enabled="false"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Miscellaneous" data-datafield="SubMiscellaneous" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Labor" data-datafield="SubLabor" style="flex:1 1 90px;"></div>
                      </div>
                      <!--Hidden field for determining whether checkboxes should be enabled / disabled-->
                      <div class="flexrow" style="visibility:hidden;">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasFacilitiesItem" data-datafield="HasFacilitiesItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLaborItem" data-datafield="HasLaborItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLossAndDamageItem" data-datafield="HasLossAndDamageItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasMiscellaneousItem" data-datafield="HasMiscellaneousItem" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasSalesItem" data-datafield="HasSalesItem" style="flex:1 1 100px;"></div>
                      </div>
                  </div>
                  </div>
                  <!-- Location / PO section -->
                  <!--<div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 125px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Print">
                      <div class="print fwformcontrol" data-type="button" style="flex:1 1 50px;margin:15px 0 0 10px;">Print</div>
                      </div>
                  </div>
                  </div>-->
                  <!-- Personnel -->
                  <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 600px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 185px;"></div>
                      </div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- RENTAL TAB -->
              <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn rental-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield bottom-line-discount" data-caption="Disc. %" data-rectype="R" data-datafield="RentalDiscountPercent" data-digits="2" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals rentalOrderItemTotal bottom-line-total-tax" data-caption="Total" data-rectype="R" data-datafield="RentalTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield rentalTotalWithTax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="R" data-datafield="RentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn rental-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- SALES TAB -->
              <div data-type="tabpage" id="salestabpage" class="salesgrid notcombined tabpage" data-tabid="salestab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sales Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Sales Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn sales-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="S" data-datafield="SalesDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals salesOrderItemTotal bottom-line-total-tax" data-caption="Total" data-rectype="S" data-datafield="SalesTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield salesTotalWithTax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="S" data-datafield="SalesTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn sales-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- PART TAB -->
              <div data-type="tabpage" id="parttabpage" class=" partgrid tabpage" data-tabid="parttab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Parts Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Parts Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn part-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="P" data-datafield="PartsDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals part-order-item-total bottom-line-total-tax" data-caption="Total" data-rectype="P" data-datafield="PartsTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield part-total-wtax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="P" data-datafield="PartsTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn part-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- MISC TAB -->
              <div data-type="tabpage" id="misctabpage" class="miscgrid notcombined tabpage" data-tabid="misctab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Misc Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Misc Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn misc-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="M" data-datafield="MiscDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals part-order-item-total bottom-line-total-tax" data-caption="Total" data-rectype="M" data-datafield="MiscTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield part-total-wtax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="M" data-datafield="MiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn misc-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- LABOR TAB -->
              <div data-type="tabpage" id="labortabpage" class="laborgrid notcombined tabpage" data-tabid="labortab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Labor Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Labor Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn labor-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="L" data-datafield="LaborDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals labor-total bottom-line-total-tax" data-caption="Total" data-rectype="L" data-datafield="LaborTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield labor-total-wtax bottom-line-total-tax" data-caption="w/ Tax" data-rectype="L" data-datafield="LaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn labor-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- SUBRENTAL TAB -->
              <div data-type="tabpage" id="subrentaltabpage" class="subrentalgrid notcombined tabpage" data-tabid="subrentaltab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Rental Item-totals"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn subrental-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield totals subRentalDaysPerWeek" data-caption="D/W" data-datafield="SubRentalDaysPerWeek" data-digits="2" data-digitsoptional="false" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="R" data-datafield="SubRentalDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals subrental-total bottom-line-total-tax subrentalAdjustmentsPeriod" data-caption="Total" data-rectype="R" data-datafield="PeriodSubRentalTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield subrental-total-wtax bottom-line-total-tax subrentalAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="R" data-datafield="PeriodSubRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals subrental-total bottom-line-total-tax subrentalAdjustmentsWeekly" data-caption="Total" data-rectype="R" data-datafield="WeeklySubRentalTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield subrental-total-wtax bottom-line-total-tax subrentalAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="R" data-datafield="WeeklySubRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals subrental-total bottom-line-total-tax subrentalAdjustmentsMonthly" data-caption="Total" data-rectype="R" data-datafield="MonthlySubRentalTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield subrental-total-wtax bottom-line-total-tax subrentalAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="R" data-datafield="MonthlySubRentalTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn subrental-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="subrental" data-datafield="" style="flex:1 1 250px;">
                          <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:-15px;"></div>
                          <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:-15px;"></div>
                          <div data-value="P" class="periodType" data-caption="Period"></div>
                      </div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- SUBSALES TAB -->
              <div data-type="tabpage" id="subsalestabpage" class="subsalesgrid notcombined tabpage" data-tabid="subsalestab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Sales Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Sales Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn subsales-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="S" data-datafield="SubSalesDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals bottom-line-total-tax" data-caption="Total" data-rectype="S" data-datafield="SubSalesTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield bottom-line-total-tax" data-caption="w/ Tax" data-rectype="S" data-datafield="SubSalesTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn subsales-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- SUBMISC TAB -->
              <div data-type="tabpage" id="submisctabpage" class="submiscgrid notcombined tabpage" data-tabid="submisctab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Miscellaneous Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Miscellaneous Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn submisc-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="M" data-datafield="SubMiscDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals submisc-total bottom-line-total-tax submiscAdjustmentsPeriod" data-caption="Total" data-rectype="M" data-datafield="PeriodSubMiscTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield submisc-total-wtax bottom-line-total-tax submiscAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="M" data-datafield="PeriodSubMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals submisc-total bottom-line-total-tax submiscAdjustmentsWeekly" data-caption="Total" data-rectype="M" data-datafield="WeeklySubMiscTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield submisc-total-wtax bottom-line-total-tax submiscAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="M" data-datafield="WeeklySubMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals submisc-total bottom-line-total-tax submiscAdjustmentsMonthly" data-caption="Total" data-rectype="M" data-datafield="MonthlySubMiscTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield submisc-total-wtax bottom-line-total-tax submiscAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="M" data-datafield="MonthlySubMiscTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn submisc-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="submisc" data-datafield="" style="flex:1 1 250px;">
                          <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:-15px;"></div>
                          <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:-15px;"></div>
                          <div data-value="P" class="periodType" data-caption="Period"></div>
                      </div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- SUBLABOR TAB -->
              <div data-type="tabpage" id="sublabortabpage" class="sublaborgrid notcombined tabpage" data-tabid="sublabortab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Labor Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Labor Items"></div>
                  </div>
              </div>
              <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn sublabor-adjustments" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="L" data-datafield="SubLaborDiscountPercent" style="flex:1 1 50px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals sublabor-total bottom-line-total-tax sublaborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodSubLaborTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sublabor-total-wtax bottom-line-total-tax sublaborAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="L" data-datafield="PeriodSubLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals sublabor-total bottom-line-total-tax sublaborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklySubLaborTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sublabor-total-wtax bottom-line-total-tax sublaborAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="L" data-datafield="WeeklySubLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals sublabor-total bottom-line-total-tax sublaborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlySubLaborTotal" style="flex:1 1 100px; display:none;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield sublabor-total-wtax bottom-line-total-tax sublaborAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="L" data-datafield="MonthlySubLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                  </div>
                  </div>
                  <div class="flexcolumn sublabor-totals" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                      <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield totals totalType" data-caption="" data-gridtype="sublabor" data-datafield="" style="flex:1 1 250px;">
                          <div data-value="W" class="weeklyType" data-caption="Weekly" style="margin-top:-15px;"></div>
                          <div data-value="M" class="monthlyType" data-caption="Monthly" style="margin-top:-15px;"></div>
                          <div data-value="P" class="periodType" data-caption="Period"></div>
                      </div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>
              <!-- REPAIR TAB -->
              <!--<div data-type="tabpage" id="repairtabpage" class="repairpartgrid notcombined tabpage" data-tabid="repairtab" data-render="false">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair Items">
                  <div class="flexrow" style="max-width:1800px;">
                  <div data-issubgrid="true" data-control="FwGrid" data-grid="RepairPartGrid" data-securitycaption="Repair Items"></div>
                  </div>
                  <div class="flexrow" style="max-width:1800px;">
                  <div class="flexcolumn" style="flex:1 1 125px;"></div>
                  <div class="flexcolumn repair-adjustments" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Adjustments">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield totals bottom-line-discount" data-caption="Disc. %" data-rectype="L" data-datafield="PartDiscountPercent" style="flex:1 1 50px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals part-order-item-total bottom-line-total-tax laborAdjustmentsPeriod" data-caption="Total" data-rectype="L" data-datafield="PeriodLaborTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield part-total-wtax bottom-line-total-tax laborAdjustmentsPeriod" data-caption="w/ Tax" data-rectype="L" data-datafield="PartIncludesTax" style="flex:1 1 75px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom-line-total-tax laborAdjustmentsWeekly" data-caption="Total" data-rectype="L" data-datafield="WeeklyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom-line-total-tax laborAdjustmentsWeekly" data-caption="w/ Tax" data-rectype="L" data-datafield="WeeklyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals laborOrderItemTotal bottom-line-total-tax laborAdjustmentsMonthly" data-caption="Total" data-rectype="L" data-datafield="MonthlyLaborTotal" style="flex:1 1 100px; display:none;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield laborTotalWithTax bottom-line-total-tax laborAdjustmentsMonthly" data-caption="w/ Tax" data-rectype="L" data-datafield="MonthlyLaborTotalIncludesTax" style="flex:1 1 75px;margin-top:10px; display:none;"></div>
                      </div>
                      </div>
                  </div>
                  <div class="flexcolumn repair-totals" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Gross Total" data-datafield="" data-enabled="false" data-totalfield="GrossTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Discount" data-datafield="" data-enabled="false" data-totalfield="Discount" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Sub-Total" data-datafield="" data-enabled="false" data-totalfield="SubTotal" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Tax" data-datafield="" data-enabled="false" data-totalfield="Tax" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield totals" data-caption="Total" data-datafield="" data-enabled="false" data-totalfield="Total" style="flex:1 1 100px;"></div>
                      </div>
                      </div>
                  </div>
                  </div>
              </div>
              </div>-->
              <!-- BILLING TAB PAGE -->
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab" style="width: 400px;">
              <!-- Billing Period -->
              <!--<div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Period">
                  <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_start_date" data-caption="Start" data-datafield="BillingStartDate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield billing_end_date" data-caption="Stop" data-datafield="BillingEndDate" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingWeeks week_or_month_field" data-caption="Weeks" data-datafield="BillingWeeks" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield BillingMonths week_or_month_field" data-caption="Months" data-datafield="BillingMonths" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Delay Billing Search Until" data-datafield="DelayBillingSearchUntil" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Lock Billing Dates" data-datafield="LockBillingDates" style="flex:1 1 150px;padding-left:25px;margin-top:10px;"></div>
                  </div>
                  </div>
              </div>-->
              <!-- Billing Cycle -->
              <div class="flexrow" style="flex:0 1 400px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Cycle">
                  <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" data-validationname="BillingCycleValidation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" style="flex:0 1 250px;" data-required="true"></div>
                      <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:0 1 250px;"></div>
                      <!--<div data-control="FwFormField" data-type="validation" data-validationname="PaymentTermsValidation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" style="flex:1 1 250px;"></div>-->
                      <!--<div data-control="FwFormField" data-type="validation" data-validationname="PaymentTypeValidation" class="fwcontrol fwformfield" data-caption="Pay Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" style="flex:1 1 250px;"></div>-->
                  </div>
                  </div>
              </div>
              <!-- Bill Based On / Labor Fees / Contact Confirmation -->
              <!--<div class="flexrow">
              <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Determine Quantities to Bill Based on">
                  <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DetermineQuantitiesToBillBasedOn" style="flex:1 1 250px;">
                      <div data-value="CONTRACT" data-caption="Contract Activity" style="margin-top:-15px;"></div>
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
                      <div data-value="false" data-caption="Add Prep Fees as Labor Charges" style="margin-top:-15px;"></div>
                      <div data-value="true" data-caption="Add Prep Fees into the Rental Item Rate"></div>
                      </div>
                  </div>
                  </div>
              </div>
              <div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
              </div>-->
              <!--<div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Confirmation">
                  <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Require Contact Confirmation" data-datafield="RequireContactConfirmation" style="flex:1 1 125px;"></div>
                  </div>
                  </div>
              </div>-->
              <!--</div>-->
              <!-- Tax Rates / Order Group / Contact Confirmation -->
              <div class="flexrow">
                  <div class="flexcolumn" style="flex:0 1 400px;">
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
                  <!--<div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                  </div>-->
                  <!--<div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="HiatusDiscountFrom" style="flex:1 1 200px;">
                          <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;margin-top:-15px;"></div>
                          <div data-value="ORDER" data-caption="This Order" style="flex:1 1 100px;"></div>
                      </div>
                      </div>
                  </div>
                  </div>-->
                  <!--<div class="flexcolumn" style="flex:1 1 25px;">
                  &#32;
                  </div>-->
                  <!--<div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Group">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="In Group?" data-datafield="InGroup" style="flex:1 1 100px;"></div>
                      <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Group No" data-datafield="GroupNumber" style="flex:1 1 125px;"></div>
                      </div>
                  </div>
                  </div>-->
              </div>
              <!-- Issue To / Bill To Address -->
              <!--<div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 15%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Address">
                      <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="PrintIssuedToAddressFrom" style="flex:1 1 150px;">
                          <div data-value="DEAL" data-caption="Deal" style="flex:1 1 100px;margin-top:-15px;"></div>
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
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" style="flex:1 1 250px;"></div>
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
              </div>-->
              <!-- Options -->
              <!--<div class="flexrow">
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
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Invoice Discount Reason" data-datafield="DiscountReasonId" data-displayfield="DiscountReason" data-validationname="DiscountReasonValidation" style="flex:1 1 250px; float:left;"></div>
                      </div>
                  </div>
                  </div>
              </div>-->
              </div>
              <!-- DELIVER/SHIP TAB -->
              <!--<div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
              <div class="flexpage">
                  <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield outtype" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="OutDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="OutDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="OutDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="OutDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="OutDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                      </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" data-formbeforevalidate="beforeValidateOutShipVia"></div>
                      </div>
                      </div>
                      <div class="flexrow">
                      <div class="flexcolumn" style="width:25%;flex:0 1 auto;">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Address" data-datafield="OutDeliveryAddressType">
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
                          <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OutDeliveryOnlineOrderNumber"></div>
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield online" data-caption="Status" data-datafield="OutDeliveryOnlineOrderStatus"></div>
                          </div>
                      </div>
                      </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield intype" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="InDeliveryTargetShipDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Required By" data-datafield="InDeliveryRequiredDate" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="time" data-timeformat="24" class="fwcontrol fwformfield" data-caption="Required Time" data-datafield="InDeliveryRequiredTime" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact" data-datafield="InDeliveryToContact" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InDeliveryToContactPhone" style="flex:1 1 250px;"></div>
                      </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                      <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="InDeliveryCarrierId" data-displayfield="InDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                          <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="InDeliveryShipViaId" data-displayfield="InDeliveryShipVia" data-formbeforevalidate="beforeValidateInShipVia"></div>
                      </div>
                      </div>
                      <div class="flexrow">
                      <div class="flexcolumn" style="width:25%;flex:0 1 auto;">
                          <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Address" data-datafield="InDeliveryAddressType">
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
              </div>-->
              <!-- CONTACTS TAB -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderContactGrid" data-securitycaption="Contacts"></div>
                  </div>
              </div>
              </div>
              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notetabpage" class="tabpage" data-tabid="notetab">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderNoteGrid" data-securitycaption="Notes"></div>
                  </div>
              </div>
              </div>
              <!-- HISTORY TAB -->
              <div data-type="tabpage" id="historytabpage" class="tabpage" data-tabid="historytab">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Status History">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="OrderStatusHistoryGrid" data-securitycaption="Order Status History"></div>
                  </div>
              </div>
              </div>
          </div>
          </div>
      </div>`;
    }

}

var TiwPurchaseOrderController = new TiwPurchaseOrder();