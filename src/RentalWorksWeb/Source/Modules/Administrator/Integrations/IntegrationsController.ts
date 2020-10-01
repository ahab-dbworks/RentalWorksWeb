routes.push({ pattern: /^module\/integrations$/, action: function (match: RegExpExecArray) { return IntegrationsController.getModuleScreen(); } });

class Integrations implements IModule {
    Module: string = 'Integrations';
    apiurl: string = 'api/v1/integrations';
    caption: string = Constants.Modules.Administrator.children.Integrations.caption;
    nav: string = Constants.Modules.Administrator.children.Integrations.nav;
    id: string = Constants.Modules.Administrator.children.Integrations.id;
    //---------------------------------------------------------------------------------
    getModuleScreen() {
        let $form;
        const screen: IModuleScreen = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };

        return screen;
    }
    //---------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }
    //---------------------------------------------------------------------------------
    //loadForm() {
    //    let $form = this.openForm('EDIT');
    //    FwModule.loadForm(this.Module, $form);

    //    return $form;
    //}

}
//-------------------------------------------------------------------------------------
var IntegrationsController = new Integrations();