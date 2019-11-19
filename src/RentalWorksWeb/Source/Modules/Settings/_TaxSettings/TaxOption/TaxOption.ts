class TaxOption {
    Module: string = 'TaxOption';
    apiurl: string = 'api/v1/taxoption';

    constructor() {
        //Sends confirmation for forcing tax rate
        FwApplicationTree.clickEvents[Constants.Modules.Settings.TaxOption.form.menuItems.ForceTaxRates.id] = e => {
            var $form, taxOptionId;
            try {
                const $form = jQuery(this).closest('.fwform');
                const taxOptionId = $form.find('div.fwformfield[data-datafield="TaxOptionId"] input').val();
                this.forceTaxRates(taxOptionId);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Tax Option', false, 'BROWSE', true);
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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            this.canadaOnlyConfiguration($form, 'U');
            FwFormField.setValueByDataField($form, 'RentalTaxRate1', 0);
            FwFormField.setValueByDataField($form, 'SalesTaxRate1', 0);
            FwFormField.setValueByDataField($form, 'LaborTaxRate1', 0);
            FwFormField.setValueByDataField($form, 'RentalTaxRate2', 0);
            FwFormField.setValueByDataField($form, 'SalesTaxRate2', 0);
            FwFormField.setValueByDataField($form, 'LaborTaxRate2', 0);
        }

        $form.find('div[data-datafield="TaxAccountId1"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="TaxAccountDescription1"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="TaxAccountId2"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="TaxAccountDescription2"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        $form.find('div[data-datafield="TaxOnTaxAccountId"]').data('onchange', function ($tr) {
            FwFormField.setValue($form, 'div[data-datafield="TaxOnTaxAccountDescription"]', $tr.find('.field[data-browsedatafield="GlAccountDescription"]').attr('data-originalvalue'));
        });

        this.events($form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TaxOptionId"] input').val(uniqueids.TaxOptionId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
        const country = FwFormField.getValueByDataField($form, 'TaxCountry');
        this.canadaOnlyConfiguration($form, country);
        this.configureTaxOnTax($form);

        $form.find('.exempttype').each((i, e) => {
            this.toggleDisableUSTaxRates($form, jQuery(e).find('input[type="checkbox"]').is(':checked'), jQuery(e).data('exempttypetxtclass'));
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
        $form.on('change', '[data-datafield="TaxCountry"] input[type="radio"]:checked', (e) => {
            this.canadaOnlyConfiguration($form, jQuery(e.currentTarget).val().toString());
        });

        $form.on('change', '.exempttype', (e) => {
            var isChecked = jQuery(e.currentTarget).find('input[type="checkbox"]').is(':checked'),
                exemptTypeClass = jQuery(e.currentTarget).data('exempttypetxtclass');

            this.toggleDisableUSTaxRates($form, isChecked, exemptTypeClass);
        });
        $form.find('div[data-datafield="TaxOnTax"]').change(e => {
            this.configureTaxOnTax($form);
        })
    }
    //----------------------------------------------------------------------------------------------
    configureTaxOnTax($form: JQuery) {
        const taxOnTax = FwFormField.getValueByDataField($form, 'TaxOnTax');
        if (taxOnTax === true) {
            FwFormField.enable($form.find('div[data-datafield="TaxOnTaxAccountId"]'));
        } else {
            FwFormField.disable($form.find('div[data-datafield="TaxOnTaxAccountId"]'));
        }
    }
    //----------------------------------------------------------------------------------------------
    canadaOnlyConfiguration($form: JQuery, country: string): void {
        if (country === 'U') {
            $form.find('.canadatab, .canadataxratespanel, .canadataxrulespanel').hide();
            FwFormField.setValueByDataField($form, 'TaxAccountId2', '');
            $form.find('.ustaxratespanel').show();
        } else {
            $form.find('.ustaxratespanel').hide();
            $form.find('.canadatab, .canadataxratespanel, .canadataxrulespanel').show();
        }
    }
    //----------------------------------------------------------------------------------------------
    toggleDisableUSTaxRates($form: JQuery, isChecked: boolean, exemptTypeClass: string): void {
        if (!isChecked) {
            $form.find(`.${exemptTypeClass}`)
                .attr('data-enabled', 'true')
                .find('input[type="text"]')
                .prop('disabled', false);
        } else {
            $form.find(`.${exemptTypeClass}`)
                .attr('data-enabled', 'false')
                .find('input[type="text"]')
                .prop('disabled', true);
        }
    }
    //----------------------------------------------------------------------------------------------
    forceTaxRates(id: any) {
        const $confirmation = FwConfirmation.renderConfirmation('Force Tax Rates', '<div style="white-space:pre;">This will update all of the following records with the tax rates: \n' +
            '------------------------------------------------------------------------------------------------- \n' +
            'Prospect and Active Quotes \n' +
            'Confirmed, Active, and Complete Orders \n' +
            'New, Open, Received, and Completed Purchase Orders \n' +
            'New and Estimated Repair Orders that have not yet been billed \n \n' +
            'Are you sure you want to force these Tax Rates? This cannot be undone.</div>');
        const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        const $no = FwConfirmation.addButton($confirmation, 'No');

        $yes.on('click', () => {
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/${id}/forcerates`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', 'Tax Rates Forced');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, null);
        });
    }
}

var TaxOptionController = new TaxOption();