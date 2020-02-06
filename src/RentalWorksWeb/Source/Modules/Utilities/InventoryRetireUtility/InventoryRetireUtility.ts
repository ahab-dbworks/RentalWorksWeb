routes.push({ pattern: /^module\/inventoryretireutility$/, action: function (match: RegExpExecArray) { return InventoryRetireUtilityController.getModuleScreen(); } });

class InventoryRetireUtility {
    Module: string = 'InventoryRetireUtility';
    apiurl: string = 'api/v1/inventoryretireutility';
    caption: string = Constants.Modules.Utilities.children.InventoryRetireUtility.caption;
    nav: string = Constants.Modules.Utilities.children.InventoryRetireUtility.nav;
    id: string = Constants.Modules.Utilities.children.InventoryRetireUtility.id;
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
        FwFormField.setValueByDataField($form, 'Quantity', 1);

        this.events($form);
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        $form.find('.retire-inv-btn').on('click', $tr => {
            if (FwModule.validateForm($form)) {
                const request: any = {};
                request.InventoryId = FwFormField.getValueByDataField($form, 'InventoryId');
                request.WarehouseId = JSON.parse(sessionStorage.getItem('warehouse')).warehouseid;
                request.Quantity = FwFormField.getValueByDataField($form, 'Quantity');
                request.Notes = FwFormField.getValueByDataField($form, 'Notes');
                request.RetiredReasonId = FwFormField.getValueByDataField($form, 'RetiredReasonId');
                request.ItemId = FwFormField.getValueByDataField($form, 'ItemId');

                FwAppData.apiMethod(true, 'POST', 'api/v1/inventoryretireutility/retireinventory', request, FwServices.defaultTimeout, response => {
                    if (response.success) {
                        FwNotification.renderNotification('SUCCESS', 'Retired Successfully');
                        $form.find('.fwformfield input').val('');
                        $form.find('.fwformfield textarea').val('');
                        FwFormField.setValueByDataField($form, 'Quantity', 1);
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
            <div id="inventoryretireutilityform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Inventory Retire Utility" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="InventoryRetireUtilityController">
              <div id="inventoryretireutilityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs"></div>
                <div class="tabpages">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Retire Inventory" style="max-width:700px">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Bar Code No." data-datafield="ItemId" data-displayfield="BarCode" data-validationname="AssetValidation" style="flex:0 1 200px;"></div>
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield itemid" data-caption="Serial No." data-datafield="ItemId" data-displayfield="SerialNumber" data-validationname="AssetValidation" style="flex:0 1 200px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" data-displayfield="ICode" data-validationname="RentalInventoryValidation" style="flex:0 1 200px;"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Item Description" data-datafield="ItemDescription" data-enabled="false" style="flex:0 1 400px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="Retired Reason" data-datafield="RetiredReasonId" data-displayfield="RetiredReason" data-validationname="RetiredReasonValidation" data-required="true" style="flex:0 1 200px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty" data-datafield="Quantity" data-required="true" style="flex:0 1 125px;"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Retire Notes" data-datafield="Notes" style="max-width:960px;"></div>
                      </div>
                      <div class="flexrow">
                        <div class="fwformcontrol retire-inv-btn" data-type="button" style="flex:0 1 140px;margin:15px 0 0 10px;text-align:center;">Retire Items</div>
                      </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var InventoryRetireUtilityController = new InventoryRetireUtility();