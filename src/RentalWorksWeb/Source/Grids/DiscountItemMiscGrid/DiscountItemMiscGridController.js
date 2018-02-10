var DiscountItemMiscGrid = /** @class */ (function () {
    function DiscountItemMiscGrid() {
        this.beforeValidate = function ($browse, $grid, request) {
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
                }
                ;
            }
        };
        this.Module = 'DiscountItemMiscGrid';
        this.apiurl = 'api/v1/discountitem';
    }
    return DiscountItemMiscGrid;
}());
window.DiscountItemMiscGridController = new DiscountItemMiscGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=DiscountItemMiscGridController.js.map