﻿routes.push({ pattern: /^module\/processcreditcard$/, action: function (match: RegExpExecArray) { return ProcessCreditCardController.getModuleScreen(); } });

class ProcessCreditCard{
    Module: string = 'ProcessCreditCard';
    apiurl: string = 'api/v1/processcreditcard';
    caption: string = Constants.Modules.Billing.children.ProcessCreditCard.caption;
    nav: string = Constants.Modules.Billing.children.ProcessCreditCard.nav;
    id: string = Constants.Modules.Billing.children.ProcessCreditCard.id;
    //----------------------------------------------------------------------------------------------
    addFormMenuItems(options: IAddFormMenuOptions): void {
        options.hasSave = false;
        const $btnProcessPayment = FwMenu.addStandardBtn(options.$menu, 'Process Payment', 'pvc2YoVG316N')
        $btnProcessPayment.on('click', async (e: JQuery.ClickEvent) => {
            try {
                const $confirmation = FwConfirmation.renderConfirmation('Confirm', 'Process Credit Card Payment?');
                const $btnOk = FwConfirmation.addButton($confirmation, 'OK', true);
                $btnOk.on('click', async (e: JQuery.ClickEvent) => {
                    try {
                        const request = new FwAjaxRequest();
                        request.httpMethod = 'POST';
                        request.setWebApiUrl('/api/v1/processcreditcard/processcreditcard');
                        const datafields = FwReport.getParameters(options.$form);
                        request.data = {
                            datafields: datafields
                        };
                        var response = await FwAjax.callWebApi(request);
                        console.log(response);
                    }
                    catch (ex) {
                        FwFunc.showError(ex);
                    }
                    FwConfirmation.destroyConfirmation($confirmation)
                });

                const $btnCancel = FwConfirmation.addButton($confirmation, 'Cancel', false);
                $btnCancel.on('click', async (e: JQuery.ClickEvent) => {
                    FwConfirmation.destroyConfirmation($confirmation);
                });
            }
            catch (ex) {
                FwFunc.showError(ex);
            }
        });
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
        
        FwModule.loadForm(this.Module, $form);

        const $totalsItems = FwFormField.getDataField($form, 'totalsItems');
        FwFormField_togglebuttons.loadItems($totalsItems, [
            { caption: 'Weekly', value: 'Weekly', checked: false },
            { caption: 'Period', value: 'Period', checked: false },
            { caption: 'Replacement', value: 'Replacement', checked: true }
        ]);

        $totalsItems.find('.fwformfield-value').on('change', (e: JQuery.ChangeEvent) => {
            this.updateTotalItemsPanels($form);        
        });
        this.updateTotalItemsPanels($form);

        return $form;
    };
    //----------------------------------------------------------------------------------------------
    updateTotalItemsPanels($form: JQuery) {
        $form.find('.totalsitemspanel').hide();
        const $totalsItems = FwFormField.getDataField($form, 'totalsItems');
        const value = FwFormField.getValue2($totalsItems);
        switch (value) {
            case 'Weekly':
                $form.find('.weeklyPanel').show();
                break;
            case 'Period':
                $form.find('.periodPanel').show();
                break;
            case 'Replacement':
                $form.find('.replacementPanel').show();
                break;
        }
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
            <div class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form"  data-hasaudit="false"  data-controller="ProcessCreditCardController">
              <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="OrderId"></div>
              <div id="inventoryretireutilityform-tabcontrol" class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs"></div>
                <div class="tabpages">
                  <div class="flexrow">
                    <div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Order" style="max-width:700px">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer No" data-datafield="CustomerNo"  data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Customer Name" data-datafield="Customer" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order No" data-datafield="OrderNo" data-enabled="false"></div>
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Order Description" data-datafield="OrderDescription" data-enabled="false"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Totals" style="max-width:700px">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="togglebuttons" class="fwcontrol fwformfield" data-caption="Totals" data-datafield="totalsItems"></div>
                        </div>
                        <div class="totalsitemspanel weeklyPanel" style="display:none">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Gross Total" data-datafield="Totals_Weekly_GrossTotal" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Discount" data-datafield="Totals_Weekly_Discount" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Sub-Total" data-datafield="Totals_Weekly_SubTotal" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Tax (X.XXX%)" data-datafield="Totals_Weekly_Tax" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Grand Total" data-datafield="Totals_Weekly_GrandTotal" data-enabled="false"></div>
                            </div>
                        </div>
                        <div class="totalsitemspanel periodPanel" style="display:none">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Gross Total" data-datafield="Totals_Period_GrossTotal" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Discount" data-datafield="Totals_Period_Discount" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Sub-Total" data-datafield="Totals_Period_SubTotal" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Tax (X.XXX%)" data-datafield="Totals_Period_Tax" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Grand Total" data-datafield="Totals_Period_GrandTotal" data-enabled="false"></div>
                            </div>
                        </div>
                        <div class="totalsitemspanel replacementPanel" style="display:none">                      
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Total Replacement Cost" data-datafield="Totals_Replacement_TotalReplacementCost" data-enabled="false"></div>
                            </div>
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Deposit Percentage" data-datafield="Totals_Replacement_DepositPercentage" data-enabled="false"></div>
                              <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Deposit Due" data-datafield="Totals_Replacement_DepositDue" data-enabled="false"></div>
                            </div>
                        </div>
                      </div>
                    </div>
                    <div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PIN Pad" style="max-width:700px">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="PIN Pad Code" data-datafield="PINPad_Code"  data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Description" data-datafield="PINPad_Description" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Type" data-datafield="PINPad_Type" data-enabled="false"></div>
                        </div>
                      </div>
                      <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Payment Amount" style="max-width:700px">
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Total Amount" data-datafield="Payment_TotalAmount" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deposit" data-datafield="Payment_Deposit" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Remaining Amount" data-datafield="Payment_RemainingAmount" data-enabled="false"></div>
                        </div>
                        <div class="flexrow">
                          <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount to Pay" data-datafield="Payment_AmountToPay"></div>
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
var ProcessCreditCardController = new ProcessCreditCard();