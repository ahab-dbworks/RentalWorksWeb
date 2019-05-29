//routes.push({ pattern: /^module\/contact$/, action: function (match: RegExpExecArray) { return ContactController.getModuleScreen(); } });

class Contact {
    Module: string = 'Contact';
    apiurl: string = 'api/v1/contact';
    caption: string = Constants.Modules.Home.Contact.caption;
    nav: string = Constants.Modules.Home.Contact.nav;
    id: string = Constants.Modules.Home.Contact.id;
    ActiveView: string = 'ALL';

    getModuleScreen() {
        var me: Contact = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, me.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    };

    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var self = this;
        //var $browse: JQuery = FwBrowse.loadBrowseFromTemplate(this.Module);
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        //var $form;
        //$form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());

        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            $form.find('.ifnew').attr('data-enabled', 'true');
            const today = FwFunc.getDate();

            FwFormField.setValueByDataField($form, 'ActiveDate', today);

            // Disable / Enable Inactive Date
            $form.find('[data-datafield="Inactive"] .fwformfield-value').on('change', function () {
                var $this = jQuery(this);
                if ($this.prop('checked') === true) {
                    FwFormField.enable($form.find('div[data-datafield="InactiveDate"]'));
                    FwFormField.setValueByDataField($form, 'InactiveDate', today);
                }
                else {
                    FwFormField.disable($form.find('div[data-datafield="InactiveDate"]'));
                    FwFormField.setValueByDataField($form, 'InactiveDate', "");
                }
            });
        }

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: JQuery = this.openForm('EDIT');

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);
        FwModule.loadForm(this.Module, $form);

        $form.find('.orderSubModule').append(this.openOrderBrowse($form));
        $form.find('.quoteSubModule').append(this.openQuoteBrowse($form));

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    openOrderBrowse($form) {
        const contactId = FwFormField.getValueByDataField($form, 'ContactId');
        const $browse = OrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = OrderController.ActiveViewFields;
            request.uniqueids = {
                ContactId: contactId
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openQuoteBrowse($form) {
        const contactId = FwFormField.getValueByDataField($form, 'ContactId');
        const $browse = QuoteController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = QuoteController.ActiveViewFields;
            request.uniqueids = {
                ContactId: contactId
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        const $contactNoteGrid = $form.find('div[data-grid="ContactNoteGrid"]');
        const $contactNoteGridControl = FwBrowse.loadGridFromTemplate('ContactNoteGrid');
        $contactNoteGrid.empty().append($contactNoteGridControl);
        $contactNoteGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ContactId: FwFormField.getValueByDataField($form, 'ContactId')
            };
        })
        $contactNoteGridControl.data('beforesave', request => {
            request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwBrowse.init($contactNoteGridControl);
        FwBrowse.renderRuntimeHtml($contactNoteGridControl);

        // Personal Event Grid
        const $contactPersonalEventGrid = $form.find('div[data-grid="ContactPersonalEventGrid"]');
        const $contactPersonalEventGridControl = FwBrowse.loadGridFromTemplate('ContactPersonalEventGrid');
        $contactPersonalEventGrid.empty().append($contactPersonalEventGridControl);
        $contactPersonalEventGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ContactId: FwFormField.getValueByDataField($form, 'ContactId')
            };
        })
        $contactPersonalEventGridControl.data('beforesave', request => {
            request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwBrowse.init($contactPersonalEventGridControl);
        FwBrowse.renderRuntimeHtml($contactPersonalEventGridControl);

        //Company Contact grid
        const $companyContactGrid = $form.find('div[data-grid="ContactCompanyGrid"]');
        const $companyContactGridControl = FwBrowse.loadGridFromTemplate('ContactCompanyGrid');
        $companyContactGrid.empty().append($companyContactGridControl);
        $companyContactGridControl.data('ondatabind', request => {
            request.uniqueids = {
                ContactId: FwFormField.getValueByDataField($form, 'ContactId')
            };
        })
        $companyContactGridControl.data('beforesave', request => {
            request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        });
        FwBrowse.init($companyContactGridControl);
        FwBrowse.renderRuntimeHtml($companyContactGridControl);
        this.addLegend($form);
    };
    //----------------------------------------------------------------------------------------------
    addLegend($form: any) {
        const $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        try {
            FwAppData.apiMethod(true, 'GET', `api/v1/companycontact/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (var key in response) {
                    FwBrowse.addLegend($companyContactGrid, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $companyContactGrid)

        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
        const $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        FwBrowse.search($contactNoteGrid);

        const $contactPersonalEventGrid = $form.find('[data-name="ContactPersonalEventGrid"]');
        FwBrowse.search($contactPersonalEventGrid);

        const $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        FwBrowse.search($companyContactGrid);

        // Disable / Enable Inactive Date
        $form.find('[data-datafield="Inactive"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                const today = FwFunc.getDate();
                FwFormField.enable($form.find('div[data-datafield="InactiveDate"]'));
                FwFormField.setValueByDataField($form, 'InactiveDate', today);
            }
            else {
                FwFormField.disable($form.find('div[data-datafield="InactiveDate"]'));
                FwFormField.setValueByDataField($form, 'InactiveDate', "");
            }
        });

        //On click events for Quote/Order tabs
        $form.on('click', '.sub-module-tab', e => {
            const tabPageId = jQuery(e.currentTarget).attr('data-tabpageid');
            const $subModuleBrowse = $form.find(`#${tabPageId} .fwbrowse`);
            FwBrowse.search($subModuleBrowse);
        });
    };
    //--------------------------------------------------------------------------------------------
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
          <div class="field" data-caption="E-mail" data-isuniqueid="false" data-datafield="Email" data-browsedatatype="text" data-sort="off" data-allcaps="false"></div>
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
            <div data-type="tab" id="quotetab" class="tab sub-module-tab" data-tabpageid="quotetabpage" data-caption="Quote"></div>
            <div data-type="tab" id="ordertab" class="tab sub-module-tab" data-tabpageid="ordertabpage" data-caption="Order"></div>
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
                        <div data-control="FwFormField" data-type="email" class="fwcontrol fwformfield" data-caption="E-mail" data-datafield="Email" style="flex:1 0 325px;" data-allcaps="false"></div>
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
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Site" data-datafield="Website" data-allcaps="false"></div>
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
                        <div data-control="FwGrid" data-grid="ContactPersonalEventGrid" data-securitycaption="Contact Notes"></div>                
                      </div>
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
                <div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Equipment Requests" style="flex:0 1 500px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Default Deal" data-datafield="DefaultDealId" data-displayfield="DefaultDeal" data-validationname="ContactJobValidation" style="float:left;width:175px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Location" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" style="float:left;width:175px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="float:left;width:175px;"></div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
     <!-- QUOTE TAB -->
           <div data-type="tabpage" id="quotetabpage" class="tabpage quoteSubModule" data-tabid="quotetab">
              </div>
     <!-- ORDER TAB -->
           <div data-type="tabpage" id="ordertabpage" class="tabpage orderSubModule" data-tabid="ordertab">
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
    //--------------------------------------------------------------------------------------------

    //--------------------------------------------------------------------------------------------
}
//--------------------------------------------------------------------------------------------
var ContactController = new Contact();