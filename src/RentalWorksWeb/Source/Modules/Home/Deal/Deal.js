var Deal = (function () {
    function Deal() {
        this.Module = 'Deal';
        this.apiurl = 'api/v1/deal';
        this.caption = 'Deal';
    }
    Deal.prototype.getModuleScreen = function (filter) {
        var screen, $browse;
        var self = this;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, self.caption, false, 'BROWSE', true);
            if (typeof filter !== 'undefined') {
                var datafields = filter.datafield.split('%20');
                for (var i = 0; i < datafields.length; i++) {
                    datafields[i] = datafields[i].charAt(0).toUpperCase() + datafields[i].substr(1);
                }
                filter.datafield = datafields.join('');
                $browse.find('div[data-browsedatafield="' + filter.datafield + '"]').find('input').val(filter.search);
            }
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    Deal.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        return $browse;
    };
    Deal.prototype.events = function ($form) {
        var _this = this;
        $form.find('[data-name="CompanyTaxOptionGrid"]').data('onselectedrowchanged', function ($control, $tr) {
            try {
                _this.updateExternalInputsWithGridValues($tr);
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
        $form.find('.deal_address input').on('change', function ($tr) {
            _this.transferDealAddressValues($form);
        });
        $form.find('.shipping_address_type_radio').on('change', function ($tr) {
            _this.shippingAddressTypeChange($form);
        });
        $form.find('.billing_radio1').on('change', function ($tr) {
            _this.billingAddressTypeChange($form);
        });
        $form.on('change', '.billing_use_discount_template input[type=checkbox]', function (e) {
            _this.toggleBillingUseDiscount($form, jQuery(e.currentTarget).is(':checked'));
        });
        $form.on('change', '.billing_use_customer input[type=checkbox]', function (e) {
            _this.useCustomer(jQuery(e.currentTarget).is(':checked'));
        });
        $form.on('change', '.billing_radio1 input[type=radio]', function (e) {
            var val = jQuery(e.currentTarget).val() !== 'OTHER' ? true : false;
            _this.toggleBillingAddressInfo($form, val);
        });
        $form.on('change', '.shipping_address_type_radio input[type=radio]', function (e) {
            var val = jQuery(e.currentTarget).val() !== 'OTHER' ? true : false;
            _this.toggleShippingAddressInfo($form, val);
        });
        $form.on('change', '.credit_use_customer input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleCredTabIfUseCustomer($form, isChecked);
        });
        $form.on('change', '.insurance_use_customer input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleInsurTabIfUseCustomer($form, isChecked);
        });
        $form.on('change', '.tax_use_customer input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleTaxTabIfUseCustomer($form, isChecked);
        });
        $form.on('change', '.exlude_quote input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleOptionsTabIfExcludeQuote($form, isChecked);
        });
    };
    Deal.prototype.useCustomer = function (isChecked) {
        var $discTemp = jQuery('.billing_use_discount_template'), $useCust = jQuery('.billing_use_customer'), $temp = jQuery('.billing_template');
        if (isChecked) {
            $temp.attr('data-enabled', 'false');
            $temp.find('input').prop('disabled', true);
            $discTemp.attr('data-enabled', 'false');
            $discTemp.find('input').prop('disabled', true);
        }
        else {
            if ($discTemp.find('input[type=checkbox]').is(':checked')) {
                $temp.attr('data-enabled', 'true');
                $temp.find('input').prop('disabled', false);
            }
            else {
                $temp.attr('data-enabled', 'false');
                $temp.find('input').prop('disabled', true);
            }
            $discTemp.attr('data-enabled', 'true');
            $discTemp.find('input').prop('disabled', false);
        }
    };
    Deal.prototype.toggleBillingUseDiscount = function ($form, isDiscountTemplate) {
        var list = ['DiscountTemplateId'];
        isDiscountTemplate ? this.enableFields($form, list) : this.disableFields($form, list);
    };
    Deal.prototype.toggleBillingAddressInfo = function ($form, isOther) {
        var list = [
            'BillToAddress1',
            'BillToAddress2',
            'BillToCity',
            'BillToState',
            'BillToZipCode',
            'BillToCountryId'
        ];
        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
    };
    Deal.prototype.toggleShippingAddressInfo = function ($form, isOther) {
        var list = [
            'ShipAddress1',
            'ShipAddress2',
            'ShipCity',
            'ShipState',
            'ShipZipCode',
            'ShipCountryId'
        ];
        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
    };
    Deal.prototype.toggleCredTabIfUseCustomer = function ($form, isCustomer) {
        var list = ['CreditStatusId',
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
        ];
        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);
    };
    Deal.prototype.toggleInsurTabIfUseCustomer = function ($form, isCustomer) {
        var list = ['InsuranceCertificationValidThrough',
            'InsuranceCoverageLiability',
            'InsuranceCoverageLiabilityDeductible',
            'InsuranceCertification',
            'InsuranceCoverageProperty',
            'InsuranceCoveragePropertyDeductible',
            'SecurityDepositAmount',
            'VehicleInsuranceCertification',
            'InsuranceCompany',
            'InsuranceCompanyAgent'];
        var $insuranceName = jQuery('.insurance_name');
        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);
        if (isCustomer) {
            $insuranceName.attr('data-enabled', 'false');
            $insuranceName.find('input').prop('disabled', true);
        }
        else {
            $insuranceName.attr('data-enabled', 'true');
            $insuranceName.find('input').prop('disabled', false);
        }
    };
    Deal.prototype.disableInsurCompanyInfo = function ($form) {
        var list = ['InsuranceCompanyAddress1',
            'InsuranceCompanyAddress2',
            'InsuranceCompanyCity',
            'InsuranceCompanyState',
            'InsuranceCompanyZipCode',
            'InsuranceCompanyCountryId',
            'InsuranceCompanyPhone',
            'InsuranceCompanyFax'];
        this.disableFields($form, list);
    };
    Deal.prototype.toggleTaxTabIfUseCustomer = function ($form, isCustomer) {
        var list = ['Taxable',
            'TaxStateOfIncorporationId',
            'TaxFederalNo',
            'NonTaxableCertificateNo',
            'NonTaxableYear',
            'NonTaxableCertificateValidThrough',
            'NonTaxableCertificateOnFile'];
        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);
    };
    Deal.prototype.toggleOptionsTabIfExcludeQuote = function ($form, isExcluded) {
        var list = ['DisableRental',
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
    };
    Deal.prototype.billingAddressTypeChange = function ($form) {
        if (FwFormField.getValue($form, '.billing_radio1') === 'CUSTOMER') {
            var customerId = FwFormField.getValueByDataField($form, 'CustomerId');
            FwAppData.apiMethod(true, 'GET', "api/v1/customer/" + customerId, null, FwServices.defaultTimeout, function onSuccess(res) {
                FwFormField.disable($form.find('.billing_att1'));
                FwFormField.disable($form.find('.billing_att2'));
                FwFormField.setValue($form, '.billing_att1', "");
                FwFormField.setValue($form, '.billing_att2', "");
                FwFormField.setValue($form, '.billing_add1', "");
                FwFormField.setValue($form, '.billing_add2', "");
                FwFormField.setValue($form, '.billing_city', "");
                FwFormField.setValue($form, '.billing_state', "");
                FwFormField.setValue($form, '.billing_zip', "");
                FwFormField.setValue($form, 'div[data-displayfield="BillToCountry"]', "", "");
                FwFormField.setValue($form, '.billing_att1', res.BillToAttention1);
                FwFormField.setValue($form, '.billing_att2', res.BillToAttention2);
                FwFormField.setValue($form, '.billing_add1', res.BillToAddress1);
                FwFormField.setValue($form, '.billing_add2', res.BillToAddress2);
                FwFormField.setValue($form, '.billing_city', res.BillToCity);
                FwFormField.setValue($form, '.billing_state', res.BillToState);
                FwFormField.setValue($form, '.billing_zip', res.BillToZipCode);
                FwFormField.setValue($form, 'div[data-displayfield="BillToCountry"]', res.BillToCountryId, res.BillToCountry);
            }, null, $form);
        }
        if (FwFormField.getValue($form, '.billing_radio1') === 'DEAL') {
            FwFormField.setValue($form, '.billing_att1', "");
            FwFormField.setValue($form, '.billing_att2', "");
            FwFormField.setValue($form, '.billing_add1', "");
            FwFormField.setValue($form, '.billing_add2', "");
            FwFormField.setValue($form, '.billing_city', "");
            FwFormField.setValue($form, '.billing_state', "");
            FwFormField.setValue($form, '.billing_zip', "");
            FwFormField.setValue($form, '.billing_country', "");
            FwFormField.enable($form.find('.billing_att1'));
            FwFormField.enable($form.find('.billing_att2'));
            FwFormField.setValue($form, '.billing_add1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValue($form, '.billing_add2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValue($form, '.billing_city', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValue($form, '.billing_state', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValue($form, '.billing_zip', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValue($form, 'div[data-displayfield="BillToCountry"]', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }
        if (FwFormField.getValue($form, '.billing_radio1') === 'OTHER') {
            FwFormField.enable($form.find('.billing_att1'));
            FwFormField.enable($form.find('.billing_att2'));
        }
    };
    Deal.prototype.shippingAddressTypeChange = function ($form) {
        if (FwFormField.getValue($form, '.shipping_address_type_radio') === 'CUSTOMER') {
            var customerId = FwFormField.getValueByDataField($form, 'CustomerId');
            FwAppData.apiMethod(true, 'GET', "api/v1/customer/" + customerId, null, FwServices.defaultTimeout, function onSuccess(res) {
                FwFormField.disable($form.find('.shipping_att'));
                FwFormField.setValue($form, '.shipping_att', "");
                FwFormField.setValue($form, '.shipping_add1', "");
                FwFormField.setValue($form, '.shipping_add2', "");
                FwFormField.setValue($form, '.shipping_city', "");
                FwFormField.setValue($form, '.shipping_state', "");
                FwFormField.setValue($form, '.shipping_zip', "");
                FwFormField.setValue($form, 'div[data-displayfield="ShipCountry"]', "", "");
                FwFormField.setValue($form, '.shipping_att', res.ShipAttention);
                FwFormField.setValue($form, '.shipping_add1', res.ShipAddress1);
                FwFormField.setValue($form, '.shipping_add2', res.ShipAddress2);
                FwFormField.setValue($form, '.shipping_city', res.ShipCity);
                FwFormField.setValue($form, '.shipping_state', res.ShipState);
                FwFormField.setValue($form, '.shipping_zip', res.ShipZipCode);
                FwFormField.setValue($form, 'div[data-displayfield="ShipCountry"]', res.ShipCountryId, res.ShipCountry);
            }, null, $form);
        }
        if (FwFormField.getValue($form, '.shipping_address_type_radio') === 'PROJECT') {
            FwFormField.setValue($form, '.shipping_att', "");
            FwFormField.setValue($form, '.shipping_add1', "");
            FwFormField.setValue($form, '.shipping_add2', "");
            FwFormField.setValue($form, '.shipping_city', "");
            FwFormField.setValue($form, '.shipping_state', "");
            FwFormField.setValue($form, '.shipping_zip', "");
            FwFormField.setValue($form, 'div[data-displayfield="ShipCountry"]', "", "");
            FwFormField.enable($form.find('.shipping_att'));
            FwFormField.setValue($form, '.shipping_add1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValue($form, '.shipping_add2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValue($form, '.shipping_city', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValue($form, '.shipping_state', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValue($form, '.shipping_zip', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValue($form, 'div[data-displayfield="ShipCountry"]', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }
        if (FwFormField.getValue($form, '.shipping_address_type_radio') === 'OTHER') {
            FwFormField.enable($form.find('.shipping_att'));
        }
    };
    Deal.prototype.transferDealAddressValues = function ($form) {
        if (FwFormField.getValue($form, '.billing_radio1') === 'DEAL') {
            FwFormField.setValue($form, '.billing_add1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValue($form, '.billing_add2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValue($form, '.billing_city', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValue($form, '.billing_state', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValue($form, '.billing_zip', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValue($form, 'div[data-displayfield="BillToCountry"]', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }
        if (FwFormField.getValue($form, '.shipping_address_type_radio') === 'PROJECT') {
            FwFormField.setValue($form, '.shipping_add1', FwFormField.getValueByDataField($form, 'Address1'));
            FwFormField.setValue($form, '.shipping_add2', FwFormField.getValueByDataField($form, 'Address2'));
            FwFormField.setValue($form, '.shipping_city', FwFormField.getValueByDataField($form, 'City'));
            FwFormField.setValue($form, '.shipping_state', FwFormField.getValueByDataField($form, 'State'));
            FwFormField.setValue($form, '.shipping_zip', FwFormField.getValueByDataField($form, 'ZipCode'));
            FwFormField.setValue($form, 'div[data-displayfield="ShipCountry"]', FwFormField.getValueByDataField($form, 'CountryId'), FwFormField.getTextByDataField($form, 'CountryId'));
        }
    };
    Deal.prototype.disableFields = function ($form, fields) {
        fields.forEach(function (e, i) { FwFormField.disable($form.find('[data-datafield="' + e + '"]')); });
    };
    Deal.prototype.enableFields = function ($form, fields) {
        fields.forEach(function (e, i) { FwFormField.enable($form.find('[data-datafield="' + e + '"]')); });
    };
    Deal.prototype.updateExternalInputsWithGridValues = function ($tr) {
        $tr.find('.column > .field').each(function (i, e) {
            var $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');
            if (value == undefined || null) {
                jQuery("." + id).find(':input').val(0);
            }
            else {
                jQuery("." + id).find(':input').val(value);
            }
        });
    };
    Deal.prototype.renderGrids = function ($form) {
        var $resaleGrid, $resaleControl, $taxOptionGrid, $taxOptionControl, $contactGrid, $contactControl, $dealNoteGrid, $dealNoteControl, $vendorGrid, $vendorControl;
        $resaleGrid = $form.find('div[data-grid="CompanyResaleGrid"]');
        $resaleControl = jQuery(jQuery('#tmpl-grids-CompanyResaleGridBrowse').html());
        $resaleGrid.empty().append($resaleControl);
        $resaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            };
        });
        $resaleControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($resaleControl);
        FwBrowse.renderRuntimeHtml($resaleControl);
        $taxOptionGrid = $form.find('div[data-grid="CompanyTaxOptionGrid"]');
        $taxOptionControl = jQuery(jQuery('#tmpl-grids-CompanyTaxOptionGridBrowse').html());
        $taxOptionGrid.empty().append($taxOptionControl);
        $taxOptionControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            };
        });
        FwBrowse.init($taxOptionControl);
        FwBrowse.renderRuntimeHtml($taxOptionControl);
        $contactGrid = $form.find('div[data-grid="ContactGrid"]');
        $contactControl = jQuery(jQuery('#tmpl-grids-ContactGridBrowse').html());
        $contactGrid.empty().append($contactControl);
        $contactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContactId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            };
        });
        FwBrowse.init($contactControl);
        FwBrowse.renderRuntimeHtml($contactControl);
        $dealNoteGrid = $form.find('div[data-grid="DealNoteGrid"]');
        $dealNoteControl = jQuery(jQuery('#tmpl-grids-DealNoteGridBrowse').html());
        $dealNoteGrid.empty().append($dealNoteControl);
        $dealNoteControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DealId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            };
        });
        $dealNoteControl.data('beforesave', function (request) {
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($dealNoteControl);
        FwBrowse.renderRuntimeHtml($dealNoteControl);
        $vendorGrid = $form.find('div[data-grid="DealShipperGrid"]');
        $vendorControl = jQuery(jQuery('#tmpl-grids-DealShipperGridBrowse').html());
        $vendorGrid.empty().append($vendorControl);
        $vendorControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DealId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            };
        });
        $vendorControl.data('beforesave', function (request) {
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($vendorControl);
        FwBrowse.renderRuntimeHtml($vendorControl);
        var nameCompanyContactGrid = 'CompanyContactGrid';
        var $companyContactGrid = $companyContactGrid = $form.find('div[data-grid="' + nameCompanyContactGrid + '"]');
        var $companyContactControl = FwBrowse.loadGridFromTemplate(nameCompanyContactGrid);
        $companyContactGrid.empty().append($companyContactControl);
        $companyContactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: FwFormField.getValueByDataField($form, 'DealId')
            };
        });
        $companyContactControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($companyContactControl);
        FwBrowse.renderRuntimeHtml($companyContactControl);
    };
    Deal.prototype.openForm = function (mode, parentmoduleinfo) {
        var $form, $submoduleQuoteBrowse, $submoduleOrderBrowse;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        $submoduleQuoteBrowse = this.openQuoteBrowse($form);
        $form.find('.quote').append($submoduleQuoteBrowse);
        FwFormField.disable($form.find('.CompanyResaleGrid'));
        $submoduleOrderBrowse = this.openOrderBrowse($form);
        $form.find('.order').append($submoduleOrderBrowse);
        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleQuoteBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var $quoteForm, controller, $browse, quoteFormData = {};
            $browse = jQuery(this).closest('.fwbrowse');
            controller = $browse.attr('data-controller');
            quoteFormData.DealId = FwFormField.getValueByDataField($form, 'DealId');
            quoteFormData.Deal = FwFormField.getValueByDataField($form, 'Deal');
            if (typeof window[controller] !== 'object')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function')
                throw 'Missing javascript function: ' + controller + '.openForm';
            $quoteForm = window[controller]['openForm']('NEW', quoteFormData);
            FwModule.openSubModuleTab($browse, $quoteForm);
        });
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').off('click');
        $submoduleOrderBrowse.find('div.btn[data-type="NewMenuBarButton"]').on('click', function () {
            var $orderForm, controller, $browse, orderFormData = {};
            $browse = jQuery(this).closest('.fwbrowse');
            controller = $browse.attr('data-controller');
            orderFormData.DealId = FwFormField.getValueByDataField($form, 'DealId');
            orderFormData.Deal = FwFormField.getValueByDataField($form, 'Deal');
            if (typeof window[controller] !== 'object')
                throw 'Missing javascript module: ' + controller;
            if (typeof window[controller]['openForm'] !== 'function')
                throw 'Missing javascript function: ' + controller + '.openForm';
            $orderForm = window[controller]['openForm']('NEW', orderFormData);
            FwModule.openSubModuleTab($browse, $orderForm);
        });
        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);
        this.events($form);
        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValue($form, 'div[data-datafield="CustomerId"]', parentmoduleinfo.CustomerId, parentmoduleinfo.Customer);
        }
        return $form;
    };
    Deal.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DealId"] input').val(uniqueids.DealId);
        FwModule.loadForm(this.Module, $form);
        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);
        return $form;
    };
    Deal.prototype.openQuoteBrowse = function ($form) {
        var $browse;
        $browse = QuoteController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.ActiveView = QuoteController.ActiveView;
            request.uniqueids = {
                DealId: $form.find('[data-datafield="DealId"] input.fwformfield-value').val()
            };
        });
        return $browse;
    };
    ;
    Deal.prototype.openOrderBrowse = function ($form) {
        var $browse;
        $browse = OrderController.openBrowse();
        $browse.data('ondatabind', function (request) {
            request.ActiveView = OrderController.ActiveView;
            request.uniqueids = {
                DealId: $form.find('[data-datafield="DealId"] input.fwformfield-value').val()
            };
        });
        return $browse;
    };
    ;
    Deal.prototype.saveForm = function ($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    };
    Deal.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="DealId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    Deal.prototype.afterLoad = function ($form) {
        var $resaleGrid, $taxOptionGrid, $contactGrid, $dealNoteGrid, $vendorGrid;
        var $quoteBrowse = $form.find('#QuoteBrowse');
        FwBrowse.search($quoteBrowse);
        var $orderBrowse = $form.find('#OrderBrowse');
        FwBrowse.search($orderBrowse);
        $resaleGrid = $form.find('[data-name="CompanyResaleGrid"]');
        FwBrowse.search($resaleGrid);
        $taxOptionGrid = $form.find('[data-name="CompanyTaxOptionGrid"]');
        FwBrowse.search($taxOptionGrid);
        $contactGrid = $form.find('[data-name="ContactGrid"]');
        FwBrowse.search($contactGrid);
        $dealNoteGrid = $form.find('[data-name="DealNoteGrid"]');
        FwBrowse.search($dealNoteGrid);
        $vendorGrid = $form.find('[data-name="DealShipperGrid"]');
        FwBrowse.search($vendorGrid);
        var $companyContactGrid = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);
        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);
        this.toggleBillingUseDiscount($form, FwFormField.getValueByDataField($form, 'UseDiscountTemplate'));
        this.useCustomer(FwFormField.getValueByDataField($form, 'UseCustomerDiscount'));
        var val_bill = FwFormField.getValueByDataField($form, 'BillToAddressType') !== 'OTHER' ? true : false;
        this.toggleBillingAddressInfo($form, val_bill);
        var val_ship = FwFormField.getValueByDataField($form, 'ShippingAddressType') !== 'OTHER' ? true : false;
        this.toggleShippingAddressInfo($form, val_ship);
        this.toggleInsurTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerInsurance'));
        this.toggleCredTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerCredit'));
        this.disableInsurCompanyInfo($form);
        this.toggleTaxTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerTax'));
        this.toggleOptionsTabIfExcludeQuote($form, FwFormField.getValueByDataField($form, 'DisableQuoteOrderActivity'));
        this.billingAddressTypeChange($form);
        this.shippingAddressTypeChange($form);
        this.transferDealAddressValues($form);
        if (FwFormField.getValueByDataField($form, 'UseCustomerTax') === true) {
            FwFormField.disable($form.find('div[data-name="CompanyResaleGrid"]'));
            FwFormField.disable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
        }
        else {
            FwFormField.enable($form.find('div[data-name="CompanyResaleGrid"]'));
            FwFormField.enable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
        }
        $form.find('[data-datafield="UseCustomerTax"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.disable($form.find('div[data-name="CompanyResaleGrid"]'));
                FwFormField.disable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
            }
            else {
                FwFormField.enable($form.find('div[data-name="CompanyResaleGrid"]'));
                FwFormField.enable($form.find('div[data-name="CompanyTaxOptionGrid"]'));
            }
        });
    };
    return Deal;
}());
var DealController = new Deal();
//# sourceMappingURL=Deal.js.map