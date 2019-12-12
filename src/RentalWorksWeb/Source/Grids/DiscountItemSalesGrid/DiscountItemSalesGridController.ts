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
            switch (datafield) {
                case 'OrderTypeId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateordertype`);
                    break;
                case 'InventoryTypeId':
                    request.uniqueids = {
                        Sales: true
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
                        CategoryId: CategoryTypeValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                    break;
                case 'InventoryId':
                    request.uniqueids = {
                        nventoryTypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeValue,
                        SubCategoryId: SubCategoryTypeValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                    break;
            };
        }
    }
}

var DiscountItemSalesGridController = new DiscountItemSalesGrid();
//----------------------------------------------------------------------------------------------