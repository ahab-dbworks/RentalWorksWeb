class InventorySubstituteGrid {
    Module: string = 'InventorySubstituteGrid';
    apiurl: string = 'api/v1/inventorysubstitute';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'SubstituteInventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatesubstituteinventoryrental`);
                break;
        }
    }
}

var InventorySubstituteGridController = new InventorySubstituteGrid();
//----------------------------------------------------------------------------------------------