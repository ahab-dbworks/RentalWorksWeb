routes.push({ pattern: /^module\/customer$/, action: function (match: RegExpExecArray) { return CustomerController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customer\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return CustomerController.getModuleScreen(filter); } });

class Customer {
    Module: string = 'Customer';
    apiurl: string = 'api/v1/customer';
    caption: string = 'Customer';
    thisModule: Customer;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        var self = this;
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        var $browse: any = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);

            if (typeof filter !== 'undefined') {
                var datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('')
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
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

    updateExternalInputsWithGridValues($tr: JQuery): void {
        $tr.find('.column > .field').each((i, e) => {
            var $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
            if (value === undefined || null) {
                jQuery('.' + id).find(':input').val(0);
            } else {
                jQuery('.' + id).find(':input').val(value);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        var $browse: any = FwBrowse.loadBrowseFromTemplate(this.Module);
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
        var $form: any = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        let $submoduleQuoteBrowse = this.openQuoteBrowse($form);
        $form.find('.quote-page').append($submoduleQuoteBrowse);
        let $submoduleOrderBrowse = this.openOrderBrowse($form);
        $form.find('.order-page').append($submoduleOrderBrowse);

        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var $quoteForm, controller, $browse, quoteFormData: any = {};
            $browse = jQuery(this).closest('.fwbrowse');
            controller = $browse.attr('data-controller');
            quoteFormData.DealId = FwFormField.getValueByDataField($form, 'DealId');
            quoteFormData.Deal = FwFormField.getValueByDataField($form, 'Deal');
            quoteFormData.RateTypeId = FwFormField.getValueByDataField($form, 'DefaultRate');
            quoteFormData.RateType = FwFormField.getTextByDataField($form, 'DefaultRate');
            quoteFormData.BillingCycleId = FwFormField.getValueByDataField($form, 'BillingCycleId');
            quoteFormData.BillingCycle = FwFormField.getTextByDataField($form, 'BillingCycleId');
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
            $quoteForm = window[controller]['openForm']('NEW', quoteFormData);
            FwModule.openSubModuleTab($browse, $quoteForm);
        });

        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var $orderForm, controller, $browse, orderFormData: any = {};
            $browse = jQuery(this).closest('.fwbrowse');
            controller = $browse.attr('data-controller');
            orderFormData.DealId = FwFormField.getValueByDataField($form, 'DealId');
            orderFormData.Deal = FwFormField.getValueByDataField($form, 'Deal');
            orderFormData.RateTypeId = FwFormField.getValueByDataField($form, 'DefaultRate');
            orderFormData.RateType = FwFormField.getTextByDataField($form, 'DefaultRate');
            orderFormData.BillingCycleId = FwFormField.getValueByDataField($form, 'BillingCycleId');
            orderFormData.BillingCycle = FwFormField.getTextByDataField($form, 'BillingCycleId');
            if (typeof window[controller] !== 'object') throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function') throw 'Missing javascript function: ' + controller + '.openForm';
            $orderForm = window[controller]['openForm']('NEW', orderFormData);
            FwModule.openSubModuleTab($browse, $orderForm);
        });

        if (mode === 'NEW') {
            let officeLocation = JSON.parse(sessionStorage.getItem('location'));
            let customerStatus = JSON.parse(sessionStorage.getItem('controldefaults'));
            FwFormField.setValue($form, 'div[data-datafield="OfficeLocationId"]', officeLocation.locationid, officeLocation.location);
            FwFormField.setValue($form, 'div[data-datafield="CustomerStatusId"]', customerStatus.defaultcustomerstatusid, customerStatus.defaultcustomerstatus);
        }

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

        return $form;
    }
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
        //FwBrowse.search($customerResaleGrid);

        var $customerNoteGrid: any = $form.find('[data-name="CustomerNoteGrid"]');
        //FwBrowse.search($customerNoteGrid);

        var $companyTaxGrid: any = $form.find('[data-name="CompanyTaxOptionGrid"]');
        //FwBrowse.search($companyTaxGrid);

        var $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');
        //FwBrowse.search($companyContactGrid);

        let $dealBrowse = $form.find('#DealBrowse');
        FwBrowse.search($dealBrowse);
        let $orderBrowse = $form.find('#OrderBrowse');
        FwBrowse.search($orderBrowse);
        let $quoteBrowse = $form.find('#QuoteBrowse');
        FwBrowse.search($quoteBrowse);

        if (FwFormField.getValue($form, 'div[data-datafield="UseDiscountTemplate"]') === true) {
            FwFormField.enable($form.find('.discount-validation'));
        };

        //open Deal browse submodule
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
         // Open Quote submodule browse
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
        // Open Order submodule browse
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

        //Click Event on tabs to load grids/browses
        $form.on('click', '[data-type="tab"]', e => {
            let tabname = jQuery(e.currentTarget).attr('id');
            let lastIndexOfTab = tabname.lastIndexOf('tab');
            let tabpage = tabname.substring(0, lastIndexOfTab) + 'tabpage' + tabname.substring(lastIndexOfTab + 3);

            let $gridControls = $form.find(`#${tabpage} [data-type="Grid"]`);
            if ($gridControls.length > 0) {
                for (let i = 0; i < $gridControls.length; i++) {
                    let $gridcontrol = jQuery($gridControls[i]);
                    FwBrowse.search($gridcontrol);
                }
            }

            let $browseControls = $form.find(`#${tabpage} [data-type="Browse"]`);
            if ($browseControls.length > 0) {
                for (let i = 0; i < $browseControls.length; i++) {
                    let $browseControl = jQuery($browseControls[i]);
                    FwBrowse.search($browseControl);
                }
            }
        });

        this.addressTypeChange($form);
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
            request.ActiveView = QuoteController.ActiveView;
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
            request.ActiveView = OrderController.ActiveView;
            request.uniqueids = {
                CustomerId: $form.find('[data-datafield="CustomerId"] input.fwformfield-value').val()
            }
        });

        return $browse;
    };
}
var CustomerController = new Customer();