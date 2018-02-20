class InventoryPrepGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryPrepGrid';
        this.apiurl = 'api/v1/inventoryprep';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="PrepRateId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="PrepDescription"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

var InventoryPrepGridController = new InventoryPrepGrid();
//----------------------------------------------------------------------------------------------