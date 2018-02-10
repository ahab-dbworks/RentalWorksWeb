var DiscountItemSalesGrid = /** @class */ (function () {
    function DiscountItemSalesGrid() {
        this.beforeValidate = function ($browse, $grid, request) {
            var validationName = request.module;
            if (validationName != null) {
                var InventoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="InventoryTypeValidation"] input')).val();
                var CategoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="SalesCategoryValidation"] input')).val();
                var SubCategoryTypeValue = jQuery($grid.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();
                console.log(InventoryTypeValue);
                console.log(CategoryTypeValue);
                console.log(SubCategoryTypeValue);
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
                }
                ;
            }
        };
        this.Module = 'DiscountItemSalesGrid';
        this.apiurl = 'api/v1/discountitem';
    }
    return DiscountItemSalesGrid;
}());
window.DiscountItemSalesGridController = new DiscountItemSalesGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=DiscountItemSalesGridController.js.map