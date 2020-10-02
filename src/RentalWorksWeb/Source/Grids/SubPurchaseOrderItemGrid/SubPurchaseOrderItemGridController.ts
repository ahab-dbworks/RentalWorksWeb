class SubPurchaseOrderItemGrid {
    Module: string = 'SubPurchaseOrderItemGrid';
    apiurl: string = 'api/v1/subpurchaseorderitem';

    addLegend($control) {
        FwBrowse.addLegend($control, 'Vendor', '#ffd666'); // orange
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="QuantityOrdered"]').on('change', 'input.value', e => {
            const itemClass = FwBrowse.getValueByDataField($control, $generatedtr, 'ItemClass');
            if (itemClass == 'K' || itemClass == 'C' || itemClass == 'N') {
                OrderItemGridController.updateAccessoryQuantities($control, $generatedtr, e, 'QuantityOrdered');
            }
        });
    }
};
//----------------------------------------------------------------------------------------------
var SubPurchaseOrderItemGridController = new SubPurchaseOrderItemGrid();