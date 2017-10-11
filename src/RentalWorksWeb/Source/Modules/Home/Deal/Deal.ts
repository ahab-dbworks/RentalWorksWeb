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

    renderGrids($form: any) {
        var $companyTaxResaleGrid, $companyTaxResaleControl, $taxOptionGrid, $taxOptionControl;

        // load companytax Grid
        $companyTaxResaleGrid = $form.find('div[data-grid="CompanyTaxResaleGrid"]');
        $companyTaxResaleControl = jQuery(jQuery('#tmpl-grids-CompanyTaxResaleGridBrowse').html());
        $companyTaxResaleGrid.empty().append($companyTaxResaleControl);
        $companyTaxResaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            }
        });
        FwBrowse.init($companyTaxResaleControl);
        FwBrowse.renderRuntimeHtml($companyTaxResaleControl);

        // load vendornote Grid
        $taxOptionGrid = $form.find('div[data-grid="TaxOptionGrid"]');
        $taxOptionControl = jQuery(jQuery('#tmpl-grids-TaxOptionGridBrowse').html());
        $taxOptionGrid.empty().append($taxOptionControl);
        $taxOptionControl.data('ondatabind', function (request) {
            request.uniqueids = {
                TaxOptionId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            }
        });
        FwBrowse.init($taxOptionControl);
        FwBrowse.renderRuntimeHtml($taxOptionControl);


    }

    openForm(mode: string) {
        var $form;

        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);

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
        var $companyTaxResaleGrid, $taxOptionGrid;

        $companyTaxResaleGrid = $form.find('[data-name="CompanyTaxResaleGrid"]');
        FwBrowse.search($companyTaxResaleGrid);

        $taxOptionGrid = $form.find('[data-name="TaxOptionGrid"]');
        FwBrowse.search($taxOptionGrid);
    }
}

(<any>window).DealController = new Deal();