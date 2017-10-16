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
        var $companyTaxResaleGrid,
            $companyTaxResaleControl,
            $taxOptionGrid,
            $taxOptionControl,
            $contactGrid,
            $contactControl,
            $dealNotesGrid,
            $dealNotesControl,
            $vendorGrid,
            $vendorControl;

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
        $taxOptionGrid = $form.find('div[data-grid="CompanyTaxOptionGrid"]');
        $taxOptionControl = jQuery(jQuery('#tmpl-grids-CompanyTaxOptionGridBrowse').html());
        $taxOptionGrid.empty().append($taxOptionControl);
        $taxOptionControl.data('ondatabind', function (request) {
            request.uniqueids = {
                TaxOptionId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
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
        var $companyTaxResaleGrid,
            $taxOptionGrid,
            $contactGrid,
            $dealNotesGrid,
            $vendorGrid;

        $companyTaxResaleGrid = $form.find('[data-name="CompanyTaxResaleGrid"]');
        FwBrowse.search($companyTaxResaleGrid);

        $taxOptionGrid = $form.find('[data-name="CompanyTaxOptionGrid"]');
        FwBrowse.search($taxOptionGrid);

        $contactGrid = $form.find('[data-name="ContactGrid"]');
        FwBrowse.search($contactGrid);

        $dealNotesGrid = $form.find('[data-name="DealNotesGrid"]');
        FwBrowse.search($dealNotesGrid);

        $vendorGrid = $form.find('[data-name="DealShipperGrid"]');
        FwBrowse.search($vendorGrid);
    }
}

(<any>window).DealController = new Deal();