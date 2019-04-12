routes.push({ pattern: /^module\/vendor$/, action: function (match: RegExpExecArray) { return VendorController.getModuleScreen(); } });

class Vendor {
    Module: string = 'Vendor';
    apiurl: string = 'api/v1/vendor';
    caption: string = 'Vendor';
    nav: string = 'module/vendor';
    id: string = '92E6B1BE-C9E1-46BD-91A0-DF257A5F909A';
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;
        var self: Vendor = this;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }
    //---------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //---------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.events($form);

        if (mode == 'NEW') {
            this.toggleRequiredFields($form);
            FwFormField.setValueByDataField($form, 'DefaultSubRentDaysPerWeek', 0);
            FwFormField.setValueByDataField($form, 'DefaultSubRentDiscountPercent', 0);
            FwFormField.setValueByDataField($form, 'DefaultSubSaleDiscountPercent', 0);
        }

        return $form;
    }
    //---------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'VendorId', uniqueids.VendorId);
        FwModule.loadForm(this.Module, $form);
        let $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
        $form.find('.purchaseOrderSubModule').append($submodulePurchaseOrderBrowse);
        let $submoduleVendorInvoiceBrowse = this.openVendorInvoiceBrowse($form);
        $form.find('.vendorInvoiceSubModule').append($submoduleVendorInvoiceBrowse);
        return $form;
    }
    //---------------------------------------------------------------------------------
    openPurchaseOrderBrowse($form) {
        let vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        let $browse;
        $browse = PurchaseOrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = PurchaseOrderController.ActiveViewFields;
            request.uniqueids = {
                VendorId: vendorId
            };
        });
        FwBrowse.search($browse);
        return $browse;
    }
   //---------------------------------------------------------------------------------------------
    openVendorInvoiceBrowse($form) {
        let vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        let $browse;
        //$browse = VendorInvoiceController.openBrowse();
        //$browse.data('ondatabind', function (request) {
        //    request.activeviewfields = VendorInvoiceController.ActiveViewFields;
        //    request.uniqueids = {
        //        VendorId: vendorId
        //    };
        //});
        //FwBrowse.search($browse);
        return $browse;
    }
   //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid = FwFormField.getValueByDataField($form, 'VendorId');
        FwModule.loadAudit($form, uniqueid);
    }
    //---------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $companyTaxOptionGrid = $form.find('[data-name="CompanyTaxOptionGrid"]');
        FwBrowse.search($companyTaxOptionGrid);

        var $vendorNoteGrid = $form.find('[data-name="VendorNoteGrid"]');
        FwBrowse.search($vendorNoteGrid);

        var $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);

        this.setupEvents($form);
    }
    //---------------------------------------------------------------------------------
    setupEvents($form: JQuery): void {
        this.toggleRequiredFields($form.find('.tabpages'));
        this.togglePanels($form, FwFormField.getValueByDataField($form, 'VendorNameType'));
    }
    //---------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.on('click', '.vendertyperadio input[type=radio]', (e) => {
            var $tab: JQuery = this.getTab(jQuery(e.currentTarget));
            var value: string = jQuery(e.currentTarget).val().toString();
            this.togglePanels($tab, value);
            this.toggleRequiredFields($tab);
        });

        $form.find('[data-name="CompanyTaxOptionGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                this.updateExternalInputsWithGridValues($tr);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        //$form.on('click', '#companytaxgrid .selected', (e) => {
        //    this.updateExternalInputsWithGridValues(e.currentTarget);
        //});

        //$form.on('click', '#vendornotegrid .selected', (e) => {
        //    this.updateExternalInputsWithGridValues(e.currentTarget);
        //});
    }
    //---------------------------------------------------------------------------------
    getTab($target: JQuery): JQuery {
        return $target.closest('.tabpage');
    }
    //---------------------------------------------------------------------------------
    togglePanels($tab: JQuery, type: string): void {
        $tab.find('.type_panels').hide();
        switch (type) {
            case 'COMPANY':
                $tab.find('#company_panel').show();
                break;
            case 'PERSON':
                $tab.find('#person_panel').show();
                break;
            default:
                throw Error(type + ' is not a known type.');
        }
    }
    //---------------------------------------------------------------------------------
    toggleRequiredFields($tab: JQuery): void {
        var $person = $tab.find('#person_panel'), $company = $tab.find('#company_panel'), personRequired = null, companyRequired = null;

        $person.is(':hidden') ? personRequired = 'false' : personRequired = 'true';
        $company.is(':hidden') ? companyRequired = 'false' : companyRequired = 'true';

        $person.each((i, e) => {
            var $field = jQuery(e).find('.fwformfield');
            if ($person.is(':hidden')) $field.removeClass('error');
            $field.attr('data-required', personRequired);
        });

        $company.each((i, e) => {
            var $field = jQuery(e).find('.fwformfield');
            if ($company.is(':hidden')) $field.removeClass('error');
            $field.attr('data-required', companyRequired);
        });
    }
    //---------------------------------------------------------------------------------
    updateExternalInputsWithGridValues($tr: JQuery): void {
        let TaxOption = $tr.find('.field[data-browsedatafield="TaxOptionId"]').attr('data-originaltext');

        $tr.find('.column > .field').each((i, e) => {
            let $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
            if (value === undefined || null) {
                jQuery(`.${id}`).find(':input').val(0);
            } else {
                jQuery(`.${id}`).find(':input').val(value);
            }
        });
        jQuery('.TaxOption').find(':input').val(TaxOption);
    }
    //---------------------------------------------------------------------------------
    renderGrids($form: JQuery) {
        // load companytax Grid
        var nameCompanyTaxOptionGrid = 'CompanyTaxOptionGrid';
        var $companyTaxOptionGrid: JQuery = $form.find('div[data-grid="' + nameCompanyTaxOptionGrid + '"]');
        var $companyTaxOptionControl: JQuery = FwBrowse.loadGridFromTemplate(nameCompanyTaxOptionGrid);
        $companyTaxOptionGrid.empty().append($companyTaxOptionControl);
        $companyTaxOptionControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
            }
        });
        $companyTaxOptionControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        });
        FwBrowse.init($companyTaxOptionControl);
        FwBrowse.renderRuntimeHtml($companyTaxOptionControl);

        // load vendornote Grid
        var nameVendorNoteGrid = 'VendorNoteGrid';
        var $vendorNoteGrid: JQuery = $form.find('div[data-grid="' + nameVendorNoteGrid + '"]');
        var $vendorNoteControl: JQuery = FwBrowse.loadGridFromTemplate(nameVendorNoteGrid);
        $vendorNoteGrid.empty().append($vendorNoteControl);
        $vendorNoteControl.data('ondatabind', function (request) {
            request.uniqueids = {
                VendorId: FwFormField.getValueByDataField($form, 'VendorId')
            }
        });
        FwBrowse.init($vendorNoteControl);
        FwBrowse.renderRuntimeHtml($vendorNoteControl);

        // ----------
        var nameCompanyContactGrid: string = 'CompanyContactGrid'
        var $companyContactGrid: any = $companyContactGrid = $form.find('div[data-grid="' + nameCompanyContactGrid + '"]');
        var $companyContactControl: any = FwBrowse.loadGridFromTemplate(nameCompanyContactGrid);
        $companyContactGrid.empty().append($companyContactControl);
        $companyContactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
            }
        });
        $companyContactControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        });
        FwBrowse.init($companyContactControl);
        FwBrowse.renderRuntimeHtml($companyContactControl);
    }
    //---------------------------------------------------------------------------------
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
    //---------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="vendorform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Vendor" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="VendorController">
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
    //---------------------------------------------------------------------------------
}
var VendorController = new Vendor();