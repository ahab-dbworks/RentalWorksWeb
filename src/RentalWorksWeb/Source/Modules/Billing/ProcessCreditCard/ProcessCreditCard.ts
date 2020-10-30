routes.push({ pattern: /^module\/processcreditcard$/, action: function (match: RegExpExecArray) { return ProcessCreditCardController.getModuleScreen(); } });

class ProcessCreditCard{
    Module: string = 'ProcessCreditCard';
    apiurl: string = 'api/v1/procesCreditCard';
    caption: string = Constants.Modules.Billing.children.ProcessCreditCard.caption;
    nav: string = Constants.Modules.Billing.children.ProcessCreditCard.nav;
    id: string = Constants.Modules.Billing.children.ProcessCreditCard.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        FwMenu.addFormMenuButtons(options);
    }
    //----------------------------------------------------------------------------------------------
    getModuleScreen(): any {
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

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValueByDataField($form, 'OrderId', parentmoduleinfo.OrderId, parentmoduleinfo.ICode);
            FwFormField.setValueByDataField($form, 'ItemDescription', parentmoduleinfo.Description);
        }
        /*
        //disables asterisk and save prompt
        $form.off('change keyup', '.fwformfield[data-enabled="true"]:not([data-isuniqueid="true"][data-datafield=""])');
        FwFormField.setValueByDataField($form, 'Quantity', 1);

        this.events($form);

        if (typeof parentmoduleinfo !== 'undefined') {
            FwFormField.setValue($form, '.itemid[data-displayfield="BarCode"]', parentmoduleinfo.ItemId, parentmoduleinfo.BarCode);
            FwFormField.setValue($form, '.itemid[data-displayfield="SerialNumber"]', parentmoduleinfo.ItemId, parentmoduleinfo.SerialNumber)
            FwFormField.setValueByDataField($form, 'InventoryId', parentmoduleinfo.InventoryId, parentmoduleinfo.ICode);
            FwFormField.setValueByDataField($form, 'ItemDescription', parentmoduleinfo.Description);
        }
        */
        return $form;
    };
    //----------------------------------------------------------------------------------------------
    events($form) {
        /*
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

                        // Refresh parent form
                        const $tab = FwTabs.getTabByElement($form);
                        const parentTabId = $tab.data('parenttabid');
                        if (typeof parentTabId === 'string') {
                            const $tabControl = jQuery('#moduletabs');
                            const $parentForm = $tabControl.find(`div[data-tabid="${parentTabId}"] .fwform`);
                            FwModule.refreshForm($parentForm);
                        }
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
        */
    }
    //----------------------------------------------------------------------------------------------
    afterLoad($form) {
        //$form.find('.itemid[data-displayfield="BarCode"] input').focus();
    }
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        const warehouse = JSON.parse(sessionStorage.getItem('warehouse'));
        /*
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
        */
    }
    //----------------------------------------------------------------------------------------------
    getFormTemplate(): string {
        return `
            <div id="processpaymentform" class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form" data-version="1" data-caption="Inventory Retire Utility" data-rendermode="template" data-tablename="" data-mode="" data-hasaudit="false" data-controller="ProcessCreditCardController">
              <div id="inventoryretireutilityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs"></div>
                <div class="tabpages">
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order" style="max-width:700px">
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer No" data-datafield="CustomerNo"  data-readonly="true"></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Name" data-datafield="Customer" data-readonly="true"></div>
                      </div>
                      <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OrderNo"  data-readonly="true""></div>
                        <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Description" data-datafield="OrderDescription" data-readonly="true"></div>
                      </div>
                  </div>
                </div>
              </div>
            </div>`;
    }
    //----------------------------------------------------------------------------------------------
}
var ProcessCreditCardController = new ProcessCreditCard();