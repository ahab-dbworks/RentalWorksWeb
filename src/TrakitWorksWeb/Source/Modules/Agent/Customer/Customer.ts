routes.push({ pattern: /^module\/customer$/, action: function (match: RegExpExecArray) { return CustomerController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customer\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return CustomerController.getModuleScreen(filter); } });

class Customer {
    Module: string = 'Customer';
    apiurl: string = 'api/v1/customer';
    caption: string = Constants.Modules.Agent.children.Customer.caption;
	nav: string = Constants.Modules.Agent.children.Customer.nav;
	id: string = Constants.Modules.Agent.children.Customer.id;
    thisModule: Customer;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: any = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                var datafields = filter.datafield.split('%20');
                for (let i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find(`div[data-browsedatafield="${filter.datafield}"]`).find('input').val(filter.search);
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

        let hasDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
        if (!hasDefaults) {
            FwAppData.apiMethod(true, 'GET', `api/v1/control/1`, null, FwServices.defaultTimeout, function onSuccess(res) {
                let ControlDefaults = {
                    defaultdealstatusid: res.DefaultDealStatusId
                    , defaultdealstatus: res.DefaultDealStatus
                    , defaultcustomerstatusid: res.DefaultCustomerStatusId
                    , defaultcustomerstatus: res.DefaultCustomerStatus
                    , defaultdealbillingcycleid: res.DefaultDealBillingCycleId
                    , defaultdealbillingcycle: res.DefaultDealBillingCycle
                }
                sessionStorage.setItem('controldefaults', JSON.stringify(ControlDefaults));
            }, null, null);
        }

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
        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            if ($form.data('mode') !== 'NEW') {
                const tabpageid = jQuery(e.currentTarget).data('tabpageid');

                const $gridControls = $form.find(`#${tabpageid} [data-type="Grid"]`);
                if ($gridControls.length > 0) {
                    for (let i = 0; i < $gridControls.length; i++) {
                        const $gridcontrol = jQuery($gridControls[i]);
                        FwBrowse.search($gridcontrol);
                    }
                }

                const $browseControls = $form.find(`#${tabpageid} [data-type="Browse"]`);
                if ($browseControls.length > 0) {
                    for (let i = 0; i < $browseControls.length; i++) {
                        const $browseControl = jQuery($browseControls[i]);
                        FwBrowse.search($browseControl);
                    }
                }
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
        //$form.find('.invoiceSubModule').append(this.openInvoiceBrowse($form));
        //$form.find('.receiptSubModule').append(this.openReceiptBrowse($form));

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
    //openInvoiceBrowse($form) {
    //    let customerId = FwFormField.getValueByDataField($form, 'CustomerId');
    //    let $browse;
    //    $browse = InvoiceController.openBrowse();
    //    $browse.data('ondatabind', function (request) {
    //        request.activeviewfields = InvoiceController.ActiveViewFields;
    //        request.uniqueids = {
    //            CustomerId: customerId
    //        };
    //    });
    //    return $browse;
    //}
    //---------------------------------------------------------------------------------------------
    //openReceiptBrowse($form) {
    //    let customerId = FwFormField.getValueByDataField($form, 'CustomerId');
    //    let $browse;
    //    $browse = ReceiptController.openBrowse();
    //    $browse.data('ondatabind', function (request) {
    //        request.activeviewfields = ReceiptController.ActiveViewFields;
    //        request.uniqueids = {
    //            CustomerId: customerId
    //        };
    //    });
    //    return $browse;
    //}
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    loadAudit($form: any) {
        var uniqueid: string = FwFormField.getValueByDataField($form, 'CustomerId');
        FwModule.loadAudit($form, uniqueid);
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
        var $browse;

        $browse = DealController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.uniqueids = {
                CustomerId: $form.find('[data-datafield="CustomerId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openQuoteBrowse($form) {
        var $browse;
        $browse = QuoteController.openBrowse();

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
        var $browse;
        $browse = OrderController.openBrowse();

        $browse.data('ondatabind', function (request) {
            request.activeviewfields = OrderController.ActiveViewFields;
            request.uniqueids = {
                CustomerId: $form.find('[data-datafield="CustomerId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
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
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="customerform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Customer" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="CustomerController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-saveorder="1" data-caption="" data-datafield="CustomerId"></div>
          <div id="customerform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="customertab" class="tab" data-tabpageid="customertabpage" data-caption="Customer"></div>
              <div data-type="tab" id="dealtab" class="tab submodule" data-tabpageid="dealtabpage" data-caption="Deal"></div>
              <div data-type="tab" id="orderhistorytab" class="tab" data-tabpageid="orderhistorytabpage" data-caption="Order History"></div>
              <div data-type="tab" id="repairhistorytab" class="tab" data-tabpageid="repairhistorytabpage" data-caption="Repair History"></div>
              <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
              <div data-type="tab" id="documentstab" class="tab" data-tabpageid="documentstabpage" data-caption="Documents"></div>-->
            </div>

            <div class="tabpages">
              <!-- ##### Customer tab ##### -->
              <div data-type="tabpage" id="customertabpage" class="tabpage" data-tabid="customertab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <!-- Customer section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="Customer" data-noduplicate="true" style="flex:1 1 350px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="No." data-datafield="CustomerNumber" data-noduplicate="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="flex:1 1 225px;" data-required="true"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer Category" data-datafield="CustomerCategoryId" data-displayfield="CustomerCategory" data-validationname="CustomerCategoryValidation" style="flex:1 1 225px;"></div>
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="CustomerTypeId" data-displayfield="CustomerType" data-validationname="CustomerTypeValidation" data-required="true" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 225px;">
                      <!-- Status section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CustomerStatusId" data-displayfield="CustomerStatus" data-validationname="CustomerStatusValidation" data-required="true" style="width:175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusAsOf" data-enabled="false" style="float:left;width:100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Terms and Conditions on File" data-datafield="TermsAndConditionsOnFile" style="float:left;width:225px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 550px;">
                      <!-- Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="City" style="flex:1 1 250px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="State" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="width:175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <!-- Contact Numbers section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Phone Toll-Free" data-datafield="PhoneTollFree" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phone" class="fwcontrol fwformfield" data-caption="Other" data-datafield="OtherPhone" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" style="flex:1 1 250px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### end Customer tab ##### -->

              <!-- ##### Deal tab ##### -->
              <div data-type="tabpage" id="dealtabpage" class="tabpage submodule deal-page" data-tabid="dealtab"></div>

              <!-- ##### Order History tab ##### -->
              <div data-type="tabpage" id="orderhistorytabpage" class="tabpage" data-tabid="orderhistorytab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order History">
                      <div class="flexrow">##### ADD MISSING ORDER HISTORY GRID #####
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"></div>
                      </div>
                    </div>
                  </div>
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order History Total" style="flex:0 0 175px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="vendor.modifiedby" data-enabled="false" style="flex:1 1 125px;"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### end Order History tab ##### -->

              <!-- ##### Repair History tab ##### -->
              <div data-type="tabpage" id="repairhistorytabpage" class="tabpage" data-tabid="repairhistorytab">
                <div class="flexpage">
                  <div class="flexrow" style="flex:1 1 500px">
                    <!-- Location Tax Options section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Repair History">
                      <div class="flexrow">##### ADD MISSING REPAIR HISTORY GRID #####
                        <div data-control="FwGrid" data-grid="VendorLocationTaxOptions" data-securitycaption="Location Tax Options"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### end Repair History tab ##### -->

              <!-- ##### Shipping tab ##### -->
              <div data-type="tabpage" id="shippingtabpage" class="tabpage" data-tabid="shippingtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 200px;">
                      <!-- Shipping Address section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="radio" class="fwcontrol fwformfield" data-caption="" data-datafield="ShippingAddressType" style="flex:0 1 225px;">
                            <div data-value="CUSTOMER" data-caption="Use Customer"></div>
                            <div data-value="OTHER" data-caption="Use Other"></div>
                          </div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 550px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Attention" data-datafield="ShipAttention" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="flex:1 1 275px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="State" data-datafield="ShipState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield shippingaddress" data-caption="Country" data-datafield="CountryId" data-enabled="false" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### end Shipping tab ##### -->

              <!-- ##### Contacts tab ##### -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contacts">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Vendor Contacts"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### end Contacts tab ##### -->

              <!-- ##### Notes tab ##### -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow" style="flex:1 1 750px;">
                    <!-- Notes section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Notes">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CustomerNoteGrid" data-securitycaption="Customer Note"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### end Notes tab ##### -->

              <!-- ##### Documents tab ##### -->
              <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
                <div class="flexpage">
                  <div class="flexrow" style="flex:1 1 120px;">
                    <!-- Notes section -->
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Documents">
                      <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CustomerDocumentsGrid" data-securitycaption="Customer Document"></div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <!-- ##### end Documents tab ##### -->
            </div>
          </div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
}

var CustomerController = new Customer();
