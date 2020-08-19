class ItemAttributeValueGrid {
    constructor() {
        this.Module = 'ItemAttributeValueGrid';
        this.apiurl = 'api/v1/itemattributevalue';
    }
    init($control) {
        $control.on('change', '[data-formdatafield="AttributeId"] input.value', function (e) {
            $control.find('[data-formdatafield="AttributeValueId"] input.value').val('');
            $control.find('[data-formdatafield="AttributeValueId"] input.text').val('');
        });
    }
}
var ItemAttributeValueGridController = new ItemAttributeValueGrid();
//# sourceMappingURL=ItemAttributeValueGridController.js.map