class OrderTypeActivityDatesGrid {
    Module: string = 'OrderTypeActivityDatesGrid';
    apiurl: string = 'api/v1/ordertypedatetype';

    onRowNewMode($control: JQuery, $tr: JQuery) {
        FwBrowse.setFieldValue($control, $tr, 'Enabled', { value: true });
    }
}

var OrderTypeActivityDatesGridController = new OrderTypeActivityDatesGrid();
//----------------------------------------------------------------------------------------------