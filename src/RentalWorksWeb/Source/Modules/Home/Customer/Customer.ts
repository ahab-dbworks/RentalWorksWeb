class Customer {
    Module: string = 'Customer';
    apiurl: string = 'api/v1/customer';
    caption: string = Constants.Modules.Home.Customer.caption;
    nav: string = Constants.Modules.Home.Customer.nav;
    id: string = Constants.Modules.Home.Customer.id;
    thisModule: Customer;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $browse: any = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            // Dashboard search
            if (typeof filter !== 'undefined') {
                const datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                const parsedSearch = filter.search.replace(/%20/g, " ").replace(/%2f/g, '/');
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(parsedSearch);
            }

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

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        // example: setting validation getapiurl functions
        //FwFormField.getDataField($form, 'OfficeLocationId').data('getapiurl', () => 'api/v1/customer/lookup/officelocations');
        
        if (mode === 'NEW') {
            let officeLocation = JSON.parse(sessionStorage.getItem('location'));
            let customerStatus = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValue($form, 'div[data-datafield="CustomerStatusId"]', customerStatus.defaultcustomerstatusid, customerStatus.defaultcustomerstatus);
        }

        // SUBMODULES
        // Deal  submodule
        let $submoduleDealBrowse = this.openDealBrowse($form);
        $form.find('.deal-page').append($submoduleDealBrowse);
        $submoduleDealBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleDealBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var $dealForm, controller, $browse, dealFormData: any = {};
            $browse = jQuery(this).closest('.fwbrowse');
            controller = $browse.attr('data-controller');
            dealFormData.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
            dealFormData.Customer = FwFormField.getValueByDataField($form, 'Customer');
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
            $dealForm = window[controller]['openForm']('NEW', dealFormData);
            FwModule.openSubModuleTab($browse, $dealForm);
        });
        // Quote submodule
        let $submoduleQuoteBrowse = this.openQuoteBrowse($form);
        $form.find('.quote-page').append($submoduleQuoteBrowse);
        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var $quoteForm, controller, $browse, quoteFormData: any = {};
            $browse = jQuery(this).closest('.fwbrowse');
            controller = $browse.attr('data-controller');
            quoteFormData.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
            quoteFormData.Customer = FwFormField.getValueByDataField($form, 'Customer');
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
            $quoteForm = window[controller]['openForm']('NEW', quoteFormData);
            FwModule.openSubModuleTab($browse, $quoteForm);
        });
        // Order submodule 
        let $submoduleOrderBrowse = this.openOrderBrowse($form);
        $form.find('.order-page').append($submoduleOrderBrowse);
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var $orderForm, controller, $browse, orderFormData: any = {};
            $browse = jQuery(this).closest('.fwbrowse');
            controller = $browse.attr('data-controller');
            orderFormData.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
            orderFormData.Customer = FwFormField.getValueByDataField($form, 'Customer');
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
            $orderForm = window[controller]['openForm']('NEW', orderFormData);
            FwModule.openSubModuleTab($browse, $orderForm);
        });
        // Customer Credit submodule
        const $submoduleCustomerCreditBrowse = this.openCustomerCreditBrowse($form);
        $form.find('.credits-page').append($submoduleCustomerCreditBrowse);

        $form.find('[data-datafield="UseDiscountTemplate"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.discount-validation'))
            } else {
                FwFormField.disable($form.find('.discount-validation'))
            }
        });

        $form.find('[data-datafield="DisableQuoteOrderActivity"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.quote-order [data-type="checkbox"]'))
            } else {
                FwFormField.disable($form.find('.quote-order [data-type="checkbox"]'))
            }
        });

        $form.find('[data-datafield="BillingAddressType"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.val() === 'OTHER') {
                FwFormField.enable($form.find('.billingaddress'));
            } else {
                FwFormField.disable($form.find('.billingaddress'));
            }
        });

        $form.find('[data-datafield="ShippingAddressType"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.val() === 'OTHER') {
                FwFormField.enable($form.find('.shippingaddress'));
            } else {
                FwFormField.disable($form.find('.shippingaddress'));
            }
        });

        this.events($form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form: any = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'CustomerId', uniqueids.CustomerId);
        FwModule.loadForm(this.Module, $form);

        $form.find('.contractSubModule').append(this.openContractBrowse($form));
        $form.find('.invoiceSubModule').append(this.openInvoiceBrowse($form));
        $form.find('.receiptSubModule').append(this.openReceiptBrowse($form));

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        let customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        let $browse;
        $browse = ContractController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ContractController.ActiveViewFields;
            request.uniqueids = {
                CustomerId: customerId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openInvoiceBrowse($form) {
        let customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        let $browse;
        $browse = InvoiceController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = InvoiceController.ActiveViewFields;
            request.uniqueids = {
                CustomerId: customerId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openReceiptBrowse($form) {
        let customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        let $browse;
        $browse = ReceiptController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ReceiptController.ActiveViewFields;
            request.uniqueids = {
                CustomerId: customerId
            };
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        // ----------
        var nameCustomerResaleGrid: string = 'CompanyResaleGrid';
        var $companyResaleGrid: any = $companyResaleGrid = $form.find('div[data-grid="' + nameCustomerResaleGrid + '"]');
        var $companyResaleGridControl: any = FwBrowse.loadGridFromTemplate(nameCustomerResaleGrid);

        $companyResaleGrid.empty().append($companyResaleGridControl);
        $companyResaleGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
            };
        });
        $companyResaleGridControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId')
        });
        FwBrowse.init($companyResaleGridControl);
        FwBrowse.renderRuntimeHtml($companyResaleGridControl);
        // ----------
        var nameCustomerNoteGrid: string = 'CustomerNoteGrid';
        var $customerNoteGrid: any = $customerNoteGrid = $form.find('div[data-grid="' + nameCustomerNoteGrid + '"]');
        var $customerNoteGridControl: any = FwBrowse.loadGridFromTemplate(nameCustomerNoteGrid);
        $customerNoteGrid.empty().append($customerNoteGridControl);
        $customerNoteGridControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CustomerId: FwFormField.getValueByDataField($form, 'CustomerId')
            }
        });
        FwBrowse.init($customerNoteGridControl);
        FwBrowse.renderRuntimeHtml($customerNoteGridControl);
        // ----------
        var nameCompanyTaxGrid: string = 'CompanyTaxOptionGrid'
        var $companyTaxGrid: any = $companyTaxGrid = $form.find('div[data-grid="' + nameCompanyTaxGrid + '"]');
        var $companyTaxControl: any = FwBrowse.loadGridFromTemplate(nameCompanyTaxGrid);
        $companyTaxGrid.empty().append($companyTaxControl);
        $companyTaxControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
            }
        });
        $companyTaxControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId');
        });
        FwBrowse.init($companyTaxControl);
        FwBrowse.renderRuntimeHtml($companyTaxControl);
        // ----------
        var nameCompanyContactGrid: string = 'CompanyContactGrid'
        var $companyContactGrid: any = $companyContactGrid = $form.find('div[data-grid="' + nameCompanyContactGrid + '"]');
        var $companyContactControl: any = FwBrowse.loadGridFromTemplate(nameCompanyContactGrid);
        $companyContactGrid.empty().append($companyContactControl);
        $companyContactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
            }
        });
        $companyContactControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId');
        });
        FwBrowse.init($companyContactControl);
        FwBrowse.renderRuntimeHtml($companyContactControl);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidateInsuranceVendor($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');

        request.uniqueids = {
            Insurance: true
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        var $customerResaleGrid: any = $form.find('[data-name="CompanyResaleGrid"]');
        var $customerNoteGrid: any = $form.find('[data-name="CustomerNoteGrid"]');
        var $companyTaxGrid: any = $form.find('[data-name="CompanyTaxOptionGrid"]');
        var $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');

        if (FwFormField.getValue($form, 'div[data-datafield="UseDiscountTemplate"]') === true) {
            FwFormField.enable($form.find('.discount-validation'));
        };

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            const tabname = jQuery(e.currentTarget).attr('id');
            const lastIndexOfTab = tabname.lastIndexOf('tab');
            const tabpage = `${tabname.substring(0, lastIndexOfTab)}tabpage${tabname.substring(lastIndexOfTab + 3)}`;

            const $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    const $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            const $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    const $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });

        this.addressTypeChange($form);
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.find('[data-name="CompanyTaxOptionGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                this.updateExternalInputsWithGridValues($tr);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
        //Billing Address Type Change
        $form.find('.billing_address_type').on('change', () => {
            this.addressTypeChange($form);
        });
        //Shipping Address Type Change
        $form.find('.shipping_address_type').on('change', () => {
            this.addressTypeChange($form);
        });
        //Customer Address Change
        $form.find('.customer_address input').on('change', () => {
            this.addressTypeChange($form);
        });
        // Insurance Vendor validation
        $form.find('div[data-datafield="InsuranceCompanyId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'InsuranceAgent', $tr.find('.field[data-formdatafield="PrimaryContact"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'InsuranceCompanyAddress1', $tr.find('.field[data-formdatafield="Address1"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'InsuranceCompanyAddress2', $tr.find('.field[data-formdatafield="Address2"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'InsuranceCompanyCity', $tr.find('.field[data-formdatafield="City"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'InsuranceCompanyState', $tr.find('.field[data-formdatafield="State"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'InsuranceCompanyZipCode', $tr.find('.field[data-formdatafield="ZipCode"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'InsuranceCompanyFax', $tr.find('.field[data-formdatafield="Fax"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'InsuranceCompanyPhone', $tr.find('.field[data-formdatafield="Phone"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="InsuranceCompanyCountryId"]', $tr.find('.field[data-formdatafield="CountryId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="Country"]').attr('data-originalvalue'));
        });
    }
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
          <div data-name="Customer" data-control="FwBrowse" data-type="Browse" id="CustomerBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="CustomerController" data-hasinactive="true">
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
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="customerform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Customer" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="CustomerController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="CustomerId"></div>
          <div id="customerform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="customertab" class="tab" data-tabpageid="customertabpage" data-caption="Customer"></div>
              <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="credittab" class="tab" data-tabpageid="credittabpage" data-caption="Credit"></div>
              <div data-type="tab" id="insurancetab" class="tab" data-tabpageid="insurancetabpage" data-caption="Insurance"></div>
              <div data-type="tab" id="taxtab" class="tab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
              <div data-type="tab" id="optionstab" class="tab" data-tabpageid="optionstabpage" data-caption="Options"></div>
              <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>

              <div data-type="tab" id="dealtab" class="tab submodule" data-tabpageid="dealtabpage" data-caption="Deal"></div>
              <div data-type="tab" id="quotetab" class="tab submodule" data-tabpageid="quotetabpage" data-caption="Quote"></div>
              <div data-type="tab" id="ordertab" class="tab submodule" data-tabpageid="ordertabpage" data-caption="Order"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="invoicetab" class="tab submodule" data-tabpageid="invoicetabpage" data-caption="Invoice"></div>
              <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contract"></div>
              <div data-type="tab" id="receipttab" class="tab submodule" data-tabpageid="receipttabpage" data-caption="Receipt"></div>
              <div data-type="tab" id="creditstab" class="tab submodule" data-tabpageid="creditstabpage" data-caption="Credits"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
              <!--
              <div data-type="tab" id="revenuetab" class="tab" data-tabpageid="revenuetabpage" data-caption="> Revenue"></div>
              <div data-type="tab" id="repairhistorytab" class="tab" data-tabpageid="repairhistorytabpage" data-caption="> Repair History"></div>
              <div data-type="tab" id="actsscenestab" class="tab" data-tabpageid="actsscenestabpage" data-caption="Acts / Scenes" style="background-color:#f5f5f5;"></div>
              <div data-type="tab" id="showscheduletab" class="tab" data-tabpageid="showscheduletabpage" data-caption="Show Schedule" style="background-color:#f5f5f5;"></div>
              <div data-type="tab" id="characterstab" class="tab" data-tabpageid="characterstabpage" data-caption="Characters" style="background-color:#f5f5f5;"></div>
              <div data-type="tab" id="wardrobetab" class="tab" data-tabpageid="wardrobetabpage" data-caption="Wardrobe" style="background-color:#f5f5f5;"></div>
              <div data-type="tab" id="brochuretab" class="tab" data-tabpageid="brochuretabpage" data-caption="Brochure" style="background-color:#f5f5f5;"></div>
              <div data-type="tab" id="projecttab" class="tab" data-tabpageid="projecttabpage" data-caption="Project"></div>
              <div data-type="tab" id="discountsdwtab" class="tab" data-tabpageid="discountsdwtabpage" data-caption="> Discounts &amp; D/W"></div>
              <div data-type="tab" id="flatpotab" class="tab" data-tabpageid="flatpotabpage" data-caption="Flat PO" style="background-color:#f5f5f5;"></div>
              <div data-type="tab" id="financechargestab" class="tab" data-tabpageid="financechargestabpage" data-caption="> Finance Charges"></div>
              <div data-type="tab" id="episodicscheduletab" class="tab" data-tabpageid="episodicscheduletabpage" data-caption="> Episodic Schedule"></div>
              <div data-type="tab" id="hiatusscheduletab" class="tab" data-tabpageid="hiatusscheduletabpage" data-caption="Hiatus Schedule" style="background-color:#f5f5f5;"></div>
              <div data-type="tab" id="orderprioritytab" class="tab" data-tabpageid="orderprioritytabpage" data-caption="> Order Priority"></div>
              <div data-type="tab" id="dealscheduletab" class="tab" data-tabpageid="dealscheduletabpage" data-caption="> Deal Schedule"></div>
              <div data-type="tab" id="ordergroupstab" class="tab" data-tabpageid="ordergroupstabpage" data-caption="> Order Groups"></div>
              <div data-type="tab" id="aragingtab" class="tab" data-tabpageid="aragingtabpage" data-caption="> A/R Aging"></div>
              <div data-type="tab" id="depletingdepositstab" class="tab" data-tabpageid="depletingdepositstabpage" data-caption="> Depleting Deposits"></div>
              <div data-type="tab" id="securitydeposittab" class="tab" data-tabpageid="securitydeposittabpage" data-caption="> Security Deposit"></div>
              <div data-type="tab" id="imagetab" class="tab" data-tabpageid="imagetabpage" data-caption="Image"></div>
              <div data-type="tab" id="webcontenttab" class="tab" data-tabpageid="webcontenttabpage" data-caption="> Web Content"></div>
              <div data-type="tab" id="eventtab" class="tab" data-tabpageid="eventtabpage" data-caption="Event"></div>
              <div data-type="tab" id="summarytab" class="tab" data-tabpageid="summarytabpage" data-caption="Summary"></div>
              <div data-type="tab" id="documentstab" class="tab" data-tabpageid="documentstabpage" data-caption="Documents"></div>
              -->
            </div>
            <div class="tabpages">
              <!-- CUSTOMER TAB -->
              <div data-type="tabpage" id="customertabpage" class="tabpage" data-tabid="customertab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 850px;">
                      <!-- Customer section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Customer" style="flex:1 1 450px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="CustomerNumber" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="flex:1 1 225px;" data-required="true"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="CustomerTypeId" data-displayfield="CustomerType" data-validationname="CustomerTypeValidation" data-required="true" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer Category" data-datafield="CustomerCategoryId" data-displayfield="CustomerCategory" data-validationname="CustomerCategoryValidation" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parent Customer" data-datafield="ParentCustomerId" data-displayfield="ParentCustomer" data-validationname="CustomerValidation" data-validationpeek="true" style="flex:1 1 350px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 225px;">
                      <!-- Status section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CustomerStatusId" data-displayfield="CustomerStatus" data-validationname="CustomerStatusValidation" data-required="true" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusAsOf" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Terms and Conditions on File" data-datafield="TermsAndConditionsOnFile" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <!-- Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="City" data-datafield="City" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="State" data-datafield="State" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield customer_address" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <!-- Contact Numbers section -->
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
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <!-- Web Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Internet">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" data-allcaps="false" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 275px;">
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
              <!-- DEAL TAB -->
              <div data-type="tabpage" id="dealtabpage" class="tabpage submodule deal-page" data-tabid="dealtab"></div>
              <!-- QUOTE TAB -->
              <div data-type="tabpage" id="quotetabpage" class="tabpage submodule quote-page" data-tabid="quotetab"></div>
              <!-- ORDER TAB -->
              <div data-type="tabpage" id="ordertabpage" class="tabpage submodule order-page" data-tabid="ordertab"></div>
              
<!--
              <##### Revenue tab #####>
              <div data-type="tabpage" id="revenuetabpage" class="tabpage" data-tabid="revenuetab">
                <div class="formpage">
                  <div class="formrow" style="width:100%;">
                    <div class="formcolumn" style="width:22%;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Period" style="width:100%;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="From" data-datafield="vendor.modifiedby" style="float:left;width:130px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="To" data-datafield="vendor.modifiedby" style="float:left;width:130px;"></div>
                        </div>
                      </div>
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
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Unassigned Revenue" data-datafield="vendor.modifiedby" data-enabled="false" style="float:left;width:11%;"></div>
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
              <##### Repair History tab #####>
              <div data-type="tabpage" id="repairhistorytabpage" class="tabpage" data-tabid="repairhistorytab">
                <div class="formpage">
                  <div class="formrow" style="width:100%;">
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
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="" data-datafield="vendor.primarycontactname" style="float:left;width:275px;"></div>
                      </div>
                      <div class="formrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Warehouses" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <##### Acts / Scenes tab #####>
              <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
                <div class="formpage">
                </div>
              </div>
              <##### Show Schedule tab #####>
              <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
                <div class="formpage">
                </div>
              </div>
              <##### Characters tab #####>
              <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
                <div class="formpage">
                </div>
              </div>
              <##### Wardrobe tab #####>
              <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
                <div class="formpage">
                </div>
              </div>
              <##### Brochure tab #####>
              <div data-type="tabpage" id="newtabpage" class="tabpage" data-tabid="newtab">
                <div class="formpage">
                </div>
              </div>
              <##### Project tab #####>
              <div data-type="tabpage" id="projecttabpage" class="tabpage" data-tabid="projecttab">
                <div class="formpage">
                  <div class="formrow" style="width:75%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Projects">
                      <div data-control="FwGrid" data-grid="CustomerNotes" data-securitycaption="Customer Notes">########## ADD MISSING PROJECT GRID ##########</div>
                    </div>
                  </div>
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:550px;">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All Locations" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show RentalWorks.NET Enabled" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                    </div>
                  </div>
                </div>
              </div>
  -->
              <!-- BILLING TAB -->
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
                <div class="flexpage">
                  <!-- Default Billing / Discounts & D/W -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Billing">
                        <!--<div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="customer.billingcycle" style="float:left;width:200px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Type" data-datafield="customer.paymenttype" style="float:left;width:200px;"></div>
                        </div>-->
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="flex:1 1 200px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Discounts &amp; D/W section -->
                    <div class="flexcolumn" style="flex:0 1 575px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Discounts &amp; D/W">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Discount Template" data-datafield="UseDiscountTemplate" style="flex:1 1 200px;margin-top:10px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield discount-validation" data-caption="Template" data-displayfield="DiscountTemplate" data-datafield="DiscountTemplateId" data-validationname="DiscountTemplateValidation" data-enabled="false" style="flex:1 1 300px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- Billing Address section -->
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Address" style="flex:0 1 800px;">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 150px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield billing_address_type" data-caption="" data-datafield="BillingAddressType" style="flex:1 1 125px;">
                              <div data-value="CUSTOMER" data-caption="Use Customer"></div>
                              <div data-value="OTHER" data-caption="Use Other"></div>
                            </div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:0 1 650px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billing_att1" data-caption="Attention 1" data-datafield="BillToAttention1" style="flex:1 1 300px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billing_att2" data-caption="Attention 2" data-datafield="BillToAttention2" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress billing_add1" data-caption="Address 1" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 300px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress billing_add2" data-caption="Address 2" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 300px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress billing_city" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 300px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress billing_state" data-caption="State" data-datafield="BillToState" data-enabled="false" style="flex:1 1 150px;"></div>
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress billing_zip" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield billingaddress billing_country" data-caption="Country" data-datafield="BillToCountryId" data-enabled="false" data-displayfield="BillToCountry" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

  <!--
              <##### Discounts &amp; D/W tab #####>
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
              <##### Flat PO tab #####>
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
              <##### Finance Charge History tab #####>
              <div data-type="tabpage" id="financechargestabpage" class="tabpage" data-tabid="financechargestab">
                <div class="formpage">
                  <div class="formrow" style="width:100%;">
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
              <##### Episodic Schedule tab #####>
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
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Episodic Schedule">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD EPISODIC SCHEDULE GRID ##########</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <##### Hiatus Schedule tab #####>
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
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Hiatus Schedule" style="width:50%;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD HIATUS SCHEDULE GRID ##########</div>
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">add Notes label for usage info...several rows of text here...</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <##### Order Priority tab #####>
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
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order Priority" style="width:65%;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD ORDER PRIORITY GRID ##########</div>
                      </div>
                    </div>
                  </div>
                  <div clsss="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:20%;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include All" data-datafield="" style="width:250px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <##### Deal Schedule tab #####>
              <div data-type="tabpage" id="dealscheduletabpage" class="tabpage" data-tabid="dealscheduletab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal Schedule" style="width:50%;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options">########## ADD DEAL SCHEDULE GRID ##########</div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <##### Order Groups tab #####>
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
              <##### A/R Aging tab #####>
              <div data-type="tabpage" id="aragingtabpage" class="tabpage" data-tabid="aragingtab">
                <div class="formpage">
                  <div class="formrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="A/R Aging" style="width:1150px;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Processed Only" data-datafield="Location" style="float:left;width:150px;margin-top:0px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Only Deals setup to Use Customer Credit" data-datafield="Location" style="float:right;width:275px;margin-top:0px;"></div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options" style="float:left;min-height:150px;min-width:1125px;border:1px solid silver;">########## ADD MISSING A/R AGING GRID ##########</div>
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
                          <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options" style="min-height:150px;min-width:700px;border:1px solid silver;">########## ADD MISSING QUOTES &amp; ORDERS GRID ##########</div>
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
              <##### Depleting Deposits / Credit Memos / Overpayments tab #####>
              <div data-type="tabpage" id="depletingdepositstabpage" class="tabpage" data-tabid="depletingdepositstab">
                <div class="formpage">
                  <div class="formrow" style="width:75%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depleting Deposits" style="width:100%;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options" style="float:left;min-height:350px;width:100%;border:1px solid silver;">##### ADD MISSING GRID FOR DEPLETING DEPOSITS / CREDIT MEMOS / OVERPAYMENTS #####</div>
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
              <##### Security Deposit tab #####>
              <div data-type="tabpage" id="securitydeposittabpage" class="tabpage" data-tabid="securitydeposittab">
                <div class="formpage">
                  <div class="formrow" style="width:75%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security Deposits" style="width:100%;">
                      <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options" style="float:left;min-height:350px;width:100%;border:1px solid silver;">##### ADD MISSING GRID FOR SECURITY DEPOSITS #####</div>
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
  -->
              <!-- CREDIT TAB -->
              <div data-type="tabpage" id="credittabpage" class="tabpage" data-tabid="credittab">
                <div class="flexpage">
                  <div class="flexrow" style="max-width:750px;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 200px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unlimited" data-datafield="CreditUnlimited" style="flex:0 1 175px;padding-left:10px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Application on File" data-datafield="CreditApplicationOnFile" style="flex:0 1 175px;padding-left:10px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:1 1 500px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CreditStatusId" data-displayfield="CreditStatus" data-validationname="CreditStatusValidation" data-required="true" style="flex:1 1 175px;"></div>
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Through Date" data-datafield="CreditStatusThroughDate" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Customer Amount" data-digits="0" data-datafield="CreditLimit" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Customer Available" data-datafield="CreditAvailable" data-enabled="false" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Customer Balance" data-datafield="CreditBalance" data-enabled="false" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:750px;">
                    <!-- Trade References section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Trade References">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="By" data-datafield="TradeReferencesVerifiedBy" style="flex:1 1 350px;"></div>
                        <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="On" data-datafield="TradeReferencesVerifiedOn" style="flex:1 1 150px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Verified" data-datafield="TradeReferencesVerified" style="flex:1 1 125px;padding-left:25px;margin-top:10px;"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow" style="max-width:750px;">
                    <!-- Responsible Party section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Responsible Party">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="CreditResponsibleParty" style="flex:1 1 350px;"></div>
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="On File" data-datafield="CreditResponsiblePartyOnFile" style="flex:1 1 125px;padding-left:25px;margin-top:10px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- CREDITS TAB -->
              <div data-type="tabpage" id="creditstabpage" class="tabpage submodule credits-page" data-tabid="creditstab"></div>
              <!-- INSURANCE TAB -->
              <div data-type="tabpage" id="insurancetabpage" class="tabpage" data-tabid="insurancetab">
                <div class="flexpage">
                  <!-- Insurance section -->
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance" style="flex:0 1 575px;">
                      <div class="flexrow">
                        <div class="flexcolumn" style="flex:0 1 225px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="InsuranceCertificationValidThrough" style="flex:0 1 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Certification on File" data-datafield="" style="flex:0 1 125px;margin-top:10px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn" style="flex:0 275px;">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield officelocation" data-caption="Liability" data-datafield="InsuranceCoverageLiability" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield vendorclass" data-caption="Deductible" data-datafield="InsuranceCoverageLiabilityDeductible" style="flex:1 1 125px;"></div>
                          </div>
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield vendorclass" data-caption="Property Value" data-datafield="InsuranceCoveragePropertyValue" style="flex:1 1 125px;"></div>
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield vendorclass" data-caption="Deductible" data-datafield="InsuranceCoveragePropertyValueDeductible" style="flex:1 1 125px;"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- Security Deposit / Vehicle Insurance -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 300px;">
                      <!-- Security Deposit section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security Deposit">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Total Amount" data-datafield="" data-enabled="false" style="flex:0 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 300px;">
                      <!-- Vehicle Insurance section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Vehicle Insurance">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Completed" data-datafield="VehicleInsuranceCertficationOnFile" style="flex:1 1 125px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <!-- Company section -->
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 600px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Company">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Name" data-datafield="InsuranceCompanyId" data-displayfield="InsuranceCompany" data-validationname="VendorValidation" data-formbeforevalidate="beforeValidateInsuranceVendor" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="InsuranceAgent" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="InsuranceCompanyAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="InsuranceCompanyAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InsuranceCompanyCity" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="InsuranceCompanyState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InsuranceCompanyZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="InsuranceCompanyCountryId" data-displayfield="InsuranceCompanyCountry" data-validationname="CountryValidation" data-enabled="false" style="flex:1 1 175px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InsuranceCompanyPhone" data-enabled="false" style="flex:1 1 125px"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="InsuranceCompanyFax" data-enabled="false" style="flex:1 1 125px;"></div>
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
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <!-- Tax section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Status">
                        <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="Taxable">
                          <div data-value="true" data-caption="Taxable"></div>
                          <div data-value="false" data-caption="Non-Taxable"></div>
                        </div>
                      </div>
                      <!-- Federal section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Federal">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State of Incorporation" data-datafield="TaxStateOfIncorporationId" data-displayfield="TaxStateOfIncorporation" data-validationname="StateValidation" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal Tax No." data-datafield="TaxFederalNo" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                      <!-- Non-Taxable section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Non-Taxable">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Certificate No." data-datafield="NonTaxableCertificateNo" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Year" data-datafield="NonTaxableYear" style="flex:1 1 75px;"></div>
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="NonTaxableCertificateValidThrough" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Certificate on File" data-datafield="NonTaxableCertificateOnFile" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 375px;">
                      <!-- Location Tax Option section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Location Tax Options">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Tax Option"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 375px;">
                      <!-- Tax Rates section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield TaxOption" data-caption="Tax Option" data-enabled="false" data-displayfield="" data-datafield="" style="flex:1 1 300px;"></div>
                          <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield RentalTaxRate1" data-caption="Rental %" data-datafield="" data-displayfield="" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield SalesTaxRate1" data-caption="Sales %" data-datafield="" data-displayfield="" data-enabled="false" style="flex:1 1 100px;"></div>
                          <div data-control="FwFormField" data-digits="4" data-type="percent" class="fwcontrol fwformfield LaborTaxRate1" data-caption="Labor %" data-datafield="" data-displayfield="" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                      </div>
                      <!-- Resale section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Resale">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyResaleGrid" data-securitycaption="Customer Resale"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- SHIPPING TAB -->
              <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="formcolumn" style="flex:0 1 200px;">
                      <!-- Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield shipping_address_type" data-caption="" data-datafield="ShippingAddressType">
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
                    <div class="flexcolumn" style="flex:0 1 600px;">
                      <!-- Default Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shipping_att shippingaddress" data-caption="Attention" data-datafield="ShipAttention" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shipping_add1 shippingaddress" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shipping_add2 shippingaddress" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shipping_city shippingaddress" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shipping_state shippingaddress" data-caption="State" data-datafield="ShipState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shipping_zip shippingaddress" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield shippingaddress" data-caption="Country" data-datafield="ShipCountryId" data-enabled="false" data-displayfield="ShipCountry" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 290px;">
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
                    <div class="flexcolumn">
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
     <!-- CONTRACT TAB -->
           <div data-type="tabpage" id="contracttabpage" class="tabpage contractSubModule" data-tabid="contracttab">
              </div>
     <!-- INVOICE TAB -->
           <div data-type="tabpage" id="invoicetabpage" class="tabpage invoiceSubModule" data-tabid="invoicetab">
              </div>
     <!-- RECEIPT TAB -->
           <div data-type="tabpage" id="receipttabpage" class="tabpage receiptSubModule" data-tabid="receipttab">
              </div>

              <!-- CONTACTS -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="flexpage">
                  <div class="flexcolumn" style="flex:1 1 100%;">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                      <div class="fwform-fieldrow">
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

              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Notes section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CustomerNoteGrid" data-securitycaption="Customer Note"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <!--<div class="formcolumn" style="width:auto;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Schedule Color" style="width:150px;">
                        <div class="fwcontrol fwcontainer fwform-fieldrow" data-control="FwContainer" data-type="fieldrow">
                          <div data-control="FwFormField" data-type="color" class="fwcontrol fwformfield" data-caption="Color" data-datafield="vendor.openeddate" style="width:100px;"></div>
                        </div>
                      </div>
                    </div>-->
                    <div class="flexcolumn">
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
              <!--
              <##### IMAGE TAB #####>
              <div data-type="tabpage" id="imagetabpage" class="tabpage" data-tabid="imagetab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Photo">
                      <div class="flexrow">
                        <div data-control="FwAppImage" data-type="" class="fwcontrol fwappimage" data-caption="" data-uniqueid1field="CustomerId" data-description="" data-rectype=""></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <##### RentalWorks.Net tab #####>
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
              <##### Web Content tab #####>
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
              <##### Events tab #####>
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
              <##### Summary tab #####>
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
              <##### Documents tab #####>
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
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Options" style="width:25%;">
                      <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Show All" data-datafield="Inactive" style="float:left;margin-left:15px;"></div>
                    </div>
                  </div>
                </div>
              </div>
        -->
            </div>
          </div>
`;
    }
    //----------------------------------------------------------------------------------------------
    addressTypeChange($form) {
        if (FwFormField.getValue($form, '.billing_address_type') === 'CUSTOMER') {
            // Values from Customer fields in general tab
            //FwFormField.setValue($form, '.billing_att1', "");
            //FwFormField.setValue($form, '.billing_att2', "");
            FwFormField.setValue($form, '.billing_add1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValue($form, '.billing_add2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValue($form, '.billing_city', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValue($form, '.billing_state', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValue($form, '.billing_zip', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValue($form, 'div[data-displayfield="BillToCountry"]', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }

        if (FwFormField.getValue($form, '.shipping_address_type') === 'CUSTOMER') {
            // Values from Customer fields in general tab
            FwFormField.enable($form.find('.shipping_att'));
            FwFormField.setValue($form, '.shipping_add1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValue($form, '.shipping_add2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValue($form, '.shipping_city', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValue($form, '.shipping_state', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValue($form, '.shipping_zip', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValue($form, 'div[data-displayfield="ShipCountry"]', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }

        if (FwFormField.getValue($form, '.billing_address_type') === 'OTHER') {
            FwFormField.enable($form.find('.billingaddress'));
        };

        if (FwFormField.getValue($form, '.shipping_address_type') === 'OTHER') {
            FwFormField.enable($form.find('.shippingaddress'));
        };
    }
    //----------------------------------------------------------------------------------------------
    openDealBrowse($form) {
        const $browse = DealController.openBrowse();

        $browse.data('ondatabind', request => {
            request.uniqueids = {
                CustomerId: $form.find('[data-datafield="CustomerId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openQuoteBrowse($form) {
        const $browse = QuoteController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = QuoteController.ActiveViewFields;
            request.uniqueids = {
                CustomerId: $form.find('[data-datafield="CustomerId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openOrderBrowse($form) {
        const $browse = OrderController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = OrderController.ActiveViewFields;
            request.uniqueids = {
                CustomerId: $form.find('[data-datafield="CustomerId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openCustomerCreditBrowse($form) {
        const $browse = CustomerCreditController.openBrowse();

        $browse.data('ondatabind', request => {
            request.uniqueids = {
                CustomerId: $form.find('[data-datafield="CustomerId"] input.fwformfield-value').val()
            }
        });
        FwBrowse.databind($browse);
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
}
var CustomerController = new Customer();