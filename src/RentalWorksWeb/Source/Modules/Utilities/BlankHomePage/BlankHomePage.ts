routes.push({ pattern: /^module\/blankhomepage$/, action: function (match: RegExpExecArray) { return BlankHomePageController.getModuleScreen(); } });

class BlankHomePage {
    Module: string = 'BlankHomePage';
    apiurl: string = 'api/v1/blankhomepage';
    caption: string = Constants.Modules.Utilities.children.BlankHomePage.caption;
    nav: string = Constants.Modules.Utilities.children.BlankHomePage.nav;
    id: string = Constants.Modules.Utilities.children.BlankHomePage.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, Constants.appCaption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {

        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        setTimeout(() => { $form.closest('#moduletabs').hide(); }, 0);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: any) {
    }
    //----------------------------------------------------------------------------------------------
}
var BlankHomePageController = new BlankHomePage();