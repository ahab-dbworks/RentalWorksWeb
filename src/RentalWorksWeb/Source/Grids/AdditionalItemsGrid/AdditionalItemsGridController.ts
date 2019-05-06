class AdditionalItemsGrid {
    Module: string = 'AdditionalItemsGrid';
    apiurl: string = 'api/v1/vendorinvoiceitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="QuantityOrdered"] input').val("1");
        });
    }

    beforeValidate($browse, $grid, request, datafield, $tr) {
        request.uniqueIds = {
            GlAccountType: 'ASSET'
        };
    }
}
//----------------------------------------------------------------------------------------------
var AdditionalItemsGridController = new AdditionalItemsGrid();