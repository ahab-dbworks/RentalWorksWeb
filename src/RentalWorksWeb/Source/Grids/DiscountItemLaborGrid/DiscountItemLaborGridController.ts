﻿class DiscountItemLaborGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemLaborGrid';
        this.apiurl = 'api/v1/discountitem';
    }

    beforeValidate = function ($browse, $grid, request) {

        var validationName = request.module;

        if (validationName != null) {
            var LaborTypeValue = jQuery($grid.find('tr.editrow [data-validationname="LaborTypeValidation"] input')).val();
            var CategoryValue = jQuery($grid.find('tr.editrow [data-validationname="LaborCategoryValidation"] input')).val();
            var SubCategoryValue = jQuery($grid.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();

            switch (validationName) {
                case 'LaborCategoryValidation':
                    request.uniqueids = {
                        LaborTypeId: LaborTypeValue
                    };
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids = {
                        CategoryId: CategoryValue
                    };
                    break;
                case 'LaborRateValidation':
                    request.uniqueids = {
                        LaborTypeId: LaborTypeValue,
                        CategoryId: CategoryValue,
                        SubCategoryId: SubCategoryValue
                    };
                    break;
            };
        }
    }
}

(<any>window).DiscountItemLaborGridController = new DiscountItemLaborGrid();
//----------------------------------------------------------------------------------------------