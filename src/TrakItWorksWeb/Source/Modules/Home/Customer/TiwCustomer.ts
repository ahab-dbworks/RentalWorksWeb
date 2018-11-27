class TiwCustomer extends Customer {

    constructor() {
      super();
      this.id = '8237418B-923D-4044-951F-98938C1EC3DE';
    }

  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}

  getBrowseTemplate(): string {
    return `
      <div data-name="Customer" data-control="FwBrowse" data-type="Browse" id="CustomerBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="TiwCustomerController">
      <div class="column" data-width="0" data-visible="false">
        <div class="field" data-isuniqueid="true" data-datafield="CustomerId" data-browsedatatype="key" ></div>
      </div>
      <div class="column" data-width="300px" data-visible="true">
        <div class="field" data-caption="Customer" data-datafield="Customer" data-browsedatatype="text" data-sort="asc"></div>
      </div>
      <div class="column" data-width="200px" data-visible="true">
        <div class="field" data-caption="No." data-datafield="CustomerNumber" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="200px" data-visible="true">
        <div class="field" data-caption="Type" data-datafield="CustomerType" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="200px" data-visible="true">
        <div class="field" data-caption="Status" data-datafield="CustomerStatus" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="auto" data-visible="true"></div>
    </div>`;
  }
  getFormTemplate(): string {
    return `
      <div id="customerform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Customer" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TiwCustomerController">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="CustomerId"></div>
        <div id="customerform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
          <div class="tabs">
            <div data-type="tab" id="customertab" class="tab" data-tabpageid="customertabpage" data-caption="Customer"></div>
            <div data-type="tab" id="dealtab" class="tab submodule" data-tabpageid="dealtabpage" data-caption="Deal"></div>
            <div data-type="tab" id="orderhistorytab" class="tab" data-tabpageid="orderhistorytabpage" data-caption="Order History"></div>
            <div data-type="tab" id="repairhistorytab" class="tab" data-tabpageid="repairhistorytabpage" data-caption="Repair History"></div>
            <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
            <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
            <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            <div data-type="tab" id="documentstab" class="tab" data-tabpageid="documentstabpage" data-caption="Documents"></div>-->
          </div>

          <div class="tabpages">
            <!-- ##### Customer tab ##### -->
            <div data-type="tabpage" id="customertabpage" class="tabpage" data-tabid="customertab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 550px;">
                    <!-- Customer section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Customer" data-noduplicate="true" style="flex:1 1 350px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="CustomerNumber" data-noduplicate="true" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="flex:1 1 225px;" data-required="true"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer Category" data-datafield="CustomerCategoryId" data-displayfield="CustomerCategory" data-validationname="CustomerCategoryValidation" style="flex:1 1 225px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="CustomerTypeId" data-displayfield="CustomerType" data-validationname="CustomerTypeValidation" data-required="true" style="flex:1 1 225px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 225px;">
                    <!-- Status section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CustomerStatusId" data-displayfield="CustomerStatus" data-validationname="CustomerStatusValidation" data-required="true" style="width:175px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusAsOf" data-enabled="false" style="float:left;width:100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Terms and Conditions on File" data-datafield="TermsAndConditionsOnFile" style="float:left;width:225px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:1 1 550px;">
                    <!-- Address section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 225px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 250px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="width:175px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <!-- Contact Numbers section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="800 Phone" data-datafield="Phone800" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Other" data-datafield="OtherPhone" style="flex:1 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" style="flex:1 1 250px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### end Customer tab ##### -->

            <!-- ##### Deal tab ##### -->
            <div data-type="tabpage" id="dealtabpage" class="tabpage submodule deal-page" data-tabid="dealtab"></div>

            <!-- ##### Order History tab ##### -->
            <div data-type="tabpage" id="orderhistorytabpage" class="tabpage" data-tabid="orderhistorytab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order History">
                    <div class="flexrow">##### ADD MISSING ORDER HISTORY GRID #####
                      <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"></div>
                    </div>
                  </div>
                </div>
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order History Total" style="flex:0 0 175px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### end Order History tab ##### -->

            <!-- ##### Repair History tab ##### -->
            <div data-type="tabpage" id="repairhistorytabpage" class="tabpage" data-tabid="repairhistorytab">
              <div class="flexpage">
                <div class="flexrow" style="flex:1 1 500px">
                  <!-- Location Tax Options section -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair History">
                    <div class="flexrow">##### ADD MISSING REPAIR HISTORY GRID #####
                      <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### end Repair History tab ##### -->

            <!-- ##### Shipping tab ##### -->
            <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="flexcolumn" style="flex:0 1 200px;">
                    <!-- Shipping Address section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="ShippingAddressType" style="flex:0 1 225px;">
                          <div data-value="CUSTOMER" data-caption="Use Customer"></div>
                          <div data-value="OTHER" data-caption="Use Other"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexcolumn" style="flex:0 1 550px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Shipping Address">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Attention" data-datafield="ShipAttention" data-enabled="false" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="flex:1 1 275px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="State" data-datafield="ShipState" data-enabled="false" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield shippingaddress" data-caption="Country" data-datafield="CountryId" data-enabled="false" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### end Shipping tab ##### -->

            <!-- ##### Contacts tab ##### -->
            <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
              <div class="flexpage">
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                    <div class="flexrow">
                      <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Vendor Contacts"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### end Contacts tab ##### -->

            <!-- ##### Notes tab ##### -->
            <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
              <div class="flexpage">
                <div class="flexrow" style="flex:1 1 750px;">
                  <!-- Notes section -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                    <div class="flexrow">
                      <div data-control="FwGrid" data-grid="CustomerNoteGrid" data-securitycaption="Customer Note"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### end Notes tab ##### -->

            <!-- ##### Documents tab ##### -->
            <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
              <div class="flexpage">
                <div class="flexrow" style="flex:1 1 120px;">
                  <!-- Notes section -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Documents">
                    <div class="flexrow">
                      <div data-control="FwGrid" data-grid="CustomerDocumentsGrid" data-securitycaption="Customer Document"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <!-- ##### end Documents tab ##### -->
          </div>
        </div>`;
  }
}

var TiwCustomerController = new TiwCustomer();
