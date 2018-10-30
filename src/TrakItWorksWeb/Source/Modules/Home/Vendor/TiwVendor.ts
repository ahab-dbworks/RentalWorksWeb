﻿class TiwVendor extends Vendor {

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
    <div data-name="Vendor" data-control="FwBrowse" data-type="Browse" id="VendorBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="VendorController" data-hasinactive="true">
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
    <div id="vendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Vendor" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="VendorController">
      <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="VendorId"></div>
      <div id="vendorform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
        <div class="tabs">
          <div data-type="tab" id="vendortab" class="tab" data-tabpageid="vendortabpage" data-caption="Vendor"></div>
          <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
          <div data-type="tab" id="taxtab" class="tab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
          <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
          <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
          <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
        </div>
        <div class="tabpages">
          <div data-type="tabpage" id="vendortabpage" class="tabpage" data-tabid="vendortab">
            <div class="formpage">
              <div class="formrow">
                <div class="formcolumn" style="width:80%;">

                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield vendertyperadio" data-caption="Vendor Type" data-datafield="VendorNameType" style="float:left;width:250px;">
                        <div data-value="COMPANY" data-caption="Company"></div>
                        <div data-value="PERSON" data-caption="Person"></div>
                      </div>
                      <div id="company_panel" class="type_panels">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Vendor" data-noduplicate="true" style="float:left;width:400px;"></div>
                      </div>
                      <div id="person_panel" class="type_panels" style="display: none;">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Salutation" data-datafield="Salutation" data-noduplicate="true" data-required="false" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name" data-datafield="FirstName" data-noduplicate="true" data-required="false" style="float:left;width:150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="" data-datafield="MiddleInitial" data-noduplicate="true" data-required="false" style="float:left;width:30px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name" data-datafield="LastName" data-noduplicate="true" data-required="false" style="float:left;width:150px;"></div>
                      </div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor No" data-datafield="VendorNumber" data-noduplicate="true" style="float:left;width:150px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal ID No" data-datafield="FederalIdNumber" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;width:225px;" data-required="true"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield vendorclass" data-caption="Class" data-datafield="VendorClassId" data-displayfield="VendorClass" data-validationname="VendorClassValidation" style="float:left;width:225px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield customer" data-caption="Rental Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="float:left;width:300px"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:20%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status" style="padding-left:1px;">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Active Date" data-datafield="ActiveDate" data-enabled="false" data-readonly="true" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Inactive Date" data-datafield="InactiveDate" data-enabled="false" data-readonly="true" style="float:left;width:100px;"></div>
                  </div>
                </div>
              </div>
              <div class="formrow">
                <div class="formcolumn" style="width:50%;">

                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="Address1" style="float:left;width:275px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="float:left;width:250px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="float:left;width:275px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" style="float:left;width:150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ZipCode" style="float:left;width:100px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="float:left;width:175px;"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:25%;">

                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Numbers" style="padding-left:1px;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="float:left;width:125px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="800 Phone" data-datafield="Phone800" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Other" data-datafield="OtherPhone" style="float:left;width:125px;"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:25%;">

                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Web / Email" style="padding-left:1px;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" style="float:left;width:250px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" style="float:left;width:250px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="formrow">
                <div class="formcolumn" style="width:50%;">

                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity Type">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rent" data-datafield="SubRent" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sales" data-datafield="SubSales" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Misc" data-datafield="SubMisc" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Labor" data-datafield="SubLabor" style="float:left;width:125px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Vehicle" data-datafield="SubVehicle" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Repair" data-datafield="Repair" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental Inventory" data-datafield="RentalInventory" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales / Parts" data-datafield="SalesPartsInventory" style="float:left;width:125px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Manufacturer" data-datafield="Manufacturer" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Freight" data-datafield="Freight" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Insurance" data-datafield="Insurance" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Consignment" data-datafield="Consignment" style="float:left;width:125px;"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:25%;">
                  <div class="formrow">

                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Sub-Rental" style="padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="D/W" data-datafield="DefaultSubRentDaysInWeek" data-required="true" style="float:left;width:75px;"></div>
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Discount %" data-datafield="DefaultSubRentDiscountPercent" data-required="true" style="float:left;width:100px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formrow">

                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Sub-Sales">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Discount %" data-datafield="DefaultSubSaleDiscountPercent" data-required="true" style="float:left;width:100px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
            <div class="formpage">
              <div class="formrow">
                <div class="formcolumn" style="width:55%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="select" class="fwcontrol fwformfield defaultrate" data-caption="Default Rate" data-datafield="DefaultRate" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sub-Rental Billing Cylce" data-datafield="BillingCycleId" data-displayfield="BillingCycle" data-validationname="BillingCycleValidation" style="float:left;width:225px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="float:left;width:225px;"></div>h
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Account Number" data-datafield="AccountNumber" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Default PO Class" data-datafield="DefaultPoClassificationId" data-displayfield="DefaultPoClassification" data-validationname="POClassificationValidation" style="float:left;width:225px;"></div>
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Organization Type" data-datafield="OrganizationTypeId" data-displayfield="OrganizationType" data-validationname="OrganizationTypeValidation" style="float:left;width:225px;"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:30%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Currency" style="padding-left:1px;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Currency" data-datafield="DefaultCurrencyId" data-displayfield="DefaultCurrencyCode" style="float:left;width:100px;" data-validationname="CurrencyValidation"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DefaultCurrency" data-enabled="false" style="float:left;width:200px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="formrow">
                <div class="formcolumn" style="width:50%;">

                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Remit To Address">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="RemitAddress1" style="float:left;width:275px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="RemitAddress2" style="float:left;width:250px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="RemitCity" style="float:left;width:275px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="RemitState" style="float:left;width:150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="RemitZipCode" style="float:left;width:100px;"></div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="RemitCountryId" data-displayfield="RemitCountry" data-validationname="CountryValidation" style="float:left;width:175px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Payee No" data-datafield="RemitPayeeNo" style="float:left;width:175px;"></div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:34%;padding-left:1px;">

                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="External ID">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ID" data-datafield="ExternalId" style="float:left;width:175px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="formrow">
                <div data-control="FwGrid" data-grid="ContactCompany" data-securitycaption="Contact Company"></div>
              </div>
            </div>
          </div>
          <div data-type="tabpage" id="taxtabpage" class="tabpage" data-tabid="taxtab">
            <div class="formpage">
              <div class="formrow" style="width:100%;">
                <div id="companytaxoptiongrid" data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Company Tax Grid" style="min-width:240px;max-width:400px;"></div>
              </div>
              <div class="formrow" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" id="" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield TaxOption" data-datafield="" data-displayfield="" data-caption="Tax Option" data-enabled="false"></div>
                    <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield RentalTaxRate1" data-datafield="" data-displayfield="" data-caption="Rental %" data-enabled="false" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield SalesTaxRate1" data-datafield="" data-displayfield="" data-caption="Sales %" data-enabled="false" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield LaborTaxRate1" data-datafield="" data-displayfield="" data-caption="Labor %" data-enabled="false" style="float:left;width:100px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
            <div class="formpage">
              <div class="formrow" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracking No.">
                  <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Tracking No. Hyperlink" data-datafield="ShippingTrackingLink" style="width:550px;"></div>
                </div>
              </div>
              <div class="formrow" style="width:100%;">
                <div class="formcolumn" style="width:50%;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Delivery">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Delivery" data-datafield="DefaultIncomingDeliveryType" style="width:265px;">
                        <div data-value="DELIVER" data-caption="Vendor Deliver"></div>
                        <div data-value="SHIP" data-caption="Ship"></div>
                        <div data-value="PICK UP" data-caption="Pick Up"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="formcolumn" style="width:50%;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Return Delivery" style="padding-left:1px;">
                      <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Return Delivery" data-datafield="DefaultOutgoingDeliveryType" style="width:265px;">
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
          <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
            <div class="formpage">
              <div class="formcolumn" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Vendor Contacts"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
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