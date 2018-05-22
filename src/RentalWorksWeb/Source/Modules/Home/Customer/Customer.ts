routes.push({ pattern: /^module\/customer$/, action: function (match: RegExpExecArray) { return CustomerController.getModuleScreen(); } });
routes.push({ pattern: /^module\/customer\/(\S+)\/(\S+)/, action: function (match: RegExpExecArray) { var filter = { 'datafield': match[1], 'search': match[2].replace(/%20/g, ' ').replace(/%2f/g, '/') }; return CustomerController.getModuleScreen(filter); } });

class Customer {
    Module: string = 'Customer';
    apiurl: string = 'api/v1/customer';
    caption: string = 'Customer';
    thisModule: Customer;

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

    openBrowse() {
        var $browse: any = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form: any = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        var $submoduleDealBrowse = this.openDealBrowse($form);
        $form.find('.deal').append($submoduleDealBrowse);

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
    }

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

    loadForm(uniqueids: any) {
        var $form: any = this.openForm('EDIT');
        FwFormField.setValueByDataField($form, 'CustomerId', uniqueids.CustomerId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid: string = FwFormField.getValueByDataField($form, 'CustomerId');
        FwModule.loadAudit($form, uniqueid);
    }

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
    //--------------------
    beforeValidateInsuranceVendor($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');

        request.uniqueids = {
            Insurance: true
        }
    }
    //--------------------
    afterLoad($form: any) {
        var $customerResaleGrid: any = $form.find('[data-name="CompanyResaleGrid"]');
        FwBrowse.search($customerResaleGrid);

        var $customerNoteGrid: any = $form.find('[data-name="CustomerNoteGrid"]');
        FwBrowse.search($customerNoteGrid);

        var $companyTaxGrid: any = $form.find('[data-name="CompanyTaxOptionGrid"]');
        FwBrowse.search($companyTaxGrid);

        var $companyContactGrid: any = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);

        var $dealBrowse = $form.find('#DealBrowse');
        FwBrowse.search($dealBrowse);

        if (FwFormField.getValue($form, 'div[data-datafield="UseDiscountTemplate"]') === true) {
            FwFormField.enable($form.find('.discount-validation'));
        };

        this.addressTypeChange($form);
    }

    addressTypeChange($form) {
        if (FwFormField.getValue($form, '.billing_address_type') === 'CUSTOMER') {
            // Values from Customer fields in general tab
            FwFormField.setValue($form, '.billing_att1', "");
            FwFormField.setValue($form, '.billing_att2', "");
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
}
var CustomerController = new Customer();