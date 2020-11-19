class CreditCardPinPad {
    Module: string = 'CreditCardPinPad';
    apiurl: string = 'api/v1/creditcardpinpad';
    caption: string = Constants.Modules.Settings.children.CreditCardSettings.caption;
    nav: string = Constants.Modules.Settings.children.CreditCardSettings.children.PinPad.nav;
    id: string = Constants.Modules.Settings.children.CreditCardSettings.children.PinPad.id;
    //----------------------------------------------------------------------------------------------
    getModuleScreen(filter?: { datafield: string, search: string }) {
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
    //----------------------------------------------------------------------------------------------
    openBrowse() {
        let $browse = FwBrowse.loadBrowseFromTemplate(this.Module);
        $browse = FwModule.openBrowse($browse);
        return $browse;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string) {
        let $form = FwModule.loadFormFromTemplate(this.Module);
        $form = FwModule.openForm($form, mode);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    loadForm(uniqueids: any) {
        var $form;
        $form = this.openForm('EDIT');
        $form.find('div.fwformfield[data-datafield="CreditCardPinPadId"] input').val(uniqueids.CreditCardPinPadId);
        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
    }
    //----------------------------------------------------------------------------------------------
}
var CreditCardPinPadController = new CreditCardPinPad();