class DiscountItemSalesGrid {
    Module: string = 'DiscountItemSalesGrid';
    apiurl: string = 'api/v1/discountitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        var validationName = request.module;

        if (validationName != null) {
            var InventoryTypeValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="InventoryTypeValidation"] input')).val();
            var CategoryTypeValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="SalesCategoryValidation"] input')).val();
            var SubCategoryTypeValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();
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