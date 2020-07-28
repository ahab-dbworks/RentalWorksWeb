class OrderBillingScheduleGrid {
    Module: string = 'OrderBillingScheduleGrid';
    apiurl: string = 'api/v1/orderbillingschedule';

    addLegend($control) {
        FwBrowse.addLegend($control, 'Hiatus', '#008000');
    }
}

var OrderBillingScheduleGridController = new OrderBillingScheduleGrid();
//----------------------------------------------------------------------------------------------