class SalesInventoryCompatibilityGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'SalesInventoryCompatibilityGrid';
        this.apiurl = 'api/v1/inventorycompatible';
    }

    generateRow = ($control, $generatedtr) => {
        $generatedtr.find('div[data-browsedatafield="CompatibleWithInventoryId"]').data('onchange', $tr => {
            $generatedtr.find('.field[data-browsedatafield="CompatibleWithDescription"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

(<any>window).SalesInventoryCompatibilityGridController = new SalesInventoryCompatibilityGrid();
//----------------------------------------------------------------------------------------------