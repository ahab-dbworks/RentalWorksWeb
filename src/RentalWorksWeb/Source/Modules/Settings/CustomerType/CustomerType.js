class CustomerType {
    constructor() {
        this.Module = 'CustomerType';
        this.apiurl = 'api/v1/customertype';
    }
    getModuleScreen() {
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
    }
    openBrowse() {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CustomerTypeId"] input').val(uniqueids.CustomerTypeId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    loadAudit($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CustomerTypeId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    }
    afterLoad($form) {
    }
}
var CustomerTypeController = new CustomerType();
//# sourceMappingURL=CustomerType.js.map