class InventoryGroupInvGrid {
    Module: string = 'InventoryGroupInvGrid';
    apiurl: string = 'api/v1/inventorygroupinventory';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Category"]').text($tr.find('.field[data-browsedatafield="Category"]').attr('data-originalvalue'));
            $generatedtr.find('.field[data-browsedatafield="Rank"]').text($tr.find('.field[data-browsedatafield="Rank"]').attr('data-originalvalue'));
        });
    };
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'InventoryId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateinventory`);
                break;
        }
    }
}

var InventoryGroupInvGridController = new InventoryGroupInvGrid();
//----------------------------------------------------------------------------------------------