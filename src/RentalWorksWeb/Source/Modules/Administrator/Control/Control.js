var Control = (function () {
    function Control() {
        this.Module = 'Control';
        this.apiurl = 'api/v1/control';
    }
    Control.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Control', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    Control.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        return $browse;
    };
    Control.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        if (mode === 'NEW') {
            FwFormField.enable($form.find('.ifnew'));
        }
        else {
            FwFormField.disable($form.find('.ifnew'));
        }
        return $form;
    };
    Control.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ControlId"] input').val(uniqueids.ControlId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    Control.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, { closetab: closetab, navigationpath: navigationpath });
    };
    Control.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="ControlId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    Control.prototype.afterLoad = function ($form) {
    };
    return Control;
}());
window.ControlController = new Control();
//# sourceMappingURL=Control.js.map