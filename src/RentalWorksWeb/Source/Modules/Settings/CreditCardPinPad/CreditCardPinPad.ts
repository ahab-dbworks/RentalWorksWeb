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
        let $form = jQuery(this.getFormTemplate());
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
        return 
`<div data-name="CreditCardPinPad" data-control="FwBrowse" data-type="Browse" id="CreditCardPinPadBrowse" class="fwcontrol fwbrowse" data-orderby="" data-controller="CreditCardPinPadController" data-hasinactive="true">
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
    }
    //--------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return 
`<div id="creditcardpinpadform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-caption="Credit Card Pin Pad" data-controller="CreditCardPinPadController">
  <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="CreditCardPinPadId"></div>
  <div id="activityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs">
      <div data-type="tab" id="generaltab" class="tab" data-tabpageid="generaltabpage" data-caption="General"></div>
    </div>
    <div class="tabpages">
      <div data-type="tabpage" id="generaltabpage" class="tabpage" data-tabid="generaltab">
        <div class="flexpage">
          <div class="flexrow">
            <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Pin Pad">
              <div class="flexrow">
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Pin Pad Code" data-datafield="Code" data-required="true" data-allcaps="false"></div>
                <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" data-required="true" data-allcaps="false"></div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var CreditCardPinPadController = new CreditCardPinPad();