class InventoryAttributeValueGrid {
    Module: string = 'InventoryAttributeValueGrid';
    apiurl: string = 'api/v1/inventoryattributevalue';

    //----------------------------------------------------------------------------------------------
    //beforeValidateAttribute = function ($browse, $grid, request, datafield, $tr) {
    //    //let carrierId = $tr.find('.field[data-browsedatafield="CarrierId"] input').val();
    //    request.uniqueIds = {
    //        HasValues: true,
    //    };
    //};
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'AttributeId':
                request.uniqueids = {
                    HasValues: true
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateattribute`);
                break;
            case 'AttributeValueId': 
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateattributevalue`);
        }
    }

}

var InventoryAttributeValueGridController = new InventoryAttributeValueGrid();
//----------------------------------------------------------------------------------------------