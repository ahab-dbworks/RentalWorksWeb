﻿routes.push({ pattern: /^module\/changeicodeutility$/, action: function (match: RegExpExecArray) { return ChangeICodeUtilityController.getModuleScreen(); } });

class ChangeICodeUtility {
    Module: string = 'ChangeICodeUtility';
    apiurl: string = 'api/v1/changeicodeutility';
    caption: string = Constants.Modules.Utilities.children.ChangeICodeUtility.caption;
    nav: string = Constants.Modules.Utilities.children.ChangeICodeUtility.nav;
    id: string = Constants.Modules.Utilities.children.ChangeICodeUtility.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen = () => {
        const screen: any = {};
        screen.$view = FwModule.getModuleControl(`${this.Module}Controller`);
        screen.viewModel = {};
        screen.properties = {};

        const $form = this.openForm('EDIT');

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
        $form.find('.ch-inv-btn').on('click', $tr => {
            if (FwModule.validateForm($form)) {
                const request: any = {};
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
               // request.WarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
                //request.Notes = FwFormField.getValueByDataField($form, 'Notes');
                request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');

                FwAppData.apiMethod(true, 'POST', 'api/v1/changeicodeutility/changeicode', request, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        FwNotification.renderNotification('SUCCESS', 'I-Code Changed Successfully');
                        $form.find('.fwformfield input').val('');
                        $form.find('.fwformfield textarea').val('');
                        FwModule.refreshForm($form);
                    } else {
                    }
                }, ex => FwFunc.showError(ex), $form);
            }
        });

        // Set Description from I-Code validation
        $form.find('[data-datafield="InventoryId"]').data('onchange', $tr => {
            FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
        });
        $form.find('.itemid').data('onchange', $tr => {
            const itemId = jQuery($tr.find('.field[data-formdatafield="ItemId"]')).attr('data-originalvalue');
            FwFormField.setValue($form, '.itemid[data-displayfield="BarCode"]', itemId, $tr.find('.field[data-formdatafield="BarCode"]').attr('data-originalvalue'));
            FwFormField.setValue($form, '.itemid[data-displayfield="SerialNumber"]', itemId, $tr.find('.field[data-formdatafield="SerialNumber"]').attr('data-originalvalue'))
            FwFormField.setValue($form, 'div[data-datafield="ItemDescription"]', $tr.find('.field[data-formdatafield="Description"]').attr('data-originalvalue'));
            FwFormField.setValue($form, 'div[data-displayfield="ICode"]', $tr.find('.field[data-formdatafield="InventoryId"]').attr('data-originalvalue'), $tr.find('.field[data-formdatafield="ICode"]').attr('data-originalvalue'));
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
            <div id="changeicodeutilityform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Change I-Code Utility" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ChangeICodeUtilityController">
              <div id="changeicodeutilityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs"></div>
                <div class="tabpages">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Change I-Code" style="max-width:700px">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Bar Code No." data-datafield="ItemId" data-displayfield="BarCode" data-validationname="AssetValidation" style="flex:0 1 200px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Serial No." data-datafield="ItemId" data-displayfield="SerialNumber" data-validationname="AssetValidation" style="flex:0 1 200px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-validationname="RentalInventoryValidation" style="flex:0 1 200px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Item Description" data-datafield="ItemDescription" data-enabled="false" style="flex:0 1 400px;"></div>
                      </div>
                      //<div class="flexrow">
                      //  <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Change Notes" data-datafield="Notes" style="max-width:960px;"></div>
                      //</div>
                      <div class="flexrow">
                        <div class="fwformcontrol ch-inv-btn" data-type="button" style="flex:0 1 140px;margin:15px 0 0 10px;text-align:center;">Change I-Code</div>
                      </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var ChangeICodeUtilityController = new ChangeICodeUtility();