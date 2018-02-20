class DiscountItemRentalGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'DiscountItemRentalGrid';
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
            var InventoryTypeValue = jQuery($grid.find('tr.editrow [data-formvalidationname="InventoryTypeValidation"] input')).val();
            var CategoryTypeId = jQuery($grid.find('tr.editrow [data-formvalidationname="RentalCategoryValidation"] input')).val();
            var SubCategoryTypeId = jQuery($grid.find('tr.editrow [data-formvalidationname="SubCategoryValidation"] input')).val();

            switch (validationName) {
                case 'InventoryTypeValidation':
                    request.uniqueids = {
                        Rental: true
                    };
                    break;
                case 'RentalCategoryValidation':
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
                case 'RentalInventoryValidation':
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
var DiscountItemRentalGridController = new DiscountItemRentalGrid();
//----------------------------------------------------------------------------------------------