var DiscountItemRentalGrid = (function () {
    function DiscountItemRentalGrid() {
        this.beforeValidate = function ($browse, $grid, request) {
            var validationName = request.module;
            if (validationName != null) {
                var InventoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="InventoryTypeValidation"] input')).val();
                var CategoryTypeId = jQuery($grid.find('tr.editrow [data-validationname="RentalCategoryValidation"] input')).val();
                var SubCategoryTypeId = jQuery($grid.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();
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
                        console.log(CategoryTypeId, InventoryTypeValue, SubCategoryTypeId);
                        break;
                }
                ;
            }
        };
        this.Module = 'DiscountItemRentalGrid';
        this.apiurl = 'api/v1/discountitem';
    }
    DiscountItemRentalGrid.prototype.generateRow = function ($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    ;
    return DiscountItemRentalGrid;
}());
window.DiscountItemRentalGridController = new DiscountItemRentalGrid();
//# sourceMappingURL=DiscountItemRentalGridController.js.map