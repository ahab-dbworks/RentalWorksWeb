class DiscountItemMiscGrid {
    Module: string = 'DiscountItemMiscGrid';
    apiurl: string = 'api/v1/discountitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        var validationName = request.module;

        if (validationName != null) {
            var MiscTypeValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="MiscTypeValidation"] input')).val();
            var CategoryValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="MiscCategoryValidation"] input')).val();
            var SubCategoryValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();
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
                        MiscTypeId: MiscTypeValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatecategory`);
                    break;
                case 'SubCategoryId':
                    request.uniqueids = {
                        TypeId: MiscTypeValue,
                        CategoryId: CategoryValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                    break;
                case 'InventoryId':
                    request.uniqueids = {
                        MiscTypeId: MiscTypeValue,
                        CategoryId: CategoryValue,
                        SubCategoryId: SubCategoryValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                    break;
            };
        }
    }
}

var DiscountItemMiscGridController = new DiscountItemMiscGrid();
//----------------------------------------------------------------------------------------------