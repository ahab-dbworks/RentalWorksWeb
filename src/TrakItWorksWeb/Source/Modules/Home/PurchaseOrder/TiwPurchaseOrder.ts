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
        <div data-name="PurchaseOrder" data-control="FwBrowse" data-type="Browse" id="PurchaseOrderBrowse" class="fwcontrol fwbrowse" data-orderby="PurchaseOrderNumber" data-controller="TiwPurchaseOrderController" data-hasinactive="false">
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
        <div id="purchaseorderform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Purchase Order" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TiwPurchaseOrderController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="PurchaseOrderId"></div>
          <div id="purchaseorderform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="generaltab" class="generaltab tab" data-tabpageid="generaltabpage" data-caption="Purchase Order"></div>
              <div data-type="tab" id="rentaltab" class="tab" data-tabpageid="rentaltabpage" data-caption="Rental"></div>
              <div data-type="tab" id="subrentaltab" class="tab" data-tabpageid="subrentaltabpage" data-caption="Sub-Rental"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="delivershiptab" class="tab" data-tabpageid="delivershiptabpage" data-caption="Deliver/Ship"></div>
              <div data-type="tab" id="notetab" class="tab" data-tabpageid="notetabpage" data-caption="Notes"></div>
              <div data-type="tab" id="historytab" class="tab" data-tabpageid="historytabpage" data-caption="History"></div>
            </div>
            <div class="tabpages">
              <!-- PURCHASE ORDER TAB -->
              <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Purchase Order">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PO No." data-datafield="PurchaseOrderNumber" data-enabled="false" style="flex:0 1 100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" style="flex:1 1 350px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="PO Date" data-datafield="PurchaseOrderDate" style="flex:1 1 125px;"></div>
                          <!--
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Requisition No." data-datafield="RequisitionNumber" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Requisition Date" data-datafield="RequisitionDate" style="flex:1 1 100px;"></div>
                          -->
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Vendor" data-datafield="VendorId" data-displayfield="Vendor" data-validationname="VendorValidation" data-formbeforevalidate="beforeValidate" data-required="true" style="flex:1 1 350px;"></div>                          
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="PoTypeId" data-displayfield="PoType" data-validationname="POTypeValidation" data-required="true" style="flex:1 1 175px;"></div>
                          <!--
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Requisition No." data-datafield="RequisitionNumber" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Requisition Date" data-datafield="RequisitionDate" style="flex:1 1 100px;"></div>
                          -->
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-validationpeek="false" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" data-validationpeek="false" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" data-required="true" style="flex:1 1 175px;"></div>                        
                        </div>
                        <div class="flexrow">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personnel">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="AgentId" data-displayfield="Agent" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 225px;"></div>
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="ProjectManagerId" data-displayfield="ProjectManager" data-enabled="true" data-required="true" data-validationname="UserValidation" style="flex:1 1 225px;"></div>
                            </div>
                          </div>
                        </div>
                        <div class="flexrow">
                          <!-- Activity section -->
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity" style="flex:1 1 770px">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="Rental" style="flex:1 1 90px;"></div>
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rent" data-datafield="SubRent" style="flex:1 1 90px;"></div>
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Repair" data-datafield="Repair" style="flex:1 1 90px;" data-enabled="false"></div>
                            </div>
                            <!--Hidden field for determining whether checkboxes should be enabled / disabled-->
                            <div class="flexrow" style="visibility:hidden;">
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasLossAndDamageItem" data-datafield="HasLossAndDamageItem" style="flex:1 1 100px;"></div>
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalItem" data-datafield="HasRentalItem" style="flex:1 1 100px;"></div>
                              <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="HasRentalSaleItem" data-datafield="HasRentalSaleItem" style="flex:1 1 100px;"></div>
                            </div>
                          </div>
                        </div>
                      </div>
                    </div>
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
                </div>
              </div>

              <!-- RENTAL TAB -->
              <div data-type="tabpage" id="rentaltabpage" class="rentalgrid notcombined tabpage" data-tabid="rentaltab" data-render="false">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                  <div class="wideflexrow">
                    <div data-control="FwGrid" data-issubgrid="false" data-grid="OrderItemGrid" data-securitycaption="Rental Items"></div>
                  </div>
                </div>
              </div>

              <!-- SUBRENTAL TAB -->
              <div data-type="tabpage" id="subrentaltabpage" class="subrentalgrid notcombined tabpage" data-tabid="subrentaltab" data-render="false">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Sub-Rental Items">
                  <div class="wideflexrow">
                    <div data-issubgrid="true" data-control="FwGrid" data-grid="OrderItemGrid" data-securitycaption="Sub-Rental Item-totals"></div>
                  </div>
                </div>
              </div>

              <!-- DELIVER/SHIP TAB -->
              <div data-type="tabpage" id="delivershiptabpage" class="tabpage" data-tabid="delivershiptab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 425px;">
                      <!-- Outgoing Section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Outgoing">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield outtype" data-caption="Type" data-datafield="OutDeliveryDeliveryType" style="flex:1 1 175px;"></div>
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
                      <!-- Ship Via Outgoing -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Ship Via">
                        <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" data-validationname="VendorValidation" class="fwcontrol fwformfield" data-caption="Carrier" data-datafield="OutDeliveryCarrierId" data-displayfield="OutDeliveryCarrier" data-formbeforevalidate="beforeValidateCarrier"></div>
                            <div data-control="FwFormField" data-type="validation" data-validationname="ShipViaValidation" class="fwcontrol fwformfield" data-caption="Ship Via" data-datafield="OutDeliveryShipViaId" data-displayfield="OutDeliveryShipVia" data-formbeforevalidate="beforeValidateOutShipVia"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 125px;">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Address" data-datafield="OutDeliveryAddressType">
                          <div data-value="DEAL" data-caption="Deal"></div>
                          <div data-value="OTHER" data-caption="Other"></div>
                          <div data-value="VENUE" data-caption="Venue"></div>
                          <div data-value="WAREHOUSE" data-caption="Warehouse"></div>
                        </div>
                      </div>
                      <div class="flexcolumn" style="flex:1 1 300px;">
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

                  <!-- Incoming Section -->
                  <div class="flexcolumn" style="flex:1 1 425px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Incoming">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield intype" data-caption="Type" data-datafield="InDeliveryDeliveryType" style="flex:1 1 175px;"></div>
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
              </div>

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
