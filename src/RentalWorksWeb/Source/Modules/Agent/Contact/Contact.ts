//routes.push({ pattern: /^module\/contact$/, action: function (match: RegExpExecArray) { return ContactController.getModuleScreen(); } });

class Contact {
    Module: string = 'Contact';
    apiurl: string = 'api/v1/contact';
    caption: string = Constants.Modules.Agent.children.Contact.caption;
    nav: string = Constants.Modules.Agent.children.Contact.nav;
    id: string = Constants.Modules.Agent.children.Contact.id;
    ActiveView: string = 'ALL';
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var me: Contact = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $browse: JQuery = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
                for (let key in response) {
                    FwBrowse.addLegend($browse, key, response[key]);
                }
            }, function onError(response) {
                FwFunc.showError(response);
            }, $browse)
        } catch (ex) {
            FwFunc.showError(ex);
        }

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);
        //FwTabs.hideTab($form.find('.ordertab'));
        //FwTabs.hideTab($form.find('.quotetab'));

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

        //FwFormField.getDataField($form, 'DefaultDealId').data('beforevalidate', ($validationbrowse: JQuery, $object: JQuery, request: any, datafield, $tr: JQuery) => {
        //    request.uniqueids.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
        //    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
        //});
        FwFormField.getDataField($form, 'LocationId').on('change', (e: JQuery.ChangeEvent) => {
            try {
                FwFormField.setValueByDataField($form, 'WarehouseId', '', '');
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //FwFormField.getDataField($form, 'WarehouseId').data('beforevalidate', ($validationbrowse: JQuery, $object: JQuery, request: any, datafield, $tr: JQuery) => {
        //    request.uniqueids.LocationId = FwFormField.getValueByDataField($form, 'LocationId');
        //});

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        let $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ContactId"] input').val(uniqueids.ContactId);

        // Documents Grid - Need to put this here, because renderGrids is called from openForm and uniqueid is not available yet on the form
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'ContactDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${uniqueids.ContactId}/document`;
            },
            gridSecurityId: 'OdKeQWKOM7sL',
            moduleSecurityId: this.id,
            parentFormDataFields: 'ContactId',
            uniqueid1Name: 'ContactId',
            getUniqueid1Value: () => uniqueids.ContactId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

        FwModule.loadForm(this.Module, $form);

        if (FwApplicationTree.isVisibleInSecurityTree('U8Zlahz3ke9i')) {
            FwTabs.showTab($form.find('.ordertab'));
            $form.find('.orderSubModule').append(this.openOrderBrowse($form));
        }

        if (FwApplicationTree.isVisibleInSecurityTree('jFkSBEur1dluU')) {
            FwTabs.showTab($form.find('.quotetab'));
            $form.find('.quoteSubModule').append(this.openQuoteBrowse($form));
        }

        return $form;
    }
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
        // Contact Note Grid
        FwBrowse.renderGrid({
            nameGrid: 'ContactNoteGrid',
            gridSecurityId: 'mkJ1Ry8nqSnw',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContactId: FwFormField.getValueByDataField($form, 'ContactId')
                };
            },
            beforeSave: (request: any) => {
                request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
            },
        });

        // Personal Event Grid
        FwBrowse.renderGrid({
            nameGrid: 'ContactPersonalEventGrid',
            gridSecurityId: '35was7r004gg',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContactId: FwFormField.getValueByDataField($form, 'ContactId')
                };
            },
            beforeSave: (request: any) => {
                request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
            }
        });

        //Company Contact grid
        FwBrowse.renderGrid({
            nameGrid: 'ContactCompanyGrid',
            gridSecurityId: 'gQHuhVDA5Do2',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    ContactId: FwFormField.getValueByDataField($form, 'ContactId')
                };
            },
            beforeSave: (request: any) => {
                request.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
            }

        });

        this.addLegend($form);
    }
    //----------------------------------------------------------------------------------------------
    addLegend($form: any) {
        const $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/companycontact/legend`, null, FwServices.defaultTimeout, function onSuccess(response) {
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
        //const $contactNoteGrid = $form.find('[data-name="ContactNoteGrid"]');
        //FwBrowse.search($contactNoteGrid);

        //const $contactPersonalEventGrid = $form.find('[data-name="ContactPersonalEventGrid"]');
        //FwBrowse.search($contactPersonalEventGrid);

        //const $companyContactGrid = $form.find('[data-name="ContactCompanyGrid"]');
        //FwBrowse.search($companyContactGrid);

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
        $form.on('click', '.submodule', e => {
            const tabPageId = jQuery(e.currentTarget).attr('data-tabpageid');
            const $subModuleBrowse = $form.find(`#${tabPageId} .fwbrowse`);
            FwBrowse.search($subModuleBrowse);
        });

        //Click Event on tabs to load grids/browses
        $form.find('.tabGridsLoaded[data-type="tab"]').removeClass('tabGridsLoaded');
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
            const $tab = jQuery(e.currentTarget);
            const tabname = $tab.attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');  // for cases where "tab" is included in the name of the tab
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;
            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if (($tab.hasClass('tabGridsLoaded') === false) && $gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    try {
                        const $gridcontrol = jQuery($gridControls[i]);
                        FwBrowse.search($gridcontrol);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if (($tab.hasClass('tabGridsLoaded') === false) && $browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
            $tab.addClass('tabGridsLoaded');
        });
    }
    //--------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'ContactTitleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontacttitle`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
            case 'DefaultDealId':
                request.uniqueids.ContactId = FwFormField.getValueByDataField($form, 'ContactId');
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedeal`);
                break;
            case 'LocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelocation`);
                break;
            case 'WarehouseId':
                request.uniqueids.LocationId = FwFormField.getValueByDataField($form, 'LocationId');
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatewarehouse`);
                break;
        }
    }
    //--------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
      <div data-name="Contact" data-control="FwBrowse" data-type="Browse" class="fwcontrol fwbrowse" data-controller="ContactController" data-hasinactive="true">
        <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="ContactId" data-browsedatatype="key" ></div>
        </div>
        <div class="column" data-width="0" data-visible="false">
            <div class="field" data-datafield="Inactive" data-browsedatatype="text"  data-visible="false"></div>
        </div>
        <div class="column" data-width="150px">
            <div class="field" data-caption="Last Name" data-isuniqueid="false" data-datafield="LastName" data-browsedatatype="text" data-sort="asc"></div>
        </div>
        <div class="column" data-width="150px">
            <div class="field" data-caption="First Name" data-isuniqueid="false" data-datafield="FirstName" data-cellcolor="FirstNameColor" data-browsedatatype="text" data-sort="off"></div>
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
    //--------------------------------------------------------------------------------------------
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
              <div data-type="tab" id="contacttab" class="tab contacttab" data-tabpageid="contacttabpage" data-caption="Contact"></div>
              <div data-type="tab" id="companytab" class="tab companytab" data-tabpageid="companytabpage" data-caption="Company"></div>
              <div data-type="tab" id="profiletab" class="tab profiletab" data-tabpageid="profiletabpage" data-caption="Profile"></div>
              <div data-type="tab" id="documentstab" class="tab documentstab" data-tabpageid="documentstabpage" data-caption="Documents"></div>
              <div data-type="tab" id="notestab" class="tab notestab" data-tabpageid="notestabpage" data-caption="Notes"></div>
              <div data-type="tab" id="accesstab" class="tab accesstab" data-tabpageid="accesstabpage" data-caption="Web Access"></div>
              <div data-type="tab" id="quotetab" class="tab submodule quotetab" data-tabpageid="quotetabpage" data-caption="Quote"></div>
              <div data-type="tab" id="ordertab" class="tab submodule ordertab" data-tabpageid="ordertabpage" data-caption="Order"></div>
            </div>
            <div class="tabpages">
              <!-- CONTACT TAB -->
              <div data-type="tabpage" id="contacttabpage" class="tabpage" data-tabid="contacttab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name" data-datafield="FirstName" data-duplicategroup="name" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="M.I." data-datafield="MiddleInitial" style="flex:0 1 50px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name" data-datafield="LastName" data-duplicategroup="name" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Contact Title" data-datafield="ContactTitleId" data-displayfield="ContactTitle" data-validationname="ContactTitleValidation" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="email" class="fwcontrol fwformfield" data-caption="E-mail" data-datafield="Email" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Numbers">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Office" data-datafield="OfficePhone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Ext" data-datafield="OfficeExtension" data-maxlength="6" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Direct" data-datafield="DirectPhone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Cellular" data-datafield="MobilePhone" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Home" data-datafield="HomePhone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="Address1" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip / Postal" data-datafield="ZipCode" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Active Date" data-datafield="ActiveDate" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Inactive Date" data-datafield="InactiveDate" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive" style="flex:1 1 75px;margin-top:10px;margin-left:5px;"></div>
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
                  <div class="wideflexrow">
                    <div data-control="FwGrid" data-grid="ContactCompanyGrid" data-securitycaption="Contact Companies"></div>
                  </div>
                </div>
              </div>
              <!-- PROFILE TAB -->
              <div data-type="tabpage" id="profiletabpage" class="tabpage" data-tabid="profiletab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 400px">
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="General Details">
                          <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Information" data-datafield="Info"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Site" data-datafield="Website"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Personal Events">
                          <div class="flexrow">
                            <div data-control="FwGrid" data-grid="ContactPersonalEventGrid" data-securitycaption="Contact Personal Events"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Photo">
                        <div class="flexrow">
                          <div data-control="FwAppImage" data-type="" class="fwcontrol fwappimage contactphoto" data-caption="" data-uniqueid1field="ContactId" data-description="" data-rectype="F"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              
              <!-- DOCUMENTS TAB -->
              <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="ContactDocumentGrid"></div>
                  </div>
                </div>
              </div>

              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="ContactNoteGrid" data-securitycaption="Contact Notes"></div>
                  </div>
                </div>
              </div>
              
              <!-- ACCESS TAB -->
              <div data-type="tabpage" id="accesstabpage" class="tabpage" data-tabid="accesstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Permissions" style="flex:0 1 500px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Enable Web Access" data-datafield="WebAccess" style="flex:1 1 200px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Account is locked out" data-datafield="LockAccount" style="flex:1 1 200px;"></div>
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
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Expire Password" data-datafield="ExpirePassword" style="flex:1 1 150px;margin-top:10px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Expires In (Days)" data-datafield="ExpireDays" style="flex:1 1 125px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Change" data-datafield="PasswordLastUpdated" data-enabled="false" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote Requests" style="flex:0 1 500px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Default Deal" data-datafield="DefaultDealId" data-displayfield="DefaultDeal" data-validationname="ContactDealValidation" style="flex:1 1 300px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Location" data-datafield="LocationId" data-displayfield="Location" data-validationname="OfficeLocationValidation" style="flex:1 1 300px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Warehouse" data-datafield="WarehouseId" data-displayfield="Warehouse" data-validationname="WarehouseValidation" style="flex:1 1 300px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- QUOTE TAB -->
              <div data-type="tabpage" id="quotetabpage" class="tabpage quoteSubModule rwSubModule" data-tabid="quotetab">
              </div>
              <!-- ORDER TAB -->
              <div data-type="tabpage" id="ordertabpage" class="tabpage orderSubModule rwSubModule" data-tabid="ordertab">
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
}
//--------------------------------------------------------------------------------------------
var ContactController = new Contact();