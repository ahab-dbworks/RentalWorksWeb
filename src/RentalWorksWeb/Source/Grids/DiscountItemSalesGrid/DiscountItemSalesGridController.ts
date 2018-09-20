class DiscountItemSalesGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemSalesGrid';
        this.apiurl = 'api/v1/discountitem';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

    beforeValidate = function ($browse, $grid, request) {
        var validationName = request.module;

        if (validationName != null) {
            var InventoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="InventoryTypeValidation"] input')).val();
            var CategoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="SalesCategoryValidation"] input')).val();
            var SubCategoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();
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
                        CategoryId: CategoryTypeValue
                    };
                    break;
                case 'SalesInventoryValidation':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeValue,
                        SubCategoryId: SubCategoryTypeValue
                    };
                    break;
            };
        }
    }
}

var DiscountItemSalesGridController = new DiscountItemSalesGrid();
//----------------------------------------------------------------------------------------------