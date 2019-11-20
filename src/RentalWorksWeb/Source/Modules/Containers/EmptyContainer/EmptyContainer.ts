routes.push({ pattern: /^module\/emptycontainer$/, action: function (match: RegExpExecArray) { return EmptyContainerController.getModuleScreen(); } });

class EmptyContainer {
    Module: string = 'EmptyContainer';
    caption: string = Constants.Modules.Container.children.EmptyContainer.caption;
    nav: string = Constants.Modules.Container.children.EmptyContainer.nav;
    id: string = Constants.Modules.Container.children.EmptyContainer.id;
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

        this.events($form);
        return $form;
    }
    //----------------------------------------------------------------------------------------------
    events($form) {
        // Populate Description when selecting container
        $form.find('[data-datafield="ContainerItemId"]').data('onchange', $tr => {
            FwFormField.setValueByDataField($form, 'Description', $tr.find('.field[data-browsedatafield="ContainerDescription"]').attr('data-originalvalue'));
            FwFormField.setValueByDataField($form, 'ItemId', $tr.find('.field[data-browsedatafield="ItemId"]').attr('data-originalvalue'));
        });

        // Toggle checkbox warning text
        $form.on('change', '[data-datafield="DeleteContainer"]', e => {
            const isChecked = jQuery(e.currentTarget).find('input').prop('checked');
            const $warningText = $form.find('.warningText');
            isChecked ? $warningText.show() : $warningText.hide();
        });

        $form.on('click', '.emptyContainer', e => {
            const id = FwFormField.getValueByDataField($form, 'ItemId');
            const $responseMsg = $form.find('.response-msg');
            const $containerField = $form.find('[data-datafield="ContainerItemId"]');
            const request: any = {};
            request.ItemId = id;
            request.DeleteAll = FwFormField.getValueByDataField($form, 'DeleteContainer');
            FwAppData.apiMethod(true, 'POST', `api/v1/containeritem/emptycontainer`, request, FwServices.defaultTimeout,
                response => {
                    $responseMsg.html(`<div style="margin: 5px;"><span>${response.msg}</span></div>`);
                    if (response.success) {
                        try {
                            $form.find('.fwformfield input').val('');
                            $form.find('.warningText').hide();
                            FwFormField.setValueByDataField($form, 'DeleteContainer', false);
                            $responseMsg.removeClass('error-msg').addClass('success-msg');
                            $containerField.removeClass('error');
                        }
                        catch (ex) {
                            FwFunc.showError(ex);
                        }
                    } else {
                        $containerField.addClass('error');
                        $responseMsg.removeClass('success-msg').addClass('error-msg');
                    }
                },
                ex => {
                    FwFunc.showError(ex);
                }, $form);
        });
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        request.uniqueids = {
            WarehouseId: warehouse.warehouseid
        };
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
<div id="emptycontainerform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Empty Container" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="EmptyContainerController">
  <div id="emptycontainerform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs"></div>
    <div class="tabpages">
      <div class="flexpage">
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Container">
          <div class="flexrow" style="max-width:50%;">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Container Item" data-datafield="ContainerItemId" data-displayfield="BarCode" data-validationname="ContainerItemValidation" style="flex:1 1 175px;"></div>
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 250px;" data-enabled="false"></div>
          </div>

          <!--Hidden field for storing ContainerItemId -->
          <div class="flexrow" style="display:none;">
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="ItemId" data-datafield="ItemId" style="flex:1 1 250px;" data-enabled="false"></div>
          </div>

          <div class="flexrow">
            <div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Completely Delete this Container instance and all History" data-datafield="DeleteContainer" style="flex:1 1 250px;"></div>
          </div>
          <div><span>Clicking the "Empty Container" button below will empty all contents of this container and move the items back to IN status.</span></div>
          <div class="warningText" style="display:none; color:red; margin-top:10px;"><span>This Container instance will be deleted.  All Fill/Empty history will be deleted.  The Item will no longer be associated to this type of Container.</span></div>
          <div class="flexrow">
            <div class="fwformcontrol emptyContainer" data-type="button" style="flex:0 1 140px;margin:15px 0 0 10px;text-align:center;">Empty Container</div>
          </div>
          <div class="flexrow">
            <div class="response-msg"></div>
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
var EmptyContainerController = new EmptyContainer();