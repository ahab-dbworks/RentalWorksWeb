class TaxOption {
    Module: string = 'TaxOption';
    apiurl: string = 'api/v1/taxoption';

    constructor() {
        var self = this;

        //Sends confirmation for forcing tax rate
        FwApplicationTree.clickEvents['{CE1AEA95-F022-4CF5-A4FA-81CE32523344}'] = function (e) {
            var $form, taxOptionId;
            try {
                $form = jQuery(this).closest('.fwform');
                taxOptionId = $form.find('div.fwformfield[data-datafield="TaxOptionId"] input').val();
                self.forceTaxRates(taxOptionId);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        };
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

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

    events($form: JQuery): void {
        $form.on('change', '.countryradio input[type="radio"]:checked', (e) => {
            this.canadaOnlyConfiguration($form, jQuery(e.currentTarget).val().toString());
        });

        $form.on('change', '.exempttype', (e) => {
            var isChecked = jQuery(e.currentTarget).find('input[type="checkbox"]').is(':checked'),
                exemptTypeClass = jQuery(e.currentTarget).data('exempttypetxtclass');

            this.toggleDisableUSTaxRates($form, isChecked, exemptTypeClass);
        });
    }

    canadaOnlyConfiguration($form: JQuery, country: string): void {
        if (country == 'U') {
            $form.find('.canadatab, .canadataxratespanel, .canadataxrulespanel').hide();
            $form.find('.ustaxratespanel').show();
        } else {
            $form.find('.ustaxratespanel').hide();
            $form.find('.canadatab, .canadataxratespanel, .canadataxrulespanel').show();
        }
    }

    toggleDisableUSTaxRates($form: JQuery, isChecked: boolean, exemptTypeClass: string): void {
        if (!isChecked) {
            $form.find('.' + exemptTypeClass)
                .attr('data-enabled', 'true')
                .find('input[type="text"]')
                .prop('disabled', false);
        } else {
            $form.find('.' + exemptTypeClass)
                .attr('data-enabled', 'false')
                .find('input[type="text"]')
                .prop('disabled', true);
        }
    }

    markFieldsNotRequired($form: JQuery): void {
        $form.find('.gstexportcodetxt, .pstexporttxt').attr('data-required', 'false');

        $form.find('.canadataxratespanel, .ustaxratespanel').find('.fwformfield').attr('data-required', 'false');

        $form.find('.desc').attr('data-required', 'false');

        $form.find('.notrequired').attr('data-required', 'false');
    }

    openBrowse() {
        var $browse;

        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        this.events($form);

        this.markFieldsNotRequired($form);

        if (mode == 'NEW') {
            this.canadaOnlyConfiguration($form, 'U');
            this.markFieldsNotRequired($form);
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

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TaxOptionId"] input').val(uniqueids.TaxOptionId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="TaxOptionId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var country = $form.find('.countryradio input[type="radio"]:checked').val();

        this.canadaOnlyConfiguration($form, country);

        $form.find('.exempttype').each((i, e) => {
            this.toggleDisableUSTaxRates($form, jQuery(e).find('input[type="checkbox"]').is(':checked'), jQuery(e).data('exempttypetxtclass'));
        });
    }

    forceTaxRates(id: any) {
        var $confirmation, $yes, $no, self;

        self = this;
        $confirmation = FwConfirmation.renderConfirmation('Force Tax Rates', '<div style="white-space:pre;">This will update all of the following records with the tax rates: \n' +
            '------------------------------------------------------------------------------------------------- \n' +
            'Prospect and Active Quotes \n' +
            'Confirmed, Active, and Complete Orders \n' +
            'New, Open, Received, and Completed Purchase Orders \n' +
            'New and Estimated Repair Orders that have not yet been billed \n \n' +
            'Are you sure you want to force these Tax Rates? This cannot be undone.</div>');
        $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        $no = FwConfirmation.addButton($confirmation, 'No');

        $yes.on('click', function () {
            FwAppData.apiMethod(true, 'POST', `${self.apiurl}/${id}/forcerates`, {}, FwServices.defaultTimeout, function onSuccess(response) {
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