class POReceiveItemGrid {
    Module: string = 'POReceiveItemGrid';
    apiurl: string = 'api/v1/purchaseorderreceiveitem';
    successSoundFileName: string;
    errorSoundFileName: string;
    notificationSoundFileName: string;
    barCodedItemIncreased: boolean = false;

    generateRow($control, $generatedtr) {
        let $form, errorSound, successSound, $quantityColumn;
        $form = $control.closest('.fwform'),
            $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');
        this.successSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).successSoundFileName;
        this.errorSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).errorSoundFileName;
        this.notificationSoundFileName = JSON.parse(sessionStorage.getItem('sounds')).notificationSoundFileName;

        let self = this;
        errorSound = new Audio(this.errorSoundFileName);
        successSound = new Audio(this.successSoundFileName);
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            let quantityColorIndex = dt.ColumnIndex.QuantityColor;
            let color = dt.Rows[rowIndex][quantityColorIndex];
            if (color == "") {
                color = 'transparent';
            }
            let $grid = $tr.parents('[data-grid="POReceiveItemGrid"]');
           
            let cellToColor = $generatedtr.find('.cellcolor');
            cellToColor.css({
                'border-left': '20px solid',
                'border-right': '20px solid transparent',
                'border-bottom': '20px solid transparent',
                'left': '0',
                'top': '0',
                'height': '0',
                'width': '0',
                'position': 'absolute',
                'right': '0px',
                'border-left-color': color
            });
          
            $quantityColumn.on('change', '.value', e => {
                let request: any = {},
                    contractId = $tr.find('[data-browsedatafield="ContractId"]').attr('data-originalvalue'),
                    itemId = $tr.find('[data-browsedatafield="PurchaseOrderItemId"]').attr('data-originalvalue'),
                    poId = FwFormField.getValueByDataField($form, 'PurchaseOrderId'),
                    newValue = jQuery(e.currentTarget).val(),
                    oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue'),
                    quantity = Number(newValue) - Number(oldValue);

                request = {
                    ContractId: contractId,
                    PurchaseOrderItemId: itemId,
                    PurchaseOrderId: poId,
                    Quantity: quantity
                }

                if (quantity != 0) {
                    FwAppData.apiMethod(true, 'POST', "api/v1/purchaseorderreceiveitem/receiveitems", request, FwServices.defaultTimeout,
                        function onSuccess(response) {
                            $form.find('.error-msg').html('');
                            if (response.success) {
                                $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                                FwBrowse.setFieldValue($grid, $tr, 'QuantityReceived', { value: response.QuantityReceived });
                                if (response.QuantityColor) {
                                    $quantityColumn.find('.cellcolor').css('border-left', `20px solid ${response.QuantityColor}`);
                                } else {
                                    $quantityColumn.find('.cellcolor').css('border-left', `20px solid transparent`);
                                }
                            } else {
                                errorSound.play();
                                $form.find('.error-msg').html(`<div><span>${response.msg}</span></div>`);
                                $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                            }

                            let $itemsTrackedByBarcode = $control.find('[data-browsedatafield="TrackedBy"][data-originalvalue="BARCODE"]');
                            for (let i = 0; i < $itemsTrackedByBarcode.length; i++) {
                                let barcodeQuantity = jQuery($itemsTrackedByBarcode[i]).parents('tr').find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                                self.barCodedItemIncreased = false;
                                if (+barcodeQuantity > 0) {
                                    self.barCodedItemIncreased = true;
                                    break;
                                }
                            }
                            if (self.barCodedItemIncreased) {
                                $form.find('.createcontract[data-type="button"]').hide();
                                $form.find('.createcontract[data-type="btnmenu"]').show();
                            } else {
                                $form.find('.createcontract[data-type="button"]').show();
                                $form.find('.createcontract[data-type="btnmenu"]').hide();
                            }
                        },
                        null, null);
                }
            });
        });
    }
    addLegend($control) {
        FwBrowse.addLegend($control, 'Positive Conflict', '#239B56');
        FwBrowse.addLegend($control, 'Items Needing Bar Code/Serial No./RFID', '#6C3483');
    }
}

var POReceiveItemGridController = new POReceiveItemGrid();
//----------------------------------------------------------------------------------------------