class InventoryContainerGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'InventoryContainerGrid';
        this.apiurl = 'api/v1/inventorycontaineritem';
    }

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    };
}

(<any>window).InventoryContainerGridController = new InventoryContainerGrid();
//----------------------------------------------------------------------------------------------