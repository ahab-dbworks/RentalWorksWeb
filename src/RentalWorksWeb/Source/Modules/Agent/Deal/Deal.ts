class Deal {
    Module: string = 'Deal';
    apiurl: string = 'api/v1/deal';
    caption: string = Constants.Modules.Agent.children.Deal.caption;
    nav: string = Constants.Modules.Agent.children.Deal.nav;
    id: string = Constants.Modules.Agent.children.Deal.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

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
    openForm(mode: string, parentmoduleinfo?: any) {
        //var $form = FwModule.loadFormFromTemplate(this.Module);
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //FwTabs.hideTab($form.find('.quotetab'));
        //FwTabs.hideTab($form.find('.ordertab'));
        //FwTabs.hideTab($form.find('.contracttab'));
        //FwTabs.hideTab($form.find('.invoicetab'));
        //FwTabs.hideTab($form.find('.receipttab'));
        //FwTabs.hideTab($form.find('.creditstab'));
        FwFormField.disable($form.find('.CompanyResaleGrid'));
        this.events($form);

        //Toggle Buttons on Billing tab
        FwFormField.loadItems($form.find('div[data-datafield="BillToAddressType"]'), [
            { value: 'CUSTOMER', caption: 'Customer', checked: true },
            { value: 'DEAL', caption: 'Deal' },
            { value: 'OTHER', caption: 'Other' }
        ]);

        //Toggle Buttons on Billing tab
        FwFormField.loadItems($form.find('div[data-datafield="PoType"]'), [
            { value: 'H', caption: 'Hardcopy', checked: true },
            { value: 'V', caption: 'Verbal' }
        ]);

        //Toggle Buttons on Tax tab - Use Customer / Use Deal Tax option
        //FwFormField.loadItems($form.find('div[data-datafield="UseCustomerTax"]'), [
        //    { value: 'TRUE',    caption: 'Use Customer', checked: true },
        //    { value: 'FALSE',   caption: 'Use Deal' }
        //]);

        //Toggle Buttons on Tax tab - Taxable / Non-Taxable option
        FwFormField.loadItems($form.find('div[data-datafield="Taxable"]'), [
            { value: 'true', caption: 'Taxable', checked: true },
            { value: 'false', caption: 'Non-Taxable' }
        ]);

        //Toggle Buttons on Shipping tab
        FwFormField.loadItems($form.find('div[data-datafield="ShippingAddressType"]'), [
            { value: 'CUSTOMER', caption: 'Customer', checked: true },
            { value: 'DEAL', caption: 'Deal' },
            { value: 'OTHER', caption: 'Other' }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="DefaultOutgoingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Deliver to Customer', checked: true },
            { value: 'SHIP', caption: 'Ship to Customer', },
            { value: 'PICK UP', caption: 'Customer Pick Up' }
        ]);

        FwFormField.loadItems($form.find('div[data-datafield="DefaultIncomingDeliveryType"]'), [
            { value: 'DELIVER', caption: 'Customer Deliver', checked: true },
            { value: 'SHIP', caption: 'Customer Ship' },
            { value: 'PICK UP', caption: 'Pick Up from Customer' }
        ]);
        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'CustomerId', parentmoduleinfo.CustomerId, parentmoduleinfo.Customer);
            FwFormField.setValueByDataField($form, 'Address1', parentmoduleinfo.Address1);
            FwFormField.setValueByDataField($form, 'Address2', parentmoduleinfo.Address2);
            FwFormField.setValueByDataField($form, 'City', parentmoduleinfo.City);
            FwFormField.setValueByDataField($form, 'State', parentmoduleinfo.State);
            FwFormField.setValueByDataField($form, 'ZipCode', parentmoduleinfo.ZipCode);
            FwFormField.setValueByDataField($form, 'CountryId', parentmoduleinfo.CountryId, parentmoduleinfo.Country);
            FwFormField.setValueByDataField($form, 'Phone', parentmoduleinfo.Phone);
            FwFormField.setValueByDataField($form, 'Fax', parentmoduleinfo.Fax);
            FwFormField.setValueByDataField($form, 'PhoneTollFree', parentmoduleinfo.PhoneTollFree);
            FwFormField.setValueByDataField($form, 'PhoneOther', parentmoduleinfo.OtherPhone);
        }

        if (mode === 'NEW') {
            FwFormField.setValueByDataField($form, 'UseCustomerDiscount', 'true');
            FwFormField.setValueByDataField($form, 'UseCustomerCredit', 'true');
            FwFormField.setValueByDataField($form, 'UseCustomerInsurance', 'true');
            FwFormField.setValueByDataField($form, 'UseCustomerTax', 'true');
            $form.find('[data-datafield="UseCustomerTax"] .fwformfield-value').change();
            $form.find('[data-datafield="UseCustomerDiscount"] .fwformfield-value').change();
            $form.find('[data-datafield="UseCustomerCredit"] .fwformfield-value').change();
            $form.find('[data-datafield="UseCustomerInsurance"] .fwformfield-value').change();

            const officeLocation = JSON.parse(sessionStorage.getItem('location'));
            const dealDefaults = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValue($form, 'div[data-datafield="DealStatusId"]', dealDefaults.defaultdealstatusid, dealDefaults.defaultdealstatus);
            FwFormField.setValue($form, 'div[data-datafield="BillingCycleId"]', dealDefaults.defaultdealbillingcycleid, dealDefaults.defaultdealbillingcycle);
            FwFormField.setValue($form, 'div[data-datafield="PoRequired"]', dealDefaults.defaultdealporequired);
            FwFormField.setValue($form, 'div[data-datafield="PoType"]', dealDefaults.defaultdealpotype);
            FwFormField.setValueByDataField($form, 'DefaultRate', officeLocation.ratetype, officeLocation.ratetype);
        }

        const userAssignedDealNo = JSON.parse(sessionStorage.getItem('controldefaults')).userassigneddealnumber;
        if (userAssignedDealNo) {
            FwFormField.enable($form.find('[data-datafield="DealNumber"]'));
            $form.find('[data-datafield="DealNumber"]').attr(`data-required`, `true`);
        }
        else {
            FwFormField.disable($form.find('[data-datafield="DealNumber"]'));
            $form.find('[data-datafield="DealNumber"]').attr(`data-required`, `false`);
        }

        // SUBMODULES
        const getData = ($form) => {
            const data = {
                DealId: FwFormField.getValueByDataField($form, 'DealId'),
                Deal: FwFormField.getValueByDataField($form, 'Deal'),
                RateTypeId: FwFormField.getValueByDataField($form, 'DefaultRate'),
                RateType: FwFormField.getTextByDataField($form, 'DefaultRate'),
                BillingCycleId: FwFormField.getValueByDataField($form, 'BillingCycleId'),
                BillingCycle: FwFormField.getTextByDataField($form, 'BillingCycleId'),
                PaymentTypeId: FwFormField.getValueByDataField($form, 'PaymentTypeId'),
                PaymentType: FwFormField.getTextByDataField($form, 'PaymentTypeId'),
                PaymentTermsId: FwFormField.getValueByDataField($form, 'PaymentTermsId'),
                PaymentTerms: FwFormField.getTextByDataField($form, 'PaymentTermsId'),
                DealNumber: FwFormField.getValueByDataField($form, 'DealNumber'),
                CustomerId: FwFormField.getValueByDataField($form, 'CustomerId'),
                Customer: FwFormField.getTextByDataField($form, 'CustomerId'),
                CurrencyId: FwFormField.getValueByDataField($form, 'CurrencyId'),
                CurrencyCode: FwFormField.getTextByDataField($form, 'CurrencyId'),
                BillToAttention1: FwFormField.getValueByDataField($form, 'BillToAttention1'),
                BillToAttention2: FwFormField.getValueByDataField($form, 'BillToAttention2'),
                BillToAddress1: FwFormField.getValueByDataField($form, 'BillToAddress1'),
                BillToAddress2: FwFormField.getValueByDataField($form, 'BillToAddress2'),
                BillToCity: FwFormField.getValueByDataField($form, 'BillToCity'),
                BillToState: FwFormField.getValueByDataField($form, 'BillToState'),
                BillToZipCode: FwFormField.getValueByDataField($form, 'BillToZipCode'),
                BillToCountryId: FwFormField.getValueByDataField($form, 'BillToCountryId'),
                BillToCountry: FwFormField.getTextByDataField($form, 'BillToCountryId'),
                BillToAddressType: FwFormField.getValueByDataField($form, 'BillToAddressType'),
                DefaultOutgoingDeliveryType: FwFormField.getValueByDataField($form, 'DefaultOutgoingDeliveryType'),
                DefaultIncomingDeliveryType: FwFormField.getValueByDataField($form, 'DefaultIncomingDeliveryType')
            }
            return data;
        }

        let nodeQuote = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'jFkSBEur1dluU');
        if (nodeQuote !== undefined && nodeQuote.properties.visible === 'T') {
            FwTabs.showTab($form.find('.quotetab'));
            const $submoduleQuoteBrowse = this.openQuoteBrowse($form);
            $form.find('.quote').append($submoduleQuoteBrowse);
            $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
            $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                if ($form.attr('data-mode') !== 'NEW') {
                    const $browse = jQuery(this).closest('.fwbrowse');
                    const controller = $browse.attr('data-controller');
                    if (typeof window[controller] !== 'object') throw `Missing javascript module: ${controller}`;
                    if (typeof window[controller]['openForm'] !== 'function') throw `Missing javascript function: ${controller}.openForm`;
                    const $quoteForm = window[controller]['openForm']('NEW', getData($form));
                    FwModule.openSubModuleTab($browse, $quoteForm);
                } else {
                    FwNotification.renderNotification('WARNING', 'Save the record first.')
                }
            });
        }

        let nodeOrder = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'U8Zlahz3ke9i');
        if (nodeOrder !== undefined && nodeQuote.properties.visible === 'T') {
            FwTabs.showTab($form.find('.ordertab'));
            const $submoduleOrderBrowse = this.openOrderBrowse($form);
            $form.find('.order').append($submoduleOrderBrowse);
            $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
            $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
                if ($form.attr('data-mode') !== 'NEW') {
                    const $browse = jQuery(this).closest('.fwbrowse');
                    const controller = $browse.attr('data-controller');
                    if (typeof window[controller] !== 'object') throw `Missing javascript module: ${controller}`;
                    if (typeof window[controller]['openForm'] !== 'function') throw `Missing javascript function: ${controller}.openForm`;
                    const $orderForm = window[controller]['openForm']('NEW', getData($form));
                    FwModule.openSubModuleTab($browse, $orderForm);
                } else {
                    FwNotification.renderNotification('WARNING', 'Save the record first.')
                }
            });
        }

        // Deal Credit submodule
        let nodeDealCredit = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'OCkLGwclipEA');
        if (nodeDealCredit !== undefined && nodeDealCredit.properties.visible === 'T') {
            FwTabs.showTab($form.find('.creditstab'));
            const $submoduleDealCreditBrowse = this.openDealCreditBrowse($form);
            $form.find('.credits-page').append($submoduleDealCreditBrowse);
        }

        //$defaultrate = $form.find('.defaultrate');
        //FwFormField.loadItems($defaultrate, [
        //    { value: 'DAILY', text: 'Daily Rate' }
        //    , { value: 'WEEKLY', text: 'Weekly Rate' }
        //    , { value: 'MONTHLY', text: 'Monthly Rate' }
        //]);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'CustomerId', parentmoduleinfo.CustomerId, parentmoduleinfo.Customer);
            FwFormField.setValueByDataField($form, 'Address1', parentmoduleinfo.Address1);
            FwFormField.setValueByDataField($form, 'Address2', parentmoduleinfo.Address2);
            FwFormField.setValueByDataField($form, 'City', parentmoduleinfo.City);
            FwFormField.setValueByDataField($form, 'State', parentmoduleinfo.State);
            FwFormField.setValueByDataField($form, 'ZipCode', parentmoduleinfo.ZipCode);
            FwFormField.setValueByDataField($form, 'CountryId', parentmoduleinfo.CountryId, parentmoduleinfo.Country);
            FwFormField.setValueByDataField($form, 'Phone', parentmoduleinfo.Phone);
            this.customerChange($form);
        }

        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);
        // Default address data before save event
        $form.data('beforesave', request => {
            this.billingAddressTypeChange($form);
            this.shippingAddressTypeChange($form);
        });
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DealId"] input').val(uniqueids.DealId);

        FwModule.loadForm(this.Module, $form);

        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);

        // Contract submodule
        let nodeContract = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'Z8MlDQp7xOqu');
        if (nodeContract !== undefined && nodeContract.properties.visible === 'T') {
            FwTabs.showTab($form.find('.contracttab'));
            $form.find('.contractSubModule').append(this.openContractBrowse($form));
        }

        // Invoice submodule
        let nodeInvoice = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'cZ9Z8aGEiDDw');
        if (nodeInvoice !== undefined && nodeInvoice.properties.visible === 'T') {
            FwTabs.showTab($form.find('.invoicetab'));
            $form.find('.invoiceSubModule').append(this.openInvoiceBrowse($form));
        }

        // Receipt submodule
        let nodeReceipt = FwApplicationTree.getNodeById(FwApplicationTree.tree, 'q4PPGLusbFw');
        if (nodeReceipt !== undefined && nodeReceipt.properties.visible === 'T') {
            FwTabs.showTab($form.find('.receipttab'));
            $form.find('.receiptSubModule').append(this.openReceiptBrowse($form));
        }

        return $form;
    }
    //---------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any): void {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    renderGrids($form: any) {
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'DealHiatusDiscountGrid',
            gridSecurityId: 'qyEHq2bK1WIJ4',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DealId: FwFormField.getValueByDataField($form, `DealId`)
                };
            },
            beforeSave: (request: any) => {
                request.DealId = FwFormField.getValueByDataField($form, `DealId`);
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'CompanyResaleGrid',
            gridSecurityId: 'k48X9sulRpmb',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
        // ----------
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
                    CompanyId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            //beforeSave: (request: any) => {
            //    request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            //}
            afterDataBindCallback: ($browse: JQuery, dt: FwJsonDataTable) => {
                this.renderWideGridColumns($form.find('[data-name="CompanyTaxOptionGrid"]'));
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'DealNoteGrid',
            gridSecurityId: 'jcwmVLFEU88k',
            moduleSecurityId: this.id,
            $form: $form,
            addGridMenu: (options: IAddGridMenuOptions) => {

            },
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DealId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            beforeSave: (request: any) => {
                request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'DealShipperGrid',
            gridSecurityId: '5cMD0y0jSUgz',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    DealId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            beforeSave: (request: any) => {
                request.DealId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
        // ----------
        FwBrowse.renderGrid({
            nameGrid: 'CompanyContactGrid',
            gridSecurityId: 'gQHuhVDA5Do2',
            moduleSecurityId: this.id,
            $form: $form,
            onDataBind: (request: any) => {
                request.uniqueids = {
                    CompanyId: FwFormField.getValueByDataField($form, 'DealId')
                };
            },
            beforeSave: (request: any) => {
                request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    renderWideGridColumns($grid: JQuery) {
        if ($grid.find('thead tr').length < 2) {
            const $thead = $grid.find('thead tr').clone(false);
            $thead.find('[data-sort]').removeAttr('data-sort');
            $thead.find('td div.divselectrow').hide();
            $thead.find('td div.field:not([data-sharedcolumn])').hide();
            const $sharedColumnTds = $thead.find('td div[data-widerow]').parents('td');
            for (let i = 0; i < $sharedColumnTds.length; i++) {
                const $td = jQuery($sharedColumnTds[i]);
                const caption = $td.find('div[data-sharedcolumn]').attr('data-sharedcolumn');
                $td.find('.caption').text(caption);
            }
            $sharedColumnTds.css('text-align', 'center');
            $grid.find('thead').prepend($thead);
        }
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any): void {
        const $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);

        // Documents Grid - Need to put this here, because renderGrids is called from openForm and uniqueid is not available yet on the form
        // Moved documents grid from loadForm to afterLoad so it loads on new records. - Jason H 04/20/20
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        FwAppDocumentGrid.renderGrid({
            $form: $form,
            caption: 'Documents',
            nameGrid: 'DealDocumentGrid',
            getBaseApiUrl: () => {
                return `${this.apiurl}/${dealId}/document`;
            },
            gridSecurityId: '5pVhTJtGXLVx',
            moduleSecurityId: this.id,
            parentFormDataFields: 'DealId',
            uniqueid1Name: 'DealId',
            getUniqueid1Value: () => dealId,
            uniqueid2Name: '',
            getUniqueid2Value: () => ''
        });

        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);
        //this.useDiscountTemplate(FwFormField.getValueByDataField($form, 'UseDiscountTemplate'));
        this.toggleBillingUseDiscount($form, FwFormField.getValueByDataField($form, 'UseDiscountTemplate'));
        const val_bill = FwFormField.getValueByDataField($form, 'BillToAddressType') !== 'OTHER' ? true : false;
        this.toggleBillingAddressInfo($form, val_bill);
        const val_ship = FwFormField.getValueByDataField($form, 'ShippingAddressType') !== 'OTHER' ? true : false;
        this.toggleShippingAddressInfo($form, val_ship);
        this.useCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerDiscount'));
        this.toggleInsurTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerInsurance'));
        this.toggleCredTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerCredit'));
        this.toggleTaxTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerTax'));
        this.disableInsurCompanyInfo($form);
        this.toggleOptionsTabIfExcludeQuote($form, FwFormField.getValueByDataField($form, 'DisableQuoteOrderActivity'));
        this.transferDealAddressValues($form);

        // Disable Tax grids if UseCustomerTax is selected on page load
        if (FwFormField.getValueByDataField($form, 'UseCustomerTax') === true) {
            FwFormField.disable($form.find('div[data-name="CompanyResaleGrid"]'));
            FwFormField.disable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-name="CompanyResaleGrid"]'));
            FwFormField.enable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
        }
        // UnlimitedCredit checkbox on Credit tab
        const useCustomerCredit = FwFormField.getValueByDataField($form, 'UseCustomerCredit');
        const unlimitedCredit = FwFormField.getValueByDataField($form, 'UnlimitedCredit');
        if (unlimitedCredit) {
            FwFormField.disable($form.find('div[data-datafield="CreditLimit"]'));
        } else if (!unlimitedCredit && !useCustomerCredit) {
            FwFormField.enable($form.find('div[data-datafield="CreditLimit"]'));
        }

        if (FwFormField.getValueByDataField($form, 'UseCustomerInsurance') === true) {
            this.getCustomerInsuranceValues($form);
        }

        // Disable Tax grids on change
        $form.find('[data-datafield="UseCustomerTax"] .fwformfield-value').on('change', function () {
            if (FwFormField.getValueByDataField($form, 'UseCustomerTax') === true) {
                FwFormField.disable($form.find('div[data-name="CompanyResaleGrid"]'));
                FwFormField.disable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
            }
            else {
                FwFormField.enable($form.find('div[data-name="CompanyResaleGrid"]'));
                FwFormField.enable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
            }
        });

        const taxable = FwFormField.getValueByDataField($form, 'Taxable');
        if (taxable === 'true') {
            FwFormField.disable($form.find('.non-taxable'));
        } else {
            FwFormField.enable($form.find('.non-taxable'));
        }


        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"][data-enabled!="false"]', e => {
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
    openContractBrowse($form) {
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const $browse = ContractController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ContractController.ActiveViewFields;
            request.uniqueids = {
                DealId: dealId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openInvoiceBrowse($form) {
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const $browse = InvoiceController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = InvoiceController.ActiveViewFields;
            request.uniqueids = {
                DealId: dealId
            };
        });
        return $browse;
    }
    //---------------------------------------------------------------------------------------------
    openReceiptBrowse($form) {
        const dealId = FwFormField.getValueByDataField($form, 'DealId');
        const $browse = ReceiptController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = ReceiptController.ActiveViewFields;
            request.uniqueids = {
                DealId: dealId
            };
        });
        return $browse;
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
        $form.find('[data-datafield="UnlimitedCredit"] .fwformfield-value').on('change', function () {
            const $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('div[data-datafield="CreditLimit"]'));
            } else {
                FwFormField.enable($form.find('div[data-datafield="CreditLimit"]'));
            }
        });
        // If user changes customer, update corresponding address fields in other tabs
        $form.find('div[data-datafield="CustomerId"]').data('onchange', e => {
            this.customerChange($form);
        });
        // If user updates general address info
        $form.find('.deal_address input').on('change', e => {
            this.transferDealAddressValues($form);
        });
        //Shipping Address Type Change
        $form.find('div[data-datafield="ShippingAddressType"]').on('change', e => {
            const val = FwFormField.getValueByDataField($form, 'ShippingAddressType') !== 'OTHER' ? true : false;
            this.toggleShippingAddressInfo($form, val);
            this.shippingAddressTypeChange($form);
        });
        //Billing Address Type Change
        $form.find('div[data-datafield="BillToAddressType"]').on('change', e => {
            const val = FwFormField.getValueByDataField($form, 'BillToAddressType') !== 'OTHER' ? true : false;
            this.toggleBillingAddressInfo($form, val);
            this.billingAddressTypeChange($form);
        });

        $form.on('change', 'div[data-datafield="UseDiscountTemplate"] input[type=checkbox]', e => {
            //this.useDiscountTemplate(jQuery(e.currentTarget).is(':checked'));
            this.toggleBillingUseDiscount($form, jQuery(e.currentTarget).is(':checked'));
        });

        $form.on('change', 'div[data-datafield="UseCustomerDiscount"] input[type=checkbox]', e => {
            this.useCustomer($form, jQuery(e.currentTarget).is(':checked'));
        });

        $form.on('change', 'div[data-datafield="UseCustomerCredit"] input[type=checkbox]', e => {
            const isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleCredTabIfUseCustomer($form, isChecked);
        });

        $form.on('change', 'div[data-datafield="UseCustomerInsurance"] input[type=checkbox]', e => {
            const isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleInsurTabIfUseCustomer($form, isChecked);
            if (isChecked) {
                this.getCustomerInsuranceValues($form);
            }
        });

        $form.find('div[data-datafield="Taxable"]').on('change', () => {
            const taxable = FwFormField.getValueByDataField($form, 'Taxable');
            if (taxable === 'true') {
                FwFormField.disable($form.find('.non-taxable'));
            } else {
                FwFormField.enable($form.find('.non-taxable'));
            }
        });

        $form.on('change', 'div[data-datafield="UseCustomerTax"] input[type=checkbox]', e => {
            const isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleTaxTabIfUseCustomer($form, isChecked);
        });

        $form.on('change', 'div[data-datafield="DisableQuoteOrderActivity"] input[type=checkbox]', e => {
            const isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleOptionsTabIfExcludeQuote($form, isChecked);
        });
        // Insurance Vendor validation
        $form.find('div[data-datafield="InsuranceCompanyId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'InsuranceCompanyAgent', $tr.find('.field[data-formdatafield="PrimaryContact"]').attr('data-originalvalue'));
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
    useCustomer($form: any, isChecked: boolean): void {
        const $temp: JQuery = jQuery('div[data-datafield="DiscountTemplateId"]');

        if (isChecked) {
            $temp.attr('data-enabled', 'false');
            $temp.find('input').prop('disabled', true);
            FwFormField.disable($form.find('[data-datafield="UseDiscountTemplate"]'))
        } else {
            const $discTemp: JQuery = jQuery('div[data-datafield="UseDiscountTemplate"]');
            if ($discTemp.find('input[type=checkbox]').is(':checked')) {
                $temp.attr('data-enabled', 'true');
                $temp.find('input').prop('disabled', false);
            } else {
                $temp.attr('data-enabled', 'false');
                $temp.find('input').prop('disabled', true);
            }
            FwFormField.enable($form.find('[data-datafield="UseDiscountTemplate"]'))
        }
    }
    //----------------------------------------------------------------------------------------------
    toggleBillingUseDiscount($form: JQuery, isDiscountTemplate: boolean): void {
        const list = ['DiscountTemplateId'];

        isDiscountTemplate ? this.enableFields($form, list) : this.disableFields($form, list);
    }
    //----------------------------------------------------------------------------------------------
    toggleBillingAddressInfo($form: JQuery, isOther: boolean) {
        const list = [
            'BillToAddress1',
            'BillToAddress2',
            'BillToCity',
            'BillToState',
            'BillToZipCode',
            'BillToCountryId'];

        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
    }
    //----------------------------------------------------------------------------------------------
    toggleShippingAddressInfo($form: JQuery, isOther: boolean) {
        const list = [
            'ShipAddress1',
            'ShipAddress2',
            'ShipCity',
            'ShipState',
            'ShipZipCode',
            'ShipCountryId'];

        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
    }
    //----------------------------------------------------------------------------------------------
    toggleCredTabIfUseCustomer($form: JQuery, isCustomer: boolean): void {
        const list = ['CreditStatusId',
            'CreditStatusThrough',
            'CreditLimit',
            'UnlimitedCredit',
            'CreditApplicationOnFile',
            'TradeReferencesVerifiedBy',
            'TradeReferencesVerifiedOn',
            'TradeReferencesVerified',
            'CreditCardName',
            'CreditCardAuthorizationFormOnFile',
            'CreditCardTypeId',
            'CreditCardExpirationMonth',
            'CreditCardExpirationYear',
            'CreditCardCode',
            'CreditCardLimit',
            'CreditCardNumber',
            'CreditResponsibleParty',
            'CreditResponsiblePartyOnFile',
            'DepletingDepositThresholdAmount',
            'DepletingDepositThresholdPercent'
            //'DepletingDepositTotal',
            //'DepletingDepositApplied',
            //'DepletingDepositRemaining'
        ];
        const unlimitedCredit = FwFormField.getValueByDataField($form, 'UnlimitedCredit');
        if (unlimitedCredit) { list.splice(2, 1); }

        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);
    }
    //----------------------------------------------------------------------------------------------
    toggleInsurTabIfUseCustomer($form: JQuery, isCustomer: boolean): void {
        const list = ['InsuranceCertificationValidThrough',
            'InsuranceCoverageLiability',
            'InsuranceCoverageLiabilityDeductible',
            'InsuranceCertification',
            'InsuranceCoverageProperty',
            'InsuranceCoveragePropertyDeductible',
            'VehicleInsuranceCertification',
            'InsuranceCompanyId',
            'InsuranceCompanyAgent'];

        const $insuranceName: JQuery = jQuery('div[data-datafield="InsuranceCompanyId"]');

        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);

        if (isCustomer) {
            $insuranceName.attr('data-enabled', 'false');
            $insuranceName.find('input').prop('disabled', true);
        } else {
            $insuranceName.attr('data-enabled', 'true');
            $insuranceName.find('input').prop('disabled', false);
        }
    }
    //----------------------------------------------------------------------------------------------
    disableInsurCompanyInfo($form: JQuery): void {
        const list = ['InsuranceCompanyAddress1',
            'InsuranceCompanyAddress2',
            'InsuranceCompanyCity',
            'InsuranceCompanyState',
            'InsuranceCompanyZipCode',
            'InsuranceCompanyCountryId',
            'InsuranceCompanyPhone',
            'InsuranceCompanyFax'];
        this.disableFields($form, list);
    }
    //----------------------------------------------------------------------------------------------
    toggleTaxTabIfUseCustomer($form: JQuery, useCustomer: boolean): void {
        const list = ['Taxable',
            'TaxStateOfIncorporationId',
            'TaxFederalNo',
            'NonTaxableCertificateNo',
            'NonTaxableYear',
            'NonTaxableCertificateValidThrough',
            'NonTaxableCertificateOnFile'];

        if (useCustomer) {
            this.disableFields($form, list);
            FwFormField.disable($form.find('div[data-name="CompanyResaleGrid"]'));
            FwFormField.disable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
        } else {
            FwFormField.enable($form.find('div[data-datafield="Taxable"]'));
            const isTaxable = FwFormField.getValueByDataField($form, 'Taxable');
            if (isTaxable === 'false') {
                this.enableFields($form, list);
                FwFormField.enable($form.find('div[data-name="CompanyResaleGrid"]'));
                FwFormField.enable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
            }
        }
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
    billingAddressTypeChange($form: any): void {
        if (FwFormField.getValueByDataField($form, 'BillToAddressType') === 'CUSTOMER') {
            const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
            FwAppData.apiMethod(true, 'GET', `api/v1/customer/${customerId}`, null, FwServices.defaultTimeout, response => {
                this.loadCustomerBillingValues($form, response);
            }, null, $form);
        }
        if (FwFormField.getValueByDataField($form, 'BillToAddressType') === 'DEAL') {
            FwFormField.enable($form.find('div[data-datafield="BillToAttention1"]'));
            FwFormField.enable($form.find('div[data-datafield="BillToAttention2"]'));
            FwFormField.setValueByDataField($form, 'BillToAddress1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValueByDataField($form, 'BillToAddress2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValueByDataField($form, 'BillToCity', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValueByDataField($form, 'BillToState', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValueByDataField($form, 'BillToZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValueByDataField($form, 'BillToCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }
        if (FwFormField.getValueByDataField($form, 'BillToAddressType') === 'OTHER') {
            FwFormField.enable($form.find('div[data-datafield="BillToAttention1"]'));
            FwFormField.enable($form.find('div[data-datafield="BillToAttention2"]'));
        }
    }
    //----------------------------------------------------------------------------------------------
    shippingAddressTypeChange($form: any): void {
        if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'CUSTOMER') {
            const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
            FwAppData.apiMethod(true, 'GET', `api/v1/customer/${customerId}`, null, FwServices.defaultTimeout, response => {
                this.loadCustomerShippingValues($form, response);
            }, null, null);
        }

        if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'DEAL') {
            FwFormField.setValueByDataField($form, 'ShipAddress1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValueByDataField($form, 'ShipAddress2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValueByDataField($form, 'ShipCity', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValueByDataField($form, 'ShipState', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValueByDataField($form, 'ShipZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValueByDataField($form, 'ShipCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }

        //if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'OTHER') {
        //    FwFormField.enable($form.find('div[data-datafield="ShipToAttention"]'));
        //}
    }
    //----------------------------------------------------------------------------------------------
    transferDealAddressValues($form: any): void {
        setTimeout(() => { // Wrapped in a setTimeout because text value in Country validation was not resetting prior to setting values
            // Billing Tab
            if (FwFormField.getValueByDataField($form, 'BillToAddressType') === 'DEAL') {
                FwFormField.setValueByDataField($form, 'BillToAddress1', FwFormField.getValueByDataField($form, 'Address1'));
                FwFormField.setValueByDataField($form, 'BillToAddress2', FwFormField.getValueByDataField($form, 'Address2'));
                FwFormField.setValueByDataField($form, 'BillToCity', FwFormField.getValueByDataField($form, 'City'));
                FwFormField.setValueByDataField($form, 'BillToState', FwFormField.getValueByDataField($form, 'State'));
                FwFormField.setValueByDataField($form, 'BillToZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
                FwFormField.setValueByDataField($form, 'BillToCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
            }
            // Shipping Tab
            if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'DEAL') {
                FwFormField.setValueByDataField($form, 'ShipAddress1', FwFormField.getValueByDataField($form, 'Address1'));
                FwFormField.setValueByDataField($form, 'ShipAddress2', FwFormField.getValueByDataField($form, 'Address2'));
                FwFormField.setValueByDataField($form, 'ShipCity', FwFormField.getValueByDataField($form, 'City'));
                FwFormField.setValueByDataField($form, 'ShipState', FwFormField.getValueByDataField($form, 'State'));
                FwFormField.setValueByDataField($form, 'ShipZipCode', FwFormField.getValueByDataField($form, 'ZipCode'));
                FwFormField.setValueByDataField($form, 'ShipCountryId', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
            }
        }, 1000)
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
    updateExternalInputsWithGridValues($tr: JQuery): void {

        $tr.find('.column > .field').each((i, e) => {
            let $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
            if (value === undefined || null) {
                jQuery(`.${id}`).find(':input').val(0);
            } else {
                jQuery(`.${id}`).find(':input').val(value);
            }
        });
        const TaxOption = $tr.find('.field[data-browsedatafield="TaxOptionId"]').attr('data-originaltext');
        jQuery('.TaxOption').find(':input').val(TaxOption);
    }
    //----------------------------------------------------------------------------------------------
    customerChange($form: any): void {
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        if (customerId) {
            FwAppData.apiMethod(true, 'GET', `api/v1/customer/${customerId}`, null, FwServices.defaultTimeout, response => {
                // Deal tab
                FwFormField.setValueByDataField($form, 'Address1', response.Address1);
                FwFormField.setValueByDataField($form, 'Address2', response.Address2);
                FwFormField.setValueByDataField($form, 'City', response.City);
                FwFormField.setValueByDataField($form, 'State', response.State);
                FwFormField.setValueByDataField($form, 'ZipCode', response.ZipCode);
                FwFormField.setValueByDataField($form, 'Phone', response.Phone);
                FwFormField.setValueByDataField($form, 'PhoneTollFree', response.PhoneTollFree);
                FwFormField.setValueByDataField($form, 'Fax', response.Fax);
                FwFormField.setValueByDataField($form, 'PhoneOther', response.OtherPhone);
                FwFormField.setValueByDataField($form, 'Email', response.Email);
                FwFormField.setValue($form, 'div[data-datafield="CountryId"]', response.CountryId, response.Country);
                FwFormField.setValue($form, 'div[data-datafield="PaymentTermsId"]', response.PaymentTermsId, response.PaymentTerms);
                // Insurance tab
                if (FwFormField.getValueByDataField($form, 'UseCustomerInsurance') === true) {
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyAddress1', response.InsuranceCompanyAddress1);
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyAddress2', response.InsuranceCompanyAddress2);
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyCity', response.InsuranceCompanyCity);
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyState', response.InsuranceCompanyState);
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyZipCode', response.InsuranceCompanyZipCode);
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyFax', response.InsuranceCompanyFax);
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyPhone', response.InsuranceCompanyPhone);
                    FwFormField.setValue($form, 'div[data-datafield="InsuranceCompanyCountryId"]', response.InsuranceCompanyCountryId, response.InsuranceCompanyCountry);
                    FwFormField.setValue($form, 'div[data-datafield="InsuranceCompanyId"]', response.InsuranceCompanyId, response.InsuranceCompany);
                    FwFormField.setValueByDataField($form, 'InsuranceCompanyAgent', response.InsuranceAgent);
                }
                // Shipping Address tab defaults
                if (FwFormField.getValueByDataField($form, 'ShippingAddressType') === 'CUSTOMER') {
                    this.loadCustomerShippingValues($form, response);
                }
                if (FwFormField.getValueByDataField($form, 'BillToAddressType') === 'CUSTOMER') {
                    this.loadCustomerBillingValues($form, response);
                }
            }, null, null);
        } else {
            console.error(`CustomerId is undefined.`)
        }
    }
    //----------------------------------------------------------------------------------------------
    loadCustomerShippingValues($form: any, response: any): void {
        FwFormField.setValueByDataField($form, 'ShipAddress1', '');
        FwFormField.setValueByDataField($form, 'ShipAddress2', '');
        FwFormField.setValueByDataField($form, 'ShipCity', '');
        FwFormField.setValueByDataField($form, 'ShipState', '');
        FwFormField.setValueByDataField($form, 'ShipZipCode', '');
        FwFormField.setValueByDataField($form, 'ShipCountryId', '', '');

        FwFormField.setValueByDataField($form, 'ShipAddress1', response.ShipAddress1);
        FwFormField.setValueByDataField($form, 'ShipAddress2', response.ShipAddress2);
        FwFormField.setValueByDataField($form, 'ShipCity', response.ShipCity);
        FwFormField.setValueByDataField($form, 'ShipState', response.ShipState);
        FwFormField.setValueByDataField($form, 'ShipZipCode', response.ShipZipCode);
        FwFormField.setValueByDataField($form, 'ShipCountryId', response.ShipCountryId, response.ShipCountry);
    }
    //----------------------------------------------------------------------------------------------
    loadCustomerBillingValues($form: any, response: any): void {
        FwFormField.disable($form.find('div[data-datafield="BillToAttention1"]'));
        FwFormField.disable($form.find('div[data-datafield="BillToAttention2"]'));
        FwFormField.setValueByDataField($form, 'BillToAttention1', '');
        FwFormField.setValueByDataField($form, 'BillToAttention2', '');
        FwFormField.setValueByDataField($form, 'BillToAddress1', '');
        FwFormField.setValueByDataField($form, 'BillToAddress2', '');
        FwFormField.setValueByDataField($form, 'BillToCity', '');
        FwFormField.setValueByDataField($form, 'BillToState', '');
        FwFormField.setValueByDataField($form, 'BillToZipCode', '');
        FwFormField.setValueByDataField($form, 'BillToCountryId', '', '');

        FwFormField.setValueByDataField($form, 'BillToAttention1', response.BillToAttention1);
        FwFormField.setValueByDataField($form, 'BillToAttention2', response.BillToAttention2);
        FwFormField.setValueByDataField($form, 'BillToAddress1', response.BillToAddress1);
        FwFormField.setValueByDataField($form, 'BillToAddress2', response.BillToAddress2);
        FwFormField.setValueByDataField($form, 'BillToCity', response.BillToCity);
        FwFormField.setValueByDataField($form, 'BillToState', response.BillToState);
        FwFormField.setValueByDataField($form, 'BillToZipCode', response.BillToZipCode);
        FwFormField.setValueByDataField($form, 'BillToCountryId', response.BillToCountryId, response.BillToCountry);
    }
    //----------------------------------------------------------------------------------------------
    getCustomerInsuranceValues($form: any): void {
        const customerId = FwFormField.getValueByDataField($form, 'CustomerId');
        if (customerId) {
            FwAppData.apiMethod(true, 'GET', `api/v1/customer/${customerId}`, null, FwServices.defaultTimeout, response => {
                FwFormField.setValueByDataField($form, 'InsuranceCompanyAddress1', response.InsuranceCompanyAddress1);
                FwFormField.setValueByDataField($form, 'InsuranceCompanyAddress2', response.InsuranceCompanyAddress2);
                FwFormField.setValueByDataField($form, 'InsuranceCompanyCity', response.InsuranceCompanyCity);
                FwFormField.setValueByDataField($form, 'InsuranceCompanyState', response.InsuranceCompanyState);
                FwFormField.setValueByDataField($form, 'InsuranceCompanyZipCode', response.InsuranceCompanyZipCode);
                FwFormField.setValueByDataField($form, 'InsuranceCompanyFax', response.InsuranceCompanyFax);
                FwFormField.setValueByDataField($form, 'InsuranceCompanyPhone', response.InsuranceCompanyPhone);
                FwFormField.setValue($form, 'div[data-datafield="InsuranceCompanyCountryId"]', response.InsuranceCompanyCountryId, response.InsuranceCompanyCountry);
                FwFormField.setValue($form, 'div[data-datafield="InsuranceCompanyId"]', response.InsuranceCompanyId, response.InsuranceCompany);
                FwFormField.setValueByDataField($form, 'InsuranceCompanyAgent', response.InsuranceAgent);
            }, null, null);
        } else {
            console.error(`CustomerId is undefined.`);
        }
    }
    //----------------------------------------------------------------------------------------------
    openQuoteBrowse($form) {
        const $browse = QuoteController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.activeviewfields = QuoteController.ActiveViewFields;
            request.uniqueids = {
                DealId: $form.find('[data-datafield="DealId"] input.fwformfield-value').val()
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
                DealId: $form.find('[data-datafield="DealId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };
    //----------------------------------------------------------------------------------------------
    openDealCreditBrowse($form) {
        const $browse = DealCreditController.openBrowse();
        $browse.data('ondatabind', request => {
            request.activeviewfields = DealCreditController.ActiveViewFields;
            request.uniqueids = {
                DealId: $form.find('[data-datafield="DealId"] input.fwformfield-value').val()
            }
        });
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield, request, $validationbrowse, $form, $tr) {
        switch (datafield) {
            case 'CustomerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecustomer`);
                break;
            case 'OfficeLocationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateofficelocation`);
                break;
            case 'DepartmentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedepartment`);
                break;
            case 'DealTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedealtype`);
                break;
            case 'DealClassificationId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedealclassification`);
                break;
            case 'ProductionTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateproductiontype`);
                break;
            case 'CsrId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecsr`);
                break;
            case 'DefaultAgentId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateagent`);
                break;
            case 'DefaultProjectManagerId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateprojectmanager`);
                break;
            case 'CountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecountry`);
                break;
            case 'DealStatusId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedealstatus`);
                break;
            case 'BillingCycleId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatebillingcycle`);
                break;
            case 'PaymentTypeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymenttype`);
                break;
            case 'PaymentTermsId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepaymentterms`);
                break;
            case 'DefaultRate':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateorderrate`);
                break;
            case 'OutsideSalesRepresentativeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesalesrepresentative`);
                break;
            case 'CreditStatusId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecreditstatus`);
                break;
            case 'InsuranceCompanyId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinsurancecompany`);
                break;
            case 'ShipCountryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateshipcountry`);
                break;
        }
    }
    //----------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        return `
        <div data-name="Deal" data-control="FwBrowse" data-type="Browse" id="DealBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="DealController" data-hasinactive="true">
          <div class="column" data-width="0" data-visible="false">
            <div class="field" data-isuniqueid="true" data-datafield="DealId" data-browsedatatype="key" ></div>
          </div>
          <div class="column" data-width="300px" data-visible="true">
            <div class="field" data-caption="Deal Name" data-datafield="Deal" data-browsedatatype="text" data-sort="asc"></div>
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
          <div class="column" data-width="300px" data-visible="true">
            <div class="field" data-caption="Phone" data-datafield="Phone" data-browsedatatype="phone" data-sort="off"></div>
          </div>
          <div class="column spacer" data-width="auto" data-visible="true"></div>
        </div>`;
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
        <div id="dealform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Deal" data-rendermode="template" data-mode="" data-hasaudit="false" data-controller="DealController">
          <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="DealId"></div>
          <div id="dealform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
            <div class="tabs">
              <div data-type="tab" id="dealtab" class="tab" data-tabpageid="dealtabpage" data-caption="Deal"></div>
              <div data-type="tab" id="contactstab" class="tab" data-tabpageid="contactstabpage" data-caption="Contacts"></div>
              <div data-type="tab" id="billingtab" class="tab" data-tabpageid="billingtabpage" data-caption="Billing"></div>
              <div data-type="tab" id="hiatustab" class="tab" data-tabpageid="hiatustabpage" data-caption="Hiatus"></div>
              <div data-type="tab" id="credittab" class="tab" data-tabpageid="credittabpage" data-caption="Credit"></div>
              <div data-type="tab" id="insurancetab" class="tab" data-tabpageid="insurancetabpage" data-caption="Insurance"></div>
              <div data-type="tab" id="taxtab" class="tab" data-tabpageid="taxtabpage" data-caption="Tax"></div>
              <div data-type="tab" id="optionstab" class="tab" data-tabpageid="optionstabpage" data-caption="Options"></div>
              <div data-type="tab" id="shippingtab" class="tab" data-tabpageid="shippingtabpage" data-caption="Shipping"></div>
              <div data-type="tab" id="quotetab" class="tab submodule quotetab" data-tabpageid="quotetabpage" data-caption="Quotes"></div>
              <div data-type="tab" id="ordertab" class="tab submodule ordertab" data-tabpageid="ordertabpage" data-caption="Orders"></div>              
              <div data-type="tab" id="contracttab" class="tab submodule contracttab" data-tabpageid="contracttabpage" data-caption="Contracts"></div>
              <div data-type="tab" id="invoicetab" class="tab submodule invoicetab" data-tabpageid="invoicetabpage" data-caption="Invoices"></div>
              <div data-type="tab" id="receipttab" class="tab submodule receipttab" data-tabpageid="receipttabpage" data-caption="Receipts"></div>
              <div data-type="tab" id="creditstab" class="tab submodule creditstab" data-tabpageid="creditstabpage" data-caption="Credits"></div>
              <div data-type="tab" id="documentstab" class="tab" data-tabpageid="documentstabpage" data-caption="Documents"></div>
              <div data-type="tab" id="notestab" class="tab" data-tabpageid="notestabpage" data-caption="Notes"></div>
            </div>
            <div class="tabpages">
             <!-- DEAL TAB -->
              <div data-type="tabpage" id="dealtabpage" class="tabpage" data-tabid="dealtab">
                <div class="flexpage">
                  <div class="flexrow">
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal Name" data-datafield="Deal" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Deal Number" data-datafield="DealNumber" data-required="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer" data-datafield="CustomerId" data-displayfield="Customer" data-validationname="CustomerValidation" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Office Location" data-datafield="OfficeLocationId" data-displayfield="OfficeLocation" data-validationname="OfficeLocationValidation" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Managing Department" data-datafield="DepartmentId" data-displayfield="Department" data-validationname="DepartmentValidation" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Type" data-datafield="DealTypeId" data-displayfield="DealType" data-validationname="DealTypeValidation" data-required="true" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Classification" data-datafield="DealClassificationId" data-displayfield="DealClassification" data-validationname="DealClassificationValidation" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Production Type" data-datafield="ProductionTypeId" data-displayfield="ProductionType" data-validationname="ProductionTypeValidation" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Customer Service Representative" data-datafield="CsrId" data-displayfield="Csr" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="DefaultAgentId" data-displayfield="DefaultAgent" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Project Manager" data-datafield="DefaultProjectManagerId" data-displayfield="DefaultProjectManager" data-validationname="UserValidation" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Address / Contact section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Address 1" data-datafield="Address1" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Address 2" data-datafield="Address2" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="City" data-datafield="City" style="flex:2 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="State" data-datafield="State" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield deal_address" data-caption="Zip/Postal" data-datafield="ZipCode" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield deal_address" data-caption="Country" data-datafield="CountryId" data-displayfield="Country" data-validationname="CountryValidation" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Contact">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Main" data-datafield="Phone" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="Fax" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Phone Toll-Free" data-datafield="PhoneTollFree" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Other" data-datafield="PhoneOther" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="email" class="fwcontrol fwformfield" data-caption="Email" data-datafield="Email" data-allcaps="false" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Status">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="DealStatusId" data-displayfield="DealStatus" data-validationname="DealStatusValidation" data-required="true" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Status Date" data-datafield="StatusAsOf" data-enabled="false" style="flex:1 1 225px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Expected Wrap Date" data-datafield="ExpectedWrapDate" style="flex:1 1 225px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- CONTACTS TAB -->
              <div data-type="tabpage" id="contactstabpage" class="tabpage" data-tabid="contactstab">
                <div class="formpage">
                  <div class="flexcolumn">
                    <div class="flexrow">
                      <div data-control="FwGrid" data-grid="CompanyContactGrid" data-securitycaption="Deal Contacts"></div>
                    </div>
                  </div>
                </div>
              </div>

             <!-- BILLING TAB -->
              <div data-type="tabpage" id="billingtabpage" class="tabpage" data-tabid="billingtab">
                <div class="formpage">
                  <div class="flexrow">
                    <div class="flexcolumn" style="flex:1 1 325px;">
                      <!-- Billing Address section -->  
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield  billing-type-radio" data-caption="Default Address" data-datafield="BillToAddressType"></div>
                        </div>
                        <div class="flexrow" style="margin-top:5px;">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 1" data-datafield="BillToAttention1" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow" style="margin-top:5px;">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention 2" data-datafield="BillToAttention2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="BillToAddress1" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="BillToAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="BillToCity" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="BillToState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="BillToZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="BillToCountryId" data-enabled="false" data-displayfield="BillToCountry" data-validationname="CountryValidation" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <!-- Commission section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Commission">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Sales Representative" data-datafield="OutsideSalesRepresentativeId" data-displayfield="OutsideSalesRepresentative" data-validationname="ContactValidation" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Invoice Percentage" data-datafield="CommissionRate" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Include Sub-Rentals" data-datafield="CommissionIncludesVendorItems" style="flex:1 1 150px;margin-top:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 325px;">
                    <!-- Billing Options section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Billing Options">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Billing Cycle" data-datafield="BillingCycleId" data-displayfield="BillingCycle" data-validationname="BillingCycleValidation" data-required="true" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Type" data-datafield="PaymentTypeId" data-displayfield="PaymentType" data-validationname="PaymentTypeValidation" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Payment Terms" data-datafield="PaymentTermsId" data-displayfield="PaymentTerms" data-validationname="PaymentTermsValidation" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Order Rate" data-datafield="DefaultRate" data-displayfield="DefaultRate" data-validationname="RateTypeValidation" data-validationpeek="false" style="flex:1 1 200px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" data-validationname="CurrencyValidation" class="fwcontrol fwformfield" data-caption="Currency Code" data-datafield="CurrencyId" data-displayfield="CurrencyCode" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Assess Finance Charge on Overdue Amount" data-datafield="AssessFinanceCharge" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Allow Order Billing Schedule Override" data-datafield="AllowBillingScheduleOverride" style="flex:1 1 275px;"></div>
                        </div>
                      </div>
                      <!-- Discount Template section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Discount Template">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Customer Template" data-datafield="UseCustomerDiscount" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Deal Template" data-datafield="UseDiscountTemplate" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Template" data-datafield="DiscountTemplateId" data-displayfield="DiscountTemplate" data-validationname="DiscountTemplateValidation" data-enabled="false" style="flex:1 1 200px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Status section -->
                    <div class="flexcolumn" style="flex:1 1 200px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PO">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Required" data-datafield="PoRequired" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Type" data-datafield="PoType"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- HIATUS TAB -->
              <div data-type="tabpage" id="hiatustabpage" class="tabpage" data-tabid="hiatustab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="DealHiatusDiscountGrid"></div>
                  </div>
                </div>
              </div>

             <!-- CREDIT TAB -->
              <div data-type="tabpage" id="credittabpage" class="tabpage" data-tabid="credittab">
                <div class="formpage">
                  <div class="flexrow">
                    <!-- Credit section -->
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Credit">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Customer Credit" data-datafield="UseCustomerCredit" style="flex:1 1 100px;padding-left:10px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Status" data-datafield="CreditStatusId" data-displayfield="CreditStatus" data-validationname="CreditStatusValidation" data-readonly="true" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Through Date" data-datafield="CreditStatusThrough" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                      <!-- Deal Credit section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Deal Credit">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount" data-dontsavedecimal="true" data-datafield="CreditLimit" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deal A/R Balance" data-datafield="CreditBalance" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Available" data-datafield="CreditAvailable" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Credit Application on File" data-datafield="CreditApplicationOnFile" style="flex:1 1 100px;padding-left:10px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unlimited Credit" data-datafield="UnlimitedCredit" style="flex:1 1 100px;padding-left:10px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <!-- Customer Credit section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Customer Credit">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount" data-datafield="CustomerCreditLimit" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Available" data-datafield="CustomerCreditAvailable" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Customer A/R Balance" data-datafield="CustomerCreditBalance" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                      <!-- Depleting Deposit section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Depleting Deposit">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deposit" data-datafield="DepletingDepositTotal" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Applied" data-datafield="DepletingDepositApplied" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="DepletingDepositRemaining" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:1 1 350px;">
                      <!-- Trade References section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Trade References">
                       <div class="flexrow">
                         <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Verified" data-datafield="TradeReferencesVerified" style="flex:1 1 275px;"></div>
                       </div>
                       <div class="flexrow">
                         <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Date Verified" data-datafield="TradeReferencesVerifiedOn" style="flex:1 1 150px;"></div>
                       </div>
                       <div class="flexrow">
                         <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Verified By" data-datafield="TradeReferencesVerifiedBy" style="flex:1 1 150px;"></div>
                       </div>
                      </div>
                      <!-- Responsible Party section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Responsible Party">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="On File" data-datafield="CreditResponsiblePartyOnFile" style="flex:1 1 100px;"></div>
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
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Customer Insurance" data-datafield="UseCustomerInsurance" style="flex:0 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield" data-caption="Valid Through" data-datafield="InsuranceCertificationValidThrough" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" data-digits="0" class="fwcontrol fwformfield" data-caption="Liability Amount" data-datafield="InsuranceCoverageLiability" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" data-digits="0" class="fwcontrol fwformfield" data-caption="Liability Deductible" data-datafield="InsuranceCoverageLiabilityDeductible" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" data-digits="0" class="fwcontrol fwformfield" data-caption="Property Value Amount" data-datafield="InsuranceCoverageProperty" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="number" data-digits="0" class="fwcontrol fwformfield" data-caption="Property Value Deductible" data-datafield="InsuranceCoveragePropertyDeductible" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Certification on File" data-datafield="InsuranceCertification" style="flex:0 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Vehicle Insurance on File" data-datafield="VehicleInsuranceCertification" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                      <!-- Security Deposit section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Security Deposits">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Total Amount" data-datafield="SecurityDepositAmount" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div class="fwformcontrol additems" data-type="button" style="flex:1 1 100px;margin:15px 5px 0px 5px;" data-enabled="false">Security Deposits</div>
                        </div>
                      </div>
                    </div>
                    <div class="flexcolumn" style="flex:0 1 425px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Insurance Vendor">
                        <!-- Insurance Vendor section -->
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Name" data-datafield="InsuranceCompanyId" data-displayfield="InsuranceCompany" data-validationname="VendorValidation" style="flex:1 1 325px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Agent" data-datafield="InsuranceCompanyAgent" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="InsuranceCompanyAddress1" data-enabled="false" style="flex:1 1 325px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="InsuranceCompanyAddress2" data-enabled="false" style="flex:1 1 300px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="InsuranceCompanyCity" data-enabled="false" style="flex:1 1 325px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="InsuranceCompanyState" data-enabled="false" style="flex:1 1 200px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="InsuranceCompanyZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="InsuranceCompanyCountryId" data-displayfield="InsuranceCompanyCountry" data-validationname="Country" data-enabled="false" style="flex:1 1 175px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Phone" data-datafield="InsuranceCompanyPhone" data-enabled="false" style="flex:1 1 125px;"></div>
                          <div data-control="FwFormField" data-type="phoneinternational" class="fwcontrol fwformfield" data-caption="Fax" data-datafield="InsuranceCompanyFax" data-enabled="false" style="flex:1 1 125px;"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <!-- TAX TAB -->
              <div data-type="tabpage" id="taxtabpage" class="tabpage" data-tabid="taxtab">
                <div class="formpage">
                  <div class="flexrow">
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:1 1 250px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Option">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Use Customer Tax" data-datafield="UseCustomerTax" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Status" data-datafield="Taxable"></div>
                          <!-- <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Taxable" data-datafield="Taxable" style="flex:1 1 125px;"></div> -->
                        </div>
                      </div>
                      <!-- Non-Taxable section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Non-Taxable">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield non-taxable" data-caption="Certificate No." data-datafield="NonTaxableCertificateNo" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield non-taxable" data-caption="Year" data-datafield="NonTaxableYear" style="flex:1 1 75px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="date" class="fwcontrol fwformfield non-taxable" data-caption="Valid Through" data-datafield="NonTaxableCertificateValidThrough" style="flex:1 1 125px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield non-taxable" data-caption="Certificate on File" data-datafield="NonTaxableCertificateOnFile" style="flex:1 1 175px;"></div>
                        </div>
                      </div>
                      <!-- Federal section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Federal Tax">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="State of Incorporation" data-datafield="TaxStateOfIncorporationId" data-displayfield="TaxStateOfIncorporation" data-validationname="StateValidation" style="flex:1 1 150px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Federal Tax No." data-datafield="TaxFederalNo" style="flex:1 1 150px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Location Tax Option section -->
                    <div class="flexcolumn" style="flex:1 1 600px;">
                      <!-- Location Tax Options section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Tax Rates">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyTaxOptionGrid" data-securitycaption="Tax Option"></div>
                        </div>
                      </div>
                      <!-- Resale section -->
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Resale Certificates" style="margin-top:-15px;">
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="CompanyResaleGrid" data-securitycaption="Deal Resale"></div>
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
                        <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Exclude Quote / Order Activity" data-datafield="DisableQuoteOrderActivity" data-enabled="true" style="flex:1 1 250px;"></div>
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
                <div class="formpage">
                  <div class="flexrow">
                    <!-- Customer section -->
                    <div class="flexcolumn" style="flex:1 1 300px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Shipping Address">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Default Address" data-datafield="ShippingAddressType"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Attention" data-datafield="ShipAttention" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 1" data-datafield="ShipAddress1" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Address 2" data-datafield="ShipAddress2" data-enabled="false" style="flex:1 1 250px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="City" data-datafield="ShipCity" data-enabled="false" style="flex:1 1 275px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="State" data-datafield="ShipState" data-enabled="false" style="flex:1 1 150px;"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Zip/Postal" data-datafield="ShipZipCode" data-enabled="false" style="flex:1 1 100px;"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Country" data-datafield="ShipCountryId" data-displayfield="ShipCountry" data-validationname="CountryValidation" data-enabled="false" style="float:left;width:175px;"></div>
                        </div>
                      </div>
                    </div>
                    <!-- Address section -->
                    <div class="flexcolumn" style="flex:1 1 500px;">
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Default Delivery">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Outgoing" data-datafield="DefaultOutgoingDeliveryType"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Incoming" data-datafield="DefaultIncomingDeliveryType"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwGrid" data-grid="DealShipperGrid" data-securitycaption="Deal Shipper"></div>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            
              <!-- QUOTE TAB -->
              <div data-type="tabpage" id="quotetabpage" class="tabpage submodule quote rwSubModule" data-tabid="quotetab"></div>
              
              <!-- ORDER TAB -->
              <div data-type="tabpage" id="ordertabpage" class="tabpage submodule order rwSubModule" data-tabid="ordertab"></div>

              <!-- CONTRACT TAB -->
              <div data-type="tabpage" id="contracttabpage" class="tabpage contractSubModule rwSubModule" data-tabid="contracttab"></div>
              
              <!-- INVOICE TAB -->
              <div data-type="tabpage" id="invoicetabpage" class="tabpage invoiceSubModule rwSubModule" data-tabid="invoicetab"></div>
              
              <!-- RECEIPT TAB -->
              <div data-type="tabpage" id="receipttabpage" class="tabpage receiptSubModule rwSubModule" data-tabid="receipttab"></div>
              
              <!-- CREDITS TAB -->
              <div data-type="tabpage" id="creditstabpage" class="tabpage submodule credits-page rwSubModule" data-tabid="creditstab"></div>

              <!-- DOCUMENTS TAB -->
              <div data-type="tabpage" id="documentstabpage" class="tabpage" data-tabid="documentstab">
                <div class="flexpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="DealDocumentGrid"></div>
                  </div>
                </div>
              </div>
              
              <!-- NOTES TAB -->
              <div data-type="tabpage" id="notestabpage" class="tabpage" data-tabid="notestab">
                <div class="formpage">
                  <div class="flexrow">
                    <div data-control="FwGrid" data-grid="DealNoteGrid" data-securitycaption="Deal Notes"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>`;
    }
}
//----------------------------------------------------------------------------------------------
var DealController = new Deal();