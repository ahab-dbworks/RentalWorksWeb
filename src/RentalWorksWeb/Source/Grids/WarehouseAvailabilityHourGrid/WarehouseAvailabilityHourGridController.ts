class WarehouseAvailabilityHourGrid {
    Module: string;
    apiurl: string;

    constructor() {
        this.Module = 'WarehouseAvailabilityHour';
        this.apiurl = 'api/v1/warehouseavailabilityhour';
    }
}

(<any>window).WarehouseAvailabilityHourGridController = new WarehouseAvailabilityHourGrid();
//----------------------------------------------------------------------------------------------