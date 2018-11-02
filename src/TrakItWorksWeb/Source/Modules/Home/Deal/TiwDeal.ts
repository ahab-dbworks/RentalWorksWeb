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
        <div data-name="Deal" data-control="FwBrowse" data-type="Browse" id="DealBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="DealController" data-hasinactive="true">
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
        <div id="dealform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Deal" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="DealController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="DealId"></div>
          <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="projecttab" class="tab" data-tabpageid="projecttabpage" data-caption="Deal"></div>
              <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="credittab" class="tab" data-tabpageid="credittabpage" data-caption="Credit"></div>
              <div data-type="tab" id="insurancetab" class="tab" data-tabpageid="insurancetabpage" data-caption="Insurance"></div>
              <div data-type="tab" id="taxtab" class="tab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
              <div data-type="tab" id="optionstab" class="tab" data-tabpageid="optionstabpage" data-caption="Options"></div>
              <div data-type="tab" id="quotetab" class="tab submodule" data-tabpageid="quotetabpage" data-caption="Quote"></div>
              <div data-type="tab" id="ordertab" class="tab submodule" data-tabpageid="ordertabpage" data-caption="Order"></div>
              <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <div data-type="tabpage" id="projecttabpage" class="tabpage" data-tabid="projecttab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Deal section -->
                    <div class="flexcolumn" style="flex:1 1 750px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Deal" style="flex:1 1 550px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="DealNumber" data-required="false" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-required="true" style="flex:1 1 325px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" data-required="true" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DealTypeId" data-displayfield="DealType" data-validationname="DealTypeValidation" data-required="true" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Classification" data-datafield="DealClassificationId" data-displayfield="DealClassification" data-validationname="DealClassificationValidation" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Production Type" data-datafield="ProductionTypeId" data-displayfield="ProductionType" data-validationname="ProductionTypeValidation" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 175px;">
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
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield deal_address" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:0 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Contact Numbers section -->
                    <div class="flexcolumn" style="flex:1 1 175px;">
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
                    <div class="flexcolumn" style="flex:1 1 350px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer Service Representative">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="CSR" data-datafield="CsrId" data-displayfield="Csr" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Default Agent / Project Manager section -->
                    <div class="flexcolumn" style="flex:1 1 700px;">
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
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
                <div class="formpage">
                  <div class="flexrow">
                    <!-- Default Billing section -->
                    <div class="flexcolumn" style="flex:1 1 575px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Billing">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" data-validationname="BillingCycleValidation" data-required="true" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" data-validationname="PaymentTypeValidation" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order Rate" data-datafield="DefaultRate" data-displayfield="DefaultRate" data-validationname="RateTypeValidation" data-validationpeek="false" style="flex:1 1 200px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Discounts & D/W section -->
                    <div class="flexcolumn" style="flex:1 1 350px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Discounts &amp; D/W">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield billing_use_customer" data-caption="Use Customer" data-datafield="UseCustomerDiscount" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield billing_use_discount_template" data-caption="Use Discount Template" data-datafield="UseDiscountTemplate" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield billing_template" data-caption="Template" data-datafield="DiscountTemplateId" data-displayfield="DiscountTemplate" data-validationname="DiscountTemplateValidation" data-enabled="false" style="flex:1 1 200px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- Billing Address section -->
                    <div class="flexcolumn" style="flex:1 1 575px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield billing-type-radio" data-caption="" data-datafield="BillToAddressType" style="flex:1 1 250px;">
                            <div data-value="CUSTOMER" data-caption="Use Customer" style="margin-top:-15px;"></div>
                            <div data-value="DEAL" data-caption="Use Deal"></div>
                            <div data-value="OTHER" data-caption="Use Other"></div>
                          </div>
                        </div>
                        <div class="flexrow" style="margin-top:5px;">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 1" data-datafield="BillToAttention1" data-enabled="false" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 2" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="BillToState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-enabled="false" data-displayfield="BillToCountry" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 350px;">
                      <!-- PO section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Required" data-datafield="PoRequired" style="flex:1 1 100px;"></div>
                          <!--<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Type" data-datafield="PoType" style="float:left;width:100px;"></div>-->
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield billing_potype" data-caption="Type" data-datafield="PoType" style="flex:1 1 150px;">
                            <div data-value="H" data-caption="Hardcopy"></div>
                            <div data-value="V" data-caption="Verbal"></div>
                          </div>
                        </div>
                      </div>
                      <!-- Commission section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Commission">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Outside Sales Representative" data-datafield="OutsideSalesRepresentativeId" data-displayfield="OutsideSalesRepresentative" data-validationname="ContactValidation" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Invoice Percentage" data-datafield="CommissionRate" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Sub-Rentals" data-datafield="CommissionIncludesVendorItems" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- Billing Options section -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 725px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Options">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Assess Finance Charge on Overdue Amount" data-datafield="AssessFinanceCharge" style="flex:0 1 350px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Order Billing Schedule Override" data-datafield="AllowBillingScheduleOverride" style="flex:0 1 350px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="credittabpage" class="tabpage" data-tabid="credittab">
                <div class="formpage">
                  <!-- Credit section -->
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit" style="flex:0 1 750px;">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 225px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield credit_use_customer" data-caption="Use Customer" data-datafield="UseCustomerCredit" style="flex:0 1 200px;padding-left:10px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unlimited" data-datafield="UnlimitedCredit" style="flex:0 1 200px;padding-left:10px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Application on File" data-datafield="CreditApplicationOnFile" style="flex:0 1 200px;padding-left:10px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:1 1 375px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CreditStatusId" data-displayfield="CreditStatus" data-validationname="CreditStatusValidation" data-readonly="true" style="flex:1 1 175px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Through Date" data-datafield="CreditStatusThrough" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Customer Amount" data-datafield="CustomerCreditLimit" data-enabled="false" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Customer Available" data-datafield="CustomerCreditAvailable" data-enabled="false" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Customer Balance" data-datafield="CustomerCreditBalance" data-enabled="false" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Deal Amount" data-datafield="CreditLimit" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deal Available" data-datafield="CreditAvailable" data-enabled="false" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deal Balance" data-datafield="CreditBalance" data-enabled="false" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- Depleting Deposit section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depleting Deposit Threshold" style="flex:0 1 750px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="DepletingDepositThresholdAmount" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Percentage" data-datafield="DepletingDepositThresholdPercent" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deposit" data-datafield="DepletingDepositTotal" data-enabled="false" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Applied" data-datafield="DepletingDepositApplied" data-enabled="false" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="DepletingDepositRemaining" data-enabled="false" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- Trade References section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Trade References" style="flex:0 1 750px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="TradeReferencesVerifiedBy" style="flex:1 1 300px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="TradeReferencesVerifiedOn" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Verified" data-datafield="TradeReferencesVerified" style="flex:0 1 125px;margin-top:10px;padding-left:25px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!-- Responsible Party section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Responsible Party" style="flex:0 1 750px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="CreditResponsibleParty" style="flex:1 1 300px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="On File" data-datafield="CreditResponsiblePartyOnFile" style="flex:0 1 125px;margin-top:10px;padding-left:25px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="insurancetabpage" class="tabpage" data-tabid="insurancetab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance" style="flex:0 1 650px;">
                      <div class="flexrow">
                        <!-- Insurance section -->
                        <div class="flexcolumn" style="flex:0 1 200px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield insurance_use_customer" data-caption="Use Customer" data-datafield="UseCustomerInsurance" style="flex:0 1 175px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Certification on File" data-datafield="InsuranceCertification" style="flex:0 1 175px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:0 1 400px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="InsuranceCertificationValidThrough" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Liability" data-datafield="InsuranceCoverageLiability" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Deductible" data-datafield="InsuranceCoverageLiabilityDeductible" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Property Value" data-datafield="InsuranceCoverageProperty" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Deductible" data-datafield="InsuranceCoveragePropertyDeductible" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 325px;">
                      <!-- Security Deposit section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security Deposit">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Total Amount" data-datafield="SecurityDepositAmount" data-enabled="false" style="flex:0 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 325px;">
                      <!-- Vehicle Insurance section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vehicle Insurance">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Completed" data-datafield="VehicleInsuranceCertification" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance Company" style="flex:0 1 650px;">
                      <!-- Address section -->
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield insurance_name" data-caption="Name" data-datafield="InsuranceCompanyId" data-displayfield="InsuranceCompany" data-validationname="VendorValidation" style="flex:1 1 325px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="InsuranceCompanyAgent" style="flex:1 1 300px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="InsuranceCompanyAddress1" data-enabled="false" style="flex:1 1 325px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="InsuranceCompanyAddress2" data-enabled="false" style="flex:1 1 300px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InsuranceCompanyCity" data-enabled="false" style="flex:1 1 325px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="InsuranceCompanyState" data-enabled="false" style="flex:1 1 200px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InsuranceCompanyZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="InsuranceCompanyCountryId" data-displayfield="InsuranceCompanyCountry" data-validationname="Country" data-enabled="false" style="flex:1 1 175px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InsuranceCompanyPhone" data-enabled="false" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="InsuranceCompanyFax" data-enabled="false" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="taxtabpage" class="tabpage" data-tabid="taxtab">
                <div class="formpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 250px;">
                      <!-- Tax section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield tax_use_customer" data-caption="Use Customer" data-datafield="UseCustomerTax" style="flex:1 1 125px;margin-top:0px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Taxable" data-datafield="Taxable" style="flex:1 1 125px;margin-top:0px;"></div>
                        <!--<div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="Status" data-datafield="MISSINGPROP" style="float:left;width:200px;">
                          <div data-value="TAXABLE" data-caption="Taxable"></div>
                          <div data-value="NONTAXABLE" data-caption="Non-Taxable"></div>
                        </div>-->
                      </div>
                      <!-- Federal section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Federal">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State of Incorporation" data-datafield="TaxStateOfIncorporationId" data-displayfield="TaxStateOfIncorporation" data-validationname="StateValidation" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal Tax No." data-datafield="TaxFederalNo" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                      <!-- Non-Taxable section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Non-Taxable">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Certificate No." data-datafield="NonTaxableCertificateNo" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Year" data-datafield="NonTaxableYear" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="NonTaxableCertificateValidThrough" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Certificate on File" data-datafield="NonTaxableCertificateOnFile" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <!-- Location Tax Options section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location Tax Options">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Tax Option"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <!-- Tax Rates section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield TaxOption" data-datafield="" data-displayfield="" data-caption="Tax Option" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield RentalTaxRate1" data-datafield="" data-displayfield="" data-caption="Rental %" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield SalesTaxRate1" data-datafield="" data-displayfield="" data-caption="Sales %" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield LaborTaxRate1" data-datafield="" data-displayfield="" data-caption="Labor %" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                      <!-- Resale section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Resale">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyResaleGrid" data-securitycaption="Deal Resale"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="optionstabpage" class="tabpage" data-tabid="optionstab">
                <div class="formpage">
                  <div class="flexrow">
                    <!-- Activity Type -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote / Order Activity" style="flex:0 1 925px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield exlude_quote" data-caption="Exclude Quote / Order Activity" data-datafield="DisableQuoteOrderActivity" style="flex:1 1 350px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="DisableRental" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="DisableSales" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="DisableFacilities" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="DisableTransportation" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Crew" data-datafield="DisableLabor" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Misc." data-datafield="DisableMisc" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Used Sale" data-datafield="DisableRentalSale" data-enabled="false" style="flex:0 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rental" data-datafield="DisableSubRental" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sales" data-datafield="DisableSubSale" data-enabled="false" style="flex:0 1 125px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Crew" data-datafield="DisableSubLabor" data-enabled="false" style="flex:0 1 125px;margin-left:250px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Misc" data-datafield="DisableSubMisc" data-enabled="false" style="flex:0 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="quotetabpage" class="tabpage submodule quote" data-tabid="quotetab"></div>
              <div data-type="tabpage" id="ordertabpage" class="tabpage submodule order" data-tabid="ordertab"></div>
              <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
                <div class="formpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 315px;">
                      <!-- Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield shipping_address_type_radio" data-caption="" data-datafield="ShippingAddressType" style="flex:1 1 265px;">
                            <div data-value="CUSTOMER" data-caption="Use Customer" style="margin-top:-15px;"></div>
                            <div data-value="DEAL" data-caption="Use Deal"></div>
                            <div data-value="OTHER" data-caption="Use Other"></div>
                          </div>
                        </div>
                      </div>
                      <!-- Default Deliver section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Outgoing Delivery" style="margin-top:13px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultOutgoingDeliveryType" style="flex:1 1 265px;">
                            <div data-value="DELIVER" data-caption="Deliver to Customer" style="margin-top:-15px;"></div>
                            <div data-value="SHIP" data-caption="Ship to Customer"></div>
                            <div data-value="PICK UP" data-caption="Customer Pick Up"></div>
                          </div>
                        </div>
                      </div>
                      <!-- Default Delivery Return section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Incoming Delivery" style="margin-top:12px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultIncomingDeliveryType" style="flex:1 1 265px;">
                            <div data-value="DELIVER" data-caption="Customer Deliver" style="margin-top:-15px;"></div>
                            <div data-value="SHIP" data-caption="Customer Ship"></div>
                            <div data-value="PICK UP" data-caption="Pick Up from Customer"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 575px;">
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
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Carrier" style="flex:0 1 635px;">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="DealShipperGrid" data-securitycaption="Deal Shipper"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="formpage">
                  <div class="flexcolumn">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Deal Contacts"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="formpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="DealNoteGrid" data-securitycaption="Deal Notes"></div>
                      </div>
                    </div>
                  </div>
                  <!--<div class="formrow">
                    <div class="formcolumn" style="width:auto;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Schedule Color" style="width:200px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="color" class="fwcontrol fwformfield" data-caption="Color" data-datafield="vendor.openeddate" style="width:100px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="formcolumn">
                      Updated section
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Updated" style="width:500px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Opened" data-datafield="vendor.openeddate" data-enabled="false" style="float:left;width:100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="vendor.openedby" data-enabled="false" style="float:left;width:350px;"></div>
                        </div>
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Modified Last" data-datafield="vendor.modifieddate" data-enabled="false" style="float:left;width:100px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:350px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>-->
                </div>
              </div>
            </div>
          </div>
        </div>`;
  }
    //##################################################
}

var TiwDealController = new TiwDeal();