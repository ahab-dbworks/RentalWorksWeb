class FormDesign {
    Module: string = 'FormDesign';
    apiurl: string = 'api/v1/formdesign';
    caption: string = Constants.Modules.Settings.children.PresentationSettings.children.FormDesign.caption;
    nav: string = Constants.Modules.Settings.children.PresentationSettings.children.FormDesign.nav;
    id: string = Constants.Modules.Settings.children.PresentationSettings.children.FormDesign.id;
    //----------------------------------------------------------------------------------------------
    addBrowseMenuItems(options: IAddBrowseMenuOptions): void {
        options.hasNew = false;
        options.hasEdit = false;
        options.hasDelete = false;
        FwMenu.addBrowseMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        var screen, $browse;

        screen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $browse = this.openBrowse();

        screen.load = () => {
            FwModule.openModuleTab($browse, this.caption, false, 'BROWSE', true);
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

    afterLoad($form: any) {
    }
}

var FormDesignController = new FormDesign();