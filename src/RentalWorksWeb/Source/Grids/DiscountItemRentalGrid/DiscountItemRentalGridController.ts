﻿class DiscountItemRentalGrid {
    Module: string = 'DiscountItemRentalGrid';
    apiurl: string = 'api/v1/discountitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        var validationName = request.module;
        if (validationName != null) {
            var InventoryTypeValue = FwBrowse.getValueByDataField($validationbrowse, $tr, 'InventoryTypeId');
            var CategoryTypeId = FwBrowse.getValueByDataField($validationbrowse, $tr, 'CategoryId');
            var SubCategoryTypeId = FwBrowse.getValueByDataField($validationbrowse, $tr, 'SubCategoryId');

            switch (validationName) {
                case 'InventoryTypeValidation':
                    request.uniqueids = {
                        Rental: true
                    };
                    break;
                case 'RentalCategoryValidation':
                    if (InventoryTypeValue != '') {
                        request.uniqueids = {
                            InventoryTypeId: InventoryTypeValue
                        };
                    }
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids = {
                        TypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId
                    };
                    break;
                case 'RentalInventoryValidation':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId,
                        SubCategoryId: SubCategoryTypeId
                    };
                    break;
            };

            for (var prop in request.uniqueids) {
                if (request.uniqueids[prop] === '') {
                    delete request.uniqueids[prop];
                }
            }
        }
    }
}
var DiscountItemRentalGridController = new DiscountItemRentalGrid();
//----------------------------------------------------------------------------------------------