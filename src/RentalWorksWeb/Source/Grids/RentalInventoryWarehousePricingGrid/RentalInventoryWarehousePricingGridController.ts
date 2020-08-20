class RentalInventoryWarehousePricingGrid {
    Module: string = 'InventoryWarehouseGrid';  //justin 04/10/2019 potential issue with shared Module value. See Issue #401
    //apiurl: string = 'api/v1/inventorywarehouse';
    apiurl: string = 'api/v1/pricing';

    generateRow($control, $generatedtr) {
        $generatedtr.find('div[data-browsedatafield="UnitValue"]').on('change', 'input.value', e => {
            calculateReplacementCost($control, $generatedtr);
        });

        const calculateReplacementCost = ($control: any, $tr: any) => {
            const markupReplacementCost = FwBrowse.getValueByDataField($control, $tr, 'MarkupReplacementCost');
            if (markupReplacementCost) {
                const markupPercent = parseFloat(FwBrowse.getValueByDataField($control, $tr, 'ReplacementCostMarkupPercent')) / 100;
                let unitValue = parseFloat(FwBrowse.getValueByDataField($control, $tr, 'UnitValue'));
                unitValue += unitValue * markupPercent;
                FwBrowse.setFieldValue($control, $tr, 'ReplacementCost', { value: unitValue.toString() });
            }
        };
    }

}

var RentalInventoryWarehousePricingGridController = new RentalInventoryWarehousePricingGrid();
//----------------------------------------------------------------------------------------------