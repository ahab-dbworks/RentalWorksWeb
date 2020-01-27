class Customer {
    Module: string = 'Customer';
    apiurl: string = 'api/v1/customer';
    caption: string = Constants.Modules.Agent.children.Customer.caption;
    nav: string = Constants.Modules.Agent.children.Customer.nav;
    id: string = Constants.Modules.Agent.children.Customer.id;
    thisModule: Customer;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse: any = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
            const chartFilters = JSON.parse(sessionStorage.getItem('chartfilter'));
            if (!chartFilters) {
                FwBrowse.databind($browse);
                FwBrowse.screenload($browse);
            }
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

        let userassignedcustomerno = JSON.parse(sessionStorage.getItem('controldefaults')).userassignedcustomernumber;
        if (userassignedcustomerno) {
            FwFormField.enable($form.find('[data-datafield="CustomerNumber"]'));
            $form.find('[data-datafield="CustomerNumber"]').attr(`data-required`, `true`);
        }
        else {
            FwFormField.disable($form.find('[data-datafield="CustomerNumber"]'));
            $form.find('[data-datafield="CustomerNumber"]').attr(`data-required`, `false`);
        }

        //Toggle Buttons on Billing tab
        FwFormField.loadItems($form.find('div[data-datafield="BillingAddressType"]'), [
            { value: 'CUSTOMER', caption: 'Customer', checked: true },
            { value: 'OTHER', caption: 'Other' }
        ]);
        //Toggle Buttons on Tax tab
        FwFormField.loadItems($form.find('div[data-datafield="Taxable"]'), [
            { value: 'TRUE', caption: 'Taxable', checked: true },
            { value: 'FALSE', caption: 'Non-Taxable' }
        ]);
        //Toggle Buttons on Shipping tab
        FwFormField.loadItems($form.find('div[data-datafield="ShippingAddressType"]'), [
            { value: 'CUSTOMER', caption: 'Customer', checked: true },
            { value: 'OTHER', caption: 'Other' }
        ]);
        // SUBMODULES
        // Deal  submodule
        const $submoduleDealBrowse = this.openDealBrowse($form);
        $form.find('.deal-page').append($submoduleDealBrowse);
        $submoduleDealBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleDealBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            if ($form.attr('data-mode') !== 'NEW') {
                const dealFormData: any = {};
                dealFormData.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
                dealFormData.Customer = FwFormField.getValueByDataField($form, 'Customer');
                dealFormData.Address1 = FwFormField.getValueByDataField($form, 'Address1');
                dealFormData.Address2 = FwFormField.getValueByDataField($form, 'Address2');
                dealFormData.City = FwFormField.getValueByDataField($form, 'City');
                dealFormData.State = FwFormField.getValueByDataField($form, 'State');
                dealFormData.ZipCode = FwFormField.getValueByDataField($form, 'ZipCode');
                dealFormData.CountryId = FwFormField.getValueByDataField($form, 'CountryId');
                dealFormData.Country = FwFormField.getTextByDataField($form, 'CountryId');
                dealFormData.Phone = FwFormField.getValueByDataField($form, 'Phone');

                const $browse = jQuery(this).closest('.fwbrowse');
                const controller = $browse.attr('data-controller');
                if (typeof window[controller] !== 'object') throw `Missing javascript module: ${controller}`;
                if (typeof window[controller]['openForm'] !== 'function') throw `Missing javascript function: ${controller}.openForm`;
                const $dealForm = window[controller]['openForm']('NEW', dealFormData);
                FwModule.openSubModuleTab($browse, $dealForm);
            } else {
                FwNotification.renderNotification('WARNING', 'Save the record first.')
            }
        });
        // Quote submodule
        const $submoduleQuoteBrowse = this.openQuoteBrowse($form);
        $form.find('.quote-page').append($submoduleQuoteBrowse);
        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            if ($form.attr('data-mode') !== 'NEW') {
                const quoteFormData: any = {};
                quoteFormData.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
                quoteFormData.Customer = FwFormField.getValueByDataField($form, 'Customer');

                const $browse = jQuery(this).closest('.fwbrowse');
                const controller = $browse.attr('data-controller');
                if (typeof window[controller] !== 'object') throw `Missing javascript module: ${controller}`;
                if (typeof window[controller]['openForm'] !== 'function') throw `Missing javascript function: ${controller}.openForm`;
                const $quoteForm = window[controller]['openForm']('NEW', quoteFormData);
                FwModule.openSubModuleTab($browse, $quoteForm);
            } else {
                FwNotification.renderNotification('WARNING', 'Save the record first.')
            }
        });
        // Order submodule 
        const $submoduleOrderBrowse = this.openOrderBrowse($form);
        $form.find('.order-page').append($submoduleOrderBrowse);
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            if ($form.attr('data-mode') !== 'NEW') {
                const orderFormData: any = {};
                orderFormData.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
                orderFormData.Customer = FwFormField.getValueByDataField($form, 'Customer');

                const $browse = jQuery(this).closest('.fwbrowse');
                const controller = $browse.attr('data-controller');
                if (typeof window[controller] !== 'object') throw `Missing javascript module: ${controller}`;
                if (typeof window[controller]['openForm'] !== 'function') throw `Missing javascript function: ${controller}.openForm`;
                const $orderForm = window[controller]['openForm']('NEW', orderFormData);
                FwModule.openSubModuleTab($browse, $orderForm);
            } else {
                FwNotification.renderNotification('WARNING', 'Save the record first.')
            }
        });
        // Customer Credit submodule
        const $submoduleCustomerCreditBrowse = this.openCustomerCreditBrowse($form);
        $form.find('.credits-page').append($submoduleCustomerCreditBrowse);

        $form.find('[data-datafield="UseDiscountTemplate"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.discount-validation'));
            } else {
                FwFormField.disable($form.find('.discount-validation'));
            }
        });
        $form.find('[data-datafield="CreditUnlimited"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('div[data-datafield="CreditLimit"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="CreditLimit"]'));
            }
        });

        $form.find('[data-datafield="DisableQuoteOrderActivity"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.quote-order [data-type="checkbox"]'));
            } else {
                FwFormField.disable($form.find('.quote-order [data-type="checkbox"]'));
            }
        });

        $form.find('[data-datafield="BillingAddressType"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.val() === 'OTHER') {
                FwFormField.enable($form.find('.billingaddress'));
            } else {
                FwFormField.disable($form.find('.billingaddress'));
            }
        });

        $form.find('[data-datafield="ShippingAddressType"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
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
        const $form: any = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'CustomerId', uniqueids.CustomerId);
        FwModule.loadForm(this.Module, $form);

        $form.find('.contractSubModule').append(this.openContractBrowse($form));
        $form.find('.invoiceSubModule').append(this.openInvoiceBrowse($form));
        $form.find('.receiptSubModule').append(this.openReceiptBrowse($form));

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    openContractBrowse($form) {
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const $browse = ContractController.openBrowse();
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
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const $browse = InvoiceController.openBrowse();
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
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        const $browse = ReceiptController.openBrowse();
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
        //Company Resale Grid
        //var nameCustomerResaleGrid: string = 'CompanyResaleGrid';
        //var $companyResaleGrid: any = $companyResaleGrid = $form.find('div[data-grid="' + nameCustomerResaleGrid + '"]');
        //var $companyResaleGridControl: any = FwBrowse.loadGridFromTemplate(nameCustomerResaleGrid);

        //$companyResaleGrid.empty().append($companyResaleGridControl);
        //$companyResaleGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
        //    };
        //});
        //$companyResaleGridControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId')
        //});
        //FwBrowse.init($companyResaleGridControl);
        //FwBrowse.renderRuntimeHtml($companyResaleGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'CompanyResaleGrid',
            gridSecurityId: 'k48X9sulRpmb',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId');
            }
        });
        // ----------
        // Customer Note Grid
        //var nameCustomerNoteGrid: string = 'CustomerNoteGrid';
        //var $customerNoteGrid: any = $customerNoteGrid = $form.find('div[data-grid="' + nameCustomerNoteGrid + '"]');
        //var $customerNoteGridControl: any = FwBrowse.loadGridFromTemplate(nameCustomerNoteGrid);
        //$customerNoteGrid.empty().append($customerNoteGridControl);
        //$customerNoteGridControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CustomerId: FwFormField.getValueByDataField($form, 'CustomerId')
        //    }
        //});
        //FwBrowse.init($customerNoteGridControl);
        //FwBrowse.renderRuntimeHtml($customerNoteGridControl);

        FwBrowse.renderGrid({
            nameGrid: 'CustomerNoteGrid',
            gridSecurityId: '6AHfzr9WBEW9',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CustomerId: FwFormField.getValueByDataField($form, 'CustomerId')
                };
            },
            beforeSave: (request: any) => {
                request.CustomerId = FwFormField.getValueByDataField($form, 'CustomerId');
            }
        });


        // ----------
        // Tax Option Grid
        //var nameCompanyTaxGrid: string = 'CompanyTaxOptionGrid'
        //var $companyTaxGrid: any = $companyTaxGrid = $form.find('div[data-grid="' + nameCompanyTaxGrid + '"]');
        //var $companyTaxControl: any = FwBrowse.loadGridFromTemplate(nameCompanyTaxGrid);
        //$companyTaxGrid.empty().append($companyTaxControl);
        //$companyTaxControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
        //    }
        //});
        //$companyTaxControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId');
        //});
        //FwBrowse.init($companyTaxControl);
        //FwBrowse.renderRuntimeHtml($companyTaxControl);

        FwBrowse.renderGrid({
            nameGrid: 'CompanyTaxOptionGrid',
            gridSecurityId: 'B9CzDEmYe1Zf',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {
                options.hasNew = false;
                options.hasDelete = false;
            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId');
            }
        });
        // ----------
        // Company Contact Grid
        //var nameCompanyContactGrid: string = 'CompanyContactGrid'
        //var $companyContactGrid: any = $companyContactGrid = $form.find('div[data-grid="' + nameCompanyContactGrid + '"]');
        //var $companyContactControl: any = FwBrowse.loadGridFromTemplate(nameCompanyContactGrid);
        //$companyContactGrid.empty().append($companyContactControl);
        //$companyContactControl.data('ondatabind', function (request) {
        //    request.uniqueids = {
        //        CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
        //    }
        //});
        //$companyContactControl.data('beforesave', function (request) {
        //    request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId');
        //});
        //FwBrowse.init($companyContactControl);
        //FwBrowse.renderRuntimeHtml($companyContactControl);

        FwBrowse.renderGrid({
            nameGrid: 'CompanyContactGrid',
            gridSecurityId: 'gQHuhVDA5Do2',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'CustomerId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'CustomerId');
            },
        });

        // Documents Grid
        FwBrowse.renderGrid({
            nameGrid: 'AppDocumentGrid',
            gridSecurityId: '',
            moduleSecurityId: this.id,
            $form: $form,
            pageSize: 10,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    uniqueid1: FwFormField.getValueByDataField($form, 'CustomerId')
                };
            },
            beforeSave: (request: any) => {
                request.uniqueid1 = FwFormField.getValueByDataField($form, 'CustomerId');
            },
            getTemplate: () => {
                return FwAppDocumentGrid.getTemplate();
            }
        });
    }

    //----------------------------------------------------------------------------------------------
    //beforeValidateInsuranceVendor($browse, $grid, request) {
    //    var $form;
    //    $form = $grid.closest('.fwform');

    //    request.uniqueids = {
    //        Insurance: true
    //    }
    //}
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        if (FwFormField.getValue($form, 'div[data-datafield="UseDiscountTemplate"]') === true) {
            FwFormField.enable($form.find('.discount-validation'));
        }
        if (FwFormField.getValue($form, 'div[data-datafield="CreditUnlimited"]') === true) {
            FwFormField.disable($form.find('div[data-datafield="CreditLimit"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="CreditLimit"]'));
        }

        this.toggleOptionsTabIfExcludeQuote($form, FwFormField.getValueByDataField($form, 'DisableQuoteOrderActivity'));

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
        $form.find('div[data-datafield="BillingAddressType"]').on('change', () => {
            this.addressTypeChange($form);
        });
        //Shipping Address Type Change
        $form.find('div[data-datafield="ShippingAddressType"]').on('change', () => {
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
        $form.on('change', '.exlude_quote input[type=checkbox]', e => {
            const isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleOptionsTabIfExcludeQuote($form, isChecked);
        });
    }
    //----------------------------------------------------------------------------------------------
    updateExternalInputsWithGridValues($tr: JQuery): void {
        $tr.find('.column > .field').each((i, e) => {
            let $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
            if (value === undefined || null) {
                jQuery(`.${id}`).find(':input').val(0);
            } else {
                jQuery(`.${id}`).find(':input').val(value);
            }
        });
        const taxOption = $tr.find('.field[data-browsedatafield="TaxOptionId"]').attr('data-originaltext');
        jQuery('.TaxOption').find(':input').val(taxOption);
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        switch (datafield) {
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelocation`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'CustomerTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomertype`);
                break;
            case 'CustomerCategoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomercategory`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
            case 'CustomerStatusId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomerstatus`);
                break;
            case 'ParentCustomerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateparentcustomer`);
                break;
            case 'PaymentTermsId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymentterms`);
                break;
            case 'CreditStatusId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecreditstatus`);
                break;
            case 'InsuranceCompanyId':
                request.uniqueids = {
                    Insurance: true
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinsurancecompany`);
                break;
            case 'TaxStateOfIncorporationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxstateofincorporation`);
                break;
        }
    }
    //--------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
          <div data-name="Customer" data-control="FwBrowse" data-type="Browse" id="CustomerBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="CustomerController" data-hasinactive="true">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="CustomerId" data-browsedatatype="key" ></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Customer Name" data-datafield="Customer" data-browsedatatype="text" data-sort="asc"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Customer Number" data-datafield="CustomerNumber" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Type" data-datafield="CustomerType" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Status" data-datafield="CustomerStatus" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Address 1" data-datafield="Address1" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="City" data-datafield="City" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="State" data-datafield="State" data-browsedatatype="text" data-sort="off"></div>
          </div>
          <div class="column" data-width="auto" data-visible="true">
            <div class="field" data-caption="Zip/Postal" data-datafield="ZipCode" data-browsedatatype="text" data-sort="off"></div>
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
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="credittab" class="tab" data-tabpageid="credittabpage" data-caption="Credit"></div>
              <div data-type="tab" id="insurancetab" class="tab" data-tabpageid="insurancetabpage" data-caption="Insurance"></div>
              <div data-type="tab" id="taxtab" class="tab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
              <div data-type="tab" id="optionstab" class="tab" data-tabpageid="optionstabpage" data-caption="Options"></div>
              <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
              <div data-type="tab" id="dealtab" class="tab submodule" data-tabpageid="dealtabpage" data-caption="Deals"></div>
              <div data-type="tab" id="quotetab" class="tab submodule" data-tabpageid="quotetabpage" data-caption="Quotes"></div>
              <div data-type="tab" id="ordertab" class="tab submodule" data-tabpageid="ordertabpage" data-caption="Orders"></div>
              <div data-type="tab" id="contracttab" class="tab submodule" data-tabpageid="contracttabpage" data-caption="Contracts"></div>
              <div data-type="tab" id="invoicetab" class="tab submodule" data-tabpageid="invoicetabpage" data-caption="Invoices"></div>
              <div data-type="tab" id="receipttab" class="tab submodule" data-tabpageid="receipttabpage" data-caption="Receipts"></div>
              <div data-type="tab" id="creditstab" class="tab submodule" data-tabpageid="creditstabpage" data-caption="Credits"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
              <!-- CUSTOMER TAB -->
              <div data-type="tabpage" id="customertabpage" class="tabpage" data-tabid="customertab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Name" data-datafield="Customer" data-required="true" style="flex:1 1 450px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Number" data-datafield="CustomerNumber" data-required="true" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" style="flex:1 1 225px;" data-required="true"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="CustomerTypeId" data-displayfield="CustomerType" data-validationname="CustomerTypeValidation" data-required="true" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Category" data-datafield="CustomerCategoryId" data-displayfield="CustomerCategory" data-validationname="CustomerCategoryValidation" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Parent Customer" data-datafield="ParentCustomerId" data-displayfield="ParentCustomer" data-validationname="CustomerValidation" data-validationpeek="true" style="flex:1 1 350px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Web Address" data-datafield="WebAddress" data-allcaps="false" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Address section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="City" data-datafield="City" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="State" data-datafield="State" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield customer_address" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield customer_address" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
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
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 250px;">
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
                </div>
              </div>

              <!-- CONTACTS -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Customer Contacts"></div>
                  </div>
                </div>
              </div>
              
              <!-- BILLING TAB -->
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Billing Address section -->
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Default Address" data-datafield="BillingAddressType"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 1" data-datafield="BillToAttention1" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 2" data-datafield="BillToAttention2" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Address 1" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Address 2" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="State" data-datafield="BillToState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield billingaddress" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield billingaddress" data-caption="Country" data-datafield="BillToCountryId" data-enabled="false" data-displayfield="BillToCountry" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <!-- Billing Options section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Options">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="flex:1 1 200px;"></div>
                        </div>
                      </div>
                      <!-- Discount Template section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Discount Template">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Discount Template" data-datafield="UseDiscountTemplate" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield discount-validation" data-caption="Template" data-displayfield="DiscountTemplate" data-datafield="DiscountTemplateId" data-validationname="DiscountTemplateValidation" data-enabled="false" style="flex:1 1 300px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              
              <!-- CREDIT TAB -->
              <div data-type="tabpage" id="credittabpage" class="tabpage" data-tabid="credittab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Credit section -->
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CreditStatusId" data-displayfield="CreditStatus" data-validationname="CreditStatusValidation" data-required="true" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Through Date" data-datafield="CreditStatusThroughDate" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount" data-dontsavedecimal="true" data-datafield="CreditLimit" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Customer A/R Balance" data-datafield="CreditBalance" data-enabled="false" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Available" data-datafield="CreditAvailable" data-enabled="false" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Credit Application on File" data-datafield="CreditApplicationOnFile" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unlimited Credit" data-datafield="CreditUnlimited" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <!-- Trade References section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Trade References">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Verified" data-datafield="TradeReferencesVerified" style="flex:0 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date Verified" data-datafield="TradeReferencesVerifiedOn" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Verified By" data-datafield="TradeReferencesVerifiedBy" style="flex:1 1 350px;"></div>
                        </div>
                      </div>
                      <!-- Responsible Party section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Responsible Party">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="On File" data-datafield="CreditResponsiblePartyOnFile" style="flex:0 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Name" data-datafield="CreditResponsibleParty" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- INSURANCE TAB -->
              <div data-type="tabpage" id="insurancetabpage" class="tabpage" data-tabid="insurancetab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <!-- Insurance section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="InsuranceCertificationValidThrough" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield officelocation" data-caption="Liability Amount" data-datafield="InsuranceCoverageLiability" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield vendorclass" data-caption="Liability Deductible" data-datafield="InsuranceCoverageLiabilityDeductible" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield vendorclass" data-caption="Property Value Amount" data-datafield="InsuranceCoveragePropertyValue" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield vendorclass" data-caption="Property Value Deductible" data-datafield="InsuranceCoveragePropertyValueDeductible" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Insurance Certification on File" data-datafield="" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Vehicle Insurance on File" data-datafield="VehicleInsuranceCertficationOnFile" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                      <!-- Security Deposits section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security Deposits">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Total Amount" data-datafield="" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div class="fwformcontrol additems" data-type="button" style="flex:1 1 100px;margin:15px 5px 0px 5px;" data-enabled="false">Security Deposits</div>
                        </div>
                      </div>
                    </div>
                    <!-- Insurance Vendor section -->
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance Vendor">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Name" data-datafield="InsuranceCompanyId" data-displayfield="InsuranceCompany" data-validationname="VendorValidation" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="InsuranceAgent" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="InsuranceCompanyAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="InsuranceCompanyAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InsuranceCompanyCity" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="InsuranceCompanyState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InsuranceCompanyZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="InsuranceCompanyCountryId" data-displayfield="InsuranceCompanyCountry" data-validationname="CountryValidation" data-enabled="false" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
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
                      <!-- Tax Status section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Option">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="" data-datafield="Taxable"></div>
                        </div>
                      </div>
                      <!-- Non-Taxable section --> 
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Non-Taxable">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Certificate No." data-datafield="NonTaxableCertificateNo" data-enabled="false" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Year" data-datafield="NonTaxableYear" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="NonTaxableCertificateValidThrough" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Tax Certificate on File" data-datafield="NonTaxableCertificateOnFile" data-enabled="false" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                      <!-- Federal section --> 
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Federal Tax">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State of Incorporation" data-datafield="TaxStateOfIncorporationId" data-displayfield="TaxStateOfIncorporation" data-validationname="StateValidation" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal Tax No." data-datafield="TaxFederalNo" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 600px;">
                      <!-- Location Tax Rates section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Tax Option"></div>
                        </div>
                      </div>
                      <!-- Resale section --> 
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Resale Certificates" style="margin-top:-15px;">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyResaleGrid" data-securitycaption="Customer Resale"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- OPTIONS TAB -->
              <div data-type="tabpage" id="optionstabpage" class="tabpage" data-tabid="optionstab">
                <div class="formpage">
                  <div class="flexrow">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Quote / Order Activity" style="flex:0 1 375px;">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield exlude_quote" data-caption="Exclude Quote / Order Activity" data-datafield="DisableQuoteOrderActivity" data-enabled="true" style="flex:1 1 250px;"></div>
                      </div>
                      <div class="flexrow" style="border-top:1px solid silver;border-bottom:1px solid silver;">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Rental" data-datafield="DisableRental" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Rental" data-datafield="DisableSubRental" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow" style="border-bottom:1px solid silver;">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sales" data-datafield="DisableSales" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Sales" data-datafield="DisableSubSale" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow" style="border-bottom:1px solid silver;">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Facilities" data-datafield="DisableFacilities" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow" style="border-bottom:1px solid silver;">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Transportation" data-datafield="DisableTransportation" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow" style="border-bottom:1px solid silver;">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Crew" data-datafield="DisableLabor" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Crew" data-datafield="DisableSubLabor" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow" style="border-bottom:1px solid silver;">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Miscellaneous" data-datafield="DisableMisc" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Sub-Miscellaneous" data-datafield="DisableSubMisc" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
                        </div>
                      </div>
                      <div class="flexrow" style="border-bottom:1px solid silver;">
                        <div class="flexcolumn">
                          <div class="flexrow">
                            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Used Sale" data-datafield="DisableRentalSale" data-enabled="false" style="flex:1 1 225px;margin-left:18px;"></div>
                          </div>
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
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:0 1 425px;"">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Default Address" data-datafield="ShippingAddressType"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Attention" data-datafield="ShipAttention" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="State" data-datafield="ShipState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield shippingaddress" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield shippingaddress" data-caption="Country" data-datafield="ShipCountryId" data-enabled="false" data-displayfield="ShipCountry" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- DEAL TAB -->
              <div data-type="tabpage" id="dealtabpage" class="tabpage submodule deal-page rwSubModule" data-tabid="dealtab"></div>

              <!-- QUOTE TAB -->
              <div data-type="tabpage" id="quotetabpage" class="tabpage submodule quote-page rwSubModule" data-tabid="quotetab"></div>

              <!-- ORDER TAB -->
              <div data-type="tabpage" id="ordertabpage" class="tabpage submodule order-page rwSubModule" data-tabid="ordertab"></div>

             <!-- CONTRACT TAB -->
             <div data-type="tabpage" id="contracttabpage" class="tabpage contractSubModule rwSubModule" data-tabid="contracttab"></div>

             <!-- INVOICE TAB -->
             <div data-type="tabpage" id="invoicetabpage" class="tabpage invoiceSubModule rwSubModule" data-tabid="invoicetab"></div>
             
             <!-- RECEIPT TAB -->
             <div data-type="tabpage" id="receipttabpage" class="tabpage receiptSubModule rwSubModule" data-tabid="receipttab"></div>

              <!-- CREDITS TAB -->
              <div data-type="tabpage" id="creditstabpage" class="tabpage submodule credits-page rwSubModule" data-tabid="creditstab"></div>

              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="CustomerNoteGrid" data-securitycaption="Customer Note"></div>
                  </div>
                </div>
              </div>
              
             <!-- DOCUMENTS TAB -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwAppDocumentGrid"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>`;
    }
    //----------------------------------------------------------------------------------------------
    addressTypeChange($form) {
        const billingAddressType = FwFormField.getValueByDataField($form, 'BillingAddressType');
        if (billingAddressType === 'CUSTOMER') {
            // Values from Customer fields in general tab
            FwFormField.setValueByDataField($form, 'BillToAddress1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValueByDataField($form, 'BillToAddress2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValueByDataField($form, 'BillToCity', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValueByDataField($form, 'BillToState', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValueByDataField($form, 'BillToZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValueByDataField($form, 'BillToCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }
        const shippingAddressType = FwFormField.getValueByDataField($form, 'ShippingAddressType');
        if (shippingAddressType === 'CUSTOMER') {
            // Values from Customer fields in general tab
            FwFormField.enable($form.find('div[ data-datafield="ShipAttention"]'));
            FwFormField.setValueByDataField($form, 'ShipAddress1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValueByDataField($form, 'ShipAddress2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValueByDataField($form, 'ShipCity', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValueByDataField($form, 'ShipState', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValueByDataField($form, 'ShipZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValueByDataField($form, 'ShipCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }

        if (billingAddressType === 'OTHER') {
            FwFormField.enable($form.find('.billingaddress'));
        }

        if (shippingAddressType === 'OTHER') {
            FwFormField.enable($form.find('.shippingaddress'));
        }
    }
    //----------------------------------------------------------------------------------------------
    disableFields($form: JQuery, fields: string[]): void {
        fields.forEach((e, i) => { FwFormField.disable($form.find(`[data-datafield="${e}"]`)); });
    }
    //----------------------------------------------------------------------------------------------
    enableFields($form: JQuery, fields: string[]): void {
        fields.forEach((e, i) => { FwFormField.enable($form.find(`[data-datafield="${e}"]`)); });
    }
    //----------------------------------------------------------------------------------------------
    toggleOptionsTabIfExcludeQuote($form: JQuery, isExcluded: boolean): void {
        const list = ['DisableRental',
            'DisableSales',
            'DisableFacilities',
            'DisableTransportation',
            'DisableLabor',
            'DisableMisc',
            'DisableRentalSale',
            'DisableSubRental',
            'DisableSubSale',
            'DisableSubLabor',
            'DisableSubMisc'];

        isExcluded ? this.enableFields($form, list) : this.disableFields($form, list);
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