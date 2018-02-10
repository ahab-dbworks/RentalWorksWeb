﻿class DiscountItemSalesGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemSalesGrid';
        this.apiurl = 'api/v1/discountitem';
    }

    beforeValidate = function ($browse, $grid, request) {

        var validationName = request.module;

        if (validationName != null) {
            var InventoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="InventoryTypeValidation"] input')).val();
            var CategoryTypeId = jQuery($grid.find('tr.editrow [data-validationname="SalesCategoryValidation"] input')).val();
            var SubCategoryTypeId = jQuery($grid.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();

            switch (validationName) {
                case 'InventoryTypeValidation':
                    request.uniqueids = {
                        Sales: true
                    };
                    break;
                case 'SalesCategoryValidation':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue
                    };
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids = {
                        TypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId
                    };
                    break;
                case 'SalesInventoryValidation':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId,
                        SubCategoryId: SubCategoryTypeId
                    };
                    break;
            };
        }
    }
}

(<any>window).DiscountItemSalesGridController = new DiscountItemSalesGrid();
//----------------------------------------------------------------------------------------------