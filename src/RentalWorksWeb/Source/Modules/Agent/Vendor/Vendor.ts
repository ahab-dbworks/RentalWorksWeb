//routes.push({ pattern: /^module\/vendor$/, action: function (match: RegExpExecArray) { return VendorController.getModuleScreen(); } });
//---------------------------------------------------------------------------------
class Vendor {
    Module: string = 'Vendor';
    apiurl: string = 'api/v1/vendor';
    caption: string = Constants.Modules.Agent.children.Vendor.caption;
    nav: string = Constants.Modules.Agent.children.Vendor.nav;
    id: string = Constants.Modules.Agent.children.Vendor.id;
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

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
    //---------------------------------------------------------------------------------
    openBrowse() {
        let $browse = jQuery(this.getBrowseTemplate());
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //---------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        this.events($form);

        if (mode == 'NEW') {
            this.toggleRequiredFields($form);

            const officeLocation = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValueByDataField($form, 'DefaultSubRentDaysPerWeek', 0);
            FwFormField.setValueByDataField($form, 'DefaultSubRentDiscountPercent', 0);
            FwFormField.setValueByDataField($form, 'DefaultSubSaleDiscountPercent', 0);
            $form.find('div[data-datafield="Vendor"]').attr('data-required', 'true');
        }

        //Toggle Buttons - Vendor tab - Vendor Type Company or Person
        FwFormField.loadItems($form.find('div[data-datafield="VendorNameType"]'), [
            { value: 'COMPANY', caption: 'Company', checked: true },
            { value: 'PERSON',  caption: 'Person' }
        ]);

        //Toggle Buttons - Deliver/Ship tab - Default Delivery Address
        FwFormField.loadItems($form.find('div[data-datafield="DefaultOutgoingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Vendor Deliver' },
            { value: 'SHIP',    caption: 'Ship' },
            { value: 'PICK UP',  caption: 'Pick Up' }
        ]);

        //Toggle Buttons - Deliver/Ship tab - Default Return Delivery Address
        FwFormField.loadItems($form.find('div[data-datafield="DefaultIncomingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Deliver' },
            { value: 'SHIP',    caption: 'Ship' },
            { value: 'PICK UP',  caption: 'Vendor Pick Up' }
        ]);

        return $form;
    }
    //---------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'VendorId', uniqueids.VendorId);
        FwModule.loadForm(this.Module, $form);
        const $submodulePurchaseOrderBrowse = this.openPurchaseOrderBrowse($form);
        $form.find('.purchaseOrderSubModule').append($submodulePurchaseOrderBrowse);
        const $submoduleVendorInvoiceBrowse = this.openVendorInvoiceBrowse($form);
        $form.find('.vendorInvoiceSubModule').append($submoduleVendorInvoiceBrowse);
        return $form;
    }
    //---------------------------------------------------------------------------------
    openPurchaseOrderBrowse($form) {
        const vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        const $browse = PurchaseOrderController.openBrowse();
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
        const vendorId = FwFormField.getValueByDataField($form, 'VendorId');
        const $browse = VendorInvoiceController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = VendorInvoiceController.ActiveViewFields;
            request.uniqueids = {
                VendorId: vendorId
            };
        });
        FwBrowse.search($browse);
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //---------------------------------------------------------------------------------
    afterLoad($form: any) {
        const $companyTaxOptionGrid = $form.find('[data-name="CompanyTaxOptionGrid"]');
        FwBrowse.search($companyTaxOptionGrid);

        const $vendorNoteGrid = $form.find('[data-name="VendorNoteGrid"]');
        FwBrowse.search($vendorNoteGrid);

        const $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');
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
        // ----------
        //Company Tax Option Grid
        //const $companyTaxOptionGrid = $form.find('div[data-grid="CompanyTaxOptionGrid"]');
        //const $companyTaxOptionControl = FwBrowse.loadGridFromTemplate('CompanyTaxOptionGrid');
        //$companyTaxOptionGrid.empty().append($companyTaxOptionControl);
        //$companyTaxOptionControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //$companyTaxOptionControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        //});
        //FwBrowse.init($companyTaxOptionControl);
        //FwBrowse.renderRuntimeHtml($companyTaxOptionControl);

        FwBrowse.renderGrid({
            nameGrid: 'CompanyTaxOptionGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
            }
        });
        // ----------
        //Vendor Note Grid
        //const $vendorNoteGrid = $form.find('div[data-grid="VendorNoteGrid"]');
        //const $vendorNoteControl = FwBrowse.loadGridFromTemplate('VendorNoteGrid');
        //$vendorNoteGrid.empty().append($vendorNoteControl);
        //$vendorNoteControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        VendorId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //FwBrowse.init($vendorNoteControl);
        //FwBrowse.renderRuntimeHtml($vendorNoteControl);

        FwBrowse.renderGrid({
            nameGrid: 'VendorNoteGrid',
            gridSecurityId: 'zuywROD73X60O',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {

                request.uniqueids = {
                    VendorId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
            //beforeSave: (request: any) => {
            //    request.VendorId = FwFormField.getValueByDataField($form, 'VendorId');
            //}
        });
        // ----------
        //const $companyContactGrid = $form.find('div[data-grid="CompanyContactGrid"]');
        //const $companyContactControl = FwBrowse.loadGridFromTemplate('CompanyContactGrid');
        //$companyContactGrid.empty().append($companyContactControl);
        //$companyContactControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
        //    }
        //});
        //$companyContactControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
        //});
        //FwBrowse.init($companyContactControl);
        //FwBrowse.renderRuntimeHtml($companyContactControl);

        FwBrowse.renderGrid({
            nameGrid: 'CompanyContactGrid',
            gridSecurityId: 'gQHuhVDA5Do2',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'VendorId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'VendorId');
            }
        });
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
        <div class="column spacer" data-width="auto" data-visible="true"></div>
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
            <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
            <div data-type="tab" id="taxtab" class="tab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
            <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
            <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
            <div data-type="tab" id="purchaseordertab" class="tab submodule" data-tabpageid="purchaseordertabpage" data-caption="Purchase Order"></div>
            <div data-type="tab" id="vendorinvoicetab" class="tab submodule" data-tabpageid="vendorinvoicetabpage" data-caption="Vendor Invoice"></div>
            <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
          </div>
          <div class="tabpages">
            <div data-type="tabpage" id="vendortabpage" class="tabpage" data-tabid="vendortab">
              <div class="flexpage">
                <div class="flexrow">
                  <!-- Vendor section -->
                  <div class="flexcolumn" style="flex:1 1 300px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield vendertyperadio" data-caption="Type" data-datafield="VendorNameType" style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor No" data-datafield="VendorNumber" data-required="true" style="flex:1 1 125px;"></div>
                      </div>
                      <div id="company_panel" class="type_panels" style="flex:1 1 500px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Vendor" style="flex:1 1 475px;"></div>
                        </div>
                      </div>
                      <div id="person_panel" class="type_panels" style="display:none;flex:1 1 500px;">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Salutation"  data-datafield="Salutation"     data-required="false" style="flex:0 1 70px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="First Name"  data-datafield="FirstName"      data-required="false" style="flex:1 1 175px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="M.I."        data-datafield="MiddleInitial"  data-required="false" style="flex:0 1 35px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Last Name"   data-datafield="LastName"       data-required="false" style="flex:1 1 200px;"></div>
                        </div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield officelocation" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="flex:1 1 200px;" data-required="true"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation"  class="fwcontrol fwformfield vendorclass" data-caption="Class" data-datafield="VendorClassId" data-displayfield="VendorClass" data-validationname="VendorClassValidation" style="flex:1 1 200px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield customer" data-caption="Rental Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" style="flex:1 1 300px"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal ID No" data-datafield="FederalIdNumber" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" data-allcaps="false" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" data-allcaps="false" style="flex:1 1 275px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Address section -->
                  <div class="flexcolumn" style="flex:1 1 275px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address" data-datafield="Address1" style="flex:1 1 275px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 275px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" style="flex:1 1 150px;"></div>
                       <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                     </div>
                     <div class="flexrow">
                       <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                     </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact Numbers">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 100px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone Toll-Free" data-datafield="PhoneTollFree" style="flex:1 1 100px;"></div>
                        <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Other" data-datafield="OtherPhone" style="flex:1 1 100px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO Defaults">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Sub-Rental D/W" data-datafield="DefaultSubRentDaysPerWeek" data-required="false" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Sub-Rental Discount %" data-datafield="DefaultSubRentDiscountPercent" data-required="false" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Sub-Sale Discount %" data-datafield="DefaultSubSaleDiscountPercent" data-required="false" style="flex:1 1 75px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Status section -->
                  <div class="flexcolumn" style="flex:1 1 275px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Active Date" data-datafield="ActiveDate" data-enabled="false" data-readonly="true" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Inactive" data-datafield="Inactive" style="flex:1 1 75px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Inactive Date" data-datafield="InactiveDate" data-enabled="false" data-readonly="true" style="flex:1 1 75px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Activity Type">
                      <div class="flexrow">
                        <div class="flexcolumn">
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental Inventory" data-datafield="RentalInventory" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales / Parts" data-datafield="SalesPartsInventory" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Repair" data-datafield="Repair" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Consignment" data-datafield="Consignment" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Manufacturer" data-datafield="Manufacturer" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Freight" data-datafield="Freight" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rent" data-datafield="SubRent" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sales" data-datafield="SubSales" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Misc" data-datafield="SubMisc" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Labor" data-datafield="SubLabor" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Vehicle" data-datafield="SubVehicle" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow activity">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Insurance" data-datafield="Insurance" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- BILLING TAB -->
            <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
              <div class="flexpage">
                <div class="flexrow">
                  <!-- Customer section -->
                  <div class="flexcolumn" style="flex:0 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Account Number" data-datafield="AccountNumber" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Default Rate" data-datafield="DefaultRate" data-displayfield="DefaultRate" data-validationname="RateTypeValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sub-Rental Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" data-validationname="BillingCycleValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Organization Type" data-datafield="OrganizationTypeId" data-displayfield="OrganizationType" data-validationname="OrganizationTypeValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Default PO Class" data-datafield="DefaultPoClassificationId" data-displayfield="DefaultPoClassification" data-validationname="POClassificationValidation" style="flex:1 1 275px;"></div>
                      </div>>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Currency" data-datafield="DefaultCurrencyId" data-displayfield="DefaultCurrencyCode" data-validationname="CurrencyValidation" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="DefaultCurrency" data-enabled="false" style="flex:1 1 275px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Address section -->
                  <div class="flexcolumn" style="flex:0 1 325px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Remit To Address">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="RemitAddress1" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="RemitAddress2" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="RemitCity" style="flex:1 1 275px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="RemitState" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="RemitZipCode"  style="flex:1 1 150px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="RemitCountryId" data-displayfield="RemitCountry" data-validationname="CountryValidation" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Payee No" data-datafield="RemitPayeeNo" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="External ID">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Vendor External ID" data-datafield="ExternalId" style="flex:1 1 275px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- TAX TAB -->
            <div data-type="tabpage" id="taxtabpage" class="tabpage" data-tabid="taxtab">
              <div class="flexpage">
                <div class="flexrow">
                  <div id="companytaxoptiongrid" data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Company Tax Grid"></div>
                </div>
              </div>
            </div>

            <!-- SHIPPING TAB -->
            <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
              <div class="flexpage">
                <div class="flexrow">
                  <!-- Default section -->
                  <div class="flexcolumn" style="flex:0 1 425px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Delivery">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DefaultOutgoingDeliveryType" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Return Delivery">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DefaultIncomingDeliveryType" style="flex:1 1 150px;"></div>
                      </div>
                    </div>
                  </div>
                  <!-- Tracking No. section -->
                  <div class="flexcolumn" style="flex:0 1 425px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tracking No">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Tracking No. Hyperlink" data-datafield="ShippingTrackingLink"></div>
                      </div>
                      <div class="flexrow">
                        <div style="padding-left:5px;font-size:14px;" >Use the token @trackingno to indicate where the tracking number needs to be injected.</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>




<!--
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Delivery">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultOutgoingDeliveryType" style="flex: 1 1 265px;">
                          <div data-value="DELIVER" data-caption="Vendor Deliver"></div>
                          <div data-value="SHIP" data-caption="Ship"></div>
                          <div data-value="PICK UP" data-caption="Pick Up"></div>
                        </div>
                      </div>
                    </div>
-->
<!--
                    <div class="flexrow">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Return Delivery" style="padding-left:1px;">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="DefaultIncomingDeliveryType" style="flex: 1 1 265px;">
                          <div data-value="DELIVER" data-caption="Deliver"></div>
                          <div data-value="SHIP" data-caption="Ship"></div>
                          <div data-value="PICK UP" data-caption="Vendor Pick Up"></div>
                        </div>
                      </div>
                    </div>
-->

            <!-- CONTACTS TAB -->
            <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
              <div class="flexpage">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Vendor Contacts"></div>
                </div>
              </div>
            </div>

            <!-- PURCHASE ORDER TAB -->
            <div data-type="tabpage" id="purchaseordertabpage" class="tabpage purchaseOrderSubModule rwSubModule" data-tabid="purchaseordertab">
            </div>

            <!-- VENDOR INVOICE TAB -->
            <div data-type="tabpage" id="vendorinvoicetabpage" class="tabpage vendorInvoiceSubModule rwSubModule" data-tabid="vendorinvoicetab">
            </div>

            <!-- NOTES TAB -->
            <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
              <div class="flexpage">
                <div class="flexrow">
                  <div data-control="FwGrid" data-grid="VendorNoteGrid" data-securitycaption="Vendor Note" style="flex:1 1 1200px;"></div>
                </div>
                <!--<div class="flexrow">
                  <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vendor Updated" style="flex:0 1 525px;">
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NoteDate" data-caption="Opened" data-enabled="false" data-datafield="InputDate" style="flex: 1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NotesBy" data-caption="By" data-enabled="false" data-datafield="ShippingTrackingLink" style="flex:1 1 350px;"></div>
                    </div>
                    <div class="flexrow">
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield Datestamp" data-caption="Modified Last" data-enabled="false" data-datafield="DateStamp" style="flex:1 1 150px;"></div>
                      <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield NotesBy" data-caption="By" data-enabled="false" data-datafield="ShippingTrackingLink" style="flex:1 1 350px;"></div>
                    </div>
                  </div>
                </div>-->
              </div>
            </div>

          </div>
        </div>
      </div>`;
    }
    //---------------------------------------------------------------------------------
}
//---------------------------------------------------------------------------------
var VendorController = new Vendor();