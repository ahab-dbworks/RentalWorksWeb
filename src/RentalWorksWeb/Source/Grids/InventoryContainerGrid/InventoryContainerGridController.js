class InventoryContainerGrid {
    constructor() {
        this.Module = 'InventoryContainerGrid';
        this.apiurl = 'api/v1/inventorycontaineritem';
    }
    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"] input').val($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    }
    ;
}
var InventoryContainerGridController = new InventoryContainerGrid();
//# sourceMappingURL=InventoryContainerGridController.js.map