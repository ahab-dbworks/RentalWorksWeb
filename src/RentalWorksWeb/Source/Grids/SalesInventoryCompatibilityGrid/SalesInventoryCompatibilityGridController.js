class SalesInventoryCompatibilityGrid {
    constructor() {
        this.Module = 'SalesInventoryCompatibilityGrid';
        this.apiurl = 'api/v1/inventorycompatible';
        this.generateRow = ($control, $generatedtr) => {
            $generatedtr.find('div[data-browsedatafield="CompatibleWithInventoryId"]').data('onchange', $tr => {
                $generatedtr.find('.field[data-browsedatafield="CompatibleWithDescription"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            });
        };
    }
}
window.SalesInventoryCompatibilityGridController = new SalesInventoryCompatibilityGrid();
//# sourceMappingURL=SalesInventoryCompatibilityGridController.js.map