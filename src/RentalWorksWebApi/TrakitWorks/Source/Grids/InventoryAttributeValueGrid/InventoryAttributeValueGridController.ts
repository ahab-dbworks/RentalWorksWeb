class InventoryAttributeValueGrid {
    Module: string = 'InventoryAttributeValueGrid';
    apiurl: string = 'api/v1/inventoryattributevalue';

    //----------------------------------------------------------------------------------------------
    beforeValidateAttribute = function ($browse, $grid, request, datafield, $tr) {
        //let carrierId = $tr.find('.field[data-browsedatafield="CarrierId"] input').val();
        request.uniqueIds = {
            HasValues: true,
        };
    };
    //----------------------------------------------------------------------------------------------

}

var InventoryAttributeValueGridController = new InventoryAttributeValueGrid();
//----------------------------------------------------------------------------------------------