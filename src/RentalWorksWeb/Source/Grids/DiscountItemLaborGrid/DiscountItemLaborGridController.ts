class DiscountItemLaborGrid {
    Module: string = 'DiscountItemLaborGrid';
    apiurl: string = 'api/v1/discountitem';

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $gridbrowse: JQuery, $tr: JQuery) {
        var validationName = request.module;

        if (validationName != null) {
            var LaborTypeValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="LaborTypeValidation"] input')).val();
            var CategoryValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="LaborCategoryValidation"] input')).val();
            var SubCategoryValue = jQuery($gridbrowse.find('tr.editrow [data-validationname="SubCategoryValidation"] input')).val();

            switch (datafield) {
                case 'OrderTypeId':
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateordertype`);
                    break;
                case 'InventoryTypeId':
                    request.uniqueids = {
                        Sales: true
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelaborinventorytype`);
                    break;
                case 'CategoryId':
                    request.uniqueids = {
                        LaborTypeId: LaborTypeValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelaborcategory`);
                    break;
                case 'SubCategoryId':
                    request.uniqueids = {
                        TypeId: LaborTypeValue,
                        CategoryId: CategoryValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubcategory`);
                    break;
                case 'InventoryId':
                    request.uniqueids = {
                        LaborTypeId: LaborTypeValue,
                        CategoryId: CategoryValue,
                        SubCategoryId: SubCategoryValue
                    };
                    $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatelaborinventory`);
                    break;
            };
        }
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

var DiscountItemLaborGridController = new DiscountItemLaborGrid();
//----------------------------------------------------------------------------------------------