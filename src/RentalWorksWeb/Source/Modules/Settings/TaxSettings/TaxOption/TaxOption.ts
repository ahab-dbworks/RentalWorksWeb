class TaxOption {
    Module: string = 'TaxOption';
    apiurl: string = 'api/v1/taxoption';
    caption: string = Constants.Modules.Settings.children.TaxSettings.children.TaxOption.caption;
    nav: string = Constants.Modules.Settings.children.TaxSettings.children.TaxOption.nav;
    id: string = Constants.Modules.Settings.children.TaxSettings.children.TaxOption.id;

    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
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
    addFormMenuItems(options: IAddFormMenuOptions): void {
        FwMenu.addFormMenuButtons(options);

        FwMenu.addSubMenuItem(options.$groupOptions, 'Force Tax Rates', 'lfZiNgs8GOJBE', (e: JQuery.ClickEvent) => {
            try {
                this.forceTaxRates(options.$form);
            } catch (ex) {
                FwFunc.showError(ex);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        if (mode === 'NEW') {
            const location = JSON.parse(sessionStorage.getItem('location'));
            FwFormField.setValueByDataField($form, 'TaxCountryId', location.countryid, location.country);

            switch (location.country.toUpperCase()) {
                case 'US':
                case 'USA':
                    FwFormField.setValueByDataField($form, 'Tax1Name', 'Tax');
                    break;
                case 'CAN':
                case 'CANADA':
                    FwFormField.setValueByDataField($form, 'Tax1Name', 'GST');
                    FwFormField.setValueByDataField($form, 'Tax2Name', 'PST');
                    break;
                case 'UK':
                case 'UNITED KINGDOM':
                    FwFormField.setValueByDataField($form, 'Tax1Name', 'VAT');
                    break;
            }
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
        this.configureTaxOnTax($form);

        $form.find('.exempttype').each((i, e) => {
            this.toggleDisableUSTaxRates($form, jQuery(e).find('input[type="checkbox"]').is(':checked'), jQuery(e).data('exempttypetxtclass'));
        });
    }
    //----------------------------------------------------------------------------------------------
    events($form: JQuery): void {
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
    forceTaxRates($form: JQuery) {
        const $confirmation = FwConfirmation.renderConfirmation('Force Tax Rates', '<div style="white-space:pre;">This will update all of the following records with the tax rates: \n' +
            '------------------------------------------------------------------------------------------------- \n' +
            'Prospect and Active Quotes \n' +
            'Confirmed, Active, and Complete Orders \n' +
            'New, Open, Received, and Completed Purchase Orders \n' +
            'New and Estimated Repair Orders that have not yet been billed \n \n' +
            'Are you sure you want to force these Tax Rates? This cannot be undone.</div>');
        const $yes = FwConfirmation.addButton($confirmation, 'Yes', false);
        const $no = FwConfirmation.addButton($confirmation, 'No');

        const taxOptionId = FwFormField.getValueByDataField($form, 'TaxOptionId');
        $yes.on('click', () => {
            FwAppData.apiMethod(true, 'POST', `${this.apiurl}/${taxOptionId}/forcerates`, {}, FwServices.defaultTimeout, function onSuccess(response) {
                try {
                    FwNotification.renderNotification('SUCCESS', 'Tax Rates Forced');
                    FwConfirmation.destroyConfirmation($confirmation);
                } catch (ex) {
                    FwFunc.showError(ex);
                }
            }, null, null);
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'TaxAccountId1':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedetaxaccount1`);
                break;
            case 'TaxAccountId2':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatetaxaccount2`);
                break;
            case 'TaxOnTaxAccountId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatedetaxontaxaccount`);
                break;
        }
    }
}

var TaxOptionController = new TaxOption();