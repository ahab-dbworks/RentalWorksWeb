class DealHiatusDiscountGrid {
    Module: string = 'DealHiatusDiscountGrid';
    apiurl: string = 'api/v1/dealhiatusdiscount';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        FwBrowse.setFieldValue($control, $tr, 'HiatusDiscountPercent', { value: "100" });
    }
}

var DealHiatusDiscountGridController = new DealHiatusDiscountGrid();
//----------------------------------------------------------------------------------------------