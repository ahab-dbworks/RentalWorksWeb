class InventoryAttributeValueGrid {
    constructor() {
        this.Module = 'InventoryAttributeValueGrid';
        this.apiurl = 'api/v1/inventoryattributevalue';
        this.beforeValidateAttribute = function ($browse, $grid, request, datafield, $tr) {
            request.uniqueIds = {
                HasValues: true,
            };
        };
    }
}
var InventoryAttributeValueGridController = new InventoryAttributeValueGrid();
//# sourceMappingURL=InventoryAttributeValueGridController.js.map