declare var FwModule: any;
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
            FwModule.openModuleTab($browse, 'TaxOptions', false, 'BROWSE', true);
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

    }

    canadaOnlyConfiguration($form: JQuery, country: string): void {        

        if (country == 'U') {

            $form.find('.canadatab, .canadataxratespanel').hide();
            $form.find('.ustaxratespanel').show();

        } else {

            $form.find('.ustaxratespanel').hide();
            $form.find('.canadatab, .canadataxratespanel').show();

        }
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
    }
}

(window as any).TaxOptionsController = new TaxOptions();