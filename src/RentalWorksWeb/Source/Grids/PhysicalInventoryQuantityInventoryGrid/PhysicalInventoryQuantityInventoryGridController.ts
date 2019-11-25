class PhysicalInventoryQuantityInventoryGrid {
    Module: string = 'PhysicalInventoryQuantityInventoryGrid';
    apiurl: string = 'api/v1/physicalinventoryquantityinventory';
    addItemRequest: any = {};
    $form: any;
    errorMsg: any;
    $trForAddItem: any;
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        this.$form = $control.closest('.fwform');
        const $quantityColumn = $generatedtr.find('[data-browsedatatype="numericupdown"]');
        this.errorMsg = this.$form.find('.error-msg.qty');

        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            $quantityColumn.on('change', '.value', e => {
                let request: any = {};
                const code = $tr.find('[data-browsedatafield="ICode"]').attr('data-originalvalue');
                const physicalInventoryId = $tr.find('[data-browsedatafield="PhysicalInventoryId"]').attr('data-originalvalue');
                const newValue = jQuery(e.currentTarget).val();
                const oldValue = $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue');
                const quantity = Number(newValue) - Number(oldValue);

                request = {
                    ICode: code,
                    Quantity: quantity,
                    PhysicalInventoryId: physicalInventoryId,
                }
                if (quantity != 0) {
                    FwAppData.apiMethod(true, 'POST', "api/v1/physicalinventory/countquantity", request, FwServices.defaultTimeout, response => {
                        if (response.success) {
                            $tr.find('[data-browsedatafield="Quantity"]').attr('data-originalvalue', Number(newValue));
                        }
                    },
                        function onError(response) {
                            $tr.find('[data-browsedatafield="Quantity"] input').val(Number(oldValue));
                        }, this.$form);
                }
            });
        });
    }
}

var PhysicalInventoryQuantityInventoryGridController = new PhysicalInventoryQuantityInventoryGrid();
//----------------------------------------------------------------------------------------------