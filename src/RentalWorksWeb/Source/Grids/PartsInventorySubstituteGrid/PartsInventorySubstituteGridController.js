class PartsInventorySubstituteGrid {
    constructor() {
        this.Module = 'PartsInventorySubstituteGrid';
        this.apiurl = 'api/v1/inventorysubstitute';
        this.generateRow = ($control, $generatedtr) => {
            $generatedtr.find('div[data-browsedatafield="SubstituteInventoryId"]').data('onchange', $tr => {
                $generatedtr.find('.field[data-browsedatafield="Description"]').text($tr.find('.field[data-browsedatafield="Description"]').attr('data-originalvalue'));
            });
        };
    }
}
var PartsInventorySubstituteGridController = new PartsInventorySubstituteGrid();
//# sourceMappingURL=PartsInventorySubstituteGridController.js.map