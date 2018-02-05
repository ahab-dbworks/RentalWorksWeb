var Deal = /** @class */ (function () {
    function Deal() {
        this.Module = 'Deal';
        this.apiurl = 'api/v1/deal';
    }
    Deal.prototype.getModuleScreen = function (filter) {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Deal', false, 'BROWSE', true);
            if (typeof filter !== 'undefined') {
                filter.search = filter.search.replace(/%20/, ' ');
                filter.datafield = filter.datafield.split('%20');
                for (var i = 0; i < filter.datafield.length; i++) {
                    filter.datafield[i] = filter.datafield[i].charAt(0).toUpperCase() + filter.datafield[i].substr(1);
                }
                filter.datafield = filter.datafield.join('');
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
        FwBrowse.init($browse);
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
        $form.on('change', '.billing_use_discount_template input[type=checkbox]', function (e) {
            //this.useDiscountTemplate(jQuery(e.currentTarget).is(':checked'));            
            _this.toggleBillingUseDiscount($form, jQuery(e.currentTarget).is(':checked'));
        });
        $form.on('change', '.billing_use_customer input[type=checkbox]', function (e) {
            _this.useCustomer(jQuery(e.currentTarget).is(':checked'));
        });
        $form.on('change', '.billing_radio1 input[type=radio]', function (e) {
            var val = jQuery(e.currentTarget).val() !== 'OTHER' ? true : false;
            _this.toggleBillingAddressInfo($form, val);
        });
        $form.on('change', '.credit_use_customer input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleCredTabIfUseCustomer($form, isChecked);
        });
        $form.on('change', '.insurance_use_customer input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleInsurTabIfUseCustomer($form, isChecked);
        });
        //$form.on('change', '.billing_potype input[type=radio]', (e) => {
        //    FwFormField.setValue($form, jQuery(e.currentTarget))
        //});
        $form.on('change', '.tax_use_customer input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleTaxTabIfUseCustomer($form, isChecked);
        });
        $form.on('change', '.exlude_quote input[type=checkbox]', function (e) {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            _this.toggleOptionsTabIfExcludeQuote($form, isChecked);
        });
    };
    //useDiscountTemplate(isChecked: boolean): void {
    //    var $temp: JQuery = jQuery('.billing_template');
    //    // DiscountTemplateId
    //    if (!isChecked) {
    //        $temp.attr('data-enabled', 'false');
    //        $temp.find('input').prop('disabled', true);
    //    } else {
    //        $temp.attr('data-enabled', 'true');
    //        $temp.find('input').prop('disabled', false);
    //    }
    //}
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
            'BillToAttention1',
            'BillToAttention2',
            'BillToAddress1',
            'BillToAddress2',
            'BillToCity',
            'BillToState',
            'BillToZipCode',
            'BillToCountryId'
        ];
        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
    };
    Deal.prototype.toggleCredTabIfUseCustomer = function ($form, isCustomer) {
        var list = ['CreditStatusId',
            'CreditStatusThrough',
            'CreditLimit',
            'CreditAvailable',
            'CreditBalance',
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
                jQuery('.' + id).find(':input').val(0);
            }
            else {
                jQuery('.' + id).find(':input').val(value);
            }
        });
    };
    Deal.prototype.renderGrids = function ($form) {
        console.log($form);
        var $resaleGrid, $resaleControl, $taxOptionGrid, $taxOptionControl, $contactGrid, $contactControl, $dealNotesGrid, $dealNotesControl, $vendorGrid, $vendorControl;
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
        $dealNotesGrid = $form.find('div[data-grid="DealNotesGrid"]');
        $dealNotesControl = jQuery(jQuery('#tmpl-grids-DealNotesGridBrowse').html());
        $dealNotesGrid.empty().append($dealNotesControl);
        $dealNotesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DealId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            };
        });
        $dealNotesControl.data('beforesave', function (request) {
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
        });
        FwBrowse.init($dealNotesControl);
        FwBrowse.renderRuntimeHtml($dealNotesControl);
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
        // ----------
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
    Deal.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        //$defaultrate = $form.find('.defaultrate');
        //FwFormField.loadItems($defaultrate, [
        //    { value: 'DAILY', text: 'Daily Rate' }
        //    , { value: 'WEEKLY', text: 'Weekly Rate' }
        //    , { value: 'MONTHLY', text: 'Monthly Rate' }
        //]);
        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);
        this.events($form);
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
    Deal.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    Deal.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="DealId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    Deal.prototype.afterLoad = function ($form) {
        var $resaleGrid, $taxOptionGrid, $contactGrid, $dealNotesGrid, $vendorGrid;
        $resaleGrid = $form.find('[data-name="CompanyResaleGrid"]');
        FwBrowse.search($resaleGrid);
        $taxOptionGrid = $form.find('[data-name="CompanyTaxOptionGrid"]');
        FwBrowse.search($taxOptionGrid);
        $contactGrid = $form.find('[data-name="ContactGrid"]');
        FwBrowse.search($contactGrid);
        $dealNotesGrid = $form.find('[data-name="DealNotesGrid"]');
        FwBrowse.search($dealNotesGrid);
        $vendorGrid = $form.find('[data-name="DealShipperGrid"]');
        FwBrowse.search($vendorGrid);
        var $companyContactGrid = $form.find('[data-name="CompanyContactGrid"]');
        FwBrowse.search($companyContactGrid);
        this.disableFields($form, ['DiscountTemplateId', 'DiscountTemplate']);
        //this.useDiscountTemplate(FwFormField.getValueByDataField($form, 'UseDiscountTemplate'));
        this.toggleBillingUseDiscount($form, FwFormField.getValueByDataField($form, 'UseDiscountTemplate'));
        this.useCustomer(FwFormField.getValueByDataField($form, 'UseCustomerDiscount'));
        var val_bill = FwFormField.getValueByDataField($form, 'BillToAddressType') !== 'OTHER' ? true : false;
        this.toggleBillingAddressInfo($form, val_bill);
        this.toggleInsurTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerInsurance'));
        this.toggleCredTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerCredit'));
        this.disableInsurCompanyInfo($form);
        this.toggleTaxTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerTax'));
        this.toggleOptionsTabIfExcludeQuote($form, FwFormField.getValueByDataField($form, 'DisableQuoteOrderActivity'));
    };
    return Deal;
}());
window.DealController = new Deal();
//# sourceMappingURL=Deal.js.map