class DiscountItemSalesGrid {
    Module: string = 'DiscountItemSalesGrid';
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
                        CategoryId: CategoryTypeId
                    };
                    break;
                case 'SalesInventoryValidation':
                    request.uniqueids = {
                        InventoryTypeId: InventoryTypeValue,
                        CategoryId: CategoryTypeId,
                        SubCategoryId: SubCategoryTypeId
                    };
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

var DiscountItemSalesGridController = new DiscountItemSalesGrid();
//----------------------------------------------------------------------------------------------