﻿class OrderItemGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'OrderItemGrid';
        this.apiurl = 'api/v1/orderitem';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

}

var OrderItemGridController = new OrderItemGrid();
//----------------------------------------------------------------------------------------------