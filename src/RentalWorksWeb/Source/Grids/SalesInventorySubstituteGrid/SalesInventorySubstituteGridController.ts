class SalesInventorySubstituteGrid {
    Module: string = 'SalesInventorySubstituteGrid';
    apiurl: string = 'api/v1/inventorysubstitute';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="SubstituteInventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'SubstituteInventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubstituteinventorysales`);
                break;
        }
    }
}

var SalesInventorySubstituteGridController = new SalesInventorySubstituteGrid();
//----------------------------------------------------------------------------------------------