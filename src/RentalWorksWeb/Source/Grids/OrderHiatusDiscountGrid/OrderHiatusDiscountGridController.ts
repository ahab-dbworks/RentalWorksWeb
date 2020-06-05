class OrderHiatusDiscountGrid {
    Module: string = 'OrderHiatusDiscountGrid';
    apiurl: string = 'api/v1/orderhiatusdiscount';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        FwBrowse.setFieldValue($control, $tr, 'DiscountPercent', { value: "100" });
    }
}

var OrderHiatusDiscountGridController = new OrderHiatusDiscountGrid();
//----------------------------------------------------------------------------------------------