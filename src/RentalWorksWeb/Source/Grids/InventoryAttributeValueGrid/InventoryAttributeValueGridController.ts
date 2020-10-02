class InventoryAttributeValueGrid {
    Module: string = 'InventoryAttributeValueGrid';
    apiurl: string = 'api/v1/inventoryattributevalue';
    //----------------------------------------------------------------------------------------------
    generateRow($control, $generatedtr) {
        FwBrowse.setAfterRenderRowCallback($control, ($tr: JQuery, dt: FwJsonDataTable, rowIndex: number) => {
            if (FwBrowse.getValueByDataField($control, $tr, 'NumericOnly') === "true") {
                $tr.find('[data-browsedatafield="AttributeValueId"]').attr('data-originalvalue', $tr.find('[data-browsedatafield="AttributeValueId"]').attr('data-originaltext')) // Prevents the value from disappearing when going into edit mode
                this.toggleFreeText($generatedtr);
            }
        });

        $generatedtr.find('div[data-browsedatafield="AttributeId"]').data('onchange', $tr => {
            if (FwBrowse.getValueByDataField($control, $tr, 'NumericOnly') === "true") {
                FwBrowse.setFieldValue($control, $generatedtr, 'AttributeValueId', { value: '' }); 
                this.toggleFreeText($generatedtr);
            }
        });
    }
    //----------------------------------------------------------------------------------------------
    toggleFreeText($tr: JQuery) {
        $tr.find('[data-browsedatafield="AttributeValueId"]').attr({ 'data-browsedatatype': 'text', 'data-formdatatype': 'text' });
        $tr.find('[data-browsedatafield="AttributeValueId"] input.value').remove();
        $tr.find('[data-browsedatafield="AttributeValueId"] input.text').removeClass('text').addClass('value').off('change');
        $tr.find('[data-browsedatafield="AttributeValueId"] .btnpeek').hide();
        $tr.find('[data-browsedatafield="AttributeValueId"] .btnvalidate').hide();
        $tr.find('[data-browsedatafield="AttributeValueId"] .sk-fading-circle validation-loader').hide();
        $tr.find('[data-browsedatafield="AttributeValueId"]').attr('data-formdatafield', 'NumericValue');
    }
    //----------------------------------------------------------------------------------------------
    //beforeValidateAttribute = function ($browse, $grid, request, datafield, $tr) {
    //    //let carrierId = $tr.find('.field[data-browsedatafield="CarrierId"] input').val();
    //    request.uniqueIds = {
    //        HasValues: true,
    //    };
    //};
    //----------------------------------------------------------------------------------------------
    beforeValidate(datafield: string, request: any, $validationbrowse: JQuery, $form: JQuery, $tr: JQuery) {
        switch (datafield) {
            case 'AttributeId':
                request.uniqueids = {
                    HasValues: true
                }
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateattribute`);
                break;
            case 'AttributeValueId':
                $validationbrowse.attr('data-apiurl', `${this.apiurl}/validateattributevalue`);
        }
    }
}

var InventoryAttributeValueGridController = new InventoryAttributeValueGrid();
//----------------------------------------------------------------------------------------------