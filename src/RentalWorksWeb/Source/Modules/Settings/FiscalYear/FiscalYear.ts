declare var FwModule: any;
declare var FwBrowse: any;

class FiscalYear {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'FiscalYear';
        this.apiurl = 'api/v1/fiscalyear';
    }

    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = function () {
            FwModule.openModuleTab($browse, 'Fiscal Year', false, 'BROWSE', true);
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
        var $fiscalYearGrid, $fiscalYearControl: JQuery;

        // load companytax Grid
        $fiscalYearGrid = $form.find('div[data-grid="FiscalMonthGrid"]');
        $fiscalYearControl = jQuery(jQuery('#tmpl-grids-FiscalMonthGridBrowse').html());
        $fiscalYearGrid.empty().append($fiscalYearControl);
        $fiscalYearControl.data('ondatabind', function (request) {
            request.uniqueids = {
                FiscalYearId: $form.find('div.fwformfield[data-datafield="FiscalYearId"] input').val()
            }            
        });
        FwBrowse.init($fiscalYearControl);
        FwBrowse.renderRuntimeHtml($fiscalYearControl);
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
        $form.find('div.fwformfield[data-datafield="FiscalYearId"] input').val(uniqueids.FiscalYearId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, closetab: boolean, navigationpath: string) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    }

    loadAudit($form: any) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="FiscalYearId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }

    afterLoad($form: any) {
        var $fiscalYearGrid;

        $fiscalYearGrid = $form.find('[data-name="FiscalMonthGrid"]');
        FwBrowse.search($fiscalYearGrid);
        
    }
}

(<any>window).FiscalYearController = new FiscalYear();