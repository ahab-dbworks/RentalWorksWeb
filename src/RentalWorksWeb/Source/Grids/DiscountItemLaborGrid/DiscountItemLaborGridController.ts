class DiscountItemLaborGrid {
    Module: string = 'DiscountItemLaborGrid';
    apiurl: string = 'api/v1/discountitem';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        var validationName = request.module;

        if (validationName != null) {
            var LaborTypeValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="LaborTypeValidation"] input')).val();
            var CategoryValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="LaborCategoryValidation"] input')).val();
            var SubCategoryValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();

            switch (validationName) {
                case 'LaborCategoryValidation':
                    request.uniqueids = {
                        LaborTypeId: LaborTypeValue
                    };
                    break;
                case 'SubCategoryValidation':
                    request.uniqueids = {
                        TypeId: LaborTypeValue,
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

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

var DiscountItemLaborGridController = new DiscountItemLaborGrid();
//----------------------------------------------------------------------------------------------