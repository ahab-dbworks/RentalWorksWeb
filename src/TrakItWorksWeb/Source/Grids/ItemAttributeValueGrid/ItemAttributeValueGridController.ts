class ItemAttributeValueGrid {
    Module: string = 'ItemAttributeValueGrid';
    apiurl: string = 'api/v1/itemattributevalue';

    init($control: JQuery) {
        $control.on('change', '[data-formdatafield="AttributeId"] input.value', function (e) {
            $control.find('[data-formdatafield="AttributeValueId"] input.value').val('');
            $control.find('[data-formdatafield="AttributeValueId"] input.text').val('');
        });
    }
}

var ItemAttributeValueGridController = new ItemAttributeValueGrid();
//----------------------------------------------------------------------------------------------