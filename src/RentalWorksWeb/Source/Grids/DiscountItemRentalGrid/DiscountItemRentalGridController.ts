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

            switch (datafield) {
                case 'OrderTypeId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateordertype`);
                    break;
                case 'InventoryTypeId':
                    request.uniqueids = {
                        Rental: true
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventorytype`);
                    break;
                case 'CategoryId':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                    break;
                case 'SubCategoryId':
                    request.uniqueids = {
                        TypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                    break;
                case 'InventoryId':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId,
                        SubCategoryId: SubCategoryTypeId
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                    break;
            };
        }
    }
}
var DiscountItemRentalGridController = new DiscountItemRentalGrid();
//----------------------------------------------------------------------------------------------