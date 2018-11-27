﻿class TiwDeal extends Deal {

  constructor() {
      super();
      this.id = '393DE600-2911-4753-85FD-ABBC4F0B1407';
  }
    //browseModel: any = {};

    //getBrowseTemplate(): void {
    //    //let template = super.getBrowseTemplate();
    //    //return template;
    //}
    //##################################################
    getBrowseTemplate(): string {
        return `
        <div data-name="Deal" data-control="FwBrowse" data-type="Browse" id="DealBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="TiwDealController" data-hasinactive="true">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="DealId" data-browsedatatype="key" ></div>
          </div>
          <div class="column" data-width="300px" data-visible="true">
            <div class="field" data-caption="Deal" data-datafield="Deal" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Deal Number" data-datafield="DealNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Deal Type" data-datafield="DealType" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="150px" data-visible="true">
            <div class="field" data-caption="Deal Status" data-datafield="DealStatus" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="300px" data-visible="true">
            <div class="field" data-caption="Customer" data-datafield="Customer" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
  }
    //##################################################
    getFormTemplate(): string {
        return `
        <div id="dealform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Deal" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TiwDealController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="DealId"></div>
          <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="dealtab" class="tab" data-tabpageid="dealtabpage" data-caption="Deal"></div>
              <div data-type="tab" id="ordertab" class="tab submodule" data-tabpageid="ordertabpage" data-caption="Order"></div>
              <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="dealtabpage" class="tabpage" data-tabid="dealtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Deal section -->
                    <div class="flexcolumn" style="flex:1 1 700px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Deal" style="flex:1 1 300px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="DealNumber" data-required="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-required="true" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" data-required="true" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DealTypeId" data-displayfield="DealType" data-validationname="DealTypeValidation" data-required="true" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Classification" data-datafield="DealClassificationId" data-displayfield="DealClassification" data-validationname="DealClassificationValidation" style="flex:1 1 200px;"></div>
                          <!--<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Production Type" data-datafield="ProductionTypeId" data-displayfield="ProductionType" data-validationname="ProductionTypeValidation" style="flex:1 1 200px;"></div>-->
                        </div>
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 150px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="DealStatusId" data-displayfield="DealStatus" data-validationname="DealStatusValidation" data-required="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusAsOf" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Expected Wrap Date" data-datafield="ExpectedWrapDate" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- Address section -->
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="City" data-datafield="City" style="flex:2 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="State" data-datafield="State" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield deal_address" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Contact Numbers section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="800 Phone" data-datafield="Phone800" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Other" data-datafield="PhoneOther" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- CSR section -->
                    <div class="flexcolumn" style="flex:1 1 275px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer Service Representative">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="CSR" data-datafield="CsrId" data-displayfield="Csr" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Default Agent / Project Manager section -->
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Agent / Project Manager">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="DefaultAgentId" data-displayfield="DefaultAgent" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="DefaultProjectManagerId" data-displayfield="DefaultProjectManager" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            
              <!-- ##### ORDER tab ##### -->
              <div data-type="tabpage" id="ordertabpage" class="tabpage submodule order" data-tabid="ordertab"></div>

              <!-- ##### SHIPPING tab ##### -->
              <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 275px;">
                      <!-- Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield shipping_address_type_radio" data-caption="" data-datafield="ShippingAddressType" style="flex:1 1 250px;">
                            <div data-value="CUSTOMER" data-caption="Use Customer"></div>
                            <div data-value="DEAL" data-caption="Use Deal"></div>
                            <div data-value="OTHER" data-caption="Use Other"></div>
                          </div>
                        </div>
                      </div>
                      <!-- Default Deliver section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Outgoing Delivery" style="margin-top:13px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultOutgoingDeliveryType" style="flex:1 1 250px;">
                            <div data-value="DELIVER" data-caption="Deliver to Customer"></div>
                            <div data-value="SHIP" data-caption="Ship to Customer"></div>
                            <div data-value="PICK UP" data-caption="Customer Pick Up"></div>
                          </div>
                        </div>
                      </div>
                      <!-- Default Delivery Return section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Incoming Delivery" style="margin-top:12px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultIncomingDeliveryType" style="flex:1 1 250px;">
                            <div data-value="DELIVER" data-caption="Customer Deliver"></div>
                            <div data-value="SHIP" data-caption="Customer Ship"></div>
                            <div data-value="PICK UP" data-caption="Pick Up from Customer"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 550px;">
                      <!-- Default Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="ShipAttention" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="ShipState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="ShipCountryId" data-displayfield="ShipCountry" data-validationname="CountryValidation" data-enabled="false" style="float:left;width:175px;"></div>
                        </div>
                      </div>
                      <!-- Carrier section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Carrier" style="flex:1 1 550px;">
                        <div class="flexrow">########## ADD CARRIER GRID HERE ##########
                          <div data-control="FwGrid" data-grid="DealShipperGrid" data-securitycaption="Deal Shipper"></div>
                          
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### CONTACTS ##### tab -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                      <div class="flexrow">########## ADD CONTACTS GRID HERE ##########
                        <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Deal Contacts"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### NOTES tab ##### -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                      <div class="flexrow">########## ADD NOTES GRID HERE ##########
                        <div data-control="FwGrid" data-grid="DealNoteGrid" data-securitycaption="Deal Notes"></div>
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

var TiwDealController = new TiwDeal();