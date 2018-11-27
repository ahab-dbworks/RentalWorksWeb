class TiwVendor extends Vendor {

  constructor() {
    super();
    this.id = '92E6B1BE-C9E1-46BD-91A0-DF257A5F909A';
  }
  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}
  getBrowseTemplate(): string {
    return `
    <div data-name="Vendor" data-control="FwBrowse" data-type="Browse" id="VendorBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="TiwVendorController" data-hasinactive="true">
      <div class="column" data-width="0" data-visible="false">
        <div class="field" data-isuniqueid="true" data-datafield="VendorId" data-browsedatatype="key" ></div>
      </div>
      <div class="column" data-width="0" data-visible="false">
        <div class="field" data-datafield="Inactive" data-browsedatatype="text"  data-visible="false"></div>
      </div>
      <div class="column" data-width="300px" data-visible="true">
        <div class="field" data-caption="Vendor" data-datafield="VendorDisplayName" data-datatype="text" data-sort="asc"></div>
      </div>
      <div class="column" data-width="40px" data-visible="true">
        <div class="field" data-caption="Vendor Number" data-datafield="VendorNumber" data-datatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
        <div class="field" data-caption="Main Phone" data-datafield="Phone" data-datatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="200px" data-visible="true">
        <div class="field" data-caption="Address" data-datafield="Address1" data-datatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px" data-visible="true">
        <div class="field" data-caption="City" data-datafield="City" data-datatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="50px" data-visible="true">
        <div class="field" data-caption="State" data-datafield="State" data-datatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="100px" data-visible="true">
        <div class="field" data-caption="Zipcode" data-datafield="ZipCode" data-datatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="auto" data-visible="true"></div>
    </div>`;
  }
  getFormTemplate(): string {
    return `
    <div id="vendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Vendor" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="TiwVendorController">
      <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="VendorId"></div>
      <div id="vendorform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs">
          <div data-type="tab" id="vendortab" class="tab" data-tabpageid="vendortabpage" data-caption="Vendor"></div>
          <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
          <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
          <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
        </div>

        <div class="tabpages">
          <!-- ##### VENDOR tab ##### -->
          <div data-type="tabpage" id="vendortabpage" class="tabpage" data-tabid="vendortab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 700px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield vendertyperadio" data-caption="Vendor Type" data-datafield="VendorNameType" style="flex:0 1 125px;">
                        <div data-value="COMPANY" data-caption="Company"></div>
                        <div data-value="PERSON" data-caption="Person"></div>
                      </div>
                      <div id="company_panel" class="type_panels" style="flex:1 1 275px;">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Vendor" data-noduplicate="true" style="flex:1 1 275px;"></div>
                      </div>
                      <div id="person_panel" class="type_panels" style="display: none;" style="flex:1 1 275px;">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Salutation" data-datafield="Salutation" data-noduplicate="true" data-required="false" style="flex:0 1 30px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name" data-datafield="FirstName" data-noduplicate="true" data-required="false" style="flex:0 1 50px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="MiddleInitial" data-noduplicate="true" data-required="false" style="flex:0 1 30px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name" data-datafield="LastName" data-noduplicate="true" data-required="false" style="flex:0 1 75px;"></div>
                      </div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor No" data-datafield="VendorNumber" data-noduplicate="true" style="flex:0 1 150px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal ID No" data-datafield="FederalIdNumber" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="flex:1 1 225px;" data-required="true"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield vendorclass" data-caption="Class" data-datafield="VendorClassId" data-displayfield="VendorClass" data-validationname="VendorClassValidation" style="flex:1 1 225px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield customer" data-caption="Rental Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="flex:1 1 300px"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 150px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Active Date" data-datafield="ActiveDate" data-enabled="false" data-readonly="true" style="flex:1 1 100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Inactive Date" data-datafield="InactiveDate" data-enabled="false" data-readonly="true" style="flex:1 1 100px;"></div>
                  </div>
                </div>
              </div>
              <!-- Address section -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 550px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 275px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 275px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Numbers">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="800 Phone" data-datafield="Phone800" style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Other" data-datafield="OtherPhone" style="flex:1 1 125px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Web / Email">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" style="flex:1 1 250px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" style="flex:1 1 250px;"></div>
                  </div>
                </div>
              </div>
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity Type">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental Inventory" data-datafield="RentalInventory" style="flex:1 1 125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales / Parts" data-datafield="SalesPartsInventory" style="flex:1 1 125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rent" data-datafield="SubRent" style="flex:1 1 125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sales" data-datafield="SubSales" style="flex:1 1 125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Manufacturer" data-datafield="Manufacturer" style="flex:1 1 125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Freight" data-datafield="Freight" style="flex:1 1 125px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- ##### SHIPPING tab ##### -->
          <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracking No." style="flex:0 1 550px;">
                  <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Tracking No. Hyperlink" data-datafield="ShippingTrackingLink"></div>
                </div>
              </div>
              <div class="flexrow">
                <div class="flexcolumn" style="flex:0 1 225px;">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Delivery">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultIncomingDeliveryType" style="flex:1 1 200px;">
                        <div data-value="DELIVER" data-caption="Vendor Deliver"></div>
                        <div data-value="SHIP" data-caption="Ship"></div>
                        <div data-value="PICK UP" data-caption="Pick Up"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:0 1 225px;">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Return Delivery">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultOutgoingDeliveryType" style="flex:1 1 200px;">
                        <div data-value="DELIVER" data-caption="Deliver"></div>
                        <div data-value="SHIP" data-caption="Ship"></div>
                        <div data-value="PICK UP" data-caption="Vendor Pick Up"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- ##### CONTACTS tab ##### -->
          <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts" style="flex:1 1 1200px;">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Vendor Contacts"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- ##### NOTES tab ##### -->
          <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
            <div class="formpage">
              <div class="formrow" style="width:100%;">
                <div class="formrow" style="width:100%;">
                  <div data-control="FwGrid" id="vendornotegrid" data-grid="VendorNoteGrid" data-securitycaption="Note Grid" style="min-width:240px;"></div>
                </div>
              </div>
              <div class="formrow" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Updated">
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NoteDate" data-caption="Opened" data-enabled="false" data-datafield="InputDate" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NotesBy" data-caption="By" data-enabled="false" data-datafield="ShippingTrackingLink" style="float:left;width:350px;"></div>
                  </div>
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield Datestamp" data-caption="Modified Last" data-enabled="false" data-datafield="DateStamp" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NotesBy" data-caption="By" data-enabled="false" data-datafield="ShippingTrackingLink" style="float:left;width:350px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>`;
  }
}
var TiwVendorController = new TiwVendor();