var CrewStatus = (function () {
    function CrewStatus() {
        this.Module = 'CrewStatus';
        this.apiurl = 'api/v1/crewstatus';
    }
    CrewStatus.prototype.getModuleScreen = function () {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(this.Module + 'Controller');
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Crew Status', false, 'BROWSE', true);
            FwBrowse.databind($browse);
            FwBrowse.screenload($browse);
        };
        screen.unload = function () {
            FwBrowse.screenunload($browse);
        };
        return screen;
    };
    CrewStatus.prototype.openBrowse = function () {
        var $browse;
        $browse = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Browse').html());
        $browse = FwModule.openBrowse($browse);
        FwBrowse.init($browse);
        return $browse;
    };
    CrewStatus.prototype.openForm = function (mode) {
        var $form;
        $form = jQuery(jQuery('#tmpl-modules-' + this.Module + 'Form').html());
        $form = FwModule.openForm($form, mode);
        return $form;
    };
    CrewStatus.prototype.loadForm = function (uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CrewStatusId"] input').val(uniqueids.CrewStatusId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    };
    CrewStatus.prototype.saveForm = function ($form, closetab, navigationpath) {
        FwModule.saveForm(this.Module, $form, closetab, navigationpath);
    };
    CrewStatus.prototype.loadAudit = function ($form) {
        var uniqueid;
        uniqueid = $form.find('div.fwformfield[data-datafield="CrewStatusId"] input').val();
        FwModule.loadAudit($form, uniqueid);
    };
    CrewStatus.prototype.afterLoad = function ($form) {
    };
    return CrewStatus;
}());
window.CrewStatusController = new CrewStatus();
//# sourceMappingURL=CrewStatus.js.map