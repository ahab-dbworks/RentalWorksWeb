class CreditCardPinPad {
    Module: string = 'CreditCardPinPad';
    apiurl: string = 'api/v1/creditcardpinpad';
    caption: string = Constants.Modules.Settings.children.CreditCardSettings.children.CreditCardPinPad.caption;
    nav: string = Constants.Modules.Settings.children.CreditCardSettings.children.CreditCardPinPad.nav;
    id: string = Constants.Modules.Settings.children.CreditCardSettings.children.CreditCardPinPad.id;
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
        let $browse = jQuery(this.getBrowseTemplate());
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
        FwFormField.setValueByDataField($form, 'CreditCardPinPadId', uniqueids.CreditCardPinPadId);

        FwModule.loadForm(this.Module, $form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form: JQuery) {
    }

    //--------------------------------------------------------------------------------------------
    getBrowseTemplate(): string {
        const template: string =  
`<div data-name="CreditCardPinPad" data-control="FwBrowse" data-type="Browse" class="fwcontrol fwbrowse" data-controller="CreditCardPinPadController" data-hasinactive="true">
  <div class="column" data-width="0" data-visible="false">
    <div class="field" data-isuniqueid="true" data-datafield="CreditCardPinPadId" data-browsedatatype="key"></div>
    <div class="field" data-datafield="Inactive" data-browsedatatype="text" data-visible="false"></div>
  </div>
  <div class="column" data-width="auto" data-visible="true">
    <div class="field" data-caption="Code" data-datafield="Code" data-browsedatatype="text" data-sort="asc"></div>
  </div>
  <div class="column" data-width="auto" data-visible="true">
    <div class="field" data-caption="Description" data-datafield="Description" data-browsedatatype="text"></div>
  </div>
</div>`;
        return template;
    }
    //----------------------------------------------------------------------------------------------
}
var CreditCardPinPadController = new CreditCardPinPad();