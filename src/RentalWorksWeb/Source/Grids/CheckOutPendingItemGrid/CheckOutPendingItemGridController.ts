class CheckOutPendingItemGrid {
    Module: string = 'CheckOutPendingItemGrid';
    apiurl: string = 'api/v1/checkoutpendingitem';

    //----------------------------------------------------------------------------------------------
    //justin 10/29/2019. Concept copied from QuikActivityGrid.  Thanks Jason!
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            const recType = FwBrowse.getValueByDataField($control, $tr, 'RecType'); // should only be "R" or "S"
            let inventoryControllerValidation: string = "";
            switch (recType) {
                case "R": inventoryControllerValidation = "RentalInventoryValidation";
                    break;
                case "S": inventoryControllerValidation = "SalesInventoryValidation";
                    break;
                default: inventoryControllerValidation = "";
                    break;
            }
            $tr.find('[data-browsedisplayfield="ICode"]').attr('data-validationname', inventoryControllerValidation);

            const $browsecontextmenu = $tr.find('.browsecontextmenu');
            $browsecontextmenu.data('contextmenuoptions', $tr => {
                FwContextMenu.addMenuItem($browsecontextmenu, `Decrease Quantity Ordered`, () => {
                    try {
                        this.decreaseQuantity($control, $tr);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
                FwContextMenu.addMenuItem($browsecontextmenu, `Substitute Items`, () => {
                    try {
                        this.substituteItems($control, $tr);
                    } catch (ex) {
                        FwFunc.showError(ex);
                    }
                });
            });
        });
    }
    //----------------------------------------------------------------------------------------------
    substituteItems($control: JQuery, $tr: JQuery) {
        const orderItemId = FwBrowse.getValueByDataField($control, $tr, 'OrderItemId');
        const quantity = FwBrowse.getValueByDataField($control, $tr, 'QuantityOrdered');
        const orderId = FwBrowse.getValueByDataField($control, $tr, 'OrderId');
        const description = FwBrowse.getValueByDataField($control, $tr, 'Description');
        const remaining = FwBrowse.getValueByDataField($control, $tr, 'MissingQuantity');
        const iCode = $tr.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext');

        const request: any = {
            OrderId: orderId,
            OrderItemId: orderItemId
        }

        FwAppData.apiMethod(true, 'POST', 'api/v1/checkout/startsubstitutesession', request, FwServices.defaultTimeout,
            response => {
                const sessionId = response.SessionId;
                const html = `
                <div id="substituteItems">
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" style="flex:0 1 200px;"></div>
                        <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 400px;"></div>
                    </div>
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" data-enabled="false" class="fwcontrol fwformfield" data-caption="Ordered" data-datafield="Ordered" style="flex:0 1 100px;"></div>
                        <div data-control="FwFormField" data-type="number" data-enabled="false" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="Remaining" style="flex:0 1 100px;"></div>                       
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty to Substitute" data-datafield="QtyToSubstitute" style="flex:0 1 100px;"></div>                
                    </div>
                    <div class="fwcontrol fwcontainer fwform-section" data-control="FwContainer" data-type="section" data-caption="Items to Substitute">
                        <div class="flexrow">
                            <div data-control="FwFormField" data-type="text" class="fwcontrol fwformfield" data-caption="Bar Code / I-Code" data-datafield="Code" style="flex:0 1 200px;"></div>
                            <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="SubstituteItemICode" style="flex:0 1 200px;"></div>
                            <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="Description" data-datafield="SubstituteItemDescription" style="flex:1 1 400px;"></div>
                        </div>
                        <div class="flexrow"> 
                            <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Quantity" data-datafield="Quantity" style="flex:0 1 100px;"></div>
                        </div>
                    </div>
                    <div class="error-msg"></div>
                    <div class="flexrow">
                        <div data-control="FwGrid" data-grid="CheckOutSubstituteSessionItemGrid"></div>
                    </div>
                </div>`;

                const $confirmation = FwConfirmation.renderConfirmation(`Substitute Items`, html);
                FwControl.renderRuntimeControls($confirmation.find('.fwcontrol'));
                const $ok = FwConfirmation.addButton($confirmation, 'Apply Substitutes');

                FwFormField.setValueByDataField($confirmation, 'InventoryId', iCode);
                FwFormField.setValueByDataField($confirmation, 'Description', description);
                FwFormField.setValueByDataField($confirmation, 'Ordered', quantity);
                FwFormField.setValueByDataField($confirmation, 'Remaining', remaining);
                FwFormField.setValueByDataField($confirmation, 'QtyToSubstitute', 1);
                const $cancel = FwConfirmation.addButton($confirmation, 'Cancel', false);
                $confirmation.find('.fwconfirmationbox').css('width', '40vw');

                const $grid = FwBrowse.renderGrid({
                    nameGrid: 'CheckOutSubstituteSessionItemGrid',
                    gridSecurityId: 'qCquw4GIfqRW5',
                    moduleSecurityId: StagingCheckoutController.id,
                    $form: $confirmation,
                    pageSize: 20,
                    addGridMenu: (options: IAddGridMenuOptions) => {
                        options.hasNew = false;
                    },
                    onDataBind: (request: any) => {
                        request.uniqueids = {
                            SessionId: sessionId
                        };
                    }
                });
                FwBrowse.search($grid);

                $confirmation.on('change', '[data-datafield="Code"], [data-datafield="Quantity"]', e => {
                    const code = FwFormField.getValueByDataField($confirmation, 'Code');
                    const quantity = FwFormField.getValueByDataField($confirmation, 'Quantity');
                    const request: any = {
                        SessionId: sessionId,
                        Code: code,
                        WarehouseId: JSON.parse(sessionStorage.getItem('warehouse')).warehouseid,
                        Quantity: quantity
                    };

                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/addsubstituteitemtosession`, request, FwServices.defaultTimeout,
                        response => {
                            if (response.success) {
                                $confirmation.find('.error-msg').html('');
                                FwFormField.setValueByDataField($confirmation, 'Code', '', '', false);
                                $confirmation.find('[data-datafield="Quantity"] input').val('');
                                FwBrowse.search($grid);
                                $confirmation.find('[data-datafield="Code"] input').focus();
                            } else {
                                if (response.status === 107) {
                                    $confirmation.find('[data-datafield="Quantity"] input').focus();
                                } else {
                                    $confirmation.find('[data-datafield="Code"] input').select();
                                    $confirmation.find('.error-msg').html(`<span style="margin-left:1em; color:white; background:red;">${response.msg}</span>`);
                                }
                            }
                            FwFormField.setValueByDataField($confirmation, 'SubstituteItemICode', response.ICode);
                            FwFormField.setValueByDataField($confirmation, 'SubstituteItemDescription', response.Description);
                        }, ex => FwFunc.showError(ex), $grid);
                });

                $confirmation.find('[data-datafield="Code"] input').focus();

                $ok.on('click', e => {
                    const request: any = {
                        SessionId: sessionId,
                    };

                    FwAppData.apiMethod(true, 'POST', `api/v1/checkout/applysubstitutesession`, request, FwServices.defaultTimeout, function onSuccess(response) {
                        if (response.success) {
                            FwConfirmation.destroyConfirmation($confirmation);
                            FwBrowse.search($control);
                        } else {
                            $confirmation.find('.error-msg').html(`<span style="margin-left:1em; color:white; background:red;">${response.msg}</span>`);
                        }
                    }, null, $control);
                })

                $cancel.on('click', e => {
                    const rowCount = $grid.data('totalRowCount');
                    if (rowCount) {
                        const $confirmClose = FwConfirmation.renderConfirmation(`Cancel Substitute`, 'Exit without applying substitutes?');
                        const $confirm = FwConfirmation.addButton($confirmClose, 'Yes');
                        FwConfirmation.addButton($confirmClose, 'No');
                        $confirm.on('click', e => {
                            FwConfirmation.destroyConfirmation($confirmation);
                        });
                    } else {
                        FwConfirmation.destroyConfirmation($confirmation);
                    }

                });
            }, ex => FwFunc.showError(ex), $control);
    }
    //----------------------------------------------------------------------------------------------
    decreaseQuantity($control: JQuery, $tr: JQuery) {
        const inventoryId = FwBrowse.getValueByDataField($control, $tr, 'InventoryId');
        const orderItemId = FwBrowse.getValueByDataField($control, $tr, 'OrderItemId');
        const quantity = FwBrowse.getValueByDataField($control, $tr, 'QuantityOrdered');
        const orderId = FwBrowse.getValueByDataField($control, $tr, 'OrderId');
        const description = FwBrowse.getValueByDataField($control, $tr, 'Description');
        const remaining = FwBrowse.getValueByDataField($control, $tr, 'MissingQuantity');
        const iCode = $tr.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext');
        const html = `
                <div id="decreaseOrderQty">
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" style="flex:1 1 0px;"></div>
                        <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 25vw;"></div>
                    </div>
                    <div class="flexrow" style="max-width:21vw;">
                        <div data-control="FwFormField" data-type="number" data-enabled="false" class="fwcontrol fwformfield" data-caption="Ordered" data-datafield="Ordered" style="flex:1 1 7vw;"></div>
                        <div data-control="FwFormField" data-type="number" data-enabled="false" class="fwcontrol fwformfield" data-caption="Remaining" data-datafield="Remaining" style="flex:1 1 7vw;"></div>                       
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty to Decrease" data-datafield="QtyToDecrease" style="flex:1 1 7vw;"></div>                
                    </div>
                </div>`;

        const $confirmation = FwConfirmation.renderConfirmation(`Decrease Quantity`, html);
        FwControl.renderRuntimeControls($confirmation.find('.fwcontrol'));
        const $ok = FwConfirmation.addButton($confirmation, 'OK', false);

        FwFormField.setValueByDataField($confirmation, 'InventoryId', iCode);
        FwFormField.setValueByDataField($confirmation, 'Description', description);
        FwFormField.setValueByDataField($confirmation, 'Ordered', quantity);
        FwFormField.setValueByDataField($confirmation, 'Remaining', remaining);
        FwFormField.setValueByDataField($confirmation, 'QtyToDecrease', remaining);
        FwConfirmation.addButton($confirmation, 'Cancel', true);
        $confirmation.find('.fwconfirmationbox').css('width', '40vw');

        $ok.on('click', e => {
            const request: any = {
                Quantity: FwFormField.getValueByDataField($confirmation, 'QtyToDecrease'),
                OrderId: orderId,
                OrderItemId: orderItemId,
                InventoryId: inventoryId
            };

            FwConfirmation.destroyConfirmation($confirmation);
            FwAppData.apiMethod(true, 'POST', `api/v1/checkout/decreaseorderquantity`, request, FwServices.defaultTimeout,
                response => {
                    const pageNo = parseInt($control.attr('data-pageno'));
                    const onDataBind = $control.data('ondatabind');
                    if (typeof onDataBind == 'function') {
                        $control.data('ondatabind', request => {
                            onDataBind(request);
                            request.pageno = pageNo;
                        });
                    }
                    FwBrowse.search($control);
                }, ex => FwFunc.showError(ex), $control);
        })
    }
    //----------------------------------------------------------------------------------------------
}

var CheckOutPendingItemGridController = new CheckOutPendingItemGrid();
//----------------------------------------------------------------------------------------------