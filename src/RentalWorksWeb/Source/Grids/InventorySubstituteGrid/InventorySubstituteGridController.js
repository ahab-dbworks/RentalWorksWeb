class InventorySubstituteGrid {
    constructor() {
        this.Module = 'InventorySubstituteGrid';
        this.apiurl = 'api/v1/inventorysubstitute';
    }
    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="InventoryId"]').data('onchange', function ($tr) {
            $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
        });
    }
    ;
}
var InventorySubstituteGridController = new InventorySubstituteGrid();
//# sourceMappingURL=InventorySubstituteGridController.js.map