class ContractDetailGrid {
    Module: string = 'ContractDetailGrid';
    apiurl: string = 'api/v1/contractitemdetail';

    generateRow($control, $generatedtr) {
        const $form = $control.closest('.fwform');
        // Bold Row
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            if ($tr.find('[data-browsedatafield="IsVoid"]').attr('data-originalvalue') === 'true') {
                $tr.css('text-decoration', 'line-through');
                $tr.find('td.column div.field').css('background-color', '#00ffff');
            }
        });
    }

    addLegend($control) {
        try {
            FwAppData.apiMethod(true, 'GET', `${this.apiurl}/legend`, null, FwServices.defaultTimeout, response => {
                for (let key in response) {
                    FwBrowse.addLegend($control, key, response[key]);
                }
            }, ex => {
                FwFunc.showError(ex);
            }, $control);
        } catch (ex) {
            FwFunc.showError(ex);
        }
    }
}
//----------------------------------------------------------------------------------------------
//Void Items
FwApplicationTree.clickEvents[Constants.Grids.ContractDetailGrid.menuItems.VoidItems.id] = function (event: JQuery.ClickEvent) {
    const $grid = jQuery(this).closest('.fwbrowse');
    const $form = jQuery(this).closest('.fwform');
    try {
        const contractItems: any = [];
        const $selectedCheckBoxes = $grid.find('tbody .cbselectrow:checked');
        const contractId = FwFormField.getValueByDataField($form, 'ContractId');

        if ($selectedCheckBoxes.length > 0) {
            const $confirmation = FwConfirmation.renderConfirmation('Void Contract Activity', '');
            const $select = FwConfirmation.addButton($confirmation, 'OK', false);
            FwConfirmation.addButton($confirmation, 'Cancel', true);
            const html = [];
            const contractType = FwFormField.getValueByDataField($form, 'ContractType');
            if (contractType === 'OUT') {
                html.push('<div data-control="FwFormField" data-type="checkbox" class="fwcontrol fwformfield" data-caption="Unstage items after voiding" data-datafield="ReturnToInventory"></div>');
            }
            html.push('<div data-control="FwFormField" data-type="textarea" class="fwcontrol fwformfield" data-caption="Reason" data-datafield="Reason" style="width:600px;"></div>');
            FwConfirmation.addControls($confirmation, html.join(''));

            $select.on('click', () => {
                for (let i = 0; i < $selectedCheckBoxes.length; i++) {
                    const $this = jQuery($selectedCheckBoxes[i]);
                    const $tr = $this.closest('tr')
                    const orderId = $tr.find('div[data-browsedatafield="OrderId"]').attr('data-originalvalue');
                    const orderItemId = $tr.find('div[data-browsedatafield="OrderItemId"]').attr('data-originalvalue');
                    const vendorId = $tr.find('div[data-browsedatafield="VendorId"]').attr('data-originalvalue');
                    const barCode = $tr.find('div[data-browsedatafield="Barcode"]').attr('data-originalvalue');
                    const quantity = $tr.find('div[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                    const item = {
                        OrderId: orderId
                        , OrderItemId: orderItemId
                        , VendorId: vendorId
                        , BarCode: barCode
                        , Quantity: quantity
                    }
                    contractItems.push(item);
                };

                const request: any = {
                    Items: contractItems
                    , ContractId: contractId
                    , Reason: FwFormField.getValueByDataField($confirmation, 'Reason')
                };

                if (contractType === 'OUT') {
                    let returnToInventory = FwFormField.getValueByDataField($confirmation, 'ReturnToInventory');
                    returnToInventory = returnToInventory === 'T' ? true : false;
                    request.ReturnToInventory = returnToInventory;
                }
            
                FwAppData.apiMethod(true, 'POST', `api/v1/contractitemdetail/voiditems`, request, FwServices.defaultTimeout,
                    response => {
                        if (!response.success) {
                            FwNotification.renderNotification('ERROR', response.msg);
                        };
                        FwBrowse.search($grid);
                        FwConfirmation.destroyConfirmation($confirmation);
                    },
                    ex => FwFunc.showError(ex), $confirmation.find('.fwconfirmationbox'));
            });
        }
    }
    catch (ex) {
        FwFunc.showError(ex);
    }
};
//----------------------------------------------------------------------------------------------
var ContractDetailGridController = new ContractDetailGrid();
//----------------------------------------------------------------------------------------------