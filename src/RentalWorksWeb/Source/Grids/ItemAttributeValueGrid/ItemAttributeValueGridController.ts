class ItemAttributeValueGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'ItemAttributeValueGrid';
        this.apiurl = 'api/v1/itemattributevalue';
    }

    init($control: JQuery) {
        $control.on('change', '[data-formdatafield="AttributeId"] input.value', function (e) {
            $control.find('[data-formdatafield="AttributeValueId"] input.value').val('');
            $control.find('[data-formdatafield="AttributeValueId"] input.text').val('');
        });
    }
}

(<any>window).ItemAttributeValueGridController = new ItemAttributeValueGrid();
//----------------------------------------------------------------------------------------------