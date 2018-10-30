class TiwContact extends Contact {

  constructor() {
    super();
    this.id = '9DC167B7-3313-4783-8A97-03C55B6AD5F2';
  }

  //browseModel: any = {};

  //getBrowseTemplate(): void {
  //    //let template = super.getBrowseTemplate();
  //    //return template;
  //}

  getBrowseTemplate(): string {
    return `
    <div data-name="Contact" data-control="FwBrowse" data-type="Browse" class="fwcontrol fwbrowse" data-controller="ContactController" data-hasinactive="true">
      <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="true" data-datafield="ContactId" data-browsedatatype="key" ></div>
          <div class="field" data-isuniqueid="true" data-datafield="CompanyContactId" data-browsedatatype="key" ></div>
      </div>
      <div class="column" data-width="0" data-visible="false">
          <div class="field" data-datafield="Inactive" data-browsedatatype="text"  data-visible="false"></div>
      </div>
      <div class="column" data-width="0" data-visible="false">
          <div class="field" data-isuniqueid="false" data-datafield="ContactRecordTypeColor" data-browsedatatype="rowbackgroundcolor" data-formdatafield="" data-formdatatype="rowbackgroundcolor"></div>
      </div>
      <div class="column" data-width="150px">
          <div class="field" data-caption="Last Name" data-isuniqueid="false" data-datafield="LastName" data-browsedatatype="text" data-sort="asc"></div>
      </div>
      <div class="column" data-width="150px">
          <div class="field" data-caption="First Name" data-isuniqueid="false" data-datafield="FirstName" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px">
          <div class="field" data-caption="Middle Initial" data-isuniqueid="false" data-datafield="MiddleInitial" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px" data-visible="true">
          <div class="field" data-caption="Mobile Phone" data-isuniqueid="false" data-datafield="MobilePhone" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px" data-visible="true">
          <div class="field" data-caption="E-mail" data-isuniqueid="false" data-datafield="Email" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px" data-visible="true">
          <div class="field" data-caption="City" data-isuniqueid="false" data-datafield="City" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px" data-visible="true">
          <div class="field" data-caption="State" data-isuniqueid="false" data-datafield="State" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column" data-width="150px" data-visible="true">
          <div class="field" data-caption="Active Date" data-isuniqueid="false" data-datafield="ActiveDate" data-browsedatatype="text" data-sort="off"></div>
      </div>
      <div class="column spacer" data-width="auto" data-visible="true"></div>
    </div>`;
  }
  getFormTemplate(): string {
    return `
    <div class="fwcontrol fwcontainer fwform contactform" data-control="FwContainer" data-type="form" data-caption="Contact" data-hasaudit="false" data-controller="ContactController">
      <div style="display:none;">
        <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="ContactId"></div>
        <!--<div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="NameFirstMiddleLast"></div>
        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-datafield="ContactRecordType"></div>-->
      </div>
      <div id="contactform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs">
        <div class="tabs">
          <div data-type="tab" id="contacttab" class="tab" data-tabpageid="contacttabpage" data-caption="Contact"></div>
          <div data-type="tab" id="companytab" class="tab" data-tabpageid="companytabpage" data-caption="Company"></div>
          <div data-type="tab" id="profiletab" class="tab" data-tabpageid="profiletabpage" data-caption="Profile"></div>
          <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
          <!-- <div data-type="tab" id="documenttab"     class="tab" data-tabpageid="documenttabpage"     data-caption="Document"></div> -->
          <div data-type="tab" id="accesstab" class="tab" data-tabpageid="accesstabpage" data-caption="Access"></div>
        </div>
        <div class="tabpages">
          <!-- CONTACT TAB -->
          <div data-type="tabpage" id="contacttabpage" class="tabpage" data-tabid="contacttab">
            <div class="flexpage">
              <!-- Name / Status -->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 550px;">
                  <!-- Name / Title  -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Name">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name" data-datafield="FirstName" data-duplicategroup="name"  style="flex:1 1 250px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="M.I." data-datafield="MiddleInitial"                                                       style="flex:1 1 35px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name" data-datafield="LastName" data-duplicategroup="name"    style="flex:1 1 275px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contact Title" data-datafield="ContactTitleId" data-displayfield="ContactTitle" data-validationname="ContactTitleValidation" style="flex:1 1 250px;"></div>
                    </div>
                  </div>
                </div>
                <!-- Status -->
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <!-- Status -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive"                            style="flex:1 1 150px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Active Date" data-datafield="ActiveDate"                           style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Inactive Date" data-datafield="InactiveDate" data-enabled="false"  style="flex:1 1 150px;"></div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- Address / Contact Info-->
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 550px;">
                  <!-- Address -->
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="Address1"   style="flex:1 1 275px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City"                style="flex:1 1 275px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State"              style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="zipcode" class="fwcontrol fwformfield" data-caption="Zip / Postal" data-datafield="ZipCode"  style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 300px;">
                  <!-- Contact Info -->
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Info">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Direct" data-datafield="DirectPhone"                    style="flex:1 0 125px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficePhone"                    style="flex:1 0 125px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ext" data-datafield="OfficeExtension" data-maxlength="6" style="flex:1 0 65px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Cellular" data-datafield="MobilePhone"  style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Home" data-datafield="HomePhone"        style="flex:1 1 125px;"></div>
                      <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax"               style="flex:1 1 125px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="email" class="fwcontrol fwformfield" data-caption="E-mail" data-datafield="Email" style="flex:1 0 325px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- COMPANY TAB -->
          <div data-type="tabpage" id="companytabpage" class="tabpage" data-tabid="companytab">
            <div class="flexpage">
              <!-- Company -->
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Company">
                <div class="wideflexrow">
                  <div data-control="FwGrid" data-grid="ContactCompanyGrid" data-securitycaption="Contact Companies"></div>
                </div>
              </div>
            </div>
          </div>
          <!-- PROFILE TAB -->
          <div data-type="tabpage" id="profiletabpage" class="tabpage" data-tabid="profiletab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="flexcolumn" style="flex:1 1 500px">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Information">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Information" data-datafield="Info"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Site" data-datafield="Website"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:1 1 600px">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Photo">
                    <div class="flexrow">
                      <div data-control="FwAppImage" data-type="" class="fwcontrol fwappimage contactphoto" data-caption="" data-uniqueid1field="ContactId" data-description="" data-rectype="F"></div>
                    </div>
                  </div>
                </div>
                <div class="flexcolumn" style="flex:0 1 405px">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personal Event">
                    <div class="flexrow">
                      <div data-control="FwGrid" data-grid="ContactPersonalEventGrid" data-securitycaption="Contact Notes"></div>                </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!-- NOTES TAB -->
          <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
            <div class="flexpage">
              <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="ContactNoteGrid" data-securitycaption="Contact Notes"></div>
                </div>
              </div>
            </div>
          </div>
          <!--
          <div data-type="tabpage" id="documenttabpage" class="tabpage" data-tabid="documenttab">
            <div class="flexpage">
              <div data-control="FwGrid" data-grid="ContactDocument" data-securitycaption="Contact Document"></div>
            </div>
          </div>
          -->
          <!-- ACCESS TAB -->
          <div data-type="tabpage" id="accesstabpage" class="tabpage" data-tabid="accesstab">
            <div class="flexpage">
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Permissions" style="flex:0 1 500px;">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Web Access" data-datafield="WebAccess"        style="flex:1 1 200px;"></div>
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Account is locked out" data-datafield="LockAccount"  style="flex:1 1 200px;"></div>
                  </div>
                  <!--
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="validation" data-validationname="GroupValidation" class="fwcontrol fwformfield" data-caption="Group" data-datafield="GroupId"></div>
                  </div>
                  -->
                </div>
              </div>
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Password" style="flex:0 1 500px;">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="password" class="fwcontrol fwformfield" data-caption="Web Password" data-datafield="WebPassword" style="flex:1 1 300px;"></div>
                  </div>
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="User must change password at next login" data-datafield="ChangePasswordAtNextLogin" style="flex:1 1 300px;"></div>
                  </div>
                </div>
              </div>
              <div class="flexrow">
                <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Password Expiration" style="flex:0 1 500px;">
                  <div class="flexrow">
                    <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Expire Password" data-datafield="ExpirePassword"                   style="flex:1 1 150px;"></div>
                    <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Expires In (Days)" data-datafield="ExpireDays"                       style="flex:1 1 125px;"></div>
                    <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Change" data-datafield="PasswordLastUpdated" data-enabled="false" style="flex:1 1 125px;"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <!--
          <div data-type="tabpage" id="emailhistorytabpage" class="tabpage" data-tabid="emailhistorytab">
            <div class="flexpage">
              <div data-control="FwGrid" data-grid="ContactEmailHistory" data-securitycaption="Contact Email History"></div>
            </div>
          </div>
          -->
        </div>
      </div>
    </div>`;
  }
}
var TiwContactController = new TiwContact();