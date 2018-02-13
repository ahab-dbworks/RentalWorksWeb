var CustomerStatus = (function () {
    function CustomerStatus() {
        this.Module = 'CustomerStatus';
        this.apiurl = 'api/v1/customerstatus';
    }
    CustomerStatus.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Customer Status', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    CustomerStatus.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        return $browse;
    };
    CustomerStatus.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    CustomerStatus.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomerStatusId"] input').val(uniqueids.CustomerStatusId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    CustomerStatus.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    };
    CustomerStatus.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CustomerStatusId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    CustomerStatus.prototype.afterLoad = function ($form) {
    };
    return CustomerStatus;
}());
window.CustomerStatusController = new CustomerStatus();
//# sourceMappingURL=CustomerStatus.js.map