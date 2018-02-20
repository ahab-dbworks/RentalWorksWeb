var CustomerType = (function () {
    function CustomerType() {
        this.Module = 'CustomerType';
        this.apiurl = 'api/v1/customertype';
    }
    CustomerType.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Customer Type', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    CustomerType.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        return $browse;
    };
    CustomerType.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    CustomerType.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomerTypeId"] input').val(uniqueids.CustomerTypeId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    CustomerType.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    };
    CustomerType.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CustomerTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    CustomerType.prototype.afterLoad = function ($form) {
    };
    return CustomerType;
}());
var CustomerTypeController = new CustomerType();
//# sourceMappingURL=CustomerType.js.map