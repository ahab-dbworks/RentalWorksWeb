class ShipVia {
    constructor() {
        this.Module = 'ShipVia';
        this.apiurl = 'api/v1/ShipVia';
        this.caption = Constants.Modules.Settings.children.ShipViaSettings.children.ShipVia.caption;
        this.nav = Constants.Modules.Settings.children.ShipViaSettings.children.ShipVia.nav;
        this.id = Constants.Modules.Settings.children.ShipViaSettings.children.ShipVia.id;
    }
    getModuleScreen() {
        var screen, $browse;
        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};
        $browse = this.openBrowse();
        screen.load = function () {
            FwModule.openModuleTab($browse, 'Ship Via', false, 'BROWSE', true);
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
        $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    openForm(mode) {
        var $form;
        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    loadForm(uniqueids) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ShipViaId"] input').val(uniqueids.ShipViaId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    saveForm($form, parameters) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    beforeValidateFreightVendor($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');
        request.uniqueids = {
            Freight: true
        };
    }
    afterLoad($form) {
    }
}
var ShipViaController = new ShipVia();
//# sourceMappingURL=ShipVia.js.map