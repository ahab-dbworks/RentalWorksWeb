var ItemAttributeValueGrid = /** @class */ (function () {
    function ItemAttributeValueGrid() {
        this.Module = 'ItemAttributeValueGrid';
        this.apiurl = 'api/v1/itemattributevalue';
    }
    ItemAttributeValueGrid.prototype.init = function ($control) {
        $control.on('change', '[data-formdatafield="AttributeId"] input.value', function (e) {
            $control.find('[data-formdatafield="AttributeValueId"] input.value').val('');
            $control.find('[data-formdatafield="AttributeValueId"] input.text').val('');
        });
    };
    return ItemAttributeValueGrid;
}());
window.ItemAttributeValueGridController = new ItemAttributeValueGrid();
//---------------------------------------------------------------------------------------------- 
//# sourceMappingURL=ItemAttributeValueGridController.js.map