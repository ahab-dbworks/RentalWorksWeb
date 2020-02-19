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
            });
        });
    }
    //----------------------------------------------------------------------------------------------
    decreaseQuantity($control: JQuery, $tr: JQuery) {
        const inventoryId = FwBrowse.getValueByDataField($control, $tr, 'InventoryId');
        const orderItemId = FwBrowse.getValueByDataField($control, $tr, 'OrderItemId');
        const quantity = FwBrowse.getValueByDataField($control, $tr, 'QuantityOrdered');
        const orderId = FwBrowse.getValueByDataField($control, $tr, 'OrderId');
        const description = FwBrowse.getValueByDataField($control, $tr, 'Description');
        const iCode = $tr.find('[data-browsedatafield="InventoryId"]').attr('data-originaltext');
        const html = `
                <div id="decreaseOrderQty">
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="I-Code" data-datafield="InventoryId" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="text" data-enabled="false" class="fwcontrol fwformfield" data-caption="Description" data-datafield="Description" style="flex:1 1 75px;"></div>
                    </div>
                    <div class="flexrow">
                        <div data-control="FwFormField" data-type="number" data-enabled="false" class="fwcontrol fwformfield" data-caption="Ordered" data-datafield="Ordered" style="flex:1 1 75px;"></div>
                        <div data-control="FwFormField" data-type="number" class="fwcontrol fwformfield" data-caption="Qty to Decrease" data-datafield="QtyToDecrease" style="flex:1 1 75px;"></div>                
                    </div>
                </div>`;

        const $confirmation = FwConfirmation.renderConfirmation(`Decrease Quantity`, html);
        FwControl.renderRuntimeControls($confirmation.find('.fwcontrol'));
        const $ok = FwConfirmation.addButton($confirmation, 'OK', false);

        FwFormField.setValueByDataField($confirmation, 'InventoryId', iCode);
        FwFormField.setValueByDataField($confirmation, 'Description', description);
        FwFormField.setValueByDataField($confirmation, 'Ordered', quantity);
        FwConfirmation.addButton($confirmation, 'Cancel', true);

        $ok.on('click', e => {
            const request: any = {
                Quantity: FwFormField.getValueByDataField($confirmation, 'QtyToDecrease'),
                OrderId: orderId,
                OrderItemId: orderItemId,
                InventoryId: inventoryId
            };

            FwAppData.apiMethod(true, 'POST', `api/v1/checkoutpendingitem/addtoorder`, request, FwServices.defaultTimeout,
                response => {
                    const pageNo = parseInt($control.attr('data-pageno'));
                    const onDataBind = $control.data('ondatabind');
                    if (typeof onDataBind == 'function') {
                        $control.data('ondatabind', request => {
                            onDataBind(request);
                            request.pageno = pageNo;
                        });
                    }
                    FwConfirmation.destroyConfirmation($confirmation);
                    FwBrowse.search($control);
                }, ex => FwFunc.showError(ex), $control);
        })
    }
    //----------------------------------------------------------------------------------------------
}

var CheckOutPendingItemGridController = new CheckOutPendingItemGrid();
//----------------------------------------------------------------------------------------------