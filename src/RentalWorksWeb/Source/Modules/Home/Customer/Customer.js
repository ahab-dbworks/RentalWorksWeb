var Customer = (function () {
    function Customer() {
        this.Module = 'Customer';
        this.apiurl = 'api/v1/customer';
    }
    Customer.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Customer', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    Customer.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    Customer.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        $form.find('[data-datafield="DisableQuoteOrderActivity"] .fwformfield-value').on('change', function () {
            var $this = jQuery(this);
            if ($this.prop('checked') === true) {
                FwFormField.enable($form.find('.quote-order [data-type="checkbox"]'));
            }
            else {
                FwFormField.disable($form.find('.quote-order [data-type="checkbox"]'));
            }
        });
        return $form;
    };
    Customer.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomerId"] input').val(uniqueids.CustomerId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    Customer.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    Customer.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CustomerId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    Customer.prototype.afterLoad = function ($form) {
    };
    return Customer;
}());
window.CustomerController = new Customer();
//# sourceMappingURL=Customer.js.map