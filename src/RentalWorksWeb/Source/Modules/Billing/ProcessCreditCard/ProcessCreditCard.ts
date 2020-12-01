routes.push({ pattern: /^module\/processcreditcard$/, action: function (match: RegExpExecArray) { return ProcessCreditCardController.getModuleScreen(); } });

//----------------------------------------------------------------------------------------------
class ProcessCreditCard {
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
                const PINPad_Code = FwFormField.getValueByDataField(options.$form, 'PINPad_Code');
                if (PINPad_Code.length === 0) {
                    FwFunc.showMessage('PIN Pad Code is required.');
                    return;
                }

                const Payment_AmountToPay = parseFloat(FwFormField.getValueByDataField(options.$form, 'Payment_AmountToPay'));
                if (Payment_AmountToPay <= 0) {
                    FwFunc.showMessage('Amount to pay must be greater than 0.');
                    return;
                }

                const processor = ProcessCreditCardFactory('Vistek')
                processor.process(options.$form);
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

        // setup the Totals switch
        const $totalsItems = FwFormField.getDataField($form, 'totalsItems');
        FwFormField_togglebuttons.loadItems($totalsItems, [
            { caption: 'Weekly', value: 'Weekly', checked: false },
            { caption: 'Period', value: 'Period', checked: false },
            { caption: 'Replacement', value: 'Replacement', checked: true }
        ]);
        $totalsItems.on('click', (e: JQuery.ClickEvent) => {
            this.updateTotalItemsPanels($form);
        });

        var $PINPad_Code = FwFormField.getDataField($form, 'PINPad_Code');
        $PINPad_Code.data('onchange', ($tr: JQuery, $formfield: JQuery) => {
            const $browse = $tr.closest('.fwbrowse');
            const description = FwBrowse.getValueByDataField($browse, $tr, 'Description');
            FwFormField.setValueByDataField($form, 'PINPad_Description', description);
        });

        setTimeout(() => {
            FwFormField.setValueByDataField($form, 'totalsItems', 'Replacement');
            this.updateTotalItemsPanels($form);
        }, 40);

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
                FwFormField.setValueByDataField($form, 'Payment_TotalAmount', FwFormField.getValueByDataField($form, 'Totals_Weekly_GrandTotal'));
                break;
            case 'Period':
                $form.find('.periodPanel').show();
                FwFormField.setValueByDataField($form, 'Payment_TotalAmount', FwFormField.getValueByDataField($form, 'Totals_Period_GrandTotal'));
                break;
            case 'Replacement':
                $form.find('.replacementPanel').show();
                FwFormField.setValueByDataField($form, 'Payment_TotalAmount', FwFormField.getValueByDataField($form, 'Totals_Replacement_DepositDue'));
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
            <div class="fwcontrol fwcontainer fwform" data-control="FwContainer" data-type="form"  data-hasaudittab="false"  data-controller="ProcessCreditCardController">
              <div data-control="FwFormField" data-type="key" class="fwcontrol fwformfield" data-isuniqueid="true" data-datafield="OrderId"></div>
              <div class="fwcontrol fwtabs" data-control="FwTabs" data-type="">
                <div class="tabs">
                    <div data-type="tab" id="processpaymenttab" class="tab" data-tabpageid="processpaymenttabpage" data-caption="Payment Info"></div>
                </div>
                <div class="tabpages">
                  <!--PAYMENT INFO TAB-->
                  <div data-type="tabpage" id="processpaymenttabpage" class="tabpage" data-tabid="processpaymenttab">
                      <div class="flexrow">
                        <div style="min-width:350px">
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
                                  <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Total Replacement Cost" data-datafield="Totals_Replacement_ReplacementCost" data-enabled="false"></div>
                                </div>
                                <div class="flexrow">
                                  <div data-control="FwFormField" data-type="percent" class="fwcontrol fwformfield" data-caption="Deposit Percentage" data-datafield="Totals_Replacement_DepositPercentage" data-enabled="false"></div>
                                  <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Deposit Due" data-datafield="Totals_Replacement_DepositDue" data-enabled="false"></div>
                                </div>
                            </div>
                          </div>
                        </div>
                        <div style="min-width:350px">
                          <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="PIN Pad" style="max-width:700px">
                            <div class="flexrow">
                              <div data-control="FwFormField" data-type="validation" class="fwcontrol fwformfield" data-caption="PIN Pad Code" data-datafield="PINPad_Code" data-displayfield="Code" data-validationname="CreditCardPinPadValidation" data-required="true"></div>
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
                              <div data-control="FwFormField" data-type="money" class="fwcontrol fwformfield" data-caption="Amount to Pay" data-datafield="Payment_AmountToPay" data-required="true"></div>
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
var ProcessCreditCardController = new ProcessCreditCard();