class DiscountItemRentalGrid {
    Module: string = 'DiscountItemRentalGrid';
    apiurl: string = 'api/v1/discountitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        var validationName = request.module;
        if (validationName != null) {
            var InventoryTypeValue = jQuery($gridbrowse.find('tr.editrow [data-formvalidationname="InventoryTypeValidation"] input')).val();
            var CategoryTypeId = jQuery($gridbrowse.find('tr.editrow [data-formvalidationname="RentalCategoryValidation"] input')).val();
            var SubCategoryTypeId = jQuery($gridbrowse.find('tr.editrow [data-formvalidationname="SubCategoryValidation"] input')).val();

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