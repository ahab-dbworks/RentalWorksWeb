class DiscountItemMiscGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemMiscGrid';
        this.apiurl = 'api/v1/discountitem';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

    beforeValidate = function ($browse, $grid, request) {

        var validationName = request.module;

        if (validationName != null) {
            var MiscTypeValue = jQuery($grid.find('tr.editrow [data-validationname="MiscTypeValidation"] input')).val();
            var CategoryValue = jQuery($grid.find('tr.editrow [data-validationname="MiscCategoryValidation"] input')).val();
            var SubCategoryValue = jQuery($grid.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();
          
            switch (validationName) {
                case 'MiscCategoryValidation':
                    request.uniqueids = {
                        MiscTypeId: MiscTypeValue
                    };
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids = {
                        TypeId: MiscTypeValue,
                        CategoryId: CategoryValue
                    };
   
                    break;
                case 'MiscRateValidation':
                    request.uniqueids = {
                        MiscTypeId: MiscTypeValue,
                        CategoryId: CategoryValue,
                        SubCategoryId: SubCategoryValue
                    };
                    break;
            };
        }
    }
}

var DiscountItemMiscGridController = new DiscountItemMiscGrid();
//----------------------------------------------------------------------------------------------