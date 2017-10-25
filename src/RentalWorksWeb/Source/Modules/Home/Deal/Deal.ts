declare var FwModule: any;
declare var FwBrowse: any;

class Deal {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'Deal';
        this.apiurl = 'api/v1/deal';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Deal', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };

        return screen;
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);

        return $browse;
    }

    events($form: JQuery): void {

        $form.find('[data-name="CompanyTaxOptionGrid"]').data('onselectedrowchanged', ($control: JQuery, $tr: JQuery) => {
            try {
                this.updateExternalInputsWithGridValues($tr);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });

        
        $form.on('change', '.billing_use_discount_template input[type=checkbox]', (e) => {
            this.useDiscountTemplate(jQuery(e.currentTarget).is(':checked'));            
        });

        $form.on('change', '.billing_use_customer input[type=checkbox]', (e) => {
            this.useCustomer(jQuery(e.currentTarget).is(':checked'));
        });

        $form.on('change', '.billing_radio1 input[type=radio]', (e) => {
            var val = jQuery(e.currentTarget).val() !== 'OTHER' ? true : false;
            this.toggleBillingAddressInfo($form, val);
        });

        $form.on('change', '.credit_use_customer input[type=checkbox]', (e) => {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleCredTabIfUseCustomer($form, isChecked);
        });

        $form.on('change', '.insurance_use_customer input[type=checkbox]', (e) => {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleInsurTabIfUseCustomer($form, isChecked);
        });

        //$form.on('change', '.billing_potype input[type=radio]', (e) => {
        //    FwFormField.setValue($form, jQuery(e.currentTarget))
        //});

        $form.on('change', '.tax_use_customer input[type=checkbox]', (e) => {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleTaxTabIfUseCustomer($form, isChecked);
        });

        $form.on('change', '.exlude_quote input[type=checkbox]', (e) => {
            var isChecked = jQuery(e.currentTarget).is(':checked');
            this.toggleOptionsTabIfExcludeQuote($form, isChecked);
        });

    }

    useDiscountTemplate(isChecked: boolean): void {
        var $temp: JQuery = jQuery('.billing_template');

        if (isChecked) {
            $temp.attr('data-enabled', 'false');
            $temp.find('input').prop('disabled', true);
        } else {
            $temp.attr('data-enabled', 'true');
            $temp.find('input').prop('disabled', false);
        }
    }

    useCustomer(isChecked: boolean): void {
        var $discTemp: JQuery = jQuery('.billing_use_discount_template'),
            $useCust: JQuery = jQuery('.billing_use_customer'),
            $temp: JQuery = jQuery('.billing_template');

        if (isChecked) {
            $temp.attr('data-enabled', 'false');
            $temp.find('input').prop('disabled', true);
            $discTemp.attr('data-enabled', 'false');
            $discTemp.find('input').prop('disabled', true);

        } else {
            if (!$discTemp.find('input[type=checkbox]').is(':checked')) {
                $temp.attr('data-enabled', 'true');
                $temp.find('input').prop('disabled', false);
            }
            $discTemp.attr('data-enabled', 'true');
            $discTemp.find('input').prop('disabled', false);
        }
    }

    toggleBillingAddressInfo($form: JQuery, isOther: boolean) {
        var list = ['BillToAttention1',
            'BillToAttention2',
            'BillToAddress1',
            'BillToAddress2',
            'BillToCity',
            'BillToState',
            'BillToZipCode',
            'BillToCountryId'];

        isOther ? this.disableFields($form, list) : this.enableFields($form, list);
        
    }

    toggleCredTabIfUseCustomer($form: JQuery, isCustomer: boolean): void {
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
            'DepletingDepositThresholdPercent',
            'DepletingDepositTotal',
            'DepletingDepositApplied',
            'DepletingDepositRemaining'];

        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);            
        
    }

    toggleInsurTabIfUseCustomer($form: JQuery, isCustomer: boolean): void {
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

        var $insuranceName: JQuery = jQuery('.insurance_name');

        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);    

        if (isCustomer) {
            $insuranceName.attr('data-enabled', 'false');
            $insuranceName.find('input').prop('disabled', true);
        } else {
            $insuranceName.attr('data-enabled', 'true');
            $insuranceName.find('input').prop('disabled', false);
        }
    }

    disableInsurCompanyInfo($form: JQuery): void {
        var list = ['InsuranceCompanyAddress1',
            'InsuranceCompanyAddress2',
            'InsuranceCompanyCity',
            'InsuranceCompanyState',
            'InsuranceCompanyZipCode',
            'InsuranceCompanyCountryId',
            'InsuranceCompanyPhone',
            'InsuranceCompanyFax'];
        this.disableFields($form, list);
    }

    toggleTaxTabIfUseCustomer($form: JQuery, isCustomer: boolean): void {
        var list = ['Taxable',
            'TaxStateOfIncorporationId',
            'TaxFederalNo',
            'NonTaxableCertificateNo',
            'NonTaxableYear',
            'NonTaxableCertificateValidThrough',
            'NonTaxableCertificateOnFile'];

        isCustomer ? this.disableFields($form, list) : this.enableFields($form, list);            
    }

    toggleOptionsTabIfExcludeQuote($form: JQuery, isExcluded: boolean): void {
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
    }

    disableFields($form: JQuery, fields: string[]): void {
        fields.forEach((e, i) => { FwFormField.disable($form.find('[data-datafield="' + e + '"]'));});
    }

    enableFields($form: JQuery, fields: string[]): void {
        fields.forEach((e, i) => { FwFormField.enable($form.find('[data-datafield="' + e + '"]'));});
    }

    updateExternalInputsWithGridValues($tr: JQuery): void {
        $tr.find('.column > .field').each((i, e) => {
            var $column = jQuery(e), id = $column.attr('data-browsedatafield'), value = $column.attr('data-originalvalue');

            if (value == undefined || null) {
                jQuery('.' + id).find(':input').val(0);
            } else {
                jQuery('.' + id).find(':input').val(value);
            }

        });
    }

    renderGrids($form: any) {
        var $resaleGrid,
            $resaleControl,
            $taxOptionGrid,
            $taxOptionControl,
            $contactGrid,
            $contactControl,
            $dealNotesGrid,
            $dealNotesControl,
            $vendorGrid,
            $vendorControl;

        $resaleGrid = $form.find('div[data-grid="CompanyResaleGrid"]');
        $resaleControl = jQuery(jQuery('#tmpl-grids-CompanyResaleGridBrowse').html());
        $resaleGrid.empty().append($resaleControl);
        $resaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            }
        });
        $resaleControl.data('beforesave', function (request) {
            request.CompanyId = FwFormField.getValueByDataField($form, 'DealId')
        });
        FwBrowse.init($resaleControl);
        FwBrowse.renderRuntimeHtml($resaleControl);

        $taxOptionGrid = $form.find('div[data-grid="CompanyTaxOptionGrid"]');
        $taxOptionControl = jQuery(jQuery('#tmpl-grids-CompanyTaxOptionGridBrowse').html());
        $taxOptionGrid.empty().append($taxOptionControl);
        $taxOptionControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            }
        });
        FwBrowse.init($taxOptionControl);
        FwBrowse.renderRuntimeHtml($taxOptionControl);

        $contactGrid = $form.find('div[data-grid="ContactGrid"]');
        $contactControl = jQuery(jQuery('#tmpl-grids-ContactGridBrowse').html());
        $contactGrid.empty().append($contactControl);
        $contactControl.data('ondatabind', function (request) {
            request.uniqueids = {
                ContactId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            }
        });
        FwBrowse.init($contactControl);
        FwBrowse.renderRuntimeHtml($contactControl);

        $dealNotesGrid = $form.find('div[data-grid="DealNotesGrid"]');
        $dealNotesControl = jQuery(jQuery('#tmpl-grids-DealNotesGridBrowse').html());
        $dealNotesGrid.empty().append($dealNotesControl);
        $dealNotesControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DealId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            }
        });
        $dealNotesControl.data('beforesave', function (request) {
            request.DealId = FwFormField.getValueByDataField($form, 'DealId');
        })
        FwBrowse.init($dealNotesControl);
        FwBrowse.renderRuntimeHtml($dealNotesControl);

        $vendorGrid = $form.find('div[data-grid="DealShipperGrid"]');
        $vendorControl = jQuery(jQuery('#tmpl-grids-DealShipperGridBrowse').html());
        $vendorGrid.empty().append($vendorControl);
        $vendorControl.data('ondatabind', function (request) {
            request.uniqueids = {
                DealId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            }
        });
        FwBrowse.init($vendorControl);
        FwBrowse.renderRuntimeHtml($vendorControl);

    }

    openForm(mode: string) {
        var $form, $defaultrate;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

        $defaultrate = $form.find('.defaultrate');
        FwFormField.loadItems($defaultrate, [
            { value: 'DAILY', text: 'Daily Rate' }
            , { value: 'WEEKLY', text: 'Weekly Rate' }
            , { value: 'MONTHLY', text: 'Monthly Rate' }
        ]);

        this.events($form);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DealId"] input').val(uniqueids.DealId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="DealId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $resaleGrid,
            $taxOptionGrid,
            $contactGrid,
            $dealNotesGrid,
            $vendorGrid;

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

        this.useDiscountTemplate(FwFormField.getValueByDataField($form, 'UseDiscountTemplate'));
        this.useCustomer(FwFormField.getValueByDataField($form, 'UseCustomerDiscount'));
        var val_bill = FwFormField.getValueByDataField($form, 'BillToAddressType') !== 'OTHER' ? true : false;
        this.toggleBillingAddressInfo($form, val_bill);
        this.toggleInsurTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerInsurance'));
        this.toggleCredTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerCredit'));
        this.disableInsurCompanyInfo($form);
        this.toggleTaxTabIfUseCustomer($form, FwFormField.getValueByDataField($form, 'UseCustomerTax'));
        this.toggleOptionsTabIfExcludeQuote($form, FwFormField.getValueByDataField($form, 'DisableQuoteOrderActivity'));
    }
}

(<any>window).DealController = new Deal();