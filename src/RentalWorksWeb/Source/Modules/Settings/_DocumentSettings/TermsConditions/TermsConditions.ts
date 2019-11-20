class TermsConditions {
    Module: string = 'TermsConditions';
    apiurl: string = 'api/v1/termsconditions';
    caption: string = Constants.Modules.Settings.children.DocumentSettings.children.TermsConditions.caption;
    nav: string = Constants.Modules.Settings.children.DocumentSettings.children.TermsConditions.nav;
    id: string = Constants.Modules.Settings.children.DocumentSettings.children.TermsConditions.id;

    getModuleScreen() {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $browse = this.openBrowse();

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
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);

        return $browse;
    }

    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);

        return $form;
    }

    loadForm(uniqueids: any) {
        const $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="TermsConditionsId"] input').val(uniqueids.TermsConditionsId);
        FwModule.loadForm(this.Module, $form);

        return $form;
    }

    saveForm($form: any, parameters: any) {
        FwModule.saveForm(this.Module, $form, parameters);
    }

    afterLoad($form: any) {
    }
}

var TermsConditionsController = new TermsConditions();