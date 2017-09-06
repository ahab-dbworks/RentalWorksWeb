var Vendor = (function () {
    function Vendor() {
        this.Module = 'Vendor';
        this.apiurl = 'api/v1/vendor';
    }
    Vendor.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        this.events();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Vendor', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    Vendor.prototype.events = function () {
        var _this = this;
        var $parent = jQuery(document);
        $parent.on('click', '.vendertyperadio input[type=radio]', function (e) {
            _this.togglePanels(jQuery(e.currentTarget).val());
        });
    };
    Vendor.prototype.togglePanels = function (type) {
        jQuery('.type_panels').hide();
        switch (type) {
            case 'Company':
                jQuery('#company_panel').show();
                break;
            case 'Person':
                jQuery('#person_panel').show();
                break;
            default:
                throw Error(type + ' is not a known type.');
        }
    };
    Vendor.prototype.renderGrids = function ($form) {
        var $companyTaxGrid, $companyTaxControl;
        // load AttributeValue Grid
        $companyTaxGrid = $form.find('div[data-grid="CompanyTaxGrid"]');
        $companyTaxControl = jQuery(jQuery('#tmpl-grids-CompanyTaxGridBrowse').html());
        $companyTaxGrid.empty().append($companyTaxControl);
        $companyTaxControl.data('ondatabind', function (request) {
            request.uniqueids = {
                CompanyId: $form.find('div.fwformfield[data-datafield="VendorId"] input').val()
            };
        });
        FwBrowse.init($companyTaxControl);
        FwBrowse.renderRuntimeHtml($companyTaxControl);
    };
    Vendor.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    Vendor.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    Vendor.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="VendorId"] input').val(uniqueids.VendorId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    Vendor.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    Vendor.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="VendorId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    Vendor.prototype.afterLoad = function ($form) {
        var $companyTaxGrid;
        $companyTaxGrid = $form.find('[data-name="CompanyTaxGrid"]');
        FwBrowse.search($companyTaxGrid);
    };
    return Vendor;
}());
window.VendorController = new Vendor();
//# sourceMappingURL=Vendor.js.map