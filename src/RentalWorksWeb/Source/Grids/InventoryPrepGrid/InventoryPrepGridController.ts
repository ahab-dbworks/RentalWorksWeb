class InventoryPrepGrid {
    Module: string = 'InventoryPrepGrid';
    apiurl: string = 'api/v1/inventoryprep';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="PrepRateId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="PrepDescription"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="PrepUnit"]').text($tr.find('.field[data-browsedatafield="Unit"]').attr('data-originalvalue'));
        });
    };
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'PrepRateId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validatepreprate`);
                break;
        }
    }
}

var InventoryPrepGridController = new InventoryPrepGrid();
//----------------------------------------------------------------------------------------------