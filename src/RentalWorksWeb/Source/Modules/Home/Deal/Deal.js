var Deal = (function () {
    function Deal() {
        this.Module = 'Deal';
        this.apiurl = 'api/v1/deal';
    }
    Deal.prototype.getModuleScreen = function () {
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
    };
    Deal.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    Deal.prototype.renderGrids = function ($form) {
        var $companyTaxResaleGrid, $companyTaxResaleControl, $taxOptionGrid, $taxOptionControl, $contactGrid, $contactControl;
        // load companytax Grid
        $companyTaxResaleGrid = $form.find('div[data-grid="CompanyTaxResaleGrid"]');
        $companyTaxResaleControl = jQuery(jQuery('#tmpl-grids-CompanyTaxResaleGridBrowse').html());
        $companyTaxResaleGrid.empty().append($companyTaxResaleControl);
        $companyTaxResaleControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: $form.find('div.fwformfield[data-datafield="DealId"] input').val()
            };
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
    };
    Deal.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    Deal.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="DealId"] input').val(uniqueids.DealId);
        FwModule.loadForm(this.Module, $form);
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
        var $companyTaxResaleGrid, $taxOptionGrid, $contactGrid;
        $companyTaxResaleGrid = $form.find('[data-name="CompanyTaxResaleGrid"]');
        FwBrowse.search($companyTaxResaleGrid);
        $taxOptionGrid = $form.find('[data-name="CompanyTaxOptionGrid"]');
        FwBrowse.search($taxOptionGrid);
        $contactGrid = $form.find('[data-name="ContactGrid"]');
        FwBrowse.search($contactGrid);
    };
    return Deal;
}());
window.DealController = new Deal();
//# sourceMappingURL=Deal.js.map