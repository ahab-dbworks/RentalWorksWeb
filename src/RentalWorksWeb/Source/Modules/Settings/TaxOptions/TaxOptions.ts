﻿declare var FwModule: any;
declare var FwBrowse: any;

class TaxOptions {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'TaxOptions';
        this.apiurl = 'api/v1/taxoption';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
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
            this.canadaOnlyConfiguration($form, jQuery(e.currentTarget).val());
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
    }

    openBrowse() {
        var $browse;

        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);        

        return $browse;
    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
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
        
        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TaxOptionId"] input').val(uniqueids.TaxOptionId);
        FwModule.loadForm(this.Module, $form);
        
        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
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
}

(window as any).TaxOptionsController = new TaxOptions();