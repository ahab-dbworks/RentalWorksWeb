class InventoryCompatibilityGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryCompatibilityGrid';
        this.apiurl = 'api/v1/inventorycompatible';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="CompatibleWithInventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="CompatibleWithDescription"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

var InventoryCompatibilityGridController = new InventoryCompatibilityGrid();
//----------------------------------------------------------------------------------------------