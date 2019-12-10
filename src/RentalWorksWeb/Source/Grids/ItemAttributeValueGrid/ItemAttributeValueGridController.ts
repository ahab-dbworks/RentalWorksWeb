class ItemAttributeValueGrid {
    Module: string = 'ItemAttributeValueGrid';
    apiurl: string = 'api/v1/itemattributevalue';

    init($control: JQuery) {
        $control.on('change', '[data-formdatafield="AttributeId"] input.value', function (e) {
            $control.find('[data-formdatafield="AttributeValueId"] input.value').val('');
            $control.find('[data-formdatafield="AttributeValueId"] input.text').val('');
        });
    }

    //----------------------------------------------------------------------------------------------
    beforeValidateAttribute = function ($browse, $grid, request, datafield, $tr) {
        request.uniqueIds = {
            HasValues: true,
        };
    };
    //----------------------------------------------------------------------------------------------

    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'AttributeId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateattribute`);
                break;
            case 'AttributeValueId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateattributevalue`);
                break;
        }
    }
}

var ItemAttributeValueGridController = new ItemAttributeValueGrid();
//----------------------------------------------------------------------------------------------