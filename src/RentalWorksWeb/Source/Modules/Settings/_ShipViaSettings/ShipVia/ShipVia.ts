class ShipVia {
    Module: string = 'ShipVia';
    apiurl: string = 'api/v1/ShipVia';
    caption: string = Constants.Modules.Settings.children.ShipViaSettings.children.ShipVia.caption;
    nav: string = Constants.Modules.Settings.children.ShipViaSettings.children.ShipVia.nav;
    id: string = Constants.Modules.Settings.children.ShipViaSettings.children.ShipVia.id;


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

    openForm(mode: string) {
        var $form;

        $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        var $form;

        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="ShipViaId"] input').val(uniqueids.ShipViaId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    beforeValidateFreightVendor($browse, $grid, request) {
        var $form;
        $form = $grid.closest('.fwform');

        request.uniqueids = {
            Freight: true
        }
    }

    afterLoad($form: any) {
    }
}

var ShipViaController = new ShipVia();