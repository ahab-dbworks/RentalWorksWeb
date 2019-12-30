class DiscountItemMiscGrid {
    Module: string = 'DiscountItemMiscGrid';
    apiurl: string = 'api/v1/discountitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwBrowse.setFieldValue($control, $generatedtr, 'InventoryTypeId', { value: $tr.find('.field[data-browsedatafield="MiscTypeId"]').attr('data-originalvalue'), text: $tr.find('.field[data-browsedatafield="MiscType"]').attr('data-originalvalue') });
            FwBrowse.setFieldValue($control, $generatedtr, 'CategoryId', { value: $tr.find('.field[data-browsedatafield="CategoryId"]').attr('data-originalvalue'), text: $tr.find('.field[data-browsedatafield="Category"]').attr('data-originalvalue') });
            FwBrowse.setFieldValue($control, $generatedtr, 'SubCategoryId', { value: $tr.find('.field[data-browsedatafield="SubCategoryId"]').attr('data-originalvalue'), text: $tr.find('.field[data-browsedatafield="SubCategory"]').attr('data-originalvalue') });
        });
    };

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {

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
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemiscinventorytype`);
                    break;
                case 'CategoryId':
                    request.uniqueids = {
                        MiscTypeId: MiscTypeValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemisccategory`);
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
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatemiscinventory`);
                    break;
            };
            for (var prop in request.uniqueids) {
                if (request.uniqueids[prop] === '') {
                    delete request.uniqueids[prop];
                }
            }
        }
}

var DiscountItemMiscGridController = new DiscountItemMiscGrid();
//----------------------------------------------------------------------------------------------