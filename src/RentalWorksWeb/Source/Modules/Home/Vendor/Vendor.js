var Vendor = (function () {
    function Vendor() {
        this.renderGrids = function ($form) {
            var $comapnyTaxGrid, $companyTaxGridControl;
            $comapnyTaxGrid = $form.find('div[data-grid="PersonalEvent"]');
            $companyTaxGridControl = jQuery(jQuery('#tmpl-grids-ContactPersonalEventBrowse').html());
            $comapnyTaxGrid.empty().append($companyTaxGridControl);
            $companyTaxGridControl.data('ondatabind', function (request) {
                request.module = 'ContactPersonalEvent';
                request.uniqueids = {
                    contactid: $form.find('div.fwformfield[data-datafield="contact.contactid"] input').val()
                };
                FwServices.grid.method(request, 'ContactPersonalEvent', 'Browse', $companyTaxGridControl, function (response) {
                    FwBrowse.databindcallback($companyTaxGridControl, response.browse);
                });
            });
            FwBrowse.init($companyTaxGridControl);
            FwBrowse.renderRuntimeHtml($companyTaxGridControl);
        };
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
    };
    return Vendor;
}());
window.VendorController = new Vendor();
//# sourceMappingURL=Vendor.js.map