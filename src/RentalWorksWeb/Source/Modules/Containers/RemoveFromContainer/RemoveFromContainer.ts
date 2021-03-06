﻿routes.push({ pattern: /^module\/removefromcontainer$/, action: function (match: RegExpExecArray) { return RemoveFromContainerController.getModuleScreen(); } });

class RemoveFromContainer {
    Module: string = 'RemoveFromContainer';
    apiurl: string = 'api/v1/removefromcontainer';
    caption: string = Constants.Modules.Container.children.RemoveFromContainer.caption;
    nav: string = Constants.Modules.Container.children.RemoveFromContainer.nav;
    id: string = Constants.Modules.Container.children.RemoveFromContainer.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        var screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        var $form = this.openForm('EDIT');

        screen.load = () => {
            FwModule.openModuleTab($form, this.caption, false, 'FORM', true);
            this.afterLoad($form);
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
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        const $responseMsg = $form.find('.response-msg');
        // Remove by BarCode or Serial Number
        $form.find('.itemid').data('onchange', $tr => {
            const itemId = jQuery($tr.find('.field[data-formdatafield="ItemId"]')).attr('data-originalvalue');
            FwFormField.setValue($form, '.itemid[data-displayfield="BarCode"]', itemId, $tr.find('.field[data-formdatafield="BarCode"]').attr('data-originalvalue'));
            FwFormField.setValue($form, '.itemid[data-displayfield="SerialNumber"]', itemId, $tr.find('.field[data-formdatafield="SerialNumber"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
            const request: any = {};
            request.ItemId = itemId;
            FwAppData.apiMethod(true, 'POST', 'api/v1/containeritem/removefromcontainer', request, FwServices.defaultTimeout, response => {
                $responseMsg.html(`<div><span>${response.msg}</span></div>`);
                if (response.success) {
                    $form.find('.fwformfield input').val('');
                    $responseMsg.removeClass('error-msg').addClass('success-msg');
                } else {
                    $responseMsg.removeClass('success-msg').addClass('error-msg');
                }
            }, ex => FwFunc.showError(ex), $form);
        });

        // Remove by Quantity
        $form.find('[data-datafield="Quantity"] input').on('keydown', e => {
            if (e.which === 13) {
                const request: any = {};
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.ContainerItemId = FwFormField.getValue($form, '.container');
                request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
                FwAppData.apiMethod(true, 'POST', 'api/v1/containeritem/removefromcontainer', request, FwServices.defaultTimeout, response => {
                    $responseMsg.html(`<div><span>${response.msg}</span></div>`);
                    if (response.success) {
                        $form.find('.fwformfield input').val('');
                        $responseMsg.removeClass('error-msg').addClass('success-msg');
                    } else {
                        $responseMsg.removeClass('success-msg').addClass('error-msg');
                    }
                }, ex => FwFunc.showError(ex), $form);
            }
        });

        // Set Description from I-Code validation
        $form.find('[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
        });
        // Set Description from Container Item validation
        $form.find('.container').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ContainerDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
        });

    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        $form.find('.itemid[data-displayfield="BarCode"] input').focus();
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        switch (datafield) {
            case 'ContainerItemId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecontaineritem`);
                break;
            case 'ItemId':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateitem`);
                break;
            case 'InventoryId':
                request.uniqueids = {
                    WarehouseId: warehouse.warehouseid,
                    TrackedBy: 'QUANTITY',
                };
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
        }
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
<div id="removefromcontainerform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Remove From Container" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="RemoveFromContainerController">
  <div id="removefromcontainerform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
    <div class="tabs"></div>
    <div class="tabpages">
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="" style="max-width:1200px">
            <div>Scanning or inputting items below will remove them from their Container and immediately move them back to IN status.  There is no option to Cancel or Undo this.</div>
            <div>"Container Item" bar code is only required when removing quantity items from a Container.</div>
        </div>
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Container" style="max-width:700px">
          <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield container" data-caption="Container Item" data-datafield="ContainerItemId" data-displayfield="BarCode" data-validationname="ContainerItemValidation" style="flex:0 1 200px;"></div>
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Container Description" data-datafield="ContainerDescription" style="flex:0 1 400px;" data-enabled="false"></div>
          </div>
        </div>
        <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Item to Remove" style="max-width:700px">
          <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Bar Code No." data-datafield="ItemId" data-displayfield="BarCode" data-validationname="AssetValidation" style="flex:0 1 200px;"></div>
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Serial No." data-datafield="ItemId" data-displayfield="SerialNumber" data-validationname="AssetValidation" style="flex:0 1 200px;"></div>
          </div>
          <div class="flexrow">
            <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-validationname="RentalInventoryValidation" style="flex:0 1 200px;"></div>
            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Item Description" data-datafield="ItemDescription" data-enabled="false" style="flex:0 1 400px;"></div>
          </div>
          <div class="flexrow">
            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty" data-datafield="Quantity" style="flex:0 1 125px;"></div>
          </div>
          <div class="flexrow" style="margin-top:8px;">
            <div class="response-msg"></div>
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