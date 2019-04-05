routes.push({ pattern: /^module\/removefromcontainer$/, action: function (match: RegExpExecArray) { return RemoveFromContainerController.getModuleScreen(); } });

class RemoveFromContainer {
    Module: string = 'RemoveFromContainer';
    caption: string = 'Remove From Container';
    nav: string = 'module/removefromcontainer';
    id: string = 'FB9876B5-165E-486C-9E06-DFB3ACB3CBF0';
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
        };
        screen.unload = function () {
        };

        return screen;
    }
    //----------------------------------------------------------------------------------------------
    openForm(mode: string, parentmoduleinfo?) {
        let $form = jQuery(this.getFormTemplate());
        $form = FwModule.openForm($form, mode);

        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
<div id="removefromcontainerform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Remove From Container" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="RemoveFromContainerController">
  <div id="removefromcontainerform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs"></div>
    <div class="tabpages">
      <div class="flexpage">
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Container">
          <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container Item" data-datafield="ItemId" data-displayfield="BarCode" data-validationname="ContainerValidation" style="flex:1 1 175px;"></div>
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 250px;" data-enabled="false"></div>
          </div>
        </div>

        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item">
          <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Bar Code No." data-datafield="ItemId" data-displayfield="BarCode" data-validationname="RentalInventoryValidation" style="flex:1 1 175px;"></div>
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Serial No." data-datafield="" data-displayfield="" data-validationname="" style="flex:1 1 175px;"></div>
          </div>
          <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="" data-displayfield="" data-validationname="" style="flex:1 1 175px;"></div>
          </div>
          <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Consignor" data-datafield="" data-displayfield="" data-validationname="" style="flex:1 1 175px;"></div>
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Agreement No." data-datafield="" data-displayfield="" data-validationname="" style="flex:1 1 175px;"></div>
            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty" data-datafield="" style="flex:1 1 175px;"></div>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>
        `;
    }
    //----------------------------------------------------------------------------------------------
}
var RemoveFromContainerController = new RemoveFromContainer();