class DiscountItemRentalGrid {
    Module: string = 'DiscountItemRentalGrid';
    apiurl: string = 'api/v1/discountitem';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            FwBrowse.setFieldValue($control, $generatedtr, 'InventoryTypeId', { value: $tr.find('.field[data-browsedatafield="InventoryTypeId"]').attr('data-originalvalue'), text: $tr.find('.field[data-browsedatafield="InventoryType"]').attr('data-originalvalue') });
            FwBrowse.setFieldValue($control, $generatedtr, 'CategoryId', { value: $tr.find('.field[data-browsedatafield="CategoryId"]').attr('data-originalvalue'), text: $tr.find('.field[data-browsedatafield="Category"]').attr('data-originalvalue') });
            FwBrowse.setFieldValue($control, $generatedtr, 'SubCategoryId', { value: $tr.find('.field[data-browsedatafield="SubCategoryId"]').attr('data-originalvalue'), text: $tr.find('.field[data-browsedatafield="SubCategory"]').attr('data-originalvalue') });
        });
    };

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        var validationName = request.module;
        if (validationName != null) {
            var InventoryTypeValue = FwBrowse.getValueByDataField($validationbrowse, $tr, 'InventoryTypeId');
            var CategoryTypeId = FwBrowse.getValueByDataField($validationbrowse, $tr, 'CategoryId');
            var SubCategoryTypeId = FwBrowse.getValueByDataField($validationbrowse, $tr, 'SubCategoryId');

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

            for (var prop in request.uniqueids) {
                if (request.uniqueids[prop] === '') {
                    delete request.uniqueids[prop];
                }
            }
        }
    }
}
var DiscountItemRentalGridController = new DiscountItemRentalGrid();
//----------------------------------------------------------------------------------------------