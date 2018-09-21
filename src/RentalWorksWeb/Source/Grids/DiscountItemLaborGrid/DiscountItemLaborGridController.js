class DiscountItemLaborGrid {
    constructor() {
        this.Module = 'DiscountItemLaborGrid';
        this.apiurl = 'api/v1/discountitem';
        this.beforeValidate = function ($browse, $grid, request) {
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
                }
                ;
            }
        };
    }
    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    }
    ;
}
var DiscountItemLaborGridController = new DiscountItemLaborGrid();
//# sourceMappingURL=DiscountItemLaborGridController.js.map