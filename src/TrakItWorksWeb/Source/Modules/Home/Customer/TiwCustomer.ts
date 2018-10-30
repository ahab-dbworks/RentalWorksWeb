﻿class TiwCustomer extends Customer {

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
      <div data-name="Customer" data-control="FwBrowse" data-type="Browse" id="CustomerBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="CustomerController">
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
      <div id="customerform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Customer" data-rendermode="template" data-mode=""
     data-hasaudit="false" data-controller="CustomerController">
  <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="CustomerId"></div>
  <div id="customerform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs">
      <div data-type="tab" id="customertab" class="tab" data-tabpageid="customertabpage" data-caption="Customer"></div>
      <div data-type="tab" id="dealtab" class="tab submodule" data-tabpageid="dealtabpage" data-caption="Deal"></div>
      <!--<div data-type="tab" id="revenuetab" class="tab" data-tabpageid="revenuetabpage" data-caption="> Revenue"></div>
      <div data-type="tab" id="quotehistorytab" class="tab" data-tabpageid="quotehistorytabpage" data-caption="> Quote History"></div>
      <div data-type="tab" id="orderhistorytab" class="tab" data-tabpageid="orderhistorytabpage" data-caption="> Order History"></div>
      <div data-type="tab" id="repairhistorytab" class="tab" data-tabpageid="repairhistorytabpage" data-caption="> Repair History"></div>
      <div data-type="tab" id="invoicehistorytab" class="tab" data-tabpageid="invoicehistorytabpage" data-caption="> Invoice History"></div>
      <div data-type="tab" id="actsscenestab" class="tab" data-tabpageid="actsscenestabpage" data-caption="Acts / Scenes" style="background-color:#f5f5f5;"></div>
      <div data-type="tab" id="showscheduletab" class="tab" data-tabpageid="showscheduletabpage" data-caption="Show Schedule" style="background-color:#f5f5f5;"></div>
      <div data-type="tab" id="characterstab" class="tab" data-tabpageid="characterstabpage" data-caption="Characters" style="background-color:#f5f5f5;"></div>
      <div data-type="tab" id="wardrobetab" class="tab" data-tabpageid="wardrobetabpage" data-caption="Wardrobe" style="background-color:#f5f5f5;"></div>
      <div data-type="tab" id="brochuretab" class="tab" data-tabpageid="brochuretabpage" data-caption="Brochure" style="background-color:#f5f5f5;"></div>
      <div data-type="tab" id="projecttab" class="tab" data-tabpageid="projecttabpage" data-caption="Project"></div>-->
      <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
      <!--<div data-type="tab" id="discountsdwtab" class="tab" data-tabpageid="discountsdwtabpage" data-caption="> Discounts &amp; D/W"></div>
      <div data-type="tab" id="flatpotab" class="tab" data-tabpageid="flatpotabpage" data-caption="Flat PO" style="background-color:#f5f5f5;"></div>
      <div data-type="tab" id="financechargestab" class="tab" data-tabpageid="financechargestabpage" data-caption="> Finance Charges"></div>
      <div data-type="tab" id="episodicscheduletab" class="tab" data-tabpageid="episodicscheduletabpage" data-caption="> Episodic Schedule"></div>
      <div data-type="tab" id="hiatusscheduletab" class="tab" data-tabpageid="hiatusscheduletabpage" data-caption="Hiatus Schedule" style="background-color:#f5f5f5;"></div>
      <div data-type="tab" id="orderprioritytab" class="tab" data-tabpageid="orderprioritytabpage" data-caption="> Order Priority"></div>
      <div data-type="tab" id="dealscheduletab" class="tab" data-tabpageid="dealscheduletabpage" data-caption="> Deal Schedule"></div>
      <div data-type="tab" id="ordergroupstab" class="tab" data-tabpageid="ordergroupstabpage" data-caption="> Order Groups"></div>-->
      <div data-type="tab" id="credittab" class="tab" data-tabpageid="credittabpage" data-caption="Credit"></div>
      <!--<div data-type="tab" id="aragingtab" class="tab" data-tabpageid="aragingtabpage" data-caption="> A/R Aging"></div>
      <div data-type="tab" id="depletingdepositstab" class="tab" data-tabpageid="depletingdepositstabpage" data-caption="> Depleting Deposits"></div>-->
      <div data-type="tab" id="insurancetab" class="tab" data-tabpageid="insurancetabpage" data-caption="Insurance"></div>
      <!--<div data-type="tab" id="securitydeposittab" class="tab" data-tabpageid="securitydeposittabpage" data-caption="> Security Deposit"></div>-->
      <div data-type="tab" id="taxtab" class="tab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
      <div data-type="tab" id="optionstab" class="tab" data-tabpageid="optionstabpage" data-caption="Options"></div>
      <!--<div data-type="tab" id="splitrentaltab" class="tab" data-tabpageid="splitrentaltabpage" data-caption="Split Rental"></div>-->
      <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
      <!--<div data-type="tab" id="additionaladdressestab" class="tab" data-tabpageid="additionaladdressestabpage" data-caption="> Additional Addresses"></div>-->
      <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
      <!--<div data-type="tab" id="rebaterentaltab" class="tab" data-tabpageid="rebaterentaltabpage" data-caption="Rebate Rental"></div>-->
      <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
      <div data-type="tab" id="imagetab" class="tab" data-tabpageid="imagetabpage" data-caption="Image"></div>
      <!--<div data-type="tab" id="rentalworksnettab" class="tab" data-tabpageid="rentalworksnettabpage" data-caption="RentalWorks.NET"></div>
      <div data-type="tab" id="webcontenttab" class="tab" data-tabpageid="webcontenttabpage" data-caption="> Web Content"></div>
      <div data-type="tab" id="eventtab" class="tab" data-tabpageid="eventtabpage" data-caption="Event"></div>
      <div data-type="tab" id="summarytab" class="tab" data-tabpageid="summarytabpage" data-caption="Summary"></div>
      <div data-type="tab" id="documentstab" class="tab" data-tabpageid="documentstabpage" data-caption="Documents"></div>-->
    </div>

    <div class="tabpages">
      <!-- ##### Customer tab ##### -->
      <div data-type="tabpage" id="customertabpage" class="tabpage" data-tabid="customertab">
        <div class="formpage">
          <div class="formrow">
            <div class="formcolumn" style="width:auto;">
              <!-- Customer section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Customer" data-noduplicate="true" style="float:left;width:350px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="CustomerNumber" data-noduplicate="true" style="float:left;width:150px;"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="float:left;width:225px;" data-required="true"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="float:left;width:225px;"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="CustomerTypeId" data-displayfield="CustomerType" data-validationname="CustomerTypeValidation" data-required="true" style="float:left;width:225px;"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer Category" data-datafield="CustomerCategoryId" data-displayfield="CustomerCategory" data-validationname="CustomerCategoryValidation" style="float:left;width:225px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parent Customer" data-datafield="ParentCustomerId" data-displayfield="ParentCustomer" data-validationname="CustomerValidation" data-validationpeek="true" style="float:left;width:350px;"></div>
                </div>
              </div>
            </div>
            <div class="formcolumn" style="width:300px;">
              <!-- Status section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CustomerStatusId" data-displayfield="CustomerStatus" data-validationname="CustomerStatusValidation" data-required="true" style="width:175px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusAsOf" data-enabled="false" style="float:left;width:100px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Terms and Conditions on File" data-datafield="TermsAndConditionsOnFile"
                       style="float:left;width:250px;"></div>
                </div>
              </div>
            </div>
          </div>
          <div class="formrow">
            <div class="formcolumn" style="width:50%;">
              <!-- Address section -->
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
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="width:175px;"></div>
                </div>
              </div>
            </div>
            <div class="formcolumn" style="width:25%;">
              <!-- Contact Numbers section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Numbers">
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
              <!-- Web/Email section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Internet" style="float:left;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" style="float:left;width:250px;"></div>
                </div>
              </div>
            </div>
          </div>
          <div class="formrow">
            <div class="formcolumn" style="width:275px;">
              <!-- CSR section
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer Service Representative">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="CSR" data-datafield="customer.csr" style="width:250px;"></div>
              </div>
            </div>
          </div>
          <div class="formcolumn" style="width:525px;">
            Default Agent / Project Manager section
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Agent / Project Manager">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="InsuranceAgent" style="float:left;width:250px;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="customerprojectmanager" style="float:left;width:250px;"></div>
              </div>
            </div>
          </div>
          <div class="formcolumn" style="width:400px;">
             Play Schedule section
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Play Schedule">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Preview" data-datafield="customer.agent" style="float:left;width:125px;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Open" data-datafield="customerprojectmanager" style="float:left;width:125px;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Wrap" data-datafield="customerprojectmanager" style="float:left;width:125px;"></div>
              </div>
            </div>
            -->
            </div>
          </div>
        </div>
      </div>
      <div data-type="tabpage" id="dealtabpage" class="tabpage submodule deal" data-tabid="dealtab">
      </div>
        <!-- ##### end Customer tab ##
    <div data-type="tabpage" id="picklisttabpage" class="tabpage submodule picklist" data-tabid="picklisttab">### -->
        <!-- ##### Revenue tab ##### -->
        <div data-type="tabpage" id="revenuetabpage" class="tabpage" data-tabid="revenuetab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <div class="formcolumn" style="width:22%;">
                <!-- Period section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Period" style="width:100%;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="From" data-datafield="vendor.modifiedby" style="float:left;width:130px;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="To" data-datafield="vendor.modifiedby" style="float:left;width:130px;"></div>
                  </div>
                </div>
                <!-- Filter section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Filter By">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="vendor.modifiedby" style="width:100%;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Company Department" data-datafield="vendor.modifiedby" style="width:100%;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="vendor.modifiedby" style="width:100%;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="vendor.modifiedby" style="width:100%;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="vendor.modifiedby" style="width:100%;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Event" data-datafield="vendor.modifiedby" style="width:100%;"></div>
                  </div>
                </div>
                <!-- Options section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Deduct Cost from Vendor Revenue" data-datafield="" style="width:100%;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:78%;">
                <div class="formrow">
                  <div class="formcolumn" style="width:63%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Revenue Type / I-Code Filter" style="padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental I-Code" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Bar Code / Serial No." data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rental Sale I-Code" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Final L&amp;D I-Code" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales I-Code" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Misc. I-Code" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Labor I-Code" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parts I-Code" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="I-Code Description" data-datafield="vendor.modifiedby" data-enabled="false" style="width:100%;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="formcolumn" style="width:37%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Facilities Filter" style="padding-left:1px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Building" data-datafield="vendor.modifiedby" style="float:left;width:100%;"></div>

                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Floor" data-datafield="vendor.modifiedby" style="float:left;width:25%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Space" data-datafield="vendor.modifiedby" style="float:left;width:75%;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Facilities Type" data-datafield="vendor.modifiedby" style="float:left;width:50%;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Activity Type" data-datafield="vendor.modifiedby" style="float:left;width:50%;"></div>
                      </div>
                    </div>
                  </div>
                </div>

                <div class="formrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Revenue Summary by Fiscal Month" style="padding-left:1px;width:100%;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"
                           style="min-height:350px;width:100%;border:1px solid silver;">########## ADD MISSING REVENUE SUMMARY GRID ##########</div>
                    </div>
                  </div>
                </div>
                <div class="formrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Revenue Summary Totals" style="padding-left:1px;width:100%;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Owned Qty" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Owned Revenue" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor Qty" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor Revenue" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Unassigned Qty" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Unassigned Revenue" data-datafield="vendor.modifiedby" data-enabled="false"
                           style="float:left;width:11%;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total Qty" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total Revenue" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
                      <button class="button theme calculate" style="float:left;margin-top:15px;margin-left:1%;line-height:12px;width:10%;font-size:10pt;">Calculate</button>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Revenue tab ##### -->
        <!-- ##### Quote History tab ##### -->
        <div data-type="tabpage" id="quotehistorytabpage" class="tabpage" data-tabid="quotehistorytab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <!-- Location Tax Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote History">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD MISSING QUOTE HISTORY GRID ##########</div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:30%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Filer">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Show Quotes" data-datafield="customer.addressoptionflg" style="width:300px;">
                    <div data-value="All" data-caption="All"></div>
                    <div data-value="Rental" data-caption="Rentals Only"></div>
                    <div data-value="Sales" data-caption="Sales Only"></div>
                    <div data-value="RentalWorksNet" data-caption="RentalWorks.NET Only"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:40%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="padding-left:1px;">
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Department" data-datafield="department" style="float:left;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                  </div>
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Locations" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>                                   				<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Agent Only" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Project Manager Only" data-datafield="Inactive" style="float:left;width:200px;margin-top:0px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:15%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote History Total" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="width:125px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <button class="button theme calculate" style="margin-top:5px;margin-left:5px;line-height:12px;width:115px;font-size:10pt;">Calculate</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Quote History tab ##### -->
        <!-- ##### Order History tab ##### -->
        <div data-type="tabpage" id="orderhistorytabpage" class="tabpage" data-tabid="orderhistorytab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <!-- Location Tax Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order History">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD MISSING ORDER HISTORY GRID ##########</div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:30%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Filer">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Show Orders" data-datafield="customer.addressoptionflg" style="width:300px;">
                    <div data-value="All" data-caption="All"></div>
                    <div data-value="Rental" data-caption="Rentals Only"></div>
                    <div data-value="Sales" data-caption="Sales Only"></div>
                    <div data-value="RentalSales" data-caption="Rental Sales Only"></div>
                    <div data-value="RentalSales" data-caption="Loss &amp; Damage Only"></div>
                    <div data-value="RentalSales" data-caption="Closed Only"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:40%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="padding-left:1px;">
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Department" data-datafield="department" style="float:left;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                  </div>
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Locations" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>                                   				<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Agent Only" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Project Manager Only" data-datafield="Inactive" style="float:left;width:200px;margin-top:0px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:15%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order History Total" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="width:125px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <button class="button theme calculate" style="margin-top:5px;margin-left:5px;line-height:12px;width:115px;font-size:10pt;">Calculate</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Order History tab ##### -->
        <!-- ##### Repair History tab ##### -->
        <div data-type="tabpage" id="repairhistorytabpage" class="tabpage" data-tabid="repairhistorytab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <!-- Location Tax Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair History">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD MISSING REPAIR HISTORY GRID ##########</div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:40%;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                <div class="formrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Department" data-datafield="department" style="float:left;"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname"
                       style="float:left;width:275px;"></div>
                </div>
                <div class="formrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Repair History tab ##### -->
        <!-- ##### Invoice History tab ##### -->
        <div data-type="tabpage" id="invoicehistorytabpage" class="tabpage" data-tabid="invoicehistorytab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <!-- Location Tax Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice History">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD MISSING INVOICE HISTORY GRID ##########</div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:30%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice Filer">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Show Invoices" data-datafield="customer.addressoptionflg" style="width:300px;">
                    <div data-value="All" data-caption="New and Approved"></div>
                    <div data-value="Rental" data-caption="Processed and Closed"></div>
                    <div data-value="Sales" data-caption="Processed"></div>
                    <div data-value="RentalWorksNet" data-caption="Closed"></div>
                    <div data-value="All" data-caption="Void"></div>
                    <div data-value="Rental" data-caption="Estimates"></div>
                    <div data-value="Sales" data-caption="Approved"></div>
                    <div data-value="RentalWorksNet" data-caption="New"></div>
                    <div data-value="RentalWorksNet" data-caption="All"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:40%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="padding-left:1px;">
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Filter by Department" data-datafield="department" style="float:left;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname"
                         style="float:left;width:275px;"></div>
                  </div>
                  <div class="formrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Locations" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Agent Only" data-datafield="Inactive" style="float:left;width:150px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Project Manager Only" data-datafield="Inactive" style="float:left;width:200px;margin-top:0px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:15%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice History Total" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="width:125px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <button class="button theme calculate" style="margin-top:5px;margin-left:5px;line-height:12px;width:115px;font-size:10pt;">Calculate</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Invoice History tab ##### -->
        <!-- ##### Acts / Scenes tab ##### -->
        <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
          <div class="formpage">

          </div>
        </div>
        <!-- ##### end Acts / Scenes tab ##### -->
        <!-- ##### Show Schedule tab ##### -->
        <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
          <div class="formpage">

          </div>
        </div>
        <!-- ##### end Show Schedule tab ##### -->
        <!-- ##### Characters tab ##### -->
        <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
          <div class="formpage">

          </div>
        </div>
        <!-- ##### end Characters tab ##### -->
        <!-- ##### Wardrobe tab ##### -->
        <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
          <div class="formpage">

          </div>
        </div>
        <!-- ##### end Wardrobe tab ##### -->
        <!-- ##### Brochure tab ##### -->
        <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
          <div class="formpage">

          </div>
        </div>
        <!-- ##### end Brochure tab ##### -->
        <!-- ##### Project tab ##### -->
        <div data-type="tabpage" id="projecttabpage" class="tabpage" data-tabid="projecttab">
          <div class="formpage">
            <div class="formrow" style="width:75%;">
              <!-- Notes section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Projects">
                <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes">########## ADD MISSING PROJECT GRID ##########</div>
              </div>
            </div>
            <div class="formrow">
              <!-- Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:550px;">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Locations" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show RentalWorks.NET Enabled" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Project tab ##### -->
        <!-- ##### Billing tab ##### -->
        <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
          <div class="formpage">
            <!-- Address / Contact Numbers / Web &amp; E-mail section -->
            <div class="formrow">
              <div class="formcolumn" style="float:left;width:425px;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Billing">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <!--<div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="customer.billingcycle" style="float:left;width:200px;"></div>
                <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Type" data-datafield="customer.paymenttype" style="float:left;width:200px;"></div>-->
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="float:left;width:200px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:375px;padding-left:1px;">
                <!-- Discounts &amp; D/W section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Discounts &amp; D/W">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Discount Template" data-datafield="UseDiscountTemplate" style="float:left;width:200px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield discount-validation" data-caption="Template" data-displayfield="DiscountTemplate" data-datafield="DiscountTemplateId" data-validationname="DiscountTemplateValidation" data-enabled="false" style="float:left;width:250px;"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:100%;">
                <!-- Billing Address section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Address">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Address Option" data-datafield="BillingAddressType" style="width:225px;">
                    <div data-value="CUSTOMER" data-caption="Use Customer"></div>
                    <div data-value="DEAL" data-caption="Use Deal"></div>
                    <div data-value="OTHER" data-caption="Use Other"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Attention 1" data-datafield="BillToAttention1" data-enabled="false" style="float:left;width:275px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Attention 2" data-datafield="BillToAttention2" data-enabled="false" style="float:left;width:250px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Address 1" data-datafield="BillToAddress1" data-enabled="false" style="float:left;width:275px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Address 2" data-datafield="BillToAddress2" data-enabled="false" style="float:left;width:250px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="float:left;width:275px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="State" data-datafield="BillToState" data-enabled="false" style="float:left;width:150px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="float:left;width:100px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield billingaddress" data-caption="Country" data-datafield="CountryId" data-enabled="false" data-displayfield="BillToCountry" data-validationname="CountryValidation" style="width:175px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Billing tab ##### -->
        <!-- ##### Discounts &amp; D/W tab ##### -->
        <div data-type="tabpage" id="discountsdwtabpage" class="tabpage" data-tabid="discountsdwtab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <div class="formcolumn" style="width:22%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Office">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Location" data-datafield="vendor.primarycontactname" style="width:225px;"></div>
                </div>
              </div>
              <div class="formcolumn" style="width:13%;padding-left:1px;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="customer.addressoptionflg" style="width:125px;">
                    <div data-value="All" data-caption="Rental"></div>
                    <div data-value="Rental" data-caption="Sales"></div>
                    <div data-value="Sales" data-caption="Labor"></div>
                    <div data-value="RentalWorksNet" data-caption="Misc."></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:39%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Update Quotes / Orders" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Active Quotes" data-datafield="Inactive" style="float:left;width:140px;margin-top:5px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Reserved Qutoes" data-datafield="Inactive" style="float:left;width:140px;margin-top:5px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Confirmed Orders" data-datafield="Inactive" style="float:left;width:140px;margin-top:5px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Active Orders" data-datafield="Inactive" style="float:left;width:140px;margin-top:5px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Complete Orders" data-datafield="Inactive" style="float:left;width:140px;margin-top:5px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Discount % for Items with Custom Rates" data-datafield="Inactive" style="float:left;width:420px;margin-top:5px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:26%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Standard Discounts &amp; D/W" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Rental %" data-datafield="vendor.modifiedby" style="float:left;width:90px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sales %" data-datafield="vendor.modifiedby" style="float:left;width:90px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Facilities %" data-datafield="vendor.modifiedby" style="float:left;width:90px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Rental D/W" data-datafield="vendor.modifiedby" style="float:left;width:90px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Facilities D/W" data-datafield="vendor.modifiedby" style="float:left;width:90px;"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:100%;">
              <!-- Location Tax Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rental Items">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD MISSING RENTAL/SALES/LABOR/MISC ITEMS GRID ##########</div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rates As Of" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:130px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Discounts &amp; D/W tab ##### -->
        <!-- ##### Flat PO tab ##### -->
        <div data-type="tabpage" id="flatpotabpage" class="tabpage" data-tabid="flatpotab">
          <div class="formpage">
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Flat PO" style="width:75%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"
                       style="min-height:150px;border:1px solid silver;">########## ADD MISSING FLAT PO GRID ##########</div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Flat PO tab ##### -->
        <!-- ##### Finance Charge History tab ##### -->
        <div data-type="tabpage" id="financechargestabpage" class="tabpage" data-tabid="financechargestab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <!-- Location Tax Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Finance Charge History">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD FINANCE CHARGE HISTORY GRID ##########</div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:15%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Pending" data-datafield="Inactive" style="float:left;width:140px;margin-top:5px;"></div>
                </div>
              </div>
              <div class="formcolumn" style="width:46%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice Totals" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Invoiced Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Pending Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                    <button class="button theme calculate" style="float:left;margin-top:15px;margin-left:5px;line-height:12px;width:115px;font-size:10pt;">Calculate</button>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Finance Charge History tab ##### -->
        <!-- ##### Episodic Schedule tab ##### -->
        <div data-type="tabpage" id="episodicscheduletabpage" class="tabpage" data-tabid="episodicscheduletab">
          <div class="formpage">
            <div class="formrow" style="width:100%;">
              <div class="formcolumn" style="width:25%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Episodic Billing">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Start Date" data-datafield="vendor.primarycontactname" style="float:left;width:130px;"></div>
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Stop Date" data-datafield="vendor.primarycontactname" data-enabled="false"
                         style="float:left;width:130px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Bill Weekends" data-datafield="Inactive" style="float:left;width:130px;margin-top:5px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Bill Holidays" data-datafield="Inactive" style="float:left;width:130px;margin-top:5px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:45%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Episode Details" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No. Episodes" data-datafield="vendor.primarycontactname" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Start No." data-datafield="vendor.primarycontactname" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Days Per" data-datafield="vendor.primarycontactname" style="float:left;width:100px;"></div>
                    <button class="button theme calculate" style="float:left;margin-top:15px;margin-left:5px;line-height:12px;width:175px;font-size:10pt;">Create Schedule</button>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:75%;">
              <!-- Episodic Schedule grid section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Episodic Schedule">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD EPISODIC SCHEDULE GRID ##########</div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Episodic Schedule tab ##### -->
        <!-- ##### Hiatus Schedule tab ##### -->
        <div data-type="tabpage" id="hiatusscheduletabpage" class="tabpage" data-tabid="hiatusscheduletab">
          <div class="formpage">
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal" style="width:50%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="vendor.primarycontactname" data-enabled="false" style="float:left;width:350px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="vendor.primarycontactname" data-enabled="false" style="float:left;width:125px;"></div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <!-- Hiatus Schedule grid section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule" style="width:50%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD HIATUS SCHEDULE GRID ##########</div>
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">add Notes label for usage info...several rows of text here...</div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Hiatus Schedule tab ##### -->
        <!-- ##### Order Priority tab ##### -->
        <div data-type="tabpage" id="orderprioritytabpage" class="tabpage" data-tabid="orderprioritytab">
          <div class="formpage">
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Department / Deal" style="width:65%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Company Department" data-datafield="vendor.primarycontactname" style="float:left;width:225px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="vendor.primarycontactname" data-enabled="false" style="float:left;width:350px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal No." data-datafield="vendor.primarycontactname" data-enabled="false" style="float:left;width:125px;"></div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <!-- Hiatus Schedule grid section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Priority" style="width:65%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD ORDER PRIORITY GRID ##########</div>
                </div>
              </div>
            </div>
            <div clsss="formrow">
              <!-- Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:20%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include All" data-datafield="" style="width:250px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Order Priority tab ##### -->
        <!-- ##### Deal Schedule tab ##### -->
        <div data-type="tabpage" id="dealscheduletabpage" class="tabpage" data-tabid="dealscheduletab">
          <div class="formpage">
            <div class="formrow">
              <!-- Deal Schedule grid section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal Schedule" style="width:50%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD DEAL SCHEDULE GRID ##########</div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Deal Schedule tab ##### -->
        <!-- ##### Order Groups tab ##### -->
        <div data-type="tabpage" id="ordergroupstabpage" class="tabpage" data-tabid="ordergroupstab">
          <div class="formpage">
            <div class="formrow">
              <div class="formcolumn" style="width:42%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal / Location">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal" data-datafield="vendor.primarycontactname" data-enabled="false" style="float:left;width:350px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal No." data-datafield="vendor.primarycontactname" data-enabled="false" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Location" data-datafield="vendor.primarycontactname" data-enabled="false" style="float:left;width:225px;"></div>
                </div>
              </div>
              <div class="formcolumn" style="width:18%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="padding-left:1px;">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="All Locations" data-datafield="Inactive" style="float:left;width:150px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Sorting" data-datafield="Inactive" style="float:left;width:150px;"></div>
                </div>
              </div>
              <div class="formcolumn" style="width:15%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Type" style="padding-left:1px;">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Quote" data-datafield="Inactive" style="float:left;width:100px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order" data-datafield="Inactive" style="float:left;width:100px;"></div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:100%;">
              <div class="formcolumn" style="width:17%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Group Number" style="padding-left:1px;">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Orders in Group No." data-datafield="Inactive" style="float:left;width:200px;margin-top:0px;"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname"
                       style="float:left;width:85px;margin-top:-16px;margin-left:-16px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Orders in All Groups" data-datafield="Inactive" style="width:200px;margin-top:0px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show Orders not in a Group" data-datafield="Inactive" style="width:200px;margin-top:0px;"></div>
                </div>
              </div>
              <div class="formcolumn" style="width:16%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Status" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Prospect" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Active" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Reserved" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Ordered" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Closed" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Cancelled" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:16%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Status" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Confirmed" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Active" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Hold" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Complete" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Closed" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Cancelled" data-datafield="Inactive" style="float:left;width:125px;margin-top:0px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:16%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order By" style="padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Order Number" data-datafield="Order Number" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Group Number" data-datafield="Group Number" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Group Sort" data-datafield="Group Sort" style="float:left;width:125px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Location" data-datafield="Location" style="float:left;width:125px;margin-top:0px;"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <!-- Quotes/Orders grid section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quotes / Orders" style="width:75%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD MISSING QUOTES / ORDERS GRID ##########</div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Total" style="width:10%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="width:125px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Order Groups tab ##### -->
        <!-- ##### A/R Aging tab ##### -->
        <div data-type="tabpage" id="aragingtabpage" class="tabpage" data-tabid="aragingtab">
          <div class="formpage">
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="A/R Aging" style="width:1150px;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Processed Only" data-datafield="Location" style="float:left;width:150px;margin-top:0px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Deals setup to Use Customer Credit" data-datafield="Location"
                       style="float:right;width:275px;margin-top:0px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"
                       style="float:left;min-height:150px;min-width:1125px;border:1px solid silver;">########## ADD MISSING A/R AGING GRID ##########</div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:35%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit" style="width:100%;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Limit" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="A/R Total" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Available" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:65%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Invoice Totals" style="width:100%;padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="> 90" data-datafield="vendor.modifiedby" data-enabled="false" style="float:right;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="61 - 90" data-datafield="vendor.modifiedby" data-enabled="false" style="float:right;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="31 - 60" data-datafield="vendor.modifiedby" data-enabled="false" style="float:right;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Current" data-datafield="vendor.modifiedby" data-enabled="false" style="float:right;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total" data-datafield="vendor.modifiedby" data-enabled="false" style="float:right;width:125px;"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:35%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit Including Unbilled Quotes/Orders" style="width:100%;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Invoice Estimate" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Available" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:65%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quotes &amp; Orders" style="width:100%;padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude Future Orders" data-datafield="Location" style="float:left;width:175px;margin-top:0px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Calculate Estimates" data-datafield="Location"
                         style="float:right;width:160px;margin-top:0px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"
                         style="min-height:150px;min-width:700px;border:1px solid silver;">########## ADD MISSING QUOTES &amp; ORDERS GRID ##########</div>
                  </div>
                </div>
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote &amp; Order Totals" style="float:right;width:37%;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Estimate" data-datafield="vendor.modifiedby" data-enabled="false" style="float:right;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Period" data-datafield="vendor.modifiedby" data-enabled="false" style="float:right;width:125px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end A/R Aging tab ##### -->
        <!-- ##### Depleting Deposits / Credit Memos / Overpayments tab ##### -->
        <div data-type="tabpage" id="depletingdepositstabpage" class="tabpage" data-tabid="depletingdepositstab">
          <div class="formpage">
            <div class="formrow" style="width:75%;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depleting Deposits" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"
                       style="float:left;min-height:350px;width:100%;border:1px solid silver;">##### ADD MISSING GRID FOR DEPLETING DEPOSITS / CREDIT MEMOS / OVERPAYMENTS #####</div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:46%;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Applied" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Refunded" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Depleting Deposits / Credit Memos / Overpayments tab ##### -->
        <!-- ##### Security Deposit tab ##### -->
        <div data-type="tabpage" id="securitydeposittabpage" class="tabpage" data-tabid="securitydeposittab">
          <div class="formpage">
            <div class="formrow" style="width:75%;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security Deposits" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"
                       style="float:left;min-height:350px;width:100%;border:1px solid silver;">##### ADD MISSING GRID FOR SECURITY DEPOSITS #####</div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:25%;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Total Current Security Deposits" style="width:100%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:125px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Security Deposit tab ##### -->
        <!-- ##### Credit tab ##### -->
        <div data-type="tabpage" id="credittabpage" class="tabpage" data-tabid="credittab">
          <div class="formpage">
            <div class="formrow">
              <div class="formcolumn" style="width:575px;">
                <!-- Credit section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CreditStatusId" data-displayfield="CreditStatus" data-validationname="CreditStatusValidation" data-required="true" style="float:left;width:175px;"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Through Date" data-datafield="CreditStatusThroughDate" style="float:left;width:130px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Amount" data-datafield="CreditLimit" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Available" data-datafield="CreditAvailable" data-enabled="false"
                         style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Balance" data-datafield="CreditBalance" data-enabled="false"
                         style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unlimited" data-datafield="CreditUnlimited" style="float:left;width:125px;padding-left:25px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:625px;padding-left:1px;">
                <div class="formrow">
                  <!-- Responsible Party section -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Responsible Party">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="CreditResponsibleParty" style="float:left;width:350px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="On File" data-datafield="CreditResponsiblePartyOnFile" style="float:left;width:125px;padding-left:25px;"></div>
                    </div>
                  </div>
                  <!-- Trade References section -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Trade References">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="TradeReferencesVerifiedBy" style="float:left;width:350px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="TradeReferencesVerifiedOn" style="float:left;width:125px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Verified" data-datafield="TradeReferencesVerified"
                           style="float:left;width:125px;padding-left:25px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:625px;">
                <!-- Credit Card section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit Card">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="CreditCardName" style="float:left;width:350px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Authorization Form On File" data-datafield="CreditCardAuthorizationOnFile"
                         style="float:left;width:225px;margin-left:25px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="CreditCardTypeId" data-displayfield="CreditCardType" data-validationname="PaymentTypeValidation" style="float:left;width:250px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="CreditCardNo" style="float:left;width:175px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Exp. Month" data-datafield="CreditCardExpirationMonth" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Exp. Year" data-datafield="CreditCardExpirationYear" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Security Code" data-datafield="CreditCardCode" style="float:left;width:100px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Limit" data-datafield="CreditCardLimit" style="float:left;width:125px;"></div>
                  </div>
                </div>
              </div>
              <!--<div class="formcolumn" style="width:400px;padding-left:1px;">
            <div class="formrow">
              Depleting Deposit section
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depleting Deposit Threshold">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.address" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Percentage" data-datafield="vendor.address" style="float:left;width:125px;"></div>
                </div>
              </div>
              Depleting Deposit Total Current Available section
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depleting Deposit Total Current Available">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deposit" data-datafield="vendor.city" data-enabled="false"
                       style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Invoice" data-datafield="vendor.state" data-enabled="false"
                       style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="vendor.zippostal" data-enabled="false"
                       style="float:left;width:125px;"></div>
                </div>
              </div>
            </div>
          </div>-->
            </div>
          </div>
        </div>
        <!-- ##### end Credit tab ##### -->
        <!-- ##### Insurance tab ##### -->
        <div data-type="tabpage" id="insurancetabpage" class="tabpage" data-tabid="insurancetab">
          <div class="formpage">
            <div class="formrow">
              <!-- Insurance section -->
              <div class="formcolumn" style="width:505px;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="InsuranceCertificationValidThrough" style="float:left;width:130px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Certification on File" data-datafield="" style="float:left;width:175px;margin-left:25px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield officelocation" data-caption="Liability" data-datafield="InsuranceCoverageLiability" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield vendorclass" data-caption="Deductible" data-datafield="InsuranceCoverageLiabilityDeductible" style="float:left;width:125px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield vendorclass" data-caption="Property Value" data-datafield="InsuranceCoveragePropertyValue" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield vendorclass" data-caption="Deductible" data-datafield="InsuranceCoveragePropertyValueDeductible" style="float:left;width:125px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:250px;padding-left:1px;">
                <!-- Vehicle Insurance section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vehicle Insurance">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Completed" data-datafield="VehicleInsuranceCertficationOnFile" style="width:125px;"></div>
                  </div>
                </div>
                <!-- Security Deposit section -->
                <!--<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security Deposit">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total Amount" data-datafield="customer.webaddress" data-enabled="false" style="width:125px;"></div>
              </div>
            </div>-->
              </div>
            </div>
            <!-- Company section -->
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Company" style="float:left;padding-right:1px; width:25%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Name" data-datafield="InsuranceCompanyId" data-displayfield="InsuranceCompany" data-validationname="VendorValidation" data-formbeforevalidate="beforeValidateInsuranceVendor" style="float:left;width:50%"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="InsuranceAgent" style="float:left;width:50%"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="InsuranceCompanyAddress1" data-enabled="false" style="float:left;width:50%"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="InsuranceCompanyAddress2" data-enabled="false" style="float:left;width:50%"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InsuranceCompanyCity" data-enabled="false" style="float:left;width:50%"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="InsuranceCompanyState" data-enabled="false" style="float:left;width:30%"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InsuranceCompanyZipCode" data-enabled="false" style="float:left;width:20%"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="InsuranceCompanyCountryId" data-displayfield="InsuranceCompanyCountry" data-validationname="CountryValidation" data-enabled="false" style="width:175px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InsuranceCompanyPhone" data-enabled="false" style="float:left;width:50%"></div>
                  <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="InsuranceCompanyFax" data-enabled="false" style="float:left;width:50%"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Insurance tab ##### -->
        <!-- ##### Tax tab ##### -->
        <div data-type="tabpage" id="taxtabpage" class="tabpage" data-tabid="taxtab">
          <div class="formpage">
            <div class="formrow">
              <div class="formcolumn" style="width:425px;">
                <!-- Tax section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax">
                  <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Taxable" style="float:left;width:250px;">
                    <div data-value="true" data-caption="Taxable"></div>
                    <div data-value="false" data-caption="Non-Taxable"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:325px;padding-left:1px;">
                <!-- Federal section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Federal">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State of Incorporation" data-datafield="TaxStateOfIncorporationId" data-displayfield="TaxStateOfIncorporation" data-validationname="StateValidation" style="float:left;width:150px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal Tax No." data-datafield="TaxFederalNo" style="float:left;width:150px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:450px;padding-left:1px;">
                <div class="formrow" style="width:100%;">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location Tax Options">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Tax Option"></div>
                    </div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:325px;padding-left:1px;">
                <div class="formrow" style="width:325px;">
                  <!-- Tax Rates section -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield TaxOption" data-caption="Tax Option" data-enabled="false" data-datafield="" data-displayfield="Customer" data-validationname="CustomerValidation" style="width:200px;"></div>
                      <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield RentalTaxRate1" data-caption="Rental %" data-datafield="" data-displayfield="" data-enabled="false" style="float:left;width:100px;"></div>
                      <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield SalesTaxRate1" data-caption="Sales %" data-datafield="" data-displayfield="" data-enabled="false" style="float:left;width:100px;"></div>
                      <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield LaborTaxRate1" data-caption="Labor %" data-datafield="" data-displayfield="" data-enabled="false" style="float:left;width:100px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:300px;">
                <!-- Non-Taxable section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Non-Taxable">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Certificate No." data-datafield="NonTaxableCertificateNo" style="float:left;width:250px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Year" data-datafield="NonTaxableYear" style="float:left;width:75px;"></div>
                    <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="NonTaxableCertificateValidThrough" style="float:left;width:130px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Certificate on File" data-datafield="NonTaxableCertificateOnFile" style="width:175px;"></div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:450px;padding-left:1px;">
                <!-- Resale section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Resale">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="CompanyResaleGrid" data-securitycaption="Customer Resale"></div>
                  </div>
                </div>
              </div>
            </div>


          </div>
        </div>
        <!-- ##### end Tax tab ##### -->
        <!-- ##### Options tab ##### -->
        <div data-type="tabpage" id="optionstabpage" class="tabpage" data-tabid="optionstab">
          <div class="formpage">
            <div class="formrow">
              <div class="formcolumn" style="width:775px;">
                <!-- Activity Type -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote / Order Activity">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude Quote / Order Activity" data-datafield="DisableQuoteOrderActivity" style="float:left;width:350px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow quote-order" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="DisableRental" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="DisableSales" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="DisableFacilities" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="DisableTransportation" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Crew" data-datafield="DisableLabor" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Misc." data-datafield="DisableMisc" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Used Sale" data-datafield="DisableRentalSale" data-enabled="false" style="float:left;width:125px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow quote-order" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rental" data-datafield="DisableSubRental" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sales" data-datafield="DisableSubSale" data-enabled="false" style="float:left;width:125px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Crew" data-datafield="DisableSubLabor" data-enabled="false" style="float:left;width:125px;margin-left:250px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Misc" data-datafield="DisableSubMisc" data-enabled="false" style="float:left;width:125px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Options tab ##### -->
        <!-- ##### Split Rental tab ##### -->
        <div data-type="tabpage" id="splitrentaltabpage" class="tabpage" data-tabid="splitrentaltab">
          <div class="formpage">
            <div class="formrow" style="width:375px;">
              <!-- Activity Type -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Split Rental Equipment">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Split Rental Customer" data-datafield="manufacturer"
                       style="float:left;width:175px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Tax Customer" data-datafield="manufacturer"
                       style="float:left;width:175px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Owned % of Rental" data-datafield="vendor.salestaxrate" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub-Rented % of Profit" data-datafield="vendor.labortaxrate" style="float:left;width:150px;"></div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:375px;">
              <!-- Activity Type -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rebate Rental Equipment">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rebate Rental Customer" data-datafield="manufacturer" style="float:left;width:175px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Owned % of Rental" data-datafield="vendor.salestaxrate" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub-Rented % of Profit" data-datafield="vendor.labortaxrate" style="float:left;width:150px;"></div>
                </div>
              </div>
            </div>
            <div class="formrow" style="width:375px;">
              <!-- Split/Rebate Rental Customer Logo -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Split/Rebate Rental Customer Logo">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="File Name" data-datafield="customer.splitrebatelogo"
                       style="float:left;width:250px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Width" data-datafield="customer.splitrebatelogowidth"
                       style="float:left;width:100px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Height" data-datafield="customer.splitrebatelogoheight"
                       style="float:left;width:100px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Split Rental tab ##### -->
        <!-- ##### Shipping tab ##### -->
        <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
          <div class="formpage">
            <div class="formrow">
              <div class="formcolumn" style="width:290px;">
                <!-- Shipping Address section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address" style="width:290px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Default Address" data-datafield="ShippingAddressType" style="float:left;width:265px;">
                      <div data-value="CUSTOMER" data-caption="Use Customer"></div>
                      <div data-value="OTHER" data-caption="Use Other"></div>
                    </div>
                  </div>
                </div>
                <!-- Default Deliver section -->
                <!--<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Outgoing Delivery" style="width:290px;">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Outgoing Delivery" data-datafield="vehiclerepair.lotflg" style="width:265px;">
                  <div data-value="OutgoingDeliver" data-caption="Deliver"></div>
                  <div data-value="OutgoingShip" data-caption="Ship"></div>
                  <div data-value="OutgoingPickUp" data-caption="Customer Pick Up"></div>
                </div>
              </div>
            </div>-->
              </div>
              <div class="formcolumn">
                <!-- Default Shipping Address section -->
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Shipping Address" style="width:550px;padding-left:1px;">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Attention" data-datafield="ShipAttention" data-enabled="false" style="float:left;width:275px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="float:left;width:275px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="float:left;width:250px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="float:left;width:275px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="State" data-datafield="ShipState" data-enabled="false" style="float:left;width:150px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="float:left;width:100px;"></div>
                  </div>
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield shippingaddress" data-caption="Country" data-datafield="CountryId" data-enabled="false" data-displayfield="Country" data-validationname="CountryValidation" style="width:175px;"></div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:290px;">
                <!-- Default Delivery Return section -->
                <!--<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Incoming Delivery" style="width:290px;">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Incoming Delivery" data-datafield="vehiclerepair.lotflg" style="width:265px;">
                  <div data-value="IncomingDeliver" data-caption="Customer Deliver"></div>
                  <div data-value="IncomingShip" data-caption="Ship"></div>
                  <div data-value="IncomingPickUp" data-caption="Pick Up"></div>
                </div>
              </div>
            </div>-->
              </div>
              <div class="formcolumn">
                <!-- Location Tax Options section -->
                <!--<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Carrier" style="width:550px;padding-left:1px;">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwGrid" data-grid="CUstomerShippingCarrier" data-securitycaption="Location Tax Options">##### ADD MISSING CARRIER GRID #####</div>
              </div>
            </div>-->
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Shipping tab ##### -->
        <!-- ##### Additional Addresses tab ##### -->
        <div data-type="tabpage" id="additionaladdressestabpage" class="tabpage" data-tabid="additionaladdressestab">
          <div class="formpage">
            <div class="formrow">
              <!-- Additional Addresses grid section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Additional Addresses" style="float:left;width:750px;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="VendorContacts" data-securitycaption="Vendor Contacts">########## ADD MISSING ADDITIONAL ADDRESSES GRID ##########</div>
                </div>
              </div>
              <!-- Additional Addresssection -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Additional Address" style="float:left;padding-left:1px;width:550px;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="vendor.remittoaddress1" style="float:left;width:275px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 1" data-datafield="vendor.remittoaddress1" style="float:left;width:275px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 2" data-datafield="vendor.remittoaddress2" style="float:left;width:250px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="vendor.remittoaddress1" style="float:left;width:275px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="vendor.remittoaddress2" style="float:left;width:250px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="vendor.remittocity" style="float:left;width:275px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="vendor.remittostate" style="float:left;width:150px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="vendor.remittozippostal" style="float:left;width:100px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="vendor.remittocountry" style="float:left;width:175px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Payee No" data-datafield="vendor.payeeno" style="float:left;width:175px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Additional Addresses tab ##### -->
        <!-- ##### Contacts tab ##### -->
        <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
          <div class="formpage">
            <div class="formcolumn" style="width:100%;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Vendor Contacts"></div>
                </div>
              </div>
            </div>

            <!--<div class="formcolumn" style="width:375px;padding-left:1px;">
          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Primary Contact">
            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="vendor.primarycontactname" data-enabled="false"
                   style="float:left;width:350px;"></div>
            </div>
            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Job Title" data-datafield="vendor.primarycontactjobtitle" data-enabled="false"
                   style="float:left;width:175px;"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Contact Title" data-datafield="vendor.primarycontacttitle" data-enabled="false"
                   style="float:left;width:175px;"></div>
            </div>
            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email" data-datafield="vendor.primarycontactemail" data-enabled="false"
                   style="float:left;width:350px;"></div>
            </div>
            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Office" data-datafield="vendor.primarycontactoffice" data-enabled="false"
                   style="float:left;width:125px;"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Extension" data-datafield="vendor.primarycontactextension" data-enabled="false"
                   style="float:left;width:75px;"></div>
            </div>
            <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Cellular" data-datafield="vendor.primarycontactcellular" data-enabled="false"
                   style="float:left;width:125px;"></div>
              <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Direct" data-datafield="vendor.primarycontactdirect" data-enabled="false"
                   style="float:left;width:125px;"></div>
            </div>
          </div>
        </div>-->
          </div>
        </div>
        <!-- ##### end Contacts tab ##### -->
        <!-- ##### Rebate Rental tab ##### -->
        <div data-type="tabpage" id="rebaterentaltabpage" class="tabpage" data-tabid="rebaterentaltab">
          <div class="formpage">
            <div class="formrow" style="width:375px;">
              <!-- Activity Type -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Rebate Rental Equipment">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rebate Rental Project" data-datafield="manufacturer" style="float:left;width:175px;"></div>
                  <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Rebate Customer" data-datafield="vendor.salestaxrate" style="float:left;width:350px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Owned % of Rental" data-datafield="vendor.salestaxrate" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub-Rented % of Profit" data-datafield="vendor.labortaxrate" style="float:left;width:150px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Rebate Rental tab ##### -->
        <!-- ##### Notes tab ##### -->
        <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
          <div class="formpage">
            <div class="formrow" style="width:750px;">
              <!-- Notes section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="CustomerNoteGrid" data-securitycaption="Customer Note" style="min-width:240px;max-width:800px;"></div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <!--<div class="formcolumn" style="width:auto;">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Schedule Color" style="width:150px;">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="color" class="fwcontrol fwformfield" data-caption="Color" data-datafield="vendor.openeddate" style="width:100px;"></div>
              </div>
            </div>
          </div>-->
              <div class="formcolumn">
                <!-- Updated section -->
                <!--<div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Updated" style="width:475px;padding-left:1px;">
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Opened" data-datafield="vendor.openeddate" data-enabled="false" style="float:left;width:100px;"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="vendor.openedby" data-enabled="false" style="float:left;width:350px;"></div>
              </div>
              <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Modified Last" data-datafield="vendor.modifieddate" data-enabled="false" style="float:left;width:100px;"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:350px;"></div>
              </div>
            </div>-->
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Notes tab ##### -->
        <!-- ##### Image tab ##### -->
        <div data-type="tabpage" id="imagetabpage" class="tabpage" data-tabid="imagetab">
          <div class="flexpage">
            <div class="flexrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Photo">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwAppImage" data-type="" class="fwcontrol fwappimage contactphoto" data-caption="Photo" data-uniqueid1field="CustomerId" data-description="" data-rectype="F"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Image tab ##### -->
        <!-- ##### RentalWorks.Net tab ##### -->
        <div data-type="tabpage" id="rentalworksnettabpage" class="tabpage" data-tabid="rentalworksnettab">
          <div class="formpage">
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Web Reports / Quote Request" style="width:35%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Web Reports" data-datafield="manufacturer" style="float:left;width:175px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Web Quote Request" data-datafield="manufacturer" style="float:left;width:175px;"></div>
                </div>
              </div>
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="QuikRequest Approver" style="width:75%;">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes">########## ADD MISSING QUIKREQUEST APPROVER GRID ##########</div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end RentalWorks.Net tab ##### -->
        <!-- ##### Web Content tab ##### -->
        <div data-type="tabpage" id="webcontenttabpage" class="tabpage" data-tabid="webcontenttab">
          <div class="formpage">
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Web Content">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Note" data-datafield="Vendor" style="min-height:500px;"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Web tab ##### -->
        <!-- ##### Events tab ##### -->
        <div data-type="tabpage" id="eventtabpage" class="tabpage" data-tabid="eventtab">
          <div class="formpage">
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Events">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="ProjectEvents" data-securitycaption="Customer Notes" style="float:left;min-height:100px;min-width:100%;border:1px solid silver;">##### ADD MISSING EVENT GRID #####</div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="formcolumn" style="width:50%;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Event Personnel">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="ProjectEvents" data-securitycaption="Customer Notes" style="min-height:150px;border:1px solid silver;">##### ADD MISSING EVENT PERSONNEL GRID #####</div>
                  </div>
                </div>
              </div>
              <div class="formcolumn" style="width:50%;padding-left:1px;">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Event Schedule">
                  <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                    <div data-control="FwGrid" data-grid="ProjectEvents" data-securitycaption="Customer Notes" style="min-height:150px;border:1px solid silver;">##### ADD MISSING EVENT SCHEDULE GRID #####</div>
                  </div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Event Report">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div class="formcolumn" style="width:25%;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Fabrication Notes" data-datafield="Vendor"></div>
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Floral Notes" data-datafield="Vendor"></div>
                    </div>
                  </div>
                  <div class="formcolumn" style="width:25%;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Install Notes" data-datafield="Vendor"></div>
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Strike Notes" data-datafield="Vendor"></div>
                    </div>
                  </div>
                  <div class="formcolumn" style="width:25%;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Entertainment Notes" data-datafield="Vendor"></div>
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Production Services Notes" data-datafield="Vendor"></div>
                    </div>
                  </div>
                  <div class="formcolumn" style="width:25%;">
                    <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Client Notes" data-datafield="Vendor"></div>
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Vendor Notes" data-datafield="Vendor"></div>
                    </div>
                  </div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Misc. Notes" data-datafield="Vendor"></div>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Events tab ##### -->
        <!-- ##### Summary tab ##### -->
        <div data-type="tabpage" id="summarytabpage" class="tabpage" data-tabid="summarytab">
          <div class="formpage">
            <div class="formcolumn" style="width:15%;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Include Statuses">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Confirmed" data-datafield="manufacturer" style="width:125px;margin-top:0px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Hold" data-datafield="manufacturer" style="width:125px;margin-top:0px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Active" data-datafield="manufacturer" style="width:125px;margin-top:0px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Complete" data-datafield="manufacturer" style="width:125px;margin-top:0px;"></div>
                  <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Closed" data-datafield="manufacturer" style="width:125px;margin-top:0px;"></div>
                </div>
              </div>
            </div>
            <div class="formcolumn" style="width:30%;padding-left:1px;">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Project Order's Value / Replacement Cost">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total Value" data-datafield="Vendor" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Total Replacement" data-datafield="Vendor" style="float:left;width:125px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Owned Value" data-datafield="Vendor" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Owned Replacement" data-datafield="Vendor" style="float:left;width:125px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub Value" data-datafield="Vendor" style="float:left;width:125px;"></div>
                  <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub Replacement" data-datafield="Vendor" style="float:left;width:125px;"></div>
                </div>
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <button class="button theme calculate" style="margin-top:5px;margin-left:65px;line-height:12px;width:115px;font-size:10pt;">Calculate</button>
                </div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Summary tab ##### -->
        <!-- ##### Documents tab ##### -->
        <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
          <div class="formpage">
            <div classs="formrow">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Documents">
                <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                  <div data-control="FwGrid" data-grid="ProjectDocuments" data-securitycaption="Customer Notes" style="min-height:150px;border:1px solid silver;">##### ADD MISSING DOCUMENTS GRID #####</div>
                </div>
              </div>
            </div>
            <div class="formrow">
              <!-- Options section -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:25%;">
                <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
              </div>
            </div>
          </div>
        </div>
        <!-- ##### end Documents tab ##### -->
      </div>
    </div>
`;
  }


}
var TiwCustomerController = new TiwCustomer();
